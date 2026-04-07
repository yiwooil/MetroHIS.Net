using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CHiraRSS001
    {
        private string m_Hosid;
        private string m_Demno;
        private string m_Cnecno;
        private string m_Cnectdd;
        private string m_BillSno;

        public CHiraRSS001(string p_hosid, string p_demno, string p_cnecno, string p_cnectdd, string p_billsno)
        {
            m_Hosid = p_hosid;
            m_Demno = p_demno;
            m_Cnecno = p_cnecno;
            m_Cnectdd = p_cnectdd;
            m_BillSno = p_billsno;
        }

        public void Send(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
            if (data.RSS001_LIST.Count < 1) return;

            for (int idx = 0; idx < data.RSS001_LIST.Count; idx++)
            {
                HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

                string qfycd = data.QFYCD;
                string insup_tp_cd = "";
                if (qfycd.StartsWith("29")) insup_tp_cd = "7"; // 보훈
                else if (qfycd.StartsWith("6")) insup_tp_cd = "8"; // 자보
                else if (qfycd.StartsWith("3")) insup_tp_cd = "5"; // 보호
                else insup_tp_cd = "4"; // 보험

                // 문서 공통 정보
                dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "RSS001"; //서식코드: 진단검사결과지
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
                dynReq.Elements["SOPR_STA_DT"].Value = data.RSS001_LIST[idx].OPSDTM; // 수술시작일시
                dynReq.Elements["SOPR_END_DT"].Value = data.RSS001_LIST[idx].OPEDTM; // 수술종료일시

                // 수술의
                dynReq.Tables["TBL_SOPR_DR"].Columns.Add("SOPR_DR_CD"); // 구분
                dynReq.Tables["TBL_SOPR_DR"].Columns.Add("SOPR_DGSBJT_CD"); // 진료과
                dynReq.Tables["TBL_SOPR_DR"].Columns.Add("IFLD_DTL_SPC_SBJT_CD"); // 내과세부진료과
                dynReq.Tables["TBL_SOPR_DR"].Columns.Add("SOPR_DR_NM"); // 수술의사성명
                //dynReq.Tables["TBL_SOPR_DR"].Columns.Add("SPOR_DR_LCS_NO"); // 수술의면허번호

                dynReq.Tables["TBL_SOPR_DR"].AddRow();
                dynReq.Tables["TBL_SOPR_DR"].Rows[0]["SOPR_DR_CD"].Value = data.RSS001_LIST[idx].DR_GUBUN; // 구분
                dynReq.Tables["TBL_SOPR_DR"].Rows[0]["SOPR_DGSBJT_CD"].Value = data.RSS001_LIST[idx].INSDPTCD; // 진료과
                dynReq.Tables["TBL_SOPR_DR"].Rows[0]["IFLD_DTL_SPC_SBJT_CD"].Value = data.RSS001_LIST[idx].INSDPTCD2; // 내과세부진료과
                dynReq.Tables["TBL_SOPR_DR"].Rows[0]["SOPR_DR_NM"].Value = data.RSS001_LIST[idx].DRNM; // 수술의사성명
                //dynReq.Tables["TBL_SOPR_DR"].Rows[0]["SPOR_DR_LCS_NO"].Value = data.RSS001_LIST[idx].GDRLCID; // 수술의면허번호

                // 보조의1
                if (data.RSS001_LIST[idx].DR_GUBUN_SUB1 != "")
                {
                    dynReq.Tables["TBL_SOPR_DR"].AddRow();
                    dynReq.Tables["TBL_SOPR_DR"].Rows[1]["SOPR_DR_CD"].Value = data.RSS001_LIST[idx].DR_GUBUN_SUB1; // 구분
                    dynReq.Tables["TBL_SOPR_DR"].Rows[1]["SOPR_DGSBJT_CD"].Value = data.RSS001_LIST[idx].INSDPTCD_SUB1; // 진료과
                    dynReq.Tables["TBL_SOPR_DR"].Rows[1]["IFLD_DTL_SPC_SBJT_CD"].Value = data.RSS001_LIST[idx].INSDPTCD2_SUB1; // 내과세부진료과
                    dynReq.Tables["TBL_SOPR_DR"].Rows[1]["SOPR_DR_NM"].Value = data.RSS001_LIST[idx].DRNM_SUB1; // 수술의사성명
                    //dynReq.Tables["TBL_SOPR_DR"].Rows[1]["SPOR_DR_LCS_NO"].Value = data.RSS001_LIST[idx].GDRLCID_SUB1; // 수술의면허번호
                }
                // 보조의2
                if (data.RSS001_LIST[idx].DR_GUBUN_SUB2 != "")
                {
                    dynReq.Tables["TBL_SOPR_DR"].AddRow();
                    dynReq.Tables["TBL_SOPR_DR"].Rows[2]["SOPR_DR_CD"].Value = data.RSS001_LIST[idx].DR_GUBUN_SUB2; // 구분
                    dynReq.Tables["TBL_SOPR_DR"].Rows[2]["SOPR_DGSBJT_CD"].Value = data.RSS001_LIST[idx].INSDPTCD_SUB2; // 진료과
                    dynReq.Tables["TBL_SOPR_DR"].Rows[2]["IFLD_DTL_SPC_SBJT_CD"].Value = data.RSS001_LIST[idx].INSDPTCD2_SUB2; // 내과세부진료과
                    dynReq.Tables["TBL_SOPR_DR"].Rows[2]["SOPR_DR_NM"].Value = data.RSS001_LIST[idx].DRNM_SUB2; // 수술의사성명
                    //dynReq.Tables["TBL_SOPR_DR"].Rows[2]["SPOR_DR_LCS_NO"].Value = data.RSS001_LIST[idx].GDRLCID_SUB2; // 수술의면허번호
                }
                // 보조의3
                if (data.RSS001_LIST[idx].DR_GUBUN_SUB3 != "")
                {
                    dynReq.Tables["TBL_SOPR_DR"].AddRow();
                    dynReq.Tables["TBL_SOPR_DR"].Rows[3]["SOPR_DR_CD"].Value = data.RSS001_LIST[idx].DR_GUBUN_SUB3; // 구분
                    dynReq.Tables["TBL_SOPR_DR"].Rows[3]["SOPR_DGSBJT_CD"].Value = data.RSS001_LIST[idx].INSDPTCD_SUB3; // 진료과
                    dynReq.Tables["TBL_SOPR_DR"].Rows[3]["IFLD_DTL_SPC_SBJT_CD"].Value = data.RSS001_LIST[idx].INSDPTCD2_SUB3; // 내과세부진료과
                    dynReq.Tables["TBL_SOPR_DR"].Rows[3]["SOPR_DR_NM"].Value = data.RSS001_LIST[idx].DRNM_SUB3; // 수술의사성명
                    //dynReq.Tables["TBL_SOPR_DR"].Rows[3]["SPOR_DR_LCS_NO"].Value = data.RSS001_LIST[idx].GDRLCID_SUB3; // 수술의면허번호
                }

                dynReq.Elements["WRTP_NM"].Value = data.RSS001_LIST[idx].EMPNM; // 작성자성명
                dynReq.Elements["WRT_DT"].Value = data.RSS001_LIST[idx].WRTDTM; // 작성일시

                // B.수술정보
                dynReq.Elements["EMY_YN"].Value = data.RSS001_LIST[idx].STAFG_YN; // 응급여부
                dynReq.Elements["NCT_KND_TXT"].Value = data.RSS001_LIST[idx].ANETPNM; // 마취종류

                // 수술전진단
                dynReq.Tables["TBL_SOPR_BF_DIAG"].Columns.Add("SOPR_BF_DIAG_NM"); // 수술전진단
                dynReq.Tables["TBL_SOPR_BF_DIAG"].AddRow();
                dynReq.Tables["TBL_SOPR_BF_DIAG"].Rows[0]["SOPR_BF_DIAG_NM"].Value = data.RSS001_LIST[idx].PREDX; // 수술전진단

                // 수술후진단
                dynReq.Tables["TBL_SOPR_AF_DIAG"].Columns.Add("SOPR_AF_DIAG_NM"); // 수술전진단
                dynReq.Tables["TBL_SOPR_AF_DIAG"].AddRow();
                dynReq.Tables["TBL_SOPR_AF_DIAG"].Rows[0]["SOPR_AF_DIAG_NM"].Value = data.RSS001_LIST[idx].POSDX; // 수술후진단

                // 수술명
                dynReq.Tables["TBL_SOPR_NM"].Columns.Add("SOPR_NM"); // 수술명
                for (int i = 0; i < data.RSS001_LIST[idx].ONM.Count; i++)
                {
                    dynReq.Tables["TBL_SOPR_NM"].AddRow();
                    dynReq.Tables["TBL_SOPR_NM"].Rows[0]["SOPR_NM"].Value = data.RSS001_LIST[idx].ONM[i]; // 수술명
                }

                // 수가코드
                dynReq.Tables["TBL_MDFEE_CD"].Columns.Add("MDFEE_CD"); // 수가코드
                for (int i = 0; i < data.RSS001_LIST[idx].ISPCD.Count; i++)
                {
                    dynReq.Tables["TBL_MDFEE_CD"].AddRow();
                    dynReq.Tables["TBL_MDFEE_CD"].Rows[0]["MDFEE_CD"].Value = data.RSS001_LIST[idx].ISPCD[i]; // 수가코드
                }

                dynReq.Elements["SOPR_PHSQ_TXT"].Value = data.RSS001_LIST[idx].POS; // 수술체위
                dynReq.Elements["LSN_LOCA_TXT"].Value = data.RSS001_LIST[idx].LESION; // 병변의위치
                dynReq.Elements["SOPR_RST_TXT"].Value = data.RSS001_LIST[idx].INDIOFSURGERY; // 수술소견
                dynReq.Elements["SOPR_PROC_TXT"].Value = data.RSS001_LIST[idx].SURFNDNPRO; // 수술절차
                dynReq.Elements["SOPR_SPCL_TXT"].Value = data.RSS001_LIST[idx].REMARK; // 중요(특이)사항


                // requestData - createDoc    최종 제출
                //               createTmpDoc 임시 제출
                //               updateTmpDoc 수정 제출
                //               selectDocList 목록 조회
                //               selectDocLink 미리보기
                //               selectDoc     상세조회
                HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(m_Hosid, isTmp ? "createTmpDoc" : "createDoc");
                if (!dynRes.Result)
                {
                    data.RSS001_RESULT.STATUS = "E"; // 오류
                    data.RSS001_RESULT.ERR_CODE = dynRes.ErrorCode;
                    data.RSS001_RESULT.ERR_DESC = dynRes.ErrorMessage;

                    data.RSS001_RESULT.DOC_NO = ""; //문서번호
                    data.RSS001_RESULT.SUPL_DATA_FOM_CD = ""; //서식코드
                    data.RSS001_RESULT.RCV_NO = ""; //접수번호
                    data.RSS001_RESULT.SP_SNO = ""; //명세서일련번호
                    data.RSS001_RESULT.HOSP_RNO = ""; //환자등록번호
                    data.RSS001_RESULT.PAT_NM = ""; //환자성명
                    data.RSS001_RESULT.INSUP_TP_CD = ""; //참고업무구분

                }
                else
                {
                    data.RSS001_RESULT.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                    data.RSS001_RESULT.ERR_CODE = "";
                    data.RSS001_RESULT.ERR_DESC = "";

                    data.RSS001_RESULT.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                    data.RSS001_RESULT.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                    data.RSS001_RESULT.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                    data.RSS001_RESULT.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                    data.RSS001_RESULT.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                    data.RSS001_RESULT.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                    data.RSS001_RESULT.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분
                }

                // 전송내역을 저장한다.
                SaveToTI86_RSS001(data, isTmp, p_sysdt, p_systm, p_conn);
            }

        }

        private void SaveToTI86_RSS001(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
        }
    }
}
