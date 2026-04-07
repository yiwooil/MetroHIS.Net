using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0723E
{
    public partial class ADD0723E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private int m_PrevTabIndex = -1;
        private string m_ErrPos = "";

        public ADD0723E()
        {
            InitializeComponent();
        }

        public ADD0723E(String user, String pwd, String prjcd, String addpara)
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

        private void ADD0723E_Load(object sender, EventArgs e)
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
                sql += Environment.NewLine + "SELECT F1.VERSION"; // 버전구분
                sql += Environment.NewLine + "     , F1.JSDEMSEQ"; // 정산심사차수
                sql += Environment.NewLine + "     , F1.JSREDAY"; // 정산통보일자
                sql += Environment.NewLine + "     , F1.CNECNO"; // 접수번호
                sql += Environment.NewLine + "     , F1.DCOUNT"; // 청구서 일련번호
                sql += Environment.NewLine + "     , F1.FMNO"; // 서식번호
                sql += Environment.NewLine + "     , F1.HOSID"; // 요양기관 기호
                sql += Environment.NewLine + "     , F1.JIWONCD"; // 지원코드
                sql += Environment.NewLine + "     , F1.DEMSEQ"; // 심사차수
                sql += Environment.NewLine + "     , F1.DEMNO"; // 청구번호
                sql += Environment.NewLine + "     , F1.GRPNO"; // 묶음번호
                sql += Environment.NewLine + "     , F1.CNECYY"; // 접수년도(CCYY)
                sql += Environment.NewLine + "     , F1.DEMUNITFG"; // 청구단위구분
                sql += Environment.NewLine + "     , F1.JRFG"; // 보험자종별구분
                sql += Environment.NewLine + "     , F2.JSYYSEQ"; // 정산연번
                sql += Environment.NewLine + "     , F2.JSREDPT1";
                sql += Environment.NewLine + "     , F2.JSREDPT2";
                sql += Environment.NewLine + "     , F2.JSREDPNM";
                sql += Environment.NewLine + "     , F2.JSREDPNO";
                sql += Environment.NewLine + "     , F2.JSRETELE"; // 정산담당
                sql += Environment.NewLine + "     , F2.JSBUSSCD"; // 정산업무코드
                sql += Environment.NewLine + "     , F2.JSBUSSNM"; // 정산업무명
                sql += Environment.NewLine + "     , F2.PMGUM"; // 본인부담환급금 합계
                sql += Environment.NewLine + "     , F2.PMGUM1"; // 본인부담환급금1 합계
                sql += Environment.NewLine + "     , F2.PMGUM2"; // 본인부담환급금2 합계
                sql += Environment.NewLine + "     , F2.BHPMGUM"; // 보훈 본인부담환급금 합계
                sql += Environment.NewLine + "     , F2.PPGUM"; // 환수금액 합계
                sql += Environment.NewLine + "     , F2.HOSRETAMT"; // 요양기관환수금 합계
                sql += Environment.NewLine + "     , F2.UNAMT"; // 보험자부담금 합계
                sql += Environment.NewLine + "     , F2.BHUNAMT"; // 보훈부담금 합계
                sql += Environment.NewLine + "     , F2.RSTAMT"; // 심사결정액 합계
                sql += Environment.NewLine + "     , F2.RSTCNT"; // 건수합계
                sql += Environment.NewLine + "     , F2.MEMO"; // 참조란
                sql += Environment.NewLine + "  FROM TIE_F0801 F1 INNER JOIN TIE_F0802 F2 ON F2.JSDEMSEQ=F1.JSDEMSEQ";
                sql += Environment.NewLine + "                                           AND F2.JSREDAY =F1.JSREDAY";
                sql += Environment.NewLine + "                                           AND F2.CNECNO  =F1.CNECNO";
                sql += Environment.NewLine + "                                           AND F2.DCOUNT  =F1.DCOUNT";
                sql += Environment.NewLine + "                    LEFT  JOIN TIE_H010 H010 ON H010.DEMNO=F1.DEMNO";
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
                sql += Environment.NewLine + " ORDER BY F1.JSREDAY DESC";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.Clear();

                    data.VERSION = reader["VERSION"].ToString().TrimEnd(); // 버전구분
                    data.JSDEMSEQ = reader["JSDEMSEQ"].ToString().TrimEnd(); // 정산심사차수
                    data.JSREDAY = reader["JSREDAY"].ToString().TrimEnd(); // 정산통보일자
                    data.CNECNO = reader["CNECNO"].ToString().TrimEnd(); // 접수번호
                    data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd(); // 청구서 일련번호
                    data.FMNO = reader["FMNO"].ToString().TrimEnd(); // 서식번호
                    data.HOSID = reader["HOSID"].ToString().TrimEnd(); // 요양기관 기호
                    data.JIWONCD = reader["JIWONCD"].ToString().TrimEnd(); // 지원코드
                    data.DEMSEQ = reader["DEMSEQ"].ToString().TrimEnd(); // 심사차수
                    data.DEMNO = reader["DEMNO"].ToString().TrimEnd(); // 청구번호
                    data.GRPNO = reader["GRPNO"].ToString().TrimEnd(); // 묶음번호
                    data.CNECYY = reader["CNECYY"].ToString().TrimEnd(); // 접수년도(CCYY)
                    data.DEMUNITFG = reader["DEMUNITFG"].ToString().TrimEnd(); // 청구단위구분
                    data.JRFG = reader["JRFG"].ToString().TrimEnd(); // 보험자종별구분
                    data.JSYYSEQ = reader["JSYYSEQ"].ToString().TrimEnd(); // 정산연번
                    data.JSREDPT1 = reader["JSREDPT1"].ToString().TrimEnd(); //
                    data.JSREDPT2 = reader["JSREDPT2"].ToString().TrimEnd(); //
                    data.JSREDPNM = reader["JSREDPNM"].ToString().TrimEnd(); //
                    data.JSREDPNO = reader["JSREDPNO"].ToString().TrimEnd(); //
                    data.JSRETELE = reader["JSRETELE"].ToString().TrimEnd(); // 정산담당
                    data.JSBUSSCD = reader["JSBUSSCD"].ToString().TrimEnd(); // 정산업무코드
                    data.JSBUSSNM = reader["JSBUSSNM"].ToString().TrimEnd(); // 정산업무명
                    data.PMGUM = ToLong(reader["PMGUM"].ToString().TrimEnd()); // 본인부담환급금 합계
                    data.PMGUM1 = ToLong(reader["PMGUM1"].ToString().TrimEnd()); // 본인부담환급금1 합계
                    data.PMGUM2 = ToLong(reader["PMGUM2"].ToString().TrimEnd()); // 본인부담환급금2 합계
                    data.BHPMGUM = ToLong(reader["BHPMGUM"].ToString().TrimEnd()); // 보훈 본인부담환급금 합계
                    data.PPGUM = ToLong(reader["PPGUM"].ToString().TrimEnd()); // 환수금액 합계
                    data.HOSRETAMT = ToLong(reader["HOSRETAMT"].ToString().TrimEnd()); // 요양기관환수금 합계
                    data.UNAMT = ToLong(reader["UNAMT"].ToString().TrimEnd()); // 보험자부담금 합계
                    data.BHUNAMT = ToLong(reader["BHUNAMT"].ToString().TrimEnd()); // 보훈부담금 합계
                    data.RSTAMT = ToLong(reader["RSTAMT"].ToString().TrimEnd()); // 심사결정액 합계
                    data.RSTCNT = ToLong(reader["RSTCNT"].ToString().TrimEnd()); // 건수합계
                    data.MEMO = reader["MEMO"].ToString().TrimEnd(); // 참조란

                    list.Add(data);

                    return true;
                });
            }

            RefreshGridMain();
        }

        public long ToLong(string p_val)
        {
            long ret = 0;
            double result = 0;
            double.TryParse(p_val, out result);
            ret = Convert.ToInt64(result);
            return ret;
        }

        public double ToDouble(string p_val)
        {
            double result = 0;
            double.TryParse(p_val, out result);
            return result;
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkMemo_Jsredpt.Visible = tabControl1.SelectedIndex == 0;

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
            string jsyyseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "JSYYSEQ").ToString();

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                m_ErrPos = "QueryPtnt";
                QueryPtnt(jsdemseq, jsreday, cnecno, dcount, jsyyseq, conn);
                m_ErrPos = "QueryCode";
                QueryCode(jsdemseq, jsreday, cnecno, dcount, jsyyseq, conn);
                m_ErrPos = "QueryOutYak";
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
            sql += Environment.NewLine + "     , PNM"; // 수진자성명
            sql += Environment.NewLine + "     , INSNM"; // 가입자 성명
            sql += Environment.NewLine + "     , UNICD"; // 사업장기호
            sql += Environment.NewLine + "     , INSID"; // 증번호
            sql += Environment.NewLine + "     , JBFG"; // 의료급여종별구분
            sql += Environment.NewLine + "     , GONSGB"; //
            sql += Environment.NewLine + "     , PMGUM"; // 본인부담환급금
            sql += Environment.NewLine + "     , PMGUM1"; // 본인부담환급금1
            sql += Environment.NewLine + "     , PMGUM2"; // 본인부담환급금2
            sql += Environment.NewLine + "     , BHPMGUM"; // 보훈 본인부담환급금
            sql += Environment.NewLine + "     , HOSRETAMT"; // 요양기관환수금(계)
            sql += Environment.NewLine + "     , UNAMT"; // 보장기관 부담금
            sql += Environment.NewLine + "     , BHUNAMT"; // 보훈부담금
            sql += Environment.NewLine + "     , RSTAMT"; // 심사결정액
            sql += Environment.NewLine + "     , MEMO"; // 명일련비고사항
            sql += Environment.NewLine + "  FROM TIE_F0803";
            sql += Environment.NewLine + " WHERE JSDEMSEQ='" + p_jsdemseq + "'";
            sql += Environment.NewLine + "   AND JSREDAY='" + p_jsreday + "'";
            sql += Environment.NewLine + "   AND CNECNO='" + p_cnecno + "'";
            sql += Environment.NewLine + "   AND DCOUNT='" + p_dcount + "'";
            sql += Environment.NewLine + "   AND JSYYSEQ='" + p_jsyyseq + "'";
            sql += Environment.NewLine + " ORDER BY EPRTNO,JSSEQNO";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CDataPtnt data = new CDataPtnt();
                data.Clear();

                data.JSDEMSEQ = reader["JSDEMSEQ"].ToString().TrimEnd(); // 정산심사차수
                data.JSREDAY = reader["JSREDAY"].ToString().TrimEnd(); // 정산통보일자
                data.CNECNO = reader["CNECNO"].ToString().TrimEnd(); // 접수번호
                data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd(); // 청구서 일련번호
                data.JSYYSEQ = reader["JSYYSEQ"].ToString().TrimEnd(); // 정산연번
                data.JSSEQNO = reader["JSSEQNO"].ToString().TrimEnd(); // 정산일련번호
                data.EPRTNO = reader["EPRTNO"].ToString().TrimEnd(); // 명세서 일련번호
                data.PNM = reader["PNM"].ToString().TrimEnd(); // 수진자성명
                data.INSNM = reader["INSNM"].ToString().TrimEnd(); // 가입자 성명
                data.UNICD = reader["UNICD"].ToString().TrimEnd(); // 사업장기호
                data.INSID = reader["INSID"].ToString().TrimEnd(); // 증번호
                data.JBFG = reader["JBFG"].ToString().TrimEnd(); // 의료급여종별구분
                data.GONSGB = reader["GONSGB"].ToString().TrimEnd(); //
                data.PMGUM = ToLong(reader["PMGUM"].ToString().TrimEnd()); // 본인부담환급금
                data.PMGUM1 = ToLong(reader["PMGUM1"].ToString().TrimEnd()); // 본인부담환급금1
                data.PMGUM2 = ToLong(reader["PMGUM2"].ToString().TrimEnd()); // 본인부담환급금2
                data.BHPMGUM = ToLong(reader["BHPMGUM"].ToString().TrimEnd()); // 보훈 본인부담환급금
                data.HOSRETAMT = ToLong(reader["HOSRETAMT"].ToString().TrimEnd()); // 요양기관환수금(계)
                data.UNAMT = ToLong(reader["UNAMT"].ToString().TrimEnd()); // 보장기관 부담금
                data.BHUNAMT = ToLong(reader["BHUNAMT"].ToString().TrimEnd()); // 보훈부담금
                data.RSTAMT = ToLong(reader["RSTAMT"].ToString().TrimEnd()); // 심사결정액
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
            sql += Environment.NewLine + "SELECT F4.JSDEMSEQ"; // 정산심사차수
            sql += Environment.NewLine + "     , F4.JSREDAY"; // 정산통보일자
            sql += Environment.NewLine + "     , F4.CNECNO"; // 접수번호
            sql += Environment.NewLine + "     , F4.DCOUNT"; // 청구서 일련번호
            sql += Environment.NewLine + "     , F4.JSYYSEQ"; // 정산연번
            sql += Environment.NewLine + "     , F4.JSSEQNO"; // 정산일련번호
            sql += Environment.NewLine + "     , F4.EPRTNO"; // 명세서 일련번호
            sql += Environment.NewLine + "     , F4.OUTCNT"; // 처방전교부번호
            sql += Environment.NewLine + "     , F4.LNO"; // 줄번호
            sql += Environment.NewLine + "     , F4.JJRMK AS JJRMK1"; // 조정사유
            sql += Environment.NewLine + "     , F4.JJRMK2"; // 조정사유2
            sql += Environment.NewLine + "     , F4.BGIHO"; // 코드
            sql += Environment.NewLine + "     , F4.IJDCNT"; // 1회투약량 인정횟수
            sql += Environment.NewLine + "     , F4.IJDQTY"; // 일투 인정횟수
            sql += Environment.NewLine + "     , F4.IJDDAY"; // 총투 인정횟수
            sql += Environment.NewLine + "     , F4.RETAMT"; // 환수금액
            sql += Environment.NewLine + "     , F4.DRUGID"; // 조제기관 기호
            sql += Environment.NewLine + "     , F4.DRUGNM"; // 조제기관 명
            sql += Environment.NewLine + "     , F4.DRUGCNECNO"; // 조제기관 접수번호
            sql += Environment.NewLine + "     , F4.DRUGCNECYY"; // 조제기관 접수년도
            sql += Environment.NewLine + "     , F4.DRUGEPRTNO"; // 조제기관 명일련번호
            sql += Environment.NewLine + "     , F4.MEMO"; // 정산심결 비고(조정사유내역)
            sql += Environment.NewLine + "     , F3.PNM";  // 환자명
            sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE = F4.BGIHO) AS BGIHONM"; // 약품명칭
            sql += Environment.NewLine + "  FROM TIE_F0804 F4 INNER JOIN TIE_F0803 F3 ON F3.JSDEMSEQ=F4.JSDEMSEQ";
            sql += Environment.NewLine + "                                           AND F3.JSREDAY =F4.JSREDAY  ";
            sql += Environment.NewLine + "                                           AND F3.CNECNO  =F4.CNECNO   ";
            sql += Environment.NewLine + "                                           AND F3.DCOUNT  =F4.DCOUNT   ";
            sql += Environment.NewLine + "                                           AND F3.JSYYSEQ =F4.JSYYSEQ  ";
            sql += Environment.NewLine + "                                           AND F3.JSSEQNO =F4.JSSEQNO  ";
            sql += Environment.NewLine + "                                           AND F3.EPRTNO  =F4.EPRTNO   ";
            sql += Environment.NewLine + " WHERE F4.JSDEMSEQ='" + p_jsdemseq + "'";
            sql += Environment.NewLine + "   AND F4.JSREDAY='" + p_jsreday + "'";
            sql += Environment.NewLine + "   AND F4.CNECNO='" + p_cnecno + "'";
            sql += Environment.NewLine + "   AND F4.DCOUNT='" + p_dcount + "'";
            sql += Environment.NewLine + "   AND F4.JSYYSEQ='" + p_jsyyseq + "'";
            sql += Environment.NewLine + " ORDER BY F4.JSDEMSEQ,F4.JSREDAY,F4.CNECNO,F4.DCOUNT,F4.JSYYSEQ,F4.JSSEQNO,F4.EPRTNO,F4.OUTCNT,F4.LNO";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CDataCode data = new CDataCode();
                data.Clear();

                data.JSDEMSEQ = reader["JSDEMSEQ"].ToString().TrimEnd(); // 정산심사차수
                data.JSREDAY = reader["JSREDAY"].ToString().TrimEnd(); // 정산통보일자
                data.CNECNO = reader["CNECNO"].ToString().TrimEnd(); // 접수번호
                data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd(); // 청구서 일련번호
                data.JSYYSEQ = reader["JSYYSEQ"].ToString().TrimEnd(); // 정산연번
                data.JSSEQNO = reader["JSSEQNO"].ToString().TrimEnd(); // 정산일련번호
                data.EPRTNO = reader["EPRTNO"].ToString().TrimEnd(); // 명세서 일련번호
                data.OUTCNT = reader["OUTCNT"].ToString().TrimEnd(); // 처방전교부번호
                data.LNO = reader["LNO"].ToString().TrimEnd(); // 줄번호
                data.JJRMK1 = reader["JJRMK1"].ToString().TrimEnd(); // 조정사유
                data.JJRMK2 = reader["JJRMK2"].ToString().TrimEnd(); // 조정사유2
                data.BGIHO = reader["BGIHO"].ToString().TrimEnd(); // 코드
                data.IJDCNT = ToDouble(reader["IJDCNT"].ToString().TrimEnd()); // 1회투약량 인정횟수
                data.IJDQTY = ToDouble(reader["IJDQTY"].ToString().TrimEnd()); // 일투 인정횟수
                data.IJDDAY = ToLong(reader["IJDDAY"].ToString().TrimEnd()); // 총투 인정횟수
                data.RETAMT = ToLong(reader["RETAMT"].ToString().TrimEnd()); // 환수금액
                data.DRUGID = reader["DRUGID"].ToString().TrimEnd(); // 조제기관 기호
                data.DRUGNM = reader["DRUGNM"].ToString().TrimEnd(); // 조제기관 명
                data.DRUGCNECNO = reader["DRUGCNECNO"].ToString().TrimEnd(); // 조제기관 접수번호
                data.DRUGCNECYY = reader["DRUGCNECYY"].ToString().TrimEnd(); // 조제기관 접수년도
                data.DRUGEPRTNO = reader["DRUGEPRTNO"].ToString().TrimEnd(); // 조제기관 명일련번호
                data.MEMO = reader["MEMO"].ToString().TrimEnd(); // 정산심결 비고(조정사유내역)
                data.PNM = reader["PNM"].ToString().TrimEnd(); // 환자명
                data.BGIHONM = reader["BGIHONM"].ToString().TrimEnd(); // 약품명칭

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
            sql += Environment.NewLine + "SELECT F5.JSDEMSEQ"; // 정산심사차수
            sql += Environment.NewLine + "     , F5.JSREDAY"; // 정산통보일자
            sql += Environment.NewLine + "     , F5.CNECNO"; // 접수번호
            sql += Environment.NewLine + "     , F5.DCOUNT"; // 청구서 일련번호
            sql += Environment.NewLine + "     , F5.JSYYSEQ"; // 정산연번
            sql += Environment.NewLine + "     , F5.JSSEQNO"; // 정산일련번호
            sql += Environment.NewLine + "     , F5.EPRTNO"; // 명세서 일련번호
            sql += Environment.NewLine + "     , F5.OUTCNT"; // 처방전교부번호
            sql += Environment.NewLine + "     , F5.LNO"; // 줄번호
            sql += Environment.NewLine + "     , F5.JKRTBKRMK"; // 본인부담률변경사유
            sql += Environment.NewLine + "     , F5.BGIHO"; // 코드
            sql += Environment.NewLine + "     , F5.IJDCNT"; // 1회투약량 인정횟수
            sql += Environment.NewLine + "     , F5.IJDQTY"; // 일투 인정횟수
            sql += Environment.NewLine + "     , F5.IJDDAY"; // 총투 인정횟수
            sql += Environment.NewLine + "     , F5.IJDJKRTBKAMT"; // 본인부담률변경금액
            sql += Environment.NewLine + "     , F5.DRUGID"; // 조제기관 기호
            sql += Environment.NewLine + "     , F5.DRUGNM"; // 조제기관 명
            sql += Environment.NewLine + "     , F5.DRUGCNECNO"; // 조제기관 접수번호
            sql += Environment.NewLine + "     , F5.DRUGCNECYY"; // 조제기관 접수년도
            sql += Environment.NewLine + "     , F5.DRUGEPRTNO"; // 조제기관 명일련번호
            sql += Environment.NewLine + "     , F5.MEMO"; // 정산심결 비고(본인부담률변경내역)
            sql += Environment.NewLine + "     , F3.PNM"; // 환자명
            sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE = F5.BGIHO) AS BGIHONM"; // 약품명칭
            sql += Environment.NewLine + "  FROM TIE_F0805 F5 INNER JOIN TIE_F0803 F3 ON F3.JSDEMSEQ=F5.JSDEMSEQ";
            sql += Environment.NewLine + "                                           AND F3.JSREDAY =F5.JSREDAY";
            sql += Environment.NewLine + "                                           AND F3.CNECNO  =F5.CNECNO";
            sql += Environment.NewLine + "                                           AND F3.DCOUNT  =F5.DCOUNT";
            sql += Environment.NewLine + "                                           AND F3.JSYYSEQ =F5.JSYYSEQ";
            sql += Environment.NewLine + "                                           AND F3.JSSEQNO =F5.JSSEQNO";
            sql += Environment.NewLine + "                                           AND F3.EPRTNO  =F5.EPRTNO";
            sql += Environment.NewLine + " WHERE F5.JSDEMSEQ='" + p_jsdemseq + "'";
            sql += Environment.NewLine + "   AND F5.JSREDAY='" + p_jsreday + "'";
            sql += Environment.NewLine + "   AND F5.CNECNO='" + p_cnecno + "'";
            sql += Environment.NewLine + "   AND F5.DCOUNT='" + p_dcount + "'";
            sql += Environment.NewLine + "   AND F5.JSYYSEQ='" + p_jsyyseq + "'";
            sql += Environment.NewLine + " ORDER BY F5.JSDEMSEQ,F5.JSREDAY,F5.CNECNO,F5.DCOUNT,F5.JSYYSEQ,F5.JSSEQNO,F5.EPRTNO,F5.OUTCNT,F5.LNO";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CDataBonrt data = new CDataBonrt();
                data.Clear();

                data.JSDEMSEQ = reader["JSDEMSEQ"].ToString().TrimEnd(); // 정산심사차수
                data.JSREDAY = reader["JSREDAY"].ToString().TrimEnd(); // 정산통보일자
                data.CNECNO = reader["CNECNO"].ToString().TrimEnd(); // 접수번호
                data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd(); // 청구서 일련번호
                data.JSYYSEQ = reader["JSYYSEQ"].ToString().TrimEnd(); // 정산연번
                data.JSSEQNO = reader["JSSEQNO"].ToString().TrimEnd(); // 정산일련번호
                data.EPRTNO = reader["EPRTNO"].ToString().TrimEnd(); // 명세서 일련번호
                data.OUTCNT = reader["OUTCNT"].ToString().TrimEnd(); // 처방전교부번호
                data.LNO = reader["LNO"].ToString().TrimEnd(); // 줄번호
                data.JKRTBKRMK = reader["JKRTBKRMK"].ToString().TrimEnd(); // 본인부담률변경사유
                data.BGIHO = reader["BGIHO"].ToString().TrimEnd(); // 코드
                data.IJDCNT = ToDouble(reader["IJDCNT"].ToString().TrimEnd()); // 1회투약량 인정횟수
                data.IJDQTY = ToDouble(reader["IJDQTY"].ToString().TrimEnd()); // 일투 인정횟수
                data.IJDDAY = ToLong(reader["IJDDAY"].ToString().TrimEnd()); // 총투 인정횟수
                data.IJDJKRTBKAMT = ToLong(reader["IJDJKRTBKAMT"].ToString().TrimEnd()); // 본인부담률변경금액
                data.DRUGID = reader["DRUGID"].ToString().TrimEnd(); // 조제기관 기호
                data.DRUGNM = reader["DRUGNM"].ToString().TrimEnd(); // 조제기관 명
                data.DRUGCNECNO = reader["DRUGCNECNO"].ToString().TrimEnd(); // 조제기관 접수번호
                data.DRUGCNECYY = reader["DRUGCNECYY"].ToString().TrimEnd(); // 조제기관 접수년도
                data.DRUGEPRTNO = reader["DRUGEPRTNO"].ToString().TrimEnd(); // 조제기관 명일련번호
                data.MEMO = reader["MEMO"].ToString().TrimEnd(); // 정산심결 비고(조정사유내역)
                data.PNM = reader["PNM"].ToString().TrimEnd(); // 환자명
                data.BGIHONM = reader["BGIHONM"].ToString().TrimEnd(); // 약품명칭

                list.Add(data);

                return true;
            });

            RefreshGridBonrt();
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
            String strTitle = "원외처방약제비 심사결과 추가 통보서";
            strTitle += "(" + tabControl1.SelectedTab.Text.ToString() + ")";

            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString(strTitle, Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            String strCaption = "";
            if (tabControl1.SelectedIndex == 0)
            {
                //strCaption += "접수번호 : " + txtAccno.Text.ToString();
                //strCaption += ", 차수 : " + txtCntno.Text.ToString();
            }
            else
            {
                string demno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "DEMNO").ToString();
                string cnecno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "CNECNO").ToString();

                strCaption += "청구번호 : " + demno + ", 접수번호 : " + cnecno;
            }
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(strCaption, Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
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
            e.Graph.DrawString("ADD0723E", Color.Black, new RectangleF(0, 0, 120, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시(사실은 조회일시)
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(sysDate + " " + sysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
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

        private void chkMemo_Jsredpt_CheckedChanged(object sender, EventArgs e)
        {
            grdMainView.OptionsView.ShowPreview = chkMemo_Jsredpt.Checked;
            grdMainView.OptionsPrint.PrintPreview = chkMemo_Jsredpt.Checked;

            grdMainView.Columns["MEMO"].Visible = !chkMemo_Jsredpt.Checked;
            grdMainView.Columns["JSREDEPT"].Visible = !chkMemo_Jsredpt.Checked;

        }

    }
}
