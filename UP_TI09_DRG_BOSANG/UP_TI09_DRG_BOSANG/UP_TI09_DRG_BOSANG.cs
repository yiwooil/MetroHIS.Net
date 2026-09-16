using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;   // .xls
using NPOI.XSSF.UserModel;   // .xlsx

namespace UP_TI09_DRG_BOSANG
{
    public partial class UP_TI09_DRG_BOSANG : Form
    {
        public UP_TI09_DRG_BOSANG()
        {
            InitializeComponent();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            string filename = "";
            try
            {
                using (OpenFileDialog diag = new OpenFileDialog())
                {
                    if (diag.ShowDialog() == DialogResult.OK)
                    {
                        filename = diag.FileName;
                    }
                }
                if (filename == "") return;

                txtFilename.Text = filename;
                Application.DoEvents();

                ReadSelectedExcelFile();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReadSelectedExcelFile()
        {

            try
            {
                string filename = txtFilename.Text.ToString().Trim();
                if (filename == "") return;

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "엑셀 파일을 읽는 중입니다.");
                this.ReadExcel_NPOI(filename);
                this.CloseProgressForm("", "");
                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void ReadExcel_NPOI(string filePath)
        {
            grdExcel.DataSource = null;
            List<CDataExcel> list = new List<CDataExcel>();
            grdExcel.DataSource = list;

            RefreshGridExcel();

            IWorkbook wb = null;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                string ext = Path.GetExtension(filePath).ToLower();

                if (ext == ".xls")
                    wb = new HSSFWorkbook(fs);
                else if (ext == ".xlsx")
                    wb = new XSSFWorkbook(fs);
                else
                    throw new Exception("지원하지 않는 파일 형식입니다: " + ext);
            }

            // 엑셀 파일명에 포함된 문구를 확인하여 자료 구분을 자동으로 설정한다.
            // 앞서 열었던 파일의 구분이 남지 않도록 해당 문구가 없으면 빈 값으로 초기화한다.
            txtGubun.Text = GetGubunFromFileName(filePath);

            ISheet sheet = wb.GetSheetAt(0);
            if (sheet == null)
                return;

            // NPOI의 FirstRowNum은 값이 있는 첫 행이 아니라
            // 엑셀 내부에 생성되어 있는 첫 행을 반환할 수 있다.
            // 따라서 실제 값이 있는 첫 번째 행과 열 범위를 직접 찾는다.
            int firstRow = -1;
            int lastRow = sheet.LastRowNum;
            int firstCol = -1;
            int lastCol = -1;
            IRow headerRow = null;

            for (int r = sheet.FirstRowNum; r <= lastRow; r++)
            {
                IRow row = sheet.GetRow(r);

                // 실제 셀이 없는 행은 FirstCellNum과 LastCellNum이 -1이다.
                if (row == null || row.FirstCellNum < 0 || row.LastCellNum < 0)
                    continue;

                int rowFirstCol = -1;
                int rowLastCol = -1;

                // 서식만 설정된 빈 셀은 제외하고 실제 값이 있는 열 범위를 찾는다.
                for (int c = row.FirstCellNum; c < row.LastCellNum; c++)
                {
                    string cellText = GetCellText(row.GetCell(c));

                    if (string.IsNullOrEmpty(cellText.Trim()))
                        continue;

                    if (rowFirstCol < 0)
                        rowFirstCol = c;

                    // LastCellNum과 동일하게 마지막 열 번호 + 1로 저장한다.
                    rowLastCol = c + 1;
                }

                // 값이 하나도 없는 행은 다음 행을 검사한다.
                if (rowFirstCol < 0)
                    continue;

                firstRow = r;
                firstCol = rowFirstCol;
                lastCol = rowLastCol;
                headerRow = row;
                break;
            }

            RefreshGridExcel();

            // 시트 전체에 읽을 수 있는 값이 없는 경우
            if (headerRow == null || firstCol < 0 || lastCol <= firstCol)
                return;

            // CDataExcel에서 지원하는 최대 열 수로 제한한다.
            int colCount = lastCol - firstCol;
            if (colCount > CDataExcel.MAX_COUNT) colCount = CDataExcel.MAX_COUNT;

            for (int r = firstRow; r <= lastRow; r++)
            {
                IRow row = sheet.GetRow(r);
                if (row == null) continue;

                // 빈 행 스킵 (ReadExcel()과 동일한 의도)
                bool isEmptyRow = true;
                for (int c = 0; c < colCount; c++)
                {
                    string v = GetCellText(row.GetCell(firstCol + c));
                    if (!string.IsNullOrEmpty(v))
                    {
                        isEmptyRow = false;
                        break;
                    }
                }
                if (isEmptyRow) continue;

                CDataExcel data = new CDataExcel();
                data.Clear();

                for (int c = 0; c < colCount; c++)
                {
                    // Interop 버전은 object를 담지만, NPOI는 일단 string으로 담는 것이 안전합니다.
                    // (필요하면 숫자/날짜를 object로 유지하도록 확장 가능)
                    data.COL_VLAUE[c] = GetCellText(row.GetCell(firstCol + c));

                    // 제목행에서 필요한 컬럼 인덱스 찾기 (ReadExcel()과 동일 로직)
                    if (r == firstRow)
                    {
                        string header = (data.COL_VLAUE[c] == null) ? "" : data.COL_VLAUE[c].ToString();
                    }
                }

                data.ERR_CD = "";
                data.ERR_MSG = "";

                // 먼저 엑셀에서 읽은 행을 추가한다.
                list.Add(data);

                // 첫 번째 헤더 행을 추가한 직후,
                // 두 번째 행에 TI09_DRG_BOSANG 테이블의 필드명을 표시한다.
                if (r == firstRow)
                {
                    CDataExcel fieldRow =
                        CreateTi09DrgBosangFieldRow(data, colCount);

                    list.Add(fieldRow);
                    RefreshGridExcel();
                }

                // 기존 ReadExcel()은 매 row마다 RefreshGridExcel() 호출
                // 동일 동작을 원하면 아래 유지 (성능상 부담이면 주석 처리 권장)
                if (r % 1000 == 0) RefreshGridExcel();
            }

            RefreshGridExcel();
        }

        private CDataExcel CreateTi09DrgBosangFieldRow(CDataExcel headerRow, int colCount)
        {
            // 엑셀의 한글 헤더명과 TI09_DRG_BOSANG 테이블 필드명을 연결한다.
            Dictionary<string, string> fieldMap = new Dictionary<string, string>(StringComparer.Ordinal)
            {
                { "분류번호", "BUNCD" },
                { "코드", "PCODE" },
                { "명칭", "PCODENM" },
                { "산정명칭", "SPEC" },
                { "보상률", "BOSANG_RT" },
                { "급여구분", "GUB_GB" },
                { "질병군적용시작일자", "ADTDT" },
                { "질별군적용시작일자", "ADTDT" },
                { "질병군적용종료일자", "ENDDT" },
                { "수술구분", "OPR_GB" },
                { "행위관련근거", "NOTI_NO" },
                { "세분류", "D_CAT" },
                { "비고", "REMARK" },
                { "중분류", "M_CAT" },
                { "중분류명", "M_CAT_NM" },
                { "제품명", "PCODENM" },
                { "질병군관련근거", "NOTI_NO" },
                { "제조회사", "MKCNM" },
                { "재질", "METERIAL" },
                { "수입(판매)업소", "MKCNMK" },
                { "규격", "PTYPE" },
                { "단위", "PDUT" }
            };

            CDataExcel fieldRow = new CDataExcel();
            fieldRow.Clear();

            for (int c = 0; c < colCount; c++)
            {
                string headerText = headerRow.COL_VLAUE[c] == null
                    ? ""
                    : headerRow.COL_VLAUE[c].ToString();
                string normalizedHeader = NormalizeExcelHeader(headerText);
                string fieldName;

                if (fieldMap.TryGetValue(normalizedHeader, out fieldName))
                    fieldRow.COL_VLAUE[c] = fieldName;
                else
                    fieldRow.COL_VLAUE[c] = "";
            }

            fieldRow.ERR_CD = "";
            fieldRow.ERR_MSG = "";

            return fieldRow;
        }

        private string NormalizeExcelHeader(string headerText)
        {
            if (string.IsNullOrEmpty(headerText))
                return "";

            // 병합되거나 줄 바꿈된 헤더도 같은 이름으로 찾을 수 있도록
            // 공백, 탭 및 개행 문자를 제거한다.
            return headerText.Trim()
                .Replace(" ", "")
                .Replace("\t", "")
                .Replace("\r", "")
                .Replace("\n", "");
        }

        private string GetGubunFromFileName(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            // "치료재료"에도 "재료"가 포함되므로 두 형태를 모두 처리할 수 있다.
            // 파일명에 두 문구가 모두 있는 경우에는 "재료"를 우선한다.
            if (fileName.IndexOf("재료", StringComparison.Ordinal) >= 0)
                return "재료";

            if (fileName.IndexOf("행위", StringComparison.Ordinal) >= 0)
                return "행위";

            return "";
        }

        private string GetCellText(ICell cell)
        {
            if (cell == null) return "";

            // 수식은 계산된 결과를 우선 (필요 시 evaluator 추가 가능)
            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue ?? "";
                case CellType.Numeric:
                    // 날짜 포맷이면 날짜 문자열로
                    if (DateUtil.IsCellDateFormatted(cell))
                        return cell.DateCellValue.ToString("yyyy-MM-dd");
                    return cell.NumericCellValue.ToString();
                case CellType.Boolean:
                    return cell.BooleanCellValue ? "1" : "0";
                case CellType.Formula:
                    // 표시 문자열로 처리(간단/안전)
                    return cell.ToString();
                case CellType.Error:
                case CellType.Blank:
                default:
                    return "";
            }
        }

        private void RefreshGridExcel()
        {
            if (grdExcel.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdExcel.BeginInvoke(new Action(() => grdExcelView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdExcelView.RefreshData();
                Application.DoEvents();
            }
        }

        private void ShowProgressForm(String caption, String description)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
            DevExpress.XtraSplashScreen.SplashScreenManager.Default.SetWaitFormCaption(caption);
            DevExpress.XtraSplashScreen.SplashScreenManager.Default.SetWaitFormDescription(description);
        }

        private void CloseProgressForm(String caption, String description)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
        }

        private void btnMake_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "자료 올리기 중입니다.");
                this.Make();
                this.CloseProgressForm("", "");
                Cursor.Current = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Make()
        {
            // txtGubun 값으로 DB의 GUBUN 값을 결정한다.
            string gubun = GetGubunCode();

            if (gubun == "")
            {
                MessageBox.Show("구분은 '행위' 또는 '재료'만 가능합니다.");
                return;
            }

            List<Dictionary<string, string>> dataList = MakeDataList();

            if (dataList.Count == 0)
            {
                MessageBox.Show("처리할 자료가 없습니다.");
                return;
            }

            // 실제 처리 건수
            int ins_upd_count = 0;

            // dataList를 순회하면서 TI09_DRG_BOSANG에 UPDATE 또는 INSERT
            string connectionString = GetServerConnectionString_CODE();
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                string sysdt = MetroLib.Util.GetSysDate(conn);

                for (int i = 0; i < dataList.Count; i++)
                {
                    this.ShowProgressForm("", "자료 올리기 중입니다.(" + ins_upd_count + "/" + (i + 1) + "/" + dataList.Count + ")");

                    Dictionary<string, string> rowData = dataList[i];

                    string pcode = rowData["PCODE"].ToString();
                    string adtdt = NormalizeDateValue(rowData["ADTDT"].ToString());

                    Dictionary<string, string> record = GetRecord(pcode, adtdt, gubun, conn);
                    if (record.Count == 0)
                    {
                        InsertData(pcode, adtdt, gubun, sysdt, rowData, conn);
                        ins_upd_count++;
                    }
                    else
                    {
                        if (IsSame(rowData, record) == true)
                        {
                            // 자료가 같으므로 제외
                        }
                        else
                        {
                            UpdateData(pcode, adtdt, gubun, sysdt, rowData, conn);
                            ins_upd_count++;
                        }
                    }
                }
            }

            MessageBox.Show(ins_upd_count + " 건이 처리되었습니다.");
        }

        private string GetGubunCode()
        {
            string gubunText = txtGubun.Text.Trim();

            if (gubunText == "행위")
                return "1";

            if (gubunText == "재료")
                return "2";

            return "";
        }

        private List<Dictionary<string, string>> MakeDataList()
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();

            // 그리드에서 편집 중인 값이 있으면 DataSource에 반영한다.
            grdExcelView.PostEditor();
            grdExcelView.UpdateCurrentRow();

            List<CDataExcel> gridRows = grdExcel.DataSource as List<CDataExcel>;

            if (gridRows == null || gridRows.Count < 3)
                return result;

            // 0행은 엑셀 헤더이고 1행은 테이블 필드명이다.
            CDataExcel fieldRow = gridRows[1];

            // 2행부터 실제 데이터이다.
            for (int rowIndex = 2; rowIndex < gridRows.Count; rowIndex++)
            {
                CDataExcel dataRow = gridRows[rowIndex];

                Dictionary<string, string> rowData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                bool hasValue = false;

                for (int columnIndex = 0; columnIndex < CDataExcel.MAX_COUNT; columnIndex++)
                {
                    string fieldName = GetGridCellValue(fieldRow, columnIndex).Trim().ToUpperInvariant();

                    // 필드명이 없는 컬럼은 DB 저장 대상이 아니다.
                    if (fieldName == "")
                        continue;

                    string fieldValue = GetGridCellValue(dataRow, columnIndex).Trim();

                    /*
                     * 동일한 필드명이 두 번 나온 경우:
                     * - 기존 값이 비어 있으면 새 값으로 교체
                     * - 새 값이 비어 있지 않으면 마지막 값을 사용
                     * - 새 값이 비어 있으면 기존 값을 유지
                     */
                    if (!rowData.ContainsKey(fieldName))
                    {
                        rowData.Add(fieldName, fieldValue);
                    }
                    else if (fieldValue != "")
                    {
                        rowData[fieldName] = fieldValue;
                    }

                    if (fieldValue != "")
                        hasValue = true;
                }

                // 매핑된 필드에 값이 하나라도 있는 행만 List에 추가한다.
                if (hasValue)
                    result.Add(rowData);
            }

            return result;
        }

        private string GetGridCellValue(CDataExcel row, int columnIndex)
        {
            if (row == null ||
                columnIndex < 0 ||
                columnIndex >= CDataExcel.MAX_COUNT ||
                row.COL_VLAUE[columnIndex] == null)
            {
                return "";
            }

            return row.COL_VLAUE[columnIndex].ToString();
        }

        private Dictionary<string,string>GetRecord(string pcode, string adtdt, string gubun, OleDbConnection p_conn)
        {
            Dictionary<string, string> record = new Dictionary<string, string>();

            // 존재 여부만 확인하므로 첫 번째 자료 한 건만 조회한다.
            string sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI09_DRG_BOSANG";
            sql += Environment.NewLine + " WHERE PCODE = ?";
            sql += Environment.NewLine + "   AND ADTDT = ?";
            sql += Environment.NewLine + "   AND GUBUN = ?";

            // OleDb 파라미터는 이름이 아니라 추가한 순서대로 연결된다.
            List<object> para = new List<object>();
            para.Add(pcode);
            para.Add(adtdt);
            para.Add(gubun);

            MetroLib.SqlHelper.GetDataReader(sql, para, p_conn, delegate(OleDbDataReader reader)
            {
                for (int c = 0; c < reader.FieldCount; c++)
                {
                    if (reader.GetName(c) == "PCODE" || reader.GetName(c) == "ADTDT" || reader.GetName(c) == "GUBUN")
                    {
                        // 제외
                    }
                    else
                    {
                        record.Add(reader.GetName(c), reader[c].ToString());
                    }
                }
                // 존재 여부만 확인하면 되므로 첫 번째 행에서 조회를 중단한다.
                return MetroLib.SqlHelper.BREAK;
            });

            return record;
        }

        private string NormalizeDateValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            return value.Trim()
                .Replace("-", "")
                .Replace(".", "")
                .Replace("/", "");
        }

        private void UpdateData(string p_pcode, string p_adtdt, string p_gubun, string p_sysdt, Dictionary<string, string> p_rowData, OleDbConnection p_conn)
        {
            List<string> flds = new List<string>();
            List<object> para = new List<object>();

            foreach (KeyValuePair<string, string> item in p_rowData)
            {
                string fieldName = item.Key.Trim().ToUpperInvariant();

                // 테이블의 조회 조건으로 사용하는 키 필드는 SET에서 제외한다.
                if (fieldName == "PCODE" || fieldName == "ADTDT") continue;

                string fieldValue = item.Value == null ? "" : item.Value.Trim();

                if (fieldName == "ENDDT")
                {
                    fieldValue = NormalizeDateValue(fieldValue);
                    if (fieldValue == "99991231") fieldValue = "";
                }

                flds.Add(fieldName);
                para.Add(fieldValue);
            }

            StringBuilder sql = new StringBuilder();

            sql.AppendLine("UPDATE TI09_DRG_BOSANG");
            sql.Append("   SET ");

            for (int i = 0; i < flds.Count; i++)
            {
                if (i > 0)
                    sql.AppendLine(",");

                sql.Append("       [");
                sql.Append(flds[i]);
                sql.Append("] = ?");
            }

            // CHGDT는 Dictionary의 값과 관계없이 항상 p_sysdt로 갱신한다.
            if (flds.Count > 0)
                sql.AppendLine(",");

            sql.Append("             [CHGDT] = ?");
            para.Add(p_sysdt);

            sql.AppendLine();
            sql.AppendLine(" WHERE PCODE = ?");
            sql.AppendLine("   AND ADTDT = ?");
            sql.AppendLine("   AND GUBUN = ?");

            // OleDb 파라미터는 SQL의 물음표 순서와 같아야 한다.
            para.Add(p_pcode);
            para.Add(p_adtdt);
            para.Add(p_gubun);

            MetroLib.SqlHelper.ExecuteSql(sql.ToString(), para, p_conn, null);
        }

        private void InsertData(string p_pcode, string p_adtdt, string p_gubun, string p_sysdt, Dictionary<string, string> p_rowData, OleDbConnection p_conn)
        {
            List<string> flds = new List<string>();
            List<object> para = new List<object>();

            foreach (KeyValuePair<string, string> item in p_rowData)
            {
                string fieldName = item.Key.Trim().ToUpperInvariant();

                // 테이블의 조회 조건으로 사용하는 키 필드는 SET에서 제외한다.
                if (fieldName == "PCODE" || fieldName == "ADTDT") continue;

                string fieldValue = item.Value == null ? "" : item.Value.Trim();

                if (fieldName == "ENDDT")
                {
                    fieldValue = NormalizeDateValue(fieldValue);
                    if (fieldValue == "99991231") fieldValue = "";
                }

                flds.Add(fieldName);
                para.Add(fieldValue);
            }

            StringBuilder sql = new StringBuilder();

            sql.Append("INSERT INTO TI09_DRG_BOSANG");
            sql.Append("(");

            for (int i = 0; i < flds.Count; i++)
            {
                sql.Append("[");
                sql.Append(flds[i]);
                sql.Append("],");
            }

            // CREDT는 Dictionary의 값과 관계없이 항상 p_sysdt로 갱신한다.
            sql.Append("CREDT,PCODE,ADTDT,GUBUN)");
            sql.AppendLine("");
            sql.Append("VALUES (");

            for (int i = 0; i < flds.Count; i++)
                sql.Append("?,");

            sql.AppendLine("?,?,?,?)");


            // OleDb 파라미터는 SQL의 물음표 순서와 같아야 한다.
            para.Add(p_sysdt);
            para.Add(p_pcode);
            para.Add(p_adtdt);
            para.Add(p_gubun);

            MetroLib.SqlHelper.ExecuteSql(sql.ToString(), para, p_conn, null);
        }

        private bool IsSame(Dictionary<string, string> p_rowData, Dictionary<string, string> p_record)
        {
            foreach (KeyValuePair<string, string> item in p_rowData)
            {
                string fieldName = item.Key.Trim().ToUpperInvariant();

                // 테이블 조회 조건으로 사용하는 키 필드는 비교에서 제외한다.
                if (fieldName == "PCODE" || fieldName == "ADTDT" || fieldName == "GUBUN") continue;

                string rowValue = item.Value == null ? "" : item.Value.Trim();
                string recordValue;

                // p_record에 동일한 필드가 없으면 서로 다른 자료로 판단한다.
                if (!p_record.TryGetValue(fieldName, out recordValue)) return false;

                recordValue = recordValue == null ? "" : recordValue.Trim();

                // UPDATE에서 사용하는 날짜 변환 규칙과 동일하게 처리한다.
                if (fieldName == "ENDDT")
                {
                    rowValue = NormalizeDateValue(rowValue);
                    recordValue = NormalizeDateValue(recordValue);

                    if (rowValue == "99991231") rowValue = "";
                    if (recordValue == "99991231") recordValue = "";
                }

                // 보상률은 문자열 형식이 아닌 실제 숫자 값으로 비교한다.
                if (fieldName == "BOSANG_RT")
                {
                    double rowDouble;
                    double recordDouble;

                    // 어느 한쪽이라도 숫자로 변환할 수 없으면 다른 값으로 판단한다.
                    if (!double.TryParse(rowValue, out rowDouble) || !double.TryParse(recordValue, out recordDouble)) return false;

                    if (rowDouble != recordDouble) return false;

                    // BOSANG_RT의 문자열 비교는 수행하지 않는다.
                    continue;
                }

                // 한 필드라도 값이 다르면 즉시 false를 반환한다.
                if (!string.Equals(rowValue, recordValue, StringComparison.Ordinal)) return false;
            }

            // 모든 비교 대상 필드의 값이 동일하다.
            return true;
        }

        private string GetServerConnectionString_CODE()
        {
            string strDBServer = GetUrlSource("http://www.metrosoft.co.kr/emr/codeserver.asp", "http://180.70.20.22/emr/codeserver.asp");

            if ("".Equals(strDBServer) == true)
            {
                strDBServer = "180.70.20.26,3515";
            }

            string strDBName = "CODE";
            string strUid = "sa";
            string strPwd = "mms";
            string strRet = GetUrlSource("http://www.metrosoft.co.kr/emr/codeserver2.asp", "http://180.70.20.22/emr/codeserver2.asp");
            string[] aRet = strRet.Split(new[] { "\r\n", "\r", "\n", " " }, StringSplitOptions.None);
            if (aRet[0] != "") strUid = aRet[0];
            if (aRet[1] != "") strPwd = aRet[1];

            string strConn = "Provider=SQLOLEDB.1;Password=" + strPwd + ";Persist Security Info=true;User ID=" + strUid + ";Initial Catalog=" + strDBName + ";Data Source=" + strDBServer + "";

            return strConn;
        }

        private string GetUrlSource(string url, string url2)
        {
            string urlSource = "";
            try
            {
                urlSource = GetUrlSourceInner(url);
                return urlSource;
            }
            catch (Exception e1)
            {
                // www.metrosoft.co.kr은 접속이 안되고
                // 180.70.20.22 로는 접속이 되는 병원이 있음.
            }
            try
            {
                urlSource = GetUrlSourceInner(url2);
                return urlSource;
            }
            catch (Exception e2)
            {
                // 180.70.20.22 로도 접속이 안되는 병원이 있음.
            }
            return urlSource;
        }

        private string GetUrlSourceInner(string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            string urlSource = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            return urlSource;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "배포 자료 만들기 중입니다.");
                this.Release();
                this.CloseProgressForm("", "");
                Cursor.Current = Cursors.Default;

                MessageBox.Show("작업이 완료되었습니다.");

            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Release()
        {
            // 경로와 확장자를 제외한 파일제목을 사용한다.
            string fileTitle = Path.GetFileNameWithoutExtension(txtFilename.Text.Trim());

            if (string.IsNullOrEmpty(fileTitle)) throw new Exception("엑셀 파일을 선택해주세요.");

            // Make 함수에서 사용하는 데이터베이스에 연결한다.
            string connectionString = GetServerConnectionString_CODE();

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                using (OleDbTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        // DB 서버의 오늘 날짜를 yyyyMMdd 형식으로 가져온다.
                        string sysdt = MetroLib.Util.GetSysDate(conn, tran);
                        int seq = 1;

                        // 오늘 날짜의 최대 SEQ 다음 값을 구한다. 자료가 없으면 1이다.
                        string sql = "";
                        sql += Environment.NewLine + "SELECT ISNULL(MAX(CAST(SEQ AS INT)), 0) + 1 AS NEXT_SEQ";
                        sql += Environment.NewLine + "  FROM H01A";
                        sql += Environment.NewLine + " WHERE CREDT = ?";

                        List<object> para = new List<object>();
                        para.Add(sysdt);

                        MetroLib.SqlHelper.GetDataReader(sql, para, conn, tran, delegate(OleDbDataReader reader)
                        {
                            seq = Convert.ToInt32(reader["NEXT_SEQ"]);
                            return MetroLib.SqlHelper.BREAK;
                        });

                        // 오늘 생성되거나 변경된 자료를 배포 테이블에 복사한다.
                        string tableName = "TI09_DRG_BOSANG_" + sysdt + "_" + seq.ToString() + "_0";

                        sql = "";
                        sql += Environment.NewLine + "SELECT *";
                        sql += Environment.NewLine + "  INTO [" + tableName + "]";
                        sql += Environment.NewLine + "  FROM TI09_DRG_BOSANG";
                        sql += Environment.NewLine + " WHERE CREDT = ?";
                        sql += Environment.NewLine + "    OR CHGDT = ?";

                        para.Clear();
                        para.Add(sysdt);
                        para.Add(sysdt);

                        MetroLib.SqlHelper.ExecuteSql(sql, para, conn, tran);

                        // 배포 테이블에 대한 정보를 H01에 등록한다.
                        sql = "";
                        sql += Environment.NewLine + "INSERT INTO H01A";
                        sql += Environment.NewLine + "       (CREDT, SEQ, RMK, HOSGRD, TBLNM, COLKEY, COLLST, COLTYP)";
                        sql += Environment.NewLine + "VALUES (?, ?, ?, ?, ?, ?, ?, ?)";

                        // OleDb 파라미터는 SQL의 물음표 순서대로 추가한다.
                        para.Clear();
                        para.Add(sysdt);
                        para.Add(seq);
                        para.Add(fileTitle);
                        para.Add("0");
                        para.Add("TI09_DRG_BOSANG");
                        para.Add("PCODE,ADTDT,GUBUN");
                        para.Add("PCODE,ADTDT,GUBUN,BOSANG_RT,PCODENM,SPEC,BUNCD,MKCNM,METERIAL,MKCNMK,PTYPE,PDUT,GUB_GB,OPR_GB,NOTI_NO,M_CAT,M_CAT_NM,D_CAT,REMARK,ENDDT,CREDT,CHGDT");
                        para.Add("C;C;C;N;C;C;C;C;C;C;C;C;C;C;C;C;C;C;C;C;C;C");

                        MetroLib.SqlHelper.ExecuteSql(sql, para, conn, tran);

                        // 두 작업이 모두 성공한 경우에만 확정한다.
                        tran.Commit();
                    }
                    catch
                    {
                        // 오류가 발생하면 테이블 생성과 H01 등록을 함께 취소한다.
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
