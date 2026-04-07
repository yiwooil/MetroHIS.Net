using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0108E
{
    public partial class ADD0108E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;

        private string m_QryDate;
        private string m_QryTime;

        public ADD0108E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";

            m_QryDate = "";
            m_QryTime = "";
        }


        public ADD0108E(String user, String pwd, String prjcd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
        }

    private void ADD0108E_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";

                // 환자자격
                cboQfycd.Items.Clear();
                cboQfycd.Items.Add("전체");
                sql = "SELECT MST3CD, CDNM FROM TA88 (nolock) WHERE MST1CD='A' AND MST2CD='26' AND ISNULL(EXPDT,'') = '' ORDER BY MST3CD ";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cboQfycd.Items.Add(reader["MST3CD"].ToString() + " " + reader["CDNM"].ToString());
                    }
                }
                cboQfycd.SelectedIndex = 0;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtYYMM.Text.ToString().Length != 6)
            {
                MessageBox.Show("수납년월을 확인하세요.");
                return;
            }

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
            List<CData> list = new List<CData>();

            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string yymm = txtYYMM.Text.ToString();
            string frdt = yymm + "01";
            string todt = yymm + "31";
            string qfycd = (cboQfycd.SelectedItem == null || cboQfycd.SelectedItem.ToString() == "전체" ? "" : cboQfycd.SelectedItem.ToString().Split(' ')[0]);

            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT S41.QFYCD ";
            sql += System.Environment.NewLine + "     , LEFT(S41.EXDT,6) AS EXMM ";
            sql += System.Environment.NewLine + "     , S41.PID ";
            sql += System.Environment.NewLine + "     , A01.PNM ";
            sql += System.Environment.NewLine + "     , S41.EXDT ";
            sql += System.Environment.NewLine + "     , S41.FINDT ";
            sql += System.Environment.NewLine + "     , S41.DPTCD ";
            sql += System.Environment.NewLine + "     , S41.BDIV ";
            sql += System.Environment.NewLine + "     , S41.PDIV ";
            sql += System.Environment.NewLine + "     , A88.CDNM AS PDIVNM";
            sql += System.Environment.NewLine + "     , S41.GONSGB ";
            sql += System.Environment.NewLine + "     , S41.DAETC ";
            sql += System.Environment.NewLine + "     , S41.TTAMT ";
            sql += System.Environment.NewLine + "     , S41.UNPAM ";
            sql += System.Environment.NewLine + "     , S41.ISPAM ";
            sql += System.Environment.NewLine + "     , S41.UISAM ";
            sql += System.Environment.NewLine + "     , (SELECT X.PREVKEY FROM TS41 X WHERE X.PID=S41.PID AND X.ENTDT=S41.ENTDT AND X.BDIV=S41.BDIV AND X.GRPNO=S41.GRPNO AND X.SEQ=0) AS PREVKEY ";
            sql += System.Environment.NewLine + "     , (SELECT COUNT(*) FROM TI12 I12 WHERE I12.PID=S41.PID AND I12.ENTDT=S41.ENTDT AND I12.BDIV=S41.BDIV AND I12.GRPNO=S41.GRPNO AND I12.SEQ=S41.SEQ) AS SUMCNT ";
            sql += System.Environment.NewLine + "  FROM TS41 S41 INNER JOIN TA01 A01 ON A01.PID=S41.PID ";
            sql += System.Environment.NewLine + "                LEFT  JOIN TA88 A88 ON A88.MST1CD='A' AND A88.MST2CD='23' AND A88.MST3CD=S41.PDIV ";
            sql += System.Environment.NewLine + " WHERE S41.FINDT BETWEEN ? AND ? ";
            sql += System.Environment.NewLine + "   AND S41.EXDT < ? ";
            sql += System.Environment.NewLine + "   AND S41.SEQ>0 ";

            if (qfycd != "")
            {
                sql += System.Environment.NewLine + "   AND S41.QFYCD = ? ";
            }
            else
            {
                sql += System.Environment.NewLine + "   AND LEFT(QFYCD,1) IN ('2','3','4','5','6','7','8','9') ";
            }
                
            sql += System.Environment.NewLine + "   AND ISNULL(S41.RECFG,'') NOT IN ('F','D','O') ";
            sql += System.Environment.NewLine + "   AND NOT (S41.BDIV='3' AND S41.PDIV='3') "; //응급6시간이상자 제외
            sql += System.Environment.NewLine + "   AND NOT (S41.BDIV='1' AND S41.PDIV='6') "; //정신과낮병동 제외
            sql += System.Environment.NewLine + "   AND NOT (S41.PDIV='D')                  "; //통원수술 제외
            sql += System.Environment.NewLine + " ORDER BY S41.QFYCD,LEFT(S41.EXDT,6),S41.PID,S41.EXDT ";
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                m_QryDate = MetroLib.Util.GetSysDate(conn);
                m_QryTime = MetroLib.Util.GetSysTime(conn);

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {

                    cmd.Parameters.Add(new OleDbParameter("@1", frdt));
                    cmd.Parameters.Add(new OleDbParameter("@2", todt));
                    cmd.Parameters.Add(new OleDbParameter("@3", frdt));
                    if (qfycd != "")
                    {
                        cmd.Parameters.Add(new OleDbParameter("@4", qfycd));
                    }

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int tmp = 0;
                        CData data = new CData();
                        data.Clear();
                        data.QFYCD = reader["QFYCD"].ToString();
                        data.EXMM = reader["EXMM"].ToString();
                        data.PID = reader["PID"].ToString();
                        data.PNM = reader["PNM"].ToString();
                        data.EXDT = reader["EXDT"].ToString();
                        data.FINDT = reader["FINDT"].ToString();
                        data.DPTCD = reader["DPTCD"].ToString();
                        data.BDIV = reader["BDIV"].ToString();
                        data.PDIV = reader["PDIV"].ToString();
                        data.PDIVNM = reader["PDIVNM"].ToString();
                        data.GONSGB = reader["GONSGB"].ToString();
                        data.DAETC = reader["DAETC"].ToString();
                        tmp = 0; int.TryParse(reader["TTAMT"].ToString(), out tmp); data.TTAMT = tmp;
                        tmp = 0; int.TryParse(reader["UNPAM"].ToString(), out tmp); data.UNAMT = tmp;
                        tmp = 0; int.TryParse(reader["ISPAM"].ToString(), out tmp); data.ISPAM = tmp;
                        tmp = 0; int.TryParse(reader["UISAM"].ToString(), out tmp); data.UISAM = tmp;
                        data.PREVKEY = reader["PREVKEY"].ToString();
                        int.TryParse(reader["SUMCNT"].ToString(), out data.SUMCNT);

                        list.Add(data);
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Print();
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

        private void Print()
        {
            DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink = new DevExpress.XtraPrinting.PrintableComponentLink();
            printableComponentLink.CreateMarginalHeaderArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(printableComponentLink_CreateReportHeaderArea);
            printableComponentLink.CreateMarginalFooterArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(printableComponentLink_CreateReportFooterArea);
            printableComponentLink.Margins = new System.Drawing.Printing.Margins(10, 10, 100, 50);
            printableComponentLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
            printableComponentLink.Landscape = true;
            printingSystem.Links.Add(printableComponentLink);
            printableComponentLink.Component = grdMain;
            printableComponentLink.ShowPreview();
        }

        private void printableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            String strTitle = "전월진료수납내역";
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString(strTitle, Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            String strYYMM = txtYYMM.Text.ToString();
            String strQfycd = cboQfycd.SelectedItem.ToString();
            String strCaption = "";
            strCaption += "수납년월 : " + strYYMM + " 진료자격 : " + strQfycd;
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(strCaption, Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void printableComponentLink_CreateReportFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 프로그램 ID
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("ADD0108E", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시(사실은 조회일시)
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(m_QryDate + " " + m_QryTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

    }
}
