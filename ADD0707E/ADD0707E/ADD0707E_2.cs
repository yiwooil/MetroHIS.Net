using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0707E
{
    public partial class ADD0707E_2 : Form
    {
        public string m_busscd;
        public bool m_out_sel;
        public string m_out_busscd;
        public string m_out_tradenm;

        private bool IsFirst;

        public ADD0707E_2()
        {
            InitializeComponent();

            m_out_sel = false;
            m_out_busscd = "";
            m_out_tradenm = "";
        }

        private void ADD0707E_2_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0707E_2_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            txtBusscd.Text = m_busscd;
            btnQuery.PerformClick();
            //if (m_busscd == "") this.QueryBuss(); // m_busscd에 "" 값이 넘어오면 TextChnged 이벤트가 발생하지 않아서.
            //txtBusscd.Select(txtBusscd.Text.ToString().Length, 0);
        }

        private void QueryBuss()
        {
            string busscd = txtBusscd.Text.ToString();

            grdBuss.DataSource = null;
            List<CDataBuss> list = new List<CDataBuss>();
            grdBuss.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT COUNT(*) CNT, CDNM, FLD1QTY ";
                sql += Environment.NewLine + "  FROM TI88 ";
                sql += Environment.NewLine + " WHERE MST1CD='A' ";
                sql += Environment.NewLine + "   AND MST2CD='BUSINESSCD' ";
                sql += Environment.NewLine + "   AND REPLACE(CDNM,'-','') LIKE '" + busscd.Replace("-", "").ToString() + "%'";
                sql += Environment.NewLine + " GROUP BY CDNM, FLD1QTY ";
                sql += Environment.NewLine + " ORDER BY CDNM, FLD1QTY ";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataBuss data = new CDataBuss();
                    data.Clear();
                    data.BUSINESSCD = reader["CDNM"].ToString();
                    data.TRADENM = reader["FLD1QTY"].ToString();

                    list.Add(data);

                    return true;

                });
            }

            RefreshGridBuss();
        }

        private void RefreshGridBuss()
        {
            if (grdBuss.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdBuss.BeginInvoke(new Action(() => grdBussView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdBussView.RefreshData();
                Application.DoEvents();
            }
        }

        private void grdBussView_DoubleClick(object sender, EventArgs e)
        {
            if (grdBussView.FocusedRowHandle < 0) return;
            SelectedRow(grdBussView.FocusedRowHandle);
        }

        private void SelectedRow(int rowHandle)
        {
            m_out_busscd = grdBussView.GetRowCellValue(rowHandle, "BUSINESSCD").ToString();
            m_out_tradenm = grdBussView.GetRowCellValue(rowHandle, "TRADENM").ToString();
            m_out_sel = true;
            this.Close();
        }

        private void txtBusscd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnQuery.PerformClick();
            }
        }

        private void grdBussView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (grdBussView.FocusedRowHandle < 0) return;
                SelectedRow(grdBussView.FocusedRowHandle);
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "조회 중입니다.");
                this.QueryBuss();
                this.CloseProgressForm("", "조회 중입니다.");
                Cursor.Current = Cursors.Default;

                grdBuss.Focus();
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "조회 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
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
    }
}
