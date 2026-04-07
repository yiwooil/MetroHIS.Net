using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    public class CDataASM035_003 : CData, IData, IDataRemake
    {
        public readonly string form_id = "ASM035"; // 서식코드
        public readonly string ver_id = "003"; // 서석버전
        public readonly string buss_cd_id = "04"; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
        public readonly string buss_detail_id = "ANE"; // 업무상세코드

        // --- A. 기본 정보 ---
        public string IPAT_DD;         // 입원일자(YYYYMMDD)
        public string DSCG_YN;         // 퇴원여부(1.Yes 2.No)
        public string DSCG_DD;         // 퇴원일자(YYYYMMDD, 퇴원여부=1일 때만)

        // --- B. 마취 정보 ---
        public string NCT_STA_DT { get; set; }      // 마취 시작일시(YYYYMMDDHHMM)
        public string NCT_END_DT { get; set; }      // 마취 종료일시(YYYYMMDDHHMM)
        public string NCT_FRM_CD;      // 마취형태 구분코드(1.정규 2.응급)
        public string ASM_NCT_MTH_CD;  // 마취방법 구분코드(다중선택, 예: 1/3/7/8)
        public string NCT_RS_CD;       // 마취사유 구분코드(01~99)
        public string MDFEE_CD;        // 수가코드(마취사유가 02/03/04일 때 필수)
        public string MDFEE_CD_NM;     // 수가코드명(마취사유가 02/03/04일 때 필수)

        // --- C. 마취 전 ---
        public string PTNT_ASM_YN;     // 마취 전 환자평가 시행여부(1.Yes 2.No)

        // --- D. 마취 중 ---
        public string LBT_TRET_YN;         // 의도적 저체온증 적용 여부(1.Yes 2.No)

        // 체온 평가(저체온증 No일 때)
        public string CNTR_TMPR_MASR_YN;   // 연속적 체온 측정 및 기록 여부(1.Yes 2.No)
        public string TMPR_RGN_CD;         // 체온 측정방법 구분코드(다중선택, 예: 05/06/07)
        public string TMPR_RGN_ETC_TXT;    // 체온 측정방법 기타 상세(99일 때)
        public string LWET_TMPR;           // 최저체온(℃, 소수점 첫째자리)

        // 신경근 감시
        public string NRRT_BLCK_USE_YN;     // 신경근 차단제 사용 여부(1.Yes 2.No)
        public string NRRT_MNTR_YN;         // 신경근 감시 여부(1.Yes 2.No, 차단제 Yes일 때)

        // --- E. 마취 후(회복실) ---
        public string RCRM_IPAT_YN;         // 회복실 입실 여부(1.Yes 2.No)
        public string RCRM_DSU_RS_CD;       // 회복실 미입실 사유(1~5, 입실 No일 때)
        public string EMSS_ASM_EXEC_FQ_CD;  // 오심 및 구토평가 실시횟수(0.미실시 1.1회 2.2회 이상)
        public string EMSS_ASM_RS_TXT;      // 오심 및 구토평가 미실시/1회 사유
        public string PAIN_ASM_EXEC_FQ_CD;  // 통증평가 실시횟수(0.미실시 1.1회 2.2회 이상)
        public string PAIN_ASM_RS_TXT;      // 통증평가 미실시/1회 사유


        public void Clear()
        {
            base.Clear(form_id, ver_id, buss_cd_id, buss_detail_id);
            ClearMe();
        }

        public void ClearMe()
        {
            // --- A. 기본 정보 ---
            IPAT_DD = "";         // 입원일자(YYYYMMDD)
            DSCG_YN = "";         // 퇴원여부(1.Yes 2.No)
            DSCG_DD = "";         // 퇴원일자(YYYYMMDD, 퇴원여부=1일 때만)

            // --- B. 마취 정보 ---
            NCT_STA_DT = "";      // 마취 시작일시(YYYYMMDDHHMM)
            NCT_END_DT = "";      // 마취 종료일시(YYYYMMDDHHMM)
            NCT_FRM_CD = "";      // 마취형태 구분코드(1.정규 2.응급)
            ASM_NCT_MTH_CD = "";  // 마취방법 구분코드(다중선택, 예: 1/3/7/8)
            NCT_RS_CD = "";       // 마취사유 구분코드(01~99)
            MDFEE_CD = "";        // 수가코드(마취사유가 02/03/04일 때 필수)
            MDFEE_CD_NM = "";     // 수가코드명(마취사유가 02/03/04일 때 필수)

            // --- C. 마취 전 ---
            PTNT_ASM_YN = "";     // 마취 전 환자평가 시행여부(1.Yes 2.No)

            // --- D. 마취 중 ---
            LBT_TRET_YN = "";         // 의도적 저체온증 적용 여부(1.Yes 2.No)

            CNTR_TMPR_MASR_YN = "";   // 연속적 체온 측정 및 기록 여부(1.Yes 2.No)
            TMPR_RGN_CD = "";         // 체온 측정방법 구분코드(다중선택, 예: 05/06/07)
            TMPR_RGN_ETC_TXT = "";    // 체온 측정방법 기타 상세(99일 때)
            LWET_TMPR = "";           // 최저체온(℃, 소수점 첫째자리)

            NRRT_BLCK_USE_YN = "";    // 신경근 차단제 사용 여부(1.Yes 2.No)
            NRRT_MNTR_YN = "";        // 신경근 감시 여부(1.Yes 2.No, 차단제 Yes일 때)

            // --- E. 마취 후(회복실) ---
            RCRM_IPAT_YN = "";        // 회복실 입실 여부(1.Yes 2.No)
            RCRM_DSU_RS_CD = "";      // 회복실 미입실 사유(1~5, 입실 No일 때)
            EMSS_ASM_EXEC_FQ_CD = ""; // 오심 및 구토평가 실시횟수(0.미실시 1.1회 2.2회 이상)
            EMSS_ASM_RS_TXT = "";     // 오심 및 구토평가 미실시/1회 사유
            PAIN_ASM_EXEC_FQ_CD = ""; // 통증평가 실시횟수(0.미실시 1.1회 2.2회 이상)
            PAIN_ASM_RS_TXT = "";     // 통증평가 미실시/1회 사유
        }

        public void ReadDataFromSaved(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            string sql = "";
            sql += Environment.NewLine + "SELECT * FROM TI84_ASM035";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + " AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + " AND SEQ = '" + SEQ + "'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                // --- A. 기본 정보 ---
                IPAT_DD = reader["IPAT_DD"].ToString();         // 입원일자(YYYYMMDD)
                DSCG_YN = reader["DSCG_YN"].ToString();         // 퇴원여부(1.Yes 2.No)
                DSCG_DD = reader["DSCG_DD"].ToString();         // 퇴원일자(YYYYMMDD, 퇴원여부=1일 때만)

                // --- B. 마취 정보 ---
                NCT_STA_DT = reader["NCT_STA_DT"].ToString();      // 마취 시작일시(YYYYMMDDHHMM)
                NCT_END_DT = reader["NCT_END_DT"].ToString();      // 마취 종료일시(YYYYMMDDHHMM)
                NCT_FRM_CD = reader["NCT_FRM_CD"].ToString();      // 마취형태 구분코드(1.정규 2.응급)
                ASM_NCT_MTH_CD = reader["ASM_NCT_MTH_CD"].ToString();  // 마취방법 구분코드(다중선택, 예: 1/3/7/8)
                NCT_RS_CD = reader["NCT_RS_CD"].ToString();        // 마취사유 구분코드(01~99)
                MDFEE_CD = reader["MDFEE_CD"].ToString();          // 수가코드(마취사유가 02/03/04일 때 필수)
                MDFEE_CD_NM = reader["MDFEE_CD_NM"].ToString();    // 수가코드명(마취사유가 02/03/04일 때 필수)

                // --- C. 마취 전 ---
                PTNT_ASM_YN = reader["PTNT_ASM_YN"].ToString();    // 마취 전 환자평가 시행여부(1.Yes 2.No)

                // --- D. 마취 중 ---
                LBT_TRET_YN = reader["LBT_TRET_YN"].ToString();         // 의도적 저체온증 적용 여부(1.Yes 2.No)
                CNTR_TMPR_MASR_YN = reader["CNTR_TMPR_MASR_YN"].ToString();   // 연속적 체온 측정 및 기록 여부(1.Yes 2.No)
                TMPR_RGN_CD = reader["TMPR_RGN_CD"].ToString();         // 체온 측정방법 구분코드(다중선택, 예: 05/06/07)
                TMPR_RGN_ETC_TXT = reader["TMPR_RGN_ETC_TXT"].ToString();    // 체온 측정방법 기타 상세(99일 때)
                LWET_TMPR = reader["LWET_TMPR"].ToString();           // 최저체온(℃, 소수점 첫째자리)
                NRRT_BLCK_USE_YN = reader["NRRT_BLCK_USE_YN"].ToString();    // 신경근 차단제 사용 여부(1.Yes 2.No)
                NRRT_MNTR_YN = reader["NRRT_MNTR_YN"].ToString();        // 신경근 감시 여부(1.Yes 2.No, 차단제 Yes일 때)

                // --- E. 마취 후(회복실) ---
                RCRM_IPAT_YN = reader["RCRM_IPAT_YN"].ToString();        // 회복실 입실 여부(1.Yes 2.No)
                RCRM_DSU_RS_CD = reader["RCRM_DSU_RS_CD"].ToString();    // 회복실 미입실 사유(1~5, 입실 No일 때)
                EMSS_ASM_EXEC_FQ_CD = reader["EMSS_ASM_EXEC_FQ_CD"].ToString(); // 오심 및 구토평가 실시횟수(0.미실시 1.1회 2.2회 이상)
                EMSS_ASM_RS_TXT = reader["EMSS_ASM_RS_TXT"].ToString();     // 오심 및 구토평가 미실시/1회 사유
                PAIN_ASM_EXEC_FQ_CD = reader["PAIN_ASM_EXEC_FQ_CD"].ToString(); // 통증평가 실시횟수(0.미실시 1.1회 2.2회 이상)
                PAIN_ASM_RS_TXT = reader["PAIN_ASM_RS_TXT"].ToString();     // 통증평가 미실시/1회 사유

                return MetroLib.SqlHelper.BREAK;
            });
        }

        public void ReadDataFromEMR(OleDbConnection conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            string u03_aneno = "";
            string u03_seq = "";

            // --- A. 기본 정보 ---
            IPAT_DD = BDEDT;  // 입원일자(YYYYMMDD)
            DSCG_YN = BDODT != "" ? "1" : "2";  // 퇴원여부(1.Yes 2.No)
            DSCG_DD = BDODT;  // 퇴원일자(YYYYMMDD, 퇴원여부=1일 때만)

            ASM_NCT_MTH_CD = ""; // 마취방법 구분코드(다중선택, 예: 1/3/7/8)

            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT U01.PID,U01.OPDT,U01.DPTCD,U01.OPSEQ,U01.SEQ";
            sql += System.Environment.NewLine + "     , U01.OPSDT,U01.OPSHR,U01.OPSMN,U01.OPEDT,U01.OPEHR,U01.OPEMN";
            sql += System.Environment.NewLine + "     , U03.ANSDT,U03.ANSHR,U03.ANSMN,U03.ANEDT,U03.ANEHR,U03.ANEMN,U03.ANEDR,U03.USRID,U03.ENTDT,U03.ENTMS,U03.ANENO,U03.SEQ";
            sql += System.Environment.NewLine + "     , U03.ANETP";
            sql += System.Environment.NewLine + "     , A07.DRNM";
            sql += System.Environment.NewLine + "  FROM TU01 U01 INNER JOIN TU03 U03 ON U03.PID=U01.PID AND U03.OPDT=U01.OPDT AND U03.DPTCD=U01.DPTCD AND U03.OPSEQ=U01.OPSEQ";
            sql += System.Environment.NewLine + "                LEFT JOIN TA07 A07 ON A07.DRID = U03.DRID";
            sql += System.Environment.NewLine + " WHERE U01.PID='" + PID + "'";
            sql += System.Environment.NewLine + "   AND U01.OPDT>='" + BDEDT + "'";
            sql += System.Environment.NewLine + "   AND U01.OPDT<='" + BDODT + "'";
            sql += System.Environment.NewLine + "   AND ISNULL(U01.CHGDT,'')=''";
            sql += System.Environment.NewLine + "   AND ISNULL(U03.CHGDT,'')=''";
            sql += System.Environment.NewLine + "   AND U03.ANENO NOT LIKE 'ZZZZ%'";
            sql += System.Environment.NewLine + " ORDER BY U01.OPDT,U01.OPSHR";

            MetroLib.SqlHelper.GetDataRow(sql, conn, p_tran, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                u03_aneno = row["ANENO"].ToString();
                u03_seq = row["SEQ"].ToString();

                string sql2 = "";

                // --- B. 마취 정보 ---
                string anshr = row["ANSHR"].ToString();
                string ansmn = row["ANSMN"].ToString();
                string anehr = row["ANEHR"].ToString();
                string anemn = row["ANEMN"].ToString();

                if (anshr.Length == 1) anshr = "0" + anshr; // 시간이 오전이면 한 글자로 들어간다.(짜증...)
                if (ansmn.Length == 1) ansmn = "0" + ansmn;
                if (anehr.Length == 1) anehr = "0" + anehr; // 시간이 오전이면 한 글자로 들어간다.(짜증...)
                if (anemn.Length == 1) anemn = "0" + anemn;

                NCT_STA_DT = CUtil.GetDateTime(row["ANSDT"].ToString(), anshr, ansmn); // 마취 시작일시(YYYYMMDDHHMM)
                NCT_END_DT = CUtil.GetDateTime(row["ANEDT"].ToString(), anehr, anemn); // 마취 종료일시(YYYYMMDDHHMM)

                // 마취방법 변환
                string anetp = row["ANETP"].ToString().ToUpper();
                if (anetp == "IV") ASM_NCT_MTH_CD = "1";
                else if (anetp == "IR" || anetp == "LL") ASM_NCT_MTH_CD = "2";
                else if (anetp == "IW") ASM_NCT_MTH_CD = "3";
                else if (anetp == "EC") ASM_NCT_MTH_CD = "4";
                else if (anetp == "MC") ASM_NCT_MTH_CD = "5";
                else if (anetp == "SP") ASM_NCT_MTH_CD = "6";
                else if (anetp == "CD" || anetp == "ED") ASM_NCT_MTH_CD = "7";
                else if (anetp == "SB" || anetp.StartsWith("BP")) ASM_NCT_MTH_CD = "8";
                else if (anetp == "ESA") ASM_NCT_MTH_CD = "9";


                // EMR082_2
                NCT_FRM_CD = ""; // 마취형태 구분코드(1.정규 2.응급)
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT NCT_FRM_CD, ASA, ASA6, ASA7";
                sql2 += System.Environment.NewLine + "  FROM EMR082_2";
                sql2 += System.Environment.NewLine + " WHERE PID='" + PID + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(UPDDT,'')<>''";

                MetroLib.SqlHelper.GetDataRow(sql2, conn, p_tran, delegate(DataRow row2)
                {
                    System.Windows.Forms.Application.DoEvents();

                    NCT_FRM_CD = row2["NCT_FRM_CD"].ToString(); // 마취형태 구분코드(1.정규 2.응급)

                    return MetroLib.SqlHelper.BREAK;
                });

                // EMR320
                bool find_emr320 = false;
                // 2026.03.09 WOOIL - EMR320 테이블을 사용하는 병원이 생기면 그때 사용하자.
                //string mntr_knd_cd = "";
                ////string lbt_tret_yn = "";
                ////string cntr_tmpr_masr_yn = "";
                ////string tmpr_rgn_cd = "";
                ////string tmpr_rgn_etc_txt = "";
                ////string lwet_tmpr = "";
                ////string rcrm_ipat_yn = "";
                ////string rcrm_dsu_rs_cd = "";

                //sql2 = "";
                //sql2 += System.Environment.NewLine + "SELECT MTH_CD, MTH_ETC, MIDD_MNTR_YN, MNTR_KND_CD, MNTR_ETC";
                ////sql2 += System.Environment.NewLine + "     , LBT_TRET_YN, CNTR_TMPR_MASR_YN, TMPR_RGN_CD, TMPR_RGN_ETC_TXT, LWET_TMPR, RCRM_IPAT_YN, RCRM_DSU_RS_CD"; 2026.03.03 WOOIL - 병원에 필드가 없음.
                //sql2 += System.Environment.NewLine + "  FROM EMR320";
                //sql2 += System.Environment.NewLine + " WHERE PID='" + PID + "'";
                //sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                //sql2 += System.Environment.NewLine + "   AND DPTCD='" + row["DPTCD"].ToString() + "'";
                //sql2 += System.Environment.NewLine + "   AND OPSEQ='" + row["OPSEQ"].ToString() + "'";
                //sql2 += System.Environment.NewLine + "   AND SEQ='" + row["SEQ"].ToString() + "'";
                //sql2 += System.Environment.NewLine + "   AND ISNULL(UPDDT,'')<>''";
                //
                //List<string> mth_cd_list = new List<string>();
                //MetroLib.SqlHelper.GetDataRow(sql2, conn, p_tran, delegate(DataRow row2)
                //{
                //    System.Windows.Forms.Application.DoEvents();
                //    find_emr320 = true;
                //
                //    string mth_cd = row2["MTH_CD"].ToString();
                //    string[] mth_cd_ary = mth_cd.Split((char)21);
                //    for (int i = 0; i < mth_cd_ary.Length; i++)
                //    {
                //        if (mth_cd_ary[i] == "1") mth_cd_list.Add((i + 1).ToString());
                //    }
                //    ASM_NCT_MTH_CD = string.Join("/", mth_cd_list.ToArray()); // 마취방법 구분코드(다중선택, 예: 1/3/7/8)
                //
                //    mntr_knd_cd = row2["MNTR_KND_CD"].ToString(); // 실제 사용은 아랫쪽에서..
                //    //lbt_tret_yn = row2["LBT_TRET_YN"].ToString(); // 실제 사용은 아랫쪽에서..
                //    //cntr_tmpr_masr_yn = row2["CNTR_TMPR_MASR_YN"].ToString(); // 실제 사용은 아랫쪽에서..
                //    //tmpr_rgn_cd = row2["TMPR_RGN_CD"].ToString(); // 실제 사용은 아랫쪽에서..
                //    //tmpr_rgn_etc_txt = row2["TMPR_RGN_ETC_TXT"].ToString(); // 실제 사용은 아랫쪽에서..
                //    //lwet_tmpr = row2["LWET_TMPR"].ToString(); // 실제 사용은 아랫쪽에서..
                //    //rcrm_ipat_yn = row2["RCRM_IPAT_YN"].ToString(); // 실제 사용은 아랫쪽에서..
                //    //rcrm_dsu_rs_cd = row2["RCRM_DSU_RS_CD"].ToString(); // 실제 사용은 아랫쪽에서..
                //
                //    return MetroLib.SqlHelper.BREAK;
                //});


                // 수술명(TU02)
                NCT_RS_CD = ""; // 마취사유 구분코드(01~99)
                MDFEE_CD = ""; // 수가코드(마취사유가 02/03/04일 때 필수)
                MDFEE_CD_NM = ""; // 수가코드명(마취사유가 02/03/04일 때 필수)
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT U02.OCD, U02.OPDT, A18.ONM, A02.ISPCD";
                sql2 += System.Environment.NewLine + "  FROM TU02 U02 INNER JOIN TA18 A18 ON A18.OCD=U02.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=U02.OCD AND X.CREDT<=U02.OPDT)";
                sql2 += System.Environment.NewLine + "                INNER JOIN TA02 A02 ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=U02.OPDT)";
                sql2 += System.Environment.NewLine + " WHERE U02.PID='" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND U02.OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND U02.DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND U02.OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(U02.CHGDT,'')=''";
                sql2 += System.Environment.NewLine + " ORDER BY U02.OCD,U02.SEQ";

                bool find = false;
                MetroLib.SqlHelper.GetDataRow(sql2, conn, p_tran, delegate(DataRow row2)
                {
                    System.Windows.Forms.Application.DoEvents();

                    string ispcd = row2["ISPCD"].ToString();
                    string ispcd15 = ispcd.Substring(0, 5);

                    // 일단 이렇게...
                    NCT_RS_CD = "";       // 마취사유 구분코드(01~99)
                    if (CUtil_ASM035.IsLA210(ispcd15)) NCT_RS_CD = "04"; // 통증조절
                    else if (CUtil_ASM035.IsR3131(ispcd15)) NCT_RS_CD = "03"; // 무통분만
                    else NCT_RS_CD = "01"; // 처치 및 수술

                    // 2026.03.10 WOOIL - 김포가자연세병원에서 NCT_RS_CD가 "01"인 경우 
                    //                    MDFEE_CD와 MDFEE_CD_NM에 값이 있으니 전송할 때 오류가 발생하여 값을 지우고 전송하니 성공함.
                    if (NCT_RS_CD == "02" || NCT_RS_CD == "03" || NCT_RS_CD == "04")
                    {
                        MDFEE_CD = ispcd;  // 수가코드(마취사유가 02/03/04일 때 필수)
                        MDFEE_CD_NM = row2["ONM"].ToString(); // 수가코드명(마취사유가 02/03/04일 때 필수)
                    }

                    find = true;

                    return MetroLib.SqlHelper.BREAK;
                });

                // 2026.03.05 WOOIL - 수술비코드를 수술재료에 같이 등록하는 병원이 있음.(서울바른척도)
                if (find == false)
                {
                    sql2 = "";
                    sql2 += System.Environment.NewLine + "SELECT U05.*, A02.ISPCD, A02.ACTFG, A18.ONM";
                    sql2 += System.Environment.NewLine + "  FROM TU05 U05 (NOLOCK) INNER JOIN TA18 A18 (NOLOCK) ON A18.OCD=U05.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X (NOLOCK) WHERE X.OCD=U05.OCD AND X.CREDT<=U05.OPDT)";
                    sql2 += System.Environment.NewLine + "                         INNER JOIN TA02 A02 (NOLOCK) ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X (NOLOCK) WHERE X.PRICD=A18.PRICD AND X.CREDT<=U05.OPDT)";
                    sql2 += System.Environment.NewLine + " WHERE U05.PID='" + PID + "'";
                    sql2 += System.Environment.NewLine + "   AND U05.BEDEDT='" + BDEDT + "'";
                    sql2 += System.Environment.NewLine + "   AND ISNULL(U05.CHGDT,'')=''";
                    sql2 += System.Environment.NewLine + "   AND ISNULL(A02.GUBUN,'')='1'"; // 수가만(재료 제외)
                    sql2 += System.Environment.NewLine + " ORDER BY U05.OPDT,U05.DPTCD,U05.OPSEQ,U05.OCD,U05.SEQ";

                    MetroLib.SqlHelper.GetDataRow(sql2, conn, delegate(DataRow row2)
                    {
                        System.Windows.Forms.Application.DoEvents();

                        string ispcd = row2["ISPCD"].ToString();
                        string ispcd15 = ispcd.Length >= 5 ? ispcd.Substring(0, 5) : ispcd;
                        string actfg = row2["ACTFG"].ToString();

                        // 일단 이렇게...
                        NCT_RS_CD = "";       // 마취사유 구분코드(01~99)
                        if (CUtil_ASM035.IsLA210(ispcd15)) NCT_RS_CD = "04"; // 통증조절
                        else if (CUtil_ASM035.IsR3131(ispcd15)) NCT_RS_CD = "03"; // 무통분만
                        else if (CUtil_ASM035.IsACTFG_8(actfg)) NCT_RS_CD = "01"; // 처치 및 수술

                        // 2026.03.10 WOOIL - 김포가자연세병원에서 NCT_RS_CD가 "01"인 경우 
                        //                    MDFEE_CD와 MDFEE_CD_NM에 값이 있으니 전송할 때 오류가 발생하여 값을 지우고 전송하니 성공함.
                        if (NCT_RS_CD == "02" || NCT_RS_CD == "03" || NCT_RS_CD == "04")
                        {
                            MDFEE_CD = ispcd;  // 수가코드(마취사유가 02/03/04일 때 필수)
                            MDFEE_CD_NM = row2["ONM"].ToString(); // 수가코드명(마취사유가 02/03/04일 때 필수)

                            return MetroLib.SqlHelper.BREAK;
                        }

                        return MetroLib.SqlHelper.CONTINUE;
                    });
                }


                // --- C. 마취 전 ---
                PTNT_ASM_YN = "2";     // 마취 전 환자평가 시행여부(1.Yes 2.No)
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT *";
                sql2 += System.Environment.NewLine + "  FROM EMR082_2";
                sql2 += System.Environment.NewLine + " WHERE PID='" + PID + "'";
                sql2 += System.Environment.NewLine + "   AND BEDEDT='" + BDEDT + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(UPDDT,'')=''";

                MetroLib.SqlHelper.GetDataRow(sql2, conn, p_tran, delegate(DataRow row2)
                {
                    System.Windows.Forms.Application.DoEvents();

                    PTNT_ASM_YN = "1";

                    return MetroLib.SqlHelper.BREAK;
                });

                // --- D. 마취 중 ---
                LBT_TRET_YN = "2";         // 의도적 저체온증 적용 여부(1.Yes 2.No) ===> 기본값 No
                //if (lbt_tret_yn != "")
                //{
                //    string[] lbt_tret_yn_ary = lbt_tret_yn.Split((char)21);
                //    if (lbt_tret_yn_ary[0] == "1") LBT_TRET_YN = "1";
                //    if (lbt_tret_yn_ary[1] == "1") LBT_TRET_YN = "2";
                //}
                //
                //CNTR_TMPR_MASR_YN = "";   // 연속적 체온 측정 및 기록 여부(1.Yes 2.No)
                //if (cntr_tmpr_masr_yn != "")
                //{
                //    string[] cntr_tmpr_masr_yn_ary = cntr_tmpr_masr_yn.Split((char)21);
                //    if (cntr_tmpr_masr_yn_ary[0] == "1") CNTR_TMPR_MASR_YN = "1";
                //    if (cntr_tmpr_masr_yn_ary[1] == "1") CNTR_TMPR_MASR_YN = "2";
                //}
                //
                //TMPR_RGN_CD = "";         // 체온 측정방법 구분코드(다중선택, 예: 05/06/07)
                //if (tmpr_rgn_cd != "")
                //{
                //    string[] tmpr_rgn_cd_ary = tmpr_rgn_cd.Split((char)21);
                //    List<string> tmpr_rgn_cd_list = new List<string>();
                //    for (int i = 0; i < tmpr_rgn_cd_ary.Length; i++)
                //    {
                //        if (tmpr_rgn_cd_ary[i] == "1")
                //        {
                //            if (i == 8)
                //            {
                //                tmpr_rgn_cd_list.Add("99"); // 기타
                //            }
                //            else
                //            {
                //                tmpr_rgn_cd_list.Add((i + 1).ToString("D2")); // 숫자를 2자리 문자로 만든다.(ex. 1 -> "01")
                //            }
                //        }
                //    }
                //    TMPR_RGN_CD = string.Join("/", tmpr_rgn_cd_list.ToArray());
                //}
                //TMPR_RGN_ETC_TXT = tmpr_rgn_etc_txt;    // 체온 측정방법 기타 상세(99일 때)
                //LWET_TMPR = lwet_tmpr;           // 최저체온(℃, 소수점 첫째자리)

                // 연속적 체온측정기록여부와 최저체온을 찾는다.
                if (find_emr320 == false)
                {
                    sql2 = "";
                    sql2 += System.Environment.NewLine + "SELECT U91A.DATA, U91.MONITOR, U91.MONITOR_TEMP ";
                    sql2 += System.Environment.NewLine + "  FROM TU91 U91 LEFT JOIN TU91A U91A ON U91A.ANENO=U91.ANENO AND U91A.OCD='C004'";
                    sql2 += System.Environment.NewLine + " WHERE U91.PID='" + PID + "'";
                    sql2 += System.Environment.NewLine + "   AND U91.ANENO='" + u03_aneno + "'";

                    MetroLib.SqlHelper.GetDataRow(sql2, conn, p_tran, delegate(DataRow row2)
                    {
                        System.Windows.Forms.Application.DoEvents();

                        string u91a_data = row2["DATA"].ToString();
                        string u91_monitor = row2["MONITOR"].ToString();
                        string u91_monitor_temp = row2["MONITOR_TEMP"].ToString();

                        string[] arr_u91a_data = u91a_data.Split('\t');
                        string[] arr_u91_monitor = u91_monitor.Split(';');
                        
                        double temp1 = MetroLib.StrHelper.ToDouble(arr_u91a_data[arr_u91a_data.Length - 1]);
                        double temp2 = MetroLib.StrHelper.ToDouble(arr_u91a_data[arr_u91a_data.Length - 4]);

                        CNTR_TMPR_MASR_YN = "2"; // 연속적 체온 측정 및 기록 여부(1.Yes 2.No)
                        if (temp1 != 0 && temp2 != 0)
                        {
                            CNTR_TMPR_MASR_YN = "1"; // 연속적 체온 측정 및 기록 여부(1.Yes 2.No)
                            LWET_TMPR = (temp1 < temp2 ? temp1 : temp2).ToString(); // 최저체온(℃, 소수점 첫째자리)
                        }

                        if (arr_u91_monitor[8] == "1")
                        {
                            TMPR_RGN_CD = "99"; // 일단 기타
                            if (u91_monitor_temp == "0") TMPR_RGN_ETC_TXT = "ESOPH";
                            else if (u91_monitor_temp == "1") TMPR_RGN_ETC_TXT = "RECTAL";
                            else if (u91_monitor_temp == "2") TMPR_RGN_ETC_TXT = "SKIN";
                            else if (u91_monitor_temp == "3") TMPR_RGN_ETC_TXT = "ETC";
                        }

                        return MetroLib.SqlHelper.BREAK;
                    });
                }

                // 2026.03.10 WOOIL - 마취방법이 "4.기관내 삽관에 의한 폐쇄순환식 전신마취"나 "5.마스크에 의한 폐쇄순환식 전신마취"가 아닌경우 기본값 처리
                //                    심평원에서 사용하지는 않지만 전송할 때 오류가 발생하므로 기본값을 넣는다.
                //                    창원제일에서 "고막"을 기본값으로 해달라고 하였음.
                if (ASM_NCT_MTH_CD != "4" && ASM_NCT_MTH_CD != "5")
                {
                    string cntr_tmpr_masr_yn = "";
                    string tmpr_rgn_cd = "";
                    string tmpr_rgn_etc_txt = "";
                    string lwet_tmpr = "";

                    sql2 = "";
                    sql2 += Environment.NewLine + "SELECT *";
                    sql2 += Environment.NewLine + "  FROM TI88B";
                    sql2 += Environment.NewLine + " WHERE MST1CD='A'";
                    sql2 += Environment.NewLine + "   AND MST2CD='EFormASM'";
                    sql2 += Environment.NewLine + "   AND MST3CD='ASM035'";
                    sql2 += Environment.NewLine + "   AND MST4CD IN ('CNTR_TMPR_MASR_YN','TMPR_RGN_CD','TMPR_RGN_ETC_TXT','LWET_TMPR')";
                    MetroLib.SqlHelper.GetDataRow(sql2, conn, delegate(DataRow row2)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        string mst4cd = row2["MST4CD"].ToString().ToUpper();

                        if (mst4cd == "CNTR_TMPR_MASR_YN") cntr_tmpr_masr_yn = row2["FLD1QTY"].ToString();
                        else if (mst4cd == "TMPR_RGN_CD") tmpr_rgn_cd = row2["FLD1QTY"].ToString();
                        else if (mst4cd == "TMPR_RGN_ETC_TXT") tmpr_rgn_etc_txt = row2["FLD1QTY"].ToString();
                        else if (mst4cd == "LWET_TMPR") lwet_tmpr = row2["FLD1QTY"].ToString();

                        return MetroLib.SqlHelper.CONTINUE;
                    });

                    if (CNTR_TMPR_MASR_YN == "") CNTR_TMPR_MASR_YN = cntr_tmpr_masr_yn; // 연속적 체온측정여부
                    if (TMPR_RGN_CD == "")
                    {
                        TMPR_RGN_CD = tmpr_rgn_cd; // 측정방법(06.고막)
                        TMPR_RGN_ETC_TXT = tmpr_rgn_etc_txt; // 기타상세 ... 
                    }
                    if (LWET_TMPR == "") LWET_TMPR = lwet_tmpr; // 체온(36.5)
                }

                find = false;
                NRRT_BLCK_USE_YN = "2";    // 신경근 차단제 사용 여부(1.Yes 2.No) ===> 기본값 No

                // EMR320_MDCT
                // 2026.03.09 WOOIL - EMR320 테이블을 사용하는 병원이 생기면 그때 사용하자.
                //sql2 = "";
                //sql2 += System.Environment.NewLine + "SELECT KND_CD, MDCT_STDT, MDCT_STTM, MDCT_EDDT, MDCT_EDTM, MDS_NM, OQTY, UNIT";
                //sql2 += System.Environment.NewLine + "  FROM EMR320_MDCT";
                //sql2 += System.Environment.NewLine + " WHERE PID='" + PID + "'";
                //sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                //sql2 += System.Environment.NewLine + "   AND DPTCD='" + row["DPTCD"].ToString() + "'";
                //sql2 += System.Environment.NewLine + "   AND OPSEQ='" + row["OPSEQ"].ToString() + "'";
                //sql2 += System.Environment.NewLine + "   AND SEQ='" + row["SEQ"].ToString() + "'";
                //sql2 += System.Environment.NewLine + " ORDER BY EMRSEQ";
                //
                //MetroLib.SqlHelper.GetDataRow(sql2, conn, p_tran, delegate(DataRow row2)
                //{
                //    System.Windows.Forms.Application.DoEvents();
                //    find = true;
                //
                //    string knd_cd = row2["KND_CD"].ToString();
                //    if (knd_cd == "3")
                //    {
                //        NRRT_BLCK_USE_YN = "1"; // 신경근 차단제 사용 여부(1.Yes 2.No)
                //        return MetroLib.SqlHelper.BREAK;
                //    }
                //
                //    return MetroLib.SqlHelper.CONTINUE;
                //});

                // 2026.03.06 WOOIL - 마취재료에 M3122080 코드가 있으면 신경근 차단제를 사용한 것임.
                //                    EMR320을 사용하지 않는 병원이 경우 ...
                if (find == false)
                {
                    string nrrt_blck_code = "";

                    // 신경근 차단제 코드를 읽는다.
                    // 창원제일종합병원은 M3122080 로큐메론임.
                    sql2 = "";
                    sql2 += Environment.NewLine + "SELECT * FROM TI88B WHERE MST1CD='A' AND MST2CD='EFormASM' AND MST3CD='ASM035' AND MST4CD='NRR_BLCK_CODE'";
                    MetroLib.SqlHelper.GetDataRow(sql2, conn, delegate(DataRow row2)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        nrrt_blck_code = row2["FLD1QTY"].ToString();
                        return MetroLib.SqlHelper.BREAK;
                    });

                    // 신경근 차단제 코드가 있으면 ...
                    if (nrrt_blck_code != "")
                    {
                        sql2 = "";
                        sql2 += System.Environment.NewLine + "SELECT U04.*";
                        sql2 += System.Environment.NewLine + "  FROM TU04 U04 (NOLOCK)";
                        sql2 += System.Environment.NewLine + " WHERE U04.PID='" + PID + "'";
                        sql2 += System.Environment.NewLine + "   AND U04.ANENO='" + u03_aneno + "'";
                        sql2 += System.Environment.NewLine + "   AND U04.OCD='" + nrrt_blck_code + "'";
                        sql2 += System.Environment.NewLine + " ORDER BY U04.OCD,U04.SEQ";

                        MetroLib.SqlHelper.GetDataRow(sql2, conn, delegate(DataRow row2)
                        {
                            System.Windows.Forms.Application.DoEvents();

                            NRRT_BLCK_USE_YN = "1"; // 신경근 차단제 사용 여부(1.Yes 2.No)
                            return MetroLib.SqlHelper.BREAK;
                        });
                    }
                }

                NRRT_MNTR_YN = "2"; // 신경근 감시 여부(1.Yes 2.No, 차단제 Yes일 때) ===> 기본값 No
                //if (mntr_knd_cd != "")
                //{
                //    string[] mntr_knd_cd_ary = mntr_knd_cd.Split((char)21);
                //    if (mntr_knd_cd_ary.Length >= 5 && mntr_knd_cd_ary[6] == "1")
                //    {
                //        NRRT_MNTR_YN = "1";
                //    }
                //}

                // --- E. 마취 후(회복실) ---
                RCRM_IPAT_YN = "";       // 회복실 입실 여부(1.Yes 2.No)
                //if (rcrm_ipat_yn != "")
                //{
                //    string[] rcrm_ipat_yn_ary = rcrm_ipat_yn.Split((char)21);
                //    if (rcrm_ipat_yn_ary[0] == "1") RCRM_IPAT_YN = "1";
                //    if (rcrm_ipat_yn_ary[0] == "2") RCRM_IPAT_YN = "2";
                //}

                RCRM_DSU_RS_CD = "";      // 회복실 미입실 사유(1~5, 입실 No일 때)
                //if (rcrm_dsu_rs_cd != "")
                //{
                //    string[] rcrm_dsu_rs_cd_ary = rcrm_dsu_rs_cd.Split((char)21);
                //    for (int i = 0; i < rcrm_dsu_rs_cd_ary.Length; i++)
                //    {
                //        if (rcrm_dsu_rs_cd_ary[i] == "1") RCRM_DSU_RS_CD = (i + 1).ToString();
                //    }
                //}
                EMSS_ASM_EXEC_FQ_CD = ""; // 오심 및 구토평가 실시횟수(0.미실시 1.1회 2.2회 이상)
                EMSS_ASM_RS_TXT = "";     // 오심 및 구토평가 미실시/1회 사유
                PAIN_ASM_EXEC_FQ_CD = ""; // 통증평가 실시횟수(0.미실시 1.1회 2.2회 이상)
                PAIN_ASM_RS_TXT = "";     // 통증평가 미실시/1회 사유

                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT U93.PT_INDT, U93.PT_INTM, U93.PT_OUTDT, U93.PT_OUTTM";
                sql2 += System.Environment.NewLine + "     , U93.ANDRID, A07.DRNM AS ANDRNM";
                sql2 += System.Environment.NewLine + "     , U93.EMPID, U93.WDATE, U93.WTIME";
                sql2 += System.Environment.NewLine + "     , U93.VOM_NRS_1, U93.VOM_NRS_2, U93.PAINCASE";
                sql2 += System.Environment.NewLine + "     , U93.PCA_1, U93.PCA_2, U93.PCA_3, U93.PCA_TXT";
                sql2 += System.Environment.NewLine + "     , U93.PARSCR1_1, U93.PARSCR1_2, U93.PARSCR1_3, U93.PARSCR1_4, U93.PARSCR1_5";
                sql2 += System.Environment.NewLine + "     , U93.PARSCR2_1, U93.PARSCR2_2, U93.PARSCR2_3, U93.PARSCR2_4, U93.PARSCR2_5";
                sql2 += System.Environment.NewLine + "     , U93.PAINDT1, U93.PAINDT2, U93.EMSSDT1, U93.EMSSDT2, U93.ASM_RST_TXT1, U93.ASM_RST_TXT2";
                sql2 += System.Environment.NewLine + "     , U93.OPDT, U93.DPTCD, U93.OPSEQ, U93.SEQ";
                sql2 += System.Environment.NewLine + "     , U93.EMSS_ASM_RS_TXT, U93.PAIN_ASM_RS_TXT";
                sql2 += System.Environment.NewLine + "  FROM TU93 U93 LEFT JOIN TA07 A07 ON A07.DRID = U93.ANDRID";
                sql2 += System.Environment.NewLine + " WHERE U93.PID='" + PID + "'";
                sql2 += System.Environment.NewLine + "   AND U93.OPDT='" + row["OPDT"].ToString() + "'";

                MetroLib.SqlHelper.GetDataRow(sql2, conn, p_tran, delegate(DataRow row2)
                {
                    System.Windows.Forms.Application.DoEvents();

                    RCRM_IPAT_YN = "1";       // 회복실 입실 여부(1.Yes 2.No)
                    /*
                    string emssdt1 = row2["EMSSDT1"].ToString(); // 오심구토 평가일시1
                    string emssdt2 = row2["EMSSDT2"].ToString(); // 오심구토 평가일시2
                    int emss_asm_exec_fq_cd = 0;
                    if (emssdt1 != "") emss_asm_exec_fq_cd++;
                    if (emssdt2 != "") emss_asm_exec_fq_cd++;

                    string paindt1 = row2["PAINDT1"].ToString(); // 통증 평가일시1
                    string paindt2 = row2["PAINDT2"].ToString(); // 통증 평가일시2
                    int pain_asm_exec_fq_cd = 0;
                    if (paindt1 != "") pain_asm_exec_fq_cd++;
                    if (paindt2 != "") pain_asm_exec_fq_cd++;
                    */
                    string vom_nrs_1 = row2["VOM_NRS_1"].ToString(); // 1회 오심,구토   유체크값(0 OR 1) + CHAR(25) + 무체크값(0 OR 1) + CHAR(25) + 통증평가도구 점수
                    string vom_nrs_2 = row2["VOM_NRS_2"].ToString(); // 2회 오심,구토   유체크값(0 OR 1) + CHAR(25) + 무체크값(0 OR 1) + CHAR(25) + 통증평가도구 점수

                    string[] ary_vom_nrs_1 = (vom_nrs_1 + (char)25 + (char)25 + (char)25).Split((char)25);
                    string[] ary_vom_nrs_2 = (vom_nrs_2 + (char)25 + (char)25 + (char)25).Split((char)25);

                    int emss_asm_exec_fq_cd = 0;
                    if (ary_vom_nrs_1[0] == "1" || ary_vom_nrs_1[1] == "1") emss_asm_exec_fq_cd++;
                    if (ary_vom_nrs_2[0] == "1" || ary_vom_nrs_2[1] == "1") emss_asm_exec_fq_cd++;

                    int pain_asm_exec_fq_cd = 0;
                    if (ary_vom_nrs_1[2] != "") pain_asm_exec_fq_cd++;
                    if (ary_vom_nrs_2[2] != "") pain_asm_exec_fq_cd++;

                    EMSS_ASM_EXEC_FQ_CD = emss_asm_exec_fq_cd.ToString(); // 오심 및 구토평가 실시횟수(0.미실시 1.1회 2.2회 이상)
                    PAIN_ASM_EXEC_FQ_CD = pain_asm_exec_fq_cd.ToString(); // 통증평가 실시횟수(0.미실시 1.1회 2.2회 이상)

                    EMSS_ASM_RS_TXT = row2["EMSS_ASM_RS_TXT"].ToString(); // 오심 및 구토평가 미실시/1회 사유
                    PAIN_ASM_RS_TXT = row2["PAIN_ASM_RS_TXT"].ToString(); // 통증평가 미실시/1회 사유

                    return MetroLib.SqlHelper.BREAK;
                });


                return MetroLib.SqlHelper.BREAK;
            });
        }

        public void InsData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran, bool del_fg)
        {
            string sql = "";

            if (del_fg == true)
            {
                sql = "";
                sql += "DELETE FROM TI84_ASM035 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            }

            List<object> para = new List<object>();

            // --- A. 기본 정보 및 B~E 단일값 메인 테이블 저장 ---
            sql = "";
            sql += Environment.NewLine + "INSERT INTO TI84_ASM035 (";
            sql += Environment.NewLine + "FORM, KEYSTR, SEQ, VER, IPAT_DD, DSCG_YN, DSCG_DD, ";
            sql += Environment.NewLine + "NCT_STA_DT, NCT_END_DT, NCT_FRM_CD, ASM_NCT_MTH_CD, NCT_RS_CD, MDFEE_CD, MDFEE_CD_NM, ";
            sql += Environment.NewLine + "PTNT_ASM_YN, LBT_TRET_YN, CNTR_TMPR_MASR_YN, TMPR_RGN_CD, TMPR_RGN_ETC_TXT, LWET_TMPR, ";
            sql += Environment.NewLine + "NRRT_BLCK_USE_YN, NRRT_MNTR_YN, RCRM_IPAT_YN, RCRM_DSU_RS_CD, EMSS_ASM_EXEC_FQ_CD, EMSS_ASM_RS_TXT, ";
            sql += Environment.NewLine + "PAIN_ASM_EXEC_FQ_CD, PAIN_ASM_RS_TXT";
            sql += Environment.NewLine + ") VALUES (";
            sql += Environment.NewLine + "?, ?, ?, ?, ?, ?, ?, ";
            sql += Environment.NewLine + "?, ?, ?, ?, ?, ?, ?, ";
            sql += Environment.NewLine + "?, ?, ?, ?, ?, ?, ";
            sql += Environment.NewLine + "?, ?, ?, ?, ?, ?, ";
            sql += Environment.NewLine + "?, ?)";

            para.Clear();
            para.Add(form);               // FORM
            para.Add(KEYSTR);             // KEYSTR
            para.Add(SEQ);                // SEQ
            para.Add(ver);                // VER
            para.Add(IPAT_DD);            // 입원일자(YYYYMMDD)
            para.Add(DSCG_YN);            // 퇴원여부(1.Yes 2.No)
            para.Add(DSCG_DD);            // 퇴원일자(YYYYMMDD)
            para.Add(NCT_STA_DT);         // 마취 시작일시(YYYYMMDDHHMM)
            para.Add(NCT_END_DT);         // 마취 종료일시(YYYYMMDDHHMM)
            para.Add(NCT_FRM_CD);         // 마취형태 구분코드(1.정규 2.응급)
            para.Add(ASM_NCT_MTH_CD);     // 마취방법 구분코드(다중선택, 예: 1/3/7/8)
            para.Add(NCT_RS_CD);          // 마취사유 구분코드(01~99)
            para.Add(MDFEE_CD);           // 수가코드(마취사유가 02/03/04일 때 필수)
            para.Add(MDFEE_CD_NM);        // 수가코드명(마취사유가 02/03/04일 때 필수)
            para.Add(PTNT_ASM_YN);        // 마취 전 환자평가 시행여부(1.Yes 2.No)
            para.Add(LBT_TRET_YN);        // 의도적 저체온증 적용 여부(1.Yes 2.No)
            para.Add(CNTR_TMPR_MASR_YN);  // 연속적 체온 측정 및 기록 여부(1.Yes 2.No)
            para.Add(TMPR_RGN_CD);        // 체온 측정방법 구분코드(다중선택, 예: 05/06/07)
            para.Add(TMPR_RGN_ETC_TXT);   // 체온 측정방법 기타 상세(99일 때)
            para.Add(LWET_TMPR);          // 최저체온(℃, 소수점 첫째자리)
            para.Add(NRRT_BLCK_USE_YN);   // 신경근 차단제 사용 여부(1.Yes 2.No)
            para.Add(NRRT_MNTR_YN);       // 신경근 감시 여부(1.Yes 2.No, 차단제 Yes일 때)
            para.Add(RCRM_IPAT_YN);       // 회복실 입실 여부(1.Yes 2.No)
            para.Add(RCRM_DSU_RS_CD);     // 회복실 미입실 사유(1~5, 입실 No일 때)
            para.Add(EMSS_ASM_EXEC_FQ_CD);// 오심 및 구토평가 실시횟수(0.미실시 1.1회 2.2회 이상)
            para.Add(EMSS_ASM_RS_TXT);    // 오심 및 구토평가 미실시/1회 사유
            para.Add(PAIN_ASM_EXEC_FQ_CD);// 통증평가 실시횟수(0.미실시 1.1회 2.2회 이상)
            para.Add(PAIN_ASM_RS_TXT);    // 통증평가 미실시/1회 사유

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
        }

        public void UpdData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            base.Upd_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran);

            List<object> para = new List<object>();

            // --- A. 기본 정보 및 B~E 단일값 메인 테이블 UPDATE ---
            string sql = "";
            sql += Environment.NewLine + "UPDATE TI84_ASM035 SET ";
            sql += Environment.NewLine + "IPAT_DD=?, DSCG_YN=?, DSCG_DD=?, ";
            sql += Environment.NewLine + "NCT_STA_DT=?, NCT_END_DT=?, NCT_FRM_CD=?, ASM_NCT_MTH_CD=?, NCT_RS_CD=?, MDFEE_CD=?, MDFEE_CD_NM=?, ";
            sql += Environment.NewLine + "PTNT_ASM_YN=?, LBT_TRET_YN=?, CNTR_TMPR_MASR_YN=?, TMPR_RGN_CD=?, TMPR_RGN_ETC_TXT=?, LWET_TMPR=?, ";
            sql += Environment.NewLine + "NRRT_BLCK_USE_YN=?, NRRT_MNTR_YN=?, RCRM_IPAT_YN=?, RCRM_DSU_RS_CD=?, EMSS_ASM_EXEC_FQ_CD=?, EMSS_ASM_RS_TXT=?, ";
            sql += Environment.NewLine + "PAIN_ASM_EXEC_FQ_CD=?, PAIN_ASM_RS_TXT=? ";
            sql += Environment.NewLine + "WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "  AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "  AND SEQ = '" + SEQ + "'";

            para.Clear();
            para.Add(IPAT_DD);            // 입원일자(YYYYMMDD)
            para.Add(DSCG_YN);            // 퇴원여부(1.Yes 2.No)
            para.Add(DSCG_DD);            // 퇴원일자(YYYYMMDD)
            para.Add(NCT_STA_DT);         // 마취 시작일시(YYYYMMDDHHMM)
            para.Add(NCT_END_DT);         // 마취 종료일시(YYYYMMDDHHMM)
            para.Add(NCT_FRM_CD);         // 마취형태 구분코드(1.정규 2.응급)
            para.Add(ASM_NCT_MTH_CD);     // 마취방법 구분코드(다중선택, 예: 1/3/7/8)
            para.Add(NCT_RS_CD);          // 마취사유 구분코드(01~99)
            para.Add(MDFEE_CD);           // 수가코드(마취사유가 02/03/04일 때 필수)
            para.Add(MDFEE_CD_NM);        // 수가코드명(마취사유가 02/03/04일 때 필수)
            para.Add(PTNT_ASM_YN);        // 마취 전 환자평가 시행여부(1.Yes 2.No)
            para.Add(LBT_TRET_YN);        // 의도적 저체온증 적용 여부(1.Yes 2.No)
            para.Add(CNTR_TMPR_MASR_YN);  // 연속적 체온 측정 및 기록 여부(1.Yes 2.No)
            para.Add(TMPR_RGN_CD);        // 체온 측정방법 구분코드(다중선택, 예: 05/06/07)
            para.Add(TMPR_RGN_ETC_TXT);   // 체온 측정방법 기타 상세(99일 때)
            para.Add(LWET_TMPR);          // 최저체온(℃, 소수점 첫째자리)
            para.Add(NRRT_BLCK_USE_YN);   // 신경근 차단제 사용 여부(1.Yes 2.No)
            para.Add(NRRT_MNTR_YN);       // 신경근 감시 여부(1.Yes 2.No, 차단제 Yes일 때)
            para.Add(RCRM_IPAT_YN);       // 회복실 입실 여부(1.Yes 2.No)
            para.Add(RCRM_DSU_RS_CD);     // 회복실 미입실 사유(1~5, 입실 No일 때)
            para.Add(EMSS_ASM_EXEC_FQ_CD);// 오심 및 구토평가 실시횟수(0.미실시 1.1회 2.2회 이상)
            para.Add(EMSS_ASM_RS_TXT);    // 오심 및 구토평가 미실시/1회 사유
            para.Add(PAIN_ASM_EXEC_FQ_CD);// 통증평가 실시횟수(0.미실시 1.1회 2.2회 이상)
            para.Add(PAIN_ASM_RS_TXT);    // 통증평가 미실시/1회 사유

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
        }

        public void DelAllData(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += "DELETE FROM TI84_ASM035 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
        }
    }
}
