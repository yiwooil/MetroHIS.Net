using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;   // .xls
using NPOI.XSSF.UserModel;   // .xlsx

namespace ADD7008E
{
    public partial class ADD7008E_EXCEL : Form
    {
        public event EventHandler<PtntChangedEventArgs> PtntChanged;

        public ADD7008E_EXCEL()
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = txtFilename.Text.ToString().Trim();
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
                    }
                }

                data.ERR_CD = "";
                data.ERR_MSG = "";

                list.Add(data);
            }

            RefreshGridExcel();
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
            ShowProgressForm("", "처리 준비 중입니다.");
            bool isOk = true;
            try
            {

                List<CDataExcel> list = grdExcel.DataSource as List<CDataExcel>;

                // row=0,1은 컬럼 제목임
                int no = 0;
                for (int row = 2; row < grdExcelView.DataRowCount; row++)
                {
                    CDataExcel data = grdExcelView.GetRow(row) as CDataExcel;

                    ShowProgressForm("", row + "/" + grdExcelView.DataRowCount);

                    if (PtntChanged != null)
                    {
                        PtntChangedEventArgs args = new PtntChangedEventArgs(++no, 
                                                                             data.COL_A.ToString(), 
                                                                             data.COL_B.ToString(),
                                                                             data.COL_C.ToString(),
                                                                             data.COL_D.ToString(),
                                                                             data.COL_E.ToString(),
                                                                             data.COL_F.ToString(),
                                                                             data.COL_G.ToString(),
                                                                             data.COL_H.ToString(),
                                                                             data.COL_I.ToString(),
                                                                             data.COL_J.ToString(),
                                                                             data.COL_K.ToString(),
                                                                             data.COL_L.ToString()
                                                                            );
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
                    if (MessageBox.Show("작업을 완료했습니다. 이 화면을 종료하시겠습니까?", "", MessageBoxButtons.YesNo) == DialogResult.Yes) this.Close();
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
