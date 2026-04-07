using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HIRA.EformEntry;
using HIRA.EformEntry.Model;
using HIRA.EformEntry.ResponseModel;

using System.Windows.Forms;

namespace ADD7000E
{
    class CRID001
    {
        public HIRA.EformEntry.Model.Document GetDocument(CDataTI2A i2a, CHosInfo p_HosInfo)
        {
            CDataTV100 v100 = new CDataTV100();
            v100.Clear();
            v100.SetData(i2a);

            // 퇴원요약지
            HIRA.EformEntry.Model.Document doc = new HIRA.EformEntry.Model.Document();

            if (v100.VER == "002")
            {
                // Metadata
                doc.Metadata["SUPL_DATA_FOM_CD"].Value = "RID001"; // 서식코드
                doc.Metadata["FOM_VER"].Value = "002"; // 서식버전
                doc.Metadata["YKIHO"].Value = p_HosInfo.GetHosId(); // 요양기관기호

                doc.Metadata["DMD_NO"].Value = i2a.DEMNO; // 청구번호
                doc.Metadata["RCV_NO"].Value = i2a.CNECNO; // 접수번호. 접수번호가 없는 경우 0000000
                doc.Metadata["RCV_YR"].Value = i2a.RCV_YR; // 접수년도 CCYY
                doc.Metadata["BILL_SNO"].Value = i2a.DCOUNT; // 청구서일련번호(접수전이면 0, 원청구는 1, 보완청구는 심결통보서에 있는 번호)
                doc.Metadata["SP_SNO"].Value = i2a.EPRTNO; // 명세서 일련번호
                doc.Metadata["INSUP_TP_CD"].Value = i2a.INSUP_TP_CD; // 보험자구분코드 (4:건강보험 5:의료급여 7:보훈 8:지동차보험)
                doc.Metadata["FOM_REF_BIZ_TP_CD"].Value = "06"; // 참고업무구분코드 (01:1차심사 02:심사보완 03:이의신청 04:평가 05:진료비민원 06:신포괄 99:기타)
                doc.Metadata["DTL_BIZ_CD"].Value = "NDR"; // 업무상세코드 참고업무의 상세 업무구분이 있는 경우(참고업부구분코드가 '04:평가'인 경우)필수기재(상세코드는 업무별로 별도 안내 받은 코드를 기재)
                doc.Metadata["REQ_DATA_NO"].Value = ""; // 요청번호 참고업무구분코드가 '05:진료비민원','09:진료의뢰회송'인 경우 필수기재(요청번호는 요청자료 목록조회 API 또는 웹포털 화면을 통해 조회가능)
                doc.Metadata["RE_RV_RCV_ID"].Value = ""; // 재심사접수ID 참고업무구분코드가 '03:이의신청'인 경우 필수개재(재심사접수ID를 요청자료 목록조회 API 또는 웹포털 화면을 통해 조회가능)

                doc.Metadata["HOSP_RNO"].Value = i2a.PID; // 의료기관에서 부여한 환자등록번호
                doc.Metadata["PAT_NM"].Value = i2a.PNM; // 수진자 성명
                doc.Metadata["PAT_JNO"].Value = i2a.RESID; // 수진자 주민등록번호 ("-" 생략)

                // A. 기본정보
                doc.Elements["IPAT_DT"].Value = v100.BEDEDTHM; // 입원일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리
                doc.Elements["IPAT_DGSBJT_CD"].Value = v100.INSDPTCD_IN; // 입원과
                doc.Elements["IPAT_IFLD_DTL_SPC_SBJT_CD"].Value = v100.INSDPTCD2_IN; // 입원 내과세부과목
                doc.Elements["IPAT_DR_NM"].Value = v100.DRNM_IN; // 입원의사 성명
                doc.Elements["DSCG_DT"].Value = v100.BEDODTHM; // 퇴원일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리
                doc.Elements["DSCG_DGSBJT_CD"].Value = v100.INSDPTCD_OUT; // 퇴원과
                doc.Elements["DSCG_IFLD_DTL_SPC_SBJT_CD"].Value = v100.INSDPTCD2_OUT; // 퇴원 내과세부과목
                doc.Elements["DSCG_DR_NM"].Value = v100.DRNM_OUT; // 퇴원의사 성명
                doc.Elements["WRTP_NM"].Value = v100.EMPNM; // 작성자 성명. 퇴원요약지를 작성한 의사 성명
                doc.Elements["WRT_DT"].Value = v100.WDTTM; // 작성일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리

                // B. 이전 입원 이력
                doc.Elements["DY30_WN_RE_IPAT_YN"].Value = v100.REBED; // 30일내 재입원 여부 1.Yes 2.No 3.모름
                doc.Elements["DY30_WN_RE_IPAT_TXT"].Value = v100.REBED_REASON; // 30일내 재입원 사유
                doc.Elements["RE_IPAT_PLAN_YN"].Value = v100.REBEDPLAN_CD; // 재입원 계획 여부 1.계획에 있는 재입원 2.계획에 없는 재입원
                doc.Elements["BF_DSCG_RCGN_YN"].Value = v100.PREOUT_CD; // 직전퇴원일아는지여부(1.알고있음 2.모름)
                doc.Elements["BF_DSCG_DD"].Value = v100.PREOUT_DT; // 직전퇴원일

                // C. 입원경과
                doc.Tables["TBL_CC"].Columns.Add("CC_TXT"); // 주호소
                doc.Tables["TBL_CC"].Columns.Add("OCUR_ERA_TXT"); // 주호소 발병시기
                for (int i = 0; i < v100.CC_COUNT; i++)
                {
                    if (i >= 99) break; // 최대 99개
                    doc.Tables["TBL_CC"].AddRow();
                    doc.Tables["TBL_CC"].Rows[i]["CC_TXT"].Value = v100.CC_COMPLAINT[i]; // 주호소
                    doc.Tables["TBL_CC"].Rows[i]["OCUR_ERA_TXT"].Value = v100.CC_ERA[i]; // 발병시기
                }
                doc.Elements["IPAT_RS_TXT"].Value = v100.HOPI; // 입원사유 및 현병력
                doc.Elements["IPAT_ELAPS_TXT"].Value = v100.COT; // 입원경과 및 치료과정 요약

                // 시술.처치 및 수술(1~99)
                doc.Tables["TBL_MOPR_SOPR"].Columns.Add("SOPR_ENFC_DT"); // 시행일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리
                doc.Tables["TBL_MOPR_SOPR"].Columns.Add("SOPR_NM"); // 시술.처치 및 수술명
                doc.Tables["TBL_MOPR_SOPR"].Columns.Add("SOPR_MED_BHV_DIV_CD"); // ICD-9-CM volumn3
                doc.Tables["TBL_MOPR_SOPR"].Columns.Add("SOPR_MDFEE_CD"); // 수가코드. 반드시 대문자로
                if (v100.OPCOUNT <= 0)
                {
                    // 시술.처치 및 수술내역이 없는 경우 ‘-’ 값 입력
                    int i = 0;
                    doc.Tables["TBL_MOPR_SOPR"].AddRow();
                    doc.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_ENFC_DT"].Value = "-"; // 시행일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리
                    doc.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_NM"].Value = "-"; // 시술.처치 및 수술명
                    doc.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_MED_BHV_DIV_CD"].Value = "-"; // ICD-9-CM volumn3
                    doc.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_MDFEE_CD"].Value = "-"; // 수가코드. 반드시 대문자로
                }
                else
                {
                    for (int i = 0; i < v100.OPCOUNT; i++)
                    {
                        if (i >= 99) break; // 최대 99개
                        doc.Tables["TBL_MOPR_SOPR"].AddRow();
                        doc.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_ENFC_DT"].Value = v100.OPDT[i]; // 시행일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리
                        doc.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_NM"].Value = v100.OPNAME[i]; // 시술.처치 및 수술명
                        doc.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_MED_BHV_DIV_CD"].Value = v100.ICD9CM[i]; // ICD-9-CM volumn3
                        doc.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_MDFEE_CD"].Value = v100.PRICD[i]; // 수가코드. 반드시 대문자로
                    }
                }

                // 검사소견(1~99)
                doc.Tables["TBL_EXM_OPN"].Columns.Add("EXM_DT");// 검사일시 CCYYMMDDHHMM 시간이 관리되지 않으면 CCYYMMDD0000
                doc.Tables["TBL_EXM_OPN"].Columns.Add("EXM_RST_DT");// 검사결과일시 CCYYMMDDHHMM
                doc.Tables["TBL_EXM_OPN"].Columns.Add("EXM_NM");// 검사명
                doc.Tables["TBL_EXM_OPN"].Columns.Add("EXM_MDFEE_CD");//수가코드
                doc.Tables["TBL_EXM_OPN"].Columns.Add("EXM_RST_TXT");//검사결과
                if (v100.GUM_COUNT <= 0)
                {
                    // 검사내역이 없는 경우 ‘-’ 값 입력
                    int i = 0;
                    doc.Tables["TBL_EXM_OPN"].AddRow();
                    doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_DT"].Value = "-"; // 검사일시 CCYYMMDDHHMM 시간이 관리되지 않으면 CCYYMMDD0000
                    doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_RST_DT"].Value = "-"; // 검사결과일시 CCYYMMDDHHMM
                    doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_NM"].Value = "-"; // 검사명
                    doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_MDFEE_CD"].Value = "-"; // 수가코드 *** 병원에서 제외시킴 ***
                    doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_RST_TXT"].Value = "-"; //검사결과
                }
                else
                {
                    for (int i = 0; i < v100.GUM_COUNT; i++)
                    {
                        doc.Tables["TBL_EXM_OPN"].AddRow();
                        doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_DT"].Value = v100.GUM_GUMDT[i]; // 검사일시 CCYYMMDDHHMM 시간이 관리되지 않으면 CCYYMMDD0000
                        doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_RST_DT"].Value = v100.GUM_RSDT[i]; // 검사결과일시 CCYYMMDDHHMM
                        doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_NM"].Value = v100.GUM_ONM[i]; // 검사명
                        doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_MDFEE_CD"].Value = ""; // 수가코드 *** 병원에서 제외시킴 ***
                        doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_RST_TXT"].Value = v100.GUM_GUMRESULT[i]; //검사결과
                    }
                }

                // 최종진단(1~99)
                if (v100.DX_COUNT > 0)
                {
                    doc.Tables["TBL_LAST_DIAG"].Columns.Add("SICK_TP_CD"); // 확진여부 1.확진 2.의증
                    doc.Tables["TBL_LAST_DIAG"].Columns.Add("LAST_DIAG_SICK_SYM"); // 상병분류기호
                    doc.Tables["TBL_LAST_DIAG"].Columns.Add("LAST_DIAG_NM"); // 진단명
                    doc.Tables["TBL_LAST_DIAG"].Columns.Add("LAST_DIAG_DGSBJT_CD"); // 진료과
                    doc.Tables["TBL_LAST_DIAG"].Columns.Add("IFLD_DTL_SPC_SBJT_CD"); // 내과 세부전문과목
                    doc.Tables["TBL_LAST_DIAG"].Columns.Add("IPAT_SICK_YN"); // 입원시 상병여부

                    for (int i = 0; i < v100.DX_COUNT; i++)
                    {
                        if (i >= 99) break; // 최대 99개

                        String strSickTpCd = "";
                        if (i == 0)
                        {
                            strSickTpCd = "1";
                        }
                        else if (v100.DX_ROFG[i] == "Y")
                        {
                            strSickTpCd = "3";
                        }
                        else
                        {
                            strSickTpCd = "2";
                        }

                        doc.Tables["TBL_LAST_DIAG"].AddRow();
                        doc.Tables["TBL_LAST_DIAG"].Rows[i]["SICK_TP_CD"].Value = strSickTpCd; // 상병분류기호 1.주상병 2.부상병 3.배제상병
                        doc.Tables["TBL_LAST_DIAG"].Rows[i]["LAST_DIAG_SICK_SYM"].Value = v100.DX_DISECD[i]; // 상병분류기호
                        doc.Tables["TBL_LAST_DIAG"].Rows[i]["LAST_DIAG_NM"].Value = v100.DX_DXD[i]; // 진단명
                        doc.Tables["TBL_LAST_DIAG"].Rows[i]["LAST_DIAG_DGSBJT_CD"].Value = v100.DX_INSDPTCD[i]; // 진료과
                        doc.Tables["TBL_LAST_DIAG"].Rows[i]["IFLD_DTL_SPC_SBJT_CD"].Value = v100.DX_INSDPTCD2[i]; // 내과 세부전문과목
                        doc.Tables["TBL_LAST_DIAG"].Rows[i]["IPAT_SICK_YN"].Value = v100.DX_POA[i]; // 입원시 상병여부 
                    }
                }

                // 전과현황(1~99)
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("TRFR_DT"); // 전과일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("REQ_DGSBJT_CD"); // 의뢰과
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("REQ_IFLD_DTL_SPC_SBJT_CD"); // 의뢰과 내과 세부진료과목
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("REQ_DR_NM"); // 의뢰의사성명
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("RPY_DGSBJT_CD"); // 회신과
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("RPY_IFLD_DTL_SPC_SBJT_CD"); // 회신과 내과 세부진료과목
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("RPY_DR_NM"); // 회신의사성명
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("TRFR_RS_TXT"); // 전과의뢰사유. 평문기재
                if (v100.TRCOUNT <= 0)
                {
                    // 전과내역이 없는 경우 ‘-’ 값 입력
                    int i = 0;
                    doc.Tables["TBL_TRFR_DESC"].AddRow();
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["TRFR_DT"].Value = "-"; // 전과일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["REQ_DGSBJT_CD"].Value = "-"; // 의뢰과
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["REQ_IFLD_DTL_SPC_SBJT_CD"].Value = ""; // 의뢰과 내과 세부진료과목
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["REQ_DR_NM"].Value = "-"; // 의뢰의사성명
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["RPY_DGSBJT_CD"].Value = "-"; // 회신과
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["RPY_IFLD_DTL_SPC_SBJT_CD"].Value = ""; // 회신과 내과 세부진료과목
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["RPY_DR_NM"].Value = "-"; // 회신의사성명
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["TRFR_RS_TXT"].Value = "-"; // 전과의뢰사유. 평문기재
                }
                else
                {
                    for (int i = 0; i < v100.TRCOUNT; i++)
                    {
                        if (i >= 99) break; // 최대 99개
                        doc.Tables["TBL_TRFR_DESC"].AddRow();
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["TRFR_DT"].Value = v100.TRDATE[i]; // 전과일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["REQ_DGSBJT_CD"].Value = v100.TROUTDPT[i]; // 의뢰과
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["REQ_IFLD_DTL_SPC_SBJT_CD"].Value = v100.TROUTDPT2[i]; // 의뢰과 내과 세부진료과목
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["REQ_DR_NM"].Value = v100.TROUTDRNM[i]; // 의뢰의사성명
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["RPY_DGSBJT_CD"].Value = v100.TRINDPT[i]; // 회신과
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["RPY_IFLD_DTL_SPC_SBJT_CD"].Value = v100.TRINDPT2[i]; // 회신과 내과 세부진료과목
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["RPY_DR_NM"].Value = v100.TRINDRNM[i]; // 회신의사성명
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["TRFR_RS_TXT"].Value = v100.TROUTREASON[i]; // 전과의뢰사유. 평문기재
                    }
                }

                // 약물 이상반응
                doc.Elements["ALRG_YN"].Value = v100.ALLERGY_CD; // 약 알러지 여부(1.유 2.무 3.확인불가)
                doc.Elements["ALRG_TXT"].Value = v100.ALLERGY_DESC; // 알러지내용. 평문기재

                // 환자 상태척도(1~99)
                doc.Tables["TBL_PTNT_STAT_SCL"].Columns.Add("MASR_ERA_CD"); // 측정시기(1.입원당시 2.입원중 3.퇴원시 9.기타)
                if (i2a.HDATE.CompareTo("20230116") < 0)
                {
                    doc.Tables["TBL_PTNT_STAT_SCL"].Columns.Add("MASR_ERA_ETC_TXT"); // 측정시기 기타 상세(평문)
                }
                else
                {
                    doc.Tables["TBL_PTNT_STAT_SCL"].Columns.Add("MASR_ERA_ETC_DT"); // 측정시기 기타 상세(yyyymmddhhmm 형식)
                }
                doc.Tables["TBL_PTNT_STAT_SCL"].Columns.Add("MASR_TL_NM"); // 도구
                doc.Tables["TBL_PTNT_STAT_SCL"].Columns.Add("MASR_RST_TXT"); // 결과
                doc.Tables["TBL_PTNT_STAT_SCL"].Columns.Add("RMK_TXT"); // 참고사항
                for (int i = 0; i < v100.MASR_COUNT; i++)
                {
                    if (i >= 99) break; // 최대 99개
                    doc.Tables["TBL_PTNT_STAT_SCL"].AddRow();
                    doc.Tables["TBL_PTNT_STAT_SCL"].Rows[i]["MASR_ERA_CD"].Value = v100.MASR_ERA_CD[i]; // 측정시기
                    if (i2a.HDATE.CompareTo("20230116") < 0)
                    {
                        doc.Tables["TBL_PTNT_STAT_SCL"].Rows[i]["MASR_ERA_ETC_TXT"].Value = v100.MASR_ERA_ETC_TXT[i]; // 측정시기 기타 상세(평문)
                    }
                    else
                    {
                        doc.Tables["TBL_PTNT_STAT_SCL"].Rows[i]["MASR_ERA_ETC_DT"].Value = v100.MASR_ERA_ETC_DT[i]; // 측정시기 기타 상세(yyyymmddhhmm 형식)
                    }
                    doc.Tables["TBL_PTNT_STAT_SCL"].Rows[i]["MASR_TL_NM"].Value = v100.MASR_TL_NM[i]; // 도구
                    doc.Tables["TBL_PTNT_STAT_SCL"].Rows[i]["MASR_RST_TXT"].Value = v100.MASR_RST_TXT[i]; // 결과
                    doc.Tables["TBL_PTNT_STAT_SCL"].Rows[i]["RMK_TXT"].Value = v100.MASR_RMK_TXT[i]; // 참고사항
                }

                // 감염 및 합병증
                doc.Elements["INFC_YN"].Value = v100.HEPA_CD; // 감염 여부 1.유 2.무 3.확인불가
                doc.Elements["INFC_TXT"].Value = v100.HEPA_DESC; // 감염 내용
                doc.Elements["CPCT_YN"].Value = v100.COMPLICATION_CD; // 합병증 여부 1.유 2.무 3.확인불가
                doc.Elements["CPCT_TXT"].Value = v100.COMPLICATION_DESC; // 합병증 내용
                // 환자안전관리 특이사상
                doc.Elements["PTNT_SAF_MGMT_YN"].Value = v100.PTNT_YN; // 발생여부 1.Yes 2.No 3.확인불가
                doc.Elements["PTNT_SAF_MGMT_TXT"].Value = v100.PTNT_TXT; // 내용

                // D. 퇴원 현황
                // 퇴원 형태 및 환자상태
                doc.Elements["DSCG_FRM_CD"].Value = v100.OUTREASON_CD; // 퇴원형태
                doc.Elements["DSCG_FRM_ETC_TXT"].Value = v100.OUTREASON_DESC; // 퇴원형태가 기타인 경우 상세기재
                doc.Elements["DSCG_PTNT_STAT_CD"].Value = v100.OUTSTATUS_CD; // 퇴원시 환자상태
                doc.Elements["DSCG_PTNT_STAT_ETC_TXT"].Value = v100.OUTSTATUS_DESC; // 퇴원시 환자상태가 기타인 경우 상세기재

                // 사망현황
                doc.Elements["DEATH_DT"].Value = v100.DEATHDTTM; // 사망일시
                doc.Elements["DEATH_SICK_SYM"].Value = v100.DEDX_DISECD[0]; // 원사인 상병분류기호
                doc.Elements["DEATH_DIAG_NM"].Value = v100.DEDX_DXD[0]; // 진단명

                // 전원사유
                doc.Elements["DHI_RS_TXT"].Value = v100.TRHOSREASON_TXT; // 전원사유

                // 퇴원 후 진료계획
                doc.Elements["DSCG_AF_DIAG_PLAN_CD"].Value = v100.OUTCARE_COD; // 퇴원 후 진료 계획
                doc.Elements["DSCG_AF_DIAG_PLAN_TXT"].Value = v100.OUTCARE_TXT; // 퇴원 후 진료 계획 상세

                // E. 퇴원 처방(1~99)
                doc.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_TP_CD"); // 처방구분(0.해당없음 1.원내 2.원외)
                doc.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_MDS_NM"); // 약품명
                doc.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_MDS_CD"); // 약품코드
                doc.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_USAG_TXT"); // 용법
                doc.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_FQ1_MDCT_QTY"); // 1회투약량
                doc.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_DY1_INJC_FQ"); // 1일투여횟수
                doc.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_TOT_INJC_DDCNT"); // 총 투약일수
                if (v100.DCORCOUNT <= 0)
                {
                    int i = 0;
                    doc.Tables["TBL_DSCG_PRSC"].AddRow();
                    doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_TP_CD"].Value = "0"; // 처방구분(0.해당없음 1.원내 2.원외)
                    doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_MDS_NM"].Value = ""; // 약품명
                    doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_MDS_CD"].Value = ""; // 약품코드
                    doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_USAG_TXT"].Value = ""; // 용법
                    doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_FQ1_MDCT_QTY"].Value = ""; // 1회투약량
                    doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_DY1_INJC_FQ"].Value = ""; // 1일투여횟수
                    doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_TOT_INJC_DDCNT"].Value = ""; // 총 투약일수
                }
                else
                {
                    for (int i = 0; i < v100.DCORCOUNT; i++)
                    {
                        if (i >= 99) break; // 최대 99개
                        doc.Tables["TBL_DSCG_PRSC"].AddRow();
                        doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_TP_CD"].Value = v100.ORDER_TYPE[i]; // 처방구분(0.해당없음 1.원내 2.원외)
                        doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_MDS_NM"].Value = v100.ONM[i]; // 약품명
                        doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_MDS_CD"].Value = ""; // 약품코드
                        doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_USAG_TXT"].Value = v100.OUNIT[i]; // 용법
                        doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_FQ1_MDCT_QTY"].Value = v100.OQTY[i]; // 1회투약량
                        doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_DY1_INJC_FQ"].Value = v100.ORDCNT[i]; // 1일투여횟수
                        doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_TOT_INJC_DDCNT"].Value = v100.ODAYCNT[i]; // 총 투약일수
                    }
                }

            }
            else
            {

                // Metadata
                doc.Metadata["SUPL_DATA_FOM_CD"].Value = "RID001"; // 서식코드
                doc.Metadata["FOM_VER"].Value = "001"; // 서식버전
                doc.Metadata["YKIHO"].Value = p_HosInfo.GetHosId(); // 요양기관기호

                doc.Metadata["DMD_NO"].Value = i2a.DEMNO; // 청구번호
                doc.Metadata["RCV_NO"].Value = i2a.CNECNO; // 접수번호. 접수번호가 없는 경우 0000000
                doc.Metadata["RCV_YR"].Value = i2a.RCV_YR; // 접수년도 CCYY
                doc.Metadata["BILL_SNO"].Value = i2a.DCOUNT; // 청구서일련번호(접수전이면 0, 원청구는 1, 보완청구는 심결통보서에 있는 번호)
                doc.Metadata["SP_SNO"].Value = i2a.EPRTNO; // 명세서 일련번호
                doc.Metadata["INSUP_TP_CD"].Value = i2a.INSUP_TP_CD; // 보험자구분코드 (4:건강보험 5:의료급여 7:보훈 8:지동차보험)
                doc.Metadata["FOM_REF_BIZ_TP_CD"].Value = "06"; // 참고업무구분코드 (01:1차심사 02:심사보완 03:이의신청 04:평가 05:진료비민원 06:신포괄 99:기타)
                doc.Metadata["HOSP_RNO"].Value = i2a.PID; // 의료기관에서 부여한 환자등록번호
                doc.Metadata["PAT_NM"].Value = i2a.PNM; // 수진자 성명
                doc.Metadata["PAT_JNO"].Value = i2a.RESID; // 수진자 주민등록번호 ("-" 생략)

                // A. 기본정보
                doc.Elements["IPAT_DT"].Value = v100.BEDEDTHM; // 입원일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리
                doc.Elements["IPAT_DGSBJT_CD"].Value = v100.INSDPTCD_IN; // 입원과
                doc.Elements["IPAT_DR_NM"].Value = v100.DRNM_IN; // 입원의사 성명
                doc.Elements["IPAT_DR_LCS_KND_CD"].Value = v100.DRTYPE_IN; // 입원의사 면허종류(1.의사 2.치과의사 3.한의사)
                doc.Elements["IPAT_DR_LCS_NO"].Value = v100.GDRLCID_IN; // 입원의사 면허번호
                doc.Elements["DSCG_DT"].Value = v100.BEDODTHM; // 퇴원일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리
                doc.Elements["DSCG_DGSBJT_CD"].Value = v100.INSDPTCD_OUT; // 퇴원과
                doc.Elements["DSCG_DR_NM"].Value = v100.DRNM_OUT; // 퇴원의사 성명
                doc.Elements["DSCG_DR_LCS_KND_CD"].Value = v100.DRTYPE_OUT; // 퇴원의사 면허종류
                doc.Elements["DSCG_DR_LCS_NO"].Value = v100.GDRLCID_OUT; // 퇴원의사 면허번호
                doc.Elements["WRTP_NM"].Value = v100.EMPNM; // 작성자 성명. 퇴원요약지를 작성한 의사 성명
                doc.Elements["WRT_DT"].Value = v100.WDTTM; // 작성일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리

                // B. 이전 입원 이력
                doc.Elements["DY30_WN_RE_IPAT_YN"].Value = v100.REBED; // 30일내 재입원 여부 1.Yes 2.No 3.모름
                doc.Elements["RE_IPAT_PLAN_YN"].Value = v100.REBEDPLAN_CD; // 재입원 계획 여부 1.계획에 있는 재입원 2.계획에 없는 재입원
                doc.Elements["BF_DSCG_RCGN_YN"].Value = v100.PREOUT_CD; // 직전퇴원일아는지여부(1.알고있음 2.모름)
                doc.Elements["BF_DSCG_DD"].Value = v100.PREOUT_DT; // 직전퇴원일

                // C. 입원경과
                doc.Elements["CC_TXT"].Value = v100.COMPLAINT; // 주호소
                doc.Elements["IPAT_RS_TXT"].Value = v100.HOPI; // 입원사유 및 현병력
                doc.Elements["IPAT_ELAPS_TXT"].Value = v100.COT; // 입원경과 및 치료과정 요약

                // 전과현황(1~99)
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("TRFR_DT"); // 전과일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("REQ_DGSBJT_CD"); // 의뢰과
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("REQ_DR_NM"); // 의뢰의사성명
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("REQ_DR_LCS_KND_CD"); // 의뢰의사 면허종류
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("REQ_DR_LCS_NO"); // 의뢰의사 면허번호
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("TRFR_RS_TXT"); // 전과의뢰사유. 평문기재
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("RPY_DGSBJT_CD"); // 회신과
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("RPY_DR_NM"); // 회신의사성명
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("RPY_DR_LCS_KND_CD"); // 회신의사 면허종류
                doc.Tables["TBL_TRFR_DESC"].Columns.Add("RPY_DR_LCS_NO"); // 회신의사 면허번호
                if (v100.TRCOUNT <= 0)
                {
                    // 전과내역이 없는 경우 ‘-’ 값 입력
                    int i = 0;
                    doc.Tables["TBL_TRFR_DESC"].AddRow();
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["TRFR_DT"].Value = "-"; // 전과일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["REQ_DGSBJT_CD"].Value = "-"; // 의뢰과
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["REQ_DR_NM"].Value = "-"; // 의뢰의사성명
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["REQ_DR_LCS_KND_CD"].Value = "-"; // 의뢰의사 면허종류
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["REQ_DR_LCS_NO"].Value = "-"; // 의뢰의사 면허번호
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["TRFR_RS_TXT"].Value = "-"; // 전과의뢰사유. 평문기재
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["RPY_DGSBJT_CD"].Value = "-"; // 회신과
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["RPY_DR_NM"].Value = "-"; // 회신의사성명
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["RPY_DR_LCS_KND_CD"].Value = "-"; // 회신의사 면허종류
                    doc.Tables["TBL_TRFR_DESC"].Rows[i]["RPY_DR_LCS_NO"].Value = "-"; // 회신의사 면허번호
                }
                else
                {
                    for (int i = 0; i < v100.TRCOUNT; i++)
                    {
                        if (i >= 99) break; // 최대 99개
                        doc.Tables["TBL_TRFR_DESC"].AddRow();
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["TRFR_DT"].Value = v100.TRDATE[i]; // 전과일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["REQ_DGSBJT_CD"].Value = v100.TROUTDPT[i]; // 의뢰과
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["REQ_DR_NM"].Value = v100.TROUTDRNM[i]; // 의뢰의사성명
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["REQ_DR_LCS_KND_CD"].Value = v100.TROUTDR[i]; // 의뢰의사 면허종류
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["REQ_DR_LCS_NO"].Value = v100.TROUTDRLCID[i]; // 의뢰의사 면허번호
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["TRFR_RS_TXT"].Value = v100.TROUTREASON[i]; // 전과의뢰사유. 평문기재
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["RPY_DGSBJT_CD"].Value = v100.TRINDPT[i]; // 회신과
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["RPY_DR_NM"].Value = v100.TRINDRNM[i]; // 회신의사성명
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["RPY_DR_LCS_KND_CD"].Value = v100.TRINDR[i]; // 회신의사 면허종류
                        doc.Tables["TBL_TRFR_DESC"].Rows[i]["RPY_DR_LCS_NO"].Value = v100.TRINDRLCID[i]; // 회신의사 면허번호
                    }
                }

                // 시술.처치 및 수술(1~99)
                doc.Tables["TBL_MOPR_SOPR"].Columns.Add("SOPR_ENFC_DT"); // 시행일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리
                doc.Tables["TBL_MOPR_SOPR"].Columns.Add("SOPR_NM"); // 시술.처치 및 수술명
                doc.Tables["TBL_MOPR_SOPR"].Columns.Add("SOPR_MED_BHV_DIV_CD"); // ICD-9-CM volumn3
                doc.Tables["TBL_MOPR_SOPR"].Columns.Add("SOPR_MDFEE_CD"); // 수가코드. 반드시 대문자로
                if (v100.OPCOUNT <= 0)
                {
                    // 시술.처치 및 수술내역이 없는 경우 ‘-’ 값 입력
                    int i = 0;
                    doc.Tables["TBL_MOPR_SOPR"].AddRow();
                    doc.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_ENFC_DT"].Value = "-"; // 시행일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리
                    doc.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_NM"].Value = "-"; // 시술.처치 및 수술명
                    doc.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_MED_BHV_DIV_CD"].Value = "-"; // ICD-9-CM volumn3
                    doc.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_MDFEE_CD"].Value = "-"; // 수가코드. 반드시 대문자로
                }
                else
                {
                    for (int i = 0; i < v100.OPCOUNT; i++)
                    {
                        if (i >= 99) break; // 최대 99개
                        doc.Tables["TBL_MOPR_SOPR"].AddRow();
                        doc.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_ENFC_DT"].Value = v100.OPDT[i]; // 시행일시. 시간이 관리되지 않으면 CCYYMMDD0000으로 처리
                        doc.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_NM"].Value = v100.OPNAME[i]; // 시술.처치 및 수술명
                        doc.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_MED_BHV_DIV_CD"].Value = v100.ICD9CM[i]; // ICD-9-CM volumn3
                        doc.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_MDFEE_CD"].Value = v100.PRICD[i]; // 수가코드. 반드시 대문자로
                    }
                }

                // 검사소견(1~99)
                doc.Tables["TBL_EXM_OPN"].Columns.Add("EXM_DT");// 검사일시 CCYYMMDDHHMM 시간이 관리되지 않으면 CCYYMMDD0000
                doc.Tables["TBL_EXM_OPN"].Columns.Add("EXM_RST_DT");// 검사결과일시 CCYYMMDDHHMM
                doc.Tables["TBL_EXM_OPN"].Columns.Add("EXM_NM");// 검사명
                doc.Tables["TBL_EXM_OPN"].Columns.Add("EXM_MDFEE_CD");//수가코드
                doc.Tables["TBL_EXM_OPN"].Columns.Add("EXM_RST_TXT");//검사결과
                if (v100.GUM_COUNT <= 0)
                {
                    // 검사내역이 없는 경우 ‘-’ 값 입력
                    int i = 0;
                    doc.Tables["TBL_EXM_OPN"].AddRow();
                    doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_DT"].Value = "-"; // 검사일시 CCYYMMDDHHMM 시간이 관리되지 않으면 CCYYMMDD0000
                    doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_RST_DT"].Value = "-"; // 검사결과일시 CCYYMMDDHHMM
                    doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_NM"].Value = "-"; // 검사명
                    doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_MDFEE_CD"].Value = "-"; // 수가코드 *** 병원에서 제외시킴 ***
                    doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_RST_TXT"].Value = "-"; //검사결과
                }
                else
                {
                    for (int i = 0; i < v100.GUM_COUNT; i++)
                    {
                        doc.Tables["TBL_EXM_OPN"].AddRow();
                        doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_DT"].Value = v100.GUM_GUMDT[i]; // 검사일시 CCYYMMDDHHMM 시간이 관리되지 않으면 CCYYMMDD0000
                        doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_RST_DT"].Value = v100.GUM_RSDT[i]; // 검사결과일시 CCYYMMDDHHMM
                        doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_NM"].Value = v100.GUM_ONM[i]; // 검사명
                        doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_MDFEE_CD"].Value = ""; // 수가코드 *** 병원에서 제외시킴 ***
                        doc.Tables["TBL_EXM_OPN"].Rows[i]["EXM_RST_TXT"].Value = v100.GUM_GUMRESULT[i]; //검사결과
                    }
                }

                // 약 알러지
                doc.Elements["ALRG_YN"].Value = v100.ALLERGY_CD; // 약 알러지 여부(1.유 2.무 3.확인불가)
                doc.Elements["ALRG_TXT"].Value = v100.ALLERGY_DESC; // 알러지내용. 평문기재

                // 의약품명칭(1~99)
                int cntDrugName = v100.CountDRUGNAME;
                if (cntDrugName > 0)
                {
                    doc.Tables["TBL_ALRG_MDS"].Columns.Add("ALRG_MDS_NM"); //약품명
                    doc.Tables["TBL_ALRG_MDS"].Columns.Add("ALRG_MDS_CD"); //약품코드

                    for (int i = 0; i < cntDrugName; i++)
                    {
                        if (i >= 99) break; // 최대 99개
                        doc.Tables["TBL_ALRG_MDS"].AddRow();
                        doc.Tables["TBL_ALRG_MDS"].Rows[i]["ALRG_MDS_NM"].Value = v100.GetDRUGNAME(i); // 약품명
                        doc.Tables["TBL_ALRG_MDS"].Rows[i]["ALRG_MDS_CD"].Value = ""; // 약품코드 *** 병원에서 제외시킴 ***

                    }
                }

                // 감염 및 합병증
                doc.Elements["INFC_YN"].Value = v100.HEPA_CD; // 감염 여부 1.유 2.무 3.확인불가
                doc.Elements["INFC_TXT"].Value = v100.HEPA_DESC; // 감염 내용
                doc.Elements["CPCT_YN"].Value = v100.COMPLICATION_CD; // 합병증 여부 1.유 2.무 3.확인불가
                doc.Elements["CPCT_TXT"].Value = v100.COMPLICATION_DESC; // 합병증 내용

                // 최종진단(1~99)
                if (v100.DX_COUNT > 0)
                {
                    doc.Tables["TBL_LAST_DIAG"].Columns.Add("FDEC_DIAG_YN"); // 확진여부 1.확진 2.의증
                    doc.Tables["TBL_LAST_DIAG"].Columns.Add("LAST_DIAG_SICK_SYM"); // 상병분류기호
                    doc.Tables["TBL_LAST_DIAG"].Columns.Add("LAST_DIAG_NM"); // 진단명
                    doc.Tables["TBL_LAST_DIAG"].Columns.Add("LAST_DIAG_DGSBJT_CD"); // 진료과
                    doc.Tables["TBL_LAST_DIAG"].Columns.Add("IPAT_SICK_YN"); // 입원시 상병여부

                    for (int i = 0; i < v100.DX_COUNT; i++)
                    {
                        if (i >= 99) break; // 최대 99개
                        doc.Tables["TBL_LAST_DIAG"].AddRow();
                        doc.Tables["TBL_LAST_DIAG"].Rows[i]["FDEC_DIAG_YN"].Value = v100.DX_ROFG[i]; // 확진여부 1.확진 2.의증
                        doc.Tables["TBL_LAST_DIAG"].Rows[i]["LAST_DIAG_SICK_SYM"].Value = v100.DX_DISECD[i]; // 상병분류기호
                        doc.Tables["TBL_LAST_DIAG"].Rows[i]["LAST_DIAG_NM"].Value = v100.DX_DXD[i]; // 진단명
                        doc.Tables["TBL_LAST_DIAG"].Rows[i]["LAST_DIAG_DGSBJT_CD"].Value = v100.DX_INSDPTCD[i]; // 진료과
                        doc.Tables["TBL_LAST_DIAG"].Rows[i]["IPAT_SICK_YN"].Value = v100.DX_POA[i]; // 입원시 상병여부 
                    }
                }

                // D. 퇴원 현황
                // 퇴원 형태 및 환자상태
                doc.Elements["DSCG_FRM_CD"].Value = v100.OUTREASON_CD; // 퇴원형태
                doc.Elements["DSCG_FRM_ETC_TXT"].Value = v100.OUTREASON_DESC; // 퇴원형태가 기타인 경우 상세기재
                doc.Elements["DSCG_PTNT_STAT_CD"].Value = v100.OUTSTATUS_CD; // 퇴원시 환자상태
                doc.Elements["DSCG_PTNT_STAT_ETC_TXT"].Value = v100.OUTSTATUS_DESC; // 퇴원시 환자상태가 기타인 경우 상세기재

                // 사망현황
                doc.Elements["DEATH_DT"].Value = v100.DEATHDTTM; // 사망일시
                doc.Elements["DEATH_SICK_SYM"].Value = v100.DEDX_DISECD[0]; // 원사인 상병분류기호
                doc.Elements["DEATH_DIAG_NM"].Value = v100.DEDX_DXD[0]; // 진단명

                // 전원현황
                doc.Elements["DHI_DMND_CD"].Value = v100.TRHOS; // 전원요구 1.환자 2.보호자 3기타 4.미해당
                doc.Elements["DHI_RS_CD"].Value = v100.TRHOSREASON; // 전원사유
                doc.Elements["CNTN_TRET_TXT"].Value = v100.CONTICARE; // 연속적 치료사항
                doc.Elements["DHI_YKIHO"].Value = v100.TRHOSNUM; // 전원 보낸 기관기호
                doc.Elements["DHI_YADM_NM"].Value = v100.TRHOSNAME; // 전원 보낸 기관명
                doc.Elements["DHI_YADM_DGSBJT_CD"].Value = v100.TRHOSDPT; // 전원 보낸 진료과

                // 퇴원 후 진료계획
                doc.Elements["DSCG_AF_DIAG_PLAN_CD"].Value = v100.OUTCARE_CD; // 퇴원 후 진료 계획
                doc.Elements["OPAT_BKN_DGSBJT_CD"].Value = v100.OUTCARE_DPTCD; // 외래 예약 진료과
                doc.Elements["OPAT_BKN_DIAG_DD_TXT"].Value = v100.OUTCARE_DT; // 외래 예약 진료일

                // E. 퇴원 처방(1~99)
                doc.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_MDS_NM"); // 약품명
                doc.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_MDS_CD"); // 약품코드
                doc.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_USAG_TXT"); // 용법
                doc.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_FQ1_MDCT_QTY"); // 1회투약량
                doc.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_DY1_INJC_FQ"); // 1일투여횟수
                doc.Tables["TBL_DSCG_PRSC"].Columns.Add("PRSC_TOT_INJC_DDCNT"); // 총 투약일수
                if (v100.DCORCOUNT <= 0)
                {
                    int i = 0;
                    doc.Tables["TBL_DSCG_PRSC"].AddRow();
                    doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_MDS_NM"].Value = "-"; // 약품명
                    doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_MDS_CD"].Value = "-"; // 약품코드
                    doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_USAG_TXT"].Value = "-"; // 용법
                    doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_FQ1_MDCT_QTY"].Value = "0.0"; // 1회투약량
                    doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_DY1_INJC_FQ"].Value = "0"; // 1일투여횟수
                    doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_TOT_INJC_DDCNT"].Value = "0"; // 총 투약일수
                }
                else
                {
                    for (int i = 0; i < v100.DCORCOUNT; i++)
                    {
                        if (i >= 99) break; // 최대 99개
                        doc.Tables["TBL_DSCG_PRSC"].AddRow();
                        doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_MDS_NM"].Value = v100.ONM[i]; // 약품명
                        doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_MDS_CD"].Value = ""; // 약품코드
                        doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_USAG_TXT"].Value = v100.OUNIT[i]; // 용법
                        doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_FQ1_MDCT_QTY"].Value = v100.OQTY[i]; // 1회투약량
                        doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_DY1_INJC_FQ"].Value = v100.ORDCNT[i]; // 1일투여횟수
                        doc.Tables["TBL_DSCG_PRSC"].Rows[i]["PRSC_TOT_INJC_DDCNT"].Value = v100.ODAYCNT[i]; // 총 투약일수
                    }
                }

                // F.기타 정보
                // 전화번호
                doc.Elements["TELNO"].Value = v100.TELNO; // 연락처

                // 조직학적 진단명
                doc.Elements["TXTR_DIAG_CD"].Value = v100.NEWDACD; // 조직적진단코드
                doc.Elements["TXTR_DIAG_NM"].Value = v100.NEWDXD; // 조직적진단명

                // Functional Outcom scale
                doc.Tables["TBL_FCLT_ASM"].Columns.Add("FCLT_ASM_TL_ENFC_DD"); // 시행일 해당 기능평가도구를 시행한 일자를 기재
                doc.Tables["TBL_FCLT_ASM"].Columns.Add("FCLT_ASM_TL_KND_CD"); // 기능평가도구종류
                doc.Tables["TBL_FCLT_ASM"].Columns.Add("FCLT_ASM_TL_ETC_TXT"); // 기능평가도구종류가타상세 종류가 9.기타인 경우 Scale값 기재
                doc.Tables["TBL_FCLT_ASM"].Columns.Add("FCLT_ASM_TL_ETC_HGH_PNT"); // 기능평가도구종류기타최고점수 종류가 9.가타인 경우 최고점 입력
                doc.Tables["TBL_FCLT_ASM"].Columns.Add("FCLT_ASM_PNT"); // 평가점수
                doc.Tables["TBL_FCLT_ASM"].Columns.Add("FCLT_ASER_CD"); // 평가자 1.의사 2.간호사 3.물리+작업치료사 9.기타
                doc.Tables["TBL_FCLT_ASM"].Columns.Add("FCLT_ASER_ETC_TXT"); // 평가자가 9.기타인 경우 기재

                doc.Tables["TBL_FCLT_ASM"].AddRow();
                doc.Tables["TBL_FCLT_ASM"].Rows[0]["FCLT_ASM_TL_ENFC_DD"].Value = ""; // 시행일 해당 기능평가도구를 시행한 일자를 기재
                doc.Tables["TBL_FCLT_ASM"].Rows[0]["FCLT_ASM_TL_KND_CD"].Value = v100.OUTFOS_KIND; // 기능평가도구종류
                doc.Tables["TBL_FCLT_ASM"].Rows[0]["FCLT_ASM_TL_ETC_TXT"].Value = v100.OUTFOS_SCALE; // 종류가 9.기타 인 경우 Scale 값
                doc.Tables["TBL_FCLT_ASM"].Rows[0]["FCLT_ASM_TL_ETC_HGH_PNT"].Value = v100.OUTFOS_TOP; // 기능평가도구종류기타최고점수 조유가 9.가타인 경우 최고점 입력
                doc.Tables["TBL_FCLT_ASM"].Rows[0]["FCLT_ASM_PNT"].Value = v100.SCORE; // 종류가 9.기타 인 경우 최고점
                doc.Tables["TBL_FCLT_ASM"].Rows[0]["FCLT_ASER_CD"].Value = v100.ASSPERSON_CD; // 평가점수
                doc.Tables["TBL_FCLT_ASM"].Rows[0]["FCLT_ASER_ETC_TXT"].Value = v100.ASSPERSON_ETC; // 평가자가 9.기타인 경우 기재
            }

            doc.addDoc();
            return doc;
        }
    }
}
