using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0721E
{
    public partial class ADD0721E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private int m_PrevTabIndex = -1;
        private string m_ErrPos = "";

        public ADD0721E()
        {
            InitializeComponent();
        }

        public ADD0721E(String user, String pwd, String prjcd, String addpara)
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

        private void ADD0721E_Load(object sender, EventArgs e)
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
                sql += Environment.NewLine + "SELECT F0501.VERSION"; // 버전구분
                sql += Environment.NewLine + "     , F0501.JSDEMSEQ"; // 정산통보번호
                sql += Environment.NewLine + "     , F0501.JSREDAY"; // 정산통보일자
                sql += Environment.NewLine + "     , F0501.CNECNO"; // 접수번호
                sql += Environment.NewLine + "     , F0501.DCOUNT"; // 청구서 일련번호
                sql += Environment.NewLine + "     , F0501.FMNO"; // 서식번호
                sql += Environment.NewLine + "     , F0501.HOSID"; // 요양기관 기호
                sql += Environment.NewLine + "     , F0501.JIWONCD"; // 지원코드
                sql += Environment.NewLine + "     , F0501.DEMSEQ"; // 심사차수(yyyymm+seq(2))
                sql += Environment.NewLine + "     , F0501.DEMNO"; // 청구번호
                sql += Environment.NewLine + "     , F0501.GRPNO"; // 묶음번호
                sql += Environment.NewLine + "     , F0501.DEMUNITFG"; // 청구단위구분
                sql += Environment.NewLine + "     , F0502.JSYYSEQ"; // 정산연번
                sql += Environment.NewLine + "     , F0502.SIMGBN"; // 심사구분
                sql += Environment.NewLine + "     , F0502.JSREDPT1"; // 정산담당부명
                sql += Environment.NewLine + "     , F0502.JSREDPT2"; // 정산담당조명
                sql += Environment.NewLine + "     , F0502.JSREDPNM"; // 정산담당자명
                sql += Environment.NewLine + "     , F0502.JSREDPNO"; // 정산담당자번호
                sql += Environment.NewLine + "     , F0502.JSRETELE"; // 정산담당자전화번호
                sql += Environment.NewLine + "     , F0502.JSBUSSCD"; // 정산업무코드
                sql += Environment.NewLine + "     , F0502.JSBUSSNM"; // 정산업무명
                sql += Environment.NewLine + "     , F0502.SKPMGUM"; // 이전심결사항-본인부담환급금 합계
                sql += Environment.NewLine + "     , F0502.SKPPGUM"; // 이전심결사항-본인추가부담금 합계
                sql += Environment.NewLine + "     , F0502.SKTTAMT"; // 이전심결사항-의료급여비용총액 합계
                sql += Environment.NewLine + "     , F0502.SKPTAMT"; // 이전심결사항-본인부담금 합계
                sql += Environment.NewLine + "     , F0502.SKRELAM"; // 이전심결사항-대불금 합계
                sql += Environment.NewLine + "     , F0502.SKJAM"; // 이전심결사항-장애인의료비
                sql += Environment.NewLine + "     , F0502.SKUNAMT"; // 이전심결사항-보장기관부담금 합계
                sql += Environment.NewLine + "     , F0502.SKBHUNAMT"; // 이전심결사항-보훈청구액 합계
                sql += Environment.NewLine + "     , F0502.SKRSTAMT"; // 이전심결사항-심사결정액 합계
                sql += Environment.NewLine + "     , F0502.SKTTTAMT"; // 이전심결사항-진료비총액
                sql += Environment.NewLine + "     , F0502.SKSUTAKAMT"; // 이전심결사항-위탁검사직접지급금 합계
                sql += Environment.NewLine + "     , F0502.SKQFDSCNT"; // 이전심결사항-수급권확인대상건수 합계
                sql += Environment.NewLine + "     , F0502.SKCNT"; // 이전심결사항-건수합계
                sql += Environment.NewLine + "     , F0502.JSPMGUM"; // 정산심결사항-본인부담환급금 합계
                sql += Environment.NewLine + "     , F0502.JSPPGUM"; // 정산심결사항-본인추가부담금 합계
                sql += Environment.NewLine + "     , F0502.JSTTAMT"; //  정산심결사항-의료급여비용총액 합계
                sql += Environment.NewLine + "     , F0502.JSPTAMT"; // 정산심결사항-본인부담금 합계
                sql += Environment.NewLine + "     , F0502.JSRELAM"; // 정산심결사항-대불금 합계
                sql += Environment.NewLine + "     , F0502.JSJAM"; // 정산심결사항-장애인의료비
                sql += Environment.NewLine + "     , F0502.JSUNAMT"; // 정산심결사항-보장기관부담금 합계
                sql += Environment.NewLine + "     , F0502.JSBHUNAMT"; // 정산심결사항-보훈부담금 합계
                sql += Environment.NewLine + "     , F0502.JSRSTAMT"; // 정산심결사항-심사결정액 합계
                sql += Environment.NewLine + "     , F0502.JSTTTAMT"; // 정산심결사항-진료비총액 합계
                sql += Environment.NewLine + "     , F0502.JSSUTAKAMT"; // 정산심결사항-위탁검사직접지급금 합계
                sql += Environment.NewLine + "     , F0502.JSRSTCHAAMT"; // 정산심결사항-결정차액 합계
                sql += Environment.NewLine + "     , F0502.JSQFDSCNT"; // 정산심결사항-수급권확인대상건수 합계
                sql += Environment.NewLine + "     , F0502.JSCNT"; // 정산심결사항-건수 합계
                sql += Environment.NewLine + "     , F0502.JSSKAMT"; // 정산심사결과인정(조정)금액합계
                sql += Environment.NewLine + "     , F0502.JSSKCNT"; // 정산심사결과인정(조정)건수합계
                sql += Environment.NewLine + "     , F0502.MEMO"; // 참조란
                sql += Environment.NewLine + "     , F0502.SKUPLMTCHATTAMT";
                sql += Environment.NewLine + "     , F0502.SKPTTTAMT";
                sql += Environment.NewLine + "     , F0502.JSUPLMTCHATTAMT";
                sql += Environment.NewLine + "     , F0502.JSPTTTAMT";
                sql += Environment.NewLine + "     , F0502.JJUPLMTCHATTAMT";
                sql += Environment.NewLine + "     , F0502.SKBAKAMT";
                sql += Environment.NewLine + "     , F0502.JSBAKAMT";
                sql += Environment.NewLine + "     , F0502.SKBHPMGUM";
                sql += Environment.NewLine + "     , F0502.JSBHPMGUM";
                sql += Environment.NewLine + "     , F0502.SKBHPPGUM";
                sql += Environment.NewLine + "     , F0502.JSBHPPGUM";
                sql += Environment.NewLine + "     , F0502.SKBHPTAMT";
                sql += Environment.NewLine + "     , F0502.JSBHPTAMT";
                sql += Environment.NewLine + "     , F0502.BAKDNSKPMGUM"; // 2014.12.04 KJW - 100/100미만 본인부담금합계(이전)
                sql += Environment.NewLine + "     , F0502.BAKDNSKPPGUM"; // 2014.12.04 KJW - 100/100미만 본인추가부담금합계
                sql += Environment.NewLine + "     , F0502.BAKDNSKTTAMT"; // 2014.12.04 KJW - 100/100미만 총액합계
                sql += Environment.NewLine + "     , F0502.BAKDNSKPTAMT"; // 2014.12.04 KJW - 100/100미만 본인일부부담금합계
                sql += Environment.NewLine + "     , F0502.BAKDNSKUNAMT"; // 2014.12.04 KJW - 100/100미만 청구액합계
                sql += Environment.NewLine + "     , F0502.BAKDNSKBHUNAMT"; // 2014.12.04 KJW - 100/100미만 보훈청구액합계
                sql += Environment.NewLine + "     , F0502.BAKDNJSPMGUM"; // 2014.12.04 KJW - 100/100미만 본인부담금합계(정산)
                sql += Environment.NewLine + "     , F0502.BAKDNJSPPGUM"; // 2014.12.04 KJW - 100/100미만 본인추가부담금합계
                sql += Environment.NewLine + "     , F0502.BAKDNJSTTAMT"; // 2014.12.04 KJW - 100/100미만 총액합계
                sql += Environment.NewLine + "     , F0502.BAKDNJSPTAMT"; // 2014.12.04 KJW - 100/100미만 본인일부부담금합계
                sql += Environment.NewLine + "     , F0502.BAKDNJSUNAMT"; // 2014.12.04 KJW - 100/100미만 청구액합계
                sql += Environment.NewLine + "     , F0502.BAKDNJSBHUNAMT"; // 2014.12.04 KJW - 100/100미만 보훈청구액합계
                sql += Environment.NewLine + "     , F0502.SKSANGKYERFAMT"; // 2014.12.04 KJW - 상계환급금(이전)
                sql += Environment.NewLine + "     , F0502.SKSANGKYEADDAMT"; // 2014.12.04 KJW - 상계추가부담금
                sql += Environment.NewLine + "     , F0502.JSSANGKYERFAMT"; // 2014.12.04 KJW - 상계환급금(정산)
                sql += Environment.NewLine + "     , F0502.JSSANGKYEADDAMT"; // 2014.12.04 KJW - 상계추가부담금
                sql += Environment.NewLine + "  FROM TIE_F0501 F0501 INNER JOIN TIE_F0502 F0502 ON F0502.JSDEMSEQ=F0501.JSDEMSEQ";
                sql += Environment.NewLine + "                                                 AND F0502.JSREDAY=F0501.JSREDAY";
                sql += Environment.NewLine + "                                                 AND F0502.CNECNO=F0501.CNECNO";
                sql += Environment.NewLine + "                                                 AND F0502.DCOUNT=F0501.DCOUNT";
                sql += Environment.NewLine + " WHERE 1=1 ";
                if (txtFrdt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0501.JSREDAY>='" + txtFrdt.Text.ToString() + "'";
                }
                if (txtTodt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0501.JSREDAY<='" + txtTodt.Text.ToString() + "'";
                }
                if (txtCnecno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0501.CNECNO='" + txtCnecno.Text.ToString() + "'";
                }
                if (txtDemno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0501.DEMNO='" + txtDemno.Text.ToString() + "'";
                }
                sql += Environment.NewLine + " ORDER BY F0501.JSREDAY DESC";


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
                    data.DEMUNITFG = reader["DEMUNITFG"].ToString().TrimEnd(); // 청구단위구분
                    data.JSYYSEQ = reader["JSYYSEQ"].ToString().TrimEnd(); // 정산연번
                    data.SIMGBN = reader["SIMGBN"].ToString().TrimEnd(); // 심사구분
                    data.JSREDPT1 = reader["JSREDPT1"].ToString().TrimEnd(); // 정산담당부명
                    data.JSREDPT2 = reader["JSREDPT2"].ToString().TrimEnd(); // 정산담당조명
                    data.JSREDPNM = reader["JSREDPNM"].ToString().TrimEnd(); // 정산담당자명
                    data.JSREDPNO = reader["JSREDPNO"].ToString().TrimEnd(); // 정산담당자번호
                    data.JSRETELE = reader["JSRETELE"].ToString().TrimEnd(); // 정산담당자전화번호
                    data.JSBUSSCD = reader["JSBUSSCD"].ToString().TrimEnd(); // 정산업무코드
                    data.JSBUSSNM = reader["JSBUSSNM"].ToString().TrimEnd(); // 정산업무명
                    data.SKPMGUM = MetroLib.StrHelper.ToLong(reader["SKPMGUM"].ToString().TrimEnd()); // 이전심결사항-본인부담환급금 합계
                    data.SKPPGUM = MetroLib.StrHelper.ToLong(reader["SKPPGUM"].ToString().TrimEnd()); // 이전심결사항-본인추가부담금 합계
                    data.SKTTAMT = MetroLib.StrHelper.ToLong(reader["SKTTAMT"].ToString().TrimEnd()); // 이전심결사항-의료급여비용총액 합계
                    data.SKPTAMT = MetroLib.StrHelper.ToLong(reader["SKPTAMT"].ToString().TrimEnd()); // 이전심결사항-본인부담금 합계
                    data.SKRELAM = MetroLib.StrHelper.ToLong(reader["SKRELAM"].ToString().TrimEnd()); // 이전심결사항-대불금 합계
                    data.SKJAM = MetroLib.StrHelper.ToLong(reader["SKJAM"].ToString().TrimEnd()); // 이전심결사항-장애인의료비
                    data.SKUNAMT = MetroLib.StrHelper.ToLong(reader["SKUNAMT"].ToString().TrimEnd()); // 이전심결사항-보장기관부담금 합계
                    data.SKBHUNAMT = MetroLib.StrHelper.ToLong(reader["SKBHUNAMT"].ToString().TrimEnd()); // 이전심결사항-보훈청구액 합계
                    data.SKRSTAMT = MetroLib.StrHelper.ToLong(reader["SKRSTAMT"].ToString().TrimEnd()); // 이전심결사항-심사결정액 합계
                    data.SKTTTAMT = MetroLib.StrHelper.ToLong(reader["SKTTTAMT"].ToString().TrimEnd()); // 이전심결사항-진료비총액
                    data.SKSUTAKAMT = MetroLib.StrHelper.ToLong(reader["SKSUTAKAMT"].ToString().TrimEnd()); // 이전심결사항-위탁검사직접지급금 합계
                    data.SKQFDSCNT = MetroLib.StrHelper.ToLong(reader["SKQFDSCNT"].ToString().TrimEnd()); // 이전심결사항-수급권확인대상건수 합계
                    data.SKCNT = MetroLib.StrHelper.ToLong(reader["SKCNT"].ToString().TrimEnd()); // 이전심결사항-건수합계
                    data.JSPMGUM = MetroLib.StrHelper.ToLong(reader["JSPMGUM"].ToString().TrimEnd()); // 정산심결사항-본인부담환급금 합계
                    data.JSPPGUM = MetroLib.StrHelper.ToLong(reader["JSPPGUM"].ToString().TrimEnd()); // 정산심결사항-본인추가부담금 합계
                    data.JSTTAMT = MetroLib.StrHelper.ToLong(reader["JSTTAMT"].ToString().TrimEnd()); // 정산심결사항-의료급여비용총액 합계
                    data.JSPTAMT = MetroLib.StrHelper.ToLong(reader["JSPTAMT"].ToString().TrimEnd()); // 정산심결사항-본인부담금 합계
                    data.JSRELAM = MetroLib.StrHelper.ToLong(reader["JSRELAM"].ToString().TrimEnd()); // 정산심결사항-대불금 합계
                    data.JSJAM = MetroLib.StrHelper.ToLong(reader["JSJAM"].ToString().TrimEnd()); // 정산심결사항-장애인의료비
                    data.JSUNAMT = MetroLib.StrHelper.ToLong(reader["JSUNAMT"].ToString().TrimEnd()); // 정산심결사항-보장기관부담금 합계
                    data.JSBHUNAMT = MetroLib.StrHelper.ToLong(reader["JSBHUNAMT"].ToString().TrimEnd()); // 정산심결사항-보훈부담금 합계
                    data.JSRSTAMT = MetroLib.StrHelper.ToLong(reader["JSRSTAMT"].ToString().TrimEnd()); // 정산심결사항-심사결정액 합계
                    data.JSTTTAMT = MetroLib.StrHelper.ToLong(reader["JSTTTAMT"].ToString().TrimEnd()); // 정산심결사항-진료비총액 합계
                    data.JSSUTAKAMT = MetroLib.StrHelper.ToLong(reader["JSSUTAKAMT"].ToString().TrimEnd()); // 정산심결사항-위탁검사직접지급금 합계
                    data.JSRSTCHAAMT = MetroLib.StrHelper.ToLong(reader["JSRSTCHAAMT"].ToString().TrimEnd()); // 정산심결사항-결정차액 합계
                    data.JSQFDSCNT = MetroLib.StrHelper.ToLong(reader["JSQFDSCNT"].ToString().TrimEnd()); // 정산심결사항-수급권확인대상건수 합계
                    data.JSCNT = MetroLib.StrHelper.ToLong(reader["JSCNT"].ToString().TrimEnd()); // 정산심결사항-건수 합계
                    data.JSSKAMT = MetroLib.StrHelper.ToLong(reader["JSSKAMT"].ToString().TrimEnd()); // 정산심사결과인정(조정)금액합계
                    data.JSSKCNT = MetroLib.StrHelper.ToLong(reader["JSSKCNT"].ToString().TrimEnd()); // 정산심사결과인정(조정)건수합계
                    data.MEMO = reader["MEMO"].ToString().TrimEnd(); // 참조란
                    data.SKUPLMTCHATTAMT = MetroLib.StrHelper.ToLong(reader["SKUPLMTCHATTAMT"].ToString().TrimEnd()); //
                    data.SKPTTTAMT = MetroLib.StrHelper.ToLong(reader["SKPTTTAMT"].ToString().TrimEnd()); //
                    data.JSUPLMTCHATTAMT = MetroLib.StrHelper.ToLong(reader["JSUPLMTCHATTAMT"].ToString().TrimEnd()); //
                    data.JSPTTTAMT = MetroLib.StrHelper.ToLong(reader["JSPTTTAMT"].ToString().TrimEnd()); //
                    data.JJUPLMTCHATTAMT = MetroLib.StrHelper.ToLong(reader["JJUPLMTCHATTAMT"].ToString().TrimEnd()); //
                    data.SKBAKAMT = MetroLib.StrHelper.ToLong(reader["SKBAKAMT"].ToString().TrimEnd()); //
                    data.JSBAKAMT = MetroLib.StrHelper.ToLong(reader["JSBAKAMT"].ToString().TrimEnd()); //
                    data.SKBHPMGUM = MetroLib.StrHelper.ToLong(reader["SKBHPMGUM"].ToString().TrimEnd()); //
                    data.JSBHPMGUM = MetroLib.StrHelper.ToLong(reader["JSBHPMGUM"].ToString().TrimEnd()); //
                    data.SKBHPPGUM = MetroLib.StrHelper.ToLong(reader["SKBHPPGUM"].ToString().TrimEnd()); //
                    data.JSBHPPGUM = MetroLib.StrHelper.ToLong(reader["JSBHPPGUM"].ToString().TrimEnd()); //
                    data.SKBHPTAMT = MetroLib.StrHelper.ToLong(reader["SKBHPTAMT"].ToString().TrimEnd()); //
                    data.JSBHPTAMT = MetroLib.StrHelper.ToLong(reader["JSBHPTAMT"].ToString().TrimEnd()); //
                    data.BAKDNSKPMGUM = MetroLib.StrHelper.ToLong(reader["BAKDNSKPMGUM"].ToString().TrimEnd()); // 2014.12.04 KJW - 100/100미만 본인부담금합계(이전)
                    data.BAKDNSKPPGUM = MetroLib.StrHelper.ToLong(reader["BAKDNSKPPGUM"].ToString().TrimEnd()); // 2014.12.04 KJW - 100/100미만 본인추가부담금합계
                    data.BAKDNSKTTAMT = MetroLib.StrHelper.ToLong(reader["BAKDNSKTTAMT"].ToString().TrimEnd()); // 2014.12.04 KJW - 100/100미만 총액합계
                    data.BAKDNSKPTAMT = MetroLib.StrHelper.ToLong(reader["BAKDNSKPTAMT"].ToString().TrimEnd()); // 2014.12.04 KJW - 100/100미만 본인일부부담금합계
                    data.BAKDNSKUNAMT = MetroLib.StrHelper.ToLong(reader["BAKDNSKUNAMT"].ToString().TrimEnd()); // 2014.12.04 KJW - 100/100미만 청구액합계
                    data.BAKDNSKBHUNAMT = MetroLib.StrHelper.ToLong(reader["BAKDNSKBHUNAMT"].ToString().TrimEnd()); // 2014.12.04 KJW - 100/100미만 보훈청구액합계
                    data.BAKDNJSPMGUM = MetroLib.StrHelper.ToLong(reader["BAKDNJSPMGUM"].ToString().TrimEnd()); // 2014.12.04 KJW - 100/100미만 본인부담금합계(정산)
                    data.BAKDNJSPPGUM = MetroLib.StrHelper.ToLong(reader["BAKDNJSPPGUM"].ToString().TrimEnd()); // 2014.12.04 KJW - 100/100미만 본인추가부담금합계
                    data.BAKDNJSTTAMT = MetroLib.StrHelper.ToLong(reader["BAKDNJSTTAMT"].ToString().TrimEnd()); // 2014.12.04 KJW - 100/100미만 총액합계
                    data.BAKDNJSPTAMT = MetroLib.StrHelper.ToLong(reader["BAKDNJSPTAMT"].ToString().TrimEnd()); // 2014.12.04 KJW - 100/100미만 본인일부부담금합계
                    data.BAKDNJSUNAMT = MetroLib.StrHelper.ToLong(reader["BAKDNJSUNAMT"].ToString().TrimEnd()); // 2014.12.04 KJW - 100/100미만 청구액합계
                    data.BAKDNJSBHUNAMT = MetroLib.StrHelper.ToLong(reader["BAKDNJSBHUNAMT"].ToString().TrimEnd()); // 2014.12.04 KJW - 100/100미만 보훈청구액합계
                    data.SKSANGKYERFAMT = MetroLib.StrHelper.ToLong(reader["SKSANGKYERFAMT"].ToString().TrimEnd()); // 2014.12.04 KJW - 상계환급금(이전)
                    data.SKSANGKYEADDAMT = MetroLib.StrHelper.ToLong(reader["SKSANGKYEADDAMT"].ToString().TrimEnd()); // 2014.12.04 KJW - 상계추가부담금
                    data.JSSANGKYERFAMT = MetroLib.StrHelper.ToLong(reader["JSSANGKYERFAMT"].ToString().TrimEnd()); // 2014.12.04 KJW - 상계환급금(정산)
                    data.JSSANGKYEADDAMT = MetroLib.StrHelper.ToLong(reader["JSSANGKYEADDAMT"].ToString().TrimEnd()); // 2014.12.04 KJW - 상계추가부담금

                    list.Add(data);

                    return true;
                });


            }


            string[] check_field1 = {

                // 심평원에서 삭제했거나 사용 보류시킨 컬럼                            
                "SKSUTAKAMT","JSSUTAKAMT",
                "SKUPLMTCHATTAMT","JSUPLMTCHATTAMT",
                "SKPTTTAMT","JSPTTTAMT",

                // 보훈관련 컬럼
                "SKBHPMGUM","JSBHPMGUM",
                "SKBHPPGUM","JSBHPPGUM",
                "SKBHUNAMT","JSBHUNAMT",
                "SKBHPTAMT","JSBHPTAMT",
                "BAKDNSKBHUNAMT","BAKDNJSBHUNAMT",

                // 100미만 컬럼
                "BAKDNSKPMGUM","BAKDNJSPMGUM",
                "BAKDNSKPPGUM","BAKDNJSPPGUM",
                "BAKDNSKTTAMT","BAKDNJSTTAMT",
                "BAKDNSKPTAMT","BAKDNJSPTAMT",
                "BAKDNSKUNAMT","BAKDNJSUNAMT", // 100미만 청구액
                "BAKDNSKBHUNAMT","BAKDNJSBHUNAMT", // 100미만 보훈청구액

                // 100총액
                "SKBAKAMT","JSBAKAMT", // 100총액

                // 기타
                "SKPMGUM","JSPMGUM", // 본인부담환급금
                "SKSANGKYERFAMT","JSSANGKYERFAMT", // 상계환급금
                "SKPPGUM","JSPPGUM", // 본인추가부담금
                "SKSANGKYEADDAMT","JSSANGKYEADDAMT", // 상계추가부담금
                "SKRELAM","JSRELAM", // 대불금
                "SKJAM","JSJAM", // 장애인의료비
            };

            DxLib.GridHelper.HideColumnIfZeroInBand(check_field1, grdMainView);
            DxLib.GridHelper.SetBandVisible(grdMainView);

            gridBand3.Visible = false;
            gridBand141.Visible = false;
            gridBand139.Visible = false;
            gridBand137.Visible = false;
            gridBand135.Visible = false;

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
            //
            if (grdSutak.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdSutak.BeginInvoke(new Action(() => grdSutakView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdSutakView.RefreshData();
                Application.DoEvents();
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
            string jsyyseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "JSYYSEQ").ToString();
            string jsskcnt = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "JSSKCNT").ToString();
            string jsskamt = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, "JSSKAMT").ToString();

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
            txtHeadJsskcnt.Text = jsskcnt;
            txtHeadJsskamt.Text = jsskamt;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                m_ErrPos = "QueryPtnt";
                QueryPtnt(jsdemseq, jsreday, cnecno, dcount, jsyyseq, demno, conn);
                m_ErrPos = "QueryCode";
                QueryCode(jsdemseq, jsreday, cnecno, dcount, jsyyseq, demno, conn);
                m_ErrPos = "QueryBonrt";
                QueryBonrt(jsdemseq, jsreday, cnecno, dcount, jsyyseq, demno, conn);
                m_ErrPos = "QuerySutak";
                QuerySutak(jsdemseq, jsreday, cnecno, dcount, jsyyseq, demno, conn);
            }

            RefreshGridMain();
        }

        private void QueryPtnt(string p_jsdemseq, string p_jsreday, string p_cnecno, string p_dcount, string p_jsyyseq, string p_demno, OleDbConnection p_conn)
        {
            List<CDataPtnt> list = new List<CDataPtnt>();
            grdPtnt.DataSource = null;
            grdPtnt.DataSource = list;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT F0503.JSDEMSEQ";   // 정산통보번호
            sql += Environment.NewLine + "     , F0503.JSREDAY";   // 정산통보일자
            sql += Environment.NewLine + "     , F0503.CNECNO";   // 접수번호
            sql += Environment.NewLine + "     , F0503.DCOUNT";   // 청구서 일련번호
            sql += Environment.NewLine + "     , F0503.JSYYSEQ";   // 정산연번
            sql += Environment.NewLine + "     , F0503.JSSEQNO";   // 정산일련번호
            sql += Environment.NewLine + "     , F0503.EPRTNO";   // 명세서 일련번호
            sql += Environment.NewLine + "     , F0503.PNM";   // 수진자성명
            sql += Environment.NewLine + "     , F0503.RESID";   // 수진자주민등록번호
            sql += Environment.NewLine + "     , F0503.INSNM";   // 세대주 성명
            sql += Environment.NewLine + "     , F0503.UNICD";   // 보장기관기호
            sql += Environment.NewLine + "     , F0503.JRHT";   // 진료형태
            sql += Environment.NewLine + "     , F0503.JBFG";   // 의료급여종별구분
            sql += Environment.NewLine + "     , F0503.GUBUN";   // 구분(1.환급 2.환수 3.환수+환급)
            sql += Environment.NewLine + "     , F0503.INSID";   // 증번호
            sql += Environment.NewLine + "     , F0503.JAJR";   // 정액정률구분(0.정액 9.정률 1.장기요양정액수가 2.장기요양행위별수가)
            sql += Environment.NewLine + "     , F0503.STEDT";   // 당월요양개시일
            sql += Environment.NewLine + "     , F0503.SKTJKH";   // 이전심결사항-특정기호
            sql += Environment.NewLine + "     , F0503.DACD";   // 상병분류기호
            sql += Environment.NewLine + "     , F0503.JJSOGE1";   // 조정소계1
            sql += Environment.NewLine + "     , F0503.JJSOGE2";   // 조정소계2
            sql += Environment.NewLine + "     , F0503.SKPMGUM";   // 이전심결사항-본인부담환급금
            sql += Environment.NewLine + "     , F0503.SKPPGUM";   // 이전심결사항-본인추가부담금
            sql += Environment.NewLine + "     , F0503.SKTTAMT";   // 이전심결사항-의료급여비용총액
            sql += Environment.NewLine + "     , F0503.SKPTAMT";   // 이전심결사항-본인부담금
            sql += Environment.NewLine + "     , F0503.SKRELAM";   // 이전심결사항-대불금
            sql += Environment.NewLine + "     , F0503.SKJAM";   // 이전심결사항-장애인의료비
            sql += Environment.NewLine + "     , F0503.SKUNAMT";   // 이전심결사항-보장기관부담금
            sql += Environment.NewLine + "     , F0503.SKTTTAMT";   // 이전심결사항-진료비총액
            sql += Environment.NewLine + "     , F0503.SKBHUNAMT";   // 이전심결사항-보훈청구액
            sql += Environment.NewLine + "     , F0503.SKRSTAMT";   // 이전심결사항-심사결정액
            sql += Environment.NewLine + "     , F0503.SKSUTAKAMT";   // 이전심결사항-위탁검사직접지급금
            sql += Environment.NewLine + "     , F0503.SKEXAMC";   // 이전심결사항-입원일수(내원일수)
            sql += Environment.NewLine + "     , F0503.SKORDDAYS";   // 이전심결사항-요양급여일수
            sql += Environment.NewLine + "     , F0503.SKORDCNT";   // 이전심결사항-처방횟수
            sql += Environment.NewLine + "     , F0503.JSTJKH";   // 정산심결사항-특정기호
            sql += Environment.NewLine + "     , F0503.JSPMGUM";   // 정산심결사항-본인부담환급금
            sql += Environment.NewLine + "     , F0503.JSPPGUM";   // 정산심결사항-본인추가부담금
            sql += Environment.NewLine + "     , F0503.JSTTAMT";   // 정산심결사항-의료급여비용총액
            sql += Environment.NewLine + "     , F0503.JSPTAMT";   // 정산심결사항-본인부담금
            sql += Environment.NewLine + "     , F0503.JSRELAM";   // 정산심결사항-대불금
            sql += Environment.NewLine + "     , F0503.JSJAM";   // 정산심결사항-장애인의료비
            sql += Environment.NewLine + "     , F0503.JSUNAMT";   // 정산심결사항-보장기관부담금
            sql += Environment.NewLine + "     , F0503.JSTTTAMT";   // 정산심결사항-진료비총액
            sql += Environment.NewLine + "     , F0503.JSBHUNAMT";   // 정산심결사항-보훈부담금
            sql += Environment.NewLine + "     , F0503.JSRSTAMT";   // 정산심결사항-심사결정액
            sql += Environment.NewLine + "     , F0503.JSSUTAKAMT";   // 정산심결사항-위탁검사직접지급금
            sql += Environment.NewLine + "     , F0503.JSRSTCHAAMT";   // 정산심결사항-결정차액
            sql += Environment.NewLine + "     , F0503.JSEXAMC";   // 정산심결사항-입원일수(내원일수)
            sql += Environment.NewLine + "     , F0503.JSORDDAYS";   // 정산심결사항-요양급여일수
            sql += Environment.NewLine + "     , F0503.JSORDCNT";   // 정산심결사항-처방횟수
            sql += Environment.NewLine + "     , F0503.MEMO";   // 명일련비고사항
            sql += Environment.NewLine + "     , F0503.SKGONSGB";
            sql += Environment.NewLine + "     , F0503.SKSBRDNTYPE";
            sql += Environment.NewLine + "     , F0503.SKTSAMT";
            sql += Environment.NewLine + "     , F0503.SK100AMT";
            sql += Environment.NewLine + "     , F0503.SKUISAMT";
            sql += Environment.NewLine + "     , F0503.JSGONSGB";
            sql += Environment.NewLine + "     , F0503.JSSBRDNTYPE";
            sql += Environment.NewLine + "     , F0503.JSTSAMT";
            sql += Environment.NewLine + "     , F0503.JS100AMT";
            sql += Environment.NewLine + "     , F0503.JSUISAMT";
            sql += Environment.NewLine + "     , F0503.SKMKDRUGCNT";   // 이전심결사항-직접조제횟수
            sql += Environment.NewLine + "     , F0503.JSMKDRUGCNT";   // 정산심결사항-직접조제횟수
            sql += Environment.NewLine + "     , F0503.SKUPLMTCHATTAMT";
            sql += Environment.NewLine + "     , F0503.SKPTTTAMT";
            sql += Environment.NewLine + "     , F0503.JSUPLMTCHATTAMT";
            sql += Environment.NewLine + "     , F0503.JSPTTTAMT";
            sql += Environment.NewLine + "     , F0503.SKBHPMGUM"; // 보훈 본인부담환급금
            sql += Environment.NewLine + "     , F0503.JSBHPMGUM";
            sql += Environment.NewLine + "     , F0503.SKBHPPGUM";
            sql += Environment.NewLine + "     , F0503.JSBHPPGUM";
            sql += Environment.NewLine + "     , F0503.SKBHPTAMT";
            sql += Environment.NewLine + "     , F0503.JSBHPTAMT";
            sql += Environment.NewLine + "     , F0503.BAKDNSKPMGUM";   // 2014.12.16 KJW - 100/100미만 본인부담환급금
            sql += Environment.NewLine + "     , F0503.BAKDNSKPPGUM";   // 2014.12.16 KJW - 100/100미만 본인추가부담금합계
            sql += Environment.NewLine + "     , F0503.BAKDNSKTTAMT";   // 2014.12.16 KJW - 100/100미만 총액합계
            sql += Environment.NewLine + "     , F0503.BAKDNSKPTAMT";   // 2014.12.16 KJW - 100/100미만 본인일부부담금합계
            sql += Environment.NewLine + "     , F0503.BAKDNSKUNAMT";   // 2014.12.16 KJW - 100/100미만 청구액합계
            sql += Environment.NewLine + "     , F0503.BAKDNSKBHUNAMT";   // 2014.12.16 KJW - 100/100미만 보훈청구액합계
            sql += Environment.NewLine + "     , F0503.BAKDNJSPMGUM";   // 2014.12.16 KJW - 100/100미만 본인부담환급금
            sql += Environment.NewLine + "     , F0503.BAKDNJSPPGUM";   // 2014.12.16 KJW - 100/100미만 본인추가부담금합계
            sql += Environment.NewLine + "     , F0503.BAKDNJSTTAMT";   // 2014.12.16 KJW - 100/100미만 총액합계
            sql += Environment.NewLine + "     , F0503.BAKDNJSPTAMT";   // 2014.12.16 KJW - 100/100미만 본인일부부담금합계
            sql += Environment.NewLine + "     , F0503.BAKDNJSUNAMT";   // 2014.12.16 KJW - 100/100미만 청구액합계
            sql += Environment.NewLine + "     , F0503.BAKDNJSBHUNAMT";   // 2014.12.16 KJW - 100/100미만 보훈청구액합계
            sql += Environment.NewLine + "     , F0503.SKSANGKYERFAMT";   // 2014.12.16 KJW - 상계환급금(이전)
            sql += Environment.NewLine + "     , F0503.SKSANGKYEADDAMT";   // 2014.12.16 KJW - 상계추가부담금
            sql += Environment.NewLine + "     , F0503.JSSANGKYERFAMT";   // 2014.12.16 KJW - 상계환급금(정산)
            sql += Environment.NewLine + "     , F0503.JSSANGKYEADDAMT";   // 2014.12.16 KJW - 상계추가부담금
            sql += Environment.NewLine + "     , F0503.SKBAKAMT";
            sql += Environment.NewLine + "     , F0503.JSBAKAMT";
            sql += Environment.NewLine + "     , F0501.DEMNO";
            sql += Environment.NewLine + "  FROM TIE_F0503 F0503 INNER JOIN TIE_F0501 F0501 ON F0501.JSDEMSEQ=F0503.JSDEMSEQ AND F0501.JSREDAY=F0503.JSREDAY AND F0501.CNECNO=F0503.CNECNO AND F0501.DCOUNT=F0503.DCOUNT";

            if (cboDQOption.SelectedIndex == 1)
            {
                // 청구번호별
                sql += Environment.NewLine + " WHERE F0501.DEMNO = '" + p_demno + "' ";
            }
            else if (cboDQOption.SelectedIndex == 2)
            {
                // 청구월별
                sql += Environment.NewLine + " WHERE F0501.DEMNO LIKE '" + p_demno.Substring(0, 6) + "%' ";
            }
            else if (cboDQOption.SelectedIndex == 3)
            {
                // 청구년도별
                sql += Environment.NewLine + " WHERE F0501.DEMNO LIKE '" + p_demno.Substring(0, 4) + "%' ";
            }
            else
            {
                sql += Environment.NewLine + " WHERE F0503.JSDEMSEQ = '" + p_jsdemseq + "'";
                sql += Environment.NewLine + "   AND F0503.JSREDAY  = '" + p_jsreday + "'";
                sql += Environment.NewLine + "   AND F0503.CNECNO   = '" + p_cnecno + "'";
                sql += Environment.NewLine + "   AND F0503.DCOUNT   = '" + p_dcount + "'";
                sql += Environment.NewLine + "   AND F0503.JSYYSEQ  = '" + p_jsyyseq + "'";
            }
            
            sql += Environment.NewLine + " ORDER BY F0503.JSDEMSEQ, F0503.JSREDAY, F0503.CNECNO, F0503.DCOUNT, F0503.JSYYSEQ, F0503.JSSEQNO, F0503.EPRTNO";

            long no = 0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CDataPtnt data = new CDataPtnt();
                data.Clear();

                data.NO = (++no);

                data.JSDEMSEQ = reader["JSDEMSEQ"].ToString().TrimEnd();   // 정산통보번호
                data.JSREDAY = reader["JSREDAY"].ToString().TrimEnd();   // 정산통보일자
                data.CNECNO = reader["CNECNO"].ToString().TrimEnd();   // 접수번호
                data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd();   // 청구서 일련번호
                data.JSYYSEQ = reader["JSYYSEQ"].ToString().TrimEnd();   // 정산연번
                data.JSSEQNO = reader["JSSEQNO"].ToString().TrimEnd();   // 정산일련번호
                data.EPRTNO = reader["EPRTNO"].ToString().TrimEnd();   // 명세서 일련번호
                data.PNM = reader["PNM"].ToString().TrimEnd();   // 수진자성명
                data.RESID = reader["RESID"].ToString().TrimEnd();   // 수진자주민등록번호
                data.INSNM = reader["INSNM"].ToString().TrimEnd();   // 세대주 성명
                data.UNICD = reader["UNICD"].ToString().TrimEnd();   // 보장기관기호
                data.JRHT = reader["JRHT"].ToString().TrimEnd();   // 진료형태
                data.JBFG = reader["JBFG"].ToString().TrimEnd();   // 의료급여종별구분
                data.GUBUN = reader["GUBUN"].ToString().TrimEnd();   // 구분(1.환급 2.환수 3.환수+환급)
                data.INSID = reader["INSID"].ToString().TrimEnd();   // 증번호
                data.JAJR = reader["JAJR"].ToString().TrimEnd();   // 정액정률구분(0.정액 9.정률 1.장기요양정액수가 2.장기요양행위별수가)
                data.STEDT = reader["STEDT"].ToString().TrimEnd();   // 당월요양개시일
                data.SKTJKH = reader["SKTJKH"].ToString().TrimEnd();   // 이전심결사항-특정기호
                data.DACD = reader["DACD"].ToString().TrimEnd();   // 상병분류기호
                data.JJSOGE1 = MetroLib.StrHelper.ToLong(reader["JJSOGE1"].ToString().TrimEnd());   // 조정소계1
                data.JJSOGE2 = MetroLib.StrHelper.ToLong(reader["JJSOGE2"].ToString().TrimEnd());   // 조정소계2
                data.SKPMGUM = MetroLib.StrHelper.ToLong(reader["SKPMGUM"].ToString().TrimEnd());   // 이전심결사항-본인부담환급금
                data.SKPPGUM = MetroLib.StrHelper.ToLong(reader["SKPPGUM"].ToString().TrimEnd());   // 이전심결사항-본인추가부담금
                data.SKTTAMT = MetroLib.StrHelper.ToLong(reader["SKTTAMT"].ToString().TrimEnd());   // 이전심결사항-의료급여비용총액
                data.SKPTAMT = MetroLib.StrHelper.ToLong(reader["SKPTAMT"].ToString().TrimEnd());   // 이전심결사항-본인부담금
                data.SKRELAM = MetroLib.StrHelper.ToLong(reader["SKRELAM"].ToString().TrimEnd());   // 이전심결사항-대불금
                data.SKJAM = MetroLib.StrHelper.ToLong(reader["SKJAM"].ToString().TrimEnd());   // 이전심결사항-장애인의료비
                data.SKUNAMT = MetroLib.StrHelper.ToLong(reader["SKUNAMT"].ToString().TrimEnd());   // 이전심결사항-보장기관부담금
                data.SKTTTAMT = MetroLib.StrHelper.ToLong(reader["SKTTTAMT"].ToString().TrimEnd());   // 이전심결사항-진료비총액
                data.SKBHUNAMT = MetroLib.StrHelper.ToLong(reader["SKBHUNAMT"].ToString().TrimEnd());   // 이전심결사항-보훈청구액
                data.SKRSTAMT = MetroLib.StrHelper.ToLong(reader["SKRSTAMT"].ToString().TrimEnd());   // 이전심결사항-심사결정액
                data.SKSUTAKAMT = MetroLib.StrHelper.ToLong(reader["SKSUTAKAMT"].ToString().TrimEnd());   // 이전심결사항-위탁검사직접지급금
                data.SKEXAMC = MetroLib.StrHelper.ToLong(reader["SKEXAMC"].ToString().TrimEnd());   // 이전심결사항-입원일수(내원일수)
                data.SKORDDAYS = MetroLib.StrHelper.ToLong(reader["SKORDDAYS"].ToString().TrimEnd());   // 이전심결사항-요양급여일수
                data.SKORDCNT = MetroLib.StrHelper.ToLong(reader["SKORDCNT"].ToString().TrimEnd());   // 이전심결사항-처방횟수
                data.JSTJKH = reader["JSTJKH"].ToString().TrimEnd();   // 정산심결사항-특정기호
                data.JSPMGUM = MetroLib.StrHelper.ToLong(reader["JSPMGUM"].ToString().TrimEnd());   // 정산심결사항-본인부담환급금
                data.JSPPGUM = MetroLib.StrHelper.ToLong(reader["JSPPGUM"].ToString().TrimEnd());   // 정산심결사항-본인추가부담금
                data.JSTTAMT = MetroLib.StrHelper.ToLong(reader["JSTTAMT"].ToString().TrimEnd());   // 정산심결사항-의료급여비용총액
                data.JSPTAMT = MetroLib.StrHelper.ToLong(reader["JSPTAMT"].ToString().TrimEnd());   // 정산심결사항-본인부담금
                data.JSRELAM = MetroLib.StrHelper.ToLong(reader["JSRELAM"].ToString().TrimEnd());   // 정산심결사항-대불금
                data.JSJAM = MetroLib.StrHelper.ToLong(reader["JSJAM"].ToString().TrimEnd());   // 정산심결사항-장애인의료비
                data.JSUNAMT = MetroLib.StrHelper.ToLong(reader["JSUNAMT"].ToString().TrimEnd());   // 정산심결사항-보장기관부담금
                data.JSTTTAMT = MetroLib.StrHelper.ToLong(reader["JSTTTAMT"].ToString().TrimEnd());   // 정산심결사항-진료비총액
                data.JSBHUNAMT = MetroLib.StrHelper.ToLong(reader["JSBHUNAMT"].ToString().TrimEnd());   // 정산심결사항-보훈부담금
                data.JSRSTAMT = MetroLib.StrHelper.ToLong(reader["JSRSTAMT"].ToString().TrimEnd());   // 정산심결사항-심사결정액
                data.JSSUTAKAMT = MetroLib.StrHelper.ToLong(reader["JSSUTAKAMT"].ToString().TrimEnd());   // 정산심결사항-위탁검사직접지급금
                data.JSRSTCHAAMT = MetroLib.StrHelper.ToLong(reader["JSRSTCHAAMT"].ToString().TrimEnd());   // 정산심결사항-결정차액
                data.JSEXAMC = MetroLib.StrHelper.ToLong(reader["JSEXAMC"].ToString().TrimEnd());   // 정산심결사항-입원일수(내원일수)
                data.JSORDDAYS = MetroLib.StrHelper.ToLong(reader["JSORDDAYS"].ToString().TrimEnd());   // 정산심결사항-요양급여일수
                data.JSORDCNT = MetroLib.StrHelper.ToLong(reader["JSORDCNT"].ToString().TrimEnd());   // 정산심결사항-처방횟수
                data.MEMO = reader["MEMO"].ToString().TrimEnd();   // 명일련비고사항
                data.SKGONSGB = reader["SKGONSGB"].ToString().TrimEnd();
                data.SKSBRDNTYPE = reader["SKSBRDNTYPE"].ToString().TrimEnd();
                data.SKTSAMT = MetroLib.StrHelper.ToLong(reader["SKTSAMT"].ToString().TrimEnd());
                data.SK100AMT = MetroLib.StrHelper.ToLong(reader["SK100AMT"].ToString().TrimEnd());
                data.SKUISAMT = MetroLib.StrHelper.ToLong(reader["SKUISAMT"].ToString().TrimEnd());
                data.JSGONSGB = reader["JSGONSGB"].ToString().TrimEnd();
                data.JSSBRDNTYPE = reader["JSSBRDNTYPE"].ToString().TrimEnd();
                data.JSTSAMT = MetroLib.StrHelper.ToLong(reader["JSTSAMT"].ToString().TrimEnd());
                data.JS100AMT = MetroLib.StrHelper.ToLong(reader["JS100AMT"].ToString().TrimEnd());
                data.JSUISAMT = MetroLib.StrHelper.ToLong(reader["JSUISAMT"].ToString().TrimEnd());
                data.SKMKDRUGCNT = MetroLib.StrHelper.ToLong(reader["SKMKDRUGCNT"].ToString().TrimEnd());   // 이전심결사항-직접조제횟수
                data.JSMKDRUGCNT = MetroLib.StrHelper.ToLong(reader["JSMKDRUGCNT"].ToString().TrimEnd());   // 정산심결사항-직접조제횟수
                data.SKUPLMTCHATTAMT = MetroLib.StrHelper.ToLong(reader["SKUPLMTCHATTAMT"].ToString().TrimEnd());
                data.SKPTTTAMT = MetroLib.StrHelper.ToLong(reader["SKPTTTAMT"].ToString().TrimEnd());
                data.JSUPLMTCHATTAMT = MetroLib.StrHelper.ToLong(reader["JSUPLMTCHATTAMT"].ToString().TrimEnd());
                data.JSPTTTAMT = MetroLib.StrHelper.ToLong(reader["JSPTTTAMT"].ToString().TrimEnd());
                data.SKBHPMGUM = MetroLib.StrHelper.ToLong(reader["SKBHPMGUM"].ToString().TrimEnd());
                data.JSBHPMGUM = MetroLib.StrHelper.ToLong(reader["JSBHPMGUM"].ToString().TrimEnd());
                data.SKBHPPGUM = MetroLib.StrHelper.ToLong(reader["SKBHPPGUM"].ToString().TrimEnd());
                data.JSBHPPGUM = MetroLib.StrHelper.ToLong(reader["JSBHPPGUM"].ToString().TrimEnd());
                data.SKBHPTAMT = MetroLib.StrHelper.ToLong(reader["SKBHPTAMT"].ToString().TrimEnd());
                data.JSBHPTAMT = MetroLib.StrHelper.ToLong(reader["JSBHPTAMT"].ToString().TrimEnd());
                data.BAKDNSKPMGUM = MetroLib.StrHelper.ToLong(reader["BAKDNSKPMGUM"].ToString().TrimEnd());   // 2014.12.16 KJW - 100/100미만 본인부담금합계(이전)
                data.BAKDNSKPPGUM = MetroLib.StrHelper.ToLong(reader["BAKDNSKPPGUM"].ToString().TrimEnd());   // 2014.12.16 KJW - 100/100미만 본인추가부담금합계
                data.BAKDNSKTTAMT = MetroLib.StrHelper.ToLong(reader["BAKDNSKTTAMT"].ToString().TrimEnd());   // 2014.12.16 KJW - 100/100미만 총액합계
                data.BAKDNSKPTAMT = MetroLib.StrHelper.ToLong(reader["BAKDNSKPTAMT"].ToString().TrimEnd());   // 2014.12.16 KJW - 100/100미만 본인일부부담금합계
                data.BAKDNSKUNAMT = MetroLib.StrHelper.ToLong(reader["BAKDNSKUNAMT"].ToString().TrimEnd());   // 2014.12.16 KJW - 100/100미만 청구액합계
                data.BAKDNSKBHUNAMT = MetroLib.StrHelper.ToLong(reader["BAKDNSKBHUNAMT"].ToString().TrimEnd());   // 2014.12.16 KJW - 100/100미만 보훈청구액합계
                data.BAKDNJSPMGUM = MetroLib.StrHelper.ToLong(reader["BAKDNJSPMGUM"].ToString().TrimEnd());   // 2014.12.16 KJW - 100/100미만 본인부담금합계(정산)
                data.BAKDNJSPPGUM = MetroLib.StrHelper.ToLong(reader["BAKDNJSPPGUM"].ToString().TrimEnd());   // 2014.12.16 KJW - 100/100미만 본인추가부담금합계
                data.BAKDNJSTTAMT = MetroLib.StrHelper.ToLong(reader["BAKDNJSTTAMT"].ToString().TrimEnd());   // 2014.12.16 KJW - 100/100미만 총액합계
                data.BAKDNJSPTAMT = MetroLib.StrHelper.ToLong(reader["BAKDNJSPTAMT"].ToString().TrimEnd());   // 2014.12.16 KJW - 100/100미만 본인일부부담금합계
                data.BAKDNJSUNAMT = MetroLib.StrHelper.ToLong(reader["BAKDNJSUNAMT"].ToString().TrimEnd());   // 2014.12.16 KJW - 100/100미만 청구액합계
                data.BAKDNJSBHUNAMT = MetroLib.StrHelper.ToLong(reader["BAKDNJSBHUNAMT"].ToString().TrimEnd());   // 2014.12.16 KJW - 100/100미만 보훈청구액합계
                data.SKSANGKYERFAMT = MetroLib.StrHelper.ToLong(reader["SKSANGKYERFAMT"].ToString().TrimEnd());   // 2014.12.16 KJW - 상계환급금(이전)
                data.SKSANGKYEADDAMT = MetroLib.StrHelper.ToLong(reader["SKSANGKYEADDAMT"].ToString().TrimEnd());   // 2014.12.16 KJW - 상계추가부담금
                data.JSSANGKYERFAMT = MetroLib.StrHelper.ToLong(reader["JSSANGKYERFAMT"].ToString().TrimEnd());   // 2014.12.16 KJW - 상계환급금(정산)
                data.JSSANGKYEADDAMT = MetroLib.StrHelper.ToLong(reader["JSSANGKYEADDAMT"].ToString().TrimEnd());   // 2014.12.16 KJW - 상계추가부담금
                data.SKBAKAMT = MetroLib.StrHelper.ToLong(reader["SKBAKAMT"].ToString().TrimEnd());
                data.JSBAKAMT = MetroLib.StrHelper.ToLong(reader["JSBAKAMT"].ToString().TrimEnd());
                data.DEMNO = reader["DEMNO"].ToString().TrimEnd();

                list.Add(data);

                return true;
            });


            {
                // 조정소계1, 조정소계2 사용유보
                bool show1 = false;
                bool show2 = false;
                bool show3 = false;
                bool show4 = false;
                for (int row = 0; row < grdPtntView.RowCount; row++)
                {
                    if (show1 == false && MetroLib.StrHelper.ToLong(grdPtntView.GetRowCellValue(row, "JJSOGE1").ToString()) != 0) show1 = true;
                    if (show2 == false && MetroLib.StrHelper.ToLong(grdPtntView.GetRowCellValue(row, "JJSOGE2").ToString()) != 0) show2 = true;
                    if (show3 == false && MetroLib.StrHelper.ToLong(grdPtntView.GetRowCellValue(row, "JJSOGE1X").ToString()) != 0) show3 = true;
                    if (show4 == false && MetroLib.StrHelper.ToLong(grdPtntView.GetRowCellValue(row, "JJSOGE2X").ToString()) != 0) show4 = true;

                    if (show1 == true && show2 == true && show3 == true && show4 == true) break;
                }
                grdPtntView.Columns["JJSOGE1"].Visible = show1 || show2 || show3 || show4;
                grdPtntView.Columns["JJSOGE2"].Visible = show1 || show2 || show3 || show4;
                grdPtntView.Columns["JJSOGE1X"].Visible = show1 || show2 || show3 || show4;
                grdPtntView.Columns["JJSOGE2X"].Visible = show1 || show2 || show3 || show4;
            }

            string[] check_field1 = {

                // 심평원에서 삭제했거나 사용 보류시킨 컬럼                            
                "SKSUTAKAMT","JSSUTAKAMT", // 위탁검사직접지급금
                "SKUPLMTCHATTAMT","JSUPLMTCHATTAMT", // 약제상한차액
                "SKPTTTAMT","JSPTTTAMT", // 수진자총액

                // 보훈관련 컬럼
                "SKBHPMGUM","JSBHPMGUM", // 보훈본인부담환급금
                "SKBHPPGUM","JSBHPPGUM", // 보훈본인추가부담금
                "SKBHUNAMT","JSBHUNAMT", // 보훈청구액
                "SKBHPTAMT","JSBHPTAMT", // 보훈본인일부부담액
                "BAKDNSKBHUNAMT","BAKDNJSBHUNAMT", // 100미만보훈청구액
                "SK100AMT", "JS100AMT", // 보훈100분의100본인부담총액

                // 100미만 컬럼
                "BAKDNSKPMGUM","BAKDNJSPMGUM",
                "BAKDNSKPPGUM","BAKDNJSPPGUM",
                "BAKDNSKTTAMT","BAKDNJSTTAMT",
                "BAKDNSKPTAMT","BAKDNJSPTAMT",
                "BAKDNSKUNAMT","BAKDNJSUNAMT", // 100미만 청구액

                // 100총액
                "SKBAKAMT","JSBAKAMT", // 100총액

                // 기타
                "SKPMGUM","JSPMGUM", // 본인부담환급금
                "SKSANGKYERFAMT","JSSANGKYERFAMT", // 상계환급금
                "SKPPGUM","JSPPGUM", // 본인추가부담금
                "SKSANGKYEADDAMT","JSSANGKYEADDAMT", // 상계추가부담금
                "SKRELAM","JSRELAM", // 대불금
                "SKJAM","JSJAM", // 장애인의료비
                "SKUISAMT", "JSUISAMT", // 비급여총액
                "SKTSAMT", "JSTSAMT", // 특수장비총액
                "SKORDCNT","JSORDCNT", // 처방횟수
                "SKMKDRUGCNT","JSMKDRUGCNT", // 직접조제횟수
            };

            DxLib.GridHelper.HideColumnIfZeroInBand(check_field1, grdPtntView);
            DxLib.GridHelper.SetBandVisible(grdPtntView);
        }

        /*
        private void SetBandVisible(DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView view)
        {
            // band에 속해있는 column을 구한다.
            Dictionary<string, string> dicBand = new Dictionary<string, string>();
            foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand band in view.Bands)
            {
                if (band.Children.Count > 0)
                {
                    foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand band2 in band.Children)
                    {
                        if (band2.Columns.Count > 0)
                        {
                            for (int j = 0; j < band2.Columns.Count; j++)
                            {
                                if (dicBand.ContainsKey(band.Name) == true)
                                {
                                    dicBand[band.Name] = dicBand[band.Name] + "," + band2.Columns[j].FieldName;
                                }
                                else
                                {
                                    dicBand[band.Name] = band2.Columns[j].FieldName;
                                }
                                if (dicBand.ContainsKey(band2.Name) == true)
                                {
                                    dicBand[band2.Name] = dicBand[band2.Name] + "," + band2.Columns[j].FieldName;
                                }
                                else
                                {
                                    dicBand[band2.Name] = band2.Columns[j].FieldName;
                                }
                            }
                        }
                    }
                }
                if (band.Columns.Count > 0)
                {
                    for (int j = 0; j < band.Columns.Count; j++)
                    {
                        if (dicBand.ContainsKey(band.Name) == true)
                        {
                            dicBand[band.Name] = dicBand[band.Name] + "," + band.Columns[j].FieldName;
                        }
                        else
                        {
                            dicBand[band.Name] = band.Columns[j].FieldName;
                        }
                    }
                }
            }
            // band에 속해있는 column이 visible = false이면 band도 visible = false임.
            foreach (KeyValuePair<string, string> kv in dicBand)
            {
                string[] fields = kv.Value.Split(',');
                bool show = false;
                foreach (string field in fields)
                {
                    if (field != "" && view.Columns[field].Visible == true) show = true;
                }
                if (view.Bands[kv.Key] != null) view.Bands[kv.Key].Visible = show;
            }
        }
        */

        private void QueryCode(string p_jsdemseq, string p_jsreday, string p_cnecno, string p_dcount, string p_jsyyseq, string p_demno, OleDbConnection p_conn)
        {
            List<CDataCode> list = new List<CDataCode>();
            grdCode.DataSource = null;
            grdCode.DataSource = list;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT F0504.JSDEMSEQ"; // 정산통보번호
            sql += Environment.NewLine + "     , F0504.JSREDAY"; // 정산통보일자
            sql += Environment.NewLine + "     , F0504.CNECNO"; // 접수번호
            sql += Environment.NewLine + "     , F0504.DCOUNT"; // 청구서 일련번호
            sql += Environment.NewLine + "     , F0504.JSYYSEQ"; // 정산연번
            sql += Environment.NewLine + "     , F0504.JSSEQNO"; // 정산일련번호
            sql += Environment.NewLine + "     , F0504.EPRTNO"; // 명세서 일련번호
            sql += Environment.NewLine + "     , F0504.LNO"; // 줄번호
            sql += Environment.NewLine + "     , F0504.SKJJRMK"; // 이전심결사항-조정사유
            sql += Environment.NewLine + "     , F0504.SKJJRMK2"; // 이전심결사항-조정사유사세
            sql += Environment.NewLine + "     , F0504.SKGUBUN"; // 이전심결사항-1,2구분
            sql += Environment.NewLine + "     , F0504.SKBGIHO"; // 이전심결사항-코드
            sql += Environment.NewLine + "     , F0504.SKDANGA"; // 이전심결사항-단가
            sql += Environment.NewLine + "     , F0504.SKDQTY"; // 이전심결사항-일투 인정횟수
            sql += Environment.NewLine + "     , F0504.SKDDAY"; // 이전심결사항-총투 인정횟수
            sql += Environment.NewLine + "     , F0504.SKIJAMT"; // 이전심결사항-인정금액
            sql += Environment.NewLine + "     , F0504.SKJJAMT"; // 이전심결사항-조정금액
            sql += Environment.NewLine + "     , F0504.SKJJSAU"; // 이전심결사항-관련근거
            sql += Environment.NewLine + "     , F0504.JSJJRMK"; // 정산심결사항-조정사유
            sql += Environment.NewLine + "     , F0504.JSJJRMK2"; // 정산심결사항-조정사유
            sql += Environment.NewLine + "     , F0504.JSBGIHO"; // 정산심결사항-코드
            sql += Environment.NewLine + "     , F0504.JSDANGA"; // 정산심결사항-단가
            sql += Environment.NewLine + "     , F0504.JSDQTY"; // 정산심결사항-일투 인정횟수
            sql += Environment.NewLine + "     , F0504.JSDDAY"; // 정산심결사항-총투 인정횟수
            sql += Environment.NewLine + "     , F0504.JSIJAMT"; // 정산심결사항-인정금액
            sql += Environment.NewLine + "     , F0504.JSJJAMT"; // 정산심결사항-조정금액
            sql += Environment.NewLine + "     , F0504.JSMEMO"; // 정산심결사항-비고(조정사유내역)
            sql += Environment.NewLine + "     , F0503.PNM"; // 수진자명
            sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE = F0504.SKBGIHO) AS SKBGIHONM";
            sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE = F0504.JSBGIHO) AS JSBGIHONM";
            sql += Environment.NewLine + "     , F0504.SKCNTQTY";
            sql += Environment.NewLine + "     , F0504.JSCNTQTY";
            sql += Environment.NewLine + "     , F0504.SKUPLMTAMT";
            sql += Environment.NewLine + "     , F0504.SKIJUPLMTCHAAMT";
            sql += Environment.NewLine + "     , F0504.SKJJUPLMTCHAAMT";
            sql += Environment.NewLine + "     , F0504.JSUPLMTAMT";
            sql += Environment.NewLine + "     , F0504.JSIJUPLMTCHAAMT";
            sql += Environment.NewLine + "     , F0504.JSJJUPLMTCHAAMT";
            sql += Environment.NewLine + "     , F0504.HANGNO";
            sql += Environment.NewLine + "     , F0501.DEMNO";
            sql += Environment.NewLine + "  FROM TIE_F0504 F0504 INNER JOIN TIE_F0503 F0503 ON F0503.JSDEMSEQ=F0504.JSDEMSEQ AND F0503.JSREDAY=F0504.JSREDAY AND F0503.CNECNO=F0504.CNECNO AND F0503.DCOUNT=F0504.DCOUNT AND F0503.JSYYSEQ=F0504.JSYYSEQ AND F0503.EPRTNO=F0504.EPRTNO";
            sql += Environment.NewLine + "                       INNER JOIN TIE_F0501 F0501 ON F0501.JSDEMSEQ=F0504.JSDEMSEQ AND F0501.JSREDAY=F0504.JSREDAY AND F0501.CNECNO=F0504.CNECNO AND F0501.DCOUNT=F0504.DCOUNT";

            if (cboDQOption.SelectedIndex == 1)
            {
                // 청구번호별
                sql += Environment.NewLine + " WHERE F0501.DEMNO = '" + p_demno + "'";
            }
            else if (cboDQOption.SelectedIndex == 2)
            {
                // 청구월별
                sql += Environment.NewLine + " WHERE F0501.DEMNO LIKE '" + p_demno.Substring(0, 6) + "%'";
            }
            else if (cboDQOption.SelectedIndex == 3)
            {
                // 청구년도별
                sql += Environment.NewLine + " WHERE F0501.DEMNO LIKE '" + p_demno.Substring(0, 4) + "%'";
            }
            else
            {
                sql += Environment.NewLine + " WHERE F0504.JSDEMSEQ = '" + p_jsdemseq + "'";
                sql += Environment.NewLine + "   AND F0504.JSREDAY  = '" + p_jsreday + "'";
                sql += Environment.NewLine + "   AND F0504.CNECNO   = '" + p_cnecno + "'";
                sql += Environment.NewLine + "   AND F0504.DCOUNT   = '" + p_dcount + "'";
                sql += Environment.NewLine + "   AND F0504.JSYYSEQ  = '" + p_jsyyseq + "'";
            }

            sql += Environment.NewLine + " ORDER BY F0504.JSDEMSEQ,F0504.JSREDAY,F0504.CNECNO,F0504.DCOUNT,F0504.JSYYSEQ,F0504.JSSEQNO,F0504.EPRTNO,F0504.LNO";

            long no = 0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CDataCode data = new CDataCode();
                data.Clear();

                data.NO = (++no);

                data.JSDEMSEQ = reader["JSDEMSEQ"].ToString().TrimEnd(); // 정산통보번호
                data.JSREDAY = reader["JSREDAY"].ToString().TrimEnd(); // 정산통보일자
                data.CNECNO = reader["CNECNO"].ToString().TrimEnd(); // 접수번호
                data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd(); // 청구서 일련번호
                data.JSYYSEQ = reader["JSYYSEQ"].ToString().TrimEnd(); // 정산연번
                data.JSSEQNO = reader["JSSEQNO"].ToString().TrimEnd(); // 정산일련번호
                data.EPRTNO = reader["EPRTNO"].ToString().TrimEnd(); // 명세서 일련번호
                data.LNO = reader["LNO"].ToString().TrimEnd(); // 줄번호
                data.SKJJRMK = reader["SKJJRMK"].ToString().TrimEnd(); // 이전심결사항-조정사유
                data.SKJJRMK2 = reader["SKJJRMK2"].ToString().TrimEnd(); // 이전심결사항-조정사유사세
                data.SKGUBUN = reader["SKGUBUN"].ToString().TrimEnd(); // 이전심결사항-1,2구분
                data.SKBGIHO = reader["SKBGIHO"].ToString().TrimEnd(); // 이전심결사항-코드
                data.SKDANGA = MetroLib.StrHelper.ToLong(reader["SKDANGA"].ToString().TrimEnd()); // 이전심결사항-단가
                data.SKDQTY = MetroLib.StrHelper.ToDouble(reader["SKDQTY"].ToString().TrimEnd()); // 이전심결사항-일투 인정횟수
                data.SKDDAY = MetroLib.StrHelper.ToLong(reader["SKDDAY"].ToString().TrimEnd()); // 이전심결사항-총투 인정횟수
                data.SKIJAMT = MetroLib.StrHelper.ToLong(reader["SKIJAMT"].ToString().TrimEnd()); // 이전심결사항-인정금액
                data.SKJJAMT = MetroLib.StrHelper.ToLong(reader["SKJJAMT"].ToString().TrimEnd()); // 이전심결사항-조정금액
                data.SKJJSAU = reader["SKJJSAU"].ToString().TrimEnd(); // 이전심결사항-관련근거
                data.JSJJRMK = reader["JSJJRMK"].ToString().TrimEnd(); // 정산심결사항-조정사유
                data.JSJJRMK2 = reader["JSJJRMK2"].ToString().TrimEnd(); // 정산심결사항-조정사유
                data.JSBGIHO = reader["JSBGIHO"].ToString().TrimEnd(); // 정산심결사항-코드
                data.JSDANGA = MetroLib.StrHelper.ToLong(reader["JSDANGA"].ToString().TrimEnd()); // 정산심결사항-단가
                data.JSDQTY = MetroLib.StrHelper.ToDouble(reader["JSDQTY"].ToString().TrimEnd()); // 정산심결사항-일투 인정횟수
                data.JSDDAY = MetroLib.StrHelper.ToLong(reader["JSDDAY"].ToString().TrimEnd()); // 정산심결사항-총투 인정횟수
                data.JSIJAMT = MetroLib.StrHelper.ToLong(reader["JSIJAMT"].ToString().TrimEnd()); // 정산심결사항-인정금액
                data.JSJJAMT = MetroLib.StrHelper.ToLong(reader["JSJJAMT"].ToString().TrimEnd()); // 정산심결사항-조정금액
                data.JSMEMO = reader["JSMEMO"].ToString().TrimEnd(); // 정산심결사항-비고(조정사유내역)
                data.PNM = reader["PNM"].ToString().TrimEnd(); // 수진자명
                data.SKBGIHONM = reader["SKBGIHONM"].ToString().TrimEnd();
                data.JSBGIHONM = reader["JSBGIHONM"].ToString().TrimEnd();
                data.SKCNTQTY = MetroLib.StrHelper.ToDouble(reader["SKCNTQTY"].ToString().TrimEnd());
                data.JSCNTQTY = MetroLib.StrHelper.ToDouble(reader["JSCNTQTY"].ToString().TrimEnd());
                data.SKUPLMTAMT = MetroLib.StrHelper.ToLong(reader["SKUPLMTAMT"].ToString().TrimEnd());
                data.SKIJUPLMTCHAAMT = MetroLib.StrHelper.ToLong(reader["SKIJUPLMTCHAAMT"].ToString().TrimEnd());
                data.SKJJUPLMTCHAAMT = MetroLib.StrHelper.ToLong(reader["SKJJUPLMTCHAAMT"].ToString().TrimEnd());
                data.JSUPLMTAMT = MetroLib.StrHelper.ToLong(reader["JSUPLMTAMT"].ToString().TrimEnd());
                data.JSIJUPLMTCHAAMT = MetroLib.StrHelper.ToLong(reader["JSIJUPLMTCHAAMT"].ToString().TrimEnd());
                data.JSJJUPLMTCHAAMT = MetroLib.StrHelper.ToLong(reader["JSJJUPLMTCHAAMT"].ToString().TrimEnd());
                data.HANGNO = reader["HANGNO"].ToString().TrimEnd();
                data.DEMNO = reader["DEMNO"].ToString().TrimEnd();

                list.Add(data);

                return true;
            });

            string[] check_field1 = {

                // 심평원에서 삭제했거나 사용 보류시킨 컬럼                            
                "SKUPLMTAMT","JSUPLMTAMT", // 약제상한차액
                "SKJJUPLMTCHAAMT","JSJJUPLMTCHAAMT", // 약제상한차액조정금액
                "SKIJUPLMTCHAAMT","JSIJUPLMTCHAAMT", // 약제상한차액인정금액

            };
            DxLib.GridHelper.HideColumnIfZeroInBand(check_field1, grdCodeView);
            DxLib.GridHelper.SetBandVisible(grdCodeView);

        }

        private void QueryBonrt(string p_jsdemseq, string p_jsreday, string p_cnecno, string p_dcount, string p_jsyyseq, string p_demno, OleDbConnection p_conn)
        {
            List<CDataBonrt> list = new List<CDataBonrt>();
            grdBonrt.DataSource = null;
            grdBonrt.DataSource = list;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT F0506.JSDEMSEQ"; // 정산통보번호
            sql += Environment.NewLine + "     , F0506.JSREDAY"; // 정산통보일자
            sql += Environment.NewLine + "     , F0506.CNECNO"; // 접수번호
            sql += Environment.NewLine + "     , F0506.DCOUNT"; // 청구서 일련번호
            sql += Environment.NewLine + "     , F0506.JSYYSEQ"; // 정산연번
            sql += Environment.NewLine + "     , F0506.JSSEQNO"; // 정산일련번호
            sql += Environment.NewLine + "     , F0506.EPRTNO"; // 명세서 일련번호
            sql += Environment.NewLine + "     , F0506.LNO"; // 줄번호
            sql += Environment.NewLine + "     , F0506.SKJKRTBKRMK"; // 이전심결사항-본인부담률변경사유
            sql += Environment.NewLine + "     , F0506.SKHANGNO"; // 이전심결사항-항번호
            sql += Environment.NewLine + "     , F0506.SKBGIHO"; // 이전심결사항-코드
            sql += Environment.NewLine + "     , F0506.SKDANGA"; // 이전심결사항-단가
            sql += Environment.NewLine + "     , F0506.SKCNT"; // 이전심결사항-1회투약 인정횟수
            sql += Environment.NewLine + "     , F0506.SKDQTY"; // 이전심결사항-일투 인정횟수
            sql += Environment.NewLine + "     , F0506.SKDDAY"; // 이전심결사항-총투 인정횟수
            sql += Environment.NewLine + "     , F0506.SKJKRTIJAMT"; // 이전심결사항-본인부담률변경 인정금액
            sql += Environment.NewLine + "     , F0506.JSJKRTBKRMK"; // 정산심결사항-본인부담률변경사유
            sql += Environment.NewLine + "     , F0506.JSHANGNO"; // 정산심결사항-항번호
            sql += Environment.NewLine + "     , F0506.JSBGIHO"; // 정산심결사항-코드
            sql += Environment.NewLine + "     , F0506.JSDANGA"; // 정산심결사항-단가
            sql += Environment.NewLine + "     , F0506.JSCNT"; // 정산심결사항-1회투약 인정횟수
            sql += Environment.NewLine + "     , F0506.JSDQTY"; // 정산심결사항-일투 인정횟수
            sql += Environment.NewLine + "     , F0506.JSDDAY"; // 정산심결사항-총투 인정횟수
            sql += Environment.NewLine + "     , F0506.JSJKRTIJAMT"; // 정산심결사항-본인부담률변경 인정금액
            sql += Environment.NewLine + "     , F0506.MEMO"; // 정산심결사항-비고(본인부담률변경내역)
            sql += Environment.NewLine + "     , F0503.PNM"; // 수진자명
            sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE = F0506.SKBGIHO) AS SKBGIHONM";
            sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE = F0506.JSBGIHO) AS JSBGIHONM";
            sql += Environment.NewLine + "     , F0501.DEMNO";
            sql += Environment.NewLine + "  FROM TIE_F0506 F0506 INNER JOIN TIE_F0503 F0503 ON F0503.JSDEMSEQ=F0506.JSDEMSEQ AND F0503.JSREDAY=F0506.JSREDAY AND F0503.CNECNO=F0506.CNECNO AND F0503.DCOUNT=F0506.DCOUNT AND F0503.JSYYSEQ=F0506.JSYYSEQ AND F0503.JSSEQNO=F0506.JSSEQNO AND F0503.EPRTNO=F0506.EPRTNO";
            sql += Environment.NewLine + "                       INNER JOIN TIE_F0501 F0501 ON F0501.JSDEMSEQ=F0506.JSDEMSEQ AND F0501.JSREDAY=F0506.JSREDAY AND F0501.CNECNO=F0506.CNECNO AND F0501.DCOUNT=F0506.DCOUNT";

            if (cboDQOption.SelectedIndex == 1)
            {
                // 청구번호별
                sql += Environment.NewLine + " WHERE F0501.DEMNO = '" + p_demno + "'";
            }
            else if (cboDQOption.SelectedIndex == 2)
            {
                // 청구월별
                sql += Environment.NewLine + " WHERE F0501.DEMNO LIKE '" + p_demno.Substring(0, 6) + "%'";
            }
            else if (cboDQOption.SelectedIndex == 3)
            {
                // 청구년도별
                sql += Environment.NewLine + " WHERE F0501.DEMNO LIKE '" + p_demno.Substring(0, 4) + "%'";
            }
            else
            {
                sql += Environment.NewLine + " WHERE F0506.JSDEMSEQ = '" + p_jsdemseq + "'";
                sql += Environment.NewLine + "   AND F0506.JSREDAY  = '" + p_jsreday + "'";
                sql += Environment.NewLine + "   AND F0506.CNECNO   = '" + p_cnecno + "'";
                sql += Environment.NewLine + "   AND F0506.DCOUNT   = '" + p_dcount + "'";
                sql += Environment.NewLine + "   AND F0506.JSYYSEQ  = '" + p_jsyyseq + "'";
            }

            sql += Environment.NewLine + " ORDER BY F0506.JSDEMSEQ,F0506.JSREDAY,F0506.CNECNO,F0506.DCOUNT,F0506.JSYYSEQ,F0506.JSSEQNO,F0506.EPRTNO,F0506.LNO";

            long no = 0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CDataBonrt data = new CDataBonrt();
                data.Clear();

                data.NO = (++no);

                data.JSDEMSEQ = reader["JSDEMSEQ"].ToString().TrimEnd(); // 정산통보번호
                data.JSREDAY = reader["JSREDAY"].ToString().TrimEnd(); // 정산통보일자
                data.CNECNO = reader["CNECNO"].ToString().TrimEnd(); // 접수번호
                data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd(); // 청구서 일련번호
                data.JSYYSEQ = reader["JSYYSEQ"].ToString().TrimEnd(); // 정산연번
                data.JSSEQNO = reader["JSSEQNO"].ToString().TrimEnd(); // 정산일련번호
                data.EPRTNO = reader["EPRTNO"].ToString().TrimEnd(); // 명세서 일련번호
                data.LNO = reader["LNO"].ToString().TrimEnd(); // 줄번호
                data.SKJKRTBKRMK = reader["SKJKRTBKRMK"].ToString().TrimEnd(); // 이전심결사항-본인부담률변경사유
                data.SKHANGNO = reader["SKHANGNO"].ToString().TrimEnd(); // 이전심결사항-항번호
                data.SKBGIHO = reader["SKBGIHO"].ToString().TrimEnd(); // 이전심결사항-코드
                data.SKDANGA = MetroLib.StrHelper.ToLong(reader["SKDANGA"].ToString().TrimEnd()); // 이전심결사항-단가
                data.SKCNT = MetroLib.StrHelper.ToDouble(reader["SKCNT"].ToString().TrimEnd()); // 이전심결사항-1회투약 인정횟수
                data.SKDQTY = MetroLib.StrHelper.ToDouble(reader["SKDQTY"].ToString().TrimEnd()); // 이전심결사항-일투 인정횟수
                data.SKDDAY = MetroLib.StrHelper.ToLong(reader["SKDDAY"].ToString().TrimEnd()); // 이전심결사항-총투 인정횟수
                data.SKJKRTIJAMT = MetroLib.StrHelper.ToLong(reader["SKJKRTIJAMT"].ToString().TrimEnd()); // 이전심결사항-본인부담률변경 인정금액
                data.JSJKRTBKRMK = reader["JSJKRTBKRMK"].ToString().TrimEnd(); // 정산심결사항-본인부담률변경사유
                data.JSHANGNO = reader["JSHANGNO"].ToString().TrimEnd(); // 정산심결사항-항번호
                data.JSBGIHO = reader["JSBGIHO"].ToString().TrimEnd(); // 정산심결사항-코드
                data.JSDANGA = MetroLib.StrHelper.ToLong(reader["JSDANGA"].ToString().TrimEnd()); // 정산심결사항-단가
                data.JSCNT = MetroLib.StrHelper.ToDouble(reader["JSCNT"].ToString().TrimEnd()); // 정산심결사항-1회투약 인정횟수
                data.JSDQTY = MetroLib.StrHelper.ToDouble(reader["JSDQTY"].ToString().TrimEnd()); // 정산심결사항-일투 인정횟수
                data.JSDDAY = MetroLib.StrHelper.ToLong(reader["JSDDAY"].ToString().TrimEnd()); // 정산심결사항-총투 인정횟수
                data.JSJKRTIJAMT = MetroLib.StrHelper.ToLong(reader["JSJKRTIJAMT"].ToString().TrimEnd()); // 정산심결사항-본인부담률변경 인정금액
                data.MEMO = reader["MEMO"].ToString().TrimEnd(); // 정산심결사항-비고(본인부담률변경내역)
                data.PNM = reader["PNM"].ToString().TrimEnd(); // 수진자명
                data.SKBGIHONM = reader["SKBGIHONM"].ToString().TrimEnd();
                data.JSBGIHONM = reader["JSBGIHONM"].ToString().TrimEnd();
                data.DEMNO = reader["DEMNO"].ToString().TrimEnd();

                list.Add(data);

                return true;
            });
        }

        private void QuerySutak(string p_jsdemseq, string p_jsreday, string p_cnecno, string p_dcount, string p_jsyyseq, string p_demno, OleDbConnection p_conn)
        {
            List<CDataSutak> list = new List<CDataSutak>();
            grdSutak.DataSource = null;
            grdSutak.DataSource = list;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT F0505.JSDEMSEQ"; // 정산통보번호
            sql += Environment.NewLine + "     , F0505.JSREDAY"; // 정산통보일자
            sql += Environment.NewLine + "     , F0505.CNECNO"; // 접수번호
            sql += Environment.NewLine + "     , F0505.DCOUNT"; // 청구서 일련번호
            sql += Environment.NewLine + "     , F0505.JSYYSEQ"; // 정산연번
            sql += Environment.NewLine + "     , F0505.JSSEQNO"; // 정산일련번호
            sql += Environment.NewLine + "     , F0505.EPRTNO"; // 명세서 일련번호
            sql += Environment.NewLine + "     , F0505.LNO"; // 줄번호
            sql += Environment.NewLine + "     , F0505.SUTAKID"; // 수탁기관기호
            sql += Environment.NewLine + "     , F0505.SUTAKAMT"; // 위탁검사직접지급금
            sql += Environment.NewLine + "     , F0505.OPRCD"; // 처리코드(미사용)
            sql += Environment.NewLine + "     , F0505.MEMO"; // 비고사항
            sql += Environment.NewLine + "     , F0501.DEMNO";
            sql += Environment.NewLine + "  FROM TIE_F0505 F0505 INNER JOIN TIE_F0501 F0501 ON F0501.JSDEMSEQ=F0505.JSDEMSEQ AND F0501.JSREDAY=F0505.JSREDAY AND F0501.CNECNO=F0505.CNECNO AND F0501.DCOUNT=F0505.DCOUNT";

            if (cboDQOption.SelectedIndex == 1)
            {
                // 청구번호별
                sql += Environment.NewLine + " WHERE F0501.DEMNO = '" + p_demno + "'";
            }
            else if (cboDQOption.SelectedIndex == 2)
            {
                // 청구월별
                sql += Environment.NewLine + " WHERE F0501.DEMNO LIKE '" + p_demno.Substring(0, 6) + "%'";
            }
            else if (cboDQOption.SelectedIndex == 3)
            {
                // 청구년도별
                sql += Environment.NewLine + " WHERE F0501.DEMNO LIKE '" + p_demno.Substring(0, 4) + "%'";
            }
            else
            {
                sql += Environment.NewLine + " WHERE F0505.JSDEMSEQ = '" + p_jsdemseq + "'";
                sql += Environment.NewLine + "   AND F0505.JSREDAY  = '" + p_jsreday + "'";
                sql += Environment.NewLine + "   AND F0505.CNECNO   = '" + p_cnecno + "'";
                sql += Environment.NewLine + "   AND F0505.DCOUNT   = '" + p_dcount + "'";
                sql += Environment.NewLine + "   AND F0505.JSYYSEQ  = '" + p_jsyyseq + "'";
            }

            sql += Environment.NewLine + " ORDER BY F0505.JSDEMSEQ, F0505.JSREDAY, F0505.CNECNO, F0505.DCOUNT, F0505.JSYYSEQ, F0505.JSSEQNO, F0505.EPRTNO, F0505.LNO";

            long no = 0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CDataSutak data = new CDataSutak();
                data.Clear();

                data.NO = (++no);

                data.JSDEMSEQ = reader["JSDEMSEQ"].ToString().TrimEnd(); // 정산통보번호
                data.JSREDAY = reader["JSREDAY"].ToString().TrimEnd(); // 정산통보일자
                data.CNECNO = reader["CNECNO"].ToString().TrimEnd(); // 접수번호
                data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd(); // 청구서 일련번호
                data.JSYYSEQ = reader["JSYYSEQ"].ToString().TrimEnd(); // 정산연번
                data.JSSEQNO = reader["JSSEQNO"].ToString().TrimEnd(); // 정산일련번호
                data.EPRTNO = reader["EPRTNO"].ToString().TrimEnd(); // 명세서 일련번호
                data.LNO = reader["LNO"].ToString().TrimEnd(); // 줄번호
                data.SUTAKID = reader["SUTAKID"].ToString().TrimEnd(); // 수탁기관기호
                data.SUTAKAMT = reader["SUTAKAMT"].ToString().TrimEnd(); // 위탁검사직접지급금
                data.OPRCD = reader["OPRCD"].ToString().TrimEnd(); // 처리코드(미사용)
                data.MEMO = reader["MEMO"].ToString().TrimEnd(); // 비고사항
                data.DEMNO = reader["DEMNO"].ToString().TrimEnd();

                list.Add(data);

                return true;
            });
        }

        private void grdMain_Load(object sender, EventArgs e)
        {
            // 컬럼면 "결정차액X"는 디자인 타임에 개발자가 뭔지 알게하려고 남겨놓고 실제로는 없는 필드이므로 이름을 지운다.
            string[] nullCaption = { "결정차액X" };
            ColumnNullCaption(grdPtntView, nullCaption);
            // 밴드명 변경
            string[] caption = {"본인부담환급금","본인부담","환급금",
                                "보훈본인부담환급금","보훈본인부담","환급금",
                                "100미만본인부담환급금","100미만본인부담","환급금",
                                "본인추가부담금","본인","추가부담금",
                                "보훈본인추가부담금","보훈본인","추가부담금",
                                "100미만본인추가부담금","100미만본인","추가부담금",
                                "상계추가부담금","상계","추가부담금",
                                "위탁검사직접지급금","위탁검사","직접지급금",
                                "보훈본인일부부담금","보훈","본인일부부담금",
                                "100미만총액","100미만","총액",
                                "100미만본인일부부담금","100미만","본인일부부담금",
                                "100미만청구액","100미만","청구액",
                                "100미만보훈청구액","100미만","보훈청구액",
                                "수급권확인대상건수","수급권확인","대상건수",
                               };
            DxLib.GridHelper.BandReCaption(grdMainView, caption);

            gridBand3.Visible = false;
            gridBand141.Visible = false;
            gridBand139.Visible = false;
            gridBand137.Visible = false;
            gridBand135.Visible = false;

            grdMainView.OptionsPrint.AutoWidth = false;
        }

        private void grdPtnt_Load(object sender, EventArgs e)
        {
            // 컬럼면 "결정차액X"는 디자인 타임에 개발자가 뭔지 알게하려고 남겨놓고 실제로는 없는 필드이므로 이름을 지운다.
            string[] nullCaption = { "결정차액X", "조정소계1X", "조정소계2X" };
            ColumnNullCaption(grdPtntView, nullCaption);

            // 밴드명 변경
            string[] caption = {"공상구분","공상","구분",
                                "본인부담구분코드","본인부담","구분코드",
                                "특정기호","특정","기호",
                                "본인부담환급금","본인부담","환급금",
                                "보훈본인부담환급금","보훈본인부담","환급금",
                                "100미만본인부담환급금","100미만본인부담","환급금",
                                "본인추가부담금","본인","추가부담금",
                                "보훈본인추가부담금","보훈본인","추가부담금",
                                "100미만본인추가부담금","100미만본인","추가부담금",
                                "상계추가부담금","상계","추가부담금",
                                "위탁검사직접지급금","위탁검사","직접지급금",
                                "보훈본인일부부담금","보훈","본인일부부담금",
                                "100미만총액","100미만","총액",
                                "100미만본인일부부담금","100미만","본인일부부담금",
                                "100미만청구액","100미만","청구액",
                                "100미만보훈청구액","100미만","보훈청구액",
                                "입내원일수","입내원","일수",
                                "의료급여일수","의료급여","일수",
                                "처방횟수","처방","횟수",
                                "직접조제횟수","직접조제","횟수",
                               };
            DxLib.GridHelper.BandReCaption(grdPtntView, caption);

            grdPtntView.OptionsPrint.AutoWidth = false;
        }

        private void ColumnNullCaption(DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView view, string[] caption)
        {
            foreach (string cap in caption)
            {
                for (int i = 0; i < view.Columns.Count; i++)
                {
                    if (view.Columns[i].Caption == cap)
                    {
                        view.Columns[i].Caption = " ";
                    }
                }
            }
        }

        /*
        private void BandReCaption(DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView view, string[] caption)
        {
            for (int i = 0; i < caption.Length; i += 3)
            {
                foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand band in view.Bands)
                {
                    if (band.Caption == caption[i])
                    {
                        band.Caption = caption[i + 1] + Environment.NewLine + caption[i + 2];
                    }
                    if (band.Children.Count >= 0)
                    {
                        foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand band2 in band.Children)
                        {
                            if (band2.Caption == caption[i])
                            {
                                band2.Caption = caption[i + 1] + Environment.NewLine + caption[i + 2];
                            }
                        }
                    }
                }
            }
        }
        */

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
            else if (tabControl1.SelectedIndex == 4)
            {
                printableComponentLink.Component = grdSutak;
            }
            
            printableComponentLink.ShowPreview();
        }
        private void printableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            String strTitle = "[보호]정산심사내역서";
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
                strCaption += "정산통보번호 : " + txtHeadJsdemseq.Text.ToString();
                strCaption += ", 정산통보일자 : " + txtHeadJsreday.Text.ToString();
                strCaption += ", 심사차수 : " + txtHeadDemseq.Text.ToString();
                strCaption += ", 접수번호 : " + txtHeadCnecno.Text.ToString();
                strCaption += ", 청구번호 : " + txtHeadDemno.Text.ToString();
                strCaption += ", 조정건수 : " + txtHeadJsskcnt.Text.ToString();
                strCaption += ", 조정금액 : " + txtHeadJsskamt.Text.ToString();
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
            e.Graph.DrawString("ADD0721E", Color.Black, new RectangleF(0, 0, 120, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(sysDate + " " + sysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void grdPtntView_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs ea = e as DevExpress.Utils.DXMouseEventArgs;
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = view.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {
                string colCaption = info.Column == null ? "N/A" : info.Column.GetCaption();
                MessageBox.Show(string.Format("DoubleClick on row: {0}, column: {1}.", info.RowHandle, colCaption));
            }
        }

        private void grdMainView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.RowHandle == DevExpress.XtraGrid.GridControl.InvalidRowHandle) return;
                string remark = grdMainView.GetRowCellValue(e.RowHandle, "JSREDEPT").ToString();
                remark += (remark != "" ? Environment.NewLine : "") + grdMainView.GetRowCellValue(e.RowHandle, "MEMO").ToString();
                txtMemo.Text = remark;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
    }
}
