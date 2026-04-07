using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0724E
{
    public partial class ADD0724E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        

        public ADD0724E()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Addpara = "";
            m_HospMulti = "";
        }

        public ADD0724E(String user, String pwd, String prjcd, String addpara)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Addpara = addpara;
            m_HospMulti = GetHospmulti();

            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.Net").CreateSubKey("ADD0724E");
            chkMemoView.Checked = reg.GetValue(chkMemoView.Name, "False").ToString() == "True" ? true : false;
            grdSubView.OptionsView.AutoCalcPreviewLineCount = chkMemoView.Checked;
            grdSubView.AppearancePrint.Preview.TextOptions.WordWrap = (chkMemoView.Checked == true ? DevExpress.Utils.WordWrap.Wrap : DevExpress.Utils.WordWrap.NoWrap);

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
                sql = "";
                sql += Environment.NewLine + "SELECT F1.JSDEMSEQ";
                sql += Environment.NewLine + "     , F1.JSREDAY";
                sql += Environment.NewLine + "     , F1.FMGBN";
                sql += Environment.NewLine + "     , F1.JRFG";
                sql += Environment.NewLine + "     , F2.JSTOTAMT";
                sql += Environment.NewLine + "     , F1.CNECNO";
                sql += Environment.NewLine + "     , F1.DEMSEQ";
                sql += Environment.NewLine + "     , F1.DEMNO";
                sql += Environment.NewLine + "     , F1.DOCUNO AS F1_DOCUNO";
                sql += Environment.NewLine + "     , F2.DOCUNO AS F2_DOCUNO";
                sql += Environment.NewLine + "     , F1.DOCUDT AS F1_DOCUDT";
                sql += Environment.NewLine + "     , F2.DOCUDT AS F2_DOCUDT";
                sql += Environment.NewLine + "     , F1.JSREDPT1 AS F1_JSREDPT1";
                sql += Environment.NewLine + "     , F1.JSREDPNM AS F1_JSREDPNM";
                sql += Environment.NewLine + "     , F1.JSRETELE AS F1_JSRETELE";
                sql += Environment.NewLine + "     , F2.JSREDPT1 AS F2_JSREDPT1";
                sql += Environment.NewLine + "     , F2.JSREDPNM AS F2_JSREDPNM";
                sql += Environment.NewLine + "     , F2.JSRETELE AS F2_JSRETELE";
                sql += Environment.NewLine + "     , F2.JSSAU";
                sql += Environment.NewLine + "     , F2.MEMO";
                sql += Environment.NewLine + "     , F1.VERSION";
                sql += Environment.NewLine + "     , F1.DCOUNT";
                sql += Environment.NewLine + "     , F1.HOSID";
                sql += Environment.NewLine + "     , F1.HOSNM";
                sql += Environment.NewLine + "     , F1.JIWONCD";
                sql += Environment.NewLine + "     , F2.JSYYSEQ";
                sql += Environment.NewLine + "     , H010.IOFG";
                sql += Environment.NewLine + "  FROM TIE_F1301 F1 INNER JOIN TIE_F1302 F2 ON F2.JSDEMSEQ=F1.JSDEMSEQ";
                sql += Environment.NewLine + "                                           AND F2.JSREDAY=F1.JSREDAY";
                sql += Environment.NewLine + "                                           AND F2.FMGBN=F1.FMGBN";
                sql += Environment.NewLine + "                                           AND F2.CNECNO=F1.CNECNO";
                sql += Environment.NewLine + "                                           AND F2.DCOUNT=F1.DCOUNT";
                sql += Environment.NewLine + "                    LEFT  JOIN TIE_H010 H010 ON F1.DEMNO = H010.DEMNO"; // 2013.09.23 KJW - 입원외래 구분추가.
                sql += Environment.NewLine + " WHERE 1=1";
                if (txtFrdt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F1.JSREDAY>='" + txtFrdt.Text.ToString() + "'";
                }
                if (txtTodt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F1.JSREDAY<='" + txtTodt.Text.ToString() + "'";
                }
                if (txtCnecno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F1.CNECNO='" + txtCnecno.Text.ToString() + "'";
                }
                if (txtDemno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F1.DEMNO='" + txtDemno.Text.ToString() + "'";
                }
                if (txtJsdemseq.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F1.JSDEMSEQ='" + txtJsdemseq.Text.ToString() + "'";
                }
                if (rbFmgbnF130.Checked == true)
                {
                    sql += Environment.NewLine + "   AND F1.FMGBN='F130'";
                }
                if (rbFmgbnF140.Checked == true)
                {
                    sql += Environment.NewLine + "   AND F1.FMGBN='F140'";
                }
                if (rbFmgbnF150.Checked == true)
                {
                    sql += Environment.NewLine + "   AND F1.FMGBN='F150'";
                }
                if (rbQfy2.Checked == true)
                {
                    sql += Environment.NewLine + "   AND F1.JRFG='4'";
                }
                if (rbQfy3.Checked == true)
                {
                    sql += Environment.NewLine + "   AND F1.JRFG='5'";
                }
                if (rbIofg1.Checked == true)
                {
                    sql += Environment.NewLine + "   AND H010.IOFG IN ('1','4')";
                }
                if (rbIofg2.Checked == true)
                {
                    sql += Environment.NewLine + "   AND H010.IOFG IN ('2','3','5')";
                }
                sql += Environment.NewLine + " ORDER BY F1.JSREDAY DESC,F1.JSDEMSEQ";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.Clear();

                    data.JSDEMSEQ = reader["JSDEMSEQ"].ToString().TrimEnd();
                    data.JSREDAY = reader["JSREDAY"].ToString().TrimEnd();
                    data.JSTOTAMT = ToLong(reader["JSTOTAMT"].ToString().TrimEnd());
                    data.CNECNO = reader["CNECNO"].ToString().TrimEnd();
                    data.DEMSEQ = reader["DEMSEQ"].ToString().TrimEnd();
                    data.DEMNO = reader["DEMNO"].ToString().TrimEnd();
                    data.F1_DOCUNO = reader["F1_DOCUNO"].ToString().TrimEnd();
                    data.F2_DOCUNO = reader["F2_DOCUNO"].ToString().TrimEnd();
                    data.F1_DOCUDT = reader["F1_DOCUDT"].ToString().TrimEnd();
                    data.F2_DOCUDT = reader["F2_DOCUDT"].ToString().TrimEnd();
                    data.F1_JSREDPT1 = reader["F1_JSREDPT1"].ToString().TrimEnd();
                    data.F1_JSREDPNM = reader["F1_JSREDPNM"].ToString().TrimEnd();
                    data.F1_JSRETELE = reader["F1_JSRETELE"].ToString().TrimEnd();
                    data.F2_JSREDPT1 = reader["F2_JSREDPT1"].ToString().TrimEnd();
                    data.F2_JSREDPNM = reader["F2_JSREDPNM"].ToString().TrimEnd();
                    data.F2_JSRETELE = reader["F2_JSRETELE"].ToString().TrimEnd();
                    data.JSSAU = reader["JSSAU"].ToString().TrimEnd();
                    data.MEMO = reader["MEMO"].ToString().TrimEnd();
                    data.IOFG = reader["IOFG"].ToString().TrimEnd();
                    data.FMGBN = reader["FMGBN"].ToString().TrimEnd();
                    data.JRFG = reader["JRFG"].ToString().TrimEnd();
                    data.VERSION = reader["VERSION"].ToString().TrimEnd();
                    data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd();
                    data.HOSID = reader["HOSID"].ToString().TrimEnd();
                    data.HOSNM = reader["HOSNM"].ToString().TrimEnd();
                    data.JIWONCD = reader["JIWONCD"].ToString().TrimEnd();
                    data.JSYYSEQ = reader["JSYYSEQ"].ToString().TrimEnd();

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

        private void QuerySub()
        {
            if (grdMainView.FocusedRowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle) return;

            List<CDataSub> list = new List<CDataSub>();
            grdSub.DataSource = null;
            grdSub.DataSource = list;

            string jsdemseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "JSDEMSEQ").ToString();
            string jsreday = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "JSREDAY").ToString();
            string fmgbn = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "FMGBN").ToString();
            string cnecno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "CNECNO").ToString();
            string dcount = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DCOUNT").ToString();
            string jsyyseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "JSYYSEQ").ToString();
            string demno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DEMNO").ToString();
            string iofg = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "IOFG").ToString();

            string tTI1A = "TI1A";
            if (iofg == "2") tTI1A = "TI2A";

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql = "";
                sql += Environment.NewLine + "SELECT F3.EPRTNO";
                sql += Environment.NewLine + "     , F3.PNM";
                sql += Environment.NewLine + "     , F3.JSAMT";
                sql += Environment.NewLine + "     , F3.JSAMT_JIW";
                sql += Environment.NewLine + "     , F3.JSAMT_MAX";
                sql += Environment.NewLine + "     , F3.JSAMT_JAE";
                sql += Environment.NewLine + "     , F3.JSAMT_OUT";
                sql += Environment.NewLine + "     , F3.JSAMT1";
                sql += Environment.NewLine + "     , F3.JSAMT2";
                sql += Environment.NewLine + "     , F3.MEMO";
                sql += Environment.NewLine + "     , A.PID";
                sql += Environment.NewLine + "     , A.JRKWA";
                sql += Environment.NewLine + "     , A.PDRID";
                sql += Environment.NewLine + "     , (SELECT A07.DRNM FROM TA07 A07 WHERE A07.DRID=A.PDRID) AS DRNM";
                sql += Environment.NewLine + "  FROM TIE_F1303 F3 LEFT JOIN " + tTI1A + " A ON A.DEMNO='" + demno + "' AND A.EPRTNO=F3.EPRTNO";
                sql += Environment.NewLine + " WHERE F3.JSDEMSEQ='" + jsdemseq + "'";
                sql += Environment.NewLine + "   AND F3.JSREDAY='" + jsreday + "'";
                sql += Environment.NewLine + "   AND F3.FMGBN='" + fmgbn + "'";
                sql += Environment.NewLine + "   AND F3.CNECNO='" + cnecno + "'";
                sql += Environment.NewLine + "   AND F3.DCOUNT='" + dcount + "'";
                sql += Environment.NewLine + "   AND F3.JSYYSEQ='" + jsyyseq + "'";
                sql += Environment.NewLine + " ORDER BY F3.EPRTNO";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataSub data = new CDataSub();
                    data.Clear();

                    data.EPRTNO = reader["EPRTNO"].ToString().TrimEnd();
                    data.PNM = reader["PNM"].ToString().TrimEnd();
                    data.JSAMT = ToLong(reader["JSAMT"].ToString().TrimEnd());
                    data.JSAMT_JIW = ToLong(reader["JSAMT_JIW"].ToString().TrimEnd());
                    data.JSAMT_MAX = ToLong(reader["JSAMT_MAX"].ToString().TrimEnd());
                    data.JSAMT_JAE = ToLong(reader["JSAMT_JAE"].ToString().TrimEnd());
                    data.JSAMT_OUT = ToLong(reader["JSAMT_OUT"].ToString().TrimEnd());
                    data.JSAMT1 = ToLong(reader["JSAMT1"].ToString().TrimEnd());
                    data.JSAMT2 = ToLong(reader["JSAMT2"].ToString().TrimEnd());
                    data.MEMO = reader["MEMO"].ToString().TrimEnd();
                    data.PID = reader["PID"].ToString().TrimEnd();
                    data.JRKWA = reader["JRKWA"].ToString().TrimEnd();
                    data.PDRID = reader["PDRID"].ToString().TrimEnd();
                    data.DRNM = reader["DRNM"].ToString().TrimEnd();
                    data.MAINROW = grdMainView.FocusedRowHandle.ToString();

                    list.Add(data);

                    return true;
                });
            }

            RefreshGridSub();
        }

        private long ToLong(string value)
        {
            long ret = 0;
            long.TryParse(value, out ret);
            return ret;
        }

        private void grdMain_DoubleClick(object sender, EventArgs e)
        {
            /*
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
            */
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

        private bool m_isPrintSub = false; // 어떤 그리드를 출력하는지 구분하는 변수

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
                grdSubView.Columns["MEMO"].Visible = false;
                grdSubView.OptionsView.ShowPreview = true;

                this.Print();

                grdSubView.OptionsView.ShowPreview = false;
                grdSubView.Columns["MEMO"].Visible = true;

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
            if (m_isPrintSub == true)
            {
                printableComponentLink.Margins = new System.Drawing.Printing.Margins(10, 10, 224, 50);
            }
            else
            {
                printableComponentLink.Margins = new System.Drawing.Printing.Margins(10, 10, 100, 50);
            }

            printableComponentLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
            printableComponentLink.Landscape = true;
            printingSystem.Links.Add(printableComponentLink);
            printableComponentLink.Component = m_isPrintSub == true ? grdSub : grdMain;
            //printableComponentLink.ShowPreview(); // <-- 아래 세 줄로 변경
            printableComponentLink.CreateDocument(printingSystem);
            DevExpress.XtraPrinting.PrintTool printTool = new DevExpress.XtraPrinting.PrintTool(printableComponentLink.PrintingSystemBase);
            this.CloseProgressForm("", "");
            Cursor.Current = Cursors.Default;
            printTool.ShowPreviewDialog(this, DevExpress.LookAndFeel.UserLookAndFeel.Default);
        }

        private Color m_borderColor = Color.DimGray;
        private Color m_backColor = Color.LightGray;

        private void printableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            try
            {
                string title = "[보험,보호]비용결정서";
                string printInfo = "";
                string printInfo2 = "";
                if (m_isPrintSub == false)
                {
                    // 가운데 정렬
                    e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
                    e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);

                    // 제목
                    e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
                    e.Graph.DrawString(title, Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
                }
                else
                {
                    int mainrow = 0;
                    int.TryParse(grdSubView.GetRowCellValue(0, "MAINROW").ToString(), out mainrow);

                    string jsdemseq = grdMainView.GetRowCellValue(mainrow, "JSDEMSEQ").ToString();
                    string jsreday = grdMainView.GetRowCellValue(mainrow, "JSREDAY").ToString();
                    string jstotamt = grdMainView.GetRowCellValue(mainrow, "JSTOTAMT").ToString();
                    string cnecno = grdMainView.GetRowCellValue(mainrow, "CNECNO").ToString();
                    string demseq = grdMainView.GetRowCellValue(mainrow, "DEMSEQ").ToString();
                    string demno = grdMainView.GetRowCellValue(mainrow, "DEMNO").ToString();
                    string docuno = grdMainView.GetRowCellValue(mainrow, "DOCUNO").ToString();
                    string docudt = grdMainView.GetRowCellValue(mainrow, "DOCUDT").ToString();
                    string iofgnm = grdMainView.GetRowCellValue(mainrow, "IOFGNM").ToString();
                    string jsredept = grdMainView.GetRowCellValue(mainrow, "JSREDEPT").ToString();
                    string remark = grdMainView.GetRowCellValue(mainrow, "REMARK").ToString();

                    jstotamt = MetroLib.StrHelper.ToNumberWithComma(jstotamt);

                    printInfo = "정산심사 차수 : " + jsdemseq + ", 통보일자 : " + jsreday + ", 결정차액 : " + jstotamt + ", 접수번호 : " + cnecno + ", 심사차수 : " + demseq + ", 청구번호 : " + demno + ", 문서번호 : " + docuno + ", 완료일자 : " + docudt + ", 입외구분 : " + iofgnm;
                    printInfo2 = "담당자 : " + jsredept;

                    // 가운데 정렬
                    e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
                    e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);

                    // 제목
                    if (m_isPrintSub == true) title += "(수진자)";
                    e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
                    e.Graph.DrawString(title, Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);

                    // 왼쪽 정렬
                    e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
                    e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);

                    // 조회조건
                    e.Graph.Font = new Font(grdSubView.Appearance.Row.Font.Name, 9F, System.Drawing.FontStyle.Regular);

                    e.Graph.DrawRect(new RectangleF(0, 60, 1080, 198), DevExpress.XtraPrinting.BorderSide.All, Color.White, m_borderColor); // 제일 바깥 테두리

                    DrawTextBox(e, 0, 60, 300, 18, "정산심사", true);
                    DrawTextBox(e, 0, 78, 100, 18, "차수", true);
                    DrawTextBox(e, 100, 78, 100, 18, "통보일자", true);
                    DrawTextBox(e, 200, 78, 100, 18, "결정차액합계", true);

                    DrawTextBox(e, 300, 60, 100, 36, "접수번호", true);
                    DrawTextBox(e, 400, 60, 100, 36, "심사차수", true);
                    DrawTextBox(e, 500, 60, 100, 36, "청구번호", true);

                    DrawTextBox(e, 600, 60, 300, 18, "문서", true);
                    DrawTextBox(e, 600, 78, 200, 18, "번호", true);
                    DrawTextBox(e, 800, 78, 100, 18, "완료일자", true);

                    DrawTextBox(e, 900, 60, 182, 36, "입외구분", true);

                    DrawTextBox(e, 0, 96, 100, 18, jsdemseq);
                    DrawTextBox(e, 100, 96, 100, 18, jsreday);
                    DrawTextBox(e, 200, 96, 100, 18, jstotamt);

                    DrawTextBox(e, 300, 96, 100, 18, cnecno);
                    DrawTextBox(e, 400, 96, 100, 18, demseq);
                    DrawTextBox(e, 500, 96, 100, 18, demno);

                    DrawTextBox(e, 600, 96, 200, 18, docuno);
                    DrawTextBox(e, 800, 96, 100, 18, docudt);

                    DrawTextBox(e, 900, 96, 182, 18, iofgnm);

                    DrawTextBox(e, 0, 114, 100, 18, "담당자", true);

                    e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Near);
                    e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Near);

                    DrawTextBox(e, 100, 114, 982, 18, jsredept);

                    e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
                    e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);

                    DrawTextBox(e, 0, 132, 100, 90, "참조란",true);

                    e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Near);
                    e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Near);

                    DrawTextBox(e, 100, 132, 982, 90, remark);

                    // 가운데 정렬
                    e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
                    e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void DrawTextBox(DevExpress.XtraPrinting.CreateAreaEventArgs e, float x, float y, float w, float h, string text)
        {
            DrawTextBox(e, x, y, w, h, text, false);
        }

        private void DrawTextBox(DevExpress.XtraPrinting.CreateAreaEventArgs e, float x, float y, float w, float h, string text, bool isBackColor)
        {
            Color backColor = isBackColor == true ? m_backColor : Color.White;
            e.Graph.DrawRect(new RectangleF(x, y, w, h), DevExpress.XtraPrinting.BorderSide.All, backColor, m_borderColor);
            e.Graph.BackColor = backColor;
            if (text == "참조란")
            {
                e.Graph.DrawString(text, Color.Black, new RectangleF(x + 1, y + 1, w - 2, h - 25), DevExpress.XtraPrinting.BorderSide.None);
            }
            else
            {
                e.Graph.DrawString(text, Color.Black, new RectangleF(x + 1, y + 1, w - 2, h - 2), DevExpress.XtraPrinting.BorderSide.None);
            }
        }

        private void printableComponentLink_CreateReportFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            try
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
                e.Graph.DrawString("ADD0724E", Color.Black, new RectangleF(0, 0, 120, 20), DevExpress.XtraPrinting.BorderSide.None);
                // 출력일시
                e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
                e.Graph.DrawString(sysDate + " " + sysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
            }
            catch (Exception ex)
            {
            }
        }

        private void grdMainView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            txtRedept.Text = "";
            txtRemark.Text = "";
            txtMemo.Text = "";
            try
            {
                txtRedept.Text = grdMainView.GetFocusedRowCellValue("JSREDEPT").ToString();
                txtRemark.Text = grdMainView.GetFocusedRowCellValue("REMARK").ToString();
                txtMemo.Text = grdSubView.GetFocusedRowCellValue("MEMO").ToString();
            }
            catch (Exception ex)
            {
            }

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

        private void grdSubView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            txtMemo.Text = grdSubView.GetFocusedRowCellValue("MEMO").ToString();
        }

        private void grdMain_Resize(object sender, EventArgs e)
        {
            int width = grdMain.Width;
            int new_width = width - (963 - 150);
            grdMainView.Columns["JSREDEPT"].Width = new_width / 2;
            grdMainView.Columns["REMARK"].Width = new_width - (new_width / 2);
        }

        private void grdSub_Resize(object sender, EventArgs e)
        {
            int width = grdSub.Width;
            grdSubView.Columns["MEMO"].Width = width - (963 - 80); // 디자인 때 grdSum.width = 963, MEMO column.width = 80
        }

        private void chkMemoView_CheckedChanged(object sender, EventArgs e)
        {
            grdSubView.OptionsView.RowAutoHeight = chkMemoView.Checked;
            grdSubView.OptionsView.AutoCalcPreviewLineCount = chkMemoView.Checked;
            grdSubView.AppearancePrint.Preview.TextOptions.WordWrap = (chkMemoView.Checked == true ? DevExpress.Utils.WordWrap.Wrap : DevExpress.Utils.WordWrap.NoWrap);

            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.Net").CreateSubKey("ADD0724E");
            reg.SetValue(chkMemoView.Name, chkMemoView.Checked == true ? "True" : "False");
        }
    }
}
