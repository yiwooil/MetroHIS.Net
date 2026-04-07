using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CHiraRIP001
    {
        private string m_Hosid;
        private string m_Demno;
        private string m_Cnecno;
        private string m_Cnectdd;
        private string m_BillSno;

        public CHiraRIP001(string p_hosid, string p_demno, string p_cnecno, string p_cnectdd, string p_billsno)
        {
            m_Hosid = p_hosid;
            m_Demno = p_demno;
            m_Cnecno = p_cnecno;
            m_Cnectdd = p_cnectdd;
            m_BillSno = p_billsno;
        }

        public void Send(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
            if (data.RIP001_LIST.Count < 1) return;

            for (int idx = 0; idx < data.RIP001_LIST.Count; idx++)
            {
                HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

                string qfycd = data.QFYCD;
                string insup_tp_cd = "";
                if (qfycd.StartsWith("29")) insup_tp_cd = "7"; // 보훈
                else if (qfycd.StartsWith("6")) insup_tp_cd = "8"; // 자보
                else if (qfycd.StartsWith("3")) insup_tp_cd = "5"; // 보호
                else insup_tp_cd = "4"; // 보험

                // 문서 공통 정보
                dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "RIP001"; //서식코드: 진단검사결과지
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
                dynReq.Elements["IPAT_DT"].Value = data.RIP001_LIST[idx].BEDEDTM; // 입원일시

                // B. 진단 및 처치

                // 1. 진단
                dynReq.Tables["TBL_DIAG"].Columns.Add("SICK_TP_CD"); // 확진여부
                dynReq.Tables["TBL_DIAG"].Columns.Add("DIAG_NM"); // 진단명

                for (int i = 0; i < data.RIP001_LIST[idx].DXD.Count; i++)
                {
                    dynReq.Tables["TBL_DIAG"].AddRow();
                    dynReq.Tables["TBL_DIAG"].Rows[i]["SICK_TP_CD"].Value = data.RIP001_LIST[idx].TYPE_123(i); // 진단구분
                    dynReq.Tables["TBL_DIAG"].Rows[i]["DIAG_NM"].Value = data.RIP001_LIST[idx].DXD[i]; // 진단명
                }

                // 2. 치치및 수술
                dynReq.Tables["TBL_MOPR_SOPR"].Columns.Add("SOPR_ENFC_DT"); // 시행일시
                dynReq.Tables["TBL_MOPR_SOPR"].Columns.Add("SOPR_NM"); // 명칭

                for (int i = 0; i < data.RIP001_LIST[idx].ODT.Count; i++)
                {
                    dynReq.Tables["TBL_MOPR_SOPR"].AddRow();
                    dynReq.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_ENFC_DT"].Value = data.RIP001_LIST[idx].ODTM(i); // 시행일시
                    dynReq.Tables["TBL_MOPR_SOPR"].Rows[i]["SOPR_NM"].Value = data.RIP001_LIST[idx].ONM[i]; // 명칭
                }

                dynReq.Elements["ALRG_YN"].Value = data.RIP001_LIST[idx].ALRG_YN; // 약물이상 반응여부
                dynReq.Elements["ALRG_TXT"].Value = data.RIP001_LIST[idx].ALRG_TXT; // 약물이상 반응내용

                // C.입원경과
                dynReq.Tables["TBL_IPAT_ELAPS"].Columns.Add("DIAG_DD"); // 진료일시
                dynReq.Tables["TBL_IPAT_ELAPS"].Columns.Add("DGSBJT_CD"); // 진료과목
                dynReq.Tables["TBL_IPAT_ELAPS"].Columns.Add("IFLD_DTL_SPC_SBJT_CD"); // 내과세부진료과목
                dynReq.Tables["TBL_IPAT_ELAPS"].Columns.Add("CHRG_DR_NM"); // 담당의사성명(주치의,전문의 등)
                dynReq.Tables["TBL_IPAT_ELAPS"].Columns.Add("WRTP_NM"); // 작성자성명
                dynReq.Tables["TBL_IPAT_ELAPS"].Columns.Add("WRT_DT"); // 작성일시
                dynReq.Tables["TBL_IPAT_ELAPS"].Columns.Add("PRBM_LIST_TXT"); // 주관적정보 & 객관적정보 & 평가
                dynReq.Tables["TBL_IPAT_ELAPS"].Columns.Add("TRET_PLAN_TXT"); // 치료계획

                for (int i = 0; i < data.RIP001_LIST[idx].EXDT.Count; i++)
                {
                    dynReq.Tables["TBL_IPAT_ELAPS"].AddRow();
                    dynReq.Tables["TBL_IPAT_ELAPS"].Rows[i]["DIAG_DD"].Value = data.RIP001_LIST[idx].EXDT[i]; // 진료일시
                    dynReq.Tables["TBL_IPAT_ELAPS"].Rows[i]["DGSBJT_CD"].Value = data.RIP001_LIST[idx].INSDPTCD[i]; // 진료과목
                    dynReq.Tables["TBL_IPAT_ELAPS"].Rows[i]["IFLD_DTL_SPC_SBJT_CD"].Value = data.RIP001_LIST[idx].INSDPTCD2[i]; // 내과세부진료과목
                    dynReq.Tables["TBL_IPAT_ELAPS"].Rows[i]["CHRG_DR_NM"].Value = data.RIP001_LIST[idx].DRNM; // 담당의사성명(주치의,전문의 등) ***
                    dynReq.Tables["TBL_IPAT_ELAPS"].Rows[i]["WRTP_NM"].Value = data.RIP001_LIST[idx].USERNM[i]; // 작성자성명
                    dynReq.Tables["TBL_IPAT_ELAPS"].Rows[i]["WRT_DT"].Value = data.RIP001_LIST[idx].WRTDTM(i); // 작성일시
                    dynReq.Tables["TBL_IPAT_ELAPS"].Rows[i]["PRBM_LIST_TXT"].Value = data.RIP001_LIST[idx].PRBM_TXT(i); // 주관적정보 & 객관적정보 & 평가
                    dynReq.Tables["TBL_IPAT_ELAPS"].Rows[i]["TRET_PLAN_TXT"].Value = data.RIP001_LIST[idx].PLAN_TXT(i); // 치료계획
                }



                // requestData - createDoc    최종 제출
                //               createTmpDoc 임시 제출
                //               updateTmpDoc 수정 제출
                //               selectDocList 목록 조회
                //               selectDocLink 미리보기
                //               selectDoc     상세조회
                HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(m_Hosid, isTmp ? "createTmpDoc" : "createDoc");
                if (!dynRes.Result)
                {
                    data.RIP001_RESULT.STATUS = "E"; // 오류
                    data.RIP001_RESULT.ERR_CODE = dynRes.ErrorCode;
                    data.RIP001_RESULT.ERR_DESC = dynRes.ErrorMessage;

                    data.RIP001_RESULT.DOC_NO = ""; //문서번호
                    data.RIP001_RESULT.SUPL_DATA_FOM_CD = ""; //서식코드
                    data.RIP001_RESULT.RCV_NO = ""; //접수번호
                    data.RIP001_RESULT.SP_SNO = ""; //명세서일련번호
                    data.RIP001_RESULT.HOSP_RNO = ""; //환자등록번호
                    data.RIP001_RESULT.PAT_NM = ""; //환자성명
                    data.RIP001_RESULT.INSUP_TP_CD = ""; //참고업무구분

                }
                else
                {
                    data.RIP001_RESULT.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                    data.RIP001_RESULT.ERR_CODE = "";
                    data.RIP001_RESULT.ERR_DESC = "";

                    data.RIP001_RESULT.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                    data.RIP001_RESULT.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                    data.RIP001_RESULT.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                    data.RIP001_RESULT.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                    data.RIP001_RESULT.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                    data.RIP001_RESULT.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                    data.RIP001_RESULT.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분
                }

                // 전송내역을 저장한다.
                SaveToTI86_RIP001(data, isTmp, p_sysdt, p_systm, p_conn);
            }

        }

        private void SaveToTI86_RIP001(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
        }
    }
}
