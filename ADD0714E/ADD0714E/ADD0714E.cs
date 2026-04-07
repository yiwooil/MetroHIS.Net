using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0714E
{
    public partial class ADD0714E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        public ADD0714E()
        {
            InitializeComponent();
            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Addpara = "";
            m_HospMulti = "";
        }

        public ADD0714E(String user, String pwd, String prjcd, String addpara)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Addpara = addpara;
            m_HospMulti = GetHospmulti();
        }

        private string GetHospmulti()
        {
            try
            {
                string ret = "";
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    string sql = "";
                    sql = "SELECT MULTIFG FROM TA94 WHERE USRID='" + m_User + "' AND PRJID='" + m_Prjcd + "'";
                    MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                    {
                        ret = reader["MULTIFG"].ToString();
                        return false;
                    });
                }
                return ret;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
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
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "SELECT * FROM TIE_F0611 WHERE DEMSEQ='" + txtDemseq.Text.ToString() + "'";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    txtHosid.Text = reader["HOSID"].ToString();
                    txtReday.Text = reader["REDAY"].ToString();
                    txtMemo.Text = reader["MEMO"].ToString();

                    return false;
                });

                sql = "SELECT * FROM TIE_F0612 WHERE DEMSEQ='" + txtDemseq.Text.ToString() + "' ORDER BY DEMSEQ, CNECNO, GRPNO, HOSID";
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.Clear();
                    data.CNECNO = reader["CNECNO"].ToString();
                    data.GRPNO = reader["GRPNO"].ToString();
                    data.HOSID = reader["HOSID"].ToString();
                    data.GUBUN = "심결";
                    long acnt = 0;
                    long.TryParse(reader["ACNT"].ToString(), out acnt);
                    data.ACNT = acnt;
                    long attamt = 0;
                    long.TryParse(reader["ATTAMT"].ToString(), out attamt);
                    data.ATTAMT = attamt;
                    long aptamt = 0;
                    long.TryParse(reader["APTAMT"].ToString(), out aptamt);
                    data.APTAMT = aptamt;
                    long ajam = 0;
                    long.TryParse(reader["AJAM"].ToString(), out ajam);
                    data.AJAM = ajam;
                    long aunamt = 0;
                    long.TryParse(reader["AUNAMT"].ToString(), out aunamt);
                    data.AUNAMT = aunamt;
                    long apmgum = 0;
                    long.TryParse(reader["APMGUM"].ToString(), out apmgum);
                    data.APMGUM = apmgum;
                    long astgum = 0;
                    long.TryParse(reader["ASTGUM"].ToString(), out astgum);
                    data.ASTGUM = astgum;
                    long agumak = 0;
                    long.TryParse(reader["AGUMAK"].ToString(), out agumak);
                    data.AGUMAK = agumak;
                    long ampamt = 0;
                    long.TryParse(reader["AMPAMT"].ToString(), out ampamt);
                    data.AMPAMT = ampamt;
                    long rcnt = 0;
                    long.TryParse(reader["RCNT"].ToString(), out rcnt);
                    data.RCNT = rcnt;
                    long rttamt = 0;
                    long.TryParse(reader["RTTAMT"].ToString(), out rttamt);
                    data.RTTAMT = rttamt;
                    data.MEMO = "";


                    list.Add(data);

                    if (data.RCNT != 0 || data.RTTAMT!=0)
                    {
                        CData data2 = new CData();
                        data2.Clear();
                        data2.GUBUN = "반송";
                        data2.ACNT = data.RCNT;
                        data2.ATTAMT = data.RTTAMT;
                        list.Add(data2);
                    }

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
            e.Graph.DrawString("[보호]심사결과 총괄표", Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("심사차수:" + txtDemseq.Text.ToString(), Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void printableComponentLink_CreateReportFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            string sysDate = "";
            string sysTime = "";
            try
            {
                string strConn = MetroLib.DBHelper.GetConnectionString();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    sysDate = MetroLib.Util.GetSysDate(conn);
                    sysTime = MetroLib.Util.GetSysTime(conn);
                }
            }
            catch (Exception ex)
            {
                // 무시
                sysDate = "00000000";
                sysTime = "000000";
            }

            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 프로그램 ID
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("ADD0714E", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(sysDate + " " + sysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ADD0714E_01 f = new ADD0714E_01();
            f.ShowDialog(this);
            txtDemseq.Text = f.m_demseq;
            f = null;
            btnQuery.PerformClick();
        }
    }
}
