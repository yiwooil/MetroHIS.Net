using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    public class CDataASM002_002 : CData, IData, IDataRemake
    {
        public readonly string form_id = "ASM002"; // 서식코드
        public readonly string ver_id = "002"; // 서석버전
        public readonly string buss_cd_id = "04"; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
        public readonly string buss_detail_id = "CAB"; // 업무상세코드

        // A. 환자정보
        public string ASM_IPAT_DT;         // 입원일시
        public string ASM_VST_PTH_CD;      // 내원경로
        public string DSCG_YN;             // 퇴원여부
        public string ASM_DSCG_DT;         // 퇴원일시
        public string DSCG_STAT_CD;        // 퇴원상태
        public string DEATH_DT;            // 사망일시 (퇴원상태가 사망인 경우)

        public string HEIG_MASR_CD;        // 신장 측정여부
        public string HEIG;                // 신장 (측정 시)

        public string BWGT_MASR_CD;        // 체중 측정여부
        public string BWGT;                // 체중 (측정 시)

        // B. 과거력 및 시술경험
        public string SMKN_YN;             // 흡연 여부
        public string NTSM_CD;             // 흡연 고유분류(과거흡연 등)
        public string HYTEN_YN;            // 고혈압
        public string DBML_YN;             // 당뇨병
        public string ACTE_MCDI_OCCUR_YN; // 3주 이내 급성심근경색증
        public string ACTE_MCDI_OCCUR_RCD_CD; // 증상발생 기록 여부
        public string ACTE_MCDI_OCCUR_DD; // 증상발생일
        public string ISTBY_AP_OCCUR_YN;   // 불안정협심증
        public string ASM_ETC_PAST_DS_CD;  // 기타 과거질환 (다중 선택 가능)

        public string PCI_MOPR_YN;         // 경피적관상동맥중재술(PCI) 경험
        public string PCI_MOPR_ADM_CD;     // 시술기관
        public string ASM_PCI_MOPR_DT;     // 시술일시
        public string LEFT_MAIN_ENFC_YN;   // LM 시행여부
        public string THREE_VSS_ENFC_YN;       // 3vessel 여부
        public string STENT_INS_TOT_CNT;   // stent 삽입 총 개수

        public string CABG_EXPR_YN;        // 관상동맥우회술 경험
        public string ETC_HRT_SOPR_EXPR_YN; // 기타 심장수술 경험

        // C. 수술 전 진료정보
        public string BPRSU_MASR_CD;       // 혈압측정여부
        public string BPRSU;               // 혈압값 (예: 120/80)

        public string PULS_MASR_CD;        // 맥박측정여부
        public string PULS;                // 맥박 수치

        public string CHOL;                // 총콜레스테롤
        public string NTFT;                // TG(트리글리세라이드)
        public string HDL;                 // HDL
        public string LDL;                 // LDL

        public string SRU_CRAT;            // 혈청 크레아티닌
        public string CRHM;                // 혈색소
        public string HCT;                 // 헤마토크리트

        public string LVEF;                // 좌심실 구혈률
        public string ASM_ECG_OPN_CD;      // 심전도 소견 (다중선택)
        public string ASM_INVS_BLDVE_CD;   // 침습혈관 수
        public string LEFT_MAIN_ILNS_YN;   // Left Main Disease

        public string SOPR_BF_IMPT_CLI_STAT_CD; // 수술 전 주요 임상상태 (다중선택)
        public string VNTR_SUP_DV_CD;      // 심실보조장치 상세

        // D. 수술정보 - CABG
        public List<string> SOPR_STA_DT = new List<string>();                // 수술 시작일시
        public List<string> SOPR_END_DT = new List<string>();                // 수술 종료일시
        public List<string> EMY_YN = new List<string>();                     // 응급여부
        public List<string> ASM_EMY_RS_CD = new List<string>();             // 응급수술 사유
        public List<string> ASM_VNTR_SUP_DV_CD = new List<string>();            // 심실보조장치 상세
        public List<string> ASM_CATH_DT = new List<string>();               // cath 시행 일시
        public List<string> EMY_RS_ETC_TXT = new List<string>();            // 기타 응급사유 상세
        public List<string> USE_BLDVE_CD = new List<string>();              // 이용혈관
        public List<string> ASM_ITAY_USE_RGN_CD = new List<string>();       // 내흉동맥 사용부위
        public List<string> ASM_ITAY_UNUS_RS_CD = new List<string>();       // 내흉동맥 미사용 사유
        public List<string> ITAY_UNUS_RS_ETC_TXT = new List<string>();      // 기타 미사용 사유 상세
        public List<string> ASM_HRT_BLDVE_SAME_SOPR_CD = new List<string>(); // 심혈관관련 동시수술
        public List<string> ETC_SAME_SOPR_CD = new List<string>();          // 기타 동시수술 여부
        public List<string> ETC_SAME_ST1_MDFEE_CD = new List<string>();     // 수술수단코드 1
        public List<string> ETC_SAME_ST1_SOPR_NM = new List<string>();      // 수술명 1
        public List<string> ETC_SAME_ND2_MDFEE_CD = new List<string>();     // 수술수단코드 2
        public List<string> ETC_SAME_ND2_SOPR_NM = new List<string>();      // 수술명 2
        public List<string> ASM_EXTB_DT = new List<string>();               // 발관일시

        // E. CABG 후 개흉술
        public string THRC_YN;                   // 개흉술 여부
        public List<string> THRC_STA_DT = new List<string>();               // 개흉술 시작일시
        public List<string> THRC_SOPR_CD = new List<string>();              // 개흉술 종류 (1수술 / 2수술 이상)
        public List<string> THRC_ST1_MDFEE_CD = new List<string>();         // 수술수가코드1
        public List<string> THRC_ST1_SOPR_NM = new List<string>();          // 수술명1
        public List<string> THRC_ND2_MDFEE_CD = new List<string>();         // 수술수가코드2
        public List<string> THRC_ND2_SOPR_NM = new List<string>();          // 수술명2
        public List<string> ASM_THRC_RS_CD = new List<string>();            // 수술 사유 (출혈, 감염 등 다중선택)
        public List<string> THRC_RS_ETC_TXT = new List<string>();           // 기타 사유 상세

        // F. 퇴원 시 약제처방
        public string ANDR_PRSC_YN;              // 항혈소판제 처방여부
        public string MDS_CD;                    // 약품코드
        public string MDS_NM;                    // 약품명
        public string ASM_ANDR_NOPRS_RS_CD;      // 미처방 사유
        public string ANDR_NOPRS_RS_ETC_TXT;     // 미처방 기타 상세

        public void Clear()
        {
            base.Clear(form_id, ver_id, buss_cd_id, buss_detail_id);
            ClearMe();
        }

        public void ClearMe()
        {
            // A. 환자정보
            ASM_IPAT_DT = "";
            ASM_VST_PTH_CD = "";
            DSCG_YN = "";
            ASM_DSCG_DT = "";
            DSCG_STAT_CD = "";
            DEATH_DT = "";
            HEIG_MASR_CD = "";
            HEIG = "";
            BWGT_MASR_CD = "";
            BWGT = "";

            // B. 과거력 및 시술경험
            SMKN_YN = "";
            NTSM_CD = "";
            HYTEN_YN = "";
            DBML_YN = "";
            ACTE_MCDI_OCCUR_YN = "";
            ACTE_MCDI_OCCUR_RCD_CD = "";
            ACTE_MCDI_OCCUR_DD = "";
            ISTBY_AP_OCCUR_YN = "";
            ASM_ETC_PAST_DS_CD = "";
            PCI_MOPR_YN = "";
            PCI_MOPR_ADM_CD = "";
            ASM_PCI_MOPR_DT = "";
            LEFT_MAIN_ENFC_YN = "";
            THREE_VSS_ENFC_YN = "";
            STENT_INS_TOT_CNT = "";
            CABG_EXPR_YN = "";
            ETC_HRT_SOPR_EXPR_YN = "";

            // C. 수술 전 진료정보
            BPRSU_MASR_CD = "";
            BPRSU = "";
            PULS_MASR_CD = "";
            PULS = "";
            CHOL = "";
            NTFT = "";
            HDL = "";
            LDL = "";
            SRU_CRAT = "";
            CRHM = "";
            HCT = "";
            LVEF = "";
            ASM_ECG_OPN_CD = "";
            ASM_INVS_BLDVE_CD = "";
            LEFT_MAIN_ILNS_YN = "";
            SOPR_BF_IMPT_CLI_STAT_CD = "";
            VNTR_SUP_DV_CD = "";

            // D. 수술정보 - CABG
            SOPR_STA_DT.Clear();
            SOPR_END_DT.Clear();
            EMY_YN.Clear();
            ASM_EMY_RS_CD.Clear();
            ASM_VNTR_SUP_DV_CD.Clear();            // 심실보조장치 상세
            ASM_CATH_DT.Clear();
            EMY_RS_ETC_TXT.Clear();
            USE_BLDVE_CD.Clear();
            ASM_ITAY_USE_RGN_CD.Clear();
            ASM_ITAY_UNUS_RS_CD.Clear();
            ITAY_UNUS_RS_ETC_TXT.Clear();
            ASM_HRT_BLDVE_SAME_SOPR_CD.Clear();
            ETC_SAME_SOPR_CD.Clear();
            ETC_SAME_ST1_MDFEE_CD.Clear();
            ETC_SAME_ST1_SOPR_NM.Clear();
            ETC_SAME_ND2_MDFEE_CD.Clear();
            ETC_SAME_ND2_SOPR_NM.Clear();
            ASM_EXTB_DT.Clear();

            // E. CABG 후 개흉술
            THRC_YN = "";
            THRC_STA_DT.Clear();
            THRC_SOPR_CD.Clear();
            THRC_ST1_MDFEE_CD.Clear();
            THRC_ST1_SOPR_NM.Clear();
            THRC_ND2_MDFEE_CD.Clear();
            THRC_ND2_SOPR_NM.Clear();
            ASM_THRC_RS_CD.Clear();
            THRC_RS_ETC_TXT.Clear();

            // F. 퇴원 시 약제처방
            ANDR_PRSC_YN = "";
            MDS_CD = "";
            MDS_NM = "";
            ASM_ANDR_NOPRS_RS_CD = "";
            ANDR_NOPRS_RS_ETC_TXT = "";
        }

        public void ReadDataFromSaved(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            // 메인 테이블 단일 값 로딩
            string sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM002";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                // A. 환자정보
                ASM_IPAT_DT = reader["ASM_IPAT_DT"].ToString();
                ASM_VST_PTH_CD = reader["ASM_VST_PTH_CD"].ToString();
                DSCG_YN = reader["DSCG_YN"].ToString();
                ASM_DSCG_DT = reader["ASM_DSCG_DT"].ToString();
                DSCG_STAT_CD = reader["DSCG_STAT_CD"].ToString();
                DEATH_DT = reader["DEATH_DT"].ToString();
                HEIG_MASR_CD = reader["HEIG_MASR_CD"].ToString();
                HEIG = reader["HEIG"].ToString();
                BWGT_MASR_CD = reader["BWGT_MASR_CD"].ToString();
                BWGT = reader["BWGT"].ToString();

                // B. 과거력 및 시술경험
                SMKN_YN = reader["SMKN_YN"].ToString();
                NTSM_CD = reader["NTSM_CD"].ToString();
                HYTEN_YN = reader["HYTEN_YN"].ToString();
                DBML_YN = reader["DBML_YN"].ToString();
                ACTE_MCDI_OCCUR_YN = reader["ACTE_MCDI_OCCUR_YN"].ToString();
                ACTE_MCDI_OCCUR_RCD_CD = reader["ACTE_MCDI_OCCUR_RCD_CD"].ToString();
                ACTE_MCDI_OCCUR_DD = reader["ACTE_MCDI_OCCUR_DD"].ToString();
                ISTBY_AP_OCCUR_YN = reader["ISTBY_AP_OCCUR_YN"].ToString();
                ASM_ETC_PAST_DS_CD = reader["ASM_ETC_PAST_DS_CD"].ToString();
                PCI_MOPR_YN = reader["PCI_MOPR_YN"].ToString();
                PCI_MOPR_ADM_CD = reader["PCI_MOPR_ADM_CD"].ToString();
                ASM_PCI_MOPR_DT = reader["ASM_PCI_MOPR_DT"].ToString();
                LEFT_MAIN_ENFC_YN = reader["LEFT_MAIN_ENFC_YN"].ToString();
                THREE_VSS_ENFC_YN = reader["THREE_VSS_ENFC_YN"].ToString();
                STENT_INS_TOT_CNT = reader["STENT_INS_TOT_CNT"].ToString();
                CABG_EXPR_YN = reader["CABG_EXPR_YN"].ToString();
                ETC_HRT_SOPR_EXPR_YN = reader["ETC_HRT_SOPR_EXPR_YN"].ToString();

                // C. 수술 전 진료정보
                BPRSU_MASR_CD = reader["BPRSU_MASR_CD"].ToString();
                BPRSU = reader["BPRSU"].ToString();
                PULS_MASR_CD = reader["PULS_MASR_CD"].ToString();
                PULS = reader["PULS"].ToString();
                CHOL = reader["CHOL"].ToString();
                NTFT = reader["NTFT"].ToString();
                HDL = reader["HDL"].ToString();
                LDL = reader["LDL"].ToString();
                SRU_CRAT = reader["SRU_CRAT"].ToString();
                CRHM = reader["CRHM"].ToString();
                HCT = reader["HCT"].ToString();
                LVEF = reader["LVEF"].ToString();
                ASM_ECG_OPN_CD = reader["ASM_ECG_OPN_CD"].ToString();
                ASM_INVS_BLDVE_CD = reader["ASM_INVS_BLDVE_CD"].ToString();
                LEFT_MAIN_ILNS_YN = reader["LEFT_MAIN_ILNS_YN"].ToString();
                SOPR_BF_IMPT_CLI_STAT_CD = reader["SOPR_BF_IMPT_CLI_STAT_CD"].ToString();
                VNTR_SUP_DV_CD = reader["VNTR_SUP_DV_CD"].ToString();

                // E.기타 개흉술
                THRC_YN = reader["THRC_YN"].ToString();

                // F. 퇴원 시 약제처방
                ANDR_PRSC_YN = reader["ANDR_PRSC_YN"].ToString();
                MDS_CD = reader["MDS_CD"].ToString();
                MDS_NM = reader["MDS_NM"].ToString();
                ASM_ANDR_NOPRS_RS_CD = reader["ASM_ANDR_NOPRS_RS_CD"].ToString();
                ANDR_NOPRS_RS_ETC_TXT = reader["ANDR_NOPRS_RS_ETC_TXT"].ToString();

                return MetroLib.SqlHelper.BREAK;
            });

            // D. CABG 수술 목록
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM002_CABG";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            sql += Environment.NewLine + " ORDER BY SEQNO";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, reader =>
            {
                System.Windows.Forms.Application.DoEvents();

                SOPR_STA_DT.Add(reader["SOPR_STA_DT"].ToString());
                SOPR_END_DT.Add(reader["SOPR_END_DT"].ToString());
                EMY_YN.Add(reader["EMY_YN"].ToString());
                ASM_EMY_RS_CD.Add(reader["ASM_EMY_RS_CD"].ToString());
                ASM_VNTR_SUP_DV_CD.Add(reader["ASM_VNTR_SUP_DV_CD"].ToString());
                ASM_CATH_DT.Add(reader["ASM_CATH_DT"].ToString());
                EMY_RS_ETC_TXT.Add(reader["EMY_RS_ETC_TXT"].ToString());
                USE_BLDVE_CD.Add(reader["USE_BLDVE_CD"].ToString());
                ASM_ITAY_USE_RGN_CD.Add(reader["ASM_ITAY_USE_RGN_CD"].ToString());
                ASM_ITAY_UNUS_RS_CD.Add(reader["ASM_ITAY_UNUS_RS_CD"].ToString());
                ITAY_UNUS_RS_ETC_TXT.Add(reader["ITAY_UNUS_RS_ETC_TXT"].ToString());
                ASM_HRT_BLDVE_SAME_SOPR_CD.Add(reader["ASM_HRT_BLDVE_SAME_SOPR_CD"].ToString());
                ETC_SAME_SOPR_CD.Add(reader["ETC_SAME_SOPR_CD"].ToString());
                ETC_SAME_ST1_MDFEE_CD.Add(reader["ETC_SAME_ST1_MDFEE_CD"].ToString());
                ETC_SAME_ST1_SOPR_NM.Add(reader["ETC_SAME_ST1_SOPR_NM"].ToString());
                ETC_SAME_ND2_MDFEE_CD.Add(reader["ETC_SAME_ND2_MDFEE_CD"].ToString());
                ETC_SAME_ND2_SOPR_NM.Add(reader["ETC_SAME_ND2_SOPR_NM"].ToString());
                ASM_EXTB_DT.Add(reader["ASM_EXTB_DT"].ToString());

                return MetroLib.SqlHelper.CONTINUE;
            });

            // E. 개흉술 목록
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM002_THRC";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";
            sql += Environment.NewLine + " ORDER BY SEQNO";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, reader =>
            {
                System.Windows.Forms.Application.DoEvents();

                THRC_STA_DT.Add(reader["THRC_STA_DT"].ToString());
                THRC_SOPR_CD.Add(reader["THRC_SOPR_CD"].ToString());
                THRC_ST1_MDFEE_CD.Add(reader["THRC_ST1_MDFEE_CD"].ToString());
                THRC_ST1_SOPR_NM.Add(reader["THRC_ST1_SOPR_NM"].ToString());
                THRC_ND2_MDFEE_CD.Add(reader["THRC_ND2_MDFEE_CD"].ToString());
                THRC_ND2_SOPR_NM.Add(reader["THRC_ND2_SOPR_NM"].ToString());
                ASM_THRC_RS_CD.Add(reader["ASM_THRC_RS_CD"].ToString());
                THRC_RS_ETC_TXT.Add(reader["THRC_RS_ETC_TXT"].ToString());

                return MetroLib.SqlHelper.CONTINUE;
            });
        }

        public void ReadDataFromEMR(OleDbConnection conn, OleDbTransaction p_tran)
        {
        }

        public void InsData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran, bool del_fg)
        {
            string sql = "";

            if (del_fg == true)
            {
                sql = "";
                sql += "DELETE FROM TI84_ASM002 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM002_CABG WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

                sql = "";
                sql += "DELETE FROM TI84_ASM002_THRC WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            }

            List<object> para = new List<object>();

            // 1. 단일 ROW: TI84_ASM002
            sql = "";
            sql += "INSERT INTO TI84_ASM002(";
            sql += "FORM, KEYSTR, SEQ, VER, ";
            sql += "ASM_IPAT_DT, ASM_VST_PTH_CD, DSCG_YN, ASM_DSCG_DT, DSCG_STAT_CD, DEATH_DT, "; // 6
            sql += "HEIG_MASR_CD, HEIG, BWGT_MASR_CD, BWGT, "; // 4
            sql += "SMKN_YN, NTSM_CD, HYTEN_YN, DBML_YN, ACTE_MCDI_OCCUR_YN, ACTE_MCDI_OCCUR_RCD_CD, ACTE_MCDI_OCCUR_DD, "; // 7
            sql += "ISTBY_AP_OCCUR_YN, ASM_ETC_PAST_DS_CD, PCI_MOPR_YN, PCI_MOPR_ADM_CD, ASM_PCI_MOPR_DT, "; // 5
            sql += "LEFT_MAIN_ENFC_YN, THREE_VSS_ENFC_YN, STENT_INS_TOT_CNT, CABG_EXPR_YN, ETC_HRT_SOPR_EXPR_YN, "; // 5
            sql += "BPRSU_MASR_CD, BPRSU, PULS_MASR_CD, PULS, "; // 4
            sql += "CHOL, NTFT, HDL, LDL, SRU_CRAT, CRHM, HCT, LVEF, "; // 8
            sql += "ASM_ECG_OPN_CD, ASM_INVS_BLDVE_CD, LEFT_MAIN_ILNS_YN, SOPR_BF_IMPT_CLI_STAT_CD, VNTR_SUP_DV_CD, "; // 5
            sql += "THRC_YN, ANDR_PRSC_YN, MDS_CD, MDS_NM, ASM_ANDR_NOPRS_RS_CD, ANDR_NOPRS_RS_ETC_TXT ";
            sql += ") ";
            sql += "VALUES(";
            sql += "?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ?)";

            para.Clear();
            para.Add(form);
            para.Add(KEYSTR);
            para.Add(SEQ);
            para.Add(ver);
            para.Add(ASM_IPAT_DT);
            para.Add(ASM_VST_PTH_CD);
            para.Add(DSCG_YN);
            para.Add(ASM_DSCG_DT);
            para.Add(DSCG_STAT_CD);
            para.Add(DEATH_DT);

            para.Add(HEIG_MASR_CD);
            para.Add(HEIG);
            para.Add(BWGT_MASR_CD);
            para.Add(BWGT);

            para.Add(SMKN_YN);
            para.Add(NTSM_CD);
            para.Add(HYTEN_YN);
            para.Add(DBML_YN);
            para.Add(ACTE_MCDI_OCCUR_YN);
            para.Add(ACTE_MCDI_OCCUR_RCD_CD);
            para.Add(ACTE_MCDI_OCCUR_DD);
            para.Add(ISTBY_AP_OCCUR_YN);
            para.Add(ASM_ETC_PAST_DS_CD);
            para.Add(PCI_MOPR_YN);
            para.Add(PCI_MOPR_ADM_CD);
            para.Add(ASM_PCI_MOPR_DT);
            para.Add(LEFT_MAIN_ENFC_YN);
            para.Add(THREE_VSS_ENFC_YN);
            para.Add(STENT_INS_TOT_CNT);
            para.Add(CABG_EXPR_YN);
            para.Add(ETC_HRT_SOPR_EXPR_YN);

            para.Add(BPRSU_MASR_CD);
            para.Add(BPRSU);
            para.Add(PULS_MASR_CD);
            para.Add(PULS);

            para.Add(CHOL);
            para.Add(NTFT);
            para.Add(HDL);
            para.Add(LDL);
            para.Add(SRU_CRAT);
            para.Add(CRHM);
            para.Add(HCT);
            para.Add(LVEF);

            para.Add(ASM_ECG_OPN_CD);
            para.Add(ASM_INVS_BLDVE_CD);
            para.Add(LEFT_MAIN_ILNS_YN);
            para.Add(SOPR_BF_IMPT_CLI_STAT_CD);
            para.Add(VNTR_SUP_DV_CD);

            para.Add(THRC_YN);

            para.Add(ANDR_PRSC_YN);
            para.Add(MDS_CD);
            para.Add(MDS_NM);
            para.Add(ASM_ANDR_NOPRS_RS_CD);
            para.Add(ANDR_NOPRS_RS_ETC_TXT);

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            // 2. 반복 ROW: CABG 수술 TBL_CABG
            for (int i = 0; i < SOPR_STA_DT.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM002_CABG(";
                sql += "FORM, KEYSTR, SEQ, SEQNO, SOPR_STA_DT, SOPR_END_DT, EMY_YN, ASM_EMY_RS_CD, ASM_VNTR_SUP_DV_CD, ASM_CATH_DT, EMY_RS_ETC_TXT, ";
                sql += "USE_BLDVE_CD, ASM_ITAY_USE_RGN_CD, ASM_ITAY_UNUS_RS_CD, ITAY_UNUS_RS_ETC_TXT, ASM_HRT_BLDVE_SAME_SOPR_CD, ETC_SAME_SOPR_CD, ";
                sql += "ETC_SAME_ST1_MDFEE_CD, ETC_SAME_ST1_SOPR_NM, ETC_SAME_ND2_MDFEE_CD, ETC_SAME_ND2_SOPR_NM, ASM_EXTB_DT)";
                sql += "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(SOPR_STA_DT[i]);
                para.Add(SOPR_END_DT[i]);
                para.Add(EMY_YN[i]);
                para.Add(ASM_EMY_RS_CD[i]);
                para.Add(ASM_VNTR_SUP_DV_CD[i]);
                para.Add(ASM_CATH_DT[i]);
                para.Add(EMY_RS_ETC_TXT[i]);
                para.Add(USE_BLDVE_CD[i]);
                para.Add(ASM_ITAY_USE_RGN_CD[i]);
                para.Add(ASM_ITAY_UNUS_RS_CD[i]);
                para.Add(ITAY_UNUS_RS_ETC_TXT[i]);
                para.Add(ASM_HRT_BLDVE_SAME_SOPR_CD[i]);
                para.Add(ETC_SAME_SOPR_CD[i]);
                para.Add(ETC_SAME_ST1_MDFEE_CD[i]);
                para.Add(ETC_SAME_ST1_SOPR_NM[i]);
                para.Add(ETC_SAME_ND2_MDFEE_CD[i]);
                para.Add(ETC_SAME_ND2_SOPR_NM[i]);
                para.Add(ASM_EXTB_DT[i]);

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // 3. 반복 ROW: 개흉술 TBL_THRC
            for (int i = 0; i < THRC_STA_DT.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM002_THRC(";
                sql += "FORM, KEYSTR, SEQ, SEQNO, THRC_STA_DT, THRC_SOPR_CD, THRC_ST1_MDFEE_CD, THRC_ST1_SOPR_NM, ";
                sql += "THRC_ND2_MDFEE_CD, THRC_ND2_SOPR_NM, ASM_THRC_RS_CD, THRC_RS_ETC_TXT)";
                sql += "VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form);
                para.Add(KEYSTR);
                para.Add(SEQ);
                para.Add(i + 1);
                para.Add(THRC_STA_DT[i]);
                para.Add(THRC_SOPR_CD[i]);
                para.Add(THRC_ST1_MDFEE_CD[i]);
                para.Add(THRC_ST1_SOPR_NM[i]);
                para.Add(THRC_ND2_MDFEE_CD[i]);
                para.Add(THRC_ND2_SOPR_NM[i]);
                para.Add(ASM_THRC_RS_CD[i]);
                para.Add(THRC_RS_ETC_TXT[i]);

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }
        }

        public void UpdData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            base.Upd_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran);

            List<object> para = new List<object>();

            // ① 단일 레코드(기본 정보) UPDATE
            string sql = "";
            sql += "UPDATE TI84_ASM002 SET ";
            sql += "ASM_IPAT_DT=?, ASM_VST_PTH_CD=?, DSCG_YN=?, ASM_DSCG_DT=?, DSCG_STAT_CD=?, DEATH_DT=?, ";
            sql += "HEIG_MASR_CD=?, HEIG=?, BWGT_MASR_CD=?, BWGT=?, ";
            sql += "SMKN_YN=?, NTSM_CD=?, HYTEN_YN=?, DBML_YN=?, ACTE_MCDI_OCCUR_YN=?, ACTE_MCDI_OCCUR_RCD_CD=?, ACTE_MCDI_OCCUR_DD=?, ";
            sql += "ISTBY_AP_OCCUR_YN=?, ASM_ETC_PAST_DS_CD=?, PCI_MOPR_YN=?, PCI_MOPR_ADM_CD=?, ASM_PCI_MOPR_DT=?, ";
            sql += "LEFT_MAIN_ENFC_YN=?, THREE_VSS_ENFC_YN=?, STENT_INS_TOT_CNT=?, CABG_EXPR_YN=?, ETC_HRT_SOPR_EXPR_YN=?, ";
            sql += "BPRSU_MASR_CD=?, BPRSU=?, PULS_MASR_CD=?, PULS=?, CHOL=?, NTFT=?, HDL=?, LDL=?, SRU_CRAT=?, CRHM=?, HCT=?, LVEF=?, ";
            sql += "ASM_ECG_OPN_CD=?, ASM_INVS_BLDVE_CD=?, LEFT_MAIN_ILNS_YN=?, SOPR_BF_IMPT_CLI_STAT_CD=?, VNTR_SUP_DV_CD=?, ";
            sql += "THRC_YN=?, ANDR_PRSC_YN=?, MDS_CD=?, MDS_NM=?, ASM_ANDR_NOPRS_RS_CD=?, ANDR_NOPRS_RS_ETC_TXT=? ";
            sql += "WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";

            para.Clear();
            para.Add(ASM_IPAT_DT);
            para.Add(ASM_VST_PTH_CD);
            para.Add(DSCG_YN);
            para.Add(ASM_DSCG_DT);
            para.Add(DSCG_STAT_CD);
            para.Add(DEATH_DT);
            para.Add(HEIG_MASR_CD);
            para.Add(HEIG);
            para.Add(BWGT_MASR_CD);
            para.Add(BWGT);

            para.Add(SMKN_YN);
            para.Add(NTSM_CD);
            para.Add(HYTEN_YN);
            para.Add(DBML_YN);
            para.Add(ACTE_MCDI_OCCUR_YN);
            para.Add(ACTE_MCDI_OCCUR_RCD_CD);
            para.Add(ACTE_MCDI_OCCUR_DD);
            para.Add(ISTBY_AP_OCCUR_YN);
            para.Add(ASM_ETC_PAST_DS_CD);
            para.Add(PCI_MOPR_YN);
            para.Add(PCI_MOPR_ADM_CD);
            para.Add(ASM_PCI_MOPR_DT);
            para.Add(LEFT_MAIN_ENFC_YN);
            para.Add(THREE_VSS_ENFC_YN);
            para.Add(STENT_INS_TOT_CNT);
            para.Add(CABG_EXPR_YN);
            para.Add(ETC_HRT_SOPR_EXPR_YN);

            para.Add(BPRSU_MASR_CD);
            para.Add(BPRSU);
            para.Add(PULS_MASR_CD);
            para.Add(PULS);
            para.Add(CHOL);
            para.Add(NTFT);
            para.Add(HDL);
            para.Add(LDL);
            para.Add(SRU_CRAT);
            para.Add(CRHM);
            para.Add(HCT);
            para.Add(LVEF);

            para.Add(ASM_ECG_OPN_CD);
            para.Add(ASM_INVS_BLDVE_CD);
            para.Add(LEFT_MAIN_ILNS_YN);
            para.Add(SOPR_BF_IMPT_CLI_STAT_CD);
            para.Add(VNTR_SUP_DV_CD);

            para.Add(THRC_YN);

            para.Add(ANDR_PRSC_YN);
            para.Add(MDS_CD);
            para.Add(MDS_NM);
            para.Add(ASM_ANDR_NOPRS_RS_CD);
            para.Add(ANDR_NOPRS_RS_ETC_TXT);

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);

            // ② CABG 수술 테이블 삭제 후 재삽입
            sql = "DELETE FROM TI84_ASM002_CABG WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            for (int i = 0; i < SOPR_STA_DT.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM002_CABG(";
                sql += "FORM, KEYSTR, SEQ, SEQNO, SOPR_STA_DT, SOPR_END_DT, EMY_YN, ASM_EMY_RS_CD, ASM_VNTR_SUP_DV_CD, ASM_CATH_DT, EMY_RS_ETC_TXT, ";
                sql += "USE_BLDVE_CD, ASM_ITAY_USE_RGN_CD, ASM_ITAY_UNUS_RS_CD, ITAY_UNUS_RS_ETC_TXT, ";
                sql += "ASM_HRT_BLDVE_SAME_SOPR_CD, ETC_SAME_SOPR_CD, ETC_SAME_ST1_MDFEE_CD, ETC_SAME_ST1_SOPR_NM, ";
                sql += "ETC_SAME_ND2_MDFEE_CD, ETC_SAME_ND2_SOPR_NM, ASM_EXTB_DT)";
                sql += " VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form); 
                para.Add(KEYSTR); 
                para.Add(SEQ); 
                para.Add(i + 1);
                para.Add(SOPR_STA_DT[i]);
                para.Add(SOPR_END_DT[i]);
                para.Add(EMY_YN[i]);
                para.Add(ASM_EMY_RS_CD[i]);
                para.Add(ASM_VNTR_SUP_DV_CD[i]);
                para.Add(ASM_CATH_DT[i]);
                para.Add(EMY_RS_ETC_TXT[i]);
                para.Add(USE_BLDVE_CD[i]);
                para.Add(ASM_ITAY_USE_RGN_CD[i]);
                para.Add(ASM_ITAY_UNUS_RS_CD[i]);
                para.Add(ITAY_UNUS_RS_ETC_TXT[i]);
                para.Add(ASM_HRT_BLDVE_SAME_SOPR_CD[i]);
                para.Add(ETC_SAME_SOPR_CD[i]);
                para.Add(ETC_SAME_ST1_MDFEE_CD[i]);
                para.Add(ETC_SAME_ST1_SOPR_NM[i]);
                para.Add(ETC_SAME_ND2_MDFEE_CD[i]);
                para.Add(ETC_SAME_ND2_SOPR_NM[i]);
                para.Add(ASM_EXTB_DT[i]);

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }

            // ③ 개흉술 수술 테이블 삭제 후 재삽입
            sql = "DELETE FROM TI84_ASM002_THRC WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);

            for (int i = 0; i < THRC_STA_DT.Count; i++)
            {
                sql = "";
                sql += "INSERT INTO TI84_ASM002_THRC(";
                sql += "FORM, KEYSTR, SEQ, SEQNO, THRC_STA_DT, THRC_SOPR_CD, THRC_ST1_MDFEE_CD, THRC_ST1_SOPR_NM, ";
                sql += "THRC_ND2_MDFEE_CD, THRC_ND2_SOPR_NM, ASM_THRC_RS_CD, THRC_RS_ETC_TXT)";
                sql += " VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                para.Clear();
                para.Add(form); 
                para.Add(KEYSTR); 
                para.Add(SEQ); 
                para.Add(i + 1);
                para.Add(THRC_STA_DT[i]);
                para.Add(THRC_SOPR_CD[i]);
                para.Add(THRC_ST1_MDFEE_CD[i]);
                para.Add(THRC_ST1_SOPR_NM[i]);
                para.Add(THRC_ND2_MDFEE_CD[i]);
                para.Add(THRC_ND2_SOPR_NM[i]);
                para.Add(ASM_THRC_RS_CD[i]);
                para.Add(THRC_RS_ETC_TXT[i]);

                MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
            }
        }

        public void DelAllData(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += "DELETE FROM TI84_ASM002 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM002_CABG WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            sql = "";
            sql += "DELETE FROM TI84_ASM002_THRC WHERE FORM = '" + form + "' AND KEYSTR = '" + KEYSTR + "' AND SEQ = '" + SEQ + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
        }
    }
}
