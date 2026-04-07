using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD9910E
{
    public partial class ADD9910E : Form
    {
        private bool IsFirst;

        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        public ADD9910E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
        }

        public ADD9910E(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }

        private void ADD9910E_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD9910E_Activated(object sender, EventArgs e)
        {
            if(IsFirst == false) return;
            IsFirst = false;

            Init();
        }

        private void Init()
        {
            try
            {
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    txtExdt.Text = MetroLib.Util.GetSysDate(conn);

                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                txtExdt.Text = DateTime.Now.ToString("yyyyMMdd");
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
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Query()
        {
            Dictionary<string, string> dicDup = new Dictionary<string, string>();

            List<CData> list = new List<CData>();

            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string code = txtCode.Text.ToString();
            if (code == "") return;

            string exdt = txtExdt.Text.ToString();
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql="";
                sql = "";
                sql += System.Environment.NewLine + "SELECT SEQNO,CODE,KORNM,ENGNM,CREDT,EXPDT ";
                sql += System.Environment.NewLine + "  FROM TZ16A ";
                sql += System.Environment.NewLine + " WHERE CODE = ?";
                sql += System.Environment.NewLine + " ORDER BY SEQNO";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Add(new OleDbParameter("@1", code));

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string credt = reader["CREDT"].ToString();
                        string expdt = reader["EXPDT"].ToString();

                        if (credt == "") credt = "19990101";
                        if (expdt == "") expdt = "99991231";

                        if (String.Compare(exdt,credt)>=0 && String.Compare(exdt, expdt)<=0){
                            CData data = new CData();
                            data.Clear();
                            data.SEQNO=reader["SEQNO"].ToString();
                            data.CODE=reader["CODE"].ToString();
                            data.KORNM=reader["KORNM"].ToString();
                            data.ENGNM=reader["ENGNM"].ToString();

                            list.Add(data);
                        }
                    }
                    reader.Close();
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

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (grdMainView.RowCount <= 1) return; // 1건이거나 없는 경우
            if (grdMainView.FocusedRowHandle == 0) return; // 맨 윗줄이면 종료

            // 위줄
            string seqno_u = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle - 1, gcSEQNO).ToString();
            string kornm_u = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle - 1, gcKORNM).ToString();
            string engnm_u = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle - 1, gcENGNM).ToString();

            // 아랫줄
            string seqno_d = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcSEQNO).ToString();
            string kornm_d = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcKORNM).ToString();
            string engnm_d = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcENGNM).ToString();

            try
            {
                Save(seqno_u, kornm_u, engnm_u, seqno_d, kornm_d, engnm_d);
                // 위줄에 아랫줄의 값을 넣는다.
                grdMainView.SetRowCellValue(grdMainView.FocusedRowHandle - 1, gcKORNM, kornm_d);
                grdMainView.SetRowCellValue(grdMainView.FocusedRowHandle - 1, gcENGNM, engnm_d);
                // 아랫줄에 위줄의 값을 넣는다.
                grdMainView.SetRowCellValue(grdMainView.FocusedRowHandle, gcKORNM, kornm_u);
                grdMainView.SetRowCellValue(grdMainView.FocusedRowHandle, gcENGNM, engnm_u);

                grdMainView.FocusedRowHandle = grdMainView.FocusedRowHandle - 1;
                RefreshGridMain();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDn_Click(object sender, EventArgs e)
        {
            if (grdMainView.RowCount <= 1) return; // 1건이거나 없는 경우
            if (grdMainView.FocusedRowHandle >= grdMainView.RowCount - 1) return; // 맨 아랫줄이면 종료

            // 위줄
            string seqno_u = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcSEQNO).ToString();
            string kornm_u = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcKORNM).ToString();
            string engnm_u = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcENGNM).ToString();

            // 아랫줄
            string seqno_d = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle + 1, gcSEQNO).ToString();
            string kornm_d = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle + 1, gcKORNM).ToString();
            string engnm_d = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle + 1, gcENGNM).ToString();

            try
            {
                Save(seqno_u, kornm_u, engnm_u, seqno_d, kornm_d, engnm_d);
                // 위줄에 아랫줄의 값을 넣는다.
                grdMainView.SetRowCellValue(grdMainView.FocusedRowHandle, gcKORNM, kornm_d);
                grdMainView.SetRowCellValue(grdMainView.FocusedRowHandle, gcENGNM, engnm_d);
                // 아랫줄에 위줄의 값을 넣는다.
                grdMainView.SetRowCellValue(grdMainView.FocusedRowHandle + 1, gcKORNM, kornm_u);
                grdMainView.SetRowCellValue(grdMainView.FocusedRowHandle + 1, gcENGNM, engnm_u);

                grdMainView.FocusedRowHandle = grdMainView.FocusedRowHandle + 1;
                RefreshGridMain();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Save(string seqno_u, string kornm_u, string engnm_u, string seqno_d, string kornm_d, string engnm_d)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                OleDbTransaction tran = null;
                try
                {
                    tran = conn.BeginTransaction();

                    // UPDATE
                    string sql = "";
                    sql = "";
                    sql += System.Environment.NewLine + "UPDATE TZ16A";
                    sql += System.Environment.NewLine + "   SET KORNM=?";
                    sql += System.Environment.NewLine + "     , ENGNM=?";
                    sql += System.Environment.NewLine + " WHERE SEQNO=?";

                    using (OleDbCommand cmd = new OleDbCommand(sql, conn, tran))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@1", kornm_d));
                        cmd.Parameters.Add(new OleDbParameter("@2", engnm_d));
                        cmd.Parameters.Add(new OleDbParameter("@3", seqno_u));

                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@1", kornm_u));
                        cmd.Parameters.Add(new OleDbParameter("@2", engnm_u));
                        cmd.Parameters.Add(new OleDbParameter("@3", seqno_d));

                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
