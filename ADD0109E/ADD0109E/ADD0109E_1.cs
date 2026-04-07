using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0109E
{
    public partial class ADD0109E_1 : Form
    {
        public string m_pricd;

        public ADD0109E_1()
        {
            InitializeComponent();
        }

        private void ADD0109E_1_Load(object sender, EventArgs e)
        {
            m_pricd = "";
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Query()
        {
            string pricd = txtPricd.Text.ToString().Trim();
            string prknm = txtPrknm.Text.ToString().Trim();

            if (pricd == "" && prknm == "") return;

            grdMain.DataSource=null;
            List<CPricd> list=new List<CPricd>();
            grdMain.DataSource=list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();


                string sql = "";
                sql="";
                sql += Environment.NewLine + "SELECT DISTINCT A02.PRICD,A02.PRKNM ";
                sql += Environment.NewLine + "  FROM TA02 A02 ";
                sql += Environment.NewLine + " WHERE 1=1 ";
                if (pricd != "")
                {
                    sql += Environment.NewLine + "   AND PRICD LIKE '" + pricd + "%'";
                }
                if (prknm != "")
                {
                    sql += Environment.NewLine + "   AND PRKNM LIKE '%" + prknm + "%'";
                }
                sql += Environment.NewLine + " ORDER BY A02.PRICD";

                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CPricd data = new CPricd();
                            data.Clear();
                            data.PRICD = reader["PRICD"].ToString();
                            data.PRKNM = reader["PRKNM"].ToString();
                            list.Add(data);
                        }
                        reader.Close();
                    }
                }

                conn.Close();
            }

            this.RefreshGridMain();
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

        private void RefreshGridMain()
        {
            if (grdMain.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdMain.BeginInvoke(new Action(() => grdMainView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdMainView.RefreshData();
                Application.DoEvents();
            }
        }

        private void grdMainView_DoubleClick(object sender, EventArgs e)
        {
            //if (grdMainView.FocusedRowHandle == null) return;
            if (grdMainView.FocusedRowHandle < 0) return;
            if (grdMainView.FocusedRowHandle >= grdMainView.RowCount) return;

            string pricd = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcPRICD).ToString();
            if (pricd != "")
            {
                m_pricd = pricd;
                this.Close();
            }
        }

    }
}
