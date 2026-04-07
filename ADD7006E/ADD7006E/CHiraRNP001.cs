using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CHiraRNP001
    {
        private string m_Hosid;
        private string m_Demno;
        private string m_Cnecno;
        private string m_Cnectdd;
        private string m_BillSno;

        public CHiraRNP001(string p_hosid, string p_demno, string p_cnecno, string p_cnectdd, string p_billsno)
        {
            m_Hosid = p_hosid;
            m_Demno = p_demno;
            m_Cnecno = p_cnecno;
            m_Cnectdd = p_cnectdd;
            m_BillSno = p_billsno;
        }

        public void Send(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
            if (data.RNP001_LIST.Count < 1) return;

            for (int idx = 0; idx < data.RNP001_LIST.Count; idx++)
            {
                HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

                string qfycd = data.QFYCD;
                string insup_tp_cd = "";
                if (qfycd.StartsWith("29")) insup_tp_cd = "7"; // 보훈
                else if (qfycd.StartsWith("6")) insup_tp_cd = "8"; // 자보
                else if (qfycd.StartsWith("3")) insup_tp_cd = "5"; // 보호
                else insup_tp_cd = "4"; // 보험

                // 문서 공통 정보
                dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "RNP001"; //서식코드: 중환자실기록자료
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
                dynReq.Elements["IPAT_DT"].Value = data.RNP001_LIST[idx].IPAT_DT; // 입원일시
                dynReq.Elements["IPAT_DGSBJT_CD"].Value = data.RNP001_LIST[idx].IPAT_DGSBJT_CD; // 진료과목
                dynReq.Elements["IFLD_DTL_SPC_SBJT_CDT"].Value = data.RNP001_LIST[idx].IFLD_DTL_SPC_SBJT_CD; // 내과 세부진료과목
                dynReq.Elements["WRTP_NM"].Value = data.RNP001_LIST[idx].WRTP_NM; // 작성자성명
                dynReq.Elements["WRT_DT"].Value = data.RNP001_LIST[idx].WRT_DT; // 작성일시
                dynReq.Elements["VST_PTH_CD"].Value = data.RNP001_LIST[idx].VST_PTH_CD; // 입원경로
                dynReq.Elements["VST_PTH_ETC_TXT"].Value = data.RNP001_LIST[idx].VST_PTH_ETC_TXT; // 입원경로 기타상세
                dynReq.Elements["PTNT_TP_CD"].Value = data.RNP001_LIST[idx].PTNT_TP_CD_12; // 환자구분(1.일반 2.신생아(28일이내))


                // 정보제공자
                dynReq.Tables["TBL_INFM_OFFRR"].Columns.Add("INFM_OFFRR_NM"); // 성명
                dynReq.Tables["TBL_INFM_OFFRR"].AddRow();
                dynReq.Tables["TBL_INFM_OFFRR"].Rows[0]["INFM_OFFRR_NM"].Value = data.RNP001_LIST[idx].INFM_OFFRR_NM; // 성명

                // B.일반정보

                dynReq.Elements["CC_TXT"].Value = data.RNP001_LIST[idx].CC_TXT; // 입원동기
                dynReq.Elements["ANMN_TXT"].Value = data.RNP001_LIST[idx].ANMN_TXT; // 과거력
                dynReq.Elements["SOPR_HIST_TXT"].Value = data.RNP001_LIST[idx].SOPR_HIST_TXT; // 수술력
                dynReq.Elements["MDCT_STAT_TXT"].Value = data.RNP001_LIST[idx].MDCT_STAT_TXT; // 최근투약상태
                dynReq.Elements["ALRG_YN"].Value = data.RNP001_LIST[idx].ALRG_YN; // 알레르기 여부
                dynReq.Elements["ALRG_TXT"].Value = data.RNP001_LIST[idx].ALRG_TXT; // 알레르기 내용
                dynReq.Elements["FMHS_TXT"].Value = data.RNP001_LIST[idx].FMHS_TXT; // 가족력
                dynReq.Elements["DRNK_YN"].Value = data.RNP001_LIST[idx].DRNK_YN; // 음주여부
                dynReq.Elements["DRNK_TXT"].Value = data.RNP001_LIST[idx].DRNK_TXT; // 음주내용
                dynReq.Elements["SMKN_YN"].Value = data.RNP001_LIST[idx].SMKN_YN; // 흡연여부
                dynReq.Elements["SMKN_TXT"].Value = data.RNP001_LIST[idx].SMKN_TXT; // 흡연내용
                dynReq.Elements["HEIG"].Value = data.RNP001_LIST[idx].HEIG; // 신장
                dynReq.Elements["BWGT"].Value = data.RNP001_LIST[idx].BWGT; // 몸무게
                dynReq.Elements["PHBD_MEDEXM_TXT"].Value = data.RNP001_LIST[idx].PHBD_MEDEXM_TXT; // 신체검진

                // C.신생아 정보

                dynReq.Elements["BIRTH_DT"].Value = data.RNP001_LIST[idx].BIRTH_DTM; // 출생일시
                dynReq.Elements["FTUS_DEV_TRM"].Value = data.RNP001_LIST[idx].FTUS_DEV_TRM; // 재태기간(주/일)
                dynReq.Elements["PARTU_FRM_TXT"].Value = data.RNP001_LIST[idx].PARTU_FRM_TXT_HIRA; // 분만형태(평문)
                dynReq.Elements["APSC_PNT"].Value = data.RNP001_LIST[idx].APSC_PNT; // Apgar Score(1분점수/5분점수)





                // requestData - createDoc    최종 제출
                //               createTmpDoc 임시 제출
                //               updateTmpDoc 수정 제출
                //               selectDocList 목록 조회
                //               selectDocLink 미리보기
                //               selectDoc     상세조회
                HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(m_Hosid, isTmp ? "createTmpDoc" : "createDoc");
                if (!dynRes.Result)
                {
                    data.RNP001_RESULT.STATUS = "E"; // 오류
                    data.RNP001_RESULT.ERR_CODE = dynRes.ErrorCode;
                    data.RNP001_RESULT.ERR_DESC = dynRes.ErrorMessage;

                    data.RNP001_RESULT.DOC_NO = ""; //문서번호
                    data.RNP001_RESULT.SUPL_DATA_FOM_CD = ""; //서식코드
                    data.RNP001_RESULT.RCV_NO = ""; //접수번호
                    data.RNP001_RESULT.SP_SNO = ""; //명세서일련번호
                    data.RNP001_RESULT.HOSP_RNO = ""; //환자등록번호
                    data.RNP001_RESULT.PAT_NM = ""; //환자성명
                    data.RNP001_RESULT.INSUP_TP_CD = ""; //참고업무구분

                }
                else
                {
                    data.RNP001_RESULT.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                    data.RNP001_RESULT.ERR_CODE = "";
                    data.RNP001_RESULT.ERR_DESC = "";

                    data.RNP001_RESULT.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                    data.RNP001_RESULT.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                    data.RNP001_RESULT.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                    data.RNP001_RESULT.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                    data.RNP001_RESULT.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                    data.RNP001_RESULT.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                    data.RNP001_RESULT.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분
                }

                // 전송내역을 저장한다.
                SaveToTI86_RNP001(data, isTmp, p_sysdt, p_systm, p_conn);
            }

        }

        private void SaveToTI86_RNP001(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
        }
    }
}
