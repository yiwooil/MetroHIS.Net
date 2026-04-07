using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;

namespace ADB0206Q
{
    public partial class ADB0206Q : Form
    {
        private String m_User;
        private String m_Pwd;

        private String m_SysDate;
        private String m_SysTime;

        private bool IsFirst;

        public ADB0206Q()
        {
            InitializeComponent();
            IsFirst = true;
        }

        public ADB0206Q(String user, String pwd)
            : this()
        {
            m_User = user;
            m_Pwd = pwd;
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
            grdMain.DataSource = null;
            List<CData> list = new List<CData>();
            List<CData> list2 = new List<CData>();
            grdMain.DataSource = list;

            string strConn = DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                m_SysDate = CUtil.GetSysDate(conn);
                m_SysTime = CUtil.GetSysTime(conn);

                // by knbang 20150312 사생활보호환자 (p)표기 위치(1:환자이름 뒤로 )
                String strPPosition = GetTA972("ADB","ADB0206Q","4",conn);

                // 20170113 by hhi. 간호정보조사지 발병일 항목 추(대구보건대학병원 요청)
                String strTV95fg = GetTA972("ADB", "ADB0206Q", "7", conn);

                // 20170713 By Moon. 
                String strFndFg = GetTA972("ADA", "ADA0101E", "2", conn);

                // 20180419 KYJ 안산튼튼병원 주민번호 전부 표시되도록 요청
                String strResIDAll = GetTA972("ADB", "ADB0206Q", "9", conn);

                // 20190604 BJW 인천힘찬병원 퇴원예정일 표기형식을 MM/DD 형식으로 표기
                String strBedodtFg = GetTA972("ADB", "ADB0206Q", "10", conn);

                //2019.06.11 BJW 강원도재활병원 엑셀 출력시 상병을 주상병으로 표시 요청
                String strPtyFg = GetTA972("ADB", "ADB0206Q", "11", conn);

                String strBDate = txtDate.Text.ToString();
                if (strBDate == "") strBDate = m_SysDate;
                String strPid = txtPid.Text.ToString();
                String strPnm = txtPnm.Text.ToString();
                String strFamnm = txtFamnm.Text.ToString();
                String strResid = txtResid.Text.ToString();
                if (strResid.Length > 6) strResid = strResid.Substring(0, 6);
                String strQfycd = ""; // 나중에
                String strDptcd = ""; // 나중에
                String strWdid = ""; // 나중에
                String strChkP = ""; ; // 나중에
                String strBedipthcd = ""; // 나중에
                String strCaseWrknm = ""; // 나중에

                string sql = "";
                sql += System.Environment.NewLine + "SELECT A04.PID";
                sql += System.Environment.NewLine + "     , A01.PNM";
                sql += System.Environment.NewLine + "     , A01.RESID";
                sql += System.Environment.NewLine + "     , A01.RESID_WON";
                sql += System.Environment.NewLine + "     , A01.PSEX";
                sql += System.Environment.NewLine + "     , A01.BTHDT";
                sql += System.Environment.NewLine + "     , A01.ADDR_WON ADDR";
                sql += System.Environment.NewLine + "     , A01.HTELNO";
                sql += System.Environment.NewLine + "     , ISNULL(A01.DRRMK,'')+CASE WHEN ISNULL(A01.WONRMK,'') <> '' THEN '['+ISNULL(A01.WONRMK,'')+']' ELSE ISNULL(A01.WONRMK,'') END DRRMK";
                sql += System.Environment.NewLine + "     , A04.BEDEDT";
                sql += System.Environment.NewLine + "     , T04.WDID2 WARDID";
                sql += System.Environment.NewLine + "     , T04.RMID2 RMID";
                sql += System.Environment.NewLine + "     , T04.BDID2 BEDID";
                sql += System.Environment.NewLine + "     , T04.DPTC2 DPTCD";
                sql += System.Environment.NewLine + "     , T04.PDR2 PDRID";
                sql += System.Environment.NewLine + "     , T04.SPEXF SPLFG";
                sql += System.Environment.NewLine + "     , T03.QFYCD QLFYCD";
                sql += System.Environment.NewLine + "     , T03.GONSGB";
                sql += System.Environment.NewLine + "     , A10.FAMNM";
                sql += System.Environment.NewLine + "     , A07.DRNM";
                sql += System.Environment.NewLine + "     , A09.DPTNM";
                sql += System.Environment.NewLine + "     , A88.CDNM QFYNM";
                sql += System.Environment.NewLine + "     , T04.BDGRD BEDGRD";
                sql += System.Environment.NewLine + "     , A06.TELNO2";
                sql += System.Environment.NewLine + "     , A06.BPTNT";
                sql += System.Environment.NewLine + "     , A06.BJSTS";
                sql += System.Environment.NewLine + "     , A06.JPTNT";
                sql += System.Environment.NewLine + "     , A06.JUSTS";
                sql += System.Environment.NewLine + "     , T03.PDIV PDIV";
                sql += System.Environment.NewLine + "     , A04.BEDODT";
                sql += System.Environment.NewLine + "     , DATEDIFF(d, A04.BEDEDT, CASE WHEN ISNULL(A04.BEDODT,'') = '' THEN '" + strBDate + "' ELSE A04.BEDODT END)+1 ILSU";
                sql += System.Environment.NewLine + "     , A04.BEDIPTHCD";
                sql += System.Environment.NewLine + "     , A10.FTEL";
                sql += System.Environment.NewLine + "     , A10.FADDR";
                sql += System.Environment.NewLine + "     , A04.JINRMK";
                sql += System.Environment.NewLine + "     , U01.OPDT";
                sql += System.Environment.NewLine + "     , U01.RSVOP";
                sql += System.Environment.NewLine + "     , A01.OTELNO";
                sql += System.Environment.NewLine + "     , A04C.BEDINTENTDT";
                sql += System.Environment.NewLine + "     , ISNULL(CONVERT(VARCHAR(8), DATEADD(d, -1, DATEADD(m, 2, A04C.BEDINTENTDT)), 112), CONVERT(VARCHAR(8), DATEADD(d, -1, DATEADD(m, 2, A04.BEDEDT)), 112)) AS BEDINTENTDT2";
				sql += System.Environment.NewLine + "     , (SELECT TOP 1 ADLRT FROM TA49 A49 WHERE A49.PID = A04.PID AND A49.BEDEDT = A04.BEDEDT AND ISNULL(A49.DELFG,'') = '' AND ISNULL(A49.SETFG,'') = 'Y' ORDER BY BEDEDT, SEQ DESC) AS ADLRT";
				sql += System.Environment.NewLine + "     , ISNULL(A01.DRRMK,'') DRRMK2";
                sql += System.Environment.NewLine + "     , ISNULL(A01.WONRMK,'') WONRMK";
                sql += System.Environment.NewLine + "     , CASE WHEN E12.PRIVATECHK = '1' THEN '사생활.' ELSE '' END + ";
				sql += System.Environment.NewLine + "       CASE WHEN (SELECT ALLERGYCHK FROM TE12H_AL (NOLOCK) WHERE PID = A04.PID) = '1' THEN 'A.' ELSE '' END + ";
				sql += System.Environment.NewLine + "       CASE WHEN (SELECT COUNT(*) FROM TE55 E55 (NOLOCK) WHERE E55.PID = A04.PID AND ISNULL(E55.UPDDT,'') ='' AND E55.SEQ = (SELECT MAX(SEQ) FROM TE55 SUB WHERE SUB.PID = E55.PID AND ISNULL(SUB.UPDDT,'') ='')) >= 1 THEN '감.' ELSE '' END + ";
				sql += System.Environment.NewLine + "       CASE WHEN (SELECT COUNT(PID) CNT FROM TV12H WHERE PID = A04.PID AND BEDEDT = A04.BEDEDT AND ISNULL(FALLFG,'') ='1') >= 1 THEN '낙.' ELSE '' END + ";
				sql += System.Environment.NewLine + "       CASE WHEN (SELECT COUNT(PID) CNT FROM TV12H WHERE PID = A04.PID AND BEDEDT = A04.BEDEDT AND ISNULL(PSFG,'') ='1' ) >= 1 THEN '욕.' ELSE '' END + ";  
				sql += System.Environment.NewLine + "       CASE WHEN (SELECT COUNT(PID) CNT FROM TV12H WHERE PID = A04.PID AND BEDEDT = A04.BEDEDT AND ISNULL(PAINFG,'') ='1' ) >= 1 THEN '통.' ELSE '' END + " ; 
				sql += System.Environment.NewLine + "       CASE WHEN (SELECT COUNT(PID) CNT FROM TV12H WHERE PID = A04.PID AND BEDEDT = A04.BEDEDT AND ISNULL(RBFG,'') ='1' ) >= 1 THEN '억.' ELSE '' END AS PSTS ";
                sql += System.Environment.NewLine + "     , ISNULL(T03.QFYSB, '') QFYCD_SUB";
                sql += System.Environment.NewLine + "     , ISNULL(T03.PDIVS, '') PDIV_SUB";
                sql += System.Environment.NewLine + "     , ISNULL(T03.GONSB, '') GONSGB_SUB";
                sql += System.Environment.NewLine + "     , A88_2.CDNM QFYNM_SUB";
                sql += System.Environment.NewLine + "     , ISNULL(T02.BDODT, '') SCH_BDODT";
                sql += System.Environment.NewLine + "     , REPLACE(REPLACE(REPLACE(REPLACE(ISNULL(I67.REMARK ,''),'$',' '),CHAR(9),''),CHAR(10),''),CHAR(13),'') SIMRMK";
                sql += System.Environment.NewLine + "     , E12.PRIVATECHK";
                sql += System.Environment.NewLine + "     , E12.WARDREFUSAL";
                sql += System.Environment.NewLine + "  FROM TA04 a04 (NOLOCK) INNER JOIN VA01 A01 (nolock) ON A01.PID = A04.PID";
                sql += System.Environment.NewLine + "                         INNER JOIN TA10 A10 (nolock) ON A10.PID = A04.PID";
                sql += System.Environment.NewLine + "                         INNER JOIN TT03 T03 (nolock) ON T03.PID = A04.PID";
                sql += System.Environment.NewLine + "                                                     AND T03.BDEDT = A04.BEDEDT";
                sql += System.Environment.NewLine + "                                                     AND T03.CREDT = (SELECT MAX(CREDT) FROM TT03 A (nolock) WHERE A.PID = A04.PID AND A.BDEDT = A04.BEDEDT AND A.CREDT <= '" + strBDate + "') ";
                sql += System.Environment.NewLine + "                         INNER JOIN TT04 T04 (nolock) ON T04.PID = A04.PID";
                sql += System.Environment.NewLine + "                                                     AND T04.BDEDT = A04.BEDEDT ";
                sql += System.Environment.NewLine + "                                                     AND T04.CRDHM = (SELECT MAX(CRDHM) FROM TT04 A (nolock) WHERE A.PID = A04.PID AND A.BDEDT = A04.BEDEDT AND A.CRDHM <= '" + strBDate + "9999') ";
                sql += System.Environment.NewLine + "                         LEFT JOIN TU01 U01 (NOLOCK) ON U01.PID = A04.PID";
				sql += System.Environment.NewLine + "                                                    AND U01.OPDT >= A04.BEDEDT";
				sql += System.Environment.NewLine + "                                                    AND U01.OPDT+'/'+U01.DPTCD+'/'+CONVERT(VARCHAR,U01.OPSEQ)+'/'+CONVERT(VARCHAR,U01.SEQ) = (SELECT MIN(SUB.OPDT+'/'+SUB.DPTCD+'/'+CONVERT(VARCHAR,SUB.OPSEQ)+'/'+CONVERT(VARCHAR,SUB.SEQ)) ";
				sql += System.Environment.NewLine + "                                                                                                                                                FROM TU01 SUB (nolock) ";
				sql += System.Environment.NewLine + "                                                                                                                                               WHERE SUB.PID = A04.PID";
				sql += System.Environment.NewLine + "                                                                                                                                                 AND SUB.OPDT >= A04.BEDEDT";
                sql += System.Environment.NewLine + "                                                                                                                                                 AND ISNULL(SUB.CHGDT,'') = '' ";
				sql += System.Environment.NewLine + "                                                                                                                                                 AND ISNULL(SUB.CANCL,'') <> '1'";
                sql += System.Environment.NewLine + "                                                                                                                                             )";
                sql += System.Environment.NewLine + "	                      LEFT JOIN TA04C A04C (NOLOCK) ON A04C.PID = A04.PID AND A04C.BEDEDT = A04.BEDEDT AND A04C.BEDINTENTDT = (SELECT MAX(BEDINTENTDT) FROM TA04C Z WHERE Z.PID = A04C.PID AND Z.BEDEDT = A04C.BEDEDT) ";
                sql += System.Environment.NewLine + "                         LEFT JOIN TT02 T02 (NOLOCK) ON a04.PID = t02.PID AND a04.BEDEDT = t02.BDEDT";
                sql += System.Environment.NewLine + "	                      LEFT JOIN TI67 I67 (nolock) ON i67.PID = a04.PID AND i67.BDEDT = a04.BEDEDT AND i67.QFYCD = a04.QLFYCD ";
                sql += System.Environment.NewLine + "			              LEFT JOIN TE12H_PR E12 (nolock) ON e12.PID = a01.PID ";
                sql += System.Environment.NewLine + "			              LEFT JOIN TA88 a88 (nolock) ON a88.MST1CD = 'A' AND a88.MST2CD = '26' AND a88.MST3CD = T03.QFYCD ";
                sql += System.Environment.NewLine + "			              LEFT JOIN TA88 a88_2 (NOLOCK) ON a88_2.MST1CD = 'A' AND a88_2.MST2CD = '26' AND a88_2.MST3CD = T03.QFYSB";
                sql += System.Environment.NewLine + "			              LEFT JOIN TA06 a06 (nolock) ON a06.WARDID= T04.WDID2 AND a06.RMID= T04.RMID2 AND a06.BEDID = T04.BDID2";
                sql += System.Environment.NewLine + "			              LEFT JOIN TA07 a07 (nolock) ON a07.DRID = T04.PDR2 ";
                sql += System.Environment.NewLine + "			              LEFT JOIN TA09 a09 (nolock) ON a09.DPTCD = T04.DPTC2 ";
                sql += System.Environment.NewLine + " WHERE A04.BEDIDT <= '" + strBDate + "'";
                sql += System.Environment.NewLine + "   AND (ISNULL(A04.BEDODT, '') = '' OR A04.BEDODT > '" + strBDate + "')";
                sql += System.Environment.NewLine + "   AND A04.WARDID <> 'ER1'";
                sql += System.Environment.NewLine + "   AND A04.PID NOT LIKE 'T%'";
                sql += System.Environment.NewLine + "   AND T04.WDID2 NOT LIKE 'WER%'";

                if (strPid != "")
                {
                    sql += System.Environment.NewLine + "   AND A04.PID = '" + strPid + "'";
                }
                if (strPnm != "")
                {
                    if (strFndFg == "1")
                    {
                        sql += System.Environment.NewLine + "   AND A01.PNM LIKE '" + strPnm + "%'";
                    }
                    else if (strFndFg == "2")
                    {
                        sql += System.Environment.NewLine + "   AND A01.PNM LIKE '%" + strPnm + "%'";
                    }
                    else
                    {
                        sql += System.Environment.NewLine + "   AND A01.PNM = '" + strPnm + "'";
                    }
                }
                if (strFamnm != "")
                {
                    if (strFndFg == "1")
                    {
                        sql += System.Environment.NewLine + "   AND A01.FAMNM LIKE '" + strFamnm + "%'";
                    }
                    else if (strFndFg == "2")
                    {
                        sql += System.Environment.NewLine + "   AND A01.FAMNM LIKE '%" + strFamnm + "%'";
                    }
                    else
                    {
                        sql += System.Environment.NewLine + "   AND A01.FAMNM = '" + strFamnm + "'";
                    }
                }
                if (strResid != "")
                {
                    sql += System.Environment.NewLine + "   AND A01.RESID1 LIKE '" + strResid + "%'";
                }
                if (strQfycd != "")
                {
                    sql += System.Environment.NewLine + "   AND T03.QFYCD LIKE '" + strQfycd + "%'";
                }
                if (strDptcd != "")
                {
                    sql += System.Environment.NewLine + "   AND T04.DPTC2 = '" + strDptcd + "'";
                }
                if (strWdid != "")
                {
                    sql += System.Environment.NewLine + "   AND T04.WDID2 = '" + strWdid + "'";
                }
                if (strChkP == "0")
                {
                    sql += System.Environment.NewLine + "   ISNULL(E12.PRIVATECHK,'') <> '1'";
                }
                if (strBedipthcd != "")
                {
                    sql += System.Environment.NewLine + "   AND a04.BEDIPTHCD = '" + strBedipthcd + "'";
                }


                // TSQL문장과 Connection 객체를 지정   
                OleDbCommand cmd = new OleDbCommand(sql, conn);

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    String strPID = reader["PID"].ToString();
                    String strPRIVATECHK = reader["PRIVATECHK"].ToString();
                    String strWARDREFUSAL = reader["WARDREFUSAL"].ToString();
                    String strPNM = reader["PNM"].ToString();
                    String strRESID = reader["RESID"].ToString();
                    String strRESID_WON = reader["RESID_WON"].ToString();
                    String strBPTNT = reader["BPTNT"].ToString();
                    String strJPTNT = reader["JPTNT"].ToString();
		            String strBJSTS = reader["BJSTS"].ToString();
		            String strJUSTS = reader["JUSTS"].ToString();
                    String strSCHBEDODT = reader["SCH_BDODT"].ToString();

                    // 환자명
                    if (strPPosition == "1")
                    {
                        // 사생활보호환자 (p)표기가 이름 뒤로
                        if (strPRIVATECHK == "1") strPNM += "(P";
                        else if (strWARDREFUSAL == "1") strPNM += "(B";
                    }
                    else
                    {
                        // 사생활보호환자 (p)표기가 이름 앞으로
                        if (strPRIVATECHK == "1") strPNM = "P)" + strPNM;
                        else if (strWARDREFUSAL == "1") strPNM = "B)" + strPNM;
                    }
                    // 주민등록번호
                    if (strResIDAll == "1")
                    {
                        strRESID = strRESID.Substring(0, 6) + "-" + strRESID.Substring(6);
                    }
                    else
                    {
                        strRESID = strRESID_WON.Substring(0, 6) + "-" + strRESID_WON.Substring(6, 1);
                    }
                    // 병실상태
                    String strWARDSTATUS = "";
                    if (strPID == strBPTNT)
                    {
                        if (strBJSTS == "1") strWARDSTATUS = "입원예정";
                        else if (strBJSTS == "2") strWARDSTATUS = "입실미처리";
                    }
                    if (strPID == strJPTNT)
                    {
                        if (strJUSTS == "1") strWARDSTATUS = "입실";
                        else if (strJUSTS == "2") strWARDSTATUS = "퇴원예정";
                        else if (strJUSTS == "3") strWARDSTATUS = "병실이동";
                    }
                    // 입원예정일
                    if (strBedodtFg == "1")
                    {
                        //2019.06.04 BJW 인천힘찬 퇴원예정일 표기형식을 MM/DD 형식으로 표기
                        strSCHBEDODT = strSCHBEDODT.Substring(4, 2) + "/" + strSCHBEDODT.Substring(6, 2);
                    }

                    CData data = new CData();
                    data.Clear();
                    data.PID = strPID;
                    data.PNM = strPNM;
                    data.RESID = strRESID;
                    data.PSEX = reader["PSEX"].ToString();
                    data.PAGE = CUtil.GetAge(reader["BTHDT"].ToString(), strBDate, "year");
                    data.WARDID = reader["WARDID"].ToString();
                    data.BEDID = reader["BEDID"].ToString();
                    data.RMID = reader["RMID"].ToString();
                    data.BEDGRD = reader["BEDGRD"].ToString();
                    data.WARDSTATUS = strWARDSTATUS;
                    data.TEL = reader["TELNO2"].ToString();
                    data.BEDEDT = reader["BEDEDT"].ToString();
                    data.BEDODT = reader["BEDODT"].ToString();
                    data.ILSU = reader["ILSU"].ToString();
                    data.BEDIPTHCD = reader["BEDIPTHCD"].ToString();
                    data.DPTNM = reader["DPTNM"].ToString();
                    data.PDRNM = reader["DRNM"].ToString();
                    data.CASEWORKERNM = "";
                    data.QFYCD = reader["QLFYCD"].ToString();
                    data.QFYNM = reader["QFYNM"].ToString();
                    data.UNINM = "";
                    data.ADDR = reader["ADDR"].ToString();
                    data.HTELNO = reader["HTELNO"].ToString();
                    data.DRRMK = reader["DRRMK"].ToString();
                    data.FAMNM = reader["FAMNM"].ToString();
                    data.FTEL = reader["FTEL"].ToString();
                    data.FADDR = reader["FADDR"].ToString();
                    data.DISENM = "";
                    data.JINRMK = reader["JINRMK"].ToString();
                    data.OPDT = reader["OPDT"].ToString();
                    data.RSVOP = reader["RSVOP"].ToString();
                    data.OTELNO = reader["OTELNO"].ToString();
                    data.ILLST = "";
                    data.PSTS = reader["PSTS"].ToString();
                    data.BEDINTENTDT = reader["BEDINTENTDT"].ToString();
                    data.BEDINTENTDT2 = reader["BEDINTENTDT2"].ToString();
                    data.ADLRT = reader["ADLRT"].ToString();
                    data.DRRMK2 = reader["DRRMK2"].ToString();
                    data.WONRMK = reader["WONRMK"].ToString();
                    data.SCHBEDODT = strSCHBEDODT;
                    data.QFYSBNM = reader["QFYNM_SUB"].ToString();
                    data.SIMRMK = reader["SIMRMK"].ToString();

                    list.Add(data);
                }
                reader.Close();

                // 추가조회
                foreach (CData data in list)
                {
                    data.CASEWORKERNM = GetCaseWorkerNm(data.PID, data.BEDEDT, conn);
                    if (strCaseWrknm != "")
                    {
                        if (data.CASEWORKERNM != strCaseWrknm)
                        {
                            continue;
                        }
                    }
                    data.UNINM = GetUninm(data.PID, data.QFYCD, conn);
                    data.DISENM = GetDisenm(data.PID, data.BEDEDT, strPtyFg, conn);
                    if (strTV95fg == "1") data.ILLST = GetIllst(data.PID, data.BEDEDT, conn);

                    list2.Add(data);
                }

                conn.Close();
            }
            list = null;
            grdMain.DataSource = null;
            grdMain.DataSource = list2;
            this.RefreshGridMain();
        }

        private String GetCaseWorkerNm(String pid, String bededt, OleDbConnection conn)
        {
            String strRet = "";
            string sql = "";
			sql += System.Environment.NewLine + "SELECT TOP 1 CASENM ";
            sql += System.Environment.NewLine + "  FROM TE91 (nolock) ";
            sql += System.Environment.NewLine + " WHERE PID = '" + pid + "' ";
            sql += System.Environment.NewLine + "   AND BEDEDT = '" + bededt + "' ";
            sql += System.Environment.NewLine + "   AND BDIV = '2' ";
            sql += System.Environment.NewLine + "   AND ISNULL(CHGDT,'') = '' ";
            sql += System.Environment.NewLine + " ORDER BY EXDT DESC, SEQ DESC ";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) strRet = reader["CASENM"].ToString();
            reader.Close();
            return strRet;
        }

        private String GetUninm(String pid, String qfycd, OleDbConnection conn)
        {
            String strRet = "";
            string sql = "";
            sql += System.Environment.NewLine + "SELECT a52.UNINM";
            sql += System.Environment.NewLine + "  FROM TA56 a56 (nolock) LEFT JOIN TA52 a52 (nolock) ON a52.UNICD = a56.UNICD ";
            sql += System.Environment.NewLine + " WHERE a56.PID = '" + pid + "' ";
            sql += System.Environment.NewLine + "   AND a56.QLFYCD = '" + qfycd + "' ";
            sql += System.Environment.NewLine + "   AND a56.CREDT = (SELECT MAX(CREDT)";
            sql += System.Environment.NewLine + "                      FROM TA56 X (nolock) ";
            sql += System.Environment.NewLine + "                     WHERE X.PID = '" + pid + "' ";
            sql += System.Environment.NewLine + "                       AND X.QLFYCD = '" + qfycd + "'";
            sql += System.Environment.NewLine + "                   ) ";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) strRet = reader["UNINM"].ToString();
            reader.Close();
            return strRet;
        }

        private String GetDisenm(String pid, String bededt, String ptyfg, OleDbConnection conn)
        {
            String strRet = "";
            string sql = "";
            sql += System.Environment.NewLine + "SELECT a16.DISEKORNM";
            sql += System.Environment.NewLine + "  FROM TA04 a04 (nolock), TT05 t05 (nolock), TA16 a16 (nolock) ";
            sql += System.Environment.NewLine + " WHERE a04.PID = '" + pid + "' ";
            sql += System.Environment.NewLine + "   AND a04.BEDEDT = '" + bededt + "' ";
            sql += System.Environment.NewLine + "   AND t05.PID = a04.PID ";
            sql += System.Environment.NewLine + "   AND t05.BDEDT = a04.BEDEDT ";
            sql += System.Environment.NewLine + "   AND t05.DPTCD = a04.DPTCD ";
            sql += System.Environment.NewLine + "   AND a16.DISECD = t05.DACD ";
            sql += System.Environment.NewLine + "   AND a16.DISEDIV = '2' ";
            if (ptyfg == "1")
            {
                sql += System.Environment.NewLine + "   AND t05.PTYSQ = '1' ";
            }
            else
            {
                sql += System.Environment.NewLine + "   AND t05.SEQ = (SELECT MIN(SEQ)";
                sql += System.Environment.NewLine + "                    FROM TT05 X (nolock) ";
                sql += System.Environment.NewLine + "                   WHERE X.PID = a04.PID ";
                sql += System.Environment.NewLine + "                     AND X.BDEDT = a04.BEDEDT ";
                sql += System.Environment.NewLine + "                     AND X.DPTCD = a04.DPTCD";
                sql += System.Environment.NewLine + "                 ) ";
            }

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) strRet = reader["DISEKORNM"].ToString();
            reader.Close();
            return strRet;
        }

        private String GetIllst(String pid, String bededt, OleDbConnection conn)
        {
            String strRet = "";
            string sql = "";
            sql += System.Environment.NewLine + "SELECT ILL_ST ";
            sql += System.Environment.NewLine + "  FROM TV95 (nolock) ";
            sql += System.Environment.NewLine + " WHERE PID = '" + pid + "' ";
            sql += System.Environment.NewLine + "   AND BEDEDT = '" + bededt + "' ";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) strRet = reader["ILL_ST"].ToString();
            reader.Close();
            return strRet;
        }

        private String GetTA972(String prjcd, String frmnm, String seq, OleDbConnection conn)
        {
            String strRet = "";
            string sql = "SELECT FLD2QTY FROM TA972 (NOLOCK) WHERE PRJCD = '" + prjcd + "' AND FRMNM = '" + frmnm + "' AND SEQ = " + seq + " ";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) strRet = reader["FLD2QTY"].ToString();
            reader.Close();
            return strRet;
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
            String strPnmFg = "";
            string strConn = DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                // 부산힘찬병원 환자이름에 * 표
                strPnmFg = GetTA972("ADB", "ADB0206Q", "1", conn);

                conn.Close();

            }

            if (strPnmFg == "1")
            {
                List<CData> list = (List<CData>)grdMain.DataSource;
                foreach (CData data in list)
                {
                    data.PnmFg = "1";
                }
            }

            DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem();
            DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink = new DevExpress.XtraPrinting.PrintableComponentLink();
            printableComponentLink.CreateMarginalHeaderArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(printableComponentLink_CreateReportHeaderArea);
            printableComponentLink.CreateMarginalFooterArea += new DevExpress.XtraPrinting.CreateAreaEventHandler(printableComponentLink_CreateReportFooterArea);
            printableComponentLink.Margins = new System.Drawing.Printing.Margins(10, 10, 100, 50);
            printableComponentLink.PaperKind = System.Drawing.Printing.PaperKind.A4;
            printableComponentLink.Landscape = true;
            printingSystem.Links.Add(printableComponentLink);
            printableComponentLink.Component = grdMain;
            grdMainView.OptionsPrint.AutoWidth = false; // 이 값이 true이면 그리드가 한 페이지에 출력되도록 폭이 줄어든다. 기본값이 true임.
            printableComponentLink.ShowPreview();

            if (strPnmFg == "1")
            {
                List<CData> list = (List<CData>)grdMain.DataSource;
                foreach (CData data in list)
                {
                    data.PnmFg = "";
                }
            }
        }

        private void printableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            String strDate = txtDate.Text.ToString();
            if (strDate == "") strDate = m_SysDate;

            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 제목
            e.Graph.Font = new Font("굴림", 16F, System.Drawing.FontStyle.Bold);
            e.Graph.DrawString("재원환자리스트", Color.Black, new RectangleF(0, 10, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
            // 조회조건
            e.Graph.Font = new Font("굴림", 10F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("기준일:" + strDate, Color.Black, new RectangleF(0, 40, 1080, 40), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void printableComponentLink_CreateReportFooterArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeAlignment(StringAlignment.Center);
            e.Graph.StringFormat = e.Graph.StringFormat.ChangeLineAlignment(StringAlignment.Center);
            // 프로그램 ID
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString("ADB0206Q", Color.Black, new RectangleF(0, 0, 90, 20), DevExpress.XtraPrinting.BorderSide.None);
            // 출력일시(사실은 조회일시)
            e.Graph.Font = new Font("굴림", 8F, System.Drawing.FontStyle.Regular);
            e.Graph.DrawString(m_SysDate + " " + m_SysTime.Substring(0, 4), Color.Black, new RectangleF(900, 0, 200, 20), DevExpress.XtraPrinting.BorderSide.None);
        }

        private void btnColumnSetting_Click(object sender, EventArgs e)
        {
            String colFieldName = "";
            String colFieldCaption = "";
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in grdMainView.Columns)
            {
                colFieldName += col.FieldName + ",";
                colFieldCaption += col.Caption + ",";
            }
            ADB0206Q_01 f = new ADB0206Q_01();
            f.colFieldName = colFieldName;
            f.colFieldCaption = colFieldCaption;
            f.ShowDialog(this);

            this.SetColumnVisible();
        }

        private bool GetSel(String colid)
        {
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("MetroHIS.NET").CreateSubKey("ADB").CreateSubKey("ADB0206Q.COLUMNS");
            String sel = reg.GetValue(colid, "T").ToString();
            return (sel == "T");
        }

        private void ADB0206Q_Activated(object sender, EventArgs e)
        {
            if (IsFirst == false) return;
            IsFirst = false;

            this.SetColumnVisible();
        }

        private void SetColumnVisible()
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in grdMainView.Columns)
            {
                col.Visible = GetSel(col.FieldName);
            }
        }

        private void grdMainView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            grdMainView.IndicatorWidth = 50;
            if (e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

    }
}
