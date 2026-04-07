using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0103E
{
    public partial class ADD0103E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;

        private bool IsFirst;

        public ADD0103E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Addpara = "";

            this.CreatePopupMenu();
        }

        public ADD0103E(String user, String pwd, String prjcd, String addpara)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Addpara = addpara;
        }

        private void CreatePopupMenu()
        {
            //
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("진료확인번호 승인요청", new EventHandler(mnuCfhcCfrNoReq_Click));
            grdMain.ContextMenu = cm;
        }

        private void ADD0103E_Load(object sender, EventArgs e)
        {
            IsFirst = true;
        }

        private void ADD0103E_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            try
            {
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    string hdate = MetroLib.Util.GetSysDate(conn);
                    txtFrdt.Text = hdate.Substring(0, 6) + "01";
                    txtTodt.Text = hdate;

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            List<CData> list = new List<CData>();
            grdMain.DataSource = list;

            string sql = "";
            sql += Environment.NewLine + "SELECT DISTINCT";
            sql += Environment.NewLine + "       S41.PID";
            sql += Environment.NewLine + "     , (SELECT A01.PNM FROM TA01 A01 WITH (NOLOCK) WHERE A01.PID=S41.PID) AS PNM";
            sql += Environment.NewLine + "     , S41.DPTCD";
            sql += Environment.NewLine + "     , S41.DRID";
            sql += Environment.NewLine + "     , S41.EXDT";
            sql += Environment.NewLine + "     , S41.FINDT";
            sql += Environment.NewLine + "     , S41.QFYCD";
            sql += Environment.NewLine + "     , S41.PDIV";
            sql += Environment.NewLine + "     , (SELECT X.RPID FROM TS41 X WHERE X.PID=S41.PID AND X.ENTDT=S41.ENTDT AND X.BDIV=S41.BDIV AND X.GRPNO=S41.GRPNO AND X.SEQ=0) AS RPID";
            sql += Environment.NewLine + "     , '' AS BIGO";             // 진료확인번호 요청결과(VB에서 사용하는 필드임)
            sql += Environment.NewLine + "     , (S41.PID+';'+S41.ENTDT+';'+S41.BDIV+';'+CONVERT(VARCHAR,S41.GRPNO)+';0') AS hKEY";
            sql += Environment.NewLine + "  FROM TS41 S41 WITH (NOLOCK)";
            sql += Environment.NewLine + " WHERE S41.EXDT >= '" + txtFrdt.Text.ToString() + "'";
            sql += Environment.NewLine + "   AND S41.EXDT <= '" + txtTodt.Text.ToString() + "'";
            sql += Environment.NewLine + "   AND S41.QFYCD LIKE '3%'";
            sql += Environment.NewLine + "   AND S41.TTAMT > 0";
            sql += Environment.NewLine + "   AND S41.UNPAM > 0";
            sql += Environment.NewLine + "   AND ((S41.SEQ>0 AND DPTCD<>'ER') OR (S41.SEQ=0 AND DPTCD='ER'))";
            sql += Environment.NewLine + "   AND ISNULL(RECFG,'') NOT IN ('F','D','O')";
            sql += Environment.NewLine + "   AND DBO.MFS_ADD_GET_CFHCCFRNO(S41.PID,S41.EXDT,S41.QFYCD,S41.DPTCD)=''";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.Clear();

                    data.SEL = true;
                    data.PID = reader["PID"].ToString();
                    data.PNM = reader["PNM"].ToString();
                    data.DPTCD = reader["DPTCD"].ToString();
                    data.DRID = reader["DRID"].ToString();
                    data.EXDT = reader["EXDT"].ToString();
                    data.FINDT = reader["FINDT"].ToString();
                    data.QFYCD = reader["QFYCD"].ToString();
                    data.PDIV = reader["PDIV"].ToString();
                    data.RPID = reader["RPID"].ToString();
                    data.BIGO = "";
                    data.HKEY = reader["HKEY"].ToString();

                    list.Add(data);

                    return true;
                });

                conn.Close();
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
            e.Graph.DrawString("진료확인번호점검", Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("진료일자:" + txtFrdt.Text.ToString() + " - " + txtTodt.Text.ToString(), Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void printableComponentLink_CreateReportFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            string sysDate = "";
            string sysTime = "";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                sysDate = MetroLib.Util.GetSysDate(conn);
                sysTime = MetroLib.Util.GetSysTime(conn);

                conn.Close();
            }
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 프로그램 ID
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("ADD0103E", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시(사실은 조회일시)
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(sysDate + " " + sysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void btnCfhcCfrNoReq_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.CfhcCfrNoReq();
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

        private void CfhcCfrNoReq()
        {
            for (int i = 0; i < grdMainView.RowCount; i++)
            {
                bool sel = (bool)grdMainView.GetRowCellValue(i, "SEL");
                if (sel == true)
                {
                    string hKey = grdMainView.GetRowCellValue(i, "HKEY").ToString();
                    grdMainView.SetRowCellValue(i, "BIGO", "진료확인번호 요청처리중입니다...");
                    RefreshGridMain();

                    string ret = ExecINSUCHECK(hKey, "1", true);

                    grdMainView.SetRowCellValue(i,"BIGO",ret);
                    RefreshGridMain();
                }
            }
        }

        private void mnuCfhcCfrNoReq_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                CfhcCfrNoReqOne();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void CfhcCfrNoReqOne()
        {
            int i = grdMainView.FocusedRowHandle;

            string hKey = grdMainView.GetRowCellValue(i, "HKEY").ToString();
            grdMainView.SetRowCellValue(i, "BIGO", "진료확인번호 요청처리중입니다...");
            RefreshGridMain();

            string ret = ExecINSUCHECK(hKey, "1", false);

            grdMainView.SetRowCellValue(i, "BIGO", ret);
            RefreshGridMain();
        }

        private string ExecINSUCHECK(string pKey, string pExDiv, bool pNoForm)
        {
            // 결과 파일 초기화
            if (System.IO.File.Exists("C:/Metro/DLL/ADD_INSUCHECK.out"))
            {
                System.IO.File.WriteAllText("C:/Metro/DLL/ADD_INSUCHECK.out", "", Encoding.Default);
            }
            // 호출
            string ret = "";
            string para = pKey + (char)21 + pExDiv + (char)21 + (pNoForm == true ? "TRUE" : "FALSE");
            string exefile = "C:/Metro/DLL/ADD_INSUCHECK.exe";
            string exefolder = "C:/Metro/DLL/";
            this.ExecCmd(exefile,exefolder,para);
            // 결과 읽기
            string out_key = "";
            string out_value = "";
            if (System.IO.File.Exists("C:/Metro/DLL/ADD_INSUCHECK.out"))
            {
                string[] lines = System.IO.File.ReadAllLines("C:/Metro/DLL/ADD_INSUCHECK.out", Encoding.Default);
                out_key = lines[0];
                out_value = lines[1];
            }
            if (para != out_key)
            {
                ret = "파라메터 값이 이상합니다.";
            }
            else
            {
                ret = out_value;
            }
            return ret;
        }

        private int ExecCmd(string fileName, string execfolder, string args)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = fileName;
            p.StartInfo.Arguments = args;
            p.StartInfo.WorkingDirectory = execfolder;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            p.Start();
            p.WaitForExit();

            return p.ExitCode;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int rowHandle = 0; rowHandle < grdMainView.RowCount; rowHandle++)
            {
                grdMainView.SetRowCellValue(rowHandle, "SEL", chkAll.Checked);
            }
        }
    }
}
