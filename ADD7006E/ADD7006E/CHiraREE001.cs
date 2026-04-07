using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CHiraREE001
    {
        private string m_Hosid;
        private string m_Demno;
        private string m_Cnecno;
        private string m_Cnectdd;
        private string m_BillSno;

        public CHiraREE001(string p_hosid, string p_demno, string p_cnecno, string p_cnectdd, string p_billsno)
        {
            m_Hosid = p_hosid;
            m_Demno = p_demno;
            m_Cnecno = p_cnecno;
            m_Cnectdd = p_cnectdd;
            m_BillSno = p_billsno;
        }

        public void Send(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
            if (data.REE001_LIST.Count < 1) return;

            for (int idx = 0; idx < data.REE001_LIST.Count; idx++)
            {
                HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

                string qfycd = data.QFYCD;
                string insup_tp_cd = "";
                if (qfycd.StartsWith("29")) insup_tp_cd = "7"; // 보훈
                else if (qfycd.StartsWith("6")) insup_tp_cd = "8"; // 자보
                else if (qfycd.StartsWith("3")) insup_tp_cd = "5"; // 보호
                else insup_tp_cd = "4"; // 보험

                // 문서 공통 정보
                dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "REE001"; //서식코드: 진단검사결과지
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
                dynReq.Elements["EMRRM_IPAT_DT"].Value = data.REE001_LIST[idx].PTMIINDTM; // 응급실도착일시
                dynReq.Elements["EMRRM_DSCG_DT"].Value = data.REE001_LIST[idx].PTMIOTDTM; // 응급실퇴실일시

                // KTAS 중증도
                dynReq.Tables["TBL_KTAS_GRD"].Columns.Add("KTAS_GRD_DIV_DT"); // 응급중증도 분류일시
                dynReq.Tables["TBL_KTAS_GRD"].Columns.Add("KTAS_GRD_TXT"); // 응급중증도 등급

                dynReq.Tables["TBL_KTAS_GRD"].AddRow();
                dynReq.Tables["TBL_KTAS_GRD"].Rows[0]["KTAS_GRD_DIV_DT"].Value = data.REE001_LIST[idx].PTMIKTDTM; // 응급중증도 분류일시
                dynReq.Tables["TBL_KTAS_GRD"].Rows[0]["KTAS_GRD_TXT"].Value = data.REE001_LIST[idx].PTMIKTS1; // 응급중증도 등급

                dynReq.Elements["DHI_YN"].Value = data.REE001_LIST[idx].DHI_YN; // 전원여부
                dynReq.Elements["OIST_TRET_TXT"].Value = "-"; // 타병원 진료 내용(없음)

                // B.기초 사정 정보

                // C.응급실 경과

                // 주호소
                dynReq.Tables["TBL_CC"].Columns.Add("CC_TXT"); // 주호소
                dynReq.Tables["TBL_CC"].AddRow();
                dynReq.Tables["TBL_C"].Rows[0]["CC_TXT"].Value = data.REE001_LIST[idx].MJ_HOSO; // 주호소
                dynReq.Tables["TBL_C"].Rows[0]["OCUR_ERA_TXT"].Value = data.REE001_LIST[idx].ONSET; // 발병시기

                dynReq.Elements["CUR_HOC_TXT"].Value = data.REE001_LIST[idx].PI; // 현병력
                dynReq.Elements["ALRG_YN"].Value = data.REE001_LIST[idx].ALRG_YN; // 약물이상 반응여부
                dynReq.Elements["ALRG_TXT"].Value = data.REE001_LIST[idx].ALRG_TXT; // 약물이상 반응내용
                dynReq.Elements["ANMN_TXT"].Value = data.REE001_LIST[idx].PHX; // 과거력
                dynReq.Elements["MDS_DOS_YN"].Value = data.REE001_LIST[idx].MDS_DOS_YN; // 약물복용여부
                dynReq.Elements["MDS_KND_CD"].Value = data.REE001_LIST[idx].MDS_KND; // 약물종류
                dynReq.Elements["MDS_ETC_TXT"].Value = data.REE001_LIST[idx].MDS_KND_ETC; // 약물종류상세
                dynReq.Elements["DRNK_YN"].Value = data.REE001_LIST[idx].DRNK_YN; // 음주여부
                dynReq.Elements["DRNK_TXT"].Value = data.REE001_LIST[idx].DRNK_TXT; // 음주내용
                dynReq.Elements["SMKN_YN"].Value = data.REE001_LIST[idx].SMKN_YN; // 흡연여부
                dynReq.Elements["SMKN_TXT"].Value = data.REE001_LIST[idx].SMKN_TXT; // 흡연내용
                dynReq.Elements["FMHS_YN"].Value = data.REE001_LIST[idx].FHX_YN; // 가족력여부
                dynReq.Elements["FMHS_TXT"].Value = data.REE001_LIST[idx].FHX; // 가족력내용
                dynReq.Elements["ROS_TXT"].Value = data.REE001_LIST[idx].ROS; // 계통문진
                dynReq.Elements["PHBD_MEDEXM_TXT"].Value = data.REE001_LIST[idx].PE; // 신체검진

                // 진료내역
                dynReq.Tables["TBL_SDR_DIAG"].Columns.Add("SDR_DIAG_DT"); // 진료일시
                dynReq.Tables["TBL_SDR_DIAG"].Columns.Add("SDR_DGSBJT_CD"); // 진료과
                dynReq.Tables["TBL_SDR_DIAG"].Columns.Add("IFLD_DTL_SPC_SBJT_CD"); // 내과세부진료과
                dynReq.Tables["TBL_SDR_DIAG"].Columns.Add("SDR_NM"); // 진료의성명
                dynReq.Tables["TBL_SDR_DIAG"].Columns.Add("SDR_LCS_NO"); // 진료의면허번호
                dynReq.Tables["TBL_SDR_DIAG"].Columns.Add("WRTP_NM"); // 작성자성명
                dynReq.Tables["TBL_SDR_DIAG"].Columns.Add("WRT_DT"); // 작성자일시
                dynReq.Tables["TBL_SDR_DIAG"].Columns.Add("PRBM_LIST_TXT"); // 문제목록 및 평가
                dynReq.Tables["TBL_SDR_DIAG"].Columns.Add("TRET_PLAN_TXT"); // 치료계획

                if (data.REE001_LIST[idx].EXDT.Count < 1)
                {
                    dynReq.Tables["TBL_SDR_DIAG"].AddRow();
                    dynReq.Tables["TBL_SDR_DIAG"].Rows[0]["SDR_DIAG_DT"].Value = "-"; // 진료일시
                    dynReq.Tables["TBL_SDR_DIAG"].Rows[0]["SDR_DGSBJT_CD"].Value = "-"; // 진료과
                    dynReq.Tables["TBL_SDR_DIAG"].Rows[0]["IFLD_DTL_SPC_SBJT_CD"].Value = "-"; // 내과세부진료과
                    dynReq.Tables["TBL_SDR_DIAG"].Rows[0]["SDR_NM"].Value = "-"; // 진료의성명
                    //dynReq.Tables["TBL_SDR_DIAG"].Rows[0]["SDR_LCS_NO"].Value = "-"; // 진료의면허번호
                    dynReq.Tables["TBL_SDR_DIAG"].Rows[0]["WRTP_NM"].Value = "-"; // 작성자성명
                    dynReq.Tables["TBL_SDR_DIAG"].Rows[0]["WRT_DT"].Value = "-"; // 작성자일시
                    dynReq.Tables["TBL_SDR_DIAG"].Rows[0]["PRBM_LIST_TXT"].Value = "-"; // 문제목록 및 평가
                    dynReq.Tables["TBL_SDR_DIAG"].Rows[0]["TRET_PLAN_TXT"].Value = "-"; // 치료계획
                }
                else
                {
                    for (int i = 0; i < data.REE001_LIST[idx].EXDT.Count; i++)
                    {
                        dynReq.Tables["TBL_SDR_DIAG"].AddRow();
                        dynReq.Tables["TBL_SDR_DIAG"].Rows[i]["SDR_DIAG_DT"].Value = data.REE001_LIST[idx].EXDTM(i); // 진료일시
                        dynReq.Tables["TBL_SDR_DIAG"].Rows[i]["SDR_DGSBJT_CD"].Value = data.REE001_LIST[idx].INSDPTCD[i]; // 진료과
                        dynReq.Tables["TBL_SDR_DIAG"].Rows[i]["IFLD_DTL_SPC_SBJT_CD"].Value = data.REE001_LIST[idx].INSDPTCD2[i]; // 내과세부진료과
                        dynReq.Tables["TBL_SDR_DIAG"].Rows[i]["SDR_NM"].Value = data.REE001_LIST[idx].USERNM[i]; // 진료의성명
                        //dynReq.Tables["TBL_SDR_DIAG"].Rows[i]["SDR_LCS_NO"].Value = data.REE001_LIST[idx].GDRLCID[i]; // 진료의면허번호
                        dynReq.Tables["TBL_SDR_DIAG"].Rows[i]["WRTP_NM"].Value = data.REE001_LIST[idx].USERNM[i]; // 작성자성명
                        dynReq.Tables["TBL_SDR_DIAG"].Rows[i]["WRT_DT"].Value = data.REE001_LIST[idx].WRTDTM(i); // 작성자일시
                        dynReq.Tables["TBL_SDR_DIAG"].Rows[i]["PRBM_LIST_TXT"].Value = data.REE001_LIST[idx].PRBM_TXT(i); // 문제목록 및 평가
                        dynReq.Tables["TBL_SDR_DIAG"].Rows[i]["TRET_PLAN_TXT"].Value = data.REE001_LIST[idx].PLAN_TXT(i); // 치료계획
                    }
                }

                // 진단
                dynReq.Tables["TBL_DIAG"].Columns.Add("FDEC_DIAG_YN"); // 확진여부
                dynReq.Tables["TBL_DIAG"].Columns.Add("DIAG_NM"); // 진단명

                for (int i = 0; i < data.REE001_LIST[idx].PTYSQ.Count; i++)
                {
                    dynReq.Tables["TBL_DIAG"].AddRow();
                    dynReq.Tables["TBL_DIAG"].Rows[i]["FDEC_DIAG_YN"].Value = data.REE001_LIST[idx].ROFG_12(i); // 확진여부
                    dynReq.Tables["TBL_DIAG"].Rows[i]["DIAG_NM"].Value = data.REE001_LIST[idx].DXD[i]; // 진단명
                }

                // 시실.처치 및 수술
                dynReq.Tables["TBL_MOPR_SOPR"].Columns.Add("SOPR_ENFC_DT"); // 시행일시
                dynReq.Tables["TBL_MOPR_SOPR"].Columns.Add("SOPR_NM"); // 명칭

                for (int i = 0; i < data.REE001_LIST[idx].ODT.Count; i++)
                {
                    dynReq.Tables["TBL_MOPR_SOPR"].AddRow();
                    dynReq.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_ENFC_DT"].Value = data.REE001_LIST[idx].ODTM(i); // 시행일시
                    dynReq.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_NM"].Value = data.REE001_LIST[idx].ONM[i]; // 명칭
                }

                dynReq.Elements["EMY_DSCG_FRM_CD"].Value = data.REE001_LIST[idx].PTMIEMRT_NM; // 퇴실형태
                dynReq.Elements["DSCG_FRM_TXT"].Value = ""; // 퇴실형태상세
                dynReq.Elements["DEATH_DT"].Value = data.REE001_LIST[idx].DEATHDTM; // 사망일시
                dynReq.Elements["DEATH_SICK_SYM"].Value = data.REE001_LIST[idx].DEATH_SICK_SYM; // 원사인 상병분류기호
                dynReq.Elements["DEATH_DIAG_NM"].Value = data.REE001_LIST[idx].DEATH_DIAG_NM; // 진단명



                // requestData - createDoc    최종 제출
                //               createTmpDoc 임시 제출
                //               updateTmpDoc 수정 제출
                //               selectDocList 목록 조회
                //               selectDocLink 미리보기
                //               selectDoc     상세조회
                HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(m_Hosid, isTmp ? "createTmpDoc" : "createDoc");
                if (!dynRes.Result)
                {
                    data.REE001_RESULT.STATUS = "E"; // 오류
                    data.REE001_RESULT.ERR_CODE = dynRes.ErrorCode;
                    data.REE001_RESULT.ERR_DESC = dynRes.ErrorMessage;

                    data.REE001_RESULT.DOC_NO = ""; //문서번호
                    data.REE001_RESULT.SUPL_DATA_FOM_CD = ""; //서식코드
                    data.REE001_RESULT.RCV_NO = ""; //접수번호
                    data.REE001_RESULT.SP_SNO = ""; //명세서일련번호
                    data.REE001_RESULT.HOSP_RNO = ""; //환자등록번호
                    data.REE001_RESULT.PAT_NM = ""; //환자성명
                    data.REE001_RESULT.INSUP_TP_CD = ""; //참고업무구분

                }
                else
                {
                    data.REE001_RESULT.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                    data.REE001_RESULT.ERR_CODE = "";
                    data.REE001_RESULT.ERR_DESC = "";

                    data.REE001_RESULT.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                    data.REE001_RESULT.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                    data.REE001_RESULT.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                    data.REE001_RESULT.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                    data.REE001_RESULT.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                    data.REE001_RESULT.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                    data.REE001_RESULT.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분
                }

                // 전송내역을 저장한다.
                SaveToTI86_REE001(data, isTmp, p_sysdt, p_systm, p_conn);
            }

        }

        private void SaveToTI86_REE001(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
        }
    }
}
