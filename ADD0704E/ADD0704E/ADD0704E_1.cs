using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0704E
{
    public partial class ADD0704E_1 : Form
    {
        private bool IsFirst = true;

        public string in_busscd = "";

        public string out_yn = "";
        public string out_busscd = "";
        public string out_bussnm = "";

        public ADD0704E_1()
        {
            InitializeComponent();
        }

        private void ADD0704E_1_Load(object sender, EventArgs e)
        {
            IsFirst = true;

            out_yn = "";
            out_busscd = "";
            out_bussnm = "";
        }

        private void ADD0704E_1_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;
            try
            {
                Query();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Query()
        {
            grdBuss.DataSource = null;
            List<CDataBuss> list = new List<CDataBuss>();
            grdBuss.DataSource = list;


            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT CDNM, FLD1QTY ";
                sql += System.Environment.NewLine + "  FROM TI88 ";
                sql += System.Environment.NewLine + " WHERE MST1CD='A' ";
                sql += System.Environment.NewLine + "   AND MST2CD='BUSINESSCD_PHA' ";
                sql += System.Environment.NewLine + "   AND CDNM LIKE '" + in_busscd + "%' ";
                sql += System.Environment.NewLine + " ORDER BY FLD1QTY, CDNM ";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataBuss data = new CDataBuss();
                    data.Clear();
                    data.BUSSCD = reader["CDNM"].ToString();
                    data.BUSSNM = reader["FLD1QTY"].ToString();
                    list.Add(data);

                    return true;
                });
            }
            this.RefreshGridBuss();
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

        private void grdBussView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0) return;
            txtBusscd.Text = grdBussView.GetRowCellValue(e.RowHandle, "BUSSCD").ToString();
            txtBussnm.Text = grdBussView.GetRowCellValue(e.RowHandle, "BUSSNM").ToString();
        }

        private void grdBussView_DoubleClick(object sender, EventArgs e)
        {
            if (grdBussView.FocusedRowHandle < 0) return;
            List<CDataBuss> list = (List<CDataBuss>)grdBuss.DataSource;
            if (list == null) return;

            out_yn = "Y";
            out_busscd = grdBussView.GetRowCellValue(grdBussView.FocusedRowHandle, "BUSSCD").ToString();
            out_bussnm = grdBussView.GetRowCellValue(grdBussView.FocusedRowHandle, "BUSSNM").ToString();

            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBusscd.Text.ToString().Trim() == "" || txtBussnm.Text.ToString().Trim() == "") return;

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Save();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

                this.Query();
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Save()
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string next_mst3cd = "";

                string sql = "";
                sql = "SELECT ISNULL(MAX(CONVERT(NUMERIC,MST3CD)),0) + 1 AS MST3CD FROM TI88 WHERE MST1CD='A' AND MST2CD='BUSINESSCD_PHA'";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    next_mst3cd = reader["MST3CD"].ToString();

                    return MetroLib.SqlHelper.CONTINUE;
                });

                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI88(MST1CD,MST2CD,MST3CD,CDNM,FLD1QTY)";
                sql += Environment.NewLine + "VALUES('A','BUSINESSCD_PHA','" + next_mst3cd + "','" + txtBusscd.Text.ToString().Trim() + "','" + txtBussnm.Text.ToString().Trim() + "')";

                MetroLib.SqlHelper.ExecuteSql(sql, conn);
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

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBusscd.Text.ToString().Trim() == "") return;

                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Del();
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;

                this.Query();
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Del()
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += Environment.NewLine + "DELETE TI88";
                sql += Environment.NewLine + " WHERE MST1CD='A'";
                sql += Environment.NewLine + "   AND MST2CD='BUSINESSCD_PHA'";
                sql += Environment.NewLine + "   AND CDNM='" + txtBusscd.Text.ToString().Trim() + "'";

                MetroLib.SqlHelper.ExecuteSql(sql, conn);
            }
        }
    }
}
