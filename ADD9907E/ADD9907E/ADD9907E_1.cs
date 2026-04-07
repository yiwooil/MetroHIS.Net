using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD9907E
{
    public partial class ADD9907E_1 : Form
    {
        public string m_InDacd;
        public string m_OutDacd;

        private bool IsFirst;

        public ADD9907E_1()
        {
            InitializeComponent();
        }

        private void ADD9907E_1_Load(object sender, EventArgs e)
        {
            IsFirst = true;
            m_OutDacd = "";
        }

        private void ADD9907E_1_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            txtDacd.Text = m_InDacd;
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
            Dictionary<string, string> dicDup = new Dictionary<string, string>();

            List<CDacdData> list = new List<CDacdData>();

            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string dacd = txtDacd.Text.ToString();
            if (dacd == "") return;

            string disediv = "2";
            string exdt = "20121001";
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string z16fg = "";

                string sql = "";
                sql = "SELECT FLD2QTY FROM TI88 WHERE MST1CD='A' AND MST2CD='HOSPITAL' AND MST3CD='24'";

                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        z16fg = reader["FLD2QTY"].ToString();
                    }
                    reader.Close();
                }

                if (z16fg == "1")
                {
                    // TZ16 사용
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT DACD, KORNM ";
                    sql += System.Environment.NewLine + "  FROM TZ16A Z16A (NOLOCK) ";
                    sql += System.Environment.NewLine + " WHERE Z16A.DACD LIKE ? ";
                    sql += System.Environment.NewLine + "   AND ISNULL(Z16A.USEFG,'') <> 'N'";  // 불완전코드제외
                    sql += System.Environment.NewLine + "   AND CASE WHEN ISNULL(Z16A.CREDT,'')='' THEN '19990101' ELSE Z16A.CREDT END <= ?";
                    sql += System.Environment.NewLine + "   AND CASE WHEN ISNULL(Z16A.EXPDT,'')='' THEN '99991231' ELSE Z16A.EXPDT END >= ?";
                    sql += System.Environment.NewLine + " ORDER BY DACD, SEQNO";

                    // TSQL문장과 Connection 객체를 지정   
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@1", dacd + "%"));
                        cmd.Parameters.Add(new OleDbParameter("@2", exdt));
                        cmd.Parameters.Add(new OleDbParameter("@3", exdt));

                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            if (dicDup.ContainsKey(reader["DACD"].ToString()) == false)
                            {
                                CDacdData data = new CDacdData();
                                data.DACD = reader["DACD"].ToString();
                                data.DANM = reader["KORNM"].ToString();
                                list.Add(data);
                                dicDup.Add(data.DACD,"");
                            }
                        }
                        reader.Close();
                    }
                }
                else
                {
                    // TA16 사용
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT DISECD AS DACD, DISENEWKOR AS KORNM";
                    sql += System.Environment.NewLine + "  FROM TA16 (NOLOCK) ";
                    sql += System.Environment.NewLine + " WHERE DISECD  LIKE ? ";
                    sql += System.Environment.NewLine + "   AND DISEDIV = ? ";
                    sql += System.Environment.NewLine + "   AND CASE WHEN ISNULL(EXPDT,'')='' THEN '99991231' ELSE EXPDT END >= ?";
                    sql += System.Environment.NewLine + " ORDER BY DISECD";

                    // TSQL문장과 Connection 객체를 지정   
                    using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("@1", dacd + "%"));
                        cmd.Parameters.Add(new OleDbParameter("@2", disediv));
                        cmd.Parameters.Add(new OleDbParameter("@3", exdt));

                        // 데이타는 서버에서 가져오도록 실행
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            while (reader.Read())
                            {
                                CDacdData data = new CDacdData();
                                data.DACD = reader["DACD"].ToString();
                                data.DANM = reader["KORNM"].ToString();
                                list.Add(data);
                            }
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
            m_OutDacd = grdMainView.GetRowCellValue(grdMainView.GetSelectedRows()[0], gcDACD).ToString();
            this.Close();
        }


    }
}
