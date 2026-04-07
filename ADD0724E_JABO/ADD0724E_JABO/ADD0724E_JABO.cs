using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0724E_JABO
{
    public partial class ADD0724E_JABO : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        public ADD0724E_JABO()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Addpara = "";
            m_HospMulti = "";
        }

        public ADD0724E_JABO(String user, String pwd, String prjcd, String addpara)
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
                this.Query("");
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

        private void Query(string queryOption)
        {
            List<CData> list = new List<CData>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                SetFrdtTodt(queryOption, conn);

                string sql = "";
                sql += Environment.NewLine + "SELECT N1.JSDEMSEQ";
                sql += Environment.NewLine + "     , N1.JSREDAY";
                sql += Environment.NewLine + "     , N1.FMGBN";
                sql += Environment.NewLine + "     , N1.JRFG";
                sql += Environment.NewLine + "     , N2.JSTOTAMT";
                sql += Environment.NewLine + "     , N1.CNECNO";
                sql += Environment.NewLine + "     , N1.DEMSEQ";
                sql += Environment.NewLine + "     , N1.DEMNO";
                sql += Environment.NewLine + "     , N2.JSREDPT1";
                sql += Environment.NewLine + "     , N2.JSREDPNM";
                sql += Environment.NewLine + "     , N2.JSRETELE";
                sql += Environment.NewLine + "     , N2.JSSAU";
                sql += Environment.NewLine + "     , N2.MEMO";
                sql += Environment.NewLine + "     , N1.VERSION";
                sql += Environment.NewLine + "     , N1.DCOUNT";
                sql += Environment.NewLine + "     , N1.HOSID";
                sql += Environment.NewLine + "     , N1.HOSNM";
                sql += Environment.NewLine + "     , N1.JBUNICD";
                sql += Environment.NewLine + "     , N2.JSYYSEQ";
                sql += Environment.NewLine + "     , N2.JSTTTAMT";
                sql += Environment.NewLine + "     , N2.JSJBPTAMT";
                sql += Environment.NewLine + "  FROM TIE_N1301 N1 INNER JOIN TIE_N1302 N2 ON N2.JSDEMSEQ=N1.JSDEMSEQ ";
                sql += Environment.NewLine + "                                           AND N2.JSREDAY =N1.JSREDAY";
                sql += Environment.NewLine + "                                           AND N2.FMGBN   =N1.FMGBN";
                sql += Environment.NewLine + "                                           AND N2.CNECNO  =N1.CNECNO";
                sql += Environment.NewLine + "                                           AND N2.DCOUNT  =N1.DCOUNT";
                sql += Environment.NewLine + " WHERE 1=1";
                if (txtFrdt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND N1.JSREDAY>='" + txtFrdt.Text.ToString() + "'";
                }
                if (txtTodt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND N1.JSREDAY<='" + txtTodt.Text.ToString() + "'";
                }
                if (txtCnecno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND N1.CNECNO='" + txtCnecno.Text.ToString() + "'";
                }
                if (txtDemno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND N1.DEMNO='" + txtDemno.Text.ToString() + "'";
                }
                if (txtJsdemseq.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND N1.JSDEMSEQ='" + txtJsdemseq.Text.ToString() + "'";
                }
                if (rbFmgbnN130.Checked == true)
                {
                    sql += Environment.NewLine + "   AND N1.FMGBN='N130'";
                }
                if (rbFmgbnN150.Checked == true)
                {
                    sql += Environment.NewLine + "   AND N1.FMGBN='N150'";
                }
                sql += Environment.NewLine + " ORDER BY N1.JSREDAY DESC,N1.JSDEMSEQ";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.Clear();
                    data.JSDEMSEQ = reader["JSDEMSEQ"].ToString();
                    data.JSREDAY = reader["JSREDAY"].ToString();
                    long jstotamt = 0;
                    long.TryParse(reader["JSTOTAMT"].ToString(), out jstotamt);
                    data.JSTOTAMT = jstotamt;
                    long jstttamt = 0;
                    long.TryParse(reader["JSTTTAMT"].ToString(), out jstttamt);
                    data.JSTTTAMT = jstttamt;
                    long jsjbptamt = 0;
                    long.TryParse(reader["JSJBPTAMT"].ToString(), out jsjbptamt);
                    data.JSJBPTAMT = jsjbptamt;
                    data.CNECNO = reader["CNECNO"].ToString();
                    data.DEMSEQ = reader["DEMSEQ"].ToString();
                    data.DEMNO = reader["DEMNO"].ToString();
                    data.JBUNICD = reader["JBUNICD"].ToString();
                    data.JSREDPT1 = reader["JSREDPT1"].ToString().TrimEnd();
                    data.JSREDPNM = reader["JSREDPNM"].ToString().TrimEnd();
                    data.JSRETELE = reader["JSRETELE"].ToString().TrimEnd();
                    data.JSSAU = reader["JSSAU"].ToString().TrimEnd();
                    data.MEMO = reader["MEMO"].ToString().TrimEnd();
                    data.FMGBN = reader["FMGBN"].ToString();
                    data.JRFG = reader["JRFG"].ToString();
                    data.VERSION = reader["VERSION"].ToString();
                    data.DCOUNT = reader["DCOUNT"].ToString();
                    data.HOSID = reader["HOSID"].ToString();
                    data.HOSNM = reader["HOSNM"].ToString();
                    data.JSYYSEQ = reader["JSYYSEQ"].ToString();

                    list.Add(data);

                    return true;
                });
            }

            RefreshGridMain();
        }

        private void SetFrdtTodt(string queryOption, OleDbConnection conn)
        {
            if (queryOption == "") return;
            if (queryOption == "5")
            {
                // 제한없음
                txtFrdt.Text = "";
                txtTodt.Text = "";
                return;
            }


            DateTime dtFrdt = DateTime.Now;
            DateTime dtTodt = DateTime.Now;

            string sysdt = MetroLib.Util.GetSysDate(conn);
            DateTime.TryParse(DateTime.ParseExact(sysdt, "yyyyMMdd", null).ToString("yyyy-MM-dd"), out dtTodt);
                
            if (queryOption == "0")
            {
                // 최근 1년
                dtFrdt = dtTodt.AddYears(-1);
            }
            else if (queryOption == "1")
            {
                // 최근 6개월
                dtFrdt = dtTodt.AddMonths(-6);
            }
            else if (queryOption == "2")
            {
                // 최근 3개월
                dtFrdt = dtTodt.AddMonths(-3);
            }
            else if (queryOption == "3")
            {
                // 최근 1개월
                dtFrdt = dtTodt.AddMonths(-1);
            }
            else if (queryOption == "4")
            {
                // 최근 1주
                dtFrdt = dtTodt.AddDays(-7);
            }
            txtFrdt.Text = dtFrdt.ToString("yyyyMMdd");
            txtTodt.Text = dtTodt.ToString("yyyyMMdd");
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

        private void RefreshGridSub()
        {
            if (grdMain.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdSub.BeginInvoke(new Action(() => grdSubView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdSubView.RefreshData();
                Application.DoEvents();
            }
        }

        private void btnQueryOneYear_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query("0");
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

        private void btnQuerySixMonth_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query("1");
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

        private void btnQueryThreeMonth_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query("2");
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

        private void btnQueryOneMonth_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query("3");
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

        private void btnQueryOneWeek_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query("4");
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

        private void btnQueryNoLimit_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Query("5");
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

        private void grdMainView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", "조회 중입니다.");
                this.QuerySub();
                this.CloseProgressForm("", "조회 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", "조회 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void QuerySub()
        {
            CDataSub sum = new CDataSub();
            sum.Clear();
            sum.PNM = "합계";

            string jsdemseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcJSDEMSEQ).ToString();
            string jsreday = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcJSREDAY).ToString();
            string fmgbn = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcFMGBN).ToString();
            string cnecno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcCNECNO).ToString();
            string dcount = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDCOUNT).ToString();
            string jsyyseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcJSYYSEQ).ToString();
            
            List<CDataSub> list = new List<CDataSub>();
            grdSub.DataSource = null;
            grdSub.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql += Environment.NewLine + "SELECT N3.EPRTNO ";
                sql += Environment.NewLine + "     , N3.PNM ";
                sql += Environment.NewLine + "     , N3.APPRNO ";
                sql += Environment.NewLine + "     , N3.JSAMT ";
                sql += Environment.NewLine + "     , N3.JSTTTAMT ";
                sql += Environment.NewLine + "     , N3.JSJBPTAMT ";
                sql += Environment.NewLine + "     , N3.JSAMT1 ";
                sql += Environment.NewLine + "     , N3.JSAMT2 ";
                sql += Environment.NewLine + "     , RTRIM(N3.MEMO) AS MEMO ";
                sql += Environment.NewLine + "  FROM TIE_N1303 N3 ";
                sql += Environment.NewLine + " WHERE N3.JSDEMSEQ='" + jsdemseq + "' ";
                sql += Environment.NewLine + "   AND N3.JSREDAY ='" + jsreday + "' ";
                sql += Environment.NewLine + "   AND N3.FMGBN   ='" + fmgbn + "' ";
                sql += Environment.NewLine + "   AND N3.CNECNO  ='" + cnecno + "' ";
                sql += Environment.NewLine + "   AND N3.DCOUNT  ='" + dcount + "' ";
                sql += Environment.NewLine + "   AND N3.JSYYSEQ ='" + jsyyseq + "' ";
                sql += Environment.NewLine + " ORDER BY N3.EPRTNO";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataSub data = new CDataSub();
                    data.Clear();
                    long eprtno = 0;
                    long.TryParse(reader["EPRTNO"].ToString(), out eprtno);
                    data.EPRTNO = eprtno;
                    data.PNM = reader["PNM"].ToString().TrimEnd();
                    data.APPRNO = reader["APPRNO"].ToString().TrimEnd();
                    long jsamt = 0;
                    long.TryParse(reader["JSAMT"].ToString(), out jsamt);
                    data.JSAMT = jsamt;
                    long jstttamt = 0;
                    long.TryParse(reader["JSTTTAMT"].ToString(), out jstttamt);
                    data.JSTTTAMT = jstttamt;
                    long jsjbptamt = 0;
                    long.TryParse(reader["JSJBPTAMT"].ToString(), out jsjbptamt);
                    data.JSJBPTAMT = jsjbptamt;
                    long jsamt1 = 0;
                    long.TryParse(reader["JSAMT1"].ToString(), out jsamt1);
                    data.JSAMT1 = jsamt1;
                    long jsamt2 = 0;
                    long.TryParse(reader["JSAMT2"].ToString(), out jsamt2);
                    data.JSAMT2 = jsamt2;
                    data.MEMO = reader["MEMO"].ToString().TrimEnd();
                    data.MAINROW = grdMainView.FocusedRowHandle; // 출력할 때 헤더정보를 가져오기 위한 값

                    list.Add(data);

                    // 합계
                    sum.JSAMT += data.JSAMT;
                    sum.JSTTTAMT += data.JSTTTAMT;
                    sum.JSJBPTAMT += data.JSJBPTAMT;
                    sum.JSAMT1 += data.JSAMT1;
                    sum.JSAMT2 += data.JSAMT2;

                    return true;
                });
            }
            list.Add(sum);

            RefreshGridSub();
        }

        private bool m_isPrintSub; // 어떤 그리드를 출력하는지 구분하는 변수

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                m_isPrintSub = false;
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

        private void btnPrintSub_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                m_isPrintSub = true;
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
            printableComponentLink.Component = m_isPrintSub == true ? grdSub : grdMain;
            printableComponentLink.ShowPreview();
        }

        private void printableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            string printInfo = "";
            if (m_isPrintSub == true)
            {
                int mainrow = 0;
                int.TryParse(grdSubView.GetRowCellValue(0, gcMAINROW).ToString(), out mainrow);

                string jsdemseq = grdMainView.GetRowCellValue(mainrow, gcJSDEMSEQ).ToString();
                string jsreday = grdMainView.GetRowCellValue(mainrow, gcJSREDAY).ToString();
                string cnecno = grdMainView.GetRowCellValue(mainrow, gcCNECNO).ToString();
                string demseq = grdMainView.GetRowCellValue(mainrow, gcDEMSEQ).ToString();
                string demno = grdMainView.GetRowCellValue(mainrow, gcDEMNO).ToString();
                string jsredept = grdMainView.GetRowCellValue(mainrow, gcJSREDEPT).ToString();

                printInfo = "정산심사 차수:" + jsdemseq + ", 정산심사 통보일자:" + jsreday + ", 접수번호:" + cnecno + ", 심사차수:" + demseq + ", 청구번호:" + demno + ", 담당자:" + jsredept;
            }

            //
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString("[자보]이의제기결과통보서", Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(printInfo, Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
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
            e.Graph.DrawString("ADD0724E_JABO", Color.Black, new RectangleF(0, 0, 120, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(sysDate + " " + sysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

    }
}
