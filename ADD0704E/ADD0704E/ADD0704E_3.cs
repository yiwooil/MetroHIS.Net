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

namespace ADD0704E
{
    public partial class ADD0704E_3 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // 이벤트를 만들자
        public event EventHandler ApplyButtonClick;
        public class MyEventArgs : EventArgs
        {
            public string BUYDT;
            public string ITEMCD;
            public long BUYQTY;
            public long BUYTOTAMT;
            public string BUSSCD;
            public string ERR_CD;
            public string ERR_MSG;
            public bool ADJUST_QTY;
        }

        private bool OnPgm = false;

        public ADD0704E_3()
        {
            InitializeComponent();

            OnPgm = true;
            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.Net").CreateSubKey("ADD0704E_3");
            chkHeader.Checked = reg.GetValue(chkHeader.Name, "False").ToString() == "True" ? true : false;
            chkAdjustQty.Checked = reg.GetValue(chkAdjustQty.Name, "False").ToString() == "True" ? true : false;
            txtColBuydt.Text = reg.GetValue(txtColBuydt.Name, "").ToString();
            txtColItemcd.Text = reg.GetValue(txtColItemcd.Name, "").ToString();
            txtColQty.Text = reg.GetValue(txtColQty.Name, "").ToString();
            txtColTotamt.Text = reg.GetValue(txtColTotamt.Name, "").ToString();
            txtColBusscd.Text = reg.GetValue(txtColBusscd.Name, "").ToString();
            OnPgm = false;
        }

        private void btnExcel_Click(object sender, EventArgs e)
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
                if (File.Exists(filename) == false) return;

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.ReadExcel(filename);
                this.HideColumn();
                this.CloseProgressForm("", "");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
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
                if (col_count > 26) col_count = 26; // A부터 Z까지만
                int buydt_col = -1;
                buydt_col = txtColBuydt.Text.ToString().Trim().CompareTo("A"); // A=0,B=1,C=1...이 된다.

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
                        if (col - 1 == buydt_col && xcel[row, col] is double)
                        {
                            // 구입일자가 일자형식으로 저장된 경우 이렇게 처래해야한다.
                            data.COL_VLAUE[col - 1] = DateTime.FromOADate((double)xcel[row, col]).ToString("yyyyMMdd");
                        }
                        else
                        {
                            data.COL_VLAUE[col - 1] = xcel[row, col];
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

        private bool IsDateString(string value)
        {
            if (value is string)
            {
                var formats = new[] { "yyyyMMdd", "yyyy-MM-dd","yyyy/MM/dd" };
                bool bRet = false;
                DateTime dtRtn;
                bRet = DateTime.TryParseExact((string)value, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtRtn);
                return bRet;
            }
            else
            {
                return false;
            }
        }

        private string DateToString(string value)
        {
            var formats = new[] { "yyyyMMdd", "yyyy-MM-dd","yyyy/MM/dd" };
            bool bRet = false;
            DateTime dtRtn;
            bRet = DateTime.TryParseExact((string)value, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtRtn);
            string strDate = dtRtn.ToString("yyyyMMdd");
            return strDate;
        }

        private void HideColumn()
        {
            string[] field_name = new string[] { "Z", "Y", "X", "W", "V", "U", "T", "S", "R", "Q", "P", "O", "N", "M", "L", "K", "J", "I", "H", "G", "F" };

            for (int col = 0; col < field_name.Length; col++)
            {
                for (int row = 0; row < grdExcelView.DataRowCount; row++)
                {
                    object obj = grdExcelView.GetRowCellValue(row, "COL_" + field_name[col]);
                    if (obj != null)
                    {
                        return;
                    }
                }
                grdExcelView.Columns["COL_" + field_name[col]].Visible = false;
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

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (txtColBuydt.Text.ToString().Trim() == "")
            {
                MessageBox.Show("구입일자 컬럼이 설정되지 않았습니다.");
                return;
            }
            if (txtColItemcd.Text.ToString().Trim() == "")
            {
                MessageBox.Show("코드 컬럼이 설정되지 않았습니다.");
                return;
            }
            if (txtColQty.Text.ToString().Trim() == "")
            {
                MessageBox.Show("수량 컬럼이 설정되지 않았습니다.");
                return;
            }
            if (txtColTotamt.Text.ToString().Trim() == "")
            {
                MessageBox.Show("금액 컬럼이 설정되지 않았습니다.");
                return;
            }
            if (txtColBusscd.Text.ToString().Trim() == "")
            {
                MessageBox.Show("구입기관 컬럼이 설정되지 않았습니다.");
                return;
            }

            try
            {
                string msg = (sender as Button).Text.ToString() + " 중입니다.";
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", msg);
                int err_cnt = Apply(msg);
                this.CloseProgressForm("", "");
                Cursor.Current = Cursors.Default;

                if (err_cnt > 0) MessageBox.Show("부정확한 자료는 제외되었으며, 빨간색으로 표시하였습니다.");
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private int Apply(string p_msg)
        {
            int err_cnt = 0;

            MyEventArgs arg = new MyEventArgs();

            int row_count = grdExcelView.DataRowCount;
            for (int row = 0; row < row_count; row++)
            {
                this.ShowProgressForm("", p_msg + " (" + (row + 1) + "/" + row_count + ")");

                if (chkHeader.Checked == true && row == 0) continue; // 제목줄 제외

                string fieldname = "";
                object fieldvalue = null;

                fieldname = "COL_" + txtColBuydt.Text.ToString();
                fieldvalue = grdExcelView.GetRowCellValue(row, fieldname);
                arg.BUYDT = fieldvalue == null ? "" : fieldvalue.ToString();

                fieldname = "COL_" + txtColItemcd.Text.ToString();
                fieldvalue = grdExcelView.GetRowCellValue(row, fieldname);
                arg.ITEMCD = fieldvalue == null ? "" : fieldvalue.ToString();

                fieldname = "COL_" + txtColQty.Text.ToString();
                fieldvalue = grdExcelView.GetRowCellValue(row, fieldname);
                arg.BUYQTY = fieldvalue == null ? 0 : MetroLib.StrHelper.ToLong(fieldvalue.ToString());

                fieldname = "COL_" + txtColTotamt.Text.ToString();
                fieldvalue = grdExcelView.GetRowCellValue(row, fieldname);
                arg.BUYTOTAMT = fieldvalue == null ? 0 : MetroLib.StrHelper.ToLong(fieldvalue.ToString());

                fieldname = "COL_" + txtColBusscd.Text.ToString();
                fieldvalue = grdExcelView.GetRowCellValue(row, fieldname);
                arg.BUSSCD = fieldvalue == null ? "" : fieldvalue.ToString();

                arg.ERR_CD = "";
                arg.ERR_MSG = "";

                arg.ADJUST_QTY = chkAdjustQty.Checked;

                if (IsDateString(arg.BUYDT) == true)
                {
                    arg.BUYDT = DateToString(arg.BUYDT);
                }

                this.ApplyButtonClick(this, arg);

                if (arg.ERR_CD != "") err_cnt++;

                grdExcelView.SetRowCellValue(row, "ERR_CD", arg.ERR_CD);
                grdExcelView.SetRowCellValue(row, "ERR_MSG", arg.ERR_MSG);

                RefreshGridExcel();
            }
            return err_cnt;
        }

        private void grdExcelView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;

            string err_cd = view.GetRowCellValue(e.RowHandle, "ERR_CD").ToString();

            if (e.Column.FieldName == "COL_" + txtColBuydt.Text.ToString()) // BUYDT
            {
                if (err_cd == "1" || err_cd == "5")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            if (e.Column.FieldName == "COL_" + txtColTotamt.Text.ToString() || e.Column.FieldName == "COL_" + txtColQty.Text.ToString()) // BUYTOTAMT, BUYQTY
            {
                if (err_cd == "2")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            if (e.Column.FieldName == "COL_" + txtColItemcd.Text.ToString()) // ITEMCD
            {
                if (err_cd == "3" || err_cd == "5")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            if (e.Column.FieldName == "COL_" + txtColBusscd.Text.ToString()) // BUSSCD
            {
                if (err_cd == "4")
                {
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                }
            }
        }

        private void chkHeader_CheckedChanged(object sender, EventArgs e)
        {
            if (OnPgm == true) return;
            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.Net").CreateSubKey("ADD0704E_3");
            reg.SetValue(chkHeader.Name, chkHeader.Checked == true ? "True" : "False");
        }

        private void txtColBuydt_TextChanged(object sender, EventArgs e)
        {
            if (OnPgm == true) return;
            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.Net").CreateSubKey("ADD0704E_3");
            reg.SetValue(txtColBuydt.Name, txtColBuydt.Text.ToString());
        }

        private void txtColItemcd_TextChanged(object sender, EventArgs e)
        {
            if (OnPgm == true) return;
            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.Net").CreateSubKey("ADD0704E_3");
            reg.SetValue(txtColItemcd.Name, txtColItemcd.Text.ToString());
        }

        private void txtColQty_TextChanged(object sender, EventArgs e)
        {
            if (OnPgm == true) return;
            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.Net").CreateSubKey("ADD0704E_3");
            reg.SetValue(txtColQty.Name, txtColQty.Text.ToString());
        }

        private void txtColTotamt_TextChanged(object sender, EventArgs e)
        {
            if (OnPgm == true) return;
            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.Net").CreateSubKey("ADD0704E_3");
            reg.SetValue(txtColTotamt.Name, txtColTotamt.Text.ToString());
        }

        private void txtColBusscd_TextChanged(object sender, EventArgs e)
        {
            if (OnPgm == true) return;
            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.Net").CreateSubKey("ADD0704E_3");
            reg.SetValue(txtColBusscd.Name, txtColBusscd.Text.ToString());
        }

        private void grdExcelView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //
        }

        private void grdExcelView_MouseDown(object sender, MouseEventArgs e)
        {
            // 그리드의 맨 왼쪽 상단을 클릭하여 모든 셀을 선택하는 기능
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = view.CalcHitInfo(e.Location);
            if (hitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.ColumnButton)
            {
                view.SelectAll();
            }
        }

        private void chkAdjustQty_CheckedChanged(object sender, EventArgs e)
        {
            if (OnPgm == true) return;
            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.Net").CreateSubKey("ADD0704E_3");
            reg.SetValue(chkAdjustQty.Name, chkAdjustQty.Checked == true ? "True" : "False");
        }
    }
}
