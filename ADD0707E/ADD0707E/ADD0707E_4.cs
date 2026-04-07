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
    public partial class ADD0707E_4 : Form
    {
        public string in_code;
        public string in_name;

        private bool IsFirst;

        public ADD0707E_4()
        {
            InitializeComponent();
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text.ToString() != "") txtName.Text = "";
        }

        private void ADD0707E_4_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0707E_4_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            txtCode.Text = in_code;
            txtName.Text = in_name;

            btnQuery.PerformClick();
        }

        private string GetName(string code)
        {
            string ret = "";
            if (code == "") return ret;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT I09.PCODENM";
                sql += Environment.NewLine + "  FROM TI09 I09";
                sql += Environment.NewLine + " WHERE I09.PCODE='" + code + "'";
                sql += Environment.NewLine + "   AND I09.GUBUN='2'";
                sql += Environment.NewLine + "   AND I09.ADTDT=(SELECT MAX(X.ADTDT) FROM TI09 X WHERE X.PCODE=I09.PCODE AND X.GUBUN=I09.GUBUN)"; 

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    ret = reader["PCODENM"].ToString();

                    return MetroLib.StrHelper.BREAK;
                });
            }
            return ret;
        }

        private void txtCode_Leave(object sender, EventArgs e)
        {
            txtName.Text = GetName(txtCode.Text.ToString());
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtName.Text = GetName(txtCode.Text.ToString());
            }
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
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }

        }

        private void Query()
        {
            grdMainHx.DataSource = null;
            List<CDataHx> list = new List<CDataHx>();
            grdMainHx.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT DEMNO,BUYDT,BUYQTY,BUYAMT,BUSINESSCD,TRADENM";
                sql += Environment.NewLine + "  FROM TIE_H0602";
                sql += Environment.NewLine + " WHERE ITEMCD = '" + txtCode.Text.ToString() + "' ";
                sql += Environment.NewLine + " ORDER BY DEMNO DESC";


                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataHx data = new CDataHx();
                    data.Clear();

                    data.DEMNO = reader["DEMNO"].ToString();
                    data.BUYDT = reader["BUYDT"].ToString();
                    data.BUYQTY = MetroLib.StrHelper.ToLong(reader["BUYQTY"].ToString());
                    data.BUYAMT = MetroLib.StrHelper.ToLong(reader["BUYAMT"].ToString());
                    data.BUSINESSCD = reader["BUSINESSCD"].ToString();
                    data.TRADENM = reader["TRADENM"].ToString();

                    list.Add(data);

                    return true;
                });
            }
            RefreshGridMainHx();
        }

        private void RefreshGridMainHx()
        {
            if (grdMainHx.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdMainHx.BeginInvoke(new Action(() => grdMainHxView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdMainHxView.RefreshData();
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
    }
}
