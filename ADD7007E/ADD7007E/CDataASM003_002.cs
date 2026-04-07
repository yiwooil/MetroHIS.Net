using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    public class CDataASM003_002 : CData, IData, IDataRemake
    {
        public readonly string form_id = "ASM003"; // 서식코드
        public readonly string ver_id = "002"; // 서석버전
        public readonly string buss_cd_id = "04"; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
        public readonly string buss_detail_id = "STR"; // 업무상세코드

        // --- A. 입∙퇴원 정보 ---
        public string ASM_HOSP_ARIV_DT;         // 병원 도착일시(YYYYMMDDHHMM)

        // 퇴원
        public string DSCG_YN;                  // 퇴원 여부(1:Yes, 2:No)
        public string ASM_DSCG_DT;              // 퇴원일시(YYYYMMDDHHMM, 퇴원 Yes)
        public string DSCG_STAT_RCD_YN;         // 퇴원상태 기록 여부(1:Yes, 2:No, 퇴원 Yes)
        public string DSCG_STAT_CD;             // 퇴원상태(1:호전퇴원, 2:치료거부, 3:가망없음, 4:타병원전원, 5:사망, 퇴원상태 기록 Yes)
        public string ACPH_DHI_RS_CD;           // 전원사유(1:급성기 치료 위해, 2:급성기 치료 후, 퇴원상태=4)
        public string ACPH_CRST_DIAG;           // 퇴원 시 최종 진단명(1~9, "/"로 구분, 퇴원 Yes)
        public string ACPH_CRST_DIAG_ETC_TXT;   // 퇴원 시 최종 진단명 기타 상세(진단명=9)
        public string ACPH_CRST_REL_CD;         // 급성기뇌졸중과의 관련성(0~3, 퇴원 시 진단명 1/3/5/7 중 하나라도 해당)
        public string CRST_SYMT_OCUR_CFR_CD;    // 뇌졸중 증상발생(1~4, 관련성=0)
        public string SYMT_OCUR_DT;             // 증상발생일시(YYYYMMDDHHMM, 증상발생=1)
        public string STAT_CFR_DT;              // 발견일시(YYYYMMDDHHMM, 증상발생=2)
        public string LAST_NRM_CFR_DT;          // 최종정상 확인일시(YYYYMMDDHHMM, 증상발생=2)
        public string ASM_FST_IPAT_PTH_CD;      // 내원장소(1:외래, 2:응급실, 3:기록없음, 증상발생=1,2)
        public string ASM_VST_PTH_CD;           // 내원경로(1:직접내원, 2:타병원전원, 3:기록없음, 내원장소=2)
        public string VST_MTH_CD;               // 내원방법(1:구급차, 2:다른 교통수단, 3:기록없음, 내원장소=2)
        public string RHBLTN_DGSBJT_TRFR_YN;    // 재활의학과 전과 여부(1:Yes, 2:No, 내원장소=2)
        public string TRFR_DD;                  // 전과일(YYYYMMDD, 전과 Yes)
        public string WLST_RCD_YN;              // 연명의료중단등결정이행서 작성여부(1:Yes, 2:No, 내원장소=2)
        public string DEATH_PCS_PTNT_RCD_DT;    // 임종과정 환자 판단서 작성일자(YYYYMMDD, 연명의료중단등결정 Yes)
        public string BRN_DEATH_YN;             // 뇌사판정 여부(1:Yes, 2:No, 내원장소=2)
        public string BRN_DEATH_DD;             // 뇌사판정일자(YYYYMMDD, 뇌사판정 Yes)

        // --- B. 진료 정보 ---
        // Stroke Scale
        public string CRST_SCL_ENFC_YN;         // Stroke Scale 시행 여부(1:Yes, 2:No, 내원장소=2)
        public string CRST_SCL_FST_EXEC_DD;     // 최초 시행일(YYYYMMDD, Stroke Scale Yes)
        public string CRST_SCL_KND_CD;          // Stroke Scale 종류(1:NIHSS, 2:GCS, 9:기타, Stroke Scale Yes)
        public string ETC_CRST_SCL_NM;          // 기타 Scale 명칭(Stroke Scale 종류=9)
        public string ETC_CRST_SCL_HGH_PNT;     // 기타 Scale 최고점수(Stroke Scale 종류=9)
        public string NIHSS_PNT;                // NIHSS 평가점수(Stroke Scale 종류=1)
        public string GCS_PNT;                  // GCS 평가점수(Stroke Scale 종류=2)
        public string ETC_CRST_SCL_ASM_PNT;     // 기타 Scale 평가점수(Stroke Scale 종류=9)

        // 기능평가 실시
        public string FCLT_ASM_TL_ENFC_YN;      // 기능평가 시행 여부(1:Yes, 2:No, 내원장소=2)
        public string LAST_FCLT_ASM_TL_ENFC_DD; // 시행일(YYYYMMDD, 기능평가 Yes)
        public string DSCG_FCLT_ASM_TL_KND_CD;  // 기능평가 종류(1:K-MBI, 2:MBI, 3:BI, 4:FIM, 5:mRS, 6:GOS, 9:기타, 기능평가 Yes)
        public string ETC_FCLT_ASM_TL_TXT;      // 기타 Scale 명칭(기능평가 종류=9)
        public string ETC_FCLT_ASM_TL_HGH_PNT;  // 기타 Scale 최고점수(기능평가 종류=9)
        public string KMBI_PNT;                 // K-MBI 평가점수(기능평가 종류=1)
        public string MBI_PNT;                  // MBI 평가점수(기능평가 종류=2)
        public string BI_PNT;                   // BI 평가점수(기능평가 종류=3)
        public string FIM_PNT;                  // FIM 평가점수(기능평가 종류=4)
        public string MRS_GRD;                  // mRS 평가등급(기능평가 종류=5)
        public string GOS_GRD;                  // GOS 평가등급(기능평가 종류=6)
        public string ETC_FCLT_ASM_PNT;         // 기타 Scale 평가점수(기능평가 종류=9)

        // 조기재활
        public string RHBLTN_DDIAG_REQ_YN;      // 재활협진 의뢰 여부(1:Yes, 2:No, 3:재활의학과 없음, 내원장소=2)
        public string FST_REQ_DD;               // 의뢰일(YYYYMMDD, 재활협진의뢰 Yes)
        public string RHBLTN_DDIAG_FST_RPY_YN;  // 재활협진 회신 여부(1:Yes, 2:No, 재활협진의뢰 Yes)
        public string RPY_DD;                   // 회신일(YYYYMMDD, 회신 Yes)
        public string RHBLTN_TRET_YN;           // 재활치료 여부(1:Yes, 2:No, 내원장소=2)
        public string FST_TRET_DD;              // 치료일(YYYYMMDD, 재활치료 Yes)
        public string FCLT_HDP_YN;              // 기능장해 여부(1:Yes, 2:No, 내원장소=2)
        public string CLI_ISTBY_RS_CD;          // 재활협진/치료 미실시 사유(00~09,99, 기능장해 Yes)
        public string CLI_ISTBY_RS_ETC_TXT;     // 미실시 사유 기타 상세(사유=99)
        public string CLI_ISTBY_RS_RCD_DD;      // 임상적 불안정 사유 등 기록 일자(YYYYMMDD, 사유=01~08,99)
        public string FCLT_HDP_ASM_YN;          // 기능장해 평가 여부(1:Yes, 2:No, 내원장소=2)
        public string FCLT_HDP_ASM_TL_KND_CD;   // 기능장해 평가도구(1:mRS, 2:NIHSS, 9:기타, 평가 Yes)
        public string FCLT_HDP_ASM_ETC_TL_TXT;  // 평가도구 기타 명칭(도구=9)
        public string HDP_MRS_GRD;              // mRS 평가등급(도구=1)
        public string HDP_NIHSS_PNT;            // NIHSS 평가점수(도구=2)
        public string FCLT_HDP_ETC_ASM_PNT;     // 기타 평가점수(도구=9)
        public string FCLT_HDP_ASM_TL_EXEC_DD;  // 평가도구 실시일(YYYYMMDD, 평가 Yes)

        // 폐렴
        public string HR48_AF_PNEM_SICK_YN;     // 입원 48시간 이후 폐렴 발생(1:Yes, 2:No, 내원장소=2)
        public string PNEM_KND_CD;              // 폐렴 종류(1:흡인성, 2:인공호흡기, 9:기타, 폐렴 Yes)
        public string PNEM_KND_ETC_TXT;         // 폐렴 종류 기타 상세(종류=9)
        public string DIAG_SICK_SYM;            // 상병분류기호(폐렴 Yes)
        public string DIAG_NM;                  // 진단명(폐렴 Yes)
        public string ATFL_RPRT_YN;             // 인공호흡기 적용 여부(1:Yes, 2:No, 폐렴 Yes)
        public string ATFL_RPRT_FST_STA_DD;     // 최초 적용 시작일(YYYYMMDD, 인공호흡기 Yes)
        public string ATFL_RPRT_FST_END_DD;     // 최초 적용 종료일(YYYYMMDD, 인공호흡기 Yes)

        // --- C. 허혈성 뇌졸중 ---
        // 정맥내 t-PA 투여(액티라제)
        public string DGM_INJC_YN;              // 정맥내 t-PA투여 여부(1:Yes, 2:No, 진단명=7)
        public string MDS_INJC_DT;              // 투여일시(YYYYMMDDHHMM, t-PA투여 Yes)
        public string MDS_INJC_NEXEC_RS_CD;     // 미투여 사유(01~11,99, t-PA투여 No)
        public string MDS_INJC_NEXEC_RS_ETC_TXT;// 미투여 사유 기타 상세(미투여사유=99)
        public string MN60_ECS_INJC_RS_CD2;     // 병원도착 60분 초과 투여 사유(0~5, t-PA투여 Yes)

        // 동맥내 혈전제거술(기계적 혈전제거술)
        public string INARTR_THBE_EXEC_YN;      // 동맥내 혈전제거술 실시 여부(1:Yes, 2:No, 진단명=7)
        public string THBE_EXEC_DT;             // 실시일시(YYYYMMDDHHMM, 혈전제거술 Yes)
        public string MN120_ECS_EXEC_RS_CD;     // 병원도착 120분 초과 실시 사유(0~4,9, 혈전제거술 Yes)
        public string MN120_ECS_EXEC_RS_ETC_TXT;// 초과 실시 사유 기타 상세(사유=9)

        // --- D. 출혈성 뇌졸중 ---
        // 지주막하출혈 최종치료
        public string SBRC_HMRHG_LAST_TRET_EXEC_YN;   // 최종치료 실시 여부(1:Yes, 2:No, 진단명=1)
        public string LAST_TRET_EXEC_DT;              // 실시일시(YYYYMMDDHHMM, 치료 Yes)
        public string HR24_ECS_LAST_TRET_EXEC_RS_CD;  // 병원도착 24시간 초과 실시 사유(1,2,9, 치료 Yes)
        public string HR24_ECS_LAST_TRET_EXEC_RS_ETC_TXT; // 초과 실시 사유 기타 상세(사유=9)

        // --- E. 기타 사항 ---
        //public string APND_DATA_NO;                   // 첨부

        public void Clear()
        {
            base.Clear(form_id, ver_id, buss_cd_id, buss_detail_id);
            ClearMe();
        }

        public void ClearMe()
        {
            // --- A. 입∙퇴원 정보 ---
            ASM_HOSP_ARIV_DT = "";           // 병원 도착일시

            DSCG_YN = "";                    // 퇴원 여부
            ASM_DSCG_DT = "";                // 퇴원일시
            DSCG_STAT_RCD_YN = "";           // 퇴원상태 기록 여부
            DSCG_STAT_CD = "";               // 퇴원상태
            ACPH_DHI_RS_CD = "";             // 전원사유
            ACPH_CRST_DIAG = "";             // 퇴원 시 최종 진단명
            ACPH_CRST_DIAG_ETC_TXT = "";     // 퇴원 시 최종 진단명 기타 상세
            ACPH_CRST_REL_CD = "";           // 급성기뇌졸중과의 관련성
            CRST_SYMT_OCUR_CFR_CD = "";      // 뇌졸중 증상발생
            SYMT_OCUR_DT = "";               // 증상발생일시
            STAT_CFR_DT = "";                // 발견일시
            LAST_NRM_CFR_DT = "";            // 최종정상 확인일시
            ASM_FST_IPAT_PTH_CD = "";        // 내원장소
            ASM_VST_PTH_CD = "";             // 내원경로
            VST_MTH_CD = "";                 // 내원방법
            RHBLTN_DGSBJT_TRFR_YN = "";      // 재활의학과 전과 여부
            TRFR_DD = "";                    // 전과일
            WLST_RCD_YN = "";                // 연명의료중단등결정이행서 작성여부
            DEATH_PCS_PTNT_RCD_DT = "";      // 임종과정 환자 판단서 작성일자
            BRN_DEATH_YN = "";               // 뇌사판정 여부
            BRN_DEATH_DD = "";               // 뇌사판정일자

            // --- B. 진료 정보 ---
            CRST_SCL_ENFC_YN = "";           // Stroke Scale 시행 여부
            CRST_SCL_FST_EXEC_DD = "";       // 최초 시행일
            CRST_SCL_KND_CD = "";            // Stroke Scale 종류
            ETC_CRST_SCL_NM = "";            // 기타 Scale 명칭
            ETC_CRST_SCL_HGH_PNT = "";       // 기타 Scale 최고점수
            NIHSS_PNT = "";                  // NIHSS 평가점수
            GCS_PNT = "";                    // GCS 평가점수
            ETC_CRST_SCL_ASM_PNT = "";       // 기타 Scale 평가점수

            FCLT_ASM_TL_ENFC_YN = "";        // 기능평가 시행 여부
            LAST_FCLT_ASM_TL_ENFC_DD = "";   // 기능평가 시행일
            DSCG_FCLT_ASM_TL_KND_CD = "";    // 기능평가 종류
            ETC_FCLT_ASM_TL_TXT = "";        // 기타 기능평가 명칭
            ETC_FCLT_ASM_TL_HGH_PNT = "";    // 기타 기능평가 최고점수
            KMBI_PNT = "";                   // K-MBI 평가점수
            MBI_PNT = "";                    // MBI 평가점수
            BI_PNT = "";                     // BI 평가점수
            FIM_PNT = "";                    // FIM 평가점수
            MRS_GRD = "";                    // mRS 평가등급
            GOS_GRD = "";                    // GOS 평가등급
            ETC_FCLT_ASM_PNT = "";           // 기타 기능평가 점수

            RHBLTN_DDIAG_REQ_YN = "";        // 재활협진 의뢰 여부
            FST_REQ_DD = "";                 // 재활협진 의뢰일
            RHBLTN_DDIAG_FST_RPY_YN = "";    // 재활협진 회신 여부
            RPY_DD = "";                     // 재활협진 회신일
            RHBLTN_TRET_YN = "";             // 재활치료 여부
            FST_TRET_DD = "";                // 재활치료일
            FCLT_HDP_YN = "";                // 기능장해 여부
            CLI_ISTBY_RS_CD = "";            // 재활협진/치료 미실시 사유
            CLI_ISTBY_RS_ETC_TXT = "";       // 미실시 사유 기타 상세
            CLI_ISTBY_RS_RCD_DD = "";        // 임상적 불안정 사유 등 기록 일자
            FCLT_HDP_ASM_YN = "";            // 기능장해 평가 여부
            FCLT_HDP_ASM_TL_KND_CD = "";     // 기능장해 평가도구
            FCLT_HDP_ASM_ETC_TL_TXT = "";    // 평가도구 기타 명칭
            HDP_MRS_GRD = "";                // mRS 평가등급
            HDP_NIHSS_PNT = "";              // NIHSS 평가점수
            FCLT_HDP_ETC_ASM_PNT = "";       // 기타 평가점수
            FCLT_HDP_ASM_TL_EXEC_DD = "";    // 평가도구 실시일

            HR48_AF_PNEM_SICK_YN = "";       // 입원 48시간 이후 폐렴 발생
            PNEM_KND_CD = "";                // 폐렴 종류
            PNEM_KND_ETC_TXT = "";           // 폐렴 종류 기타 상세
            DIAG_SICK_SYM = "";              // 상병분류기호
            DIAG_NM = "";                    // 진단명
            ATFL_RPRT_YN = "";               // 인공호흡기 적용 여부
            ATFL_RPRT_FST_STA_DD = "";       // 최초 적용 시작일
            ATFL_RPRT_FST_END_DD = "";       // 최초 적용 종료일

            // --- C. 허혈성 뇌졸중 ---
            DGM_INJC_YN = "";                // 정맥내 t-PA투여 여부
            MDS_INJC_DT = "";                // 투여일시
            MDS_INJC_NEXEC_RS_CD = "";       // 미투여 사유
            MDS_INJC_NEXEC_RS_ETC_TXT = "";  // 미투여 사유 기타 상세
            MN60_ECS_INJC_RS_CD2 = "";       // 병원도착 60분 초과 투여 사유

            INARTR_THBE_EXEC_YN = "";        // 동맥내 혈전제거술 실시 여부
            THBE_EXEC_DT = "";               // 실시일시
            MN120_ECS_EXEC_RS_CD = "";       // 병원도착 120분 초과 실시 사유
            MN120_ECS_EXEC_RS_ETC_TXT = "";  // 초과 실시 사유 기타 상세

            // --- D. 출혈성 뇌졸중 ---
            SBRC_HMRHG_LAST_TRET_EXEC_YN = "";       // 최종치료 실시 여부
            LAST_TRET_EXEC_DT = "";                  // 실시일시
            HR24_ECS_LAST_TRET_EXEC_RS_CD = "";      // 병원도착 24시간 초과 실시 사유
            HR24_ECS_LAST_TRET_EXEC_RS_ETC_TXT = ""; // 초과 실시 사유 기타 상세

            // --- E. 기타 사항 ---
            //APND_DATA_NO = "";                       // 첨부
        }


        public void ReadDataFromSaved(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            System.Windows.Forms.Application.DoEvents();

            // --- A~E 전체 단일값 메인 테이블 읽기 ---
            string sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI84_ASM003";
            sql += Environment.NewLine + " WHERE FORM = '" + form + "'";
            sql += Environment.NewLine + "   AND KEYSTR = '" + KEYSTR + "'";
            sql += Environment.NewLine + "   AND SEQ = '" + SEQ + "'";

            MetroLib.SqlHelper.GetDataReader(sql, p_conn, p_tran, delegate(OleDbDataReader reader)
            {
                System.Windows.Forms.Application.DoEvents();

                ASM_HOSP_ARIV_DT = reader["ASM_HOSP_ARIV_DT"].ToString();           // 병원 도착일시
                DSCG_YN = reader["DSCG_YN"].ToString();                             // 퇴원 여부
                ASM_DSCG_DT = reader["ASM_DSCG_DT"].ToString();                     // 퇴원일시
                DSCG_STAT_RCD_YN = reader["DSCG_STAT_RCD_YN"].ToString();           // 퇴원상태 기록 여부
                DSCG_STAT_CD = reader["DSCG_STAT_CD"].ToString();                   // 퇴원상태
                ACPH_DHI_RS_CD = reader["ACPH_DHI_RS_CD"].ToString();               // 전원사유
                ACPH_CRST_DIAG = reader["ACPH_CRST_DIAG"].ToString();               // 퇴원 시 최종 진단명
                ACPH_CRST_DIAG_ETC_TXT = reader["ACPH_CRST_DIAG_ETC_TXT"].ToString(); // 퇴원 시 최종 진단명 기타 상세
                ACPH_CRST_REL_CD = reader["ACPH_CRST_REL_CD"].ToString();           // 급성기뇌졸중과의 관련성
                CRST_SYMT_OCUR_CFR_CD = reader["CRST_SYMT_OCUR_CFR_CD"].ToString(); // 뇌졸중 증상발생
                SYMT_OCUR_DT = reader["SYMT_OCUR_DT"].ToString();                   // 증상발생일시
                STAT_CFR_DT = reader["STAT_CFR_DT"].ToString();                     // 발견일시
                LAST_NRM_CFR_DT = reader["LAST_NRM_CFR_DT"].ToString();             // 최종정상 확인일시
                ASM_FST_IPAT_PTH_CD = reader["ASM_FST_IPAT_PTH_CD"].ToString();     // 내원장소
                ASM_VST_PTH_CD = reader["ASM_VST_PTH_CD"].ToString();               // 내원경로
                VST_MTH_CD = reader["VST_MTH_CD"].ToString();                       // 내원방법
                RHBLTN_DGSBJT_TRFR_YN = reader["RHBLTN_DGSBJT_TRFR_YN"].ToString(); // 재활의학과 전과 여부
                TRFR_DD = reader["TRFR_DD"].ToString();                             // 전과일
                WLST_RCD_YN = reader["WLST_RCD_YN"].ToString();                     // 연명의료중단등결정이행서 작성여부
                DEATH_PCS_PTNT_RCD_DT = reader["DEATH_PCS_PTNT_RCD_DT"].ToString(); // 임종과정 환자 판단서 작성일자
                BRN_DEATH_YN = reader["BRN_DEATH_YN"].ToString();                   // 뇌사판정 여부
                BRN_DEATH_DD = reader["BRN_DEATH_DD"].ToString();                   // 뇌사판정일자

                CRST_SCL_ENFC_YN = reader["CRST_SCL_ENFC_YN"].ToString();           // Stroke Scale 시행 여부
                CRST_SCL_FST_EXEC_DD = reader["CRST_SCL_FST_EXEC_DD"].ToString();   // 최초 시행일
                CRST_SCL_KND_CD = reader["CRST_SCL_KND_CD"].ToString();             // Stroke Scale 종류
                ETC_CRST_SCL_NM = reader["ETC_CRST_SCL_NM"].ToString();             // 기타 Scale 명칭
                ETC_CRST_SCL_HGH_PNT = reader["ETC_CRST_SCL_HGH_PNT"].ToString();   // 기타 Scale 최고점수
                NIHSS_PNT = reader["NIHSS_PNT"].ToString();                         // NIHSS 평가점수
                GCS_PNT = reader["GCS_PNT"].ToString();                             // GCS 평가점수
                ETC_CRST_SCL_ASM_PNT = reader["ETC_CRST_SCL_ASM_PNT"].ToString();   // 기타 Scale 평가점수

                FCLT_ASM_TL_ENFC_YN = reader["FCLT_ASM_TL_ENFC_YN"].ToString();     // 기능평가 시행 여부
                LAST_FCLT_ASM_TL_ENFC_DD = reader["LAST_FCLT_ASM_TL_ENFC_DD"].ToString(); // 기능평가 시행일
                DSCG_FCLT_ASM_TL_KND_CD = reader["DSCG_FCLT_ASM_TL_KND_CD"].ToString();   // 기능평가 종류
                ETC_FCLT_ASM_TL_TXT = reader["ETC_FCLT_ASM_TL_TXT"].ToString();     // 기타 기능평가 명칭
                ETC_FCLT_ASM_TL_HGH_PNT = reader["ETC_FCLT_ASM_TL_HGH_PNT"].ToString();   // 기타 기능평가 최고점수
                KMBI_PNT = reader["KMBI_PNT"].ToString();                           // K-MBI 평가점수
                MBI_PNT = reader["MBI_PNT"].ToString();                             // MBI 평가점수
                BI_PNT = reader["BI_PNT"].ToString();                               // BI 평가점수
                FIM_PNT = reader["FIM_PNT"].ToString();                             // FIM 평가점수
                MRS_GRD = reader["MRS_GRD"].ToString();                             // mRS 평가등급
                GOS_GRD = reader["GOS_GRD"].ToString();                             // GOS 평가등급
                ETC_FCLT_ASM_PNT = reader["ETC_FCLT_ASM_PNT"].ToString();           // 기타 기능평가 점수

                RHBLTN_DDIAG_REQ_YN = reader["RHBLTN_DDIAG_REQ_YN"].ToString();     // 재활협진 의뢰 여부
                FST_REQ_DD = reader["FST_REQ_DD"].ToString();                       // 재활협진 의뢰일
                RHBLTN_DDIAG_FST_RPY_YN = reader["RHBLTN_DDIAG_FST_RPY_YN"].ToString(); // 재활협진 회신 여부
                RPY_DD = reader["RPY_DD"].ToString();                               // 재활협진 회신일
                RHBLTN_TRET_YN = reader["RHBLTN_TRET_YN"].ToString();               // 재활치료 여부
                FST_TRET_DD = reader["FST_TRET_DD"].ToString();                     // 재활치료일
                FCLT_HDP_YN = reader["FCLT_HDP_YN"].ToString();                     // 기능장해 여부
                CLI_ISTBY_RS_CD = reader["CLI_ISTBY_RS_CD"].ToString();             // 재활협진/치료 미실시 사유
                CLI_ISTBY_RS_ETC_TXT = reader["CLI_ISTBY_RS_ETC_TXT"].ToString();   // 미실시 사유 기타 상세
                CLI_ISTBY_RS_RCD_DD = reader["CLI_ISTBY_RS_RCD_DD"].ToString();     // 임상적 불안정 사유 등 기록 일자
                FCLT_HDP_ASM_YN = reader["FCLT_HDP_ASM_YN"].ToString();             // 기능장해 평가 여부
                FCLT_HDP_ASM_TL_KND_CD = reader["FCLT_HDP_ASM_TL_KND_CD"].ToString(); // 기능장해 평가도구
                FCLT_HDP_ASM_ETC_TL_TXT = reader["FCLT_HDP_ASM_ETC_TL_TXT"].ToString(); // 평가도구 기타 명칭
                HDP_MRS_GRD = reader["HDP_MRS_GRD"].ToString();                     // mRS 평가등급
                HDP_NIHSS_PNT = reader["HDP_NIHSS_PNT"].ToString();                 // NIHSS 평가점수
                FCLT_HDP_ETC_ASM_PNT = reader["FCLT_HDP_ETC_ASM_PNT"].ToString();   // 기타 평가점수
                FCLT_HDP_ASM_TL_EXEC_DD = reader["FCLT_HDP_ASM_TL_EXEC_DD"].ToString(); // 평가도구 실시일

                HR48_AF_PNEM_SICK_YN = reader["HR48_AF_PNEM_SICK_YN"].ToString();   // 입원 48시간 이후 폐렴 발생
                PNEM_KND_CD = reader["PNEM_KND_CD"].ToString();                     // 폐렴 종류
                PNEM_KND_ETC_TXT = reader["PNEM_KND_ETC_TXT"].ToString();           // 폐렴 종류 기타 상세
                DIAG_SICK_SYM = reader["DIAG_SICK_SYM"].ToString();                 // 상병분류기호
                DIAG_NM = reader["DIAG_NM"].ToString();                             // 진단명
                ATFL_RPRT_YN = reader["ATFL_RPRT_YN"].ToString();                   // 인공호흡기 적용 여부
                ATFL_RPRT_FST_STA_DD = reader["ATFL_RPRT_FST_STA_DD"].ToString();   // 최초 적용 시작일
                ATFL_RPRT_FST_END_DD = reader["ATFL_RPRT_FST_END_DD"].ToString();   // 최초 적용 종료일

                DGM_INJC_YN = reader["DGM_INJC_YN"].ToString();                     // 정맥내 t-PA투여 여부
                MDS_INJC_DT = reader["MDS_INJC_DT"].ToString();                     // 투여일시
                MDS_INJC_NEXEC_RS_CD = reader["MDS_INJC_NEXEC_RS_CD"].ToString();   // 미투여 사유
                MDS_INJC_NEXEC_RS_ETC_TXT = reader["MDS_INJC_NEXEC_RS_ETC_TXT"].ToString(); // 미투여 사유 기타 상세
                MN60_ECS_INJC_RS_CD2 = reader["MN60_ECS_INJC_RS_CD2"].ToString();   // 병원도착 60분 초과 투여 사유

                INARTR_THBE_EXEC_YN = reader["INARTR_THBE_EXEC_YN"].ToString();     // 동맥내 혈전제거술 실시 여부
                THBE_EXEC_DT = reader["THBE_EXEC_DT"].ToString();                   // 실시일시
                MN120_ECS_EXEC_RS_CD = reader["MN120_ECS_EXEC_RS_CD"].ToString();   // 병원도착 120분 초과 실시 사유
                MN120_ECS_EXEC_RS_ETC_TXT = reader["MN120_ECS_EXEC_RS_ETC_TXT"].ToString(); // 초과 실시 사유 기타 상세

                SBRC_HMRHG_LAST_TRET_EXEC_YN = reader["SBRC_HMRHG_LAST_TRET_EXEC_YN"].ToString(); // 최종치료 실시 여부
                LAST_TRET_EXEC_DT = reader["LAST_TRET_EXEC_DT"].ToString();           // 실시일시
                HR24_ECS_LAST_TRET_EXEC_RS_CD = reader["HR24_ECS_LAST_TRET_EXEC_RS_CD"].ToString(); // 병원도착 24시간 초과 실시 사유
                HR24_ECS_LAST_TRET_EXEC_RS_ETC_TXT = reader["HR24_ECS_LAST_TRET_EXEC_RS_ETC_TXT"].ToString(); // 초과 실시 사유 기타 상세

                //APND_DATA_NO = "";                   // 첨부

                return MetroLib.SqlHelper.BREAK;
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
                sql += "DELETE FROM TI84_ASM003 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "' AND SEQ='" + SEQ + "'";
                MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
            }
            List<object> para = new List<object>();

            // --- A~E 전체 단일값 메인 테이블 저장 ---
            sql = "";
            sql += "INSERT INTO TI84_ASM003 (";
            sql += "FORM, KEYSTR, SEQ, VER, ASM_HOSP_ARIV_DT, DSCG_YN, ASM_DSCG_DT, DSCG_STAT_RCD_YN, DSCG_STAT_CD, ACPH_DHI_RS_CD, "; // 10
            sql += "ACPH_CRST_DIAG, ACPH_CRST_DIAG_ETC_TXT, ACPH_CRST_REL_CD, CRST_SYMT_OCUR_CFR_CD, SYMT_OCUR_DT, STAT_CFR_DT, "; // 6
            sql += "LAST_NRM_CFR_DT, ASM_FST_IPAT_PTH_CD, ASM_VST_PTH_CD, VST_MTH_CD, RHBLTN_DGSBJT_TRFR_YN, TRFR_DD, WLST_RCD_YN, "; // 7
            sql += "DEATH_PCS_PTNT_RCD_DT, BRN_DEATH_YN, BRN_DEATH_DD, CRST_SCL_ENFC_YN, CRST_SCL_FST_EXEC_DD, CRST_SCL_KND_CD, "; // 6
            sql += "ETC_CRST_SCL_NM, ETC_CRST_SCL_HGH_PNT, NIHSS_PNT, GCS_PNT, ETC_CRST_SCL_ASM_PNT, FCLT_ASM_TL_ENFC_YN, "; // 6
            sql += "LAST_FCLT_ASM_TL_ENFC_DD, DSCG_FCLT_ASM_TL_KND_CD, ETC_FCLT_ASM_TL_TXT, ETC_FCLT_ASM_TL_HGH_PNT, KMBI_PNT, "; // 5
            sql += "MBI_PNT, BI_PNT, FIM_PNT, MRS_GRD, GOS_GRD, ETC_FCLT_ASM_PNT, RHBLTN_DDIAG_REQ_YN, FST_REQ_DD, "; // 8
            sql += "RHBLTN_DDIAG_FST_RPY_YN, RPY_DD, RHBLTN_TRET_YN, FST_TRET_DD, FCLT_HDP_YN, CLI_ISTBY_RS_CD, "; // 6
            sql += "CLI_ISTBY_RS_ETC_TXT, CLI_ISTBY_RS_RCD_DD, FCLT_HDP_ASM_YN, FCLT_HDP_ASM_TL_KND_CD, FCLT_HDP_ASM_ETC_TL_TXT, "; // 5
            sql += "HDP_MRS_GRD, HDP_NIHSS_PNT, FCLT_HDP_ETC_ASM_PNT, FCLT_HDP_ASM_TL_EXEC_DD, HR48_AF_PNEM_SICK_YN, PNEM_KND_CD, "; // 6
            sql += "PNEM_KND_ETC_TXT, DIAG_SICK_SYM, DIAG_NM, ATFL_RPRT_YN, ATFL_RPRT_FST_STA_DD, ATFL_RPRT_FST_END_DD, "; // 6
            sql += "DGM_INJC_YN, MDS_INJC_DT, MDS_INJC_NEXEC_RS_CD, MDS_INJC_NEXEC_RS_ETC_TXT, MN60_ECS_INJC_RS_CD2, "; // 5
            sql += "INARTR_THBE_EXEC_YN, THBE_EXEC_DT, MN120_ECS_EXEC_RS_CD, MN120_ECS_EXEC_RS_ETC_TXT, "; // 4
            sql += "SBRC_HMRHG_LAST_TRET_EXEC_YN, LAST_TRET_EXEC_DT, HR24_ECS_LAST_TRET_EXEC_RS_CD, HR24_ECS_LAST_TRET_EXEC_RS_ETC_TXT "; // 4
            sql += ") VALUES (";
            sql += "?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ?, ";
            sql += "?, ?, ?, ?, ";
            sql += "?, ?, ?, ?)";

            para.Clear();
            para.Add(form);                           // FORM
            para.Add(KEYSTR);                         // KEYSTR
            para.Add(SEQ);                            // SEQ
            para.Add(ver);                            // VER
            para.Add(ASM_HOSP_ARIV_DT);               // 병원 도착일시
            para.Add(DSCG_YN);                        // 퇴원 여부
            para.Add(ASM_DSCG_DT);                    // 퇴원일시
            para.Add(DSCG_STAT_RCD_YN);               // 퇴원상태 기록 여부
            para.Add(DSCG_STAT_CD);                   // 퇴원상태
            para.Add(ACPH_DHI_RS_CD);                 // 전원사유
            para.Add(ACPH_CRST_DIAG);                 // 퇴원 시 최종 진단명
            para.Add(ACPH_CRST_DIAG_ETC_TXT);         // 퇴원 시 최종 진단명 기타 상세
            para.Add(ACPH_CRST_REL_CD);               // 급성기뇌졸중과의 관련성
            para.Add(CRST_SYMT_OCUR_CFR_CD);          // 뇌졸중 증상발생
            para.Add(SYMT_OCUR_DT);                   // 증상발생일시
            para.Add(STAT_CFR_DT);                    // 발견일시
            para.Add(LAST_NRM_CFR_DT);                // 최종정상 확인일시
            para.Add(ASM_FST_IPAT_PTH_CD);            // 내원장소
            para.Add(ASM_VST_PTH_CD);                 // 내원경로
            para.Add(VST_MTH_CD);                     // 내원방법
            para.Add(RHBLTN_DGSBJT_TRFR_YN);          // 재활의학과 전과 여부
            para.Add(TRFR_DD);                        // 전과일
            para.Add(WLST_RCD_YN);                    // 연명의료중단등결정이행서 작성여부
            para.Add(DEATH_PCS_PTNT_RCD_DT);          // 임종과정 환자 판단서 작성일자
            para.Add(BRN_DEATH_YN);                   // 뇌사판정 여부
            para.Add(BRN_DEATH_DD);                   // 뇌사판정일자
            para.Add(CRST_SCL_ENFC_YN);               // Stroke Scale 시행 여부
            para.Add(CRST_SCL_FST_EXEC_DD);           // 최초 시행일
            para.Add(CRST_SCL_KND_CD);                // Stroke Scale 종류
            para.Add(ETC_CRST_SCL_NM);                // 기타 Scale 명칭
            para.Add(ETC_CRST_SCL_HGH_PNT);           // 기타 Scale 최고점수
            para.Add(NIHSS_PNT);                      // NIHSS 평가점수
            para.Add(GCS_PNT);                        // GCS 평가점수
            para.Add(ETC_CRST_SCL_ASM_PNT);           // 기타 Scale 평가점수
            para.Add(FCLT_ASM_TL_ENFC_YN);            // 기능평가 시행 여부
            para.Add(LAST_FCLT_ASM_TL_ENFC_DD);       // 기능평가 시행일
            para.Add(DSCG_FCLT_ASM_TL_KND_CD);        // 기능평가 종류
            para.Add(ETC_FCLT_ASM_TL_TXT);            // 기타 기능평가 명칭
            para.Add(ETC_FCLT_ASM_TL_HGH_PNT);        // 기타 기능평가 최고점수
            para.Add(KMBI_PNT);                       // K-MBI 평가점수
            para.Add(MBI_PNT);                        // MBI 평가점수
            para.Add(BI_PNT);                         // BI 평가점수
            para.Add(FIM_PNT);                        // FIM 평가점수
            para.Add(MRS_GRD);                        // mRS 평가등급
            para.Add(GOS_GRD);                        // GOS 평가등급
            para.Add(ETC_FCLT_ASM_PNT);               // 기타 기능평가 점수
            para.Add(RHBLTN_DDIAG_REQ_YN);            // 재활협진 의뢰 여부
            para.Add(FST_REQ_DD);                     // 재활협진 의뢰일
            para.Add(RHBLTN_DDIAG_FST_RPY_YN);        // 재활협진 회신 여부
            para.Add(RPY_DD);                         // 재활협진 회신일
            para.Add(RHBLTN_TRET_YN);                 // 재활치료 여부
            para.Add(FST_TRET_DD);                    // 재활치료일
            para.Add(FCLT_HDP_YN);                    // 기능장해 여부
            para.Add(CLI_ISTBY_RS_CD);                // 재활협진/치료 미실시 사유
            para.Add(CLI_ISTBY_RS_ETC_TXT);           // 미실시 사유 기타 상세
            para.Add(CLI_ISTBY_RS_RCD_DD);            // 임상적 불안정 사유 등 기록 일자
            para.Add(FCLT_HDP_ASM_YN);                // 기능장해 평가 여부
            para.Add(FCLT_HDP_ASM_TL_KND_CD);         // 기능장해 평가도구
            para.Add(FCLT_HDP_ASM_ETC_TL_TXT);        // 평가도구 기타 명칭
            para.Add(HDP_MRS_GRD);                    // mRS 평가등급
            para.Add(HDP_NIHSS_PNT);                  // NIHSS 평가점수
            para.Add(FCLT_HDP_ETC_ASM_PNT);           // 기타 평가점수
            para.Add(FCLT_HDP_ASM_TL_EXEC_DD);        // 평가도구 실시일
            para.Add(HR48_AF_PNEM_SICK_YN);           // 입원 48시간 이후 폐렴 발생
            para.Add(PNEM_KND_CD);                    // 폐렴 종류
            para.Add(PNEM_KND_ETC_TXT);               // 폐렴 종류 기타 상세
            para.Add(DIAG_SICK_SYM);                  // 상병분류기호
            para.Add(DIAG_NM);                        // 진단명
            para.Add(ATFL_RPRT_YN);                   // 인공호흡기 적용 여부
            para.Add(ATFL_RPRT_FST_STA_DD);           // 최초 적용 시작일
            para.Add(ATFL_RPRT_FST_END_DD);           // 최초 적용 종료일
            para.Add(DGM_INJC_YN);                    // 정맥내 t-PA투여 여부
            para.Add(MDS_INJC_DT);                    // 투여일시
            para.Add(MDS_INJC_NEXEC_RS_CD);           // 미투여 사유
            para.Add(MDS_INJC_NEXEC_RS_ETC_TXT);      // 미투여 사유 기타 상세
            para.Add(MN60_ECS_INJC_RS_CD2);           // 병원도착 60분 초과 투여 사유
            para.Add(INARTR_THBE_EXEC_YN);            // 동맥내 혈전제거술 실시 여부
            para.Add(THBE_EXEC_DT);                   // 실시일시
            para.Add(MN120_ECS_EXEC_RS_CD);           // 병원도착 120분 초과 실시 사유
            para.Add(MN120_ECS_EXEC_RS_ETC_TXT);      // 초과 실시 사유 기타 상세
            para.Add(SBRC_HMRHG_LAST_TRET_EXEC_YN);   // 최종치료 실시 여부
            para.Add(LAST_TRET_EXEC_DT);              // 실시일시
            para.Add(HR24_ECS_LAST_TRET_EXEC_RS_CD);  // 병원도착 24시간 초과 실시 사유
            para.Add(HR24_ECS_LAST_TRET_EXEC_RS_ETC_TXT); // 초과 실시 사유 기타 상세

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
        }

        public void UpdData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            base.Upd_ASM000(p_sysdt, p_systm, p_user, p_conn, p_tran);

            List<object> para = new List<object>();

            // --- A~E 전체 단일값 메인 테이블 UPDATE ---
            string sql = "";
            sql += "UPDATE TI84_ASM003 SET ";
            sql += "ASM_HOSP_ARIV_DT=?, DSCG_YN=?, ASM_DSCG_DT=?, DSCG_STAT_RCD_YN=?, DSCG_STAT_CD=?, ACPH_DHI_RS_CD=?, ";
            sql += "ACPH_CRST_DIAG=?, ACPH_CRST_DIAG_ETC_TXT=?, ACPH_CRST_REL_CD=?, CRST_SYMT_OCUR_CFR_CD=?, SYMT_OCUR_DT=?, STAT_CFR_DT=?, ";
            sql += "LAST_NRM_CFR_DT=?, ASM_FST_IPAT_PTH_CD=?, ASM_VST_PTH_CD=?, VST_MTH_CD=?, RHBLTN_DGSBJT_TRFR_YN=?, TRFR_DD=?, WLST_RCD_YN=?, ";
            sql += "DEATH_PCS_PTNT_RCD_DT=?, BRN_DEATH_YN=?, BRN_DEATH_DD=?, CRST_SCL_ENFC_YN=?, CRST_SCL_FST_EXEC_DD=?, CRST_SCL_KND_CD=?, ";
            sql += "ETC_CRST_SCL_NM=?, ETC_CRST_SCL_HGH_PNT=?, NIHSS_PNT=?, GCS_PNT=?, ETC_CRST_SCL_ASM_PNT=?, FCLT_ASM_TL_ENFC_YN=?, ";
            sql += "LAST_FCLT_ASM_TL_ENFC_DD=?, DSCG_FCLT_ASM_TL_KND_CD=?, ETC_FCLT_ASM_TL_TXT=?, ETC_FCLT_ASM_TL_HGH_PNT=?, KMBI_PNT=?, ";
            sql += "MBI_PNT=?, BI_PNT=?, FIM_PNT=?, MRS_GRD=?, GOS_GRD=?, ETC_FCLT_ASM_PNT=?, RHBLTN_DDIAG_REQ_YN=?, FST_REQ_DD=?, ";
            sql += "RHBLTN_DDIAG_FST_RPY_YN=?, RPY_DD=?, RHBLTN_TRET_YN=?, FST_TRET_DD=?, FCLT_HDP_YN=?, CLI_ISTBY_RS_CD=?, ";
            sql += "CLI_ISTBY_RS_ETC_TXT=?, CLI_ISTBY_RS_RCD_DD=?, FCLT_HDP_ASM_YN=?, FCLT_HDP_ASM_TL_KND_CD=?, FCLT_HDP_ASM_ETC_TL_TXT=?, ";
            sql += "HDP_MRS_GRD=?, HDP_NIHSS_PNT=?, FCLT_HDP_ETC_ASM_PNT=?, FCLT_HDP_ASM_TL_EXEC_DD=?, HR48_AF_PNEM_SICK_YN=?, PNEM_KND_CD=?, ";
            sql += "PNEM_KND_ETC_TXT=?, DIAG_SICK_SYM=?, DIAG_NM=?, ATFL_RPRT_YN=?, ATFL_RPRT_FST_STA_DD=?, ATFL_RPRT_FST_END_DD=?, ";
            sql += "DGM_INJC_YN=?, MDS_INJC_DT=?, MDS_INJC_NEXEC_RS_CD=?, MDS_INJC_NEXEC_RS_ETC_TXT=?, MN60_ECS_INJC_RS_CD2=?, ";
            sql += "INARTR_THBE_EXEC_YN=?, THBE_EXEC_DT=?, MN120_ECS_EXEC_RS_CD=?, MN120_ECS_EXEC_RS_ETC_TXT=?, ";
            sql += "SBRC_HMRHG_LAST_TRET_EXEC_YN=?, LAST_TRET_EXEC_DT=?, HR24_ECS_LAST_TRET_EXEC_RS_CD=?, HR24_ECS_LAST_TRET_EXEC_RS_ETC_TXT=? ";
            sql += "WHERE FORM=? AND KEYSTR=? AND SEQ=?";

            para.Clear();
            para.Add(ASM_HOSP_ARIV_DT);               // 병원 도착일시
            para.Add(DSCG_YN);                        // 퇴원 여부
            para.Add(ASM_DSCG_DT);                    // 퇴원일시
            para.Add(DSCG_STAT_RCD_YN);               // 퇴원상태 기록 여부
            para.Add(DSCG_STAT_CD);                   // 퇴원상태
            para.Add(ACPH_DHI_RS_CD);                 // 전원사유
            para.Add(ACPH_CRST_DIAG);                 // 퇴원 시 최종 진단명
            para.Add(ACPH_CRST_DIAG_ETC_TXT);         // 퇴원 시 최종 진단명 기타 상세
            para.Add(ACPH_CRST_REL_CD);               // 급성기뇌졸중과의 관련성
            para.Add(CRST_SYMT_OCUR_CFR_CD);          // 뇌졸중 증상발생
            para.Add(SYMT_OCUR_DT);                   // 증상발생일시
            para.Add(STAT_CFR_DT);                    // 발견일시
            para.Add(LAST_NRM_CFR_DT);                // 최종정상 확인일시
            para.Add(ASM_FST_IPAT_PTH_CD);            // 내원장소
            para.Add(ASM_VST_PTH_CD);                 // 내원경로
            para.Add(VST_MTH_CD);                     // 내원방법
            para.Add(RHBLTN_DGSBJT_TRFR_YN);          // 재활의학과 전과 여부
            para.Add(TRFR_DD);                        // 전과일
            para.Add(WLST_RCD_YN);                    // 연명의료중단등결정이행서 작성여부
            para.Add(DEATH_PCS_PTNT_RCD_DT);          // 임종과정 환자 판단서 작성일자
            para.Add(BRN_DEATH_YN);                   // 뇌사판정 여부
            para.Add(BRN_DEATH_DD);                   // 뇌사판정일자
            para.Add(CRST_SCL_ENFC_YN);               // Stroke Scale 시행 여부
            para.Add(CRST_SCL_FST_EXEC_DD);           // 최초 시행일
            para.Add(CRST_SCL_KND_CD);                // Stroke Scale 종류
            para.Add(ETC_CRST_SCL_NM);                // 기타 Scale 명칭
            para.Add(ETC_CRST_SCL_HGH_PNT);           // 기타 Scale 최고점수
            para.Add(NIHSS_PNT);                      // NIHSS 평가점수
            para.Add(GCS_PNT);                        // GCS 평가점수
            para.Add(ETC_CRST_SCL_ASM_PNT);           // 기타 Scale 평가점수
            para.Add(FCLT_ASM_TL_ENFC_YN);            // 기능평가 시행 여부
            para.Add(LAST_FCLT_ASM_TL_ENFC_DD);       // 기능평가 시행일
            para.Add(DSCG_FCLT_ASM_TL_KND_CD);        // 기능평가 종류
            para.Add(ETC_FCLT_ASM_TL_TXT);            // 기타 기능평가 명칭
            para.Add(ETC_FCLT_ASM_TL_HGH_PNT);        // 기타 기능평가 최고점수
            para.Add(KMBI_PNT);                       // K-MBI 평가점수
            para.Add(MBI_PNT);                        // MBI 평가점수
            para.Add(BI_PNT);                         // BI 평가점수
            para.Add(FIM_PNT);                        // FIM 평가점수
            para.Add(MRS_GRD);                        // mRS 평가등급
            para.Add(GOS_GRD);                        // GOS 평가등급
            para.Add(ETC_FCLT_ASM_PNT);               // 기타 기능평가 점수
            para.Add(RHBLTN_DDIAG_REQ_YN);            // 재활협진 의뢰 여부
            para.Add(FST_REQ_DD);                     // 재활협진 의뢰일
            para.Add(RHBLTN_DDIAG_FST_RPY_YN);        // 재활협진 회신 여부
            para.Add(RPY_DD);                         // 재활협진 회신일
            para.Add(RHBLTN_TRET_YN);                 // 재활치료 여부
            para.Add(FST_TRET_DD);                    // 재활치료일
            para.Add(FCLT_HDP_YN);                    // 기능장해 여부
            para.Add(CLI_ISTBY_RS_CD);                // 재활협진/치료 미실시 사유
            para.Add(CLI_ISTBY_RS_ETC_TXT);           // 미실시 사유 기타 상세
            para.Add(CLI_ISTBY_RS_RCD_DD);            // 임상적 불안정 사유 등 기록 일자
            para.Add(FCLT_HDP_ASM_YN);                // 기능장해 평가 여부
            para.Add(FCLT_HDP_ASM_TL_KND_CD);         // 기능장해 평가도구
            para.Add(FCLT_HDP_ASM_ETC_TL_TXT);        // 평가도구 기타 명칭
            para.Add(HDP_MRS_GRD);                    // mRS 평가등급
            para.Add(HDP_NIHSS_PNT);                  // NIHSS 평가점수
            para.Add(FCLT_HDP_ETC_ASM_PNT);           // 기타 평가점수
            para.Add(FCLT_HDP_ASM_TL_EXEC_DD);        // 평가도구 실시일
            para.Add(HR48_AF_PNEM_SICK_YN);           // 입원 48시간 이후 폐렴 발생
            para.Add(PNEM_KND_CD);                    // 폐렴 종류
            para.Add(PNEM_KND_ETC_TXT);               // 폐렴 종류 기타 상세
            para.Add(DIAG_SICK_SYM);                  // 상병분류기호
            para.Add(DIAG_NM);                        // 진단명
            para.Add(ATFL_RPRT_YN);                   // 인공호흡기 적용 여부
            para.Add(ATFL_RPRT_FST_STA_DD);           // 최초 적용 시작일
            para.Add(ATFL_RPRT_FST_END_DD);           // 최초 적용 종료일
            para.Add(DGM_INJC_YN);                    // 정맥내 t-PA투여 여부
            para.Add(MDS_INJC_DT);                    // 투여일시
            para.Add(MDS_INJC_NEXEC_RS_CD);           // 미투여 사유
            para.Add(MDS_INJC_NEXEC_RS_ETC_TXT);      // 미투여 사유 기타 상세
            para.Add(MN60_ECS_INJC_RS_CD2);           // 병원도착 60분 초과 투여 사유
            para.Add(INARTR_THBE_EXEC_YN);            // 동맥내 혈전제거술 실시 여부
            para.Add(THBE_EXEC_DT);                   // 실시일시
            para.Add(MN120_ECS_EXEC_RS_CD);           // 병원도착 120분 초과 실시 사유
            para.Add(MN120_ECS_EXEC_RS_ETC_TXT);      // 초과 실시 사유 기타 상세
            para.Add(SBRC_HMRHG_LAST_TRET_EXEC_YN);   // 최종치료 실시 여부
            para.Add(LAST_TRET_EXEC_DT);              // 실시일시
            para.Add(HR24_ECS_LAST_TRET_EXEC_RS_CD);  // 병원도착 24시간 초과 실시 사유
            para.Add(HR24_ECS_LAST_TRET_EXEC_RS_ETC_TXT); // 초과 실시 사유 기타 상세
            para.Add(form);                           // FORM (WHERE)
            para.Add(KEYSTR);                         // KEYSTR (WHERE)
            para.Add(SEQ);                         // SEQ (WHERE)

            MetroLib.SqlHelper.ExecuteSql(sql, para, p_conn, p_tran);
        }

        public void DelAllData(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string sql = "";
            sql = "";
            sql += "DELETE FROM TI84_ASM003 WHERE FORM='" + form + "' AND KEYSTR='" + KEYSTR + "'";
            MetroLib.SqlHelper.ExecuteSql(sql, p_conn, p_tran);
        }
    }
}
