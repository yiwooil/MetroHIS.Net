using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0720E
{
    public partial class ADD0720E : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        private int m_PrevTabIndex = -1;
        private string m_ErrPos = "";

        public ADD0720E()
        {
            InitializeComponent();
        }

        public ADD0720E(String user, String pwd, String prjcd, String addpara)
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

        private void ADD0720E_Load(object sender, EventArgs e)
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
                sql += Environment.NewLine + "SELECT F0401.VERSION"; // 버전구분
                sql += Environment.NewLine + "     , F0401.JSDEMSEQ"; // 정산심사차수
                sql += Environment.NewLine + "     , F0401.JSREDAY"; // 정산통보일자
                sql += Environment.NewLine + "     , F0401.CNECNO"; // 접수번호
                sql += Environment.NewLine + "     , F0401.DCOUNT"; // 청구서 일련번호
                sql += Environment.NewLine + "     , F0401.FMNO"; // 서식번호
                sql += Environment.NewLine + "     , F0401.HOSID"; // 요양기관 기호
                sql += Environment.NewLine + "     , F0401.JIWONCD"; // 지원코드
                sql += Environment.NewLine + "     , F0401.DEMSEQ"; // 심사차수(yyyymm+seq(2))
                sql += Environment.NewLine + "     , F0401.DEMNO"; // 청구번호
                sql += Environment.NewLine + "     , F0401.GRPNO"; // 묶음번호
                sql += Environment.NewLine + "     , F0401.DEMUNITFG"; // 청구단위구분
                sql += Environment.NewLine + "     , F0401.JRFG"; // 보험자종별구분
                sql += Environment.NewLine + "     , F0402.JSYYSEQ"; // 정산연번
                sql += Environment.NewLine + "     , F0402.SIMGBN"; // 심사구분
                sql += Environment.NewLine + "     , F0402.JSREDPT1"; // 정산담당부명
                sql += Environment.NewLine + "     , F0402.JSREDPT2"; // 정산담당조명
                sql += Environment.NewLine + "     , F0402.JSREDPNM"; // 정산담당자명
                sql += Environment.NewLine + "     , F0402.JSREDPNO"; // 정산담당자번호
                sql += Environment.NewLine + "     , F0402.JSRETELE"; // 정산담당자전화번호
                sql += Environment.NewLine + "     , F0402.JSBUSSCD"; // 정산업무코드
                sql += Environment.NewLine + "     , F0402.JSBUSSNM"; // 정산업무명
                sql += Environment.NewLine + "     , F0402.SKPMGUM"; // 이전심결사항-본인부담환급금 합계
                sql += Environment.NewLine + "     , F0402.SKPPGUM"; // 이전심결사항-본인추가부담금 합계
                sql += Environment.NewLine + "     , F0402.SKTTAMT"; // 이전심결사항-요양급여비용총액 합계
                sql += Environment.NewLine + "     , F0402.SKPTAMT"; // 이전심결사항-본인부담금 합계
                sql += Environment.NewLine + "     , F0402.SKJIWONAMT"; // 이전심결사항-지원금
                sql += Environment.NewLine + "     , F0402.SKJAM"; // 이전심결사항-장애인의료비
                sql += Environment.NewLine + "     , F0402.SKMAXPTAMT"; // 이전심결사항-본인부담상한초과금 합계
                sql += Environment.NewLine + "     , F0402.SKMAXPTCHAAMT"; //  이전심결사항-본인부담상한액차액 합계
                sql += Environment.NewLine + "     , F0402.SKUNAMT"; // 이전심결사항-보험자부담금 합계
                sql += Environment.NewLine + "     , F0402.SKBHUNAMT"; // 이전심결사항-보훈청구액 합계
                sql += Environment.NewLine + "     , F0402.SKRSTAMT"; // 이전심결사항-심사결정액 합계
                sql += Environment.NewLine + "     , F0402.SKSUTAKAMT"; // 이전심결사항-위탁검사직접지급금 합계
                sql += Environment.NewLine + "     , F0402.SKCNT"; // 이전심결사항-건수합계
                sql += Environment.NewLine + "     , F0402.JSPMGUM"; // 정산심결사항-본인부담환급금 합계
                sql += Environment.NewLine + "     , F0402.JSPPGUM"; // 정산심결사항-본인추가부담금 합계
                sql += Environment.NewLine + "     , F0402.JSTTAMT"; // 정산심결사항-요양급여비용총액 합계
                sql += Environment.NewLine + "     , F0402.JSPTAMT"; // 정산심결사항-본인부담금 합계
                sql += Environment.NewLine + "     , F0402.JSJIWONAMT"; // 정산심결사항-지원금
                sql += Environment.NewLine + "     , F0402.JSJAM"; // 정산심결사항-장애인의료비
                sql += Environment.NewLine + "     , F0402.JSMAXPTAMT"; // 정산심결사항-본인부담상한초과금 합계
                sql += Environment.NewLine + "     , F0402.JSMAXPTCHAAMT"; // 정산심결사항-본인부담상한액차액 합계
                sql += Environment.NewLine + "     , F0402.JSUNAMT"; // 정산심결사항-보험자부담금 합계
                sql += Environment.NewLine + "     , F0402.JSBHUNAMT"; // 정산심결사항-보훈부담금 합계
                sql += Environment.NewLine + "     , F0402.JSRSTAMT"; // 정산심결사항-심사결정액 합계
                sql += Environment.NewLine + "     , F0402.JSSUTAKAMT"; // 정산심결사항-위탁검사직접지급금 합계
                sql += Environment.NewLine + "     , F0402.JSRSTCHAAMT"; // 정산심결사항-결정차액 합계
                sql += Environment.NewLine + "     , F0402.JSCNT"; // 정산심결사항-건수 합계
                sql += Environment.NewLine + "     , F0402.JSSKAMT"; // 정산심사결과인정(조정)금액합계
                sql += Environment.NewLine + "     , F0402.JSSKCNT"; // 정산심사결과인정(조정)건수합계
                sql += Environment.NewLine + "     , F0402.SKCDJS"; // 이전심결사항-차등지수
                sql += Environment.NewLine + "     , F0402.SKCJJCNT"; // 이전심결사항-진찰(조제)횟수
                sql += Environment.NewLine + "     , F0402.SKCJDAYS"; // 이전심결사항-진료(조제)일수
                sql += Environment.NewLine + "     , F0402.SKCJDR"; // 이전심결사항-의(약)사수
                sql += Environment.NewLine + "     , F0402.SKCJJAMT"; // 이전심결사항-진찰(조제)료
                sql += Environment.NewLine + "     , F0402.SKCJJMAMT"; // 이전심결사항-진찰료(조제료)차감액
                sql += Environment.NewLine + "     , F0402.JSCDJS"; // 정산심결사항-차등지수
                sql += Environment.NewLine + "     , F0402.JSCJJCNT"; // 정산심결사항-진찰(조제)횟수
                sql += Environment.NewLine + "     , F0402.JSCJDAYS"; // 정산심결사항-진료(조제)일수
                sql += Environment.NewLine + "     , F0402.JSCJDR"; // 정산심결사항-의(약)사수
                sql += Environment.NewLine + "     , F0402.JSCJJAMT"; // 정산심결사항-진찰(조제)료
                sql += Environment.NewLine + "     , F0402.JSCJJMAMT"; // 정산심결사항-진찰료(조제료)차감액
                sql += Environment.NewLine + "     , F0402.JSCJRSTCHAAMT"; // 정산심결사항-차등지수결정차액
                sql += Environment.NewLine + "     , F0402.MEMO"; // 명일련비고사항
                sql += Environment.NewLine + "     , F0402.SKUPLMTCHATTAMT"; // 이전심결사항-약제상한차액
                sql += Environment.NewLine + "     , F0402.SKPTTTAMT"; // 이전심결사항-수진자총액
                sql += Environment.NewLine + "     , F0402.JSUPLMTCHATTAMT"; // 정산심결사항-약제상한차액
                sql += Environment.NewLine + "     , F0402.JSPTTTAMT"; // 정산심결사항-수진자총액
                sql += Environment.NewLine + "     , F0402.JJUPLMTCHATTAMT"; // 정산심결사항-상한차액조정금액
                sql += Environment.NewLine + "     , F0402.SKBAKAMT"; // 이전심결사항-100/100총액
                sql += Environment.NewLine + "     , F0402.JSBAKAMT"; // 정산심결사항-100/100총액
                sql += Environment.NewLine + "     , F0402.SKBHPMGUM"; // 이전심결사항-보훈본인부담환급금
                sql += Environment.NewLine + "     , F0402.JSBHPMGUM"; // 정산심결사항-보훈본인부담환급금
                sql += Environment.NewLine + "     , F0402.SKBHPPGUM"; // 이전심결사항-보훈본인추가부담금
                sql += Environment.NewLine + "     , F0402.JSBHPPGUM"; // 정산심결사항-보훈본인추가부담금
                sql += Environment.NewLine + "     , F0402.SKBHPTAMT"; // 이전심결사항-보훈본인일부부담금
                sql += Environment.NewLine + "     , F0402.JSBHPTAMT"; // 정산심결사항-보훈본인일부부담금
                sql += Environment.NewLine + "     , F0402.BAKDNSKPMGUM"; // 2014.03.21 KJW - 이전심결사항-100/100미만 본인부담금합계
                sql += Environment.NewLine + "     , F0402.BAKDNSKPPGUM"; // 2014.03.21 KJW - 이전심결사항-100/100미만 본인추가부담금합계
                sql += Environment.NewLine + "     , F0402.BAKDNSKTTAMT"; // 2014.03.21 KJW - 이전심결사항-100/100미만 총액합계
                sql += Environment.NewLine + "     , F0402.BAKDNSKPTAMT"; // 2014.03.21 KJW - 이전심결사항-100/100미만 본인일부부담금합계
                sql += Environment.NewLine + "     , F0402.BAKDNSKUNAMT"; // 2014.03.21 KJW - 이전심결사항-100/100미만 청구액합계
                sql += Environment.NewLine + "     , F0402.BAKDNSKBHUNAMT"; // 2014.03.21 KJW - 이전심결사항-100/100미만 보훈청구액합계
                sql += Environment.NewLine + "     , F0402.BAKDNJSPMGUM"; // 2014.03.21 KJW - 정산심결사항-100/100미만 본인부담금합계
                sql += Environment.NewLine + "     , F0402.BAKDNJSPPGUM"; // 2014.03.21 KJW - 정산심결사항-100/100미만 본인추가부담금합계
                sql += Environment.NewLine + "     , F0402.BAKDNJSTTAMT"; // 2014.03.21 KJW - 정산심결사항-100/100미만 총액합계
                sql += Environment.NewLine + "     , F0402.BAKDNJSPTAMT"; // 2014.03.21 KJW - 정산심결사항-100/100미만 본인일부부담금합계
                sql += Environment.NewLine + "     , F0402.BAKDNJSUNAMT"; // 2014.03.21 KJW - 정산심결사항-100/100미만 청구액합계
                sql += Environment.NewLine + "     , F0402.BAKDNJSBHUNAMT"; // 2014.03.21 KJW - 정산심결사항-100/100미만 보훈청구액합계
                sql += Environment.NewLine + "     , F0402.SKSANGKYERFAMT"; // 2014.09.24 KJW - 이전심결사항-상계환급금
                sql += Environment.NewLine + "     , F0402.SKSANGKYEADDAMT"; // 2014.09.24 KJW - 이전심결사항-상계추가부담금
                sql += Environment.NewLine + "     , F0402.JSSANGKYERFAMT"; // 2014.09.24 KJW - 정산심결사항-상계환급금
                sql += Environment.NewLine + "     , F0402.JSSANGKYEADDAMT"; // 2014.09.24 KJW - 정산심결사항-상계추가부담금
                sql += Environment.NewLine + "  FROM TIE_F0401 F0401 INNER JOIN TIE_F0402 F0402 ON F0402.JSDEMSEQ = F0401.JSDEMSEQ";
                sql += Environment.NewLine + "                                                 AND F0401.JSREDAY  = F0402.JSREDAY";
                sql += Environment.NewLine + "                                                 AND F0401.CNECNO   = F0402.CNECNO";
                sql += Environment.NewLine + "                                                 AND F0401.DCOUNT   = F0402.DCOUNT";
                sql += Environment.NewLine + " WHERE 1=1 ";
                if (txtFrdt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0401.JSREDAY>='" + txtFrdt.Text.ToString() + "'";
                }
                if (txtTodt.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0401.JSREDAY<='" + txtTodt.Text.ToString() + "'";
                }
                if (txtCnecno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0401.CNECNO='" + txtCnecno.Text.ToString() + "'";
                }
                if (txtDemno.Text.ToString() != "")
                {
                    sql += Environment.NewLine + "   AND F0401.DEMNO='" + txtDemno.Text.ToString() + "'";
                }
                sql += Environment.NewLine + " ORDER BY F0401.JSREDAY DESC";


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
                    data.JRFG = reader["JRFG"].ToString().TrimEnd(); // 보험자종별구분
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
                    data.SKTTAMT = MetroLib.StrHelper.ToLong(reader["SKTTAMT"].ToString().TrimEnd()); // 이전심결사항-요양급여비용총액 합계
                    data.SKPTAMT = MetroLib.StrHelper.ToLong(reader["SKPTAMT"].ToString().TrimEnd()); // 이전심결사항-본인부담금 합계
                    data.SKJIWONAMT = MetroLib.StrHelper.ToLong(reader["SKJIWONAMT"].ToString().TrimEnd()); // 이전심결사항-지원금
                    data.SKJAM = MetroLib.StrHelper.ToLong(reader["SKJAM"].ToString().TrimEnd()); // 이전심결사항-장애인의료비
                    data.SKMAXPTAMT = MetroLib.StrHelper.ToLong(reader["SKMAXPTAMT"].ToString().TrimEnd()); // 이전심결사항-본인부담상한초과금 합계
                    data.SKMAXPTCHAAMT = MetroLib.StrHelper.ToLong(reader["SKMAXPTCHAAMT"].ToString().TrimEnd()); //  이전심결사항-본인부담상한액차액 합계
                    data.SKUNAMT = MetroLib.StrHelper.ToLong(reader["SKUNAMT"].ToString().TrimEnd()); // 이전심결사항-보험자부담금 합계
                    data.SKBHUNAMT = MetroLib.StrHelper.ToLong(reader["SKBHUNAMT"].ToString().TrimEnd()); // 이전심결사항-보훈청구액 합계
                    data.SKRSTAMT = MetroLib.StrHelper.ToLong(reader["SKRSTAMT"].ToString().TrimEnd()); // 이전심결사항-심사결정액 합계
                    data.SKSUTAKAMT = MetroLib.StrHelper.ToLong(reader["SKSUTAKAMT"].ToString().TrimEnd()); // 이전심결사항-위탁검사직접지급금 합계
                    data.SKCNT = MetroLib.StrHelper.ToLong(reader["SKCNT"].ToString().TrimEnd()); // 이전심결사항-건수합계
                    data.JSPMGUM = MetroLib.StrHelper.ToLong(reader["JSPMGUM"].ToString().TrimEnd()); // 정산심결사항-본인부담환급금 합계
                    data.JSPPGUM = MetroLib.StrHelper.ToLong(reader["JSPPGUM"].ToString().TrimEnd()); // 정산심결사항-본인추가부담금 합계
                    data.JSTTAMT = MetroLib.StrHelper.ToLong(reader["JSTTAMT"].ToString().TrimEnd()); // 정산심결사항-요양급여비용총액 합계
                    data.JSPTAMT = MetroLib.StrHelper.ToLong(reader["JSPTAMT"].ToString().TrimEnd()); // 정산심결사항-본인부담금 합계
                    data.JSJIWONAMT = MetroLib.StrHelper.ToLong(reader["JSJIWONAMT"].ToString().TrimEnd()); // 정산심결사항-지원금
                    data.JSJAM = MetroLib.StrHelper.ToLong(reader["JSJAM"].ToString().TrimEnd()); // 정산심결사항-장애인의료비
                    data.JSMAXPTAMT = MetroLib.StrHelper.ToLong(reader["JSMAXPTAMT"].ToString().TrimEnd()); // 정산심결사항-본인부담상한초과금 합계
                    data.JSMAXPTCHAAMT = MetroLib.StrHelper.ToLong(reader["JSMAXPTCHAAMT"].ToString().TrimEnd()); // 정산심결사항-본인부담상한액차액 합계
                    data.JSUNAMT = MetroLib.StrHelper.ToLong(reader["JSUNAMT"].ToString().TrimEnd()); // 정산심결사항-보험자부담금 합계
                    data.JSBHUNAMT = MetroLib.StrHelper.ToLong(reader["JSBHUNAMT"].ToString().TrimEnd()); // 정산심결사항-보훈부담금 합계
                    data.JSRSTAMT = MetroLib.StrHelper.ToLong(reader["JSRSTAMT"].ToString().TrimEnd()); // 정산심결사항-심사결정액 합계
                    data.JSSUTAKAMT = MetroLib.StrHelper.ToLong(reader["JSSUTAKAMT"].ToString().TrimEnd()); // 정산심결사항-위탁검사직접지급금 합계
                    data.JSRSTCHAAMT = MetroLib.StrHelper.ToLong(reader["JSRSTCHAAMT"].ToString().TrimEnd()); // 정산심결사항-결정차액 합계
                    data.JSCNT = MetroLib.StrHelper.ToLong(reader["JSCNT"].ToString().TrimEnd()); // 정산심결사항-건수 합계
                    data.JSSKAMT = MetroLib.StrHelper.ToLong(reader["JSSKAMT"].ToString().TrimEnd()); // 정산심사결과인정(조정)금액합계
                    data.JSSKCNT = MetroLib.StrHelper.ToLong(reader["JSSKCNT"].ToString().TrimEnd()); // 정산심사결과인정(조정)건수합계
                    data.SKCDJS = MetroLib.StrHelper.ToDouble(reader["SKCDJS"].ToString().TrimEnd()); // 이전심결사항-차등지수
                    data.SKCJJCNT = MetroLib.StrHelper.ToLong(reader["SKCJJCNT"].ToString().TrimEnd()); // 이전심결사항-진찰(조제)횟수
                    data.SKCJDAYS = MetroLib.StrHelper.ToLong(reader["SKCJDAYS"].ToString().TrimEnd()); // 이전심결사항-진료(조제)일수
                    data.SKCJDR = MetroLib.StrHelper.ToLong(reader["SKCJDR"].ToString().TrimEnd()); // 이전심결사항-의(약)사수
                    data.SKCJJAMT = MetroLib.StrHelper.ToLong(reader["SKCJJAMT"].ToString().TrimEnd()); // 이전심결사항-진찰(조제)료
                    data.SKCJJMAMT = MetroLib.StrHelper.ToLong(reader["SKCJJMAMT"].ToString().TrimEnd()); // 이전심결사항-진찰료(조제료)차감액
                    data.JSCDJS = MetroLib.StrHelper.ToDouble(reader["JSCDJS"].ToString().TrimEnd()); // 정산심결사항-차등지수
                    data.JSCJJCNT = MetroLib.StrHelper.ToLong(reader["JSCJJCNT"].ToString().TrimEnd()); // 정산심결사항-진찰(조제)횟수
                    data.JSCJDAYS = MetroLib.StrHelper.ToLong(reader["JSCJDAYS"].ToString().TrimEnd()); // 정산심결사항-진료(조제)일수
                    data.JSCJDR = MetroLib.StrHelper.ToLong(reader["JSCJDR"].ToString().TrimEnd()); // 정산심결사항-의(약)사수
                    data.JSCJJAMT = MetroLib.StrHelper.ToLong(reader["JSCJJAMT"].ToString().TrimEnd()); // 정산심결사항-진찰(조제)료
                    data.JSCJJMAMT = MetroLib.StrHelper.ToLong(reader["JSCJJMAMT"].ToString().TrimEnd()); // 정산심결사항-진찰료(조제료)차감액
                    data.JSCJRSTCHAAMT = MetroLib.StrHelper.ToLong(reader["JSCJRSTCHAAMT"].ToString().TrimEnd()); // 정산심결사항-차등지수결정차액
                    data.MEMO = reader["MEMO"].ToString().TrimEnd(); // 명일련비고사항
                    data.SKUPLMTCHATTAMT = MetroLib.StrHelper.ToLong(reader["SKUPLMTCHATTAMT"].ToString().TrimEnd()); // 이전심결사항-약제상한차액
                    data.SKPTTTAMT = MetroLib.StrHelper.ToLong(reader["SKPTTTAMT"].ToString().TrimEnd()); // 이전심결사항-수진자총액
                    data.JSUPLMTCHATTAMT = MetroLib.StrHelper.ToLong(reader["JSUPLMTCHATTAMT"].ToString().TrimEnd()); // 정산심결사항-약제상한차액
                    data.JSPTTTAMT = MetroLib.StrHelper.ToLong(reader["JSPTTTAMT"].ToString().TrimEnd()); // 정산심결사항-수진자총액
                    data.JJUPLMTCHATTAMT = MetroLib.StrHelper.ToLong(reader["JJUPLMTCHATTAMT"].ToString().TrimEnd()); // 정산심결사항-상한차액조정금액
                    data.SKBAKAMT = MetroLib.StrHelper.ToLong(reader["SKBAKAMT"].ToString().TrimEnd()); // 이전심결사항-100/100총액
                    data.JSBAKAMT = MetroLib.StrHelper.ToLong(reader["JSBAKAMT"].ToString().TrimEnd()); // 정산심결사항-100/100총액
                    data.SKBHPMGUM = MetroLib.StrHelper.ToLong(reader["SKBHPMGUM"].ToString().TrimEnd()); // 이전심결사항-보훈본인부담환급금
                    data.JSBHPMGUM = MetroLib.StrHelper.ToLong(reader["JSBHPMGUM"].ToString().TrimEnd()); // 정산심결사항-보훈본인부담환급금
                    data.SKBHPPGUM = MetroLib.StrHelper.ToLong(reader["SKBHPPGUM"].ToString().TrimEnd()); // 이전심결사항-보훈본인추가부담금
                    data.JSBHPPGUM = MetroLib.StrHelper.ToLong(reader["JSBHPPGUM"].ToString().TrimEnd()); // 정산심결사항-보훈본인추가부담금
                    data.SKBHPTAMT = MetroLib.StrHelper.ToLong(reader["SKBHPTAMT"].ToString().TrimEnd()); // 이전심결사항-보훈본인일부부담금
                    data.JSBHPTAMT = MetroLib.StrHelper.ToLong(reader["JSBHPTAMT"].ToString().TrimEnd()); // 정산심결사항-보훈본인일부부담금
                    data.BAKDNSKPMGUM = MetroLib.StrHelper.ToLong(reader["BAKDNSKPMGUM"].ToString().TrimEnd()); // 2014.03.21 KJW - 이전심결사항-100/100미만 본인부담금합계
                    data.BAKDNSKPPGUM = MetroLib.StrHelper.ToLong(reader["BAKDNSKPPGUM"].ToString().TrimEnd()); // 2014.03.21 KJW - 이전심결사항-100/100미만 본인추가부담금합계
                    data.BAKDNSKTTAMT = MetroLib.StrHelper.ToLong(reader["BAKDNSKTTAMT"].ToString().TrimEnd()); // 2014.03.21 KJW - 이전심결사항-100/100미만 총액합계
                    data.BAKDNSKPTAMT = MetroLib.StrHelper.ToLong(reader["BAKDNSKPTAMT"].ToString().TrimEnd()); // 2014.03.21 KJW - 이전심결사항-100/100미만 본인일부부담금합계
                    data.BAKDNSKUNAMT = MetroLib.StrHelper.ToLong(reader["BAKDNSKUNAMT"].ToString().TrimEnd()); // 2014.03.21 KJW - 이전심결사항-100/100미만 청구액합계
                    data.BAKDNSKBHUNAMT = MetroLib.StrHelper.ToLong(reader["BAKDNSKBHUNAMT"].ToString().TrimEnd()); // 2014.03.21 KJW - 이전심결사항-100/100미만 보훈청구액합계
                    data.BAKDNJSPMGUM = MetroLib.StrHelper.ToLong(reader["BAKDNJSPMGUM"].ToString().TrimEnd()); // 2014.03.21 KJW - 정산심결사항-100/100미만 본인부담금합계
                    data.BAKDNJSPPGUM = MetroLib.StrHelper.ToLong(reader["BAKDNJSPPGUM"].ToString().TrimEnd()); // 2014.03.21 KJW - 정산심결사항-100/100미만 본인추가부담금합계
                    data.BAKDNJSTTAMT = MetroLib.StrHelper.ToLong(reader["BAKDNJSTTAMT"].ToString().TrimEnd()); // 2014.03.21 KJW - 정산심결사항-100/100미만 총액합계
                    data.BAKDNJSPTAMT = MetroLib.StrHelper.ToLong(reader["BAKDNJSPTAMT"].ToString().TrimEnd()); // 2014.03.21 KJW - 정산심결사항-100/100미만 본인일부부담금합계
                    data.BAKDNJSUNAMT = MetroLib.StrHelper.ToLong(reader["BAKDNJSUNAMT"].ToString().TrimEnd()); // 2014.03.21 KJW - 정산심결사항-100/100미만 청구액합계
                    data.BAKDNJSBHUNAMT = MetroLib.StrHelper.ToLong(reader["BAKDNJSBHUNAMT"].ToString().TrimEnd()); // 2014.03.21 KJW - 정산심결사항-100/100미만 보훈청구액합계
                    data.SKSANGKYERFAMT = MetroLib.StrHelper.ToLong(reader["SKSANGKYERFAMT"].ToString().TrimEnd()); // 2014.09.24 KJW - 이전심결사항-상계환급금
                    data.SKSANGKYEADDAMT = MetroLib.StrHelper.ToLong(reader["SKSANGKYEADDAMT"].ToString().TrimEnd()); // 2014.09.24 KJW - 이전심결사항-상계추가부담금
                    data.JSSANGKYERFAMT = MetroLib.StrHelper.ToLong(reader["JSSANGKYERFAMT"].ToString().TrimEnd()); // 2014.09.24 KJW - 정산심결사항-상계환급금
                    data.JSSANGKYEADDAMT = MetroLib.StrHelper.ToLong(reader["JSSANGKYEADDAMT"].ToString().TrimEnd()); // 2014.09.24 KJW - 정산심결사항-상계추가부담금

                    list.Add(data);

                    return true;
                });
            }

            string[] check_field =
            {
                "SKPMGUM", "JSPMGUM", // 본인부담환급금 합계
                "SKPPGUM", "JSPPGUM", // 본인추가부담금 합계
                "SKTTAMT", "JSTTAMT", // 요양급여비용총액 합계
                "SKPTAMT", "JSPTAMT", // 본인부담금 합계
                "SKJIWONAMT", "JSJIWONAMT", // 지원금
                "SKJAM", "JSJAM", // 장애인의료비
                "SKMAXPTAMT", "JSMAXPTAMT", // 본인부담상한초과금 합계
                "SKMAXPTCHAAMT", "JSMAXPTCHAAMT", //  본인부담상한액차액 합계
                "SKUNAMT", "JSUNAMT", // 보험자부담금 합계
                "SKBHUNAMT", "JSBHUNAMT", // 보훈청구액 합계
                "SKRSTAMT", "JSRSTAMT", // 심사결정액 합계
                "SKSUTAKAMT", "JSSUTAKAMT", // 위탁검사직접지급금 합계
                "SKCNT", "JSCNT", // 건수합계
                "SKCDJS", "JSCDJS", // 차등지수
                "SKCJJCNT", "JSCJJCNT", // 진찰(조제)횟수
                "SKCJDAYS", "JSCJDAYS", // 진료(조제)일수
                "SKCJDR", "JSCJDR", // 의(약)사수
                "SKCJJAMT", "JSCJJAMT", // 진찰(조제)료
                "SKCJJMAMT", "JSCJJMAMT", // 진찰료(조제료)차감액
                "SKUPLMTCHATTAMT", "JSUPLMTCHATTAMT", // 약제상한차액
                "SKPTTTAMT", "JSPTTTAMT", // 수진자총액
                "SKBAKAMT", "JSBAKAMT", // 100/100총액
                "SKBHPMGUM", "JSBHPMGUM", // 보훈본인부담환급금
                "SKBHPPGUM", "JSBHPPGUM", // 보훈본인추가부담금
                "SKBHPTAMT", "JSBHPTAMT", // 이전심결사항-보훈본인일부부담금
                "BAKDNSKPMGUM", "BAKDNJSPMGUM", // 100/100미만 본인부담환급금합계
                "BAKDNSKPPGUM", "BAKDNJSPPGUM", // 100/100미만 본인추가부담금합계
                "BAKDNSKTTAMT", "BAKDNJSTTAMT", // 100/100미만 총액합계
                "BAKDNSKPTAMT", "BAKDNJSPTAMT", // 100/100미만 본인일부부담금합계
                "BAKDNSKUNAMT", "BAKDNJSUNAMT", // 100/100미만 청구액합계
                "BAKDNSKBHUNAMT", "BAKDNJSBHUNAMT", // 100/100미만 보훈청구액합계
                "SKSANGKYERFAMT", "JSSANGKYERFAMT", // 상계환급금
                "SKSANGKYEADDAMT", "JSSANGKYEADDAMT", // 상계추가부담금

                "JSCJRSTCHAAMT_X", "JSCJRSTCHAAMT", // 차등지수정산심사결정차액
            };
            DxLib.GridHelper.HideColumnIfZeroInBand(check_field, grdMainView);

            string[] check_field1 = { "JJUPLMTCHATTAMT" }; // 상한차액조정금액
            DxLib.GridHelper.HideColumnIfZero(check_field1, grdMainView);

            DxLib.GridHelper.SetBandVisible(grdMainView);

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

        private void grdMain_Load(object sender, EventArgs e)
        {
            // 밴드명 변경
            string[] caption = {"상한차액조정금액","상한차액","조정금액",
                                "본인부담환급금","본인부담","환급금",
                                "보훈본인부담환급금","보훈본인부담","환급금",
                                "100미만본인부담환급금","100미만본인부담","환급금",
                                "본인추가부담금","본인","추가부담금",
                                "보훈본인추가부담금","보훈본인","추가부담금",
                                "100미만본인추가부담금","100미만본인","추가부담금",
                                "상계추가부담금","상계","추가부담금",
                                "본인부담상한액초과금","본인부담","상한액초과금",
                                "본인부담상한액차액","본인부담","상한액차액",
                                "위탁검사직접지급금","위탁검사","직접지급금",
                                "보훈본인일부부담금","보훈","본인일부부담금",
                                "100미만총액","100미만","총액",
                                "100미만본인일부부담금","100미만","본인일부부담금",
                                "100미만청구액","100미만","청구액",
                                "100미만보훈청구액","100미만","보훈청구액",
                               };
            DxLib.GridHelper.BandReCaption(grdMainView, caption);

            //gridBand3.Visible = false;
            //gridBand141.Visible = false;
            //gridBand139.Visible = false;
            //gridBand137.Visible = false;
            //gridBand135.Visible = false;

            grdMainView.OptionsPrint.AutoWidth = false;
        }

        /*
        private void HideColumnIfZero(string[] check_field, DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView view)
        {
            for (int i = 0; i < check_field.Length; i++)
            {
                bool show = false;
                for (int row = 0; row < view.RowCount; row++)
                {
                    if (show == false && MetroLib.StrHelper.ToLong(view.GetRowCellValue(row, check_field[i]).ToString()) != 0) show = true;
                    if (show == true) break;
                }

                view.Columns[check_field[i]].Visible = show;
            }
        }
        */

        /*
        private void HideColumnIfZeroInBand(string[] check_field, DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView view)
        {
            for (int i = 0; i < check_field.Length; i += 2)
            {
                bool show1 = false;
                bool show2 = false;
                for (int row = 0; row < view.RowCount; row++)
                {
                    if (show1 == false && MetroLib.StrHelper.ToLong(view.GetRowCellValue(row, check_field[i]).ToString()) != 0) show1 = true;
                    if (show2 == false && MetroLib.StrHelper.ToLong(view.GetRowCellValue(row, check_field[i + 1]).ToString()) != 0) show2 = true;
                    if (show1 == true && show2 == true) break;
                }
                // 순서가 중요함. [i + 1]을 먼저하고 [i]를 나중에 해야함.
                view.Columns[check_field[i + 1]].Visible = show1 || show2;
                view.Columns[check_field[i]].Visible = show1 || show2;
            }
        }
        */

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
            sql += Environment.NewLine + "SELECT F0403.JSDEMSEQ"; // 정산심사차수
            sql += Environment.NewLine + "     , F0403.JSREDAY"; // 정산통보일자
            sql += Environment.NewLine + "     , F0403.CNECNO"; // 접수번호
            sql += Environment.NewLine + "     , F0403.DCOUNT"; // 청구서 일련번호
            sql += Environment.NewLine + "     , F0403.JSYYSEQ"; // 정산연번
            sql += Environment.NewLine + "     , F0403.JSSEQNO"; // 정산일련번호
            sql += Environment.NewLine + "     , F0403.EPRTNO"; // 명세서 일련번호
            sql += Environment.NewLine + "     , F0403.PNM"; // 수진자성명
            sql += Environment.NewLine + "     , F0403.INSNM"; // 가입자 성명
            sql += Environment.NewLine + "     , F0403.UNICD"; // 사업장기호
            sql += Environment.NewLine + "     , F0403.JRHT"; // 진료형태
            sql += Environment.NewLine + "     , F0403.GUBUN"; // 구분(1.환급 2.환수 3.환수+환급)
            sql += Environment.NewLine + "     , F0403.INSID"; // 증번호
            sql += Environment.NewLine + "     , F0403.JAJR"; // 정액정률구분(0.정액 9.정률 1.장기요양정액수가 2.장기요양행위별수가)
            sql += Environment.NewLine + "     , F0403.STEDT"; // 당월요양개시일
            sql += Environment.NewLine + "     , F0403.SKTJKH"; // 이전심결사항-특정기호
            sql += Environment.NewLine + "     , F0403.SKDRGNO"; // 이전심결사항-DRG번호
            sql += Environment.NewLine + "     , F0403.DACD"; // 상병분류기호
            sql += Environment.NewLine + "     , F0403.JJSOGE1"; // 조정소계1
            sql += Environment.NewLine + "     , F0403.JJSOGE2"; // 조정소계2
            sql += Environment.NewLine + "     , F0403.SKPMGUM"; // 이전심결사항-본인부담환급금
            sql += Environment.NewLine + "     , F0403.SKPPGUM"; // 이전심결사항-본인추가부담금
            sql += Environment.NewLine + "     , F0403.SKCHOGUM"; // 이전심결사항-초진료
            sql += Environment.NewLine + "     , F0403.SKJAEGUM"; // 이전심결사항-재진료
            sql += Environment.NewLine + "     , F0403.SKTTAMT"; // 이전심결사항-요양급여비용총액
            sql += Environment.NewLine + "     , F0403.SKPTAMT"; // 이전심결사항-본인부담금
            sql += Environment.NewLine + "     , F0403.SKMAXPTAMT"; // 이전심결사항-본인부담상한초과금
            sql += Environment.NewLine + "     , F0403.SKMAXPTCHAAMT"; // 이전심결사항-본인부담상한액차액
            sql += Environment.NewLine + "     , F0403.SKUNAMT"; // 이전심결사항-보험자부담금
            sql += Environment.NewLine + "     , F0403.SKTTTAMT"; // 이전심결사항-진료비총액
            sql += Environment.NewLine + "     , F0403.SKBHUNAMT"; // 이전심결사항-보훈청구액
            sql += Environment.NewLine + "     , F0403.SKRSTAMT"; // 이전심결사항-심사결정액
            sql += Environment.NewLine + "     , F0403.SKSUTAKAMT"; // 이전심결사항-위탁검사직접지급금
            sql += Environment.NewLine + "     , F0403.SKCHOCNT"; // 이전심결사항-초진횟수
            sql += Environment.NewLine + "     , F0403.SKCNONCNT"; // 이전심결사항-초진가산횟수(1일분투약횟수)
            sql += Environment.NewLine + "     , F0403.SKJAECNT";   // 이전심결사항-재진횟수(2일분투약횟수)
            sql += Environment.NewLine + "     , F0403.SKJAENCNT"; // 이전심결사항-재진가산횟수(3일분이상투약횟수)
            sql += Environment.NewLine + "     , F0403.SKEXAMC"; // 이전심결사항-입원일수(내원일수)
            sql += Environment.NewLine + "     , F0403.SKORDDAYS"; // 이전심결사항-요양급여일수
            sql += Environment.NewLine + "     , F0403.SKORDCNT"; // 이전심결사항-처방횟수
            sql += Environment.NewLine + "     , F0403.JSTJKH"; // 정산심결사항-특정기호
            sql += Environment.NewLine + "     , F0403.JSDRGNO"; // 정산심결사항-DRG번호
            sql += Environment.NewLine + "     , F0403.JSPMGUM"; // 정산심결사항-본인부담환급금
            sql += Environment.NewLine + "     , F0403.JSPPGUM"; // 정산심결사항-본인추가부담금
            sql += Environment.NewLine + "     , F0403.JSCHOGUM"; // 정산심결사항-초진료
            sql += Environment.NewLine + "     , F0403.JSJAEGUM"; // 정산심결사항-재진료
            sql += Environment.NewLine + "     , F0403.JSTTAMT"; // 정산심결사항-요양급여비용총액
            sql += Environment.NewLine + "     , F0403.JSPTAMT"; // 정산심결사항-본인부담금
            sql += Environment.NewLine + "     , F0403.JSMAXPTAMT"; // 정산심결사항-본인부담상한초과금
            sql += Environment.NewLine + "     , F0403.JSMAXPTCHAAMT"; // 정산심결사항-본인부담상한액차액
            sql += Environment.NewLine + "     , F0403.JSUNAMT"; // 정산심결사항-보험자부담금
            sql += Environment.NewLine + "     , F0403.JSTTTAMT"; // 정산심결사항-진료비총액
            sql += Environment.NewLine + "     , F0403.JSBHUNAMT"; // 정산심결사항-보훈청구액
            sql += Environment.NewLine + "     , F0403.JSRSTAMT"; // 정산심결사항-심사결정액
            sql += Environment.NewLine + "     , F0403.JSSUTAKAMT"; // 정산심결사항-위탁검사직접지급금
            sql += Environment.NewLine + "     , F0403.JSRSTCHAAMT"; // 정산심결사항-결정차액
            sql += Environment.NewLine + "     , F0403.JSCHOCNT"; // 정산심결사항-초진횟수
            sql += Environment.NewLine + "     , F0403.JSCHONCNT"; // 정산심결사항-초진가산횟수(1일분투약횟수)
            sql += Environment.NewLine + "     , F0403.JSJAECNT"; // 정산심결사항-재진횟수(2일분투약횟수)
            sql += Environment.NewLine + "     , F0403.JSJAENCNT"; // 정산심결사항-재진가산횟수(3일분이상투약횟수)
            sql += Environment.NewLine + "     , F0403.JSEXAMC"; // 정산심결사항-입원일수(내원일수)
            sql += Environment.NewLine + "     , F0403.JSORDDAYS"; // 정산심결사항-요양급여일수
            sql += Environment.NewLine + "     , F0403.JSORDCNT"; // 정산심결사항-처방횟수
            sql += Environment.NewLine + "     , F0403.MEMO"; // 명일련비고사항
            sql += Environment.NewLine + "     , F0403.SKGONSGB";
            sql += Environment.NewLine + "     , F0403.SKTSAMT";
            sql += Environment.NewLine + "     , F0403.SK100AMT";
            sql += Environment.NewLine + "     , F0403.SKUISAMT";
            sql += Environment.NewLine + "     , F0403.JSGONSGB";
            sql += Environment.NewLine + "     , F0403.JSTSAMT";
            sql += Environment.NewLine + "     , F0403.JS100AMT";
            sql += Environment.NewLine + "     , F0403.JSUISAMT";
            sql += Environment.NewLine + "     , F0403.SKJIWONAMT";
            sql += Environment.NewLine + "     , F0403.JSJIWONAMT";
            sql += Environment.NewLine + "     , F0403.SKJAM";
            sql += Environment.NewLine + "     , F0403.JSJAM";
            sql += Environment.NewLine + "     , F0403.SKUPLMTCHATTAMT";
            sql += Environment.NewLine + "     , F0403.SKPTTTAMT";
            sql += Environment.NewLine + "     , F0403.JSUPLMTCHATTAMT";
            sql += Environment.NewLine + "     , F0403.JSPTTTAMT";
            sql += Environment.NewLine + "     , F0403.SKTSJRAMT";
            sql += Environment.NewLine + "     , F0403.JSTSJRAMT";
            sql += Environment.NewLine + "     , F0403.SKBHPMGUM";
            sql += Environment.NewLine + "     , F0403.JSBHPMGUM";
            sql += Environment.NewLine + "     , F0403.SKBHPPGUM";
            sql += Environment.NewLine + "     , F0403.JSBHPPGUM";
            sql += Environment.NewLine + "     , F0403.SKBHPTAMT";
            sql += Environment.NewLine + "     , F0403.JSBHPTAMT";
            sql += Environment.NewLine + "     , F0403.BAKDNSKPMGUM"; // 2014.03.21 KJW - 100/100미만 본인부담금합계(이전)
            sql += Environment.NewLine + "     , F0403.BAKDNSKPPGUM"; // 2014.03.21 KJW - 100/100미만 본인추가부담금합계
            sql += Environment.NewLine + "     , F0403.BAKDNSKTTAMT"; // 2014.03.21 KJW - 100/100미만 총액합계
            sql += Environment.NewLine + "     , F0403.BAKDNSKPTAMT"; // 2014.03.21 KJW - 100/100미만 본인일부부담금합계
            sql += Environment.NewLine + "     , F0403.BAKDNSKUNAMT"; // 2014.03.21 KJW - 100/100미만 청구액합계
            sql += Environment.NewLine + "     , F0403.BAKDNSKBHUNAMT"; // 2014.03.21 KJW - 100/100미만 보훈청구액합계
            sql += Environment.NewLine + "     , F0403.BAKDNJSPMGUM"; // 2014.03.21 KJW - 100/100미만 본인부담금합계(정산)
            sql += Environment.NewLine + "     , F0403.BAKDNJSPPGUM"; // 2014.03.21 KJW - 100/100미만 본인추가부담금합계
            sql += Environment.NewLine + "     , F0403.BAKDNJSTTAMT"; // 2014.03.21 KJW - 100/100미만 총액합계
            sql += Environment.NewLine + "     , F0403.BAKDNJSPTAMT"; // 2014.03.21 KJW - 100/100미만 본인일부부담금합계
            sql += Environment.NewLine + "     , F0403.BAKDNJSUNAMT"; // 2014.03.21 KJW - 100/100미만 청구액합계
            sql += Environment.NewLine + "     , F0403.BAKDNJSBHUNAMT"; // 2014.03.21 KJW - 100/100미만 보훈청구액합계
            sql += Environment.NewLine + "     , F0403.SKSANGKYERFAMT"; // 2014.09.24 KJW - 상계환급금(이전)
            sql += Environment.NewLine + "     , F0403.SKSANGKYEADDAMT"; // 2014.09.24 KJW - 상계추가부담금
            sql += Environment.NewLine + "     , F0403.JSSANGKYERFAMT"; // 2014.09.24 KJW - 상계환급금(정산)
            sql += Environment.NewLine + "     , F0403.JSSANGKYEADDAMT"; // 2014.09.24 KJW - 상계추가부담금
            sql += Environment.NewLine + "     , F0403.SKBAKAMT"; // 2023.04.07 WOOIL - 이전심사 - 건강보험100/100 본인부담총액
            sql += Environment.NewLine + "     , F0403.JSBAKAMT"; // 2023.04.07 WOOIL - 정산심사 - 건강보험100/100 본인부담총액
            sql += Environment.NewLine + "     , F0401.DEMNO"; // 2023.04.07 WOOIL - 청구번호
            sql += Environment.NewLine + "  FROM TIE_F0403 F0403 INNER JOIN TIE_F0401 F0401 ON F0401.JSDEMSEQ=F0403.JSDEMSEQ AND F0401.JSREDAY=F0403.JSREDAY AND F0401.CNECNO=F0403.CNECNO AND F0401.DCOUNT=F0403.DCOUNT";

            if (cboDQOption.SelectedIndex == 1)
            {
                // 청구번호별
                sql += Environment.NewLine + " WHERE F0401.DEMNO = '" + p_demno + "' ";
            }
            else if (cboDQOption.SelectedIndex == 2)
            {
                // 청구월별
                sql += Environment.NewLine + " WHERE F0401.DEMNO LIKE '" + p_demno.Substring(0, 6) + "%' ";
            }
            else if (cboDQOption.SelectedIndex == 3)
            {
                // 청구년도별
                sql += Environment.NewLine + " WHERE F0401.DEMNO LIKE '" + p_demno.Substring(0, 4) + "%' ";
            }
            else
            {
                sql += Environment.NewLine + " WHERE F0403.JSDEMSEQ = '" + p_jsdemseq + "'";
                sql += Environment.NewLine + "   AND F0403.JSREDAY  = '" + p_jsreday + "'";
                sql += Environment.NewLine + "   AND F0403.CNECNO   = '" + p_cnecno + "'";
                sql += Environment.NewLine + "   AND F0403.DCOUNT   = '" + p_dcount + "'";
                sql += Environment.NewLine + "   AND F0403.JSYYSEQ  = '" + p_jsyyseq + "'";
            }
            sql += Environment.NewLine + " ORDER BY F0403.JSDEMSEQ, F0403.JSREDAY, F0403.CNECNO, F0403.DCOUNT, F0403.JSYYSEQ, F0403.JSSEQNO, F0403.EPRTNO";

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
                data.PNM = reader["PNM"].ToString().TrimEnd(); // 수진자성명
                data.INSNM = reader["INSNM"].ToString().TrimEnd(); // 가입자 성명
                data.UNICD = reader["UNICD"].ToString().TrimEnd(); // 사업장기호
                data.JRHT = reader["JRHT"].ToString().TrimEnd(); // 진료형태
                data.GUBUN = reader["GUBUN"].ToString().TrimEnd(); // 구분(1.환급 2.환수 3.환수+환급)
                data.INSID = reader["INSID"].ToString().TrimEnd(); // 증번호
                data.JAJR = reader["JAJR"].ToString().TrimEnd(); // 정액정률구분(0.정액 9.정률 1.장기요양정액수가 2.장기요양행위별수가)
                data.STEDT = reader["STEDT"].ToString().TrimEnd(); // 당월요양개시일
                data.SKTJKH = reader["SKTJKH"].ToString().TrimEnd(); // 이전심결사항-특정기호
                data.SKDRGNO = reader["SKDRGNO"].ToString().TrimEnd(); // 이전심결사항-DRG번호
                data.DACD = reader["DACD"].ToString().TrimEnd(); // 상병분류기호
                data.JJSOGE1 = MetroLib.StrHelper.ToLong(reader["JJSOGE1"].ToString().TrimEnd()); // 조정소계1
                data.JJSOGE2 = MetroLib.StrHelper.ToLong(reader["JJSOGE2"].ToString().TrimEnd()); // 조정소계2
                data.SKPMGUM = MetroLib.StrHelper.ToLong(reader["SKPMGUM"].ToString().TrimEnd()); // 이전심결사항-본인부담환급금
                data.SKPPGUM = MetroLib.StrHelper.ToLong(reader["SKPPGUM"].ToString().TrimEnd()); // 이전심결사항-본인추가부담금
                data.SKCHOGUM = MetroLib.StrHelper.ToLong(reader["SKCHOGUM"].ToString().TrimEnd()); // 이전심결사항-초진료
                data.SKJAEGUM = MetroLib.StrHelper.ToLong(reader["SKJAEGUM"].ToString().TrimEnd()); // 이전심결사항-재진료
                data.SKTTAMT = MetroLib.StrHelper.ToLong(reader["SKTTAMT"].ToString().TrimEnd()); // 이전심결사항-요양급여비용총액
                data.SKPTAMT = MetroLib.StrHelper.ToLong(reader["SKPTAMT"].ToString().TrimEnd()); // 이전심결사항-본인부담금
                data.SKMAXPTAMT = MetroLib.StrHelper.ToLong(reader["SKMAXPTAMT"].ToString().TrimEnd()); // 이전심결사항-본인부담상한초과금
                data.SKMAXPTCHAAMT = MetroLib.StrHelper.ToLong(reader["SKMAXPTCHAAMT"].ToString().TrimEnd()); // 이전심결사항-본인부담상한액차액
                data.SKUNAMT = MetroLib.StrHelper.ToLong(reader["SKUNAMT"].ToString().TrimEnd()); // 이전심결사항-보험자부담금
                data.SKTTTAMT = MetroLib.StrHelper.ToLong(reader["SKTTTAMT"].ToString().TrimEnd()); // 이전심결사항-진료비총액
                data.SKBHUNAMT = MetroLib.StrHelper.ToLong(reader["SKBHUNAMT"].ToString().TrimEnd()); // 이전심결사항-보훈청구액
                data.SKRSTAMT = MetroLib.StrHelper.ToLong(reader["SKRSTAMT"].ToString().TrimEnd()); // 이전심결사항-심사결정액
                data.SKSUTAKAMT = MetroLib.StrHelper.ToLong(reader["SKSUTAKAMT"].ToString().TrimEnd()); // 이전심결사항-위탁검사직접지급금
                data.SKCHOCNT = MetroLib.StrHelper.ToLong(reader["SKCHOCNT"].ToString().TrimEnd()); // 이전심결사항-초진횟수
                data.SKCNONCNT = MetroLib.StrHelper.ToLong(reader["SKCNONCNT"].ToString().TrimEnd()); // 이전심결사항-초진가산횟수(1일분투약횟수)
                data.SKJAECNT = MetroLib.StrHelper.ToLong(reader["SKJAECNT"].ToString().TrimEnd());   // 이전심결사항-재진횟수(2일분투약횟수)
                data.SKJAENCNT = MetroLib.StrHelper.ToLong(reader["SKJAENCNT"].ToString().TrimEnd()); // 이전심결사항-재진가산횟수(3일분이상투약횟수)
                data.SKEXAMC = MetroLib.StrHelper.ToLong(reader["SKEXAMC"].ToString().TrimEnd()); // 이전심결사항-입원일수(내원일수)
                data.SKORDDAYS = MetroLib.StrHelper.ToLong(reader["SKORDDAYS"].ToString().TrimEnd()); // 이전심결사항-요양급여일수
                data.SKORDCNT = MetroLib.StrHelper.ToLong(reader["SKORDCNT"].ToString().TrimEnd()); // 이전심결사항-처방횟수
                data.JSTJKH = reader["JSTJKH"].ToString().TrimEnd(); // 정산심결사항-특정기호
                data.JSDRGNO = reader["JSDRGNO"].ToString().TrimEnd(); // 정산심결사항-DRG번호
                data.JSPMGUM = MetroLib.StrHelper.ToLong(reader["JSPMGUM"].ToString().TrimEnd()); // 정산심결사항-본인부담환급금
                data.JSPPGUM = MetroLib.StrHelper.ToLong(reader["JSPPGUM"].ToString().TrimEnd()); // 정산심결사항-본인추가부담금
                data.JSCHOGUM = MetroLib.StrHelper.ToLong(reader["JSCHOGUM"].ToString().TrimEnd()); // 정산심결사항-초진료
                data.JSJAEGUM = MetroLib.StrHelper.ToLong(reader["JSJAEGUM"].ToString().TrimEnd()); // 정산심결사항-재진료
                data.JSTTAMT = MetroLib.StrHelper.ToLong(reader["JSTTAMT"].ToString().TrimEnd()); // 정산심결사항-요양급여비용총액
                data.JSPTAMT = MetroLib.StrHelper.ToLong(reader["JSPTAMT"].ToString().TrimEnd()); // 정산심결사항-본인부담금
                data.JSMAXPTAMT = MetroLib.StrHelper.ToLong(reader["JSMAXPTAMT"].ToString().TrimEnd()); // 정산심결사항-본인부담상한초과금
                data.JSMAXPTCHAAMT = MetroLib.StrHelper.ToLong(reader["JSMAXPTCHAAMT"].ToString().TrimEnd()); // 정산심결사항-본인부담상한액차액
                data.JSUNAMT = MetroLib.StrHelper.ToLong(reader["JSUNAMT"].ToString().TrimEnd()); // 정산심결사항-보험자부담금
                data.JSTTTAMT = MetroLib.StrHelper.ToLong(reader["JSTTTAMT"].ToString().TrimEnd()); // 정산심결사항-진료비총액
                data.JSBHUNAMT = MetroLib.StrHelper.ToLong(reader["JSBHUNAMT"].ToString().TrimEnd()); // 정산심결사항-보훈청구액
                data.JSRSTAMT = MetroLib.StrHelper.ToLong(reader["JSRSTAMT"].ToString().TrimEnd()); // 정산심결사항-심사결정액
                data.JSSUTAKAMT = MetroLib.StrHelper.ToLong(reader["JSSUTAKAMT"].ToString().TrimEnd()); // 정산심결사항-위탁검사직접지급금
                data.JSRSTCHAAMT = MetroLib.StrHelper.ToLong(reader["JSRSTCHAAMT"].ToString().TrimEnd()); // 정산심결사항-결정차액
                data.JSCHOCNT = MetroLib.StrHelper.ToLong(reader["JSCHOCNT"].ToString().TrimEnd()); // 정산심결사항-초진횟수
                data.JSCHONCNT = MetroLib.StrHelper.ToLong(reader["JSCHONCNT"].ToString().TrimEnd()); // 정산심결사항-초진가산횟수(1일분투약횟수)
                data.JSJAECNT = MetroLib.StrHelper.ToLong(reader["JSJAECNT"].ToString().TrimEnd()); // 정산심결사항-재진횟수(2일분투약횟수)
                data.JSJAENCNT = MetroLib.StrHelper.ToLong(reader["JSJAENCNT"].ToString().TrimEnd()); // 정산심결사항-재진가산횟수(3일분이상투약횟수)
                data.JSEXAMC = MetroLib.StrHelper.ToLong(reader["JSEXAMC"].ToString().TrimEnd()); // 정산심결사항-입원일수(내원일수)
                data.JSORDDAYS = MetroLib.StrHelper.ToLong(reader["JSORDDAYS"].ToString().TrimEnd()); // 정산심결사항-요양급여일수
                data.JSORDCNT = MetroLib.StrHelper.ToLong(reader["JSORDCNT"].ToString().TrimEnd()); // 정산심결사항-처방횟수
                data.MEMO = reader["MEMO"].ToString().TrimEnd(); // 명일련비고사항
                data.SKGONSGB = reader["SKGONSGB"].ToString().TrimEnd();
                data.SKTSAMT = MetroLib.StrHelper.ToLong(reader["SKTSAMT"].ToString().TrimEnd());
                data.SK100AMT = MetroLib.StrHelper.ToLong(reader["SK100AMT"].ToString().TrimEnd());
                data.SKUISAMT = MetroLib.StrHelper.ToLong(reader["SKUISAMT"].ToString().TrimEnd());
                data.JSGONSGB = reader["JSGONSGB"].ToString().TrimEnd();
                data.JSTSAMT = MetroLib.StrHelper.ToLong(reader["JSTSAMT"].ToString().TrimEnd());
                data.JS100AMT = MetroLib.StrHelper.ToLong(reader["JS100AMT"].ToString().TrimEnd());
                data.JSUISAMT = MetroLib.StrHelper.ToLong(reader["JSUISAMT"].ToString().TrimEnd());
                data.SKJIWONAMT = MetroLib.StrHelper.ToLong(reader["SKJIWONAMT"].ToString().TrimEnd());
                data.JSJIWONAMT = MetroLib.StrHelper.ToLong(reader["JSJIWONAMT"].ToString().TrimEnd());
                data.SKJAM = MetroLib.StrHelper.ToLong(reader["SKJAM"].ToString().TrimEnd());
                data.JSJAM = MetroLib.StrHelper.ToLong(reader["JSJAM"].ToString().TrimEnd());
                data.SKUPLMTCHATTAMT = MetroLib.StrHelper.ToLong(reader["SKUPLMTCHATTAMT"].ToString().TrimEnd());
                data.SKPTTTAMT = MetroLib.StrHelper.ToLong(reader["SKPTTTAMT"].ToString().TrimEnd());
                data.JSUPLMTCHATTAMT = MetroLib.StrHelper.ToLong(reader["JSUPLMTCHATTAMT"].ToString().TrimEnd());
                data.JSPTTTAMT = MetroLib.StrHelper.ToLong(reader["JSPTTTAMT"].ToString().TrimEnd());
                data.SKTSJRAMT = MetroLib.StrHelper.ToLong(reader["SKTSJRAMT"].ToString().TrimEnd());
                data.JSTSJRAMT = MetroLib.StrHelper.ToLong(reader["JSTSJRAMT"].ToString().TrimEnd());
                data.SKBHPMGUM = MetroLib.StrHelper.ToLong(reader["SKBHPMGUM"].ToString().TrimEnd());
                data.JSBHPMGUM = MetroLib.StrHelper.ToLong(reader["JSBHPMGUM"].ToString().TrimEnd());
                data.SKBHPPGUM = MetroLib.StrHelper.ToLong(reader["SKBHPPGUM"].ToString().TrimEnd());
                data.JSBHPPGUM = MetroLib.StrHelper.ToLong(reader["JSBHPPGUM"].ToString().TrimEnd());
                data.SKBHPTAMT = MetroLib.StrHelper.ToLong(reader["SKBHPTAMT"].ToString().TrimEnd());
                data.JSBHPTAMT = MetroLib.StrHelper.ToLong(reader["JSBHPTAMT"].ToString().TrimEnd());
                data.BAKDNSKPMGUM = MetroLib.StrHelper.ToLong(reader["BAKDNSKPMGUM"].ToString().TrimEnd()); // 2014.03.21 KJW - 100/100미만 본인부담금합계(이전)
                data.BAKDNSKPPGUM = MetroLib.StrHelper.ToLong(reader["BAKDNSKPPGUM"].ToString().TrimEnd()); // 2014.03.21 KJW - 100/100미만 본인추가부담금합계
                data.BAKDNSKTTAMT = MetroLib.StrHelper.ToLong(reader["BAKDNSKTTAMT"].ToString().TrimEnd()); // 2014.03.21 KJW - 100/100미만 총액합계
                data.BAKDNSKPTAMT = MetroLib.StrHelper.ToLong(reader["BAKDNSKPTAMT"].ToString().TrimEnd()); // 2014.03.21 KJW - 100/100미만 본인일부부담금합계
                data.BAKDNSKUNAMT = MetroLib.StrHelper.ToLong(reader["BAKDNSKUNAMT"].ToString().TrimEnd()); // 2014.03.21 KJW - 100/100미만 청구액합계
                data.BAKDNSKBHUNAMT = MetroLib.StrHelper.ToLong(reader["BAKDNSKBHUNAMT"].ToString().TrimEnd()); // 2014.03.21 KJW - 100/100미만 보훈청구액합계
                data.BAKDNJSPMGUM = MetroLib.StrHelper.ToLong(reader["BAKDNJSPMGUM"].ToString().TrimEnd()); // 2014.03.21 KJW - 100/100미만 본인부담금합계(정산)
                data.BAKDNJSPPGUM = MetroLib.StrHelper.ToLong(reader["BAKDNJSPPGUM"].ToString().TrimEnd()); // 2014.03.21 KJW - 100/100미만 본인추가부담금합계
                data.BAKDNJSTTAMT = MetroLib.StrHelper.ToLong(reader["BAKDNJSTTAMT"].ToString().TrimEnd()); // 2014.03.21 KJW - 100/100미만 총액합계
                data.BAKDNJSPTAMT = MetroLib.StrHelper.ToLong(reader["BAKDNJSPTAMT"].ToString().TrimEnd()); // 2014.03.21 KJW - 100/100미만 본인일부부담금합계
                data.BAKDNJSUNAMT = MetroLib.StrHelper.ToLong(reader["BAKDNJSUNAMT"].ToString().TrimEnd()); // 2014.03.21 KJW - 100/100미만 청구액합계
                data.BAKDNJSBHUNAMT = MetroLib.StrHelper.ToLong(reader["BAKDNJSBHUNAMT"].ToString().TrimEnd()); // 2014.03.21 KJW - 100/100미만 보훈청구액합계
                data.SKSANGKYERFAMT = MetroLib.StrHelper.ToLong(reader["SKSANGKYERFAMT"].ToString().TrimEnd()); // 2014.09.24 KJW - 상계환급금(이전)
                data.SKSANGKYEADDAMT = MetroLib.StrHelper.ToLong(reader["SKSANGKYEADDAMT"].ToString().TrimEnd()); // 2014.09.24 KJW - 상계추가부담금
                data.JSSANGKYERFAMT = MetroLib.StrHelper.ToLong(reader["JSSANGKYERFAMT"].ToString().TrimEnd()); // 2014.09.24 KJW - 상계환급금(정산)
                data.JSSANGKYEADDAMT = MetroLib.StrHelper.ToLong(reader["JSSANGKYEADDAMT"].ToString().TrimEnd()); // 2014.09.24 KJW - 상계추가부담금
                data.SKBAKAMT = MetroLib.StrHelper.ToLong(reader["SKBAKAMT"].ToString().TrimEnd()); // 2023.04.07 WOOIL - 이전심사 - 건강보험100/100 본인부담총액
                data.JSBAKAMT = MetroLib.StrHelper.ToLong(reader["JSBAKAMT"].ToString().TrimEnd()); // 2023.04.07 WOOIL - 정산심사 - 건강보험100/100 본인부담총액
                data.DEMNO = reader["DEMNO"].ToString().TrimEnd(); // 2023.04.07 WOOIL - 청구번호

                list.Add(data);

                return true;
            });

            string[] check_field =
            {
                "SKPMGUM", "JSPMGUM", // 본인부담환급금
                "SKPPGUM", "JSPPGUM", // 본인추가부담금
                "SKCHOGUM", "JSCHOGUM", // 초진료
                "SKJAEGUM", "JSJAEGUM", // 재진료
                "SKTTAMT", "JSTTAMT", // 요양급여비용총액
                "SKPTAMT", "JSPTAMT", // 본인부담금
                "SKMAXPTAMT", "JSMAXPTAMT", // 본인부담상한초과금
                "SKMAXPTCHAAMT", "JSMAXPTCHAAMT", // 본인부담상한액차액
                "SKUNAMT", "JSUNAMT", // 보험자부담금
                "SKTTTAMT", "JSTTTAMT", // 진료비총액
                "SKBHUNAMT", "JSBHUNAMT", // 보훈청구액
                "SKRSTAMT", "JSRSTAMT", // 심사결정액
                "SKSUTAKAMT", "JSSUTAKAMT", // 위탁검사직접지급금
                "SKCHOCNT", "JSCHOCNT", // 초진횟수
                "SKCNONCNT", "JSCHONCNT", // 초진가산횟수(1일분투약횟수)
                "SKJAECNT", "JSJAECNT", // 재진횟수(2일분투약횟수)
                "SKJAENCNT", "JSJAENCNT", // 재진가산횟수(3일분이상투약횟수)
                "SKEXAMC", "JSEXAMC", // 입원일수(내원일수)
                "SKORDDAYS", "JSORDDAYS", // 요양급여일수
                "SKORDCNT", "JSORDCNT", // 처방횟수
                "SKTSAMT", "JSTSAMT", // 
                "SK100AMT", "JS100AMT", //
                "SKUISAMT", "JSUISAMT", //
                "SKJIWONAMT", "JSJIWONAMT", //
                "SKJAM", "JSJAM", //
                "SKUPLMTCHATTAMT", "JSUPLMTCHATTAMT", //
                "SKPTTTAMT", "JSPTTTAMT", //
                "SKTSJRAMT", "JSTSJRAMT", //
                "SKBHPMGUM", "JSBHPMGUM", //
                "SKBHPPGUM", "JSBHPPGUM", //
                "SKBHPTAMT", "JSBHPTAMT", //
                "BAKDNSKPMGUM", "BAKDNJSPMGUM", // 100/100미만 본인부담환급금합계
                "BAKDNSKPPGUM", "BAKDNJSPPGUM", // 100/100미만 본인추가부담금합계
                "BAKDNSKTTAMT", "BAKDNJSTTAMT", // 100/100미만 총액합계
                "BAKDNSKPTAMT", "BAKDNJSPTAMT", // 100/100미만 본인일부부담금합계
                "BAKDNSKUNAMT", "BAKDNJSUNAMT", // 100/100미만 청구액합계
                "BAKDNSKBHUNAMT", "BAKDNJSBHUNAMT", // 100/100미만 보훈청구액합계
                "SKSANGKYERFAMT", "JSSANGKYERFAMT", // 상계환급금
                "SKSANGKYEADDAMT", "JSSANGKYEADDAMT", // 상계추가부담금
                "SKBAKAMT", "JSBAKAMT", // 건강보험100/100 본인부담총액
            };
            DxLib.GridHelper.HideColumnIfZeroInBand(check_field, grdPtntView);

            //string[] check_field1 = { "JJUPLMTCHATTAMT" }; // 상한차액조정금액
            //HideColumnIfZero(check_field1, grdPtntView);

            DxLib.GridHelper.SetBandVisible(grdPtntView);
        }

        private void grdPtnt_Load(object sender, EventArgs e)
        {
            // 밴드명 변경
            string[] caption = {"본인부담환급금","본인부담","환급금",
                                "보훈본인부담환급금","보훈본인부담","환급금",
                                "100미만본인부담환급금","100미만본인부담","환급금",
                                "본인추가부담금","본인","추가부담금",
                                "보훈본인추가부담금","보훈본인","추가부담금",
                                "100미만본인추가부담금","100미만본인","추가부담금",
                                "상계추가부담금","상계","추가부담금",
                                "요양급여비용총액","요양급여","비용총액",
                                "본인부담상한액초과금","본인부담","상한액초과금",
                                "본인부담상한액차액","본인부담","상한액차액",
                                "위탁검사직접지급금","위탁검사","직접지급금",
                                "보훈본인일부부담금","보훈","본인일부부담금",
                                "100미만총액","100미만","총액",
                                "100미만본인일부부담금","100미만","본인일부부담금",
                                "100미만청구액","100미만","청구액",
                                "100미만보훈청구액","100미만","보훈청구액",
                                "초진횟수","초진","횟수",
                                "초진가산횟수","초진가산","횟수",
                                "재진횟수","재진","횟수",
                                "재진가산횟수","재진가산","횟수",
                                "입내원일수","입내원","일수",
                                "요양급여일수","요양급여","일수",
                                "처방횟수","처방","횟수",
                                "특정기호","특정","기호",
                                "공상구분","공상","구분",
                               };
            DxLib.GridHelper.BandReCaption(grdPtntView, caption);

            grdPtntView.OptionsPrint.AutoWidth = false;
        }

        private void grdCode_Load(object sender, EventArgs e)
        {
            // 밴드명 변경
            string[] caption = {"약제상한차액조정금액","약제상한차액","조정금액",
                                "약제상한차액인정금액","약제상한차액","인정금액",
                               };
            DxLib.GridHelper.BandReCaption(grdCodeView, caption);
        }

        private void QueryCode(string p_jsdemseq, string p_jsreday, string p_cnecno, string p_dcount, string p_jsyyseq, string p_demno, OleDbConnection p_conn)
        {
            List<CDataCode> list = new List<CDataCode>();
            grdCode.DataSource = null;
            grdCode.DataSource = list;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT F0404.JSDEMSEQ"; // 정산심사차수
            sql += Environment.NewLine + "     , F0404.JSREDAY"; // 정산통보일자
            sql += Environment.NewLine + "     , F0404.CNECNO"; // 접수번호
            sql += Environment.NewLine + "     , F0404.DCOUNT"; // 청구서 일련번호
            sql += Environment.NewLine + "     , F0404.JSYYSEQ"; // 정산연번
            sql += Environment.NewLine + "     , F0404.JSSEQNO"; // 정산일련번호
            sql += Environment.NewLine + "     , F0404.EPRTNO"; // 명세서 일련번호
            sql += Environment.NewLine + "     , F0404.LNO"; // 줄번호
            sql += Environment.NewLine + "     , F0404.SKJJRMK"; // 이전심결사항-조정사유
            sql += Environment.NewLine + "     , F0404.SKJJRMK2"; // 이전심결사항-조정사유상세
            sql += Environment.NewLine + "     , F0404.SKGUBUN"; // 이전심결사항-1,2구분
            sql += Environment.NewLine + "     , F0404.SKBGIHO"; // 이전심결사항-코드
            sql += Environment.NewLine + "     , F0404.SKDANGA"; // 이전심결사항-단가
            sql += Environment.NewLine + "     , F0404.SKDQTY"; // 이전심결사항-일투 인정횟수
            sql += Environment.NewLine + "     , F0404.SKDDAY"; // 이전심결사항-총투 인정횟수
            sql += Environment.NewLine + "     , F0404.SKIJAMT"; // 이전심결사항-인정금액
            sql += Environment.NewLine + "     , F0404.SKJJAMT"; // 이전심결사항-조정금액
            sql += Environment.NewLine + "     , F0404.SKJJSAU"; // 이전심결사항-관련근거
            sql += Environment.NewLine + "     , F0404.JSJJRMK"; // 정산심결사항-조정사유
            sql += Environment.NewLine + "     , F0404.JSJJRMK2"; // 정산심결사항-조정사유상세
            sql += Environment.NewLine + "     , F0404.JSBGIHO"; // 정산심결사항-코드
            sql += Environment.NewLine + "     , F0404.JSDANGA"; // 정산심결사항-단가
            sql += Environment.NewLine + "     , F0404.JSDQTY"; // 정산심결사항-일투 인정횟수
            sql += Environment.NewLine + "     , F0404.JSDDAY"; // 정산심결사항-총투 인정횟수
            sql += Environment.NewLine + "     , F0404.JSIJAMT"; // 정산심결사항-인정금액
            sql += Environment.NewLine + "     , F0404.JSJJAMT"; // 정산심결사항-조정금액
            sql += Environment.NewLine + "     , F0404.JSMEMO"; // 정산심결사항-비고(조정사유내역)
            sql += Environment.NewLine + "     , F0403.PNM"; // 수진자명
            sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE = F0404.SKBGIHO) AS SKBGIHONM";
            sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE = F0404.JSBGIHO) AS JSBGIHONM";
            sql += Environment.NewLine + "     , F0404.SKCNTQTY";
            sql += Environment.NewLine + "     , F0404.JSCNTQTY";
            sql += Environment.NewLine + "     , F0404.SKUPLMTAMT";
            sql += Environment.NewLine + "     , F0404.SKIJUPLMTCHAAMT";
            sql += Environment.NewLine + "     , F0404.SKJJUPLMTCHAAMT";
            sql += Environment.NewLine + "     , F0404.JSUPLMTAMT";
            sql += Environment.NewLine + "     , F0404.JSIJUPLMTCHAAMT";
            sql += Environment.NewLine + "     , F0404.JSJJUPLMTCHAAMT";
            sql += Environment.NewLine + "     , F0404.HANGNO";
            sql += Environment.NewLine + "     , F0401.DEMNO";
            sql += Environment.NewLine + "  FROM TIE_F0404 F0404 INNER JOIN TIE_F0403 F0403 ON F0403.JSDEMSEQ=F0404.JSDEMSEQ AND F0403.JSREDAY=F0404.JSREDAY AND F0403.CNECNO=F0404.CNECNO AND F0403.DCOUNT=F0404.DCOUNT AND F0403.JSYYSEQ=F0404.JSYYSEQ AND F0403.JSSEQNO=F0404.JSSEQNO AND F0403.EPRTNO=F0404.EPRTNO";
            sql += Environment.NewLine + "                       INNER JOIN TIE_F0401 F0401 ON F0401.JSDEMSEQ=F0404.JSDEMSEQ AND F0401.JSREDAY=F0404.JSREDAY AND F0401.CNECNO=F0404.CNECNO AND F0401.DCOUNT=F0404.DCOUNT";

            if (cboDQOption.SelectedIndex == 1)
            {
                // 청구번호별
                sql += Environment.NewLine + " WHERE F0401.DEMNO = '" + p_demno + "' ";
            }
            else if (cboDQOption.SelectedIndex == 2)
            {
                // 청구월별
                sql += Environment.NewLine + " WHERE F0401.DEMNO LIKE '" + p_demno.Substring(0, 6) + "%' ";
            }
            else if (cboDQOption.SelectedIndex == 3)
            {
                // 청구년도별
                sql += Environment.NewLine + " WHERE F0401.DEMNO LIKE '" + p_demno.Substring(0, 4) + "%' ";
            }
            else
            {
                sql += Environment.NewLine + " WHERE F0404.JSDEMSEQ = '" + p_jsdemseq + "'";
                sql += Environment.NewLine + "   AND F0404.JSREDAY  = '" + p_jsreday + "'";
                sql += Environment.NewLine + "   AND F0404.CNECNO   = '" + p_cnecno + "'";
                sql += Environment.NewLine + "   AND F0404.DCOUNT   = '" + p_dcount + "'";
                sql += Environment.NewLine + "   AND F0404.JSYYSEQ  = '" + p_jsyyseq + "'";
            }
            sql += Environment.NewLine + " ORDER BY F0404.JSDEMSEQ,F0404.JSREDAY,F0404.CNECNO,F0404.DCOUNT,F0404.JSYYSEQ,F0404.JSSEQNO,F0404.EPRTNO,F0404.LNO";

            long no = 0;
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                CDataCode data = new CDataCode();
                data.Clear();

                data.NO = (++no);

                data.JSDEMSEQ = reader["JSDEMSEQ"].ToString().TrimEnd(); // 정산심사차수
                data.JSREDAY = reader["JSREDAY"].ToString().TrimEnd(); // 정산통보일자
                data.CNECNO = reader["CNECNO"].ToString().TrimEnd(); // 접수번호
                data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd(); // 청구서 일련번호
                data.JSYYSEQ = reader["JSYYSEQ"].ToString().TrimEnd(); // 정산연번
                data.JSSEQNO = reader["JSSEQNO"].ToString().TrimEnd(); // 정산일련번호
                data.EPRTNO = reader["EPRTNO"].ToString().TrimEnd(); // 명세서 일련번호
                data.LNO = reader["LNO"].ToString().TrimEnd(); // 줄번호
                data.SKJJRMK = reader["SKJJRMK"].ToString().TrimEnd(); // 이전심결사항-조정사유
                data.SKJJRMK2 = reader["SKJJRMK2"].ToString().TrimEnd(); // 이전심결사항-조정사유상세
                data.SKGUBUN = reader["SKGUBUN"].ToString().TrimEnd(); // 이전심결사항-1,2구분
                data.SKBGIHO = reader["SKBGIHO"].ToString().TrimEnd(); // 이전심결사항-코드
                data.SKDANGA = MetroLib.StrHelper.ToLong(reader["SKDANGA"].ToString().TrimEnd()); // 이전심결사항-단가
                data.SKDQTY = MetroLib.StrHelper.ToDouble(reader["SKDQTY"].ToString().TrimEnd()); // 이전심결사항-일투 인정횟수
                data.SKDDAY = MetroLib.StrHelper.ToLong(reader["SKDDAY"].ToString().TrimEnd()); // 이전심결사항-총투 인정횟수
                data.SKIJAMT = MetroLib.StrHelper.ToLong(reader["SKIJAMT"].ToString().TrimEnd()); // 이전심결사항-인정금액
                data.SKJJAMT = MetroLib.StrHelper.ToLong(reader["SKJJAMT"].ToString().TrimEnd()); // 이전심결사항-조정금액
                data.SKJJSAU = reader["SKJJSAU"].ToString().TrimEnd(); // 이전심결사항-관련근거
                data.JSJJRMK = reader["JSJJRMK"].ToString().TrimEnd(); // 정산심결사항-조정사유
                data.JSJJRMK2 = reader["JSJJRMK2"].ToString().TrimEnd(); // 정산심결사항-조정사유상세
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
                data.HANGNO = reader["HANGNO"].ToString().TrimEnd(); // 항번호
                data.DEMNO = reader["DEMNO"].ToString().TrimEnd(); // 청구번호

                list.Add(data);

                return true;
            });
            string[] check_field =
            {
                "SKUPLMTAMT", "JSUPLMTAMT", // 약제상한차액
                "SKIJUPLMTCHAAMT", "JSIJUPLMTCHAAMT", // 약제상한차액조정금액
                "SKJJUPLMTCHAAMT", "JSJJUPLMTCHAAMT", // 약제상한차액인정금액
            };
            DxLib.GridHelper.HideColumnIfZeroInBand(check_field, grdCodeView);

            DxLib.GridHelper.SetBandVisible(grdCodeView);
        }

        private void grdBonrt_Load(object sender, EventArgs e)
        {
            // 밴드명 변경
            string[] caption = {"조정사유","조정","사유",
                               };
            DxLib.GridHelper.BandReCaption(grdBonrtView, caption);
        }

        private void QueryBonrt(string p_jsdemseq, string p_jsreday, string p_cnecno, string p_dcount, string p_jsyyseq, string p_demno, OleDbConnection p_conn)
        {
            List<CDataBonrt> list = new List<CDataBonrt>();
            grdBonrt.DataSource = null;
            grdBonrt.DataSource = list;

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT F0406.JSDEMSEQ"; // 정산심사차수
            sql += Environment.NewLine + "     , F0406.JSREDAY"; // 정산통보일자
            sql += Environment.NewLine + "     , F0406.CNECNO"; // 접수번호
            sql += Environment.NewLine + "     , F0406.DCOUNT"; // 청구서 일련번호
            sql += Environment.NewLine + "     , F0406.JSYYSEQ"; // 정산연번
            sql += Environment.NewLine + "     , F0406.JSSEQNO"; // 정산일련번호
            sql += Environment.NewLine + "     , F0406.EPRTNO"; // 명세서 일련번호
            sql += Environment.NewLine + "     , F0406.LNO"; // 줄번호
            sql += Environment.NewLine + "     , F0406.SKJKRTBKRMK"; // 이전심결사항-본인부담률변경사유
            sql += Environment.NewLine + "     , F0406.SKHANGNO"; // 이전심결사항-항번호
            sql += Environment.NewLine + "     , F0406.SKBGIHO"; // 이전심결사항-코드
            sql += Environment.NewLine + "     , F0406.SKDANGA"; // 이전심결사항-단가
            sql += Environment.NewLine + "     , F0406.SKCNT"; // 이전심결사항-1회투약 인정횟수
            sql += Environment.NewLine + "     , F0406.SKDQTY"; // 이전심결사항-일투 인정횟수
            sql += Environment.NewLine + "     , F0406.SKDDAY"; // 이전심결사항-총투 인정횟수
            sql += Environment.NewLine + "     , F0406.SKJKRTIJAMT"; // 이전심결사항-본인부담률변경 인정금액
            sql += Environment.NewLine + "     , F0406.JSJKRTBKRMK"; // 정산심결사항-본인부담률변경사유
            sql += Environment.NewLine + "     , F0406.JSHANGNO"; // 정산심결사항-항번호
            sql += Environment.NewLine + "     , F0406.JSBGIHO"; // 정산심결사항-코드
            sql += Environment.NewLine + "     , F0406.JSDANGA"; // 정산심결사항-단가
            sql += Environment.NewLine + "     , F0406.JSCNT"; // 정산심결사항-1회투약 인정횟수
            sql += Environment.NewLine + "     , F0406.JSDQTY"; // 정산심결사항-일투 인정횟수
            sql += Environment.NewLine + "     , F0406.JSDDAY"; // 정산심결사항-총투 인정횟수
            sql += Environment.NewLine + "     , F0406.JSJKRTIJAMT"; // 정산심결사항-본인부담률변경 인정금액
            sql += Environment.NewLine + "     , F0406.MEMO"; // 정산심결사항-비고(본인부담률변경내역)
            sql += Environment.NewLine + "     , F0403.PNM"; // 수진자명
            sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE = F0406.SKBGIHO) AS SKBGIHONM";
            sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE = F0406.JSBGIHO) AS JSBGIHONM";
            sql += Environment.NewLine + "     , F0401.DEMNO";
            sql += Environment.NewLine + "  FROM TIE_F0406 F0406 INNER JOIN TIE_F0403 F0403 ON F0403.JSDEMSEQ=F0406.JSDEMSEQ AND F0403.JSREDAY=F0406.JSREDAY AND F0403.CNECNO=F0406.CNECNO AND F0403.DCOUNT=F0406.DCOUNT AND F0403.JSYYSEQ=F0406.JSYYSEQ AND F0403.JSSEQNO=F0406.JSSEQNO AND F0403.EPRTNO=F0406.EPRTNO";
            sql += Environment.NewLine + "                       INNER JOIN TIE_F0401 F0401 ON F0401.JSDEMSEQ=F0406.JSDEMSEQ AND F0401.JSREDAY=F0406.JSREDAY AND F0401.CNECNO=F0406.CNECNO AND F0401.DCOUNT=F0406.DCOUNT";

            if (cboDQOption.SelectedIndex == 1)
            {
                //청구번호별
                sql += Environment.NewLine + " WHERE F0401.DEMNO = '" + p_demno + "' ";
            }
            else if (cboDQOption.SelectedIndex == 2)
            {
                // 청구월별
                sql += Environment.NewLine + " WHERE F0401.DEMNO LIKE '" + p_demno.Substring(0, 6) + "%' ";
            }
            else if (cboDQOption.SelectedIndex == 3)
            {
                // 청구년도별
                sql += Environment.NewLine + " WHERE F0401.DEMNO LIKE '" + p_demno.Substring(0, 4) + "%' ";
            }
            else
            {
                sql += Environment.NewLine + " WHERE F0406.JSDEMSEQ = '" + p_jsdemseq + "'";
                sql += Environment.NewLine + "   AND F0406.JSREDAY  = '" + p_jsreday + "'";
                sql += Environment.NewLine + "   AND F0406.CNECNO   = '" + p_cnecno + "'";
                sql += Environment.NewLine + "   AND F0406.DCOUNT   = '" + p_dcount + "'";
                sql += Environment.NewLine + "   AND F0406.JSYYSEQ  = '" + p_jsyyseq + "'";
            }

            sql += Environment.NewLine + " ORDER BY F0406.JSDEMSEQ,F0406.JSREDAY,F0406.CNECNO,F0406.DCOUNT,F0406.JSYYSEQ,F0406.JSSEQNO,F0406.EPRTNO,F0406.LNO";

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
                data.LNO = reader["LNO"].ToString().TrimEnd(); // 줄번호
                data.SKJKRTBKRMK = reader["SKJKRTBKRMK"].ToString().TrimEnd(); // 이전심결사항-본인부담률변경사유
                data.SKHANGNO = reader["SKHANGNO"].ToString().TrimEnd(); // 이전심결사항-항번호
                data.SKBGIHO = reader["SKBGIHO"].ToString().TrimEnd(); // 이전심결사항-코드
                data.SKDANGA = reader["SKDANGA"].ToString().TrimEnd(); // 이전심결사항-단가
                data.SKCNT = reader["SKCNT"].ToString().TrimEnd(); // 이전심결사항-1회투약 인정횟수
                data.SKDQTY = reader["SKDQTY"].ToString().TrimEnd(); // 이전심결사항-일투 인정횟수
                data.SKDDAY = reader["SKDDAY"].ToString().TrimEnd(); // 이전심결사항-총투 인정횟수
                data.SKJKRTIJAMT = reader["SKJKRTIJAMT"].ToString().TrimEnd(); // 이전심결사항-본인부담률변경 인정금액
                data.JSJKRTBKRMK = reader["JSJKRTBKRMK"].ToString().TrimEnd(); // 정산심결사항-본인부담률변경사유
                data.JSHANGNO = reader["JSHANGNO"].ToString().TrimEnd(); // 정산심결사항-항번호
                data.JSBGIHO = reader["JSBGIHO"].ToString().TrimEnd(); // 정산심결사항-코드
                data.JSDANGA = reader["JSDANGA"].ToString().TrimEnd(); // 정산심결사항-단가
                data.JSCNT = reader["JSCNT"].ToString().TrimEnd(); // 정산심결사항-1회투약 인정횟수
                data.JSDQTY = reader["JSDQTY"].ToString().TrimEnd(); // 정산심결사항-일투 인정횟수
                data.JSDDAY = reader["JSDDAY"].ToString().TrimEnd(); // 정산심결사항-총투 인정횟수
                data.JSJKRTIJAMT = reader["JSJKRTIJAMT"].ToString().TrimEnd(); // 정산심결사항-본인부담률변경 인정금액
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
            sql += Environment.NewLine + "SELECT F0405.JSDEMSEQ"; // 정산심사차수
            sql += Environment.NewLine + "     , F0405.JSREDAY"; // 정산통보일자
            sql += Environment.NewLine + "     , F0405.CNECNO"; // 접수번호
            sql += Environment.NewLine + "     , F0405.DCOUNT"; // 청구서 일련번호
            sql += Environment.NewLine + "     , F0405.JSYYSEQ"; // 정산연번
            sql += Environment.NewLine + "     , F0405.JSSEQNO"; // 정산일련번호
            sql += Environment.NewLine + "     , F0405.EPRTNO"; // 명세서 일련번호
            sql += Environment.NewLine + "     , F0405.LNO"; // 줄번호
            sql += Environment.NewLine + "     , F0405.SUTAKID"; // 수탁기관기호
            sql += Environment.NewLine + "     , F0405.SUTAKAMT"; // 위탁검사직접지급금
            sql += Environment.NewLine + "     , F0405.OPRCD"; // 처리코드(미사용)
            sql += Environment.NewLine + "     , F0405.MEMO"; // 비고사항
            sql += Environment.NewLine + "     , F0401.DEMNO";
            sql += Environment.NewLine + "  FROM TIE_F0405 F0405 INNER JOIN TIE_F0401 F0401 ON F0401.JSDEMSEQ=F0405.JSDEMSEQ AND F0401.JSREDAY=F0405.JSREDAY AND F0401.CNECNO=F0405.CNECNO AND F0401.DCOUNT=F0405.DCOUNT";

            if (cboDQOption.SelectedIndex == 1)
            {
                // 청구번호별
                sql += Environment.NewLine + " WHERE F0401.DEMNO = '" + p_demno + "'";
            }
            else if (cboDQOption.SelectedIndex == 2)
            {
                // 청구월별
                sql += Environment.NewLine + " WHERE F0401.DEMNO LIKE '" + p_demno.Substring(0, 6) + "%'";
            }
            else if (cboDQOption.SelectedIndex == 3)
            {
                // 청구년도별
                sql += Environment.NewLine + " WHERE F0401.DEMNO LIKE '" + p_demno.Substring(0, 4) + "%'";
            }
            else
            {
                sql += Environment.NewLine + " WHERE F0405.JSDEMSEQ = '" + p_jsdemseq + "'";
                sql += Environment.NewLine + "   AND F0405.JSREDAY  = '" + p_jsreday + "'";
                sql += Environment.NewLine + "   AND F0405.CNECNO   = '" + p_cnecno + "'";
                sql += Environment.NewLine + "   AND F0405.DCOUNT   = '" + p_dcount + "'";
                sql += Environment.NewLine + "   AND F0405.JSYYSEQ  = '" + p_jsyyseq + "'";
            }
            sql += Environment.NewLine + " ORDER BY F0405.JSDEMSEQ, F0405.JSREDAY, F0405.CNECNO, F0405.DCOUNT, F0405.JSYYSEQ, F0405.JSSEQNO, F0405.EPRTNO, F0405.LNO";

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
            String strTitle = "[보험]정산심사내역서";
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
            e.Graph.DrawString("ADD0720E", Color.Black, new RectangleF(0, 0, 120, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(sysDate + " " + sysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }
    }
}
