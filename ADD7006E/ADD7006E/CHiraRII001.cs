using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CHiraRII001
    {
        private string m_Hosid;
        private string m_Demno;
        private string m_Cnecno;
        private string m_Cnectdd;
        private string m_BillSno;

        public CHiraRII001(string p_hosid, string p_demno, string p_cnecno, string p_cnectdd, string p_billsno)
        {
            m_Hosid = p_hosid;
            m_Demno = p_demno;
            m_Cnecno = p_cnecno;
            m_Cnectdd = p_cnectdd;
            m_BillSno = p_billsno;
        }

        public void Send(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
            if (data.RII001_LIST.Count < 1) return;

            for (int idx = 0; idx < data.RII001_LIST.Count; idx++)
            {
                HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

                string qfycd = data.QFYCD;
                string insup_tp_cd = "";
                if (qfycd.StartsWith("29")) insup_tp_cd = "7"; // 보훈
                else if (qfycd.StartsWith("6")) insup_tp_cd = "8"; // 자보
                else if (qfycd.StartsWith("3")) insup_tp_cd = "5"; // 보호
                else insup_tp_cd = "4"; // 보험

                // 문서 공통 정보
                dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "RII001"; //서식코드: 진단검사결과지
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
                dynReq.Elements["IPAT_DT"].Value = data.RII001_LIST[idx].BEDEDTM; // 입원일시
                dynReq.Elements["IPAT_DGSBJT_CD"].Value = data.RII001_LIST[idx].INSDPTCD; // 진료과
                dynReq.Elements["IFLD_DTL_SPC_SBJT_CD"].Value = data.RII001_LIST[idx].INSDPTCD2; // 내과 세부진료과목
                dynReq.Elements["CHRG_DR_NM"].Value = data.RII001_LIST[idx].DRNM; // 담당의사 성명
                dynReq.Elements["WRTP_NM"].Value = data.RII001_LIST[idx].EMPNM; // 작성자 성명
                dynReq.Elements["WRT_DT"].Value = data.RII001_LIST[idx].SYSDTM; // 작성일시

                // B. 입원정보
                dynReq.Elements["VST_PTH_CD"].Value = data.RII001_LIST[idx].PATHCD; // 입원경로
                dynReq.Elements["VST_PTH_ETC_TXT"].Value = data.RII001_LIST[idx].PATH_DETAIL; // 입원경로상세

                // 주호소
                dynReq.Tables["TBL_CC"].Columns.Add("CC_TXT"); // 주호소
                dynReq.Tables["TBL_CC"].Columns.Add("OCUR_ERA_TXT"); // 발병시기
                dynReq.Tables["TBL_CC"].AddRow();
                dynReq.Tables["TBL_C"].Rows[0]["CC_TXT"].Value = data.RII001_LIST[idx].CC; // 주호소
                dynReq.Tables["TBL_C"].Rows[0]["OCUR_ERA_TXT"].Value = data.RII001_LIST[idx].CC_DATE; // 발병시기

                dynReq.Elements["CUR_HOC_TXT"].Value = data.RII001_LIST[idx].PI; // 현병력
                dynReq.Elements["ALRG_YN"].Value = data.RII001_LIST[idx].ALRG_YN; // 약물이상 반응여부
                dynReq.Elements["ALRG_TXT"].Value = data.RII001_LIST[idx].ALRG_TXT; // 약물이상 반응내용
                dynReq.Elements["ANMN_TXT"].Value = data.RII001_LIST[idx].PHX; // 과거력
                dynReq.Elements["MDS_DOS_YN"].Value = data.RII001_LIST[idx].MDS_DOS_YN; // 약물복용여부
                dynReq.Elements["MDS_KND_CD"].Value = data.RII001_LIST[idx].MDS_KND; // 약물종류
                dynReq.Elements["MDS_ETC_TXT"].Value = data.RII001_LIST[idx].MDS_KND_ETC; // 약물종류상세
                //dynReq.Elements["DRNK_YN"].Value = ""; // 음주여부
                //dynReq.Elements["DRNK_TXT"].Value = ""; // 음주내용
                //dynReq.Elements["SMKN_YN"].Value = ""; // 흡연여부
                //dynReq.Elements["SMKN_TXT"].Value = ""; // 흡연내용
                dynReq.Elements["FMHS_YN"].Value = data.RII001_LIST[idx].FHX_YN; // 가족력여부
                dynReq.Elements["FMHS_TXT"].Value = data.RII001_LIST[idx].FHX; // 가족력내용
                dynReq.Elements["ROS_TXT"].Value = data.RII001_LIST[idx].ROS; // 계통문진
                dynReq.Elements["PHBD_MEDEXM_TXT"].Value = data.RII001_LIST[idx].PE; // 신체검진
                dynReq.Elements["PRBM_LIST_TXT"].Value = data.RII001_LIST[idx].PRBM_LIST; // 문제목록및평가

                // 초기진단
                dynReq.Tables["TBL_EARLY_DIAG"].Columns.Add("EARLY_FDEC_DIAG_YN"); // 확진여부
                dynReq.Tables["TBL_EARLY_DIAG"].Columns.Add("EARLY_DIAG_NM"); // 진단명

                for (int i = 0; i < data.RII001_LIST[idx].PTYSQ.Count; i++)
                {
                    dynReq.Tables["TBL_EARLY_DIAG"].AddRow();
                    dynReq.Tables["TBL_EARLY_DIAG"].Rows[i]["EARLY_FDEC_DIAG_YN"].Value = data.RII001_LIST[idx].ROFG_12(i); // 확진여부
                    dynReq.Tables["TBL_EARLY_DIAG"].Rows[i]["EARLY_DIAG_NM"].Value = data.RII001_LIST[idx].DXD[i]; // 진단명
                }

                dynReq.Elements["TRET_PLAN_TXT"].Value = data.RII001_LIST[idx].TXPLAN; // 치료계획


                // requestData - createDoc    최종 제출
                //               createTmpDoc 임시 제출
                //               updateTmpDoc 수정 제출
                //               selectDocList 목록 조회
                //               selectDocLink 미리보기
                //               selectDoc     상세조회
                HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(m_Hosid, isTmp ? "createTmpDoc" : "createDoc");
                if (!dynRes.Result)
                {
                    data.RII001_RESULT.STATUS = "E"; // 오류
                    data.RII001_RESULT.ERR_CODE = dynRes.ErrorCode;
                    data.RII001_RESULT.ERR_DESC = dynRes.ErrorMessage;

                    data.RII001_RESULT.DOC_NO = ""; //문서번호
                    data.RII001_RESULT.SUPL_DATA_FOM_CD = ""; //서식코드
                    data.RII001_RESULT.RCV_NO = ""; //접수번호
                    data.RII001_RESULT.SP_SNO = ""; //명세서일련번호
                    data.RII001_RESULT.HOSP_RNO = ""; //환자등록번호
                    data.RII001_RESULT.PAT_NM = ""; //환자성명
                    data.RII001_RESULT.INSUP_TP_CD = ""; //참고업무구분

                }
                else
                {
                    data.RII001_RESULT.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                    data.RII001_RESULT.ERR_CODE = "";
                    data.RII001_RESULT.ERR_DESC = "";

                    data.RII001_RESULT.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                    data.RII001_RESULT.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                    data.RII001_RESULT.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                    data.RII001_RESULT.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                    data.RII001_RESULT.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                    data.RII001_RESULT.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                    data.RII001_RESULT.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분
                }

                // 전송내역을 저장한다.
                SaveToTI86_RII001(data, isTmp, p_sysdt, p_systm, p_conn);
            }

        }

        private void SaveToTI86_RII001(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
        }
    }
}
