using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0731E
{
    public partial class ADD0731E_02 : Form
    {
        public bool m_sel;

        public string m_in_pcode;
        public string m_in_pcodenm;

        public string m_out_pcode;
        public string m_out_pcodenm;

        private bool IsFirst;

        public ADD0731E_02()
        {
            InitializeComponent();
        }

        private void ADD0731E_02_Load(object sender, EventArgs e)
        {
            IsFirst = true;
            m_sel = false;
        }

        private void ADD0731E_02_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            txtCode.Text = m_in_pcode;
            txtName.Text = m_in_pcodenm;
            Application.DoEvents();
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

        private void Query()
        {
            List<CDataCode> list = new List<CDataCode>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql += Environment.NewLine + "SELECT TOP 100 A.PCODE,A.PCODENM";
                sql += Environment.NewLine + "  FROM TI09_JABO A";
                sql += Environment.NewLine + " WHERE A.ADTDT=(SELECT MAX(X.ADTDT) FROM TI09_JABO X WHERE X.PCODE=A.PCODE AND X.GUBUN=A.GUBUN)";
                if (txtCode.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND A.PCODE LIKE '" + txtCode.Text.ToString() + "%'";
                }
                if (txtName.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND A.PCODENM LIKE '%" + txtName.Text.ToString() + "%'";
                }
                if (rbHan.Checked == true)
                {
                    // 한방
                    sql += Environment.NewLine + "   AND A.GUBUN IN ('8','9')";
                }
                else
                {
                    // 양방
                    sql += Environment.NewLine + "   AND A.GUBUN NOT IN ('8','9')";
                }
                sql += Environment.NewLine + "   AND A.JABOFG NOT IN ('1','2')";
                sql += Environment.NewLine + " UNION ";
                sql += Environment.NewLine + "SELECT TOP 100 A.PCODE,A.PCODENM";
                sql += Environment.NewLine + "  FROM TI09 A";
                sql += Environment.NewLine + " WHERE A.ADTDT=(SELECT MAX(X.ADTDT) FROM TI09 X WHERE X.PCODE=A.PCODE AND X.GUBUN=A.GUBUN)";
                sql += Environment.NewLine + "   AND (A.KUMAK1 = 0 AND A.KUMAK2 = 0 AND A.KUMAK3 = 0 AND A.KUMAK4 = 0 AND A.KUMAK5 = 0 AND A.KUMAK6 = 0)";
                if (txtCode.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND A.PCODE LIKE '" + txtCode.Text.ToString() + "%'";
                }
                if (txtName.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND A.PCODENM LIKE '%" + txtName.Text.ToString() + "%'";
                }
                if (rbHan.Checked == true)
                {
                    // 한방
                    sql += Environment.NewLine + "   AND A.GUBUN = '9'";
                }
                else
                {
                    // 양방
                    sql += Environment.NewLine + "   AND A.GUBUN = '1'";
                }
                sql += Environment.NewLine + " ORDER BY A.PCODE";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataCode data = new CDataCode();
                    data.CODE = reader["PCODE"].ToString();
                    data.CODENAME = reader["PCODENM"].ToString();

                    list.Add(data);

                    return true;
                });
            }

            RefreshGridMain();
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
            m_out_pcode = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcCODE).ToString();
            m_out_pcodenm = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcCODENAME).ToString();
            m_sel = true;
            this.Close();
        }
    }
}
