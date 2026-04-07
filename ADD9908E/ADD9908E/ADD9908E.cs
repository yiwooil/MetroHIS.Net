using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD9908E
{
    public partial class ADD9908E : Form
    {
        private bool IsFirst;

        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        public ADD9908E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
        }

        public ADD9908E(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }

        private void ADD9908E_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD9908E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "초기화 중입니다.");
                this.QueryJBUnicd();
                this.Query();
                this.CloseProgressForm("", "초기화 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "초기화 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void QueryJBUnicd()
        {
            List<CJBUnicd> jb_list = new List<CJBUnicd>();

            grdJBUnicd.DataSource = null;
            grdJBUnicd.DataSource = jb_list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                // 표준기호조회
                string sql = "";
                sql = "SELECT MST3CD,CDNM FROM TI88 WHERE MST1CD='A' AND MST2CD='JBUNICD' ORDER BY MST3CD";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CJBUnicd jb_data = new CJBUnicd();
                        jb_data.Clear();
                        jb_data.STDCODE = reader["MST3CD"].ToString();
                        jb_data.STDNAME = reader["CDNM"].ToString();

                        jb_list.Add(jb_data);
                    }
                    reader.Close();
                }

                conn.Close();
            
            }
            this.RefreshGridJBUnicd();
        }

        private void Query()
        {
            List<CData> list = new List<CData>();

            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                // 원내에서 사용하는 코드
                string sql = "";
                sql = "SELECT A52.UNICD,A52.UNINM,A52.JBUNICD,I88.CDNM AS JBUNINM FROM TA52 A52 LEFT JOIN TI88 I88 ON I88.MST1CD='A' AND I88.MST2CD='JBUNICD' AND I88.MST3CD=A52.JBUNICD WHERE A52.QLFYCD LIKE '6%' ORDER BY A52.UNICD";

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CData data = new CData();
                        data.Clear();
                        data.UNICD = reader["UNICD"].ToString();
                        data.UNINM = reader["UNINM"].ToString();
                        data.JBUNICD = reader["JBUNICD"].ToString();
                        data.JBUNINM = reader["JBUNINM"].ToString();

                        list.Add(data);
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

        private void RefreshGridJBUnicd()
        {
            if (grdJBUnicd.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdJBUnicd.BeginInvoke(new Action(() => grdJBUnicdView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdJBUnicdView.RefreshData();
                Application.DoEvents();
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdMainView.FocusedRowHandle < 0) return;
                if (grdJBUnicdView.FocusedRowHandle < 0) return;

                string unicd = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcUNICD).ToString();
                if (unicd == "") return;
                string jbunicd = grdJBUnicdView.GetRowCellValue(grdJBUnicdView.FocusedRowHandle, gcSTDCODE).ToString();

                SaveJBUnicd(unicd, jbunicd);
                Query();

                // 처리한 줄로 포커스 이동
                for (int i = 0; i < grdMainView.RowCount; i++)
                {
                    if (unicd == grdMainView.GetRowCellValue(i, gcUNICD).ToString())
                    {
                        grdMainView.FocusedRowHandle = i;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdMainView.FocusedRowHandle < 0) return;

                string unicd = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcUNICD).ToString();
                if (unicd == "") return;

                SaveJBUnicd(unicd, "");
                Query();

                // 처리한 줄로 포커스 이동
                for (int i = 0; i < grdMainView.RowCount; i++)
                {
                    if (unicd == grdMainView.GetRowCellValue(i, gcUNICD).ToString())
                    {
                        grdMainView.FocusedRowHandle = i;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveJBUnicd(string p_unicd, string p_jbunicd)
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "UPDATE TA52";
                sql += System.Environment.NewLine + "   SET JBUNICD=?";
                sql += System.Environment.NewLine + " WHERE UNICD=?";

                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@1", p_jbunicd));
                    cmd.Parameters.Add(new OleDbParameter("@2", p_unicd));

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        private void grdJBUnicdView_DoubleClick(object sender, EventArgs e)
        {
            btnApply.PerformClick();
        }

    }
}
