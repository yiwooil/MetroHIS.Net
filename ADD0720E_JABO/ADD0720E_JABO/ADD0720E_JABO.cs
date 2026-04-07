using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADD0720E_JABO
{
    public partial class ADD0720E_JABO : Form
    {
        private String m_User;
        private String m_Pwd;
        private String m_Prjcd;
        private String m_Addpara;
        private String m_HospMulti;

        public ADD0720E_JABO()
        {
            InitializeComponent();

            m_User = "";
            m_Pwd = "";
            m_Prjcd = "";
            m_Addpara = "";
            m_HospMulti = "";
        }

        public ADD0720E_JABO(String user, String pwd, String prjcd, String addpara)
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

        private void ADD0720E_JABO_Load(object sender, EventArgs e)
        {
            cboQueryOption.SelectedIndex = 0;
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
            bool isMemo = false; // 메모가 있는지 여부
            bool isJbunicd = false; // 손보사가 있는지 여부
            CData sum = new CData();
            sum.Clear();

            CData sum2 = new CData();
            sum2.Clear();

            List<CData> list = new List<CData>();
            grdMain.DataSource = null;
            grdMain.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                SetFrdtTodt(queryOption, conn);

                string sql = "";
                sql += Environment.NewLine + "SELECT N1.VERSION         "; // 버전구분
                sql += Environment.NewLine + "     , N1.JBUNICD         "; // 보험회사코드
                sql += Environment.NewLine + "     , N1.JSDEMSEQ        "; // 정산심사차수
                sql += Environment.NewLine + "     , N1.JSREDAY         "; // 정산통보일자
                sql += Environment.NewLine + "     , N1.CNECNO          "; // 접수번호
                sql += Environment.NewLine + "     , N1.DCOUNT          "; // 청구서 일련번호
                sql += Environment.NewLine + "     , N1.FMNO            "; // 서식번호
                sql += Environment.NewLine + "     , N1.HOSID           "; // 요양기관 기호
                sql += Environment.NewLine + "     , N1.DEMSEQ          "; // 심사차수(yyyymm+seq(2))
                sql += Environment.NewLine + "     , N1.DEMNO           "; // 청구번호
                sql += Environment.NewLine + "     , N1.GRPNO           "; // 묶음번호
                sql += Environment.NewLine + "     , N1.DEMUNITFG       "; // 청구단위구분
                sql += Environment.NewLine + "     , N1.JRFG            "; // 보험자종별구분
                sql += Environment.NewLine + "     , N2.JSYYSEQ         "; // 정산연번
                sql += Environment.NewLine + "     , N2.SIMGBN          "; // 심사구분
                sql += Environment.NewLine + "     , N2.JSREDPT1        "; // 정산담당부명
                sql += Environment.NewLine + "     , N2.JSREDPT2        "; // 정산담당조명
                sql += Environment.NewLine + "     , N2.JSREDPNM        "; // 정산담당자명
                sql += Environment.NewLine + "     , N2.JSREDPNO        "; // 정산담당자번호
                sql += Environment.NewLine + "     , N2.JSRETELE        "; // 정산담당자전화번호
                sql += Environment.NewLine + "     , N2.JSBUSSCD        "; // 정산업무코드
                sql += Environment.NewLine + "     , N2.JSBUSSNM        "; // 정산업무명
                sql += Environment.NewLine + "     , N2.SKTTTAMT        "; // 이전심결사항-진료비총액 합계
                sql += Environment.NewLine + "     , N2.SKRSTAMT        "; // 이전심결사항-심사결정액 합계
                sql += Environment.NewLine + "     , N2.SKSUTAKAMT      "; // 이전심결사항-위탁검사직접지급금 합계
                sql += Environment.NewLine + "     , N2.SKUPLMTCHATTAMT "; // 이전심결사항-약제상한차액총액 합계
                sql += Environment.NewLine + "     , N2.SKJBPTAMT       "; // 이전심결사항-환자납부총액 합계
                sql += Environment.NewLine + "     , N2.SKCNT           "; // 이전심결사항-건수합계
                sql += Environment.NewLine + "     , N2.JSTTTAMT        "; // 정산심결사항-진료비총액 합계
                sql += Environment.NewLine + "     , N2.JSRSTAMT        "; // 정산심결사항-심사결정액 합계
                sql += Environment.NewLine + "     , N2.JSSUTAKAMT      "; // 정산심결사항-위탁검사직접지급금 합계
                sql += Environment.NewLine + "     , N2.JSUPLMTCHATTAMT "; // 정산심결사항-약제상한차액총액 합계
                sql += Environment.NewLine + "     , N2.JSJBPTAMT       "; // 정산심결사항-환자납부총액 합계
                sql += Environment.NewLine + "     , N2.JSRSTCHAAMT     "; // 정산심결사항-결정차액 합계
                sql += Environment.NewLine + "     , N2.JSCNT           "; // 정산심결사항-건수 합계
                sql += Environment.NewLine + "     , N2.SKCDJS          "; // 이전심결사항-차등지수
                sql += Environment.NewLine + "     , N2.SKCJJCNT        "; // 이전심결사항-진찰(조제)횟수
                sql += Environment.NewLine + "     , N2.SKCJDAYS        "; // 이전심결사항-진료(조제)일수
                sql += Environment.NewLine + "     , N2.SKCJDR          "; // 이전심결사항-의(약)사수
                sql += Environment.NewLine + "     , N2.SKCJJAMT        "; // 이전심결사항-진찰(조제)료
                sql += Environment.NewLine + "     , N2.SKCJJMAMT       "; // 이전심결사항-진찰료(조제료)차감액
                sql += Environment.NewLine + "     , N2.JSCDJS          "; // 정산심결사항-차등지수
                sql += Environment.NewLine + "     , N2.JSCJJCNT        "; // 정산심결사항-진찰(조제)횟수
                sql += Environment.NewLine + "     , N2.JSCJDAYS        "; // 정산심결사항-진료(조제)일수
                sql += Environment.NewLine + "     , N2.JSCJDR          "; // 정산심결사항-의(약)사수
                sql += Environment.NewLine + "     , N2.JSCJJAMT        "; // 정산심결사항-진찰(조제)료
                sql += Environment.NewLine + "     , N2.JSCJJMAMT       "; // 정산심결사항-진찰료(조제료)차감액
                sql += Environment.NewLine + "     , N2.JSCJRSTCHAAMT   "; // 정산심결사항-차등지수결정차액
                sql += Environment.NewLine + "     , N2.MEMO            ";  //' 명일련비고사항
                sql += Environment.NewLine + "  FROM TIE_N0401 N1 INNER JOIN TIE_N0402 N2 ON N2.JSDEMSEQ = N1.JSDEMSEQ ";
                sql += Environment.NewLine + "                                           AND N2.JSREDAY  = N1.JSREDAY  ";
                sql += Environment.NewLine + "                                           AND N2.CNECNO   = N1.CNECNO   ";
                sql += Environment.NewLine + "                                           AND N2.DCOUNT   = N1.DCOUNT   ";
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
                sql += Environment.NewLine + " ORDER BY N1.JSREDAY DESC,N1.JSDEMSEQ";

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CData data = new CData();
                    data.Clear();
                    data.SKJSDIV = 0;

                    data.JSDEMSEQ = reader["JSDEMSEQ"].ToString();           // 정산심사차수
                    data.JSREDAY = reader["JSREDAY"].ToString();             // 정산통보일자
                    data.DEMSEQ = reader["DEMSEQ"].ToString();               // 심사차수(yyyymm+seq(2))
                    data.CNECNO = reader["CNECNO"].ToString();               // 접수번호
                    data.DEMNO = reader["DEMNO"].ToString();                 // 청구번호
                    data.SIMGBN = reader["SIMGBN"].ToString();               // 심사구분

                    data.SKTTTAMT = ToLong(reader["SKTTTAMT"].ToString());              // 이전심결사항-진료비총액 합계
                    data.SKRSTAMT = ToLong(reader["SKRSTAMT"].ToString());              // 이전심결사항-심사결정액 합계
                    data.SKSUTAKAMT = ToLong(reader["SKSUTAKAMT"].ToString());          // 이전심결사항-위탁검사직접지급금 합계
                    data.SKRSTCHAAMT = 0;                                               // ***** 운율을 맞추기위한용도임.
                    data.SKUPLMTCHATTAMT = ToLong(reader["SKUPLMTCHATTAMT"].ToString());// 약제상한차액총액 합계
                    data.SKJBPTAMT = ToLong(reader["SKJBPTAMT"].ToString());            // 환자납부총액 합계

                    data.SKCNT = ToLong(reader["SKCNT"].ToString());                 // 이전심결사항-건수합계
                    data.SKCDJS = ToFloat(reader["SKCDJS"].ToString());              // 이전심결사항-차등지수
                    data.SKCJJCNT = ToLong(reader["SKCJJCNT"].ToString());           // 이전심결사항-진찰(조제)횟수
                    data.SKCJDAYS = ToLong(reader["SKCJDAYS"].ToString());           // 이전심결사항-진료(조제)일수
                    data.SKCJDR = ToLong(reader["SKCJDR"].ToString());               // 이전심결사항-의(약)사수
                    data.SKCJJAMT = ToLong(reader["SKCJJAMT"].ToString());           // 이전심결사항-진찰(조제)료
                    data.SKCJJMAMT = ToLong(reader["SKCJJMAMT"].ToString());         // 이전심결사항-진찰료(조제료)차감액
                    data.SKCJRSTCHAAMT = 0;                                          // ***** 운율을 맞추기위한용도임.

                    data.JSTTTAMT = ToLong(reader["JSTTTAMT"].ToString());              // 정산심결사항-진료비총액 합계
                    data.JSRSTAMT = ToLong(reader["JSRSTAMT"].ToString());              // 정산심결사항-심사결정액 합계
                    data.JSSUTAKAMT = ToLong(reader["JSSUTAKAMT"].ToString());          // 정산심결사항-위탁검사직접지급금 합계
                    data.JSRSTCHAAMT = ToLong(reader["JSRSTCHAAMT"].ToString());        // 정산심결사항-결정차액 합계
                    data.JSUPLMTCHATTAMT = ToLong(reader["JSUPLMTCHATTAMT"].ToString());// 약제상한차액총액 합계
                    data.JSJBPTAMT = ToLong(reader["JSJBPTAMT"].ToString());            // 환자납부총액 합계
                    
                    data.JSCNT = ToLong(reader["JSCNT"].ToString());                 // 정산심결사항-건수 합계
                    data.JSCDJS = ToFloat(reader["JSCDJS"].ToString());              // 정산심결사항-차등지수
                    data.JSCJJCNT = ToLong(reader["JSCJJCNT"].ToString());           // 정산심결사항-진찰(조제)횟수
                    data.JSCJDAYS = ToLong(reader["JSCJDAYS"].ToString());           // 정산심결사항-진료(조제)일수
                    data.JSCJDR = ToLong(reader["JSCJDR"].ToString());               // 정산심결사항-의(약)사수
                    data.JSCJJAMT = ToLong(reader["JSCJJAMT"].ToString());           // 정산심결사항-진찰(조제)료
                    data.JSCJJMAMT = ToLong(reader["JSCJJMAMT"].ToString());         // 정산심결사항-진찰료(조제료)차감액
                    data.JSCJRSTCHAAMT = ToLong(reader["JSCJRSTCHAAMT"].ToString()); // 정산심결사항-차등지수결정차액

                    data.MEMO = reader["MEMO"].ToString().TrimEnd();           // 명일련비고사항
                    data.JSREDPT1 = reader["JSREDPT1"].ToString().TrimEnd();   // 정산담당부명
                    data.JSREDPT2 = reader["JSREDPT2"].ToString().TrimEnd();   // 정산담당조명
                    data.JSREDPNM = reader["JSREDPNM"].ToString().TrimEnd();   // 정산담당자명
                    data.JSREDPNO = reader["JSREDPNO"].ToString().TrimEnd();   // 정산담당자번호
                    data.JSRETELE = reader["JSRETELE"].ToString().TrimEnd();   // 정산담당자전화번호
                    data.JSYYSEQ = reader["JSYYSEQ"].ToString().TrimEnd();     // 정산연번
                    data.JBUNICD = reader["JBUNICD"].ToString().TrimEnd();     // 보험회사코드
                    data.HOSID = reader["HOSID"].ToString().TrimEnd();         // 요양기관 기호
                    data.FMNO = reader["FMNO"].ToString().TrimEnd();           // 서식번호
                    data.GRPNO = reader["GRPNO"].ToString().TrimEnd();         // 묶음번호
                    data.VERSION = reader["VERSION"].ToString().TrimEnd();     // 버전구분
                    data.DCOUNT = reader["DCOUNT"].ToString().TrimEnd();       // 청구서 일련번호
                    data.DEMUNITFG = reader["DEMUNITFG"].ToString().TrimEnd(); // 청구단위구분
                    data.JRFG = reader["JRFG"].ToString().TrimEnd();           // 보험자종별구분
                    data.JSBUSSCD = reader["JSBUSSCD"].ToString().TrimEnd();   // 정산업무코드
                    data.JSBUSSNM = reader["JSBUSSNM"].ToString().TrimEnd();   // 정산업무명

                    list.Add(data);

                    CData data2 = new CData();
                    data2.Clear();
                    data2.SKJSDIV = 1;

                    data2.JSDEMSEQ = data.JSDEMSEQ;
                    data2.JSREDAY = data.JSREDAY;
                    data2.DEMSEQ = data.DEMSEQ;
                    data2.CNECNO = data.CNECNO;
                    data2.DEMNO = data.DEMNO;
                    data2.SIMGBN = data.SIMGBN;

                    data2.SKTTTAMT = data.SKTTTAMT;
                    data2.SKRSTAMT = data.SKRSTAMT;
                    data2.SKSUTAKAMT = data.SKSUTAKAMT;
                    data2.SKRSTCHAAMT = data.SKRSTCHAAMT;
                    data2.SKUPLMTCHATTAMT = data.SKUPLMTCHATTAMT;
                    data2.SKJBPTAMT = data.SKJBPTAMT;

                    data2.SKCNT = data.SKCNT;
                    data2.SKCDJS = data.SKCDJS;
                    data2.SKCJJCNT = data.SKCJJCNT;
                    data2.SKCJDAYS = data.SKCJDAYS;
                    data2.SKCJDR = data.SKCJDR;
                    data2.SKCJJAMT = data.SKCJJAMT;
                    data2.SKCJJMAMT = data.SKCJJMAMT;
                    data2.SKCJRSTCHAAMT = data.SKCJRSTCHAAMT;

                    data2.JSTTTAMT = data.JSTTTAMT;
                    data2.JSRSTAMT = data.JSRSTAMT;
                    data2.JSSUTAKAMT = data.JSSUTAKAMT;
                    data2.JSRSTCHAAMT = data.JSRSTCHAAMT;
                    data2.JSUPLMTCHATTAMT = data.JSUPLMTCHATTAMT;
                    data2.JSJBPTAMT = data.JSJBPTAMT;

                    data2.JSCNT = data.JSCNT;
                    data2.JSCDJS = data.JSCDJS;
                    data2.JSCJJCNT = data.JSCJJCNT;
                    data2.JSCJDAYS = data.JSCJDAYS;
                    data2.JSCJDR = data.JSCJDR;
                    data2.JSCJJAMT = data.JSCJJAMT;
                    data2.JSCJJMAMT = data.JSCJJMAMT;
                    data2.JSCJRSTCHAAMT = data.JSCJRSTCHAAMT;

                    data2.MEMO = data.MEMO;
                    data2.JSREDPT1 = data.JSREDPT1;
                    data2.JSREDPT2 = data.JSREDPT2;
                    data2.JSREDPNM = data.JSREDPNM;
                    data2.JSREDPNO = data.JSREDPNO;
                    data2.JSRETELE = data.JSRETELE;
                    data2.JSYYSEQ = data.JSYYSEQ;
                    data2.JBUNICD = data.JBUNICD;
                    data2.HOSID = data.HOSID;
                    data2.FMNO = data.FMNO;
                    data2.GRPNO = data.GRPNO;
                    data2.VERSION = data.VERSION;
                    data2.DCOUNT = data.DCOUNT;
                    data2.DEMUNITFG = data.DEMUNITFG;
                    data2.JRFG = data.JRFG;
                    data2.JSBUSSCD = data.JSBUSSCD;
                    data2.JSBUSSNM = data.JSBUSSNM;

                    list.Add(data2);

                    // 합계
                    sum.JSDEMSEQ = "합계";
                    sum.SKJSDIV = 0;
                    sum.SKTTTAMT += data.SKTTTAMT;              // 이전심결사항-진료비총액 합계
                    sum.SKRSTAMT += data.SKRSTAMT;              // 이전심결사항-심사결정액 합계
                    sum.SKSUTAKAMT += data.SKSUTAKAMT;          // 이전심결사항-위탁검사직접지급금 합계
                    sum.SKRSTCHAAMT += data.SKRSTCHAAMT;        // ***** 운율을 맞추기위한용도임.
                    sum.SKUPLMTCHATTAMT += data.SKUPLMTCHATTAMT;// 약제상한차액총액 합계
                    sum.SKJBPTAMT += data.SKJBPTAMT;            // 환자납부총액 합계

                    sum.SKCNT += data.SKCNT;                 // 이전심결사항-건수합계
                    sum.SKCDJS += data.SKCDJS;               // 이전심결사항-차등지수
                    sum.SKCJJCNT += data.SKCJJCNT;           // 이전심결사항-진찰(조제)횟수
                    sum.SKCJDAYS += data.SKCJDAYS;           // 이전심결사항-진료(조제)일수
                    sum.SKCJDR += data.SKCJDR;               // 이전심결사항-의(약)사수
                    sum.SKCJJAMT += data.SKCJJAMT;           // 이전심결사항-진찰(조제)료
                    sum.SKCJJMAMT += data.SKCJJMAMT;         // 이전심결사항-진찰료(조제료)차감액
                    sum.SKCJRSTCHAAMT += data.SKCJRSTCHAAMT; // ***** 운율을 맞추기위한용도임.

                    sum.JSTTTAMT += data.JSTTTAMT;              // 정산심결사항-진료비총액 합계
                    sum.JSRSTAMT += data.JSRSTAMT;              // 정산심결사항-심사결정액 합계
                    sum.JSSUTAKAMT += data.JSSUTAKAMT;          // 정산심결사항-위탁검사직접지급금 합계
                    sum.JSRSTCHAAMT += data.JSRSTCHAAMT;        // 정산심결사항-결정차액 합계
                    sum.JSUPLMTCHATTAMT += data.JSUPLMTCHATTAMT;// 약제상한차액총액 합계
                    sum.JSJBPTAMT += data.JSJBPTAMT;            // 환자납부총액 합계
                    
                    sum.JSCNT += data.JSCNT;                 // 정산심결사항-건수 합계
                    sum.JSCDJS += data.JSCDJS;               // 정산심결사항-차등지수
                    sum.JSCJJCNT += data.JSCJJCNT;           // 정산심결사항-진찰(조제)횟수
                    sum.JSCJDAYS += data.JSCJDAYS;           // 정산심결사항-진료(조제)일수
                    sum.JSCJDR += data.JSCJDR;               // 정산심결사항-의(약)사수
                    sum.JSCJJAMT += data.JSCJJAMT;           // 정산심결사항-진찰(조제)료
                    sum.JSCJJMAMT += data.JSCJJMAMT;         // 정산심결사항-진찰료(조제료)차감액
                    sum.JSCJRSTCHAAMT += data.JSCJRSTCHAAMT; // 정산심결사항-차등지수결정차액

                    sum2.JSDEMSEQ = "합계";
                    sum2.SKJSDIV = 1;
                    sum2.SKTTTAMT += data.SKTTTAMT;              // 이전심결사항-진료비총액 합계
                    sum2.SKRSTAMT += data.SKRSTAMT;              // 이전심결사항-심사결정액 합계
                    sum2.SKSUTAKAMT += data.SKSUTAKAMT;          // 이전심결사항-위탁검사직접지급금 합계
                    sum2.SKRSTCHAAMT += data.SKRSTCHAAMT;        // ***** 운율을 맞추기위한용도임.
                    sum2.SKUPLMTCHATTAMT += data.SKUPLMTCHATTAMT;// 약제상한차액총액 합계
                    sum2.SKJBPTAMT += data.SKJBPTAMT;            // 환자납부총액 합계

                    sum2.SKCNT += data.SKCNT;                 // 이전심결사항-건수합계
                    sum2.SKCDJS += data.SKCDJS;               // 이전심결사항-차등지수
                    sum2.SKCJJCNT += data.SKCJJCNT;           // 이전심결사항-진찰(조제)횟수
                    sum2.SKCJDAYS += data.SKCJDAYS;           // 이전심결사항-진료(조제)일수
                    sum2.SKCJDR += data.SKCJDR;               // 이전심결사항-의(약)사수
                    sum2.SKCJJAMT += data.SKCJJAMT;           // 이전심결사항-진찰(조제)료
                    sum2.SKCJJMAMT += data.SKCJJMAMT;         // 이전심결사항-진찰료(조제료)차감액
                    sum2.SKCJRSTCHAAMT += data.SKCJRSTCHAAMT; // ***** 운율을 맞추기위한용도임.

                    sum2.JSTTTAMT += data.JSTTTAMT;              // 정산심결사항-진료비총액 합계
                    sum2.JSRSTAMT += data.JSRSTAMT;              // 정산심결사항-심사결정액 합계
                    sum2.JSSUTAKAMT += data.JSSUTAKAMT;          // 정산심결사항-위탁검사직접지급금 합계
                    sum2.JSRSTCHAAMT += data.JSRSTCHAAMT;        // 정산심결사항-결정차액 합계
                    sum2.JSUPLMTCHATTAMT += data.JSUPLMTCHATTAMT;// 약제상한차액총액 합계
                    sum2.JSJBPTAMT += data.JSJBPTAMT;            // 환자납부총액 합계

                    sum2.JSCNT += data.JSCNT;                 // 정산심결사항-건수 합계
                    sum2.JSCDJS += data.JSCDJS;               // 정산심결사항-차등지수
                    sum2.JSCJJCNT += data.JSCJJCNT;           // 정산심결사항-진찰(조제)횟수
                    sum2.JSCJDAYS += data.JSCJDAYS;           // 정산심결사항-진료(조제)일수
                    sum2.JSCJDR += data.JSCJDR;               // 정산심결사항-의(약)사수
                    sum2.JSCJJAMT += data.JSCJJAMT;           // 정산심결사항-진찰(조제)료
                    sum2.JSCJJMAMT += data.JSCJJMAMT;         // 정산심결사항-진찰료(조제료)차감액
                    sum2.JSCJRSTCHAAMT += data.JSCJRSTCHAAMT; // 정산심결사항-차등지수결정차액

                    if (data.MEMO != "") isMemo = true;
                    if (data.JBUNICD != "") isJbunicd = true;

                    return true;
                });
            }

            if (list.Count > 0)
            {
                list.Add(sum);
                list.Add(sum2);
            }

            RefreshGridMain();

            // 금액이 없는 자료는 숨김
            gcSKJS_TTTAMT.Visible = sum.SKJS_TTTAMT != 0 || sum2.SKJS_TTTAMT != 0;
            gcSKJS_RSTAMT.Visible = sum.SKJS_RSTAMT != 0 || sum2.SKJS_RSTAMT != 0;
            gcSKJS_SUTAKAMT.Visible = sum.SKJS_SUTAKAMT != 0 || sum2.SKJS_SUTAKAMT != 0;
            gcSKJS_RSTCHAAMT.Visible = sum.SKJS_RSTCHAAMT != 0 || sum2.SKJS_RSTCHAAMT != 0;
            gcSKJS_UPLMTCHATTAMT.Visible = sum.SKJS_UPLMTCHATTAMT != 0 || sum2.SKJS_UPLMTCHATTAMT != 0;
            gcSKJS_JBPTAMT.Visible = sum.SKJS_JBPTAMT != 0 || sum2.SKJS_JBPTAMT != 0;

            gcSKJS_CNT.Visible = sum.SKJS_CNT != 0 || sum2.SKJS_CNT != 0;
            gcSKJS_CDJS.Visible = sum.SKJS_CDJS != 0 || sum2.SKJS_CDJS != 0;
            gcSKJS_CJJCNT.Visible = sum.SKJS_CJJCNT != 0 || sum2.SKJS_CJJCNT != 0;
            gcSKJS_CJDAYS.Visible = sum.SKJS_CJDAYS != 0 || sum2.SKJS_CJDAYS != 0;
            gcSKJS_CJDR.Visible = sum.SKJS_CJDR != 0 || sum2.SKJS_CJDR != 0;
            gcSKJS_CJJAMT.Visible = sum.SKJS_CJJAMT != 0 || sum2.SKJS_CJJAMT != 0;
            gcSKJS_CJJMAMT.Visible = sum.SKJS_CJJMAMT != 0 || sum2.SKJS_CJJMAMT != 0;
            gcSKJS_CJRSTCHAAMT.Visible = sum.SKJS_CJRSTCHAAMT != 0 || sum2.SKJS_CJRSTCHAAMT != 0;


            gcMEMO.Visible = isMemo;
            gcJBUNICD.Visible = isJbunicd;

            RefreshGridMain();
        }

        private long ToLong(string value)
        {
            long ret = 0;
            long.TryParse(value, out ret);
            return ret;
        }

        private float ToFloat(string value)
        {
            float ret = 0;
            float.TryParse(value, out ret);
            return ret;
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

        private void grdMainView_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.RowHandle1 % 2 == 0 && e.RowHandle2 % 2 != 0)
            {
                // 그리드의 OptionsView.AllowCellMerge를 True로 해놓아야 한다.
                // 셀 머지를 하지 않는 필드는 속성에서 OptionsColumn.AllowMerge를 False로 해놓았다.
                e.Merge = true;
                e.Handled = true;
            }
            else
            {
                e.Merge = false;
                e.Handled = true;
            }
        }

        private void tabJobdiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabJobdiv.SelectedIndex == 0)
            {
                // 리스트
            }
            else
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this.ShowProgressForm("", "조회 중입니다.");

                    if (tabJobdiv.SelectedIndex == 1) this.QueryPtnt();   // 명단
                    if (tabJobdiv.SelectedIndex == 2) this.QueryDetail(); // 상세
                    if (tabJobdiv.SelectedIndex == 3) this.QuerySutak();  // 위탁

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
        }

        private void QueryPtnt()
        {
            if (grdMainView.FocusedRowHandle < 0) return;

            string jsdemseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcJSDEMSEQ).ToString();
            string jsreday = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcJSREDAY).ToString();
            string cnecno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcCNECNO).ToString();
            string dcount = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDCOUNT).ToString();
            string jsyyseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcJSYYSEQ).ToString();
            string demno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDEMNO).ToString();

            bool isMemo = false; // 메모가 있는지 여부

            CDataPtnt sum = new CDataPtnt();
            sum.Clear();

            CDataPtnt sum2 = new CDataPtnt();
            sum2.Clear();

            List<CDataPtnt> list = new List<CDataPtnt>();
            grdPtnt.DataSource = null;
            grdPtnt.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                string sql = "";
                sql += Environment.NewLine + "SELECT N0403.JSDEMSEQ";        // 정산심사차수
                sql += Environment.NewLine + "     , N0403.JSREDAY";         // 정산통보일자
                sql += Environment.NewLine + "     , N0403.CNECNO";          // 접수번호
                sql += Environment.NewLine + "     , N0403.DCOUNT";          // 청구서 일련번호
                sql += Environment.NewLine + "     , N0403.JSYYSEQ";         // 정산연번
                sql += Environment.NewLine + "     , N0403.JSSEQNO";         // 정산일련번호
                sql += Environment.NewLine + "     , N0403.EPRTNO";          // 명세서 일련번호
                sql += Environment.NewLine + "     , N0403.APPRNO";          // 사고접수번호
                sql += Environment.NewLine + "     , N0403.PNM";             // 수진자성명
                sql += Environment.NewLine + "     , N0403.JRHT";            // 진료형태
                sql += Environment.NewLine + "     , N0403.GUBUN";           // 구분(1.환급 2.환수 3.환수+환급)
                sql += Environment.NewLine + "     , N0403.STEDT";           // 당월요양개시일
                sql += Environment.NewLine + "     , N0403.DACD";            // 상병분류기호
                sql += Environment.NewLine + "     , N0403.SKCHOGUM";        // 이전심결사항-초진료
                sql += Environment.NewLine + "     , N0403.SKJAEGUM";        // 이전심결사항-재진료
                sql += Environment.NewLine + "     , N0403.SKTTTAMT";        // 이전심결사항-진료비총액
                sql += Environment.NewLine + "     , N0403.SKRSTAMT";        // 이전심결사항-심사결정액
                sql += Environment.NewLine + "     , N0403.SKSUTAKAMT";      // 이전심결사항-위탁검사직접지급금
                sql += Environment.NewLine + "     , N0403.SKUPLMTCHATTAMT"; // 이전심결사항-약제상한차액총액 합계
                sql += Environment.NewLine + "     , N0403.SKJBPTAMT";       // 이전심결사항-환자납부총액 합계
                sql += Environment.NewLine + "     , N0403.SKCHOCNT";        // 이전심결사항-초진횟수
                sql += Environment.NewLine + "     , N0403.SKCNONCNT";       // 이전심결사항-초진가산횟수(1일분투약횟수)
                sql += Environment.NewLine + "     , N0403.SKJAECNT";        // 이전심결사항-재진횟수(2일분투약횟수)
                sql += Environment.NewLine + "     , N0403.SKJAENCNT";       // 이전심결사항-재진가산횟수(3일분이상투약횟수)
                sql += Environment.NewLine + "     , N0403.SKEXAMC";         // 이전심결사항-입원일수(내원일수)
                sql += Environment.NewLine + "     , N0403.SKORDDAYS";       // 이전심결사항-요양급여일수
                sql += Environment.NewLine + "     , N0403.SKORDCNT";        // 이전심결사항-처방횟수
                sql += Environment.NewLine + "     , N0403.JSCHOGUM";        // 정산심결사항-초진료
                sql += Environment.NewLine + "     , N0403.JSJAEGUM";        // 정산심결사항-재진료
                sql += Environment.NewLine + "     , N0403.JSTTTAMT";        // 정산심결사항-진료비총액
                sql += Environment.NewLine + "     , N0403.JSRSTAMT";        // 정산심결사항-심사결정액
                sql += Environment.NewLine + "     , N0403.JSSUTAKAMT";      // 정산심결사항-위탁검사직접지급금
                sql += Environment.NewLine + "     , N0403.JSRSTCHAAMT";     // 정산심결사항-결정차액
                sql += Environment.NewLine + "     , N0403.JSUPLMTCHATTAMT"; // 정산심결사항-약제상한차액총액 합계
                sql += Environment.NewLine + "     , N0403.JSJBPTAMT";       // 정산심결사항-환자납부총액 합계
                sql += Environment.NewLine + "     , N0403.JSCHOCNT";        // 정산심결사항-초진횟수
                sql += Environment.NewLine + "     , N0403.JSCHONCNT";       // 정산심결사항-초진가산횟수(1일분투약횟수)
                sql += Environment.NewLine + "     , N0403.JSJAECNT";        // 정산심결사항-재진횟수(2일분투약횟수)
                sql += Environment.NewLine + "     , N0403.JSJAENCNT";       // 정산심결사항-재진가산횟수(3일분이상투약횟수)
                sql += Environment.NewLine + "     , N0403.JSEXAMC";         // 정산심결사항-입원일수(내원일수)
                sql += Environment.NewLine + "     , N0403.JSORDDAYS";       // 정산심결사항-요양급여일수
                sql += Environment.NewLine + "     , N0403.JSORDCNT";        // 정산심결사항-처방횟수
                sql += Environment.NewLine + "     , N0403.MEMO";            // 명일련비고사항
                sql += Environment.NewLine + "     , N0401.DEMNO";           // 청구번호
                sql += Environment.NewLine + "  FROM TIE_N0403 N0403 INNER JOIN TIE_N0401 N0401 ON N0401.JSDEMSEQ=N0403.JSDEMSEQ AND N0401.JSREDAY=N0403.JSREDAY AND N0401.CNECNO=N0403.CNECNO AND N0401.DCOUNT=N0403.DCOUNT";
                if (cboQueryOption.SelectedIndex == 1)
                {
                    // 청구번호별
                    sql += Environment.NewLine + " WHERE N0401.DEMNO = '" + demno + "' ";
                    sql += Environment.NewLine + " ORDER BY N0401.DEMNO, N0403.EPRTNO";
                }
                else if (cboQueryOption.SelectedIndex == 2)
                {
                    // 청구월별
                    sql += Environment.NewLine + " WHERE N0401.DEMNO LIKE '" + demno.Substring(0, 6) + "%' ";
                    sql += Environment.NewLine + " ORDER BY N0401.DEMNO, N0403.EPRTNO";
                }
                else if (cboQueryOption.SelectedIndex == 3)
                {
                    // 청구년도별
                    sql += Environment.NewLine + " WHERE N0401.DEMNO LIKE '" + demno.Substring(0, 4) + "%' ";
                    sql += Environment.NewLine + " ORDER BY N0401.DEMNO, N0403.EPRTNO";
                }
                else
                {
                    // 기본
                    sql += Environment.NewLine + " WHERE N0403.JSDEMSEQ = '" + jsdemseq + "' ";
                    sql += Environment.NewLine + "   AND N0403.JSREDAY  = '" + jsreday + "' ";
                    sql += Environment.NewLine + "   AND N0403.CNECNO   = '" + cnecno + "' ";
                    sql += Environment.NewLine + "   AND N0403.DCOUNT   = '" + dcount + "' ";
                    sql += Environment.NewLine + "   AND N0403.JSYYSEQ  = '" + jsyyseq + "' ";
                    sql += Environment.NewLine + " ORDER BY N0403.JSDEMSEQ, N0403.JSREDAY, N0403.CNECNO, N0403.DCOUNT, N0403.JSYYSEQ, N0403.JSSEQNO, N0403.EPRTNO";
                }

                int no = 0;
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataPtnt data = new CDataPtnt();
                    data.Clear();
                    data.NO = ++no;
                    data.SKJSDIV = 0;
                    data.JSDEMSEQ = reader["JSDEMSEQ"].ToString();        // 정산심사차수
                    data.JSREDAY = reader["JSREDAY"].ToString();          // 정산통보일자
                    data.CNECNO = reader["CNECNO"].ToString();            // 접수번호
                    data.DCOUNT = reader["DCOUNT"].ToString();            // 청구서 일련번호
                    data.JSYYSEQ = reader["JSYYSEQ"].ToString();          // 정산연번
                    data.JSSEQNO = reader["JSSEQNO"].ToString();          // 정산일련번호
                    data.EPRTNO = reader["EPRTNO"].ToString();            // 명세서 일련번호
                    data.APPRNO = reader["APPRNO"].ToString().TrimEnd();  // 사고접수번호
                    data.PNM = reader["PNM"].ToString().TrimEnd();        // 수진자성명
                    data.JRHT = reader["JRHT"].ToString().TrimEnd();      // 진료형태
                    data.GUBUN = reader["GUBUN"].ToString().TrimEnd();    // 구분(1.환급 2.환수 3.환수+환급)
                    data.STEDT = reader["STEDT"].ToString().TrimEnd();    // 당월요양개시일
                    data.DACD = reader["DACD"].ToString().TrimEnd();      // 상병분류기호
                    data.SKCHOGUM = ToLong(reader["SKCHOGUM"].ToString());        // 이전심결사항-초진료
                    data.SKJAEGUM = ToLong(reader["SKJAEGUM"].ToString());        // 이전심결사항-재진료
                    data.SKTTTAMT = ToLong(reader["SKTTTAMT"].ToString());        // 이전심결사항-진료비총액
                    data.SKRSTAMT = ToLong(reader["SKRSTAMT"].ToString());        // 이전심결사항-심사결정액
                    data.SKSUTAKAMT = ToLong(reader["SKSUTAKAMT"].ToString());    // 이전심결사항-위탁검사직접지급금
                    data.SKUPLMTCHATTAMT = ToLong(reader["SKUPLMTCHATTAMT"].ToString()); // 이전심결사항-약제상한차액총액 합계
                    data.SKJBPTAMT = ToLong(reader["SKJBPTAMT"].ToString());      // 이전심결사항-환자납부총액 합계
                    data.SKCHOCNT = ToLong(reader["SKCHOCNT"].ToString());        // 이전심결사항-초진횟수
                    data.SKCHONCNT = ToLong(reader["SKCNONCNT"].ToString());      // 이전심결사항-초진가산횟수(1일분투약횟수)
                    data.SKJAECNT = ToLong(reader["SKJAECNT"].ToString());        // 이전심결사항-재진횟수(2일분투약횟수)
                    data.SKJAENCNT = ToLong(reader["SKJAENCNT"].ToString());      // 이전심결사항-재진가산횟수(3일분이상투약횟수)
                    data.SKEXAMC = ToLong(reader["SKEXAMC"].ToString());          // 이전심결사항-입원일수(내원일수)
                    data.SKORDDAYS = ToLong(reader["SKORDDAYS"].ToString());      // 이전심결사항-요양급여일수
                    data.SKORDCNT = ToLong(reader["SKORDCNT"].ToString());        // 이전심결사항-처방횟수
                    data.JSCHOGUM = ToLong(reader["JSCHOGUM"].ToString());        // 정산심결사항-초진료
                    data.JSJAEGUM = ToLong(reader["JSJAEGUM"].ToString());        // 정산심결사항-재진료
                    data.JSTTTAMT = ToLong(reader["JSTTTAMT"].ToString());        // 정산심결사항-진료비총액
                    data.JSRSTAMT = ToLong(reader["JSRSTAMT"].ToString());        // 정산심결사항-심사결정액
                    data.JSSUTAKAMT = ToLong(reader["JSSUTAKAMT"].ToString());    // 정산심결사항-위탁검사직접지급금
                    data.JSRSTCHAAMT = ToLong(reader["JSRSTCHAAMT"].ToString());  // 정산심결사항-결정차액
                    data.JSUPLMTCHATTAMT = ToLong(reader["JSUPLMTCHATTAMT"].ToString()); // 정산심결사항-약제상한차액총액 합계
                    data.JSJBPTAMT = ToLong(reader["JSJBPTAMT"].ToString());      // 정산심결사항-환자납부총액 합계
                    data.JSCHOCNT = ToLong(reader["JSCHOCNT"].ToString());        // 정산심결사항-초진횟수
                    data.JSCHONCNT = ToLong(reader["JSCHONCNT"].ToString());      // 정산심결사항-초진가산횟수(1일분투약횟수)
                    data.JSJAECNT = ToLong(reader["JSJAECNT"].ToString());        // 정산심결사항-재진횟수(2일분투약횟수)
                    data.JSJAENCNT = ToLong(reader["JSJAENCNT"].ToString());      // 정산심결사항-재진가산횟수(3일분이상투약횟수)
                    data.JSEXAMC = ToLong(reader["JSEXAMC"].ToString());          // 정산심결사항-입원일수(내원일수)
                    data.JSORDDAYS = ToLong(reader["JSORDDAYS"].ToString());      // 정산심결사항-요양급여일수
                    data.JSORDCNT = ToLong(reader["JSORDCNT"].ToString());        // 정산심결사항-처방횟수
                    data.MEMO = reader["MEMO"].ToString().TrimEnd();              // 명일련비고사항
                    data.DEMNO = reader["DEMNO"].ToString().TrimEnd();            // 청구번호

                    if (data.MEMO != "") isMemo = true;

                    list.Add(data);

                    CDataPtnt data2 = new CDataPtnt();
                    data2.Clear();
                    data2.NO = no;
                    data2.SKJSDIV = 1;
                    data2.JSDEMSEQ = data.JSDEMSEQ;        // 정산심사차수
                    data2.JSREDAY = data.JSREDAY;          // 정산통보일자
                    data2.CNECNO = data.CNECNO;            // 접수번호
                    data2.DCOUNT = data.DCOUNT;            // 청구서 일련번호
                    data2.JSYYSEQ = data.JSYYSEQ;          // 정산연번
                    data2.JSSEQNO = data.JSSEQNO;          // 정산일련번호
                    data2.EPRTNO = data.EPRTNO;            // 명세서 일련번호
                    data2.APPRNO = data.APPRNO;            // 사고접수번호
                    data2.PNM = data.PNM;                  // 수진자성명
                    data2.JRHT = data.JRHT;                // 진료형태
                    data2.GUBUN = data.GUBUN;              // 구분(1.환급 2.환수 3.환수+환급)
                    data2.STEDT = data.STEDT;              // 당월요양개시일
                    data2.DACD = data.DACD;                // 상병분류기호
                    data2.SKCHOGUM = data.SKCHOGUM;        // 이전심결사항-초진료
                    data2.SKJAEGUM = data.SKJAEGUM;        // 이전심결사항-재진료
                    data2.SKTTTAMT = data.SKTTTAMT;        // 이전심결사항-진료비총액
                    data2.SKRSTAMT = data.SKRSTAMT;        // 이전심결사항-심사결정액
                    data2.SKSUTAKAMT = data.SKSUTAKAMT;    // 이전심결사항-위탁검사직접지급금
                    data2.SKUPLMTCHATTAMT = data.SKUPLMTCHATTAMT; // 이전심결사항-약제상한차액총액 합계
                    data2.SKJBPTAMT = data.SKJBPTAMT;      // 이전심결사항-환자납부총액 합계
                    data2.SKCHOCNT = data.SKCHOCNT;        // 이전심결사항-초진횟수
                    data2.SKCHONCNT = data.SKCHONCNT;      // 이전심결사항-초진가산횟수(1일분투약횟수)
                    data2.SKJAECNT = data.SKJAECNT;        // 이전심결사항-재진횟수(2일분투약횟수)
                    data2.SKJAENCNT = data.SKJAENCNT;      // 이전심결사항-재진가산횟수(3일분이상투약횟수)
                    data2.SKEXAMC = data.SKEXAMC;          // 이전심결사항-입원일수(내원일수)
                    data2.SKORDDAYS = data.SKORDDAYS;      // 이전심결사항-요양급여일수
                    data2.SKORDCNT = data.SKORDCNT;        // 이전심결사항-처방횟수
                    data2.JSCHOGUM = data.JSCHOGUM;        // 정산심결사항-초진료
                    data2.JSJAEGUM = data.JSJAEGUM;        // 정산심결사항-재진료
                    data2.JSTTTAMT = data.JSTTTAMT;        // 정산심결사항-진료비총액
                    data2.JSRSTAMT = data.JSRSTAMT;        // 정산심결사항-심사결정액
                    data2.JSSUTAKAMT = data.JSSUTAKAMT;    // 정산심결사항-위탁검사직접지급금
                    data2.JSRSTCHAAMT = data.JSRSTCHAAMT;  // 정산심결사항-결정차액
                    data2.JSUPLMTCHATTAMT = data.JSUPLMTCHATTAMT; // 정산심결사항-약제상한차액총액 합계
                    data2.JSJBPTAMT = data.JSJBPTAMT;      // 정산심결사항-환자납부총액 합계
                    data2.JSCHOCNT = data.JSCHOCNT;        // 정산심결사항-초진횟수
                    data2.JSCHONCNT = data.JSCHONCNT;      // 정산심결사항-초진가산횟수(1일분투약횟수)
                    data2.JSJAECNT = data.JSJAECNT;        // 정산심결사항-재진횟수(2일분투약횟수)
                    data2.JSJAENCNT = data.JSJAENCNT;      // 정산심결사항-재진가산횟수(3일분이상투약횟수)
                    data2.JSEXAMC = data.JSEXAMC;          // 정산심결사항-입원일수(내원일수)
                    data2.JSORDDAYS = data.JSORDDAYS;      // 정산심결사항-요양급여일수
                    data2.JSORDCNT = data.JSORDCNT;        // 정산심결사항-처방횟수
                    data2.MEMO = data.MEMO;                // 명일련비고사항
                    data2.DEMNO = data.DEMNO;              // 청구번호

                    list.Add(data2);

                    // 합계
                    sum.SKJSDIV = 0;
                    sum.EPRTNO = "합계";
                    sum.SKCHOGUM += data.SKCHOGUM;
                    sum.SKJAEGUM += data.SKJAEGUM;
                    sum.SKTTTAMT += data.SKTTTAMT;
                    sum.SKRSTAMT += data.SKRSTAMT;
                    sum.SKSUTAKAMT += data.SKSUTAKAMT;
                    sum.SKUPLMTCHATTAMT += data.SKUPLMTCHATTAMT;
                    sum.SKJBPTAMT += data.SKJBPTAMT;
                    sum.SKCHOCNT += data.SKCHOCNT;
                    sum.SKCHONCNT += data.SKCHONCNT;
                    sum.SKJAECNT += data.SKJAECNT;
                    sum.SKJAENCNT += data.SKJAENCNT;
                    sum.SKEXAMC += data.SKEXAMC;
                    sum.SKORDDAYS += data.SKORDDAYS;
                    sum.SKORDCNT += data.SKORDCNT;
                    sum.JSCHOGUM += data.JSCHOGUM;
                    sum.JSJAEGUM += data.JSJAEGUM;
                    sum.JSTTTAMT += data.JSTTTAMT;
                    sum.JSRSTAMT += data.JSRSTAMT;
                    sum.JSSUTAKAMT += data.JSSUTAKAMT;
                    sum.JSRSTCHAAMT += data.JSRSTCHAAMT;
                    sum.JSUPLMTCHATTAMT += data.JSUPLMTCHATTAMT;
                    sum.JSJBPTAMT += data.JSJBPTAMT;
                    sum.JSCHOCNT += data.JSCHOCNT;
                    sum.JSCHONCNT += data.JSCHONCNT;
                    sum.JSJAECNT += data.JSJAECNT;
                    sum.JSJAENCNT += data.JSJAENCNT;
                    sum.JSEXAMC += data.JSEXAMC;
                    sum.JSORDDAYS += data.JSORDDAYS;
                    sum.JSORDCNT += data.JSORDCNT;

                    sum2.SKJSDIV = 1;
                    sum2.EPRTNO = "합계";
                    sum2.SKCHOGUM += data.SKCHOGUM;
                    sum2.SKJAEGUM += data.SKJAEGUM;
                    sum2.SKTTTAMT += data.SKTTTAMT;
                    sum2.SKRSTAMT += data.SKRSTAMT;
                    sum2.SKSUTAKAMT += data.SKSUTAKAMT;
                    sum2.SKUPLMTCHATTAMT += data.SKUPLMTCHATTAMT;
                    sum2.SKJBPTAMT += data.SKJBPTAMT;
                    sum2.SKCHOCNT += data.SKCHOCNT;
                    sum2.SKCHONCNT += data.SKCHONCNT;
                    sum2.SKJAECNT += data.SKJAECNT;
                    sum2.SKJAENCNT += data.SKJAENCNT;
                    sum2.SKEXAMC += data.SKEXAMC;
                    sum2.SKORDDAYS += data.SKORDDAYS;
                    sum2.SKORDCNT += data.SKORDCNT;
                    sum2.JSCHOGUM += data.JSCHOGUM;
                    sum2.JSJAEGUM += data.JSJAEGUM;
                    sum2.JSTTTAMT += data.JSTTTAMT;
                    sum2.JSRSTAMT += data.JSRSTAMT;
                    sum2.JSSUTAKAMT += data.JSSUTAKAMT;
                    sum2.JSRSTCHAAMT += data.JSRSTCHAAMT;
                    sum2.JSUPLMTCHATTAMT += data.JSUPLMTCHATTAMT;
                    sum2.JSJBPTAMT += data.JSJBPTAMT;
                    sum2.JSCHOCNT += data.JSCHOCNT;
                    sum2.JSCHONCNT += data.JSCHONCNT;
                    sum2.JSJAECNT += data.JSJAECNT;
                    sum2.JSJAENCNT += data.JSJAENCNT;
                    sum2.JSEXAMC += data.JSEXAMC;
                    sum2.JSORDDAYS += data.JSORDDAYS;
                    sum2.JSORDCNT += data.JSORDCNT;

                    return true;
                });
            }

            if (list.Count > 0)
            {
                list.Add(sum);
                list.Add(sum2);
            }
            RefreshGridPtnt();

            // 금액이 없는 자료는 숨김
            gcSKJS_TTTAMT_PTNT.Visible = sum.SKJS_TTTAMT != 0 || sum2.SKJS_TTTAMT != 0; 
            gcSKJS_RSTAMT_PTNT.Visible = sum.SKJS_RSTAMT != 0 || sum2.SKJS_RSTAMT != 0; 
            gcSKJS_JSRSTCHAAMT_PTNT.Visible = sum.SKJS_JSRSTCHAAMT != 0 || sum2.SKJS_JSRSTCHAAMT != 0; 
            gcSKJS_CHOGUM_PTNT.Visible = sum.SKJS_CHOGUM != 0 || sum2.SKJS_CHOGUM != 0; 
            gcSKJS_JAEGUM_PTNT.Visible = sum.SKJS_JAEGUM != 0 || sum2.SKJS_JAEGUM != 0; 
            gcSKJS_SUTAKAMT_PTNT.Visible = sum.SKJS_SUTAKAMT != 0 || sum2.SKJS_SUTAKAMT != 0; 
            gcSKJS_UPLMTCHATTAMT_PTNT.Visible = sum.SKJS_UPLMTCHATTAMT != 0 || sum2.SKJS_UPLMTCHATTAMT != 0; 
            gcSKJS_JBPTAMT_PTNT.Visible = sum.SKJS_JBPTAMT != 0 || sum2.SKJS_JBPTAMT != 0; 
            gcSKJS_CHOCNT_PTNT.Visible = sum.SKJS_CHOCNT != 0 || sum2.SKJS_CHOCNT != 0; 
            gcSKJS_CHONCNT_PTNT.Visible = sum.SKJS_CHONCNT != 0 || sum2.SKJS_CHONCNT != 0; 
            gcSKJS_JAECNT_PTNT.Visible = sum.SKJS_JAECNT != 0 || sum2.SKJS_JAECNT != 0; 
            gcSKJS_JAENCNT_PTNT.Visible = sum.SKJS_JAENCNT != 0 || sum2.SKJS_JAENCNT != 0; 
            gcSKJS_EXAMC_PTNT.Visible = sum.SKJS_EXAMC != 0 || sum2.SKJS_EXAMC != 0; 
            gcSKJS_ORDDAYS_PTNT.Visible = sum.SKJS_ORDDAYS != 0 || sum2.SKJS_ORDDAYS != 0; 
            gcSKJS_ORDCNT_PTNT.Visible = sum.SKJS_ORDCNT != 0 || sum2.SKJS_ORDCNT != 0; 

            gcMEMO_PTNT.Visible = isMemo;

            // 컬럼순서 재설정
            int vIndex = 9;
            gcSKJS_TTTAMT_PTNT.VisibleIndex = gcSKJS_TTTAMT_PTNT.Visible ? ++vIndex : -1;
            gcSKJS_RSTAMT_PTNT.VisibleIndex = gcSKJS_RSTAMT_PTNT.Visible ? ++vIndex : -1;
            gcSKJS_JSRSTCHAAMT_PTNT.VisibleIndex = gcSKJS_RSTCHAAMT.Visible ? ++vIndex : -1;
            gcSKJS_CHOGUM_PTNT.VisibleIndex = gcSKJS_CHOGUM_PTNT.Visible ? ++vIndex : -1;
            gcSKJS_JAEGUM_PTNT.VisibleIndex = gcSKJS_JAEGUM_PTNT.Visible ? ++vIndex : -1;
            gcSKJS_SUTAKAMT_PTNT.VisibleIndex = gcSKJS_SUTAKAMT_PTNT.Visible ? ++vIndex : -1;
            gcSKJS_UPLMTCHATTAMT_PTNT.VisibleIndex = gcSKJS_UPLMTCHATTAMT_PTNT.Visible ? ++vIndex : -1;
            gcSKJS_JBPTAMT_PTNT.VisibleIndex = gcSKJS_JBPTAMT_PTNT.Visible ? ++vIndex : -1;
            gcSKJS_CHOCNT_PTNT.VisibleIndex = gcSKJS_CHOCNT_PTNT.Visible ? ++vIndex : -1;
            gcSKJS_CHONCNT_PTNT.VisibleIndex = gcSKJS_CHONCNT_PTNT.Visible ? ++vIndex : -1;
            gcSKJS_JAECNT_PTNT.VisibleIndex = gcSKJS_JAECNT_PTNT.Visible ? ++vIndex : -1;
            gcSKJS_JAENCNT_PTNT.VisibleIndex = gcSKJS_JAENCNT_PTNT.Visible ? ++vIndex : -1;
            gcSKJS_EXAMC_PTNT.VisibleIndex = gcSKJS_EXAMC_PTNT.Visible ? ++vIndex : -1;
            gcSKJS_ORDDAYS_PTNT.VisibleIndex = gcSKJS_ORDDAYS_PTNT.Visible ? ++vIndex : -1;
            gcSKJS_ORDCNT_PTNT.VisibleIndex = gcSKJS_ORDCNT_PTNT.Visible ? ++vIndex : -1;
            gcMEMO_PTNT.VisibleIndex = gcMEMO_PTNT.Visible ? ++vIndex : -1;
            
            RefreshGridPtnt();
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

        private void grdPtntView_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.RowHandle1 % 2 == 0 && e.RowHandle2 % 2 != 0)
            {
                // 그리드의 OptionsView.AllowCellMerge를 True로 해놓아야 한다.
                // 셀 머지를 하지 않는 필드는 속성에서 OptionsColumn.AllowMerge를 False로 해놓았다.
                if (e.CellValue1.ToString() != e.CellValue2.ToString())
                {
                    e.Merge = false;
                    e.Handled = true;
                }
                else
                {
                    e.Merge = true;
                    e.Handled = true;
                }
            }
            else
            {
                e.Merge = false;
                e.Handled = true;
            }
        }

        private void QueryDetail()
        {
            if (grdMainView.FocusedRowHandle < 0) return;

            string jsdemseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcJSDEMSEQ).ToString();
            string jsreday = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcJSREDAY).ToString();
            string cnecno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcCNECNO).ToString();
            string dcount = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDCOUNT).ToString();
            string jsyyseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcJSYYSEQ).ToString();
            string demno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDEMNO).ToString();

            bool isUplmtamt = false; // 상한가 자료가 있느지 여부
            bool isIjUplmtchaamt = false;
            bool isJjUplmtchaamt = false;
            bool isJsMemo = false; // 메모가 있는지 여부

            CDataDetail sum = new CDataDetail();
            sum.Clear();

            CDataDetail sum2 = new CDataDetail();
            sum2.Clear();

            List<CDataDetail> list = new List<CDataDetail>();
            grdDetail.DataSource = null;
            grdDetail.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                string sql = "";
                sql += Environment.NewLine + "SELECT N0404.JSDEMSEQ";  // 정산심사차수
                sql += Environment.NewLine + "     , N0404.JSREDAY";   // 정산통보일자
                sql += Environment.NewLine + "     , N0404.CNECNO";    // 접수번호
                sql += Environment.NewLine + "     , N0404.DCOUNT";    // 청구서 일련번호
                sql += Environment.NewLine + "     , N0404.JSYYSEQ";   // 정산연번
                sql += Environment.NewLine + "     , N0404.JSSEQNO";   // 정산일련번호
                sql += Environment.NewLine + "     , N0404.EPRTNO";    // 명세서 일련번호
                sql += Environment.NewLine + "     , N0404.LNO";       // 줄번호
                sql += Environment.NewLine + "     , N0404.SKJJRMK";   // 이전심결사항-조정사유
                sql += Environment.NewLine + "     , N0404.SKJJRMK2";  // 이전심결사항-조정사유
                sql += Environment.NewLine + "     , N0404.SKBGIHO";   // 이전심결사항-코드
                sql += Environment.NewLine + "     , N0404.SKDANGA";   // 이전심결사항-단가
                sql += Environment.NewLine + "     , N0404.SKDQTY";    // 이전심결사항-일투 인정횟수
                sql += Environment.NewLine + "     , N0404.SKDDAY";    // 이전심결사항-총투 인정횟수
                sql += Environment.NewLine + "     , N0404.SKIJAMT";   // 이전심결사항-인정금액
                sql += Environment.NewLine + "     , N0404.SKJJAMT";   // 이전심결사항-조정금액
                sql += Environment.NewLine + "     , N0404.JSJJRMK";   // 정산심결사항-조정사유
                sql += Environment.NewLine + "     , N0404.JSJJRMK2";  // 정산심결사항-조정사유
                sql += Environment.NewLine + "     , N0404.JSBGIHO";   // 정산심결사항-코드
                sql += Environment.NewLine + "     , N0404.JSDANGA";   // 정산심결사항-단가
                sql += Environment.NewLine + "     , N0404.JSDQTY";    // 정산심결사항-일투 인정횟수
                sql += Environment.NewLine + "     , N0404.JSDDAY";    // 정산심결사항-총투 인정횟수
                sql += Environment.NewLine + "     , N0404.JSIJAMT";   // 정산심결사항-인정금액
                sql += Environment.NewLine + "     , N0404.JSJJAMT";   // 정산심결사항-조정금액
                sql += Environment.NewLine + "     , N0404.JSMEMO";    // 정산심결사항-비고(조정사유내역)
                sql += Environment.NewLine + "     , N0403.PNM";       // 수진자명
                sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE = N0404.SKBGIHO) AS SKBGIHONM";
                sql += Environment.NewLine + "     , (SELECT TOP 1 I09.PCODENM FROM TI09 I09 WHERE I09.PCODE = N0404.JSBGIHO) AS JSBGIHONM";
                sql += Environment.NewLine + "     , N0404.SKCNTQTY";
                sql += Environment.NewLine + "     , N0404.JSCNTQTY";
                sql += Environment.NewLine + "     , N0404.SKUPLMTAMT";
                sql += Environment.NewLine + "     , N0404.SKIJUPLMTCHAAMT";
                sql += Environment.NewLine + "     , N0404.SKJJUPLMTCHAAMT";
                sql += Environment.NewLine + "     , N0404.JSUPLMTAMT";
                sql += Environment.NewLine + "     , N0404.JSIJUPLMTCHAAMT";
                sql += Environment.NewLine + "     , N0404.JSJJUPLMTCHAAMT";
                sql += Environment.NewLine + "     , N0404.HANGNO";
                sql += Environment.NewLine + "     , N0401.DEMNO";
                sql += Environment.NewLine + "  FROM TIE_N0404 N0404 INNER JOIN TIE_N0403 N0403 ON N0403.JSDEMSEQ=N0404.JSDEMSEQ AND N0403.JSREDAY=N0404.JSREDAY AND N0403.CNECNO=N0404.CNECNO AND N0403.DCOUNT=N0404.DCOUNT AND N0403.JSYYSEQ=N0404.JSYYSEQ AND N0403.JSSEQNO=N0404.JSSEQNO AND N0403.EPRTNO=N0404.EPRTNO";
                sql += Environment.NewLine + "                       INNER JOIN TIE_N0401 N0401 ON N0401.JSDEMSEQ=N0404.JSDEMSEQ AND N0401.JSREDAY=N0404.JSREDAY AND N0401.CNECNO=N0404.CNECNO AND N0401.DCOUNT=N0404.DCOUNT";
                if (cboQueryOption.SelectedIndex == 1)
                {
                    // 청구번호별
                    sql += Environment.NewLine + " WHERE N0401.DEMNO = '" + demno + "' ";
                    sql += Environment.NewLine + " ORDER BY N0401.DEMNO, N0404.EPRTNO, N0404.LNO";
                }
                else if (cboQueryOption.SelectedIndex == 2)
                {
                    // 청구월별
                    sql += Environment.NewLine + " WHERE N0401.DEMNO LIKE '" + demno.Substring(0, 6) + "%' ";
                    sql += Environment.NewLine + " ORDER BY N0401.DEMNO, N0404.EPRTNO, N0404.LNO";
                }
                else if (cboQueryOption.SelectedIndex == 3)
                {
                    // 청구년도별
                    sql += Environment.NewLine + " WHERE N0401.DEMNO LIKE '" + demno.Substring(0, 4) + "%' ";
                    sql += Environment.NewLine + " ORDER BY N0401.DEMNO, N0404.EPRTNO, N0404.LNO";
                }
                else
                {
                    // 기본
                    sql += Environment.NewLine + " WHERE N0404.JSDEMSEQ = '" + jsdemseq + "' ";
                    sql += Environment.NewLine + "   AND N0404.JSREDAY  = '" + jsreday + "' ";
                    sql += Environment.NewLine + "   AND N0404.CNECNO   = '" + cnecno + "' ";
                    sql += Environment.NewLine + "   AND N0404.DCOUNT   = '" + dcount + "' ";
                    sql += Environment.NewLine + "   AND N0404.JSYYSEQ  = '" + jsyyseq + "' ";
                    sql += Environment.NewLine + " ORDER BY N0404.JSDEMSEQ, N0404.JSREDAY, N0404.CNECNO, N0404.DCOUNT, N0404.JSYYSEQ, N0404.JSSEQNO, N0404.EPRTNO, N0404.LNO";
                }

                int no = 0;
                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataDetail data = new CDataDetail();
                    data.Clear();

                    data.SKJSDIV = 0;
                    data.NO = ++no;

                    data.JSDEMSEQ = reader["JSDEMSEQ"].ToString(); // 정산심사차수
                    data.JSREDAY = reader["JSREDAY"].ToString();   // 정산통보일자
                    data.CNECNO = reader["CNECNO"].ToString();     // 접수번호
                    data.DCOUNT = reader["DCOUNT"].ToString();     // 청구서 일련번호
                    data.JSYYSEQ = reader["JSYYSEQ"].ToString();   // 정산연번
                    data.JSSEQNO = reader["JSSEQNO"].ToString();   // 정산일련번호
                    data.EPRTNO = reader["EPRTNO"].ToString();     // 명세서 일련번호
                    data.LNO = reader["LNO"].ToString();           // 줄번호
                    data.SKJJRMK = reader["SKJJRMK"].ToString().TrimEnd();   // 이전심결사항-조정사유
                    data.SKJJRMK2 = reader["SKJJRMK2"].ToString().TrimEnd(); // 이전심결사항-조정사유2
                    data.SKBGIHO = reader["SKBGIHO"].ToString();   // 이전심결사항-코드
                    data.SKDANGA = ToFloat(reader["SKDANGA"].ToString());     // 이전심결사항-단가
                    data.SKDQTY = ToFloat(reader["SKDQTY"].ToString());       // 이전심결사항-일투 인정횟수
                    data.SKDDAY = ToLong(reader["SKDDAY"].ToString());       // 이전심결사항-총투 인정횟수
                    data.SKIJAMT = ToLong(reader["SKIJAMT"].ToString());     // 이전심결사항-인정금액
                    data.SKJJAMT = ToLong(reader["SKJJAMT"].ToString());     // 이전심결사항-조정금액
                    data.JSJJRMK = reader["JSJJRMK"].ToString().TrimEnd();   // 정산심결사항-조정사유
                    data.JSJJRMK2 = reader["JSJJRMK2"].ToString().TrimEnd(); // 정산심결사항-조정사유2
                    data.JSBGIHO = reader["JSBGIHO"].ToString();   // 정산심결사항-코드
                    data.JSDANGA = ToFloat(reader["JSDANGA"].ToString());     // 정산심결사항-단가
                    data.JSDQTY = ToFloat(reader["JSDQTY"].ToString());       // 정산심결사항-일투 인정횟수
                    data.JSDDAY = ToLong(reader["JSDDAY"].ToString());       // 정산심결사항-총투 인정횟수
                    data.JSIJAMT = ToLong(reader["JSIJAMT"].ToString());     // 정산심결사항-인정금액
                    data.JSJJAMT = ToLong(reader["JSJJAMT"].ToString());     // 정산심결사항-조정금액
                    data.JSMEMO = reader["JSMEMO"].ToString().TrimEnd();     // 정산심결사항-비고(조정사유내역)
                    data.PNM = reader["PNM"].ToString().TrimEnd();           // 수진자명
                    data.SKBGIHONM = reader["SKBGIHONM"].ToString();
                    data.JSBGIHONM = reader["JSBGIHONM"].ToString();
                    data.SKCNTQTY = ToFloat(reader["SKCNTQTY"].ToString());
                    data.JSCNTQTY = ToFloat(reader["JSCNTQTY"].ToString());
                    data.SKUPLMTAMT = ToLong(reader["SKUPLMTAMT"].ToString());
                    data.SKIJUPLMTCHAAMT = ToLong(reader["SKIJUPLMTCHAAMT"].ToString());
                    data.SKJJUPLMTCHAAMT = ToLong(reader["SKJJUPLMTCHAAMT"].ToString());
                    data.JSUPLMTAMT = ToLong(reader["JSUPLMTAMT"].ToString());
                    data.JSIJUPLMTCHAAMT = ToLong(reader["JSIJUPLMTCHAAMT"].ToString());
                    data.JSJJUPLMTCHAAMT = ToLong(reader["JSJJUPLMTCHAAMT"].ToString());
                    data.HANGNO = reader["HANGNO"].ToString();
                    data.DEMNO = reader["DEMNO"].ToString();

                    if (data.SKUPLMTAMT != 0 || data.JSUPLMTAMT != 0) isUplmtamt = true;
                    if (data.SKIJUPLMTCHAAMT != 0 || data.JSIJUPLMTCHAAMT != 0) isIjUplmtchaamt = true;
                    if (data.SKJJUPLMTCHAAMT != 0 || data.JSJJUPLMTCHAAMT != 0) isJjUplmtchaamt = true;
                    if (data.JSMEMO != "") isJsMemo = true;

                    list.Add(data);

                    CDataDetail data2 = new CDataDetail();
                    data2.Clear();

                    data2.SKJSDIV = 1;
                    data2.NO = no;

                    data2.JSDEMSEQ = data.JSDEMSEQ;
                    data2.JSREDAY = data.JSREDAY;
                    data2.CNECNO = data.CNECNO;
                    data2.DCOUNT = data.DCOUNT;
                    data2.JSYYSEQ = data.JSYYSEQ;
                    data2.JSSEQNO = data.JSSEQNO;
                    data2.EPRTNO = data.EPRTNO;
                    data2.LNO = data.LNO;
                    data2.SKJJRMK = data.SKJJRMK;
                    data2.SKJJRMK2 = data.SKJJRMK2;
                    data2.SKBGIHO = data.SKBGIHO;
                    data2.SKDANGA = data.SKDANGA;
                    data2.SKDQTY = data.SKDQTY;
                    data2.SKDDAY = data.SKDDAY;
                    data2.SKIJAMT = data.SKIJAMT;
                    data2.SKJJAMT = data.SKJJAMT;
                    data2.JSJJRMK = data.JSJJRMK;
                    data2.JSJJRMK2 = data.JSJJRMK2;
                    data2.JSBGIHO = data.JSBGIHO;
                    data2.JSDANGA = data.JSDANGA;
                    data2.JSDQTY = data.JSDQTY;
                    data2.JSDDAY = data.JSDDAY;
                    data2.JSIJAMT = data.JSIJAMT;
                    data2.JSJJAMT = data.JSJJAMT;
                    data2.JSMEMO = data.JSMEMO;
                    data2.PNM = data.PNM;
                    data2.SKBGIHONM = data.SKBGIHONM;
                    data2.JSBGIHONM = data.JSBGIHONM;
                    data2.SKCNTQTY = data.SKCNTQTY;
                    data2.JSCNTQTY = data.JSCNTQTY;
                    data2.SKUPLMTAMT = data.SKUPLMTAMT;
                    data2.SKIJUPLMTCHAAMT = data.SKIJUPLMTCHAAMT;
                    data2.SKJJUPLMTCHAAMT = data.SKJJUPLMTCHAAMT;
                    data2.JSUPLMTAMT = data.JSUPLMTAMT;
                    data2.JSIJUPLMTCHAAMT = data.JSIJUPLMTCHAAMT;
                    data2.JSJJUPLMTCHAAMT = data.JSJJUPLMTCHAAMT;
                    data2.HANGNO = data.HANGNO;
                    data2.DEMNO = data.DEMNO;

                    list.Add(data2);

                    // 합계
                    sum.SKJSDIV = 0;
                    sum.EPRTNO = "합계";
                    sum.SKIJAMT += data.SKIJAMT;
                    sum.SKJJAMT += data.SKJJAMT;
                    sum.JSIJAMT += data.JSIJAMT;
                    sum.JSJJAMT += data.JSJJAMT;
                    sum.SKIJUPLMTCHAAMT += data.SKIJUPLMTCHAAMT;
                    sum.SKJJUPLMTCHAAMT += data.SKJJUPLMTCHAAMT;

                    sum2.SKJSDIV = 1;
                    sum2.EPRTNO = "합계";
                    sum2.SKIJAMT += data.SKIJAMT;
                    sum2.SKJJAMT += data.SKJJAMT;
                    sum2.JSIJAMT += data.JSIJAMT;
                    sum2.JSJJAMT += data.JSJJAMT;
                    sum2.JSIJUPLMTCHAAMT += data.JSIJUPLMTCHAAMT;
                    sum2.JSJJUPLMTCHAAMT += data.JSJJUPLMTCHAAMT;

                    return true;
                });
            }

            if (list.Count > 0)
            {
                list.Add(sum);
                list.Add(sum2);
            }
            RefreshGridDetail();

            // 금액이 없는 자료는 숨김
            gcSKJS_UPLMTAMT_DETAIL.Visible = isUplmtamt;
            gcSKJS_IJUPLMTCHAAMT_DETAIL.Visible = isIjUplmtchaamt;
            gcSKJS_JJUPLMTCHAAMT_DETAIL.Visible = isJjUplmtchaamt;
            gcJSMEMO_DETAIL.Visible = isJsMemo;

            // 컬럼순서 재설정
            int vIndex = 15;
            gcSKJS_UPLMTAMT_DETAIL.VisibleIndex = gcSKJS_UPLMTAMT_DETAIL.Visible ? ++vIndex : -1;
            gcSKJS_IJUPLMTCHAAMT_DETAIL.VisibleIndex = gcSKJS_IJUPLMTCHAAMT_DETAIL.Visible ? ++vIndex : -1;
            gcSKJS_JJUPLMTCHAAMT_DETAIL.VisibleIndex = gcSKJS_JJUPLMTCHAAMT_DETAIL.Visible ? ++vIndex : -1;
            gcJSMEMO_DETAIL.VisibleIndex = gcJSMEMO_DETAIL.Visible ? ++vIndex : -1;

            RefreshGridPtnt();
        }

        private void RefreshGridDetail()
        {
            if (grdDetail.InvokeRequired)
            {
                // 폼 이외의 스레드에서 호출한 경우
                grdDetail.BeginInvoke(new Action(() => grdDetailView.RefreshData()));
            }
            else
            {
                // 폼에서 호출한 경우
                grdDetailView.RefreshData();
                Application.DoEvents();
            }
        }

        private void grdDetailView_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.RowHandle1 % 2 == 0 && e.RowHandle2 % 2 != 0)
            {
                // 그리드의 OptionsView.AllowCellMerge를 True로 해놓아야 한다.
                // 셀 머지를 하지 않는 필드는 속성에서 OptionsColumn.AllowMerge를 False로 해놓았다.
                if (e.CellValue1.ToString() != e.CellValue2.ToString())
                {
                    e.Merge = false;
                    e.Handled = true;
                }
                else
                {
                    e.Merge = true;
                    e.Handled = true;
                }
            }
            else
            {
                e.Merge = false;
                e.Handled = true;
            }
        }

        private void QuerySutak()
        {
            if (grdMainView.FocusedRowHandle < 0) return;

            string jsdemseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcJSDEMSEQ).ToString();
            string jsreday = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcJSREDAY).ToString();
            string cnecno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcCNECNO).ToString();
            string dcount = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDCOUNT).ToString();
            string jsyyseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcJSYYSEQ).ToString();
            string demno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDEMNO).ToString();

            CDataSutak sum = new CDataSutak();
            sum.Clear();

            List<CDataSutak> list = new List<CDataSutak>();
            grdSutak.DataSource = null;
            grdSutak.DataSource = list;

            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                string sql = "";
                sql += Environment.NewLine + "SELECT N0405.JSDEMSEQ";  // 정산심사차수
                sql += Environment.NewLine + "     , N0405.JSREDAY";   // 정산통보일자
                sql += Environment.NewLine + "     , N0405.CNECNO";    // 접수번호
                sql += Environment.NewLine + "     , N0405.DCOUNT";    // 청구서 일련번호
                sql += Environment.NewLine + "     , N0405.JSYYSEQ";   // 정산연번
                sql += Environment.NewLine + "     , N0405.JSSEQNO";   // 정산일련번호
                sql += Environment.NewLine + "     , N0405.EPRTNO";    // 명세서 일련번호
                sql += Environment.NewLine + "     , N0405.LNO";       // 줄번호
                sql += Environment.NewLine + "     , N0405.SUTAKID";   // 수탁기관기호
                sql += Environment.NewLine + "     , N0405.SUTAKAMT";  // 위탁검사직접지급금
                sql += Environment.NewLine + "     , N0405.MEMO";      // 비고사항
                sql += Environment.NewLine + "     , N0401.DEMNO";
                sql += Environment.NewLine + "  FROM TIE_N0405 N0405 INNER JOIN TIE_N0401 N0401 ON N0401.JSDEMSEQ=N0405.JSDEMSEQ AND N0401.JSREDAY=N0405.JSREDAY AND N0401.CNECNO=N0405.CNECNO AND N0401.DCOUNT=N0405.DCOUNT";
                if (cboQueryOption.SelectedIndex == 1)
                {
                    // 청구번호별
                    sql += Environment.NewLine + " WHERE N0401.DEMNO = '" + demno + "' ";
                    sql += Environment.NewLine + " ORDER BY N0401.DEMNO, N0404.EPRTNO, N0404.LNO";
                }
                else if (cboQueryOption.SelectedIndex == 2)
                {
                    // 청구월별
                    sql += Environment.NewLine + " WHERE N0401.DEMNO LIKE '" + demno.Substring(0, 6) + "%' ";
                    sql += Environment.NewLine + " ORDER BY N0401.DEMNO, N0404.EPRTNO, N0404.LNO";
                }
                else if (cboQueryOption.SelectedIndex == 3)
                {
                    // 청구년도별
                    sql += Environment.NewLine + " WHERE N0401.DEMNO LIKE '" + demno.Substring(0, 4) + "%' ";
                    sql += Environment.NewLine + " ORDER BY N0401.DEMNO, N0404.EPRTNO, N0404.LNO";
                }
                else
                {
                    // 기본
                    sql += Environment.NewLine + " WHERE N0405.JSDEMSEQ = '" + jsdemseq + "'";
                    sql += Environment.NewLine + "   AND N0405.JSREDAY  = '" + jsreday + "'";
                    sql += Environment.NewLine + "   AND N0405.CNECNO   = '" + cnecno + "'";
                    sql += Environment.NewLine + "   AND N0405.DCOUNT   = '" + dcount + "'";
                    sql += Environment.NewLine + "   AND N0405.JSYYSEQ  = '" + jsyyseq + "'";
                    sql += Environment.NewLine + " ORDER BY N0405.JSDEMSEQ, N0405.JSREDAY, N0405.CNECNO, N0405.DCOUNT, N0405.JSYYSEQ, N0405.JSSEQNO, N0405.EPRTNO, N0405.LNO";
                }

                MetroLib.SqlHelper.GetDataReader(sql, conn, delegate(OleDbDataReader reader)
                {
                    CDataSutak data = new CDataSutak();
                    data.Clear();
                    data.JSDEMSEQ = reader["JSDEMSEQ"].ToString(); // 정산심사차수
                    data.JSREDAY = reader["JSREDAY"].ToString();  // 정산통보일자
                    data.CNECNO = reader["CNECNO"].ToString();   // 접수번호
                    data.DCOUNT = reader["DCOUNT"].ToString();   // 청구서 일련번호
                    data.JSYYSEQ = reader["JSYYSEQ"].ToString();  // 정산연번
                    data.JSSEQNO = reader["JSSEQNO"].ToString();  // 정산일련번호
                    data.EPRTNO = reader["EPRTNO"].ToString();   // 명세서 일련번호
                    data.LNO = reader["LNO"].ToString();      // 줄번호
                    data.SUTAKID = reader["SUTAKID"].ToString();  // 수탁기관기호
                    data.SUTAKAMT = reader["SUTAKAMT"].ToString(); // 위탁검사직접지급금
                    data.MEMO = reader["MEMO"].ToString().TrimEnd();     // 비고사항
                    data.DEMNO = reader["DEMNO"].ToString();

                    list.Add(data);

                    sum.EPRTNO = "합계";
                    sum.SUTAKAMT += data.SUTAKAMT;

                    return true;
                });
            }
            if (list.Count > 0)
            {
                list.Add(sum);
            }
            RefreshGridSutak();
        }

        private void RefreshGridSutak()
        {
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
            if (tabJobdiv.SelectedIndex == 0) printableComponentLink.Component = grdMain;
            else if (tabJobdiv.SelectedIndex == 1) printableComponentLink.Component = grdPtnt;
            else if (tabJobdiv.SelectedIndex == 2) printableComponentLink.Component = grdDetail;
            else if (tabJobdiv.SelectedIndex == 3) printableComponentLink.Component = grdSutak;
            //printableComponentLink.ShowPreview(); // <-- 아래 세 줄로 변경
            printableComponentLink.CreateDocument();
            DevExpress.XtraPrinting.PrintTool printTool = new DevExpress.XtraPrinting.PrintTool(printableComponentLink.PrintingSystemBase);
            printTool.ShowRibbonPreview();
        }

        private void printableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            string title = "";
            string printInfo = "";
            if (tabJobdiv.SelectedIndex == 0) title = " (리스트)";
            if (tabJobdiv.SelectedIndex == 1) title = " (명단)";
            if (tabJobdiv.SelectedIndex == 2) title = " (상세)";
            if (tabJobdiv.SelectedIndex == 3) title = " (위탁)";

            if (tabJobdiv.SelectedIndex == 0)
            {
                if (txtFrdt.Text.ToString() == "" && txtTodt.Text.ToString() == "")
                {
                    printInfo = "전체기간";
                }
                else
                {
                    printInfo = "기간 : " + txtFrdt.Text.ToString() + "~" + txtTodt.Text.ToString();
                }
                if (txtCnecno.Text.ToString() != "") printInfo += ", 접수번호 : " + txtCnecno.Text.ToString();
                if (txtDemno.Text.ToString() != "") printInfo += ", 청구번호 : " + txtDemno.Text.ToString();
                if (txtJsdemseq.Text.ToString() != "") printInfo += ", 정산심사차수 : " + txtJsdemseq.Text.ToString();
            }
            else
            {
                if (grdMainView.FocusedRowHandle < 0) return;

                string jsdemseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcJSDEMSEQ).ToString();
                string jsreday = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcJSREDAY).ToString();
                string cnecno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcCNECNO).ToString();
                string demseq = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDEMSEQ).ToString();
                string demno = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcDEMNO).ToString();
                string jsredept = grdMainView.GetRowCellValue(grdMainView.FocusedRowHandle, gcJSREDEPT).ToString();

                if (cboQueryOption.SelectedIndex == 0)
                {
                    // 기본
                    printInfo = "정산심사 차수:" + jsdemseq + ", 정산심사 통보일자:" + jsreday + ", 접수번호:" + cnecno + ", 심사차수:" + demseq + ", 청구번호:" + demno + ", 담당자:" + jsredept;
                }
                else if (cboQueryOption.SelectedIndex == 1)
                {
                    // 청구번호
                    printInfo = "청구번호:" + demno;
                }
                else if (cboQueryOption.SelectedIndex == 1)
                {
                    // 청구월
                    printInfo = "청구월:" + demno.Substring(0, 6);
                }
                else if (cboQueryOption.SelectedIndex == 1)
                {
                    // 청구년도
                    printInfo = "청구년도:" + demno.Substring(0, 4);
                }
            }

            //
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString("[자보]정산심사내역서" + title, Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
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
            e.Graph.DrawString("ADD0720E_JABO", Color.Black, new RectangleF(0, 0, 120, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(sysDate + " " + sysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }
    }
}
