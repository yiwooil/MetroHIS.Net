using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0719E
{
    public partial class ADD0719E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private int m_PrevTabIndex = -1;
        private string m_ErrPos = "";

        public ADD0719E()
        {
            InitializeComponent();
        }

        public ADD0719E(String user, String pwd, String prjcd, String addpara)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
            m_Prjcd = prjcd;
            m_Addpara = addpara;
            m_HospMulti = GetHospmulti();

            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.Net").CreateSubKey("ADD0719E");
            chkBigofg.Checked = reg.GetValue(chkBigofg.Name, "False").ToString() == "True" ? true : false;
            this.SetShowPreview();
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

        private void ADD0719E_Load(object sender, EventArgs e)
        {
            cboDQOption.SelectedIndex = 0;
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
                sql += Environment.NewLine + "SELECT F0901.DEMSEQ"; // 심사차수
                sql += Environment.NewLine + "     , F0901.REDAY"; // 통보일자
                sql += Environment.NewLine + "     , F0901.CNECNO"; // 접수번호
                sql += Environment.NewLine + "     , F0901.DEMNO"; // 청구번호
                sql += Environment.NewLine + "     , F0901.JRFG"; // 보험자구분
                sql += Environment.NewLine + "     , F0902.JJCNT"; // 심사조정건수
                sql += Environment.NewLine + "     , F0902.JJAMT"; // 심사조정금액
                sql += Environment.NewLine + "     , F0902.HOSRETAMT"; // 요양기관환수금
                sql += Environment.NewLine + "     , F0902.PTRETAMT"; // 본인부담환급금
                sql += Environment.NewLine + "     , F0902.PMGUM1"; // 본인부담환급금1
                sql += Environment.NewLine + "     , F0902.PMGUM2"; // 본인부담환급금2
                sql += Environment.NewLine + "     , F0902.UNRETAMT"; // 보험자부담환급금
                sql += Environment.NewLine + "     , F0902.BHUNRETAMT"; // 보훈부담환수금
                sql += Environment.NewLine + "     , F0902.BHPMGUM"; // 보훈본인부담환급금
                sql += Environment.NewLine + "     , F0901.MEMO"; // 참조
                sql += Environment.NewLine + "     , F0901.REDPT1";
                sql += Environment.NewLine + "     , F0901.REDPT2";
                sql += Environment.NewLine + "     , F0901.REDPNM";
                sql += Environment.NewLine + "     , F0901.RETELE";
                sql += Environment.NewLine + "     , F0901.JIWONCD"; // 지원코드
                sql += Environment.NewLine + "     , F0901.HOSID"; // 요양기관기호
                sql += Environment.NewLine + "     , F0901.FMNO"; // 서식번호
                sql += Environment.NewLine + "     , F0901.GRPNO"; // 묶음번호
                sql += Environment.NewLine + "     , F0901.CNECYY"; // 접수년도
                sql += Environment.NewLine + "     , F0901.VERSION"; // 버전구분
                sql += Environment.NewLine + "     , F0901.DCOUNT"; // 청구서일련번호
                sql += Environment.NewLine + "  FROM TIE_F0901 F0901 INNER JOIN TIE_F0902 F0902 ON F0902.DEMSEQ=F0901.DEMSEQ";
                sql += Environment.NewLine + "                                                 AND F0902.REDAY =F0901.REDAY";
                sql += Environment.NewLine + "                                                 AND F0902.CNECNO=F0901.CNECNO";
                sql += Environment.NewLine + "                                                 AND F0902.DCOUNT=F0901.DCOUNT";
                sql += Environment.NewLine + " WHERE 1=1";

                if (txtFrdt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0901.REDAY>='" + txtFrdt.Text.ToString() + "'";
                }
                if (txtTodt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0901.REDAY<='" + txtTodt.Text.ToString() + "'";
                }
                if (txtCnecno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0901.CNECNO='" + txtCnecno.Text.ToString() + "'";
                }
                if (txtDemno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0901.DEMNO='" + txtDemno.Text.ToString() + "'";
                }

                sql += Environment.NewLine + " ORDER BY F0901.REDAY DESC";

                long no = 0;
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.Clear();

                    data.NO = (++no);

                    data.DEMSEQ = reader["DEMSEQ"].ToString().TrimEnd(); // 심사차수
                    data.REDAY = reader["REDAY"].ToString().TrimEnd(); // 통보일자
                    data.CNECNO = reader["CNECNO"].ToString().TrimEnd(); // 접수번호
                    data.DEMNO = reader["DEMNO"].ToString().TrimEnd(); // 청구번호
                    data.JRFG = reader["JRFG"].ToString().TrimEnd(); // 보험자구분
                    data.JJCNT = MetroLib.StrHelper.ToLong(reader["JJCNT"].ToString().TrimEnd()); // 심사조정건수
                    data.JJAMT = MetroLib.StrHelper.ToLong(reader["JJAMT"].ToString().TrimEnd()); // 심사조정금액
                    data.HOSRETAMT = MetroLib.StrHelper.ToLong(reader["HOSRETAMT"].ToString().TrimEnd()); // 요양기관환수금
                    data.PTRETAMT = MetroLib.StrHelper.ToLong(reader["PTRETAMT"].ToString().TrimEnd()); // 본인부담환급금
                    data.PMGUM1 = MetroLib.StrHelper.ToLong(reader["PMGUM1"].ToString().TrimEnd()); // 본인부담환급금1
                    data.PMGUM2 = MetroLib.StrHelper.ToLong(reader["PMGUM2"].ToString().TrimEnd()); // 본인부담환급금2
                    data.UNRETAMT = MetroLib.StrHelper.ToLong(reader["UNRETAMT"].ToString().TrimEnd()); // 보험자부담환급금
                    data.BHUNRETAMT = MetroLib.StrHelper.ToLong(reader["BHUNRETAMT"].ToString().TrimEnd()); // 보훈부담환수금
                    data.BHPMGUM = MetroLib.StrHelper.ToLong(reader["BHPMGUM"].ToString().TrimEnd()); // 보훈본인부담환급금
                    data.MEMO = reader["MEMO"].ToString().TrimEnd(); // 참조
                    data.REDPT1 = reader["REDPT1"].ToString().TrimEnd();
                    data.REDPT2 = reader["REDPT2"].ToString().TrimEnd();
                    data.REDPNM = reader["REDPNM"].ToString().TrimEnd();
                    data.RETELE = reader["RETELE"].ToString().TrimEnd();
                    data.JIWONCD = reader["JIWONCD"].ToString().TrimEnd(); // 지원코드
                    data.HOSID = reader["HOSID"].ToString().TrimEnd(); // 요양기관기호
                    data.FMNO = reader["FMNO"].ToString().TrimEnd(); // 서식번호
                    data.GRPNO = reader["GRPNO"].ToString().TrimEnd(); // 묶음번호
                    data.CNECYY = reader["CNECYY"].ToString().TrimEnd(); // 접수년도
                    data.VERSION = reader["VERSION"].ToString().TrimEnd(); // 버전구분
                    data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd(); // 청구서일련번호

                    list.Add(data);

                    return true;
                });
            }

            string[] check_field =
            {
                "PTRETAMT",
                "BHPMGUM", 
                "BHUNRETAMT",
            };
            DxLib.GridHelper.HideColumnIfZero(check_field, grdMainView);

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
            //
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
            //
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
            //
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panPrint.Visible = (tabControl1.SelectedIndex != 0);
            btnPrint.Enabled = (tabControl1.SelectedIndex == 0);

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

            string cnecno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "CNECNO").ToString();
            string reday = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "REDAY").ToString();
            string dcount = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DCOUNT").ToString();
            string jjcnt = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "JJCNT").ToString();
            string jjamt = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "JJAMT").ToString();

            string demseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DEMSEQ").ToString();
            string demno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DEMNO").ToString();
            string redpt = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "REDEPT").ToString();
            string memo = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "MEMO").ToString();

            txtHeadDemseq.Text = demseq;
            txtHeadCnecno.Text = cnecno;
            txtHeadReday.Text = reday;
            txtHeadDemno.Text = demno;
            txtHeadRedpt.Text = redpt;
            txtHeadMemo.Text = memo;
            txtHeadJjcnt.Text = jjcnt;
            txtHeadJjamt.Text = jjamt;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                m_ErrPos = "QueryPtnt";
                QueryPtnt(demseq, reday, cnecno, dcount, demno, conn);
                m_ErrPos = "QueryCode";
                QueryCode(demseq, reday, cnecno, dcount, demno, conn);
                m_ErrPos = "QueryBonrt";
                QueryBonrt(demseq, reday, cnecno, dcount, demno, conn);
            }

            RefreshGridMain();
        }

        private void QueryPtnt(string p_demseq, string p_reday, string p_cnecno, string p_dcount, string p_demno, OleDbConnection p_conn)
        {
            List<CDataPtnt> list = new List<CDataPtnt>();
            grdPtnt.DataSource = null;
            grdPtnt.DataSource = list;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT F0903.EPRTNO"; // 명일련
            sql += Environment.NewLine + "     , F0903.PNM"; // 수진자성명
            sql += Environment.NewLine + "     , F0903.UNICD"; // 사업장(보장기관)기호
            sql += Environment.NewLine + "     , F0903.INSID"; // 증번호(보장시설기호)
            sql += Environment.NewLine + "     , F0903.JBFG"; // 의료급여종별구분
            sql += Environment.NewLine + "     , F0903.GONSGB"; // 공상등구분
            sql += Environment.NewLine + "     , F0903.REDPT"; // 심사담당조
            sql += Environment.NewLine + "     , F0903.HOSRETAMT"; // 요양기관환수금
            sql += Environment.NewLine + "     , F0903.PTRETAMT"; // 본인부담환급금
            sql += Environment.NewLine + "     , F0903.PMGUM1"; // 본인부담환급금1
            sql += Environment.NewLine + "     , F0903.PMGUM2"; // 본인부담환급금2
            sql += Environment.NewLine + "     , F0903.UNRETAMT"; // 보험자(보장기관)부담환수금
            sql += Environment.NewLine + "     , F0903.BHUNRETAMT"; // 보훈부담환수금
            sql += Environment.NewLine + "     , F0903.BHPMGUM"; // 보훈본인부담환급금
            sql += Environment.NewLine + "     , F0903.MEMO"; // 명일련비고사항
            sql += Environment.NewLine + "     , F0903.DEMSEQ"; // 심사차수
            sql += Environment.NewLine + "     , F0903.REDAY"; // 통보일자
            sql += Environment.NewLine + "     , F0903.CNECNO"; // 접수번호
            sql += Environment.NewLine + "     , F0903.DCOUNT"; // 청구서일련번호
            sql += Environment.NewLine + "     , F0901.DEMNO";
            sql += Environment.NewLine + "  FROM TIE_F0903 F0903 INNER JOIN TIE_F0901 F0901 ON F0901.DEMSEQ=F0903.DEMSEQ";
            sql += Environment.NewLine + "                                                 AND F0901.REDAY =F0903.REDAY";
            sql += Environment.NewLine + "                                                 AND F0901.CNECNO=F0903.CNECNO";
            sql += Environment.NewLine + "                                                 AND F0901.DCOUNT=F0903.DCOUNT";


            if (cboDQOption.SelectedIndex == 1)
            {
                // 청구번호별
                sql += Environment.NewLine + " WHERE F0901.DEMNO = '" + p_demno + "' ";
            }
            else if (cboDQOption.SelectedIndex == 2)
            {
                // 청구월별
                sql += Environment.NewLine + " WHERE F0901.DEMNO LIKE '" + p_demno.Substring(0, 6) + "%' ";
            }
            else if (cboDQOption.SelectedIndex == 3)
            {
                // 청구년도별
                sql += Environment.NewLine + " WHERE F0901.DEMNO LIKE '" + p_demno.Substring(0, 4) + "%' ";
            }
            else
            {
                sql += Environment.NewLine + " WHERE F0903.DEMSEQ = '" + p_demseq + "'";
                sql += Environment.NewLine + "   AND F0903.REDAY  = '" + p_reday + "'";
                sql += Environment.NewLine + "   AND F0903.CNECNO = '" + p_cnecno + "'";
                sql += Environment.NewLine + "   AND F0903.DCOUNT = '" + p_dcount + "'";
            }
            sql += Environment.NewLine + " ORDER BY F0903.DEMSEQ,F0903.REDAY,F0903.CNECNO,F0903.DCOUNT,F0903.EPRTNO";

            long no = 0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CDataPtnt data = new CDataPtnt();
                data.Clear();

                data.NO = (++no);

                data.EPRTNO = reader["EPRTNO"].ToString().TrimEnd(); // 명일련
                data.PNM = reader["PNM"].ToString().TrimEnd(); // 수진자성명
                data.UNICD = reader["UNICD"].ToString().TrimEnd(); // 사업장(보장기관)기호
                data.INSID = reader["INSID"].ToString().TrimEnd(); // 증번호(보장시설기호)
                data.JBFG = reader["JBFG"].ToString().TrimEnd(); // 의료급여종별구분
                data.GONSGB = reader["GONSGB"].ToString().TrimEnd(); // 공상등구분
                data.REDPT = reader["REDPT"].ToString().TrimEnd(); // 심사담당조
                data.HOSRETAMT = MetroLib.StrHelper.ToLong(reader["HOSRETAMT"].ToString().TrimEnd()); // 요양기관환수금
                data.PTRETAMT = MetroLib.StrHelper.ToLong(reader["PTRETAMT"].ToString().TrimEnd()); // 본인부담환급금
                data.PMGUM1 = MetroLib.StrHelper.ToLong(reader["PMGUM1"].ToString().TrimEnd()); // 본인부담환급금1
                data.PMGUM2 = MetroLib.StrHelper.ToLong(reader["PMGUM2"].ToString().TrimEnd()); // 본인부담환급금2
                data.UNRETAMT = MetroLib.StrHelper.ToLong(reader["UNRETAMT"].ToString().TrimEnd()); // 보험자(보장기관)부담환수금
                data.BHUNRETAMT = MetroLib.StrHelper.ToLong(reader["BHUNRETAMT"].ToString().TrimEnd()); // 보훈부담환수금
                data.BHPMGUM = MetroLib.StrHelper.ToLong(reader["BHPMGUM"].ToString().TrimEnd()); // 보훈본인부담환급금
                data.MEMO = reader["MEMO"].ToString().TrimEnd(); // 명일련비고사항
                data.DEMSEQ = reader["DEMSEQ"].ToString().TrimEnd(); // 심사차수
                data.REDAY = reader["REDAY"].ToString().TrimEnd(); // 통보일자
                data.CNECNO = reader["CNECNO"].ToString().TrimEnd(); // 접수번호
                data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd(); // 청구서일련번호
                data.DEMNO = reader["DEMNO"].ToString().TrimEnd();

                list.Add(data);

                return true;
            });

            string[] check_field =
            {
                "PTRETAMT", 
                "BHPMGUM", 
                "BHUNRETAMT",
            };
            DxLib.GridHelper.HideColumnIfZero(check_field, grdPtntView);
        }

        private void QueryCode(string p_demseq, string p_reday, string p_cnecno, string p_dcount, string p_demno, OleDbConnection p_conn)
        {
            List<CDataCode> list = new List<CDataCode>();
            grdCode.DataSource = null;
            grdCode.DataSource = list;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT F0904.OUTCNT"; // 처방전교부번호
            sql += Environment.NewLine + "     , F0904.LNO"; // 줄번호
            sql += Environment.NewLine + "     , F0904.JJRMK"; // 조정사유
            sql += Environment.NewLine + "     , F0904.JJRMK2"; // 조정사유
            sql += Environment.NewLine + "     , F0904.IJDCNT"; // 1회투약량
            sql += Environment.NewLine + "     , F0904.IJDQTY"; // 일투
            sql += Environment.NewLine + "     , F0904.IJDDAY"; // 총투
            sql += Environment.NewLine + "     , F0904.JJAMT"; // 조정금액
            sql += Environment.NewLine + "     , F0904.BGIHO"; // 조정의약품코드
            sql += Environment.NewLine + "     , F0904.DRUGID"; // 조제기관기호
            sql += Environment.NewLine + "     , F0904.DRUGNM"; // 조제기관명
            sql += Environment.NewLine + "     , F0904.DRUGCNECNO"; // 조제기관접수번호
            sql += Environment.NewLine + "     , F0904.DRUGCNECYY"; // 조제기관접수년도
            sql += Environment.NewLine + "     , F0904.DRUGEPRTNO"; // 조제기관명일련번호
            sql += Environment.NewLine + "     , F0904.MEMO"; // 비고사항
            sql += Environment.NewLine + "     , F0904.DEMSEQ"; // 심사차수
            sql += Environment.NewLine + "     , F0904.REDAY"; // 통보일자
            sql += Environment.NewLine + "     , F0904.CNECNO"; // 접수번호
            sql += Environment.NewLine + "     , F0904.DCOUNT"; // 청구서일련번호
            sql += Environment.NewLine + "     , F0904.EPRTNO"; // 명세서일련번호
            sql += Environment.NewLine + "     , F0903.PNM"; // 환자명
            sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE=F0904.BGIHO) AS BGIHONM"; // 약품명칭
            sql += Environment.NewLine + "     , F0901.DEMNO";
            sql += Environment.NewLine + "  FROM TIE_F0904 F0904 INNER JOIN TIE_F0903 F0903 ON  F0903.DEMSEQ=F0904.DEMSEQ";
            sql += Environment.NewLine + "                                                  AND F0903.REDAY =F0904.REDAY";
            sql += Environment.NewLine + "                                                  AND F0903.CNECNO=F0904.CNECNO";
            sql += Environment.NewLine + "                                                  AND F0903.DCOUNT=F0904.DCOUNT";
            sql += Environment.NewLine + "                                                  AND F0903.EPRTNO=F0904.EPRTNO";
            sql += Environment.NewLine + "                       INNER JOIN TIE_F0901 F0901 ON  F0901.DEMSEQ=F0904.DEMSEQ";
            sql += Environment.NewLine + "                                                  AND F0901.REDAY =F0904.REDAY";
            sql += Environment.NewLine + "                                                  AND F0901.CNECNO=F0904.CNECNO";
            sql += Environment.NewLine + "                                                  AND F0901.DCOUNT=F0904.DCOUNT";


            if (cboDQOption.SelectedIndex == 1)
            {
                // 청구번호별
                sql += Environment.NewLine + " WHERE F0901.DEMNO = '" + p_demno + "' ";
            }
            else if (cboDQOption.SelectedIndex == 2)
            {
                // 청구월별
                sql += Environment.NewLine + " WHERE F0901.DEMNO LIKE '" + p_demno.Substring(0, 6) + "%' ";
            }
            else if (cboDQOption.SelectedIndex == 3)
            {
                // 청구년도별
                sql += Environment.NewLine + " WHERE F0901.DEMNO LIKE '" + p_demno.Substring(0, 4) + "%' ";
            }
            else
            {
                sql += Environment.NewLine + " WHERE F0904.DEMSEQ = '" + p_demseq + "'";
                sql += Environment.NewLine + "   AND F0904.REDAY  = '" + p_reday + "'";
                sql += Environment.NewLine + "   AND F0904.CNECNO = '" + p_cnecno + "'";
                sql += Environment.NewLine + "   AND F0904.DCOUNT = '" + p_dcount + "'";
            }
            sql += Environment.NewLine + " ORDER BY F0904.DEMSEQ,F0904.REDAY,F0904.CNECNO,F0904.DCOUNT,F0904.EPRTNO,F0904.OUTCNT,F0904.LNO";

            long no = 0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CDataCode data = new CDataCode();
                data.Clear();

                data.NO = (++no);

                data.OUTCNT = reader["OUTCNT"].ToString().TrimEnd(); // 처방전교부번호
                data.LNO = reader["LNO"].ToString().TrimEnd(); // 줄번호
                data.JJRMK = reader["JJRMK"].ToString().TrimEnd(); // 조정사유
                data.JJRMK2 = reader["JJRMK2"].ToString().TrimEnd(); // 조정사유
                data.IJDCNT = MetroLib.StrHelper.ToDouble(reader["IJDCNT"].ToString().TrimEnd()); // 1회투약량
                data.IJDQTY = MetroLib.StrHelper.ToDouble(reader["IJDQTY"].ToString().TrimEnd()); // 일투
                data.IJDDAY = MetroLib.StrHelper.ToLong(reader["IJDDAY"].ToString().TrimEnd()); // 총투
                data.JJAMT = MetroLib.StrHelper.ToLong(reader["JJAMT"].ToString().TrimEnd()); // 조정금액
                data.BGIHO = reader["BGIHO"].ToString().TrimEnd(); // 조정의약품코드
                data.DRUGID = reader["DRUGID"].ToString().TrimEnd(); // 조제기관기호
                data.DRUGNM = reader["DRUGNM"].ToString().TrimEnd(); // 조제기관명
                data.DRUGCNECNO = reader["DRUGCNECNO"].ToString().TrimEnd(); // 조제기관접수번호
                data.DRUGCNECYY = reader["DRUGCNECYY"].ToString().TrimEnd(); // 조제기관접수년도
                data.DRUGEPRTNO = reader["DRUGEPRTNO"].ToString().TrimEnd(); // 조제기관명일련번호
                data.MEMO = reader["MEMO"].ToString().TrimEnd(); // 비고사항
                data.DEMSEQ = reader["DEMSEQ"].ToString().TrimEnd(); // 심사차수
                data.REDAY = reader["REDAY"].ToString().TrimEnd(); // 통보일자
                data.CNECNO = reader["CNECNO"].ToString().TrimEnd(); // 접수번호
                data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd(); // 청구서일련번호
                data.EPRTNO = reader["EPRTNO"].ToString().TrimEnd(); // 명세서일련번호
                data.PNM = reader["PNM"].ToString().TrimEnd(); // 환자명
                data.BGIHONM = reader["BGIHONM"].ToString().TrimEnd(); // 약품명칭
                data.DEMNO = reader["DEMNO"].ToString().TrimEnd();

                list.Add(data);

                return true;
            });
        }

        private void QueryBonrt(string p_demseq, string p_reday, string p_cnecno, string p_dcount, string p_demno, OleDbConnection p_conn)
        {
            List<CDataBonrt> list = new List<CDataBonrt>();
            grdBonrt.DataSource = null;
            grdBonrt.DataSource = list;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT F0905.OUTCNT"; // 처방전교부번호
            sql += Environment.NewLine + "     , F0905.LNO"; //줄번호
            sql += Environment.NewLine + "     , F0905.JKRTBKRMK"; //본인부담률 변경사유
            sql += Environment.NewLine + "     , F0905.IJDBKCNT"; //1회투약량
            sql += Environment.NewLine + "     , F0905.IJDBKQTY"; //일투
            sql += Environment.NewLine + "     , F0905.IJDBKDAY"; //총투
            sql += Environment.NewLine + "     , F0905.IJDJKRTBKAMT"; //본인부담률 변경인정금액
            sql += Environment.NewLine + "     , F0905.BGIHO"; //본인부담률 의약품코드
            sql += Environment.NewLine + "     , F0905.DRUGID"; //조제기관기호
            sql += Environment.NewLine + "     , F0905.DRUGNM"; //조제기관명
            sql += Environment.NewLine + "     , F0905.DRUGCNECNO"; //조제기관접수번호
            sql += Environment.NewLine + "     , F0905.DRUGCNECYY"; //조제기관접수년도
            sql += Environment.NewLine + "     , F0905.DRUGEPRTNO"; //조제기관명일련번호
            sql += Environment.NewLine + "     , F0905.MEMO"; //비고사항
            sql += Environment.NewLine + "     , F0905.DEMSEQ"; //심사차수
            sql += Environment.NewLine + "     , F0905.REDAY"; //통보일자
            sql += Environment.NewLine + "     , F0905.CNECNO"; //접수번호
            sql += Environment.NewLine + "     , F0905.DCOUNT"; //청구서일련번호
            sql += Environment.NewLine + "     , F0905.EPRTNO"; //명세서일련번호
            sql += Environment.NewLine + "     , F0903.PNM"; //환자명
            sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE = F0905.BGIHO) AS BGIHONM"; //약품명칭
            sql += Environment.NewLine + "     , F0901.DEMNO";
            sql += Environment.NewLine + "  FROM TIE_F0905 F0905 INNER JOIN TIE_F0903 F0903 ON  F0903.DEMSEQ=F0905.DEMSEQ";
            sql += Environment.NewLine + "                                                  AND F0903.REDAY =F0905.REDAY ";
            sql += Environment.NewLine + "                                                  AND F0903.CNECNO=F0905.CNECNO";
            sql += Environment.NewLine + "                                                  AND F0903.DCOUNT=F0905.DCOUNT";
            sql += Environment.NewLine + "                                                  AND F0903.EPRTNO=F0905.EPRTNO";
            sql += Environment.NewLine + "                       INNER JOIN TIE_F0901 F0901 ON  F0901.DEMSEQ=F0905.DEMSEQ";
            sql += Environment.NewLine + "                                                  AND F0901.REDAY =F0905.REDAY ";
            sql += Environment.NewLine + "                                                  AND F0901.CNECNO=F0905.CNECNO";
            sql += Environment.NewLine + "                                                  AND F0901.DCOUNT=F0905.DCOUNT";


            if (cboDQOption.SelectedIndex == 1)
            {
                // 청구번호별
                sql += Environment.NewLine + " WHERE F0901.DEMNO = '" + p_demno + "' ";
            }
            else if (cboDQOption.SelectedIndex == 2)
            {
                // 청구월별
                sql += Environment.NewLine + " WHERE F0901.DEMNO LIKE '" + p_demno.Substring(0, 6) + "%' ";
            }
            else if (cboDQOption.SelectedIndex == 3)
            {
                // 청구년도별
                sql += Environment.NewLine + " WHERE F0901.DEMNO LIKE '" + p_demno.Substring(0, 4) + "%' ";
            }
            else
            {
                sql += Environment.NewLine + " WHERE F0905.DEMSEQ = '" + p_demseq + "'";
                sql += Environment.NewLine + "   AND F0905.REDAY  = '" + p_reday + "'";
                sql += Environment.NewLine + "   AND F0905.CNECNO = '" + p_cnecno + "'";
                sql += Environment.NewLine + "   AND F0905.DCOUNT = '" + p_dcount + "'";
            }
            sql += Environment.NewLine + " ORDER BY F0905.DEMSEQ,F0905.REDAY,F0905.CNECNO,F0905.DCOUNT,F0905.EPRTNO,F0905.OUTCNT,F0905.LNO";

            long no = 0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CDataBonrt data = new CDataBonrt();
                data.Clear();

                data.NO = (++no);

                data.OUTCNT = reader["OUTCNT"].ToString().TrimEnd(); // 처방전교부번호
                data.LNO = reader["LNO"].ToString().TrimEnd(); //줄번호
                data.JKRTBKRMK = reader["JKRTBKRMK"].ToString().TrimEnd(); //본인부담률 변경사유
                data.IJDBKCNT = MetroLib.StrHelper.ToDouble(reader["IJDBKCNT"].ToString().TrimEnd()); //1회투약량
                data.IJDBKQTY = MetroLib.StrHelper.ToDouble(reader["IJDBKQTY"].ToString().TrimEnd()); //일투
                data.IJDBKDAY = MetroLib.StrHelper.ToLong(reader["IJDBKDAY"].ToString().TrimEnd()); //총투
                data.IJDJKRTBKAMT = MetroLib.StrHelper.ToLong(reader["IJDJKRTBKAMT"].ToString().TrimEnd()); //본인부담률 변경인정금액
                data.BGIHO = reader["BGIHO"].ToString().TrimEnd(); //본인부담률 의약품코드
                data.DRUGID = reader["DRUGID"].ToString().TrimEnd(); //조제기관기호
                data.DRUGNM = reader["DRUGNM"].ToString().TrimEnd(); //조제기관명
                data.DRUGCNECNO = reader["DRUGCNECNO"].ToString().TrimEnd(); //조제기관접수번호
                data.DRUGCNECYY = reader["DRUGCNECYY"].ToString().TrimEnd(); //조제기관접수년도
                data.DRUGEPRTNO = reader["DRUGEPRTNO"].ToString().TrimEnd(); //조제기관명일련번호
                data.MEMO = reader["MEMO"].ToString().TrimEnd(); //비고사항
                data.DEMSEQ = reader["DEMSEQ"].ToString().TrimEnd(); //심사차수
                data.REDAY = reader["REDAY"].ToString().TrimEnd(); //통보일자
                data.CNECNO = reader["CNECNO"].ToString().TrimEnd(); //접수번호
                data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd(); //청구서일련번호
                data.EPRTNO = reader["EPRTNO"].ToString().TrimEnd(); //명세서일련번호
                data.PNM = reader["PNM"].ToString().TrimEnd(); //환자명
                data.BGIHONM = reader["BGIHONM"].ToString().TrimEnd(); //약품명칭
                data.DEMNO = reader["DEMNO"].ToString().TrimEnd();

                list.Add(data);

                return true;
            });
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Print(0);
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

        private int m_PrintIndex = 0;

        private void Print(int index)
        {
            m_PrintIndex = index;

            DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink = new DevExpress.XtraPrinting.PrintableComponentLink();
            printableComponentLink.CreateMarginalHeaderArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(printableComponentLink_CreateReportHeaderArea);
            printableComponentLink.CreateMarginalFooterArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(printableComponentLink_CreateReportFooterArea);
            printableComponentLink.Margins = new System.Drawing.Printing.Margins(10, 10, 100, 50);
            printableComponentLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
            printableComponentLink.Landscape = true;
            printingSystem.Links.Add(printableComponentLink);
            if (m_PrintIndex == 0)
            {
                printableComponentLink.Component = grdMain;
            }
            else if (m_PrintIndex == 1)
            {
                if (txtHeadMemo.Text.ToString() != "") printableComponentLink.Margins = new System.Drawing.Printing.Margins(10, 10, 150, 50);
                printableComponentLink.Component = grdPtnt;
            }
            else if (m_PrintIndex == 2)
            {
                if (txtHeadMemo.Text.ToString() != "") printableComponentLink.Margins = new System.Drawing.Printing.Margins(10, 10, 150, 50);
                printableComponentLink.Component = grdCode;
            }
            else if (m_PrintIndex == 3)
            {
                if (txtHeadMemo.Text.ToString() != "") printableComponentLink.Margins = new System.Drawing.Printing.Margins(10, 10, 150, 50);
                printableComponentLink.Component = grdBonrt;
            }

            printableComponentLink.ShowPreview();
        }

        private void printableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            String strTitle = "[원외]심사결과통보서";
            if (m_PrintIndex == 0)
            {
                strTitle += "(리스트)";
            }
            else if (m_PrintIndex == 1)
            {
                strTitle += "(명단)";
            }
            else if (m_PrintIndex == 2)
            {
                strTitle += "(코드)";
            }
            else if (m_PrintIndex == 3)
            {
                strTitle += "(본인부담율)";
            }

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
            if (m_PrintIndex != 0)
            {
                strCaption += "심사차수 : " + txtHeadDemseq.Text.ToString();
                strCaption += ", 통보일자 : " + txtHeadReday.Text.ToString();
                strCaption += ", 접수번호 : " + txtHeadCnecno.Text.ToString();
                strCaption += ", 청구번호 : " + txtHeadDemno.Text.ToString();
                strCaption += ", 조정건수 : " + txtHeadJjcnt.Text.ToString();
                strCaption += ", 조정금액 : " + txtHeadJjamt.Text.ToString();
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

                e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
                e.Graph.DrawString(strCaption, Color.Black, new RectangleF(0, 40, 1080, 100), DevExpress.XtraPrinting.BorderSide.None);
            }
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
            e.Graph.DrawString("ADD0719E", Color.Black, new RectangleF(0, 0, 120, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(sysDate + " " + sysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
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

        private void chkBigofg_CheckedChanged(object sender, EventArgs e)
        {
            this.ShowProgressForm("", (sender as CheckBox).Text.ToString() + " 처리 중입니다."); 
            
            Microsoft.Win32.RegistryKey reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.Net").CreateSubKey("ADD0719E");
            reg.SetValue(chkBigofg.Name, chkBigofg.Checked == true ? "True" : "False");
            this.SetShowPreview();
       
            this.CloseProgressForm("", (sender as CheckBox).Text.ToString() + " 처리 중입니다.");
        }

        private void SetShowPreview()
        {
            grdMainView.OptionsView.ShowPreview = chkBigofg.Checked;
            grdMainView.OptionsPrint.PrintPreview = chkBigofg.Checked;
            grdPtntView.OptionsView.ShowPreview = chkBigofg.Checked;
            grdPtntView.OptionsPrint.PrintPreview = chkBigofg.Checked;
            grdCodeView.OptionsView.ShowPreview = chkBigofg.Checked;
            grdCodeView.OptionsPrint.PrintPreview = chkBigofg.Checked;
            grdBonrtView.OptionsView.ShowPreview = chkBigofg.Checked;
            grdBonrtView.OptionsPrint.PrintPreview = chkBigofg.Checked;
        }

        private void btnPrintPtnt_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Print(1);
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

        private void btnPrintCode_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Print(2);
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

        private void btnPrintBonrt_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.ShowProgressForm("", (sender as Button).Text.ToString() + " 중입니다.");
                this.Print(3);
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

    }
}
