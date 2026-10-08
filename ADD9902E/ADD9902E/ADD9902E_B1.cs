using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD9902E
{
    public partial class ADD9902E_B1 : Form
    {
        public ADD9902E_B1()
        {
            InitializeComponent();
        }

        public ADD9902E_B1(string table, string mst1cd, string mst2cd, string mst3cd)
            : this()
        {
            label1.Text = table;
            txtMST1CD.Text = mst1cd;
            txtMST2CD.Text = mst2cd;
            txtMST3CD.Text = mst3cd;
        }

        private void ADD9902E_B1_Shown(object sender, EventArgs e)
        {
            btnQuery.PerformClick();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                if (Save() == true)
                {
                    this.Query();
                }
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
            bool isTI88 = label1.Text == "TI88";

            string sql = "";

            if (isTI88 == true)
            {
                sql += Environment.NewLine + "SELECT CDNM,FLD1QTY,FLD2QTY,FLD3QTY,FLD4QTY,FLD1CD,FLD2CD,FLD3CD,FLD4CD,FLD5CD";
                sql += Environment.NewLine + "      ,'' AS FLD6CD,'' AS FLD7CD,'' AS FLD8CD,'' AS EXPDT";
                sql += Environment.NewLine + "  FROM TI88";
            }
            else
            {
                sql += Environment.NewLine + "SELECT CDNM,FLD1QTY,FLD2QTY,FLD3QTY,FLD4QTY,FLD1CD,FLD2CD,FLD3CD,FLD4CD,FLD5CD";
                sql += Environment.NewLine + "      ,FLD6CD,FLD7CD,FLD8CD,EXPDT";
                sql += Environment.NewLine + "  FROM TA88";
            }

            sql += Environment.NewLine + " WHERE MST1CD=?";
            sql += Environment.NewLine + "   AND MST2CD=?";
            sql += Environment.NewLine + "   AND MST3CD=?";

            List<Object> para = new List<Object>();
            para.Add(txtMST1CD.Text);
            para.Add(txtMST2CD.Text);
            para.Add(txtMST3CD.Text);

            string strConn = MetroLib.DBHelper.GetConnectionString();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                MetroLib.SqlHelper.GetDataReader(sql, para, conn, delegate(OleDbDataReader reader)
                {
                    txtCDNM.Text = reader["CDNM"].ToString();
                    txtFLD1QTY.Text = reader["FLD1QTY"].ToString();
                    txtFLD2QTY.Text = reader["FLD2QTY"].ToString();
                    txtFLD3QTY.Text = reader["FLD3QTY"].ToString();
                    txtFLD4QTY.Text = reader["FLD4QTY"].ToString();
                    txtFLD1CD.Text = reader["FLD1CD"].ToString();
                    txtFLD2CD.Text = reader["FLD2CD"].ToString();
                    txtFLD3CD.Text = reader["FLD3CD"].ToString();
                    txtFLD4CD.Text = reader["FLD4CD"].ToString();
                    txtFLD5CD.Text = reader["FLD5CD"].ToString();
                    txtFLD6CD.Text = reader["FLD6CD"].ToString();
                    txtFLD7CD.Text = reader["FLD7CD"].ToString();
                    txtFLD8CD.Text = reader["FLD8CD"].ToString();
                    txtEXPDT.Text = reader["EXPDT"].ToString();

                    return MetroLib.SqlHelper.BREAK;
                });
            }

            txtFLD6CD.Enabled = (isTI88 == false);
            txtFLD7CD.Enabled = (isTI88 == false);
            txtFLD8CD.Enabled = (isTI88 == false);
            txtEXPDT.Enabled = (isTI88 == false);
        }

        private bool Save()
        {
            bool isTI88 = label1.Text == "TI88";

            string tTI88 = "TI88";
            if (isTI88 == false)
            {
                tTI88 = "TA88";
            }

            string sql = "";

            sql += Environment.NewLine + "UPDATE " + tTI88 + "";
            sql += Environment.NewLine + "   SET FLD1QTY=?";
            sql += Environment.NewLine + "     , FLD2QTY=?";
            sql += Environment.NewLine + "     , FLD3QTY=?";
            sql += Environment.NewLine + "     , FLD4QTY=?";
            sql += Environment.NewLine + "     , FLD1CD=?";
            sql += Environment.NewLine + "     , FLD2CD=?";
            sql += Environment.NewLine + "     , FLD3CD=?";
            sql += Environment.NewLine + "     , FLD4CD=?";
            sql += Environment.NewLine + "     , FLD5CD=?";

            if (isTI88 == false)
            {
                sql += Environment.NewLine + "     , FLD6CD=?";
                sql += Environment.NewLine + "     , FLD7CD=?";
                sql += Environment.NewLine + "     , FLD8CD=?";
                sql += Environment.NewLine + "     , EXPDT=?";
            }

            sql += Environment.NewLine + " WHERE MST1CD=?";
            sql += Environment.NewLine + "   AND MST2CD=?";
            sql += Environment.NewLine + "   AND MST3CD=?";

            List<Object> para = new List<Object>();
            para.Add(txtFLD1QTY.Text);
            para.Add(txtFLD2QTY.Text);
            para.Add(txtFLD3QTY.Text);
            para.Add(txtFLD4QTY.Text);
            para.Add(txtFLD1CD.Text);
            para.Add(txtFLD2CD.Text);
            para.Add(txtFLD3CD.Text);
            para.Add(txtFLD4CD.Text);
            para.Add(txtFLD5CD.Text);

            if (isTI88 == false)
            {
                para.Add(txtFLD6CD.Text);
                para.Add(txtFLD7CD.Text);
                para.Add(txtFLD8CD.Text);
                para.Add(txtEXPDT.Text);
            }

            para.Add(txtMST1CD.Text);
            para.Add(txtMST2CD.Text);
            para.Add(txtMST3CD.Text);

            string strConn = MetroLib.DBHelper.GetConnectionString();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                MetroLib.SqlHelper.ExecuteSql(sql, para, conn, null);
            }

            MessageBox.Show("저장이 완료되었습니다.");

            return true;
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
