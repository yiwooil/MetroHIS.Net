using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;   // .xls
using NPOI.XSSF.UserModel;   // .xlsx

namespace ADD7007E
{
    public partial class ADD7007E_EXCEL : Form
    {
        public event EventHandler<PtntChangedEventArgs> PtntChanged;

        private int COL_CNECNO = -1; // 접수번호 컬럼
        private int COL_CNECDD = -1; // 접수일자 컬럼
        private int COL_EPRTNO = -1; // 명일련번호 컬럼
        private int COL_PNM = -1; // 환자명 컬럼
        private int COL_DACD1 = -1; // 진단명 컬럼
        private int COL_OPCODE1 = -1; // 수술코드 컬럼

        private ADD7007E_EXCEL()
        {
            InitializeComponent();
        }

        public ADD7007E_EXCEL(string typeName, string frdt, string todt): this()
        {
            txtTypeName.Text = typeName;
            txtFrdt.Text = frdt;
            txtTodt.Text = todt;
        }

        private void ADD7007E_EXCEL_Load(object sender, EventArgs e)
        {

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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadExcel_Click(object sender, EventArgs e)
        {

            txtInfo.Text = "";

            try
            {
                COL_CNECNO = -1; // 접수번호 컬럼
                COL_CNECDD = -1; // 접수일자 컬럼
                COL_EPRTNO = -1; // 명일련번호 컬럼
                COL_PNM = -1; // 환자명 컬럼
                COL_DACD1 = -1; // 진단명 컬럼
                COL_OPCODE1 = -1; // 수술코드 컬럼

                string filename = txtFilename.Text.ToString().Trim();
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "엑셀 파일을 읽는 중입니다.");
                if (chkNPOI.Checked == true)
                {
                    this.ReadExcel_NPOI(filename);
                }
                else
                {
                    this.ReadExcel(filename);
                }
                
                this.CloseProgressForm("", "");
                Cursor.Current = Cursors.Default;

                txtInfo.Text = (COL_CNECNO >= 0 ? "접수번호=" + ToExcelColumnName(COL_CNECNO) + ", " : "")
                             + (COL_CNECDD >= 0 ? "접수일=" + ToExcelColumnName(COL_CNECDD) + ", " : "")
                             + (COL_EPRTNO >= 0 ? "명일련번호=" + ToExcelColumnName(COL_EPRTNO) + ", " : "")
                             + (COL_PNM >= 0 ? "환자명=" + ToExcelColumnName(COL_PNM) + ", " : "")
                             + (COL_DACD1 >= 0 ? "진단코드=" + ToExcelColumnName(COL_DACD1) + ", " : "")
                             + (COL_OPCODE1 >= 0 ? "수술코드1=" + ToExcelColumnName(COL_OPCODE1) : "");
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private string ToExcelColumnName(int idx)
        {
            if (idx < 0) return "";

            StringBuilder sb = new StringBuilder();

            // 0-based idx -> Excel column name
            int n = idx;
            while (n >= 0)
            {
                int rem = n % 26;
                sb.Insert(0, (char)('A' + rem));
                n = (n / 26) - 1;
            }

            return sb.ToString();
        }

        private void ReadExcel(string filename)
        {
            grdExcel.DataSource = null;
            List<CDataExcel> list = new List<CDataExcel>();
            grdExcel.DataSource = list;

            uint excepProcessId = 0;
            Microsoft.Office.Interop.Excel.Application app = null;
            Microsoft.Office.Interop.Excel.Workbook wb = null;
            Microsoft.Office.Interop.Excel.Worksheet ws = null;

            try
            {
                app = new Microsoft.Office.Interop.Excel.Application();
                wb = app.Workbooks.Open(filename, 0, true, 5, "", ":", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.get_Item(1); // 첫번째 sheet 사용

                // 자료가 있는 column 개수와 row 개수를 구한다.
                int col_count = ws.UsedRange.Columns.Count;
                int row_count = ws.UsedRange.Rows.Count;
                if (col_count > CDataExcel.MAX_COUNT) col_count = CDataExcel.MAX_COUNT; // A부터 Z까지만

                object[,] xcel = (object[,])ws.UsedRange.Value2;
                for (int row = 1; row <= row_count; row++)
                {
                    bool IsEmptyRow = true;
                    for (int col = 1; col <= col_count; col++)
                    {
                        if (xcel[row, col] != null)
                        {
                            IsEmptyRow = false;
                            break;
                        }
                    }
                    if (IsEmptyRow == true) continue;

                    CDataExcel data = new CDataExcel();
                    data.Clear();
                    for (int col = 1; col <= col_count; col++)
                    {
                        data.COL_VLAUE[col - 1] = xcel[row, col];

                        // 제목줄에서 필요한 컬럼명을 구한다.
                        if (row == 1)
                        {
                            string header = data.COL_VLAUE[col - 1].ToString();
                            SetColIndex(header, col - 1);
                        }
                    }
                    data.ERR_CD = "";
                    data.ERR_MSG = "";

                    list.Add(data);

                    RefreshGridExcel();
                }

                wb.Close(false, Type.Missing, Type.Missing);
                app.Quit();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                ReleaseExcelObject(ws);
                ReleaseExcelObject(wb);
                ReleaseExcelObject(app);

                if (app != null && excepProcessId > 0)
                {
                    System.Diagnostics.Process.GetProcessById((int)excepProcessId).Kill();
                }
            }

            RefreshGridExcel();
        }

        private void ReadExcel_NPOI(string filePath)
        {
            grdExcel.DataSource = null;
            List<CDataExcel> list = new List<CDataExcel>();
            grdExcel.DataSource = list;

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

            ISheet sheet = wb.GetSheetAt(0);
            if (sheet == null)
                return;

            // Used range 유사 처리: 첫 행 기준으로 컬럼 개수 산정
            int firstRow = sheet.FirstRowNum;
            int lastRow = sheet.LastRowNum;

            // 첫 행(제목행)
            IRow headerRow = sheet.GetRow(firstRow);
            if (headerRow == null)
                return;

            int firstCol = headerRow.FirstCellNum;
            int lastCol = headerRow.LastCellNum;   // 마지막+1

            // A~Z까지만 (CDataExcel.MAX_COUNT 준수)
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
                        SetColIndex(header, c);
                    }
                }

                data.ERR_CD = "";
                data.ERR_MSG = "";

                list.Add(data);

                // 기존 ReadExcel()은 매 row마다 RefreshGridExcel() 호출
                // 동일 동작을 원하면 아래 유지 (성능상 부담이면 주석 처리 권장)
                //RefreshGridExcel();
            }

            RefreshGridExcel();
        }

        private void SetColIndex(string header, int c)
        {
            if (header == "접수번호")
            {
                COL_CNECNO = c;
            }
            else if (header == "접수일")
            {
                COL_CNECDD = c;
            }
            else if (header == "명일련")
            {
                COL_EPRTNO = c;
            }
            else if (header == "환자성명")
            {
                COL_PNM = c;
            }
            else if (header == "상병코드1")
            {
                COL_DACD1 = c;
            }
            else if (header == "수술코드1")
            {
                COL_OPCODE1 = c;
            }
        }

        // 보관...
        private void ReadExcelForDataTable_NPOI(string filePath)
        {
            DataTable dt = ReadExcelToDataTable_NPOI(filePath);
            grdExcel.DataSource = dt;          // DevExpress GridControl
            RefreshGridExcel();
        }

        // 보관...
        private DataTable ReadExcelToDataTable_NPOI(string filePath)
        {
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

            ISheet sheet = wb.GetSheetAt(0); // 첫 시트
            DataTable dt = new DataTable();

            if (sheet == null) return dt;

            // 첫 행(헤더)
            IRow headerRow = sheet.GetRow(sheet.FirstRowNum);
            if (headerRow == null) return dt;

            int firstCol = headerRow.FirstCellNum;
            int lastCol = headerRow.LastCellNum; // 마지막+1

            // 컬럼 생성 (헤더가 비면 COL_1, COL_2 ... 부여)
            for (int c = firstCol; c < lastCol; c++)
            {
                string colName = GetCellText(headerRow.GetCell(c));
                if (string.IsNullOrEmpty(colName))
                    colName = "COL_" + (c - firstCol + 1);

                // 동일 컬럼명 중복 방지
                if (dt.Columns.Contains(colName))
                    colName = colName + "_" + (c - firstCol + 1);

                dt.Columns.Add(colName);
            }

            // 데이터 행(헤더 다음 행부터)
            for (int r = sheet.FirstRowNum + 1; r <= sheet.LastRowNum; r++)
            {
                IRow row = sheet.GetRow(r);
                if (row == null) continue;

                DataRow dr = dt.NewRow();
                for (int c = firstCol; c < lastCol; c++)
                {
                    dr[c - firstCol] = GetCellText(row.GetCell(c));
                }
                dt.Rows.Add(dr);
            }

            return dt;
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

        private void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception)
            {
                obj = null;
                throw;
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btnMake_Click(object sender, EventArgs e)
        {
            string typeName = txtTypeName.Text.ToString().Trim();
            if (typeName != "수술의예방적항생제사용" && typeName != "마취")
            {
                MessageBox.Show("준비중입니다.");
                return;
            }

            ShowProgressForm("", "처리 준비 중입니다.");
            bool isOk = true;
            try
            {
                string frdt = txtFrdt.Text.ToString().Trim();
                string todt = txtTodt.Text.ToString().Trim();

                List<CDataExcel> list = grdExcel.DataSource as List<CDataExcel>;

                // row=0은 컬럼 제목임
                for (int row = 1; row < grdExcelView.DataRowCount; row++)
                {
                    CDataExcel data = list[row];
                    string cnecno = data.COL_VLAUE[COL_CNECNO].ToString();
                    string cnecdd = data.COL_VLAUE[COL_CNECDD].ToString();
                    string eprtno = data.COL_VLAUE[COL_EPRTNO].ToString();
                    string pnm = data.COL_VLAUE[COL_PNM].ToString();
                    string dacd1 = data.COL_VLAUE[COL_DACD1].ToString();
                    string opcode1 = data.COL_VLAUE[COL_OPCODE1].ToString();

                    ShowProgressForm("", pnm + " 환자를 처리하는 중입니다.");

                    if (PtntChanged != null)
                    {
                        PtntChangedEventArgs args = new PtntChangedEventArgs(frdt, todt, row, cnecno, cnecdd, eprtno, pnm, dacd1, opcode1);
                        PtntChanged(this, args);
                        if (args.Success == false)
                        {
                            isOk = false;
                            MessageBox.Show(args.FailureMessage);
                            break;
                        }
                    }
                }
                CloseProgressForm("", "");

                if (isOk)
                {
                    if (MessageBox.Show("대상자 생성을 완료했습니다. 이 화면을 종료하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes) this.Close();
                }

            }
            catch (Exception ex)
            {
                CloseProgressForm("", "");
                MessageBox.Show(ex.Message);
            }
        }
    }
}
