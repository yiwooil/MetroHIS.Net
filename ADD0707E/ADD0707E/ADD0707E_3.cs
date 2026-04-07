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
    public partial class ADD0707E_3 : Form
    {
        private bool IsFirst;
        public ADD0707E_3()
        {
            InitializeComponent();
        }

        private void ADD0707E_3_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0707E_3_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            this.Query();
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
                sql += Environment.NewLine + "SELECT COUNT(*) CNT, CDNM, FLD1QTY ";
                sql += Environment.NewLine + "  FROM TI88 ";
                sql += Environment.NewLine + " WHERE MST1CD='A' ";
                sql += Environment.NewLine + "   AND MST2CD='BUSINESSCD' ";
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

        private void grdBussView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0) return;
            txtBusscd.Text = grdBussView.GetRowCellValue(e.RowHandle, "BUSINESSCD").ToString();
            txtBussnm.Text = grdBussView.GetRowCellValue(e.RowHandle, "TRADENM").ToString();
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
                sql = "SELECT ISNULL(MAX(CONVERT(NUMERIC,MST3CD)),0) + 1 AS MST3CD FROM TI88 WHERE MST1CD='A' AND MST2CD='BUSINESSCD'";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    next_mst3cd = reader["MST3CD"].ToString();

                    return MetroLib.SqlHelper.CONTINUE;
                });

                sql = "";
                sql += Environment.NewLine + "INSERT INTO TI88(MST1CD,MST2CD,MST3CD,CDNM,FLD1QTY)";
                sql += Environment.NewLine + "VALUES('A','BUSINESSCD','" + next_mst3cd + "','" + txtBusscd.Text.ToString().Trim() + "','" + txtBussnm.Text.ToString().Trim() + "')";

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
                sql += Environment.NewLine + "   AND MST2CD='BUSINESSCD'";
                sql += Environment.NewLine + "   AND CDNM='" + txtBusscd.Text.ToString().Trim() + "'";

                MetroLib.SqlHelper.ExecuteSql(sql, conn);
            }
        }
    }
}
