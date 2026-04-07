using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CHiraRID001
    {
        private string m_Hosid;
        private string m_Demno;
        private string m_Cnecno;
        private string m_Cnectdd;
        private string m_BillSno;

        public CHiraRID001(string p_hosid, string p_demno, string p_cnecno, string p_cnectdd, string p_billsno)
        {
            m_Hosid = p_hosid;
            m_Demno = p_demno;
            m_Cnecno = p_cnecno;
            m_Cnectdd = p_cnectdd;
            m_BillSno = p_billsno;
        }

        public void Send(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
            if (data.RID001_LIST.Count < 1) return;

            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

            string qfycd = data.QFYCD;
            string insup_tp_cd = "";
            if (qfycd.StartsWith("29")) insup_tp_cd = "7"; // 보훈
            else if (qfycd.StartsWith("6")) insup_tp_cd = "8"; // 자보
            else if (qfycd.StartsWith("3")) insup_tp_cd = "5"; // 보호
            else insup_tp_cd = "4"; // 보험

            // 문서 공통 정보
            dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "RID001"; //서식코드: 진단검사결과지
            dynReq.Metadata["FOM_VER"].Value = "002"; // 서식버전
            dynReq.Metadata["YKIHO"].Value = m_Hosid; // 요양기관기호
            dynReq.Metadata["DMD_NO"].Value = m_Demno; // 청구번호
            dynReq.Metadata["RCV_NO"].Value = m_Cnecno; // 접수번호(없는 경우 0000000)
            dynReq.Metadata["RCV_YR"].Value = m_Cnectdd.Substring(0, 4); // 접수년도
            dynReq.Metadata["BILL_SNO"].Value = m_BillSno; // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)
            dynReq.Metadata["SP_SNO"].Value = data.EPRTNO; // 명일련
            dynReq.Metadata["INSUP_TP_CD"].Value = insup_tp_cd; // 보험자구분코드(4.보험 5.보호 7.보훈 8.자보)
            dynReq.Metadata["FOM_REF_BIZ_TP_CD"].Value = "01"; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)

            dynReq.Metadata["HOSP_RNO"].Value = data.PID; // 환자등록번호.병원에서 부여한 번호
            dynReq.Metadata["PAT_NM"].Value = data.PNM; // 환자명
            dynReq.Metadata["PAT_JNO"].Value = data.RESID; // 주민번호

            // A. 기본정보
            dynReq.Elements["IPAT_DT"].Value = data.RID001_LIST[0].BEDEDTM; // 입원일시
            dynReq.Elements["IPAT_DGSBJT_CD"].Value = data.RID001_LIST[0].INSDPTCD_IN; //입원과
            dynReq.Elements["IPAT_IFLD_DTL_SPC_SBJT_CD"].Value = data.RID001_LIST[0].INSDPTCD2_IN; // 입원 내과세부진료과
            dynReq.Elements["IPAT_DR_NM"].Value = data.RID001_LIST[0].DRNM_IN; // 입원의사명

            dynReq.Elements["DSCG_DT"].Value = data.RID001_LIST[0].BEDODTM; // 퇴원일시
            dynReq.Elements["DSCG_DGSBJT_CD"].Value = data.RID001_LIST[0].INSDPTCD_OUT; //퇴원과
            dynReq.Elements["DSCG_IFLD_DTL_SPC_SBJT_CD"].Value = data.RID001_LIST[0].INSDPTCD2_OUT; // 퇴원 내과세부진료과
            dynReq.Elements["DSCG_DR_NM"].Value = data.RID001_LIST[0].DRNM_OUT; // 퇴원의사명

            dynReq.Elements["WRTP_NM"].Value = data.RID001_LIST[0].EMPNM; // 작성자
            dynReq.Elements["WRT_DT"].Value = data.RID001_LIST[0].WDTM; // 작성일시

            // B.이전 입원 이력
            dynReq.Elements["DY30_WN_RE_IPAT_YN"].Value = data.RID001_LIST[0].REBED; // 30일내 재입원 여부
            dynReq.Elements["DY30_WN_RE_IPAT_TXT"].Value = data.RID001_LIST[0].REBED_REASON; // 30일내 재입원 사유
            dynReq.Elements["RE_IPAT_PLAN_YN"].Value = data.RID001_LIST[0].REBEDPLAN; // 재입원 계획 여부
            dynReq.Elements["BF_DSCG_RCGN_YN"].Value = data.RID001_LIST[0].PREOUT_CD; // 직전 퇴원일을 알고 있는지 여부
            dynReq.Elements["BF_DSCG_DD"].Value = data.RID001_LIST[0].PREOUT_DT; // 직전 퇴원일

            // C.입원경과

            // 주호소
            dynReq.Tables["TBL_CC"].Columns.Add("CC_TXT"); // 주호소
            dynReq.Tables["TBL_CC"].Columns.Add("OCUR_ERA_TXT"); // 발병시기

            for (int i = 0; i < data.RID001_LIST[0].COMPLAINT.Count; i++)
            {
                dynReq.Tables["TBL_CC"].AddRow();
                dynReq.Tables["TBL_CC"].Rows[i]["CC_TXT"].Value = data.RID001_LIST[0].COMPLAINT[i]; // 주호소
                dynReq.Tables["TBL_CC"].Rows[i]["OCUR_ERA_TXT"].Value = data.RID001_LIST[0].ERA[i]; // 발병시기
            }

            dynReq.Elements["IPAT_RS_TXT"].Value = data.RID001_LIST[0].HOPI; // 입원사유 및 현병력
            dynReq.Elements["IPAT_ELAPS_TXT"].Value = data.RID001_LIST[0].COT; // 입원경과 및 치료과정

            // 처치 및 수술
            dynReq.Tables["TBL_MOPR_SOPR"].Columns.Add("SOPR_ENFC_DT"); // 시행일시
            dynReq.Tables["TBL_MOPR_SOPR"].Columns.Add("SOPR_NM"); // 처치 및 수술명
            dynReq.Tables["TBL_MOPR_SOPR"].Columns.Add("SOPR_MED_BHV_DIV_CD"); // ICD-9-CM
            dynReq.Tables["TBL_MOPR_SOPR"].Columns.Add("SOPR_MDFEE_CD"); // 수가코드

            for (int i = 0; i < data.RID001_LIST[0].OPDT.Count; i++)
            {
                dynReq.Tables["TBL_MOPR_SOPR"].AddRow();
                dynReq.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_ENFC_DT"].Value = data.RID001_LIST[0].OPDT[i]; // 시행일시
                dynReq.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_NM"].Value = data.RID001_LIST[0].OPNAME[i]; // 처치 및 수술명
                dynReq.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_MED_BHV_DIV_CD"].Value = data.RID001_LIST[0].ICD9CM[i]; // ICD-9-CM
                dynReq.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_MDFEE_CD"].Value = "";// data.RID001_LIST[0].PRICD[i]; // 수가코드
            }

            // 검사소견
            dynReq.Tables["TBL_EXM_OPN"].Columns.Add("EXM_DT"); // 검사일시
            dynReq.Tables["TBL_EXM_OPN"].Columns.Add("EXM_NM"); // 검사명
            dynReq.Tables["TBL_EXM_OPN"].Columns.Add("EXM_RST_DT"); // 검사결과일시
            dynReq.Tables["TBL_EXM_OPN"].Columns.Add("EXM_RST_TXT"); // 검사결과
            dynReq.Tables["TBL_EXM_OPN"].Columns.Add("EXM_MDFEE_CD"); // 수가코드

            if (data.RID001_LIST[0].GUMDT.Count < 1)
            {
                dynReq.Tables["TBL_EXM_OPN"].AddRow();
                dynReq.Tables["TBL_EXM_OPN"].Rows[0]["EXM_DT"].Value = "-"; // 검사일시
                dynReq.Tables["TBL_EXM_OPN"].Rows[0]["EXM_NM"].Value = "-"; // 검사명
                dynReq.Tables["TBL_EXM_OPN"].Rows[0]["EXM_RST_DT"].Value = "-"; // 검사결과일시
                dynReq.Tables["TBL_EXM_OPN"].Rows[0]["EXM_RST_TXT"].Value = "-"; // 검사결과
                dynReq.Tables["TBL_EXM_OPN"].Rows[0]["EXM_MDFEE_CD"].Value = ""; // 수가코드
            }
            else
            {
                for (int i = 0; i < data.RID001_LIST[0].GUMDT.Count; i++)
                {
                    dynReq.Tables["TBL_EXM_OPN"].AddRow();
                    dynReq.Tables["TBL_EXM_OPN"].Rows[i]["EXM_DT"].Value = data.RID001_LIST[0].GUMDT[i]; // 검사일시
                    dynReq.Tables["TBL_EXM_OPN"].Rows[i]["EXM_NM"].Value = data.RID001_LIST[0].GUMNM[i]; // 검사명
                    dynReq.Tables["TBL_EXM_OPN"].Rows[i]["EXM_RST_DT"].Value = data.RID001_LIST[0].RSDT[i]; // 검사결과일시
                    dynReq.Tables["TBL_EXM_OPN"].Rows[i]["EXM_RST_TXT"].Value = data.RID001_LIST[0].GUMRESULT[i]; // 검사결과
                    dynReq.Tables["TBL_EXM_OPN"].Rows[i]["EXM_MDFEE_CD"].Value = ""; // 수가코드
                }
            }

            // 최종진단
            dynReq.Tables["TBL_LAST_DIAG"].Columns.Add("SICK_TP_CD"); // 상병분류구분
            dynReq.Tables["TBL_LAST_DIAG"].Columns.Add("LAST_DIAG_SICK_SYM"); // 상병분류기호
            dynReq.Tables["TBL_LAST_DIAG"].Columns.Add("LAST_DIAG_NM"); // 진단명
            dynReq.Tables["TBL_LAST_DIAG"].Columns.Add("LAST_DIAG_DGSBJT_CD"); // 진료과
            dynReq.Tables["TBL_LAST_DIAG"].Columns.Add("IFLD_DTL_SPC_SBJT_CD"); // 내과 세분진료과목
            dynReq.Tables["TBL_LAST_DIAG"].Columns.Add("IPAT_SICK_YN"); // 입원시 상병여부

            for (int i = 0; i < data.RID001_LIST[0].DXD.Count; i++)
            {
                dynReq.Tables["TBL_LAST_DIAG"].AddRow();
                dynReq.Tables["TBL_LAST_DIAG"].Rows[i]["SICK_TP_CD"].Value = data.RID001_LIST[0].ROFG[i]; // 상병분류구분
                dynReq.Tables["TBL_LAST_DIAG"].Rows[i]["LAST_DIAG_SICK_SYM"].Value = data.RID001_LIST[0].DISECD[i]; // 상병분류기호
                dynReq.Tables["TBL_LAST_DIAG"].Rows[i]["LAST_DIAG_NM"].Value = data.RID001_LIST[0].DXD[i]; // 진단명
                dynReq.Tables["TBL_LAST_DIAG"].Rows[i]["LAST_DIAG_DGSBJT_CD"].Value = data.RID001_LIST[0].INSDPTCD[i]; // 진료과
                dynReq.Tables["TBL_LAST_DIAG"].Rows[i]["IFLD_DTL_SPC_SBJT_CD"].Value = data.RID001_LIST[0].INSDPTCD2[i]; // 내과 세부진료과목
                dynReq.Tables["TBL_LAST_DIAG"].Rows[i]["IPAT_SICK_YN"].Value = data.RID001_LIST[0].POA_CD(i); // 입원시 상병여부
            }

            // 전과
            dynReq.Tables["TBL_TRFR_DESC"].Columns.Add("TRFR_DT"); // 전과일시
            dynReq.Tables["TBL_TRFR_DESC"].Columns.Add("REQ_DGSBJT_CD"); // 의뢰과
            dynReq.Tables["TBL_TRFR_DESC"].Columns.Add("REQ_IFLD_DTL_SPC_SBJT_CD"); // 의뢰과 내과세부진료과목
            dynReq.Tables["TBL_TRFR_DESC"].Columns.Add("REQ_DR_NM"); // 의뢰의사 성명
            dynReq.Tables["TBL_TRFR_DESC"].Columns.Add("RPY_DGSBJT_CD"); // 회신과
            dynReq.Tables["TBL_TRFR_DESC"].Columns.Add("RPY_IFLD_DTL_SPC_SBJT_CD"); // 회신과 내과세부진료과목
            dynReq.Tables["TBL_TRFR_DESC"].Columns.Add("RPY_DR_NM"); // 회신의사
            dynReq.Tables["TBL_TRFR_DESC"].Columns.Add("TRFR_RS_TXT"); // 전과사유

            if (data.RID001_LIST[0].TR_DATE.Count < 1)
            {
                dynReq.Tables["TBL_TRFR_DESC"].AddRow();
                dynReq.Tables["TBL_TRFR_DESC"].Rows[0]["TRFR_DT"].Value = "-"; // 전과일시
                dynReq.Tables["TBL_TRFR_DESC"].Rows[0]["REQ_DGSBJT_CD"].Value = "-"; // 의뢰과
                dynReq.Tables["TBL_TRFR_DESC"].Rows[0]["REQ_IFLD_DTL_SPC_SBJT_CD"].Value = "-"; // 의뢰과 내과세부진료과목
                dynReq.Tables["TBL_TRFR_DESC"].Rows[0]["REQ_DR_NM"].Value = "-"; // 의뢰의사 성명
                dynReq.Tables["TBL_TRFR_DESC"].Rows[0]["RPY_DGSBJT_CD"].Value = "-"; // 회신과
                dynReq.Tables["TBL_TRFR_DESC"].Rows[0]["RPY_IFLD_DTL_SPC_SBJT_CD"].Value = "-"; // 회신과 내과세부진료과목
                dynReq.Tables["TBL_TRFR_DESC"].Rows[0]["RPY_DR_NM"].Value = "-"; // 회신의사
                dynReq.Tables["TBL_TRFR_DESC"].Rows[0]["TRFR_RS_TXT"].Value = ""; // 전과사유
            }
            else
            {
                for (int i = 0; i < data.RID001_LIST[0].TR_DATE.Count; i++)
                {
                    dynReq.Tables["TBL_TRFR_DESC"].AddRow();
                    dynReq.Tables["TBL_TRFR_DESC"].Rows[i]["TRFR_DT"].Value = data.RID001_LIST[0].TR_DATE[i]; // 전과일시
                    dynReq.Tables["TBL_TRFR_DESC"].Rows[i]["REQ_DGSBJT_CD"].Value = data.RID001_LIST[0].TR_OUT_INSDPTCD[i]; // 의뢰과
                    dynReq.Tables["TBL_TRFR_DESC"].Rows[i]["REQ_IFLD_DTL_SPC_SBJT_CD"].Value = data.RID001_LIST[0].TR_OUT_INSDPTCD2[i]; // 의뢰과 내과세부진료과목
                    dynReq.Tables["TBL_TRFR_DESC"].Rows[i]["REQ_DR_NM"].Value = data.RID001_LIST[0].TR_OUT_DRNM[i]; // 의뢰의사 성명
                    dynReq.Tables["TBL_TRFR_DESC"].Rows[i]["RPY_DGSBJT_CD"].Value = data.RID001_LIST[0].TR_IN_INSDPTCD[i]; // 회신과
                    dynReq.Tables["TBL_TRFR_DESC"].Rows[i]["RPY_IFLD_DTL_SPC_SBJT_CD"].Value = data.RID001_LIST[0].TR_IN_INSDPTCD2[i]; // 회신과 내과세부진료과목
                    dynReq.Tables["TBL_TRFR_DESC"].Rows[i]["RPY_DR_NM"].Value = data.RID001_LIST[0].TR_IN_DRNM[i]; // 회신의사
                    dynReq.Tables["TBL_TRFR_DESC"].Rows[i]["TRFR_RS_TXT"].Value = data.RID001_LIST[0].TR_OUT_REASON[i]; // 전과사유
                }
            }

            dynReq.Elements["ALRG_YN"].Value = data.RID001_LIST[0].ALLERGY_CD; // 약물이상반응여부
            dynReq.Elements["ALRG_TXT"].Value = data.RID001_LIST[0].ALLERGY_DESC; // 약물이상반응내용

            // 환자 상태 척도
            dynReq.Tables["TBL_PTNT_STAT_SCL"].Columns.Add("MASR_ERA_CD"); // 측정시기
            dynReq.Tables["TBL_PTNT_STAT_SCL"].Columns.Add("MASR_ERA_ETC_TXT"); // 측정시기상세
            dynReq.Tables["TBL_PTNT_STAT_SCL"].Columns.Add("MASR_TL_NM"); // 도구
            dynReq.Tables["TBL_PTNT_STAT_SCL"].Columns.Add("MASR_RST_TXT"); // 결과
            dynReq.Tables["TBL_PTNT_STAT_SCL"].Columns.Add("RMK_TXT"); // 참고사항

            for (int i = 0; i < data.RID001_LIST[0].ERA_CD.Count; i++)
            {
                dynReq.Tables["TBL_TRFR_DESC"].AddRow();
                dynReq.Tables["TBL_TRFR_DESC"].Rows[i]["MASR_ERA_CD"].Value = data.RID001_LIST[0].ERA_CD[i]; // 측정시기
                dynReq.Tables["TBL_TRFR_DESC"].Rows[i]["MASR_ERA_ETC_TXT"].Value = data.RID001_LIST[0].ERA_ETC_TXT[i]; // 측정시기상세
                dynReq.Tables["TBL_TRFR_DESC"].Rows[i]["MASR_TL_NM"].Value = data.RID001_LIST[0].TL_NM[i]; // 도구
                dynReq.Tables["TBL_TRFR_DESC"].Rows[i]["MASR_RST_TXT"].Value = data.RID001_LIST[0].RST_TXT[i]; // 결과
                dynReq.Tables["TBL_TRFR_DESC"].Rows[i]["RMK_TXT"].Value = data.RID001_LIST[0].RMK_TXT[i]; // 참고사항
            }

            dynReq.Elements["INFC_YN"].Value = data.RID001_LIST[0].HEPA_CD; // 감염 발생여부
            dynReq.Elements["INFC_TXT"].Value = data.RID001_LIST[0].HEPA_DESC; // 감염 상세내용
            dynReq.Elements["CPCT_YN"].Value = data.RID001_LIST[0].COMPLICATION_CD; // 합병증 발생여부
            dynReq.Elements["CPCT_TXT"].Value = data.RID001_LIST[0].COMPLICATION_DESC; // 합병증 상세내용
            dynReq.Elements["PTNT_SAF_MGMT_YN"].Value = data.RID001_LIST[0].PTNT_YN; // 환자안전관리 특이사항 발생여부
            dynReq.Elements["PTNT_SAF_MGMT_TXT"].Value = data.RID001_LIST[0].PTNT_TXT; // 환자안전관리 특이사항 상세내용

            // D.퇴원현황

            dynReq.Elements["DSCG_FRM_CD"].Value = data.RID001_LIST[0].OUTREASON_CD; // 퇴원형태
            dynReq.Elements["DSCG_FRM_ETC_TXT"].Value = data.RID001_LIST[0].OUTREASON_DESC; // 퇴원형태 기타상세
            dynReq.Elements["DSCG_PTNT_STAT_CD"].Value = data.RID001_LIST[0].OUTSTATUS_CD; // 퇴원시 환자상태
            dynReq.Elements["DSCG_PTNT_STAT_ETC_TXT"].Value = data.RID001_LIST[0].OUTSTATUS_DESC; // 퇴원시 환자상태 기타상세
            dynReq.Elements["DEATH_CD"].Value = data.RID001_LIST[0].DEATHDTM; // 사망일시
            dynReq.Elements["DEATH_SICK_SYM"].Value = data.RID001_LIST[0].DEATH_SICK; // 원사인
            dynReq.Elements["DEATH_DIAG_SYM"].Value = data.RID001_LIST[0].DEATH_DIAG; // 진단명
            dynReq.Elements["DHI_RS_TXT"].Value = data.RID001_LIST[0].DHI_RS_TXT; // 전원사유
            dynReq.Elements["DSCG_AF_DIAG_PLAN_CD"].Value = data.RID001_LIST[0].OUTCARE_CD; // 퇴원후 진료계획
            dynReq.Elements["DSCG_AF_DIAG_PLAN_TXT"].Value = data.RID001_LIST[0].OUTCARE_DESC; // 퇴원후 진료계획 상세

            // E.퇴원처방
            dynReq.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_TP_CD"); // 처방 구분
            dynReq.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_MDS_NM"); // 약품명
            dynReq.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_MDS_CD"); // 약품코드
            dynReq.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_USAG_TXT"); // 용법
            dynReq.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_FQ1_MDCT_QTY"); // 1회 투약량
            dynReq.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_DY1_INJC_FQ"); // 1일 투여횟수
            dynReq.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_TOT_INJC_DDCNT"); // 총 투약일수

            for (int i = 0; i < data.RID001_LIST[0].ORDER_TYPE.Count; i++)
            {
                dynReq.Tables["TBL_DSCG_PRSC"].AddRow();
                dynReq.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_TP_CD"].Value = data.RID001_LIST[0].ORDER_TYPE[i]; // 처방 구분
                dynReq.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_MDS_NM"].Value = data.RID001_LIST[0].ONM[i]; // 약품명
                dynReq.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_MDS_CD"].Value = ""; // 약품코드
                dynReq.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_USAG_TXT"].Value = data.RID001_LIST[0].OUNIT[i]; // 용법
                dynReq.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_FQ1_MDCT_QTY"].Value = data.RID001_LIST[0].OQTY[i]; // 1회 투약량
                dynReq.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_DY1_INJC_FQ"].Value = data.RID001_LIST[0].ORDCNT[i]; // 1일 투여횟수
                dynReq.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_TOT_INJC_DDCNT"].Value = data.RID001_LIST[0].ODAYCNT[i]; // 총 투약일수
            }

            // F.추가정보

            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(m_Hosid, isTmp ? "createTmpDoc" : "createDoc");
            if (!dynRes.Result)
            {
                data.RID001_RESULT.STATUS = "E"; // 오류
                data.RID001_RESULT.ERR_CODE = dynRes.ErrorCode;
                data.RID001_RESULT.ERR_DESC = dynRes.ErrorMessage;

                data.RID001_RESULT.DOC_NO = ""; //문서번호
                data.RID001_RESULT.SUPL_DATA_FOM_CD = ""; //서식코드
                data.RID001_RESULT.RCV_NO = ""; //접수번호
                data.RID001_RESULT.SP_SNO = ""; //명세서일련번호
                data.RID001_RESULT.HOSP_RNO = ""; //환자등록번호
                data.RID001_RESULT.PAT_NM = ""; //환자성명
                data.RID001_RESULT.INSUP_TP_CD = ""; //참고업무구분

            }
            else
            {
                data.RID001_RESULT.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.RID001_RESULT.ERR_CODE = "";
                data.RID001_RESULT.ERR_DESC = "";

                data.RID001_RESULT.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                data.RID001_RESULT.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                data.RID001_RESULT.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                data.RID001_RESULT.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                data.RID001_RESULT.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                data.RID001_RESULT.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                data.RID001_RESULT.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분
            }

            // 전송내역을 저장한다.
            SaveToTI86_RID001(data, isTmp, p_sysdt, p_systm, p_conn);

        }

        private void SaveToTI86_RID001(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
        }
    }
}
