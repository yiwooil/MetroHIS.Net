using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD8002Q
{
    public partial class ADD8002Q_P : Form
    {
        public Boolean m_SEL;
        public String m_ACCNO;
        public String m_CNTNO;

        private Boolean IsFirst;

        public ADD8002Q_P()
        {
            InitializeComponent();
        }

        private void ADD8002Q_P_Load(object sender, EventArgs e)
        {
            IsFirst = true;
            m_SEL = false;
            m_ACCNO = "";
            m_CNTNO = "";
        }

        private void ADD8002Q_P_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "자료검색 중입니다.");
                this.Query();
                this.CloseProgressForm("", "자료검색 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "자료검색 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void Query()
        {

            List<CDataP> list = new List<CDataP>();

            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string sql = "";
            sql += System.Environment.NewLine + "SELECT ACCNO,CNTNO,REPDT,PAYDT,CNTTOT,DEMTOT,REALPAYAMT,MEMO ";
            sql += System.Environment.NewLine + "  FROM TIE_I020 ";
            sql += System.Environment.NewLine + " ORDER BY REPDT DESC,ACCNO,CNTNO ";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                // TSQL문장과 Connection 객체를 지정   
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CDataP data = new CDataP();
                    data.Clear();
                    data.ACCNO = reader["ACCNO"].ToString();
                    data.CNTNO = reader["CNTNO"].ToString();
                    data.REPDT = reader["REPDT"].ToString();
                    data.PAYDT = reader["PAYDT"].ToString();
                    data.CNTTOT = reader["CNTTOT"].ToString();
                    data.DEMTOT = reader["DEMTOT"].ToString();
                    data.REALPAYAMT = reader["REALPAYAMT"].ToString();
                    data.MEMO = reader["MEMO"].ToString().Trim();

                    list.Add(data);

                }
                reader.Close();

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
            DevExpress.Utils.DXMouseEventArgs ea = e as DevExpress.Utils.DXMouseEventArgs;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = grdMainView.CalcHitInfo(ea.Location);
            if (info.RowHandle < 0) return;
            List<CDataP> list = (List<CDataP>)grdMain.DataSource;
            CDataP data = list[info.RowHandle];

            m_SEL = true;
            m_ACCNO = data.ACCNO;
            m_CNTNO = data.CNTNO;

            this.Close();
        }
    }
}
