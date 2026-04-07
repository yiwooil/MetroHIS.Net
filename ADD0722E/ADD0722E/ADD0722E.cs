using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0722E
{
    public partial class ADD0722E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private int m_PrevTabIndex = -1;
        private string m_ErrPos = "";

        public ADD0722E()
        {
            InitializeComponent();
        }
        public ADD0722E(String user, String pwd, String prjcd, String addpara)
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

        private void CreatePopupMenu()
        {
            //
            //ContextMenu cm = new ContextMenu();
            //cm.MenuItems.Add("보완청구내성생성", new EventHandler(mnuBOMake_Click));
            //cm.MenuItems.Add("이의신청", new EventHandler(mnuObjMake_Click));
            //grdMain.ContextMenu = cm;
        }

        private void ADD0722E_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            m_PrevTabIndex = 0;
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
                sql += Environment.NewLine + "SELECT F0701.VERSION"; // 버전구분
                sql += Environment.NewLine + "     , F0701.JSDEMSEQ"; // 정산심사차수
                sql += Environment.NewLine + "     , F0701.JSREDAY"; // 정산통보일자
                sql += Environment.NewLine + "     , F0701.CNECNO"; // 접수번호
                sql += Environment.NewLine + "     , F0701.DCOUNT"; // 청구서 일련번호
                sql += Environment.NewLine + "     , F0701.FMNO"; // 서식번호
                sql += Environment.NewLine + "     , F0701.HOSID"; // 요양기관 기호
                sql += Environment.NewLine + "     , F0701.JIWONCD"; // 지원코드
                sql += Environment.NewLine + "     , F0701.DEMSEQ"; // 심사차수(yyyymm+seq(2))
                sql += Environment.NewLine + "     , F0701.DEMNO"; // 청구번호
                sql += Environment.NewLine + "     , F0701.GRPNO"; // 묶음번호
                sql += Environment.NewLine + "     , F0701.CNECYY"; // 접수년도(CCYY)
                sql += Environment.NewLine + "     , F0701.DEMUNITFG"; // 청구단위구분
                sql += Environment.NewLine + "     , F0701.JRFG"; // 보험자종별구분
                sql += Environment.NewLine + "     , F0702.JSYYSEQ"; // 정산연번
                sql += Environment.NewLine + "     , F0702.SIMGBN"; // 심사구분
                sql += Environment.NewLine + "     , F0702.JSREDPT1";
                sql += Environment.NewLine + "     , F0702.JSREDPT2";
                sql += Environment.NewLine + "     , F0702.JSREDPNM";
                sql += Environment.NewLine + "     , F0702.JSREDPNO";
                sql += Environment.NewLine + "     , F0702.JSRETELE"; // 정산담당
                sql += Environment.NewLine + "     , F0702.JSBUSSCD"; // 정산업무코드
                sql += Environment.NewLine + "     , F0702.JSBUSSNM"; // 정산업무명
                sql += Environment.NewLine + "     , F0702.SKPMGUM"; // 이전심결사항-본인부담환급금
                sql += Environment.NewLine + "     , F0702.SKPMGUM1"; // 이전심결사항-본인부담환급금1
                sql += Environment.NewLine + "     , F0702.SKPMGUM2"; // 이전심결사항-본인부담환급금2
                sql += Environment.NewLine + "     , F0702.SKJJAMT"; // 이전심결사항-조정금액
                sql += Environment.NewLine + "     , F0702.SKHOSRETAMT"; // 이전심결사항-요양기관환수금 합계
                sql += Environment.NewLine + "     , F0702.SKUNAMT"; // 이전심결사항-보험자부담금
                sql += Environment.NewLine + "     , F0702.SKBHUNAMT"; // 이전심결사항-보훈청구액
                sql += Environment.NewLine + "     , F0702.SKRSTAMT"; // 이전심결사항-심사결정액
                sql += Environment.NewLine + "     , F0702.SKCNT"; // 이전심결사항-건수합계
                sql += Environment.NewLine + "     , F0702.JSPMGUM"; // 정산심결사항-본인부담환급금
                sql += Environment.NewLine + "     , F0702.JSPMGUM1"; // 정산심결사항-본인부담환급금1
                sql += Environment.NewLine + "     , F0702.JSPMGUM2"; // 정산심결사항-본인부담환급금2
                sql += Environment.NewLine + "     , F0702.JSJJAMT"; // 정산심결사항-조정금액
                sql += Environment.NewLine + "     , F0702.JSHOSRETAMT"; // 정산심결사항-요양기관환수금 합계
                sql += Environment.NewLine + "     , F0702.JSUNAMT"; // 정산심결사항-보험자부담금 합계
                sql += Environment.NewLine + "     , F0702.JSBHUNAMT"; // 정산심결사항-보훈부담금
                sql += Environment.NewLine + "     , F0702.JSRSTAMT"; // 정산심결사항-심사결정액
                sql += Environment.NewLine + "     , F0702.JSRSTCHAAMT"; // 정산심결사항-결정차액
                sql += Environment.NewLine + "     , F0702.JSCNT"; // 정산심결사항-건수 합계
                sql += Environment.NewLine + "     , F0702.MEMO"; // 명일련비고사항
                sql += Environment.NewLine + "     , F0702.SKBHPMGUM";
                sql += Environment.NewLine + "     , F0702.JSBHPMGUM";
                sql += Environment.NewLine + "  FROM TIE_F0701 F0701 INNER JOIN TIE_F0702 F0702 ON F0702.JSDEMSEQ = F0701.JSDEMSEQ";
                sql += Environment.NewLine + "                                                 AND F0702.JSREDAY  = F0701.JSREDAY ";
                sql += Environment.NewLine + "                                                 AND F0702.CNECNO   = F0701.CNECNO  ";
                sql += Environment.NewLine + "                                                 AND F0702.DCOUNT   = F0701.DCOUNT  ";
                sql += Environment.NewLine + " WHERE 1=1 ";
                if (txtFrdt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0701.JSREDAY>='" + txtFrdt.Text.ToString() + "'";
                }
                if (txtTodt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0701.JSREDAY<='" + txtTodt.Text.ToString() + "'";
                }
                if (txtCnecno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0701.CNECNO='" + txtCnecno.Text.ToString() + "'";
                }
                if (txtDemno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0701.DEMNO='" + txtDemno.Text.ToString() + "'";
                }
                if (txtInsmm.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0701.FTDAYS LIKE '" + txtInsmm.Text.ToString() + "%' ";
                }
                sql += Environment.NewLine + " ORDER BY F0701.JSREDAY DESC";

                long no = 0;
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.Clear();

                    data.NO = (++no);

                    data.VERSION = reader["VERSION"].ToString().TrimEnd(); // 버전구분
                    data.JSDEMSEQ = reader["JSDEMSEQ"].ToString().TrimEnd(); // 정산심사차수
                    data.JSREDAY = reader["JSREDAY"].ToString().TrimEnd(); // 정산통보일자
                    data.CNECNO = reader["CNECNO"].ToString().TrimEnd(); // 접수번호
                    data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd(); // 청구서 일련번호
                    data.FMNO = reader["FMNO"].ToString().TrimEnd(); // 서식번호
                    data.HOSID = reader["HOSID"].ToString().TrimEnd(); // 요양기관 기호
                    data.JIWONCD = reader["JIWONCD"].ToString().TrimEnd(); // 지원코드
                    data.DEMSEQ = reader["DEMSEQ"].ToString().TrimEnd(); // 심사차수(yyyymm+seq(2))
                    data.DEMNO = reader["DEMNO"].ToString().TrimEnd(); // 청구번호
                    data.GRPNO = reader["GRPNO"].ToString().TrimEnd(); // 묶음번호
                    data.CNECYY = reader["CNECYY"].ToString().TrimEnd(); // 접수년도(CCYY)
                    data.DEMUNITFG = reader["DEMUNITFG"].ToString().TrimEnd(); // 청구단위구분
                    data.JRFG = reader["JRFG"].ToString().TrimEnd(); // 보험자종별구분
                    data.JSYYSEQ = reader["JSYYSEQ"].ToString().TrimEnd(); // 정산연번
                    data.SIMGBN = reader["SIMGBN"].ToString().TrimEnd(); // 심사구분
                    data.JSREDPT1 = reader["JSREDPT1"].ToString().TrimEnd();
                    data.JSREDPT2 = reader["JSREDPT2"].ToString().TrimEnd();
                    data.JSREDPNM = reader["JSREDPNM"].ToString().TrimEnd();
                    data.JSREDPNO = reader["JSREDPNO"].ToString().TrimEnd();
                    data.JSRETELE = reader["JSRETELE"].ToString().TrimEnd(); // 정산담당
                    data.JSBUSSCD = reader["JSBUSSCD"].ToString().TrimEnd(); // 정산업무코드
                    data.JSBUSSNM = reader["JSBUSSNM"].ToString().TrimEnd(); // 정산업무명
                    data.SKPMGUM = MetroLib.StrHelper.ToLong(reader["SKPMGUM"].ToString().TrimEnd()); // 이전심결사항-본인부담환급금
                    data.SKPMGUM1 = MetroLib.StrHelper.ToLong(reader["SKPMGUM1"].ToString().TrimEnd()); // 이전심결사항-본인부담환급금1
                    data.SKPMGUM2 = MetroLib.StrHelper.ToLong(reader["SKPMGUM2"].ToString().TrimEnd()); // 이전심결사항-본인부담환급금2
                    data.SKJJAMT = MetroLib.StrHelper.ToLong(reader["SKJJAMT"].ToString().TrimEnd()); // 이전심결사항-조정금액
                    data.SKHOSRETAMT = MetroLib.StrHelper.ToLong(reader["SKHOSRETAMT"].ToString().TrimEnd()); // 이전심결사항-요양기관환수금 합계
                    data.SKUNAMT = MetroLib.StrHelper.ToLong(reader["SKUNAMT"].ToString().TrimEnd()); // 이전심결사항-보험자부담금
                    data.SKBHUNAMT = MetroLib.StrHelper.ToLong(reader["SKBHUNAMT"].ToString().TrimEnd()); // 이전심결사항-보훈청구액
                    data.SKRSTAMT = MetroLib.StrHelper.ToLong(reader["SKRSTAMT"].ToString().TrimEnd()); // 이전심결사항-심사결정액
                    data.SKCNT = MetroLib.StrHelper.ToLong(reader["SKCNT"].ToString().TrimEnd()); // 이전심결사항-건수합계
                    data.JSPMGUM = MetroLib.StrHelper.ToLong(reader["JSPMGUM"].ToString().TrimEnd()); // 정산심결사항-본인부담환급금
                    data.JSPMGUM1 = MetroLib.StrHelper.ToLong(reader["JSPMGUM1"].ToString().TrimEnd()); // 정산심결사항-본인부담환급금1
                    data.JSPMGUM2 = MetroLib.StrHelper.ToLong(reader["JSPMGUM2"].ToString().TrimEnd()); // 정산심결사항-본인부담환급금2
                    data.JSJJAMT = MetroLib.StrHelper.ToLong(reader["JSJJAMT"].ToString().TrimEnd()); // 정산심결사항-조정금액
                    data.JSHOSRETAMT = MetroLib.StrHelper.ToLong(reader["JSHOSRETAMT"].ToString().TrimEnd()); // 정산심결사항-요양기관환수금 합계
                    data.JSUNAMT = MetroLib.StrHelper.ToLong(reader["JSUNAMT"].ToString().TrimEnd()); // 정산심결사항-보험자부담금 합계
                    data.JSBHUNAMT = MetroLib.StrHelper.ToLong(reader["JSBHUNAMT"].ToString().TrimEnd()); // 정산심결사항-보훈부담금
                    data.JSRSTAMT = MetroLib.StrHelper.ToLong(reader["JSRSTAMT"].ToString().TrimEnd()); // 정산심결사항-심사결정액
                    data.JSRSTCHAAMT = MetroLib.StrHelper.ToLong(reader["JSRSTCHAAMT"].ToString().TrimEnd()); // 정산심결사항-결정차액
                    data.JSCNT = MetroLib.StrHelper.ToLong(reader["JSCNT"].ToString().TrimEnd()); // 정산심결사항-건수 합계
                    data.SKBHPMGUM = MetroLib.StrHelper.ToLong(reader["SKBHPMGUM"].ToString().TrimEnd());
                    data.JSBHPMGUM = MetroLib.StrHelper.ToLong(reader["JSBHPMGUM"].ToString().TrimEnd());
                    data.MEMO = reader["MEMO"].ToString().TrimEnd(); // 명일련비고사항

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

        private void btnQueryOneYear_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                this.Query("0");
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQuerySixMonth_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                this.Query("1");
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQueryThreeMonth_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                this.Query("2");
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQueryOneMonth_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                this.Query("3");
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQueryOneWeek_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                this.Query("4");
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQueryNoLimit_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                this.Query("5");
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.CloseProgressForm("", (sender as Button).Text.ToString() + " 조회 중입니다.");
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
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

        private void RefreshGridPtnt()
        {
            if (grdPtnt.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdPtnt.BeginInvoke(new Action(() => grdPtntView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdPtntView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridCode()
        {
            if (grdCode.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdCode.BeginInvoke(new Action(() => grdCodeView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdCodeView.RefreshData();
                Application.DoEvents();
            }
        }

        private void RefreshGridBonrt()
        {
            if (grdBonrt.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdBonrt.BeginInvoke(new Action(() => grdBonrtView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdBonrtView.RefreshData();
                Application.DoEvents();
            }
        }

        private void grdMainView_KeyDown(object sender, KeyEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {
                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != String.Empty)
                {
                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());
                }
                else
                {
                    Clipboard.SetText("");
                    //MessageBox.Show("The value in the selected cell is null or empty!");
                }
                e.Handled = true;
            }
        }

        private void grdMainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle) return;
                string remark = grdMainView.GetRowCellValue(e.RowHandle, "REMARK").ToString();
                txtMemo.Text = remark;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 0)
            {
                if (m_PrevTabIndex == 0)
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
                        MessageBox.Show(ex.Message + Environment.NewLine + m_ErrPos);
                    }

                }
            }
            m_PrevTabIndex = tabControl1.SelectedIndex;
        }

        private void QuerySub()
        {
            if (grdMainView.FocusedRowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle) return;

            string jsdemseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "JSDEMSEQ").ToString();
            string jsreday = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "JSREDAY").ToString();
            string cnecno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "CNECNO").ToString();
            string dcount = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DCOUNT").ToString();
            string jsyyseq =  grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "JSYYSEQ").ToString();
            string jscnt = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "JSCNT").ToString();
            string jsamt = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "JSRSTCHAAMT").ToString();

            string demseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DEMSEQ").ToString();
            string demno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DEMNO").ToString();
            string jsredpt = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "JSREDEPT").ToString();
            string memo = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "MEMO").ToString();

            txtHeadJsdemseq.Text = jsdemseq;
            txtHeadJsreday.Text = jsreday;
            txtHeadCnecno.Text = cnecno;
            txtHeadDemseq.Text = demseq;
            txtHeadDemno.Text = demno;
            txtHeadRedpt.Text = jsredpt;
            txtHeadMemo.Text = memo;
            txtHeadJscnt.Text = jscnt;
            txtHeadJsamt.Text = jsamt;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                m_ErrPos = "QueryPtnt";
                QueryPtnt(jsdemseq, jsreday, cnecno, dcount, jsyyseq, conn);
                m_ErrPos = "QueryCode";
                QueryCode(jsdemseq, jsreday, cnecno, dcount, jsyyseq, conn);
                m_ErrPos = "QueryBonrt";
                QueryBonrt(jsdemseq, jsreday, cnecno, dcount, jsyyseq, conn);
            }
        }

        private void QueryPtnt(string p_jsdemseq, string p_jsreday, string p_cnecno, string p_dcount, string p_jsyyseq, OleDbConnection p_conn)
        {
            List<CDataPtnt> list = new List<CDataPtnt>();
            grdPtnt.DataSource = null;
            grdPtnt.DataSource = list;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT JSDEMSEQ"; // 정산심사차수
            sql += Environment.NewLine + "     , JSREDAY"; // 정산통보일자
            sql += Environment.NewLine + "     , CNECNO"; // 접수번호
            sql += Environment.NewLine + "     , DCOUNT"; // 청구서 일련번호
            sql += Environment.NewLine + "     , JSYYSEQ"; // 정산연번
            sql += Environment.NewLine + "     , JSSEQNO"; // 정산일련번호
            sql += Environment.NewLine + "     , EPRTNO"; // 명세서 일련번호
            sql += Environment.NewLine + "     , GUBUN"; // 구분(1.환급 2.환수 3.환수+환급)
            sql += Environment.NewLine + "     , PNM"; // 수진자성명
            sql += Environment.NewLine + "     , INSNM"; // 가입자 성명
            sql += Environment.NewLine + "     , UNICD"; // 보장기관기호
            sql += Environment.NewLine + "     , INSID"; // 증번호
            sql += Environment.NewLine + "     , JBFG"; // 의료급여종별구분
            sql += Environment.NewLine + "     , SKGONSGB"; //
            sql += Environment.NewLine + "     , SKPMGUM"; // 이전심결사항-본인부담환급금
            sql += Environment.NewLine + "     , SKPMGUM1"; // 이전심결사항-본인부담환급금1
            sql += Environment.NewLine + "     , SKPMGUM2"; // 이전심결사항-본인부담환급금2
            sql += Environment.NewLine + "     , SKHOSRETAMT"; // 이전심결사항-요양기관환수금 합계
            sql += Environment.NewLine + "     , SKUNAMT"; // 이전심결사항-보험자부담금
            sql += Environment.NewLine + "     , SKBHUNAMT"; // 이전심결사항-보훈청구액
            sql += Environment.NewLine + "     , SKRSTAMT"; // 이전심결사항-심사결정액
            sql += Environment.NewLine + "     , JSGONSGB"; //
            sql += Environment.NewLine + "     , JSPMGUM"; // 정산심결사항-본인부담환급금
            sql += Environment.NewLine + "     , JSPMGUM1"; // 정산심결사항-본인부담환급금1
            sql += Environment.NewLine + "     , JSPMGUM2"; // 정산심결사항-본인부담환급금2
            sql += Environment.NewLine + "     , JSHOSRETAMT"; // 정산심결사항-요양기관환수금 합계
            sql += Environment.NewLine + "     , JSUNAMT"; // 정산심결사항-보험자부담금 합계
            sql += Environment.NewLine + "     , JSBHUNAMT"; // 정산심결사항-보훈부담금
            sql += Environment.NewLine + "     , JSRSTAMT"; // 정산심결사항-심사결정액
            sql += Environment.NewLine + "     , JSRSTCHAAMT"; // 정산심결사항-결정차액
            sql += Environment.NewLine + "     , MEMO"; // 명일련비고사항
            sql += Environment.NewLine + "     , SKBHPMGUM";
            sql += Environment.NewLine + "     , JSBHPMGUM";
            sql += Environment.NewLine + "  FROM TIE_F0703";
            sql += Environment.NewLine + " WHERE JSDEMSEQ = '" + p_jsdemseq + "'";
            sql += Environment.NewLine + "   AND JSREDAY  = '" + p_jsreday + "'";
            sql += Environment.NewLine + "   AND CNECNO   = '" + p_cnecno + "'";
            sql += Environment.NewLine + "   AND DCOUNT   = '" + p_dcount + "'";
            sql += Environment.NewLine + "   AND JSYYSEQ  = '" + p_jsyyseq + "'";
            sql += Environment.NewLine + " ORDER BY JSSEQNO,EPRTNO";

            long no = 0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CDataPtnt data = new CDataPtnt();
                data.Clear();

                data.NO = (++no);
                data.JSDEMSEQ = reader["JSDEMSEQ"].ToString().TrimEnd(); // 정산심사차수
                data.JSREDAY = reader["JSREDAY"].ToString().TrimEnd(); // 정산통보일자
                data.CNECNO = reader["CNECNO"].ToString().TrimEnd(); // 접수번호
                data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd(); // 청구서 일련번호
                data.JSYYSEQ = reader["JSYYSEQ"].ToString().TrimEnd(); // 정산연번
                data.JSSEQNO = reader["JSSEQNO"].ToString().TrimEnd(); // 정산일련번호
                data.EPRTNO = reader["EPRTNO"].ToString().TrimEnd(); // 명세서 일련번호
                data.GUBUN = reader["GUBUN"].ToString().TrimEnd(); // 구분(1.환급 2.환수 3.환수+환급)
                data.PNM = reader["PNM"].ToString().TrimEnd(); // 수진자성명
                data.INSNM = reader["INSNM"].ToString().TrimEnd(); // 가입자 성명
                data.UNICD = reader["UNICD"].ToString().TrimEnd(); // 보장기관기호
                data.INSID = reader["INSID"].ToString().TrimEnd(); // 증번호
                data.JBFG = reader["JBFG"].ToString().TrimEnd(); // 의료급여종별구분
                data.SKGONSGB = reader["SKGONSGB"].ToString().TrimEnd(); //
                data.SKPMGUM = MetroLib.StrHelper.ToLong(reader["SKPMGUM"].ToString().TrimEnd()); // 이전심결사항-본인부담환급금
                data.SKPMGUM1 = MetroLib.StrHelper.ToLong(reader["SKPMGUM1"].ToString().TrimEnd()); // 이전심결사항-본인부담환급금1
                data.SKPMGUM2 = MetroLib.StrHelper.ToLong(reader["SKPMGUM2"].ToString().TrimEnd()); // 이전심결사항-본인부담환급금2
                data.SKHOSRETAMT = MetroLib.StrHelper.ToLong(reader["SKHOSRETAMT"].ToString().TrimEnd()); // 이전심결사항-요양기관환수금 합계
                data.SKUNAMT = MetroLib.StrHelper.ToLong(reader["SKUNAMT"].ToString().TrimEnd()); // 이전심결사항-보험자부담금
                data.SKBHUNAMT = MetroLib.StrHelper.ToLong(reader["SKBHUNAMT"].ToString().TrimEnd()); // 이전심결사항-보훈청구액
                data.SKRSTAMT = MetroLib.StrHelper.ToLong(reader["SKRSTAMT"].ToString().TrimEnd()); // 이전심결사항-심사결정액
                data.JSGONSGB = reader["JSGONSGB"].ToString().TrimEnd(); //
                data.JSPMGUM = MetroLib.StrHelper.ToLong(reader["JSPMGUM"].ToString().TrimEnd()); // 정산심결사항-본인부담환급금
                data.JSPMGUM1 = MetroLib.StrHelper.ToLong(reader["JSPMGUM1"].ToString().TrimEnd()); // 정산심결사항-본인부담환급금1
                data.JSPMGUM2 = MetroLib.StrHelper.ToLong(reader["JSPMGUM2"].ToString().TrimEnd()); // 정산심결사항-본인부담환급금2
                data.JSHOSRETAMT = MetroLib.StrHelper.ToLong(reader["JSHOSRETAMT"].ToString().TrimEnd()); // 정산심결사항-요양기관환수금 합계
                data.JSUNAMT = MetroLib.StrHelper.ToLong(reader["JSUNAMT"].ToString().TrimEnd()); // 정산심결사항-보험자부담금 합계
                data.JSBHUNAMT = MetroLib.StrHelper.ToLong(reader["JSBHUNAMT"].ToString().TrimEnd()); // 정산심결사항-보훈부담금
                data.JSRSTAMT = MetroLib.StrHelper.ToLong(reader["JSRSTAMT"].ToString().TrimEnd()); // 정산심결사항-심사결정액
                data.JSRSTCHAAMT = MetroLib.StrHelper.ToLong(reader["JSRSTCHAAMT"].ToString().TrimEnd()); // 정산심결사항-결정차액
                data.SKBHPMGUM = MetroLib.StrHelper.ToLong(reader["SKBHPMGUM"].ToString().TrimEnd());
                data.JSBHPMGUM = MetroLib.StrHelper.ToLong(reader["JSBHPMGUM"].ToString().TrimEnd());
                data.MEMO = reader["MEMO"].ToString().TrimEnd(); // 명일련비고사항

                list.Add(data);

                return true;
            });

            RefreshGridPtnt();
        }

        private void QueryCode(string p_jsdemseq, string p_jsreday, string p_cnecno, string p_dcount, string p_jsyyseq, OleDbConnection p_conn)
        {
            List<CDataCode> list = new List<CDataCode>();
            grdCode.DataSource = null;
            grdCode.DataSource = list;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT F0704.JSDEMSEQ"; // 정산심사차수
            sql += Environment.NewLine + "     , F0704.JSREDAY"; // 정산통보일자
            sql += Environment.NewLine + "     , F0704.CNECNO"; // 접수번호
            sql += Environment.NewLine + "     , F0704.DCOUNT"; // 청구서 일련번호
            sql += Environment.NewLine + "     , F0704.JSYYSEQ"; // 정산연번
            sql += Environment.NewLine + "     , F0704.JSSEQNO"; // 정산일련번호
            sql += Environment.NewLine + "     , F0704.EPRTNO"; // 명세서 일련번호
            sql += Environment.NewLine + "     , F0704.OUTCNT"; // 처방전교부번호
            sql += Environment.NewLine + "     , F0704.LNO"; // 줄번호
            sql += Environment.NewLine + "     , F0704.SKJJRMK"; // 이전심결사항-조정사유
            sql += Environment.NewLine + "     , F0704.SKJJRMK2";
            sql += Environment.NewLine + "     , F0704.SKBGIHO"; // 이전심결사항-코드
            sql += Environment.NewLine + "     , F0704.SKIJDCNT"; // 이전심결사항-1회투약량 인정횟수
            sql += Environment.NewLine + "     , F0704.SKIJDQTY"; // 이전심결사항-일투 인정횟수
            sql += Environment.NewLine + "     , F0704.SKIJDDAY"; // 이전심결사항-총투 인정횟수
            sql += Environment.NewLine + "     , F0704.SKJJAMT"; // 이전심결사항-조정금액
            sql += Environment.NewLine + "     , F0704.SKRSTAMT"; // 이전심결사항-심사결정액
            sql += Environment.NewLine + "     , F0704.JSJJRMK"; // 정산심결사항-조정사유
            sql += Environment.NewLine + "     , F0704.JSJJRMK2"; 
            sql += Environment.NewLine + "     , F0704.JSBGIHO"; // 정산심결사항-코드
            sql += Environment.NewLine + "     , F0704.JSIJDCNT"; // 정산심결사항-1회투약량 인정횟수
            sql += Environment.NewLine + "     , F0704.JSIJDQTY"; // 정산심결사항-일투 인정횟수
            sql += Environment.NewLine + "     , F0704.JSIJDDAY"; // 정산심결사항-총투 인정횟수
            sql += Environment.NewLine + "     , F0704.JSJJAMT"; // 정산심결사항-조정금액
            sql += Environment.NewLine + "     , F0704.JSRSTAMT"; // 정산심결사항-심사결정액
            sql += Environment.NewLine + "     , F0704.DRUGID"; // 조제기관 기호
            sql += Environment.NewLine + "     , F0704.DRUGNM"; // 조제기관 명
            sql += Environment.NewLine + "     , F0704.DRUGCNECNO"; // 조제기관 접수번호
            sql += Environment.NewLine + "     , F0704.DRUGCNECYY"; // 조제기관 접수년도
            sql += Environment.NewLine + "     , F0704.DRUGEPRTNO"; // 조제기관 명일련번호
            sql += Environment.NewLine + "     , F0704.MEMO"; // 명일련비고사항
            sql += Environment.NewLine + "     , F0703.PNM"; // 수진자명
            sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE = F0704.SKBGIHO) AS SKBGIHONM "; //
            sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE = F0704.JSBGIHO) AS JSBGIHONM "; //
            sql += Environment.NewLine + "  FROM TIE_F0704 F0704 INNER JOIN TIE_F0703 F0703 ON F0703.JSDEMSEQ = F0704.JSDEMSEQ ";
            sql += Environment.NewLine + "                                                 AND F0703.JSREDAY  = F0704.JSREDAY  ";
            sql += Environment.NewLine + "                                                 AND F0703.CNECNO   = F0704.CNECNO   ";
            sql += Environment.NewLine + "                                                 AND F0703.DCOUNT   = F0704.DCOUNT   ";
            sql += Environment.NewLine + "                                                 AND F0703.JSYYSEQ  = F0704.JSYYSEQ  ";
            sql += Environment.NewLine + "                                                 AND F0703.JSSEQNO  = F0704.JSSEQNO  ";
            sql += Environment.NewLine + "                                                 AND F0703.EPRTNO   = F0704.EPRTNO   ";
            sql += Environment.NewLine + " WHERE F0704.JSDEMSEQ = '" + p_jsdemseq + "' ";
            sql += Environment.NewLine + "   AND F0704.JSREDAY  = '" + p_jsreday + "' ";
            sql += Environment.NewLine + "   AND F0704.CNECNO   = '" + p_cnecno + "' ";
            sql += Environment.NewLine + "   AND F0704.DCOUNT   = '" + p_dcount + "' ";
            sql += Environment.NewLine + "   AND F0704.JSYYSEQ  = '" + p_jsyyseq + "' ";
            sql += Environment.NewLine + " ORDER BY F0704.JSDEMSEQ,F0704.JSREDAY,F0704.CNECNO,F0704.DCOUNT,F0704.JSYYSEQ,F0704.JSSEQNO,F0704.EPRTNO,F0704.OUTCNT,F0704.LNO ";

            long no = 0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CDataCode data = new CDataCode();
                data.Clear();

                data.NO = (++no);
                data.JSDEMSEQ = reader["JSDEMSEQ"].ToString(); // 정산심사차수
                data.JSREDAY = reader["JSREDAY"].ToString(); // 정산통보일자
                data.CNECNO = reader["CNECNO"].ToString(); // 접수번호
                data.DCOUNT = reader["DCOUNT"].ToString(); // 청구서 일련번호
                data.JSYYSEQ = reader["JSYYSEQ"].ToString(); // 정산연번
                data.JSSEQNO = reader["JSSEQNO"].ToString(); // 정산일련번호
                data.EPRTNO = reader["EPRTNO"].ToString().TrimEnd(); // 명세서 일련번호
                data.OUTCNT = reader["OUTCNT"].ToString().TrimEnd(); // 처방전교부번호
                data.LNO = reader["LNO"].ToString().TrimEnd(); // 줄번호
                data.SKJJRMK = reader["SKJJRMK"].ToString().TrimEnd() + reader["SKJJRMK2"].ToString().TrimEnd(); // 이전심결사항-조정사유
                data.SKBGIHO = reader["SKBGIHO"].ToString().TrimEnd(); // 이전심결사항-코드
                data.SKIJDCNT = MetroLib.StrHelper.ToDouble(reader["SKIJDCNT"].ToString().TrimEnd()); // 이전심결사항-1회투약량 인정횟수
                data.SKIJDQTY = MetroLib.StrHelper.ToLong(reader["SKIJDQTY"].ToString().TrimEnd()); // 이전심결사항-일투 인정횟수
                data.SKIJDDAY = MetroLib.StrHelper.ToLong(reader["SKIJDDAY"].ToString().TrimEnd()); // 이전심결사항-총투 인정횟수
                data.SKJJAMT = MetroLib.StrHelper.ToLong(reader["SKJJAMT"].ToString().TrimEnd()); // 이전심결사항-조정금액
                data.SKRSTAMT = MetroLib.StrHelper.ToLong(reader["SKRSTAMT"].ToString().TrimEnd()); // 이전심결사항-심사결정액
                data.JSJJRMK = reader["JSJJRMK"].ToString().TrimEnd() + reader["JSJJRMK2"].ToString().TrimEnd(); // 정산심결사항-조정사유
                data.JSBGIHO = reader["JSBGIHO"].ToString().TrimEnd(); // 정산심결사항-코드
                data.JSIJDCNT = MetroLib.StrHelper.ToDouble(reader["JSIJDCNT"].ToString().TrimEnd()); // 정산심결사항-1회투약량 인정횟수
                data.JSIJDQTY = MetroLib.StrHelper.ToLong(reader["JSIJDQTY"].ToString().TrimEnd()); // 정산심결사항-일투 인정횟수
                data.JSIJDDAY = MetroLib.StrHelper.ToLong(reader["JSIJDDAY"].ToString().TrimEnd()); // 정산심결사항-총투 인정횟수
                data.JSJJAMT = MetroLib.StrHelper.ToLong(reader["JSJJAMT"].ToString().TrimEnd()); // 정산심결사항-조정금액
                data.JSRSTAMT = MetroLib.StrHelper.ToLong(reader["JSRSTAMT"].ToString().TrimEnd()); // 정산심결사항-심사결정액
                data.DRUGID = reader["DRUGID"].ToString().TrimEnd(); // 조제기관 기호
                data.DRUGNM = reader["DRUGNM"].ToString().TrimEnd(); // 조제기관 명
                data.DRUGCNECNO = reader["DRUGCNECNO"].ToString(); // 조제기관 접수번호
                data.DRUGCNECYY = reader["DRUGCNECYY"].ToString(); // 조제기관 접수년도
                data.DRUGEPRTNO = reader["DRUGEPRTNO"].ToString(); // 조제기관 명일련번호
                data.MEMO = reader["MEMO"].ToString().TrimEnd(); // 명일련비고사항
                data.PNM = reader["PNM"].ToString().TrimEnd(); // 수진자명
                data.SKBGIHONM = reader["SKBGIHONM"].ToString().TrimEnd(); //
                data.JSBGIHONM = reader["JSBGIHONM"].ToString().TrimEnd(); //

                list.Add(data);

                return true;
            });

            RefreshGridCode();
        }

        private void QueryBonrt(string p_jsdemseq, string p_jsreday, string p_cnecno, string p_dcount, string p_jsyyseq, OleDbConnection p_conn)
        {
            List<CDataBonrt> list = new List<CDataBonrt>();
            grdBonrt.DataSource = null;
            grdBonrt.DataSource = list;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT F0705.JSDEMSEQ"; // 정산심사차수
            sql += Environment.NewLine + "     , F0705.JSREDAY"; // 정산통보일자
            sql += Environment.NewLine + "     , F0705.CNECNO"; // 접수번호
            sql += Environment.NewLine + "     , F0705.DCOUNT"; // 청구서 일련번호
            sql += Environment.NewLine + "     , F0705.JSYYSEQ"; // 정산연번
            sql += Environment.NewLine + "     , F0705.JSSEQNO"; // 정산일련번호
            sql += Environment.NewLine + "     , F0705.EPRTNO"; // 명세서 일련번호
            sql += Environment.NewLine + "     , F0705.OUTCNT"; // 처방전교부번호
            sql += Environment.NewLine + "     , F0705.LNO"; // 줄번호

            sql += Environment.NewLine + "     , F0705.SKJKRTBKRMK"; // 이전심결사항-본인부담률변경사유
            sql += Environment.NewLine + "     , F0705.SKBGIHO"; // 이전심결사항-코드
            sql += Environment.NewLine + "     , F0705.SKIJDCNT"; // 이전심결사항-1회투약량 인정횟수
            sql += Environment.NewLine + "     , F0705.SKIJDQTY"; // 이전심결사항-일투 인정횟수
            sql += Environment.NewLine + "     , F0705.SKIJDDAY"; // 이전심결사항-총투 인정횟수
            sql += Environment.NewLine + "     , F0705.SKJKRTBKAMT"; // 이전심결사항-본인부담률변경인정금액

            sql += Environment.NewLine + "     , F0705.JSJKRTBKRMK"; // 정산심결사항-본인부담률변경사유
            sql += Environment.NewLine + "     , F0705.JSBGIHO"; // 정산심결사항-코드
            sql += Environment.NewLine + "     , F0705.JSIJDCNT"; // 정산심결사항-1회투약량 인정횟수
            sql += Environment.NewLine + "     , F0705.JSIJDQTY"; // 정산심결사항-일투 인정횟수
            sql += Environment.NewLine + "     , F0705.JSIJDDAY"; // 정산심결사항-총투 인정횟수
            sql += Environment.NewLine + "     , F0705.JSJKRTBKAMT"; // 정산심결사항-본인부담률변경인정금액

            sql += Environment.NewLine + "     , F0705.DRUGID"; // 조제기관 기호
            sql += Environment.NewLine + "     , F0705.DRUGNM"; // 조제기관 명
            sql += Environment.NewLine + "     , F0705.DRUGCNECNO"; // 조제기관 접수번호
            sql += Environment.NewLine + "     , F0705.DRUGCNECYY"; // 조제기관 접수년도
            sql += Environment.NewLine + "     , F0705.DRUGEPRTNO"; // 조제기관 명일련번호
            sql += Environment.NewLine + "     , F0705.MEMO"; // 명일련비고사항
            sql += Environment.NewLine + "     , F0703.PNM"; // 수진자명
            sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE = F0705.SKBGIHO) AS SKBGIHONM";
            sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE = F0705.JSBGIHO) AS JSBGIHONM";
            sql += Environment.NewLine + "  FROM TIE_F0705 F0705 INNER JOIN TIE_F0703 F0703 ON F0703.JSDEMSEQ = F0705.JSDEMSEQ";
            sql += Environment.NewLine + "                                                 AND F0703.JSREDAY  = F0705.JSREDAY ";
            sql += Environment.NewLine + "                                                 AND F0703.CNECNO   = F0705.CNECNO  ";
            sql += Environment.NewLine + "                                                 AND F0703.DCOUNT   = F0705.DCOUNT  ";
            sql += Environment.NewLine + "                                                 AND F0703.JSYYSEQ  = F0705.JSYYSEQ ";
            sql += Environment.NewLine + "                                                 AND F0703.JSSEQNO  = F0705.JSSEQNO ";
            sql += Environment.NewLine + "                                                 AND F0703.EPRTNO   = F0705.EPRTNO  ";
            sql += Environment.NewLine + " WHERE F0705.JSDEMSEQ = '" + p_jsdemseq + "' ";
            sql += Environment.NewLine + "   AND F0705.JSREDAY  = '" + p_jsreday + "' ";
            sql += Environment.NewLine + "   AND F0705.CNECNO   = '" + p_cnecno + "' ";
            sql += Environment.NewLine + "   AND F0705.DCOUNT   = '" + p_dcount + "' ";
            sql += Environment.NewLine + "   AND F0705.JSYYSEQ  = '" + p_jsyyseq + "' ";
            sql += Environment.NewLine + " ORDER BY F0705.JSDEMSEQ,F0705.JSREDAY,F0705.CNECNO,F0705.DCOUNT,F0705.JSYYSEQ,F0705.JSSEQNO,F0705.EPRTNO,F0705.OUTCNT,F0705.LNO ";

            long no = 0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CDataBonrt data = new CDataBonrt();
                data.Clear();

                data.NO = (++no);

                data.JSDEMSEQ = reader["JSDEMSEQ"].ToString().TrimEnd(); // 정산심사차수
                data.JSREDAY = reader["JSREDAY"].ToString().TrimEnd(); // 정산통보일자
                data.CNECNO = reader["CNECNO"].ToString().TrimEnd(); // 접수번호
                data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd(); // 청구서 일련번호
                data.JSYYSEQ = reader["JSYYSEQ"].ToString().TrimEnd(); // 정산연번
                data.JSSEQNO = reader["JSSEQNO"].ToString().TrimEnd(); // 정산일련번호
                data.EPRTNO = reader["EPRTNO"].ToString().TrimEnd(); // 명세서 일련번호
                data.OUTCNT = reader["OUTCNT"].ToString().TrimEnd(); // 처방전교부번호
                data.LNO = reader["LNO"].ToString().TrimEnd(); // 줄번호
                data.SKJKRTBKRMK = reader["SKJKRTBKRMK"].ToString().TrimEnd(); // 이전심결사항-본인부담률변경사유
                data.SKBGIHO = reader["SKBGIHO"].ToString().TrimEnd(); // 이전심결사항-코드
                data.SKIJDCNT = reader["SKIJDCNT"].ToString().TrimEnd(); // 이전심결사항-1회투약량 인정횟수
                data.SKIJDQTY = reader["SKIJDQTY"].ToString().TrimEnd(); // 이전심결사항-일투 인정횟수
                data.SKIJDDAY = reader["SKIJDDAY"].ToString().TrimEnd(); // 이전심결사항-총투 인정횟수
                data.SKJKRTBKAMT = reader["SKJKRTBKAMT"].ToString().TrimEnd(); // 이전심결사항-본인부담률변경인정금액
                data.JSJKRTBKRMK = reader["JSJKRTBKRMK"].ToString().TrimEnd(); // 정산심결사항-본인부담률변경사유
                data.JSBGIHO = reader["JSBGIHO"].ToString().TrimEnd(); // 정산심결사항-코드
                data.JSIJDCNT = reader["JSIJDCNT"].ToString().TrimEnd(); // 정산심결사항-1회투약량 인정횟수
                data.JSIJDQTY = reader["JSIJDQTY"].ToString().TrimEnd(); // 정산심결사항-일투 인정횟수
                data.JSIJDDAY = reader["JSIJDDAY"].ToString().TrimEnd(); // 정산심결사항-총투 인정횟수
                data.JSJKRTBKAMT = reader["JSJKRTBKAMT"].ToString().TrimEnd(); // 정산심결사항-본인부담률변경인정금액
                data.DRUGID = reader["DRUGID"].ToString().TrimEnd(); // 조제기관 기호
                data.DRUGNM = reader["DRUGNM"].ToString().TrimEnd(); // 조제기관 명
                data.DRUGCNECNO = reader["DRUGCNECNO"].ToString().TrimEnd(); // 조제기관 접수번호
                data.DRUGCNECYY = reader["DRUGCNECYY"].ToString().TrimEnd(); // 조제기관 접수년도
                data.DRUGEPRTNO = reader["DRUGEPRTNO"].ToString().TrimEnd(); // 조제기관 명일련번호
                data.MEMO = reader["MEMO"].ToString().TrimEnd(); // 명일련비고사항
                data.PNM = reader["PNM"].ToString(); // 수진자명
                data.SKBGIHONM = reader["SKBGIHONM"].ToString().TrimEnd();
                data.JSBGIHONM = reader["JSBGIHONM"].ToString().TrimEnd();

                list.Add(data);

                return true;
            });

            RefreshGridBonrt();
        }

        private void grdCodeView_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (e.Column.FieldName == "SKGONSGB")
            {
                view.Columns["JSGONSGB"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKPMGUM")
            {
                view.Columns["JSPMGUM"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKPMGUM1")
            {
                view.Columns["JSPMGUM1"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKPMGUM2")
            {
                view.Columns["JSPMGUM2"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKHOSRETAMT")
            {
                view.Columns["JSHOSRETAMT"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKUNAMT")
            {
                view.Columns["JSUNAMT"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKBHUNAMT")
            {
                view.Columns["JSBHUNAMT"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKRSTAMT")
            {
                view.Columns["JSRSTAMT"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKBHPMGUM")
            {
                view.Columns["JSBHPMGUM"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKJJRMK")
            {
                view.Columns["JSJJRMK"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKBGIHO")
            {
                view.Columns["JSBGIHO"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKBGIHONM")
            {
                view.Columns["JSBGIHONM"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKIJDCNT")
            {
                view.Columns["JSIJDCNT"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKIJDQTY")
            {
                view.Columns["JSIJDQTY"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKIJDDAY")
            {
                view.Columns["JSIJDDAY"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKJJAMT")
            {
                view.Columns["JSJJAMT"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKRSTAMT")
            {
                view.Columns["JSRSTAMT"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKJKRTBKRMK")
            {
                view.Columns["JSJKRTBKRMK"].Width = e.Column.Width;
            }
            else if (e.Column.FieldName == "SKJKRTBKAMT")
            {
                view.Columns["JSJKRTBKAMT"].Width = e.Column.Width;
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
            if (tabControl1.SelectedIndex == 0)
            {
                printableComponentLink.Component = grdMain;
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                printableComponentLink.Component = grdPtnt;
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                printableComponentLink.Component = grdCode;
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                printableComponentLink.Component = grdBonrt;
            }
            printableComponentLink.ShowPreview();
        }

        private void printableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            String strTitle = "[원외]정산심사내역서";
            strTitle += "(" + tabControl1.SelectedTab.Text.ToString() + ")";

            // 가운데 정렬
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString(strTitle, Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);

            // 왼쪽정렬
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Near);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Near);
            // 조회조건
            String strCaption = "";
            if (tabControl1.SelectedIndex != 0)
            {
                strCaption += "정산심사차수 : " + txtHeadJsdemseq.Text.ToString();
                strCaption += ", 정산통보일자 : " + txtHeadJsreday.Text.ToString();
                strCaption += ", 심사차수 : " + txtHeadDemseq.Text.ToString();
                strCaption += ", 접수번호 : " + txtHeadCnecno.Text.ToString();
                strCaption += ", 청구번호 : " + txtHeadDemno.Text.ToString();
                strCaption += ", 건수 : " + txtHeadJscnt.Text.ToString();
                strCaption += ", 결정차액 : " + txtHeadJsamt.Text.ToString();
                if (txtHeadRedpt.Text.ToString().TrimEnd() != "")
                {
                    strCaption += Environment.NewLine;
                    strCaption += "심사담당 : " + txtHeadRedpt.Text.ToString();
                }
                if (txtHeadMemo.Text.ToString().TrimEnd() != "")
                {
                    strCaption += Environment.NewLine;
                    strCaption += "메모 : " + txtHeadMemo.Text.ToString();
                }

            }
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(strCaption, Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 가운데 정렬
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
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
            }

            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 프로그램 ID
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("ADD0722E", Color.Black, new RectangleF(0, 0, 120, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(sysDate + " " + sysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }
    }
}
