using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0251Q
{
    public partial class ADD0251Q : Form
    {
        private String m_User;
        private String m_Pwd;

        private String m_SysDate;
        private String m_SysTime;

        public ADD0251Q()
        {
            InitializeComponent();
            m_User = "";
            m_Pwd = "";
        }

        public ADD0251Q(String user, String pwd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
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
            if (txtYYMM.Text.ToString() == "")
            {
                MessageBox.Show("청구월을 입력하세요.");
                return;
            }

            String strFrdt = txtYYMM.Text.ToString() + "01";
            String strTodt = txtYYMM.Text.ToString() + "31";

            grdMain.DataSource = null;
            List<CData> list = new List<CData>();
            grdMain.DataSource = list;

            string sql = "";
            sql += System.Environment.NewLine + "SELECT T42.SYSDT, T41.PID, A01.PNM, A04.BEDEDT, A04.BEDODT, ";
            sql += System.Environment.NewLine + "       T42.QFYCD bQFYCD, T42.DPTCD bDPTCD, T42.RPDT bRPDT, ";
            sql += System.Environment.NewLine + "       CASE WHEN SUBSTRING(T42.RPID,7,1) = 'D' THEN '퇴원' ELSE '분할' END bRPID, T42.UNAMT bUNAMT, I2A.SIMNO, ";
            sql += System.Environment.NewLine + "       T41.QFYCD aQFYCD, T41.DPTCD aDPTCD, T41.RPDT aRPDT, ";
            sql += System.Environment.NewLine + "       CASE WHEN SUBSTRING(T41.RPID,7,1) = 'D' THEN '퇴원' ELSE '분할' END aRPID, T41.UNAMT aUNAMT, ";
            sql += System.Environment.NewLine + "       I2A.JRBY, I2A.UNISQ ";
            sql += System.Environment.NewLine + "FROM   TT41 T41 INNER JOIN TT42 T42 ON   T41.PID   = T42.PID ";
            sql += System.Environment.NewLine + "                                     AND T41.BDEDT = T42.BDEDT ";
            sql += System.Environment.NewLine + "                LEFT  JOIN TI2A I2A ON   T42.RPDT  = I2A.BDODT ";
            sql += System.Environment.NewLine + "                                     AND T42.QFYCD = I2A.QFYCD ";
            sql += System.Environment.NewLine + "                                     AND T42.PID   = I2A.PID ";
            sql += System.Environment.NewLine + "                INNER JOIN TA04 A04 ON   T41.PID   = A04.PID ";
            sql += System.Environment.NewLine + "                                     AND T41.BDEDT = A04.BEDEDT ";
            sql += System.Environment.NewLine + "                INNER JOIN TA01 A01 ON   T41.PID   = A01.PID ";
            sql += System.Environment.NewLine + "WHERE  T41.RPDT >= '" + strFrdt + "' ";
            sql += System.Environment.NewLine + "   AND T41.RPDT <= '" + strTodt + "' ";
            sql += System.Environment.NewLine + "   AND T41.DRID  = 'zzzzzz'  ";
            sql += System.Environment.NewLine + "   AND T41.PID NOT LIKE 'T%' ";
            sql += System.Environment.NewLine + "   AND T42.DRID  = 'zzzzzz'  ";
            sql += System.Environment.NewLine + "   AND ISNULL(I2A.SIMCS,1) = '1' ";
            sql += System.Environment.NewLine + "ORDER BY T41.PID, T41.BDEDT, T42.SYSDT ";

            string strConn = DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                m_SysDate = CUtil.GetSysDate(conn);
                m_SysTime = CUtil.GetSysTime(conn);

                // TSQL문장과 Connection 객체를 지정   
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CData data = new CData();
                    data.Clear();
                    data.CANCELDT = reader["SYSDT"].ToString();
                    data.PID = reader["PID"].ToString();
                    data.PNM = reader["PNM"].ToString();
                    data.BEDEDT = reader["BEDEDT"].ToString();
                    data.BEDODT = reader["BEDODT"].ToString();
                    data.QFYCD_B = reader["BQFYCD"].ToString();
                    data.DPTCD_B = reader["BDPTCD"].ToString();
                    data.RPDT_B = reader["BRPDT"].ToString();
                    data.RPID_B = reader["BRPID"].ToString();
                    data.UNAMT_B = reader["BUNAMT"].ToString();
                    data.SIMNO_B = reader["SIMNO"].ToString();
                    data.QFYCD_A = reader["AQFYCD"].ToString();
                    data.DPTCD_A = reader["ADPTCD"].ToString();
                    data.RPDT_A = reader["ARPDT"].ToString();
                    data.RPID_A = reader["ARPID"].ToString();
                    data.UNAMT_A = reader["AUNAMT"].ToString();

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
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString("퇴원재수납환자리스트", Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("청구월:" + txtYYMM.Text.ToString(), Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void printableComponentLink_CreateReportFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 프로그램 ID
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("ADD0251Q", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시(사실은 조회일시)
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(m_SysDate + " " + m_SysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void grdMainView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
    }
}
