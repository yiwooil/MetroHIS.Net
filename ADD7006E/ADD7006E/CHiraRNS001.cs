using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CHiraRNS001
    {
        private string m_Hosid;
        private string m_Demno;
        private string m_Cnecno;
        private string m_Cnectdd;
        private string m_BillSno;

        public CHiraRNS001(string p_hosid, string p_demno, string p_cnecno, string p_cnectdd, string p_billsno)
        {
            m_Hosid = p_hosid;
            m_Demno = p_demno;
            m_Cnecno = p_cnecno;
            m_Cnectdd = p_cnectdd;
            m_BillSno = p_billsno;
        }

        public void Send(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
            if (data.RNS001_LIST.Count < 1) return;

            for (int idx = 0; idx < data.RNS001_LIST.Count; idx++)
            {
                HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

                string qfycd = data.QFYCD;
                string insup_tp_cd = "";
                if (qfycd.StartsWith("29")) insup_tp_cd = "7"; // 보훈
                else if (qfycd.StartsWith("6")) insup_tp_cd = "8"; // 자보
                else if (qfycd.StartsWith("3")) insup_tp_cd = "5"; // 보호
                else insup_tp_cd = "4"; // 보험

                // 문서 공통 정보
                dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "RNS001"; //서식코드: 수술간호기록자료
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

                // A.기본정보
                dynReq.Elements["OPRM_IPAT_DT"].Value = data.RNS001_LIST[idx].OR_INDTM; // 수술실 입실일시
                dynReq.Elements["OPRM_DSCG_DT"].Value = data.RNS001_LIST[idx].PT_OUTDTM; // 수술실 퇴실일시
                dynReq.Elements["SOPR_STAT_DT"].Value = data.RNS001_LIST[idx].OP_STDTM; // 수술 시작일시
                dynReq.Elements["SOPR_END_DT"].Value = data.RNS001_LIST[idx].OP_ENDDTM; // 수술 종료일시
                dynReq.Elements["DSFN_NURSE_NM"].Value = data.RNS001_LIST[idx].SRNURS1; // 소독 간호사 성명
                dynReq.Elements["CRCL_NURSE_NM"].Value = data.RNS001_LIST[idx].CIRNURS1; // 순회 간호사 성명


                // B.수술 전 확인
                dynReq.Elements["PTNT_POSI_CFR_YN"].Value = data.RNS001_LIST[idx].TIMEOUTCHK; // Time Out


                // C.부위 정보

                // 수술 전 진단
                dynReq.Tables["TBL_SOPR_BF_DIAG"].Columns.Add("DIAG_NM"); // 진단명
                dynReq.Tables["TBL_SOPR_BF_DIAG"].AddRow();
                dynReq.Tables["TBL_SOPR_BF_DIAG"].Rows[0]["DIAG_NM"].Value = data.RNS001_LIST[idx].PREDXNM; // 진단명

                // 수술 후 진단
                dynReq.Tables["TBL_SOPR_AF_DIAG"].Columns.Add("DIAG_NM"); // 진단명
                dynReq.Tables["TBL_SOPR_AF_DIAG"].AddRow();
                dynReq.Tables["TBL_SOPR_AF_DIAG"].Rows[0]["DIAG_NM"].Value = data.RNS001_LIST[idx].POSTDXNM; // 진단명

                // 수술명
                dynReq.Tables["TBL_SOPR_NM"].Columns.Add("SOPR_NM"); // 수술명
                dynReq.Tables["TBL_SOPR_NM"].AddRow();
                dynReq.Tables["TBL_SOPR_NM"].Rows[0]["SOPR_NM"].Value = data.RNS001_LIST[idx].POSTOPNM; // 수술명

                // D.수술 정보

                // 1.삽입관
                dynReq.Tables["TBL_CNNL"].Columns.Add("CNNL_ETC_TXT"); // 종류
                dynReq.Tables["TBL_CNNL"].Columns.Add("RMK_TXT"); // 참고사항
                for (int i = 0; i < data.RNS001_LIST[idx].TUBE_1.Count; i++)
                {
                    dynReq.Tables["TBL_CNNL"].AddRow();
                    dynReq.Tables["TBL_CNNL"].Rows[i]["CNNL_ETC_TXT"].Value = data.RNS001_LIST[idx].TUBE_1[i]; // 종류
                    dynReq.Tables["TBL_CNNL"].Rows[i]["RMK_TXT"].Value = data.RNS001_LIST[idx].TUBE_RMK(i); // 참고사항
                }

                // 2.치료재료 및 고가 소모품

                // 3.수술 중 사용 약품
                dynReq.Tables["TBL_SOPR_MDS"].Columns.Add("MDS_NM"); // 약품명
                dynReq.Tables["TBL_SOPR_MDS"].Columns.Add("TOT_INJC_QTY"); // 투여용량
                for (int i = 0; i < data.RNS001_LIST[idx].TUBE_1.Count; i++)
                {
                    dynReq.Tables["TBL_SOPR_MDS"].AddRow();
                    dynReq.Tables["TBL_SOPR_MDS"].Rows[i]["MDS_NM"].Value = data.RNS001_LIST[idx].ONM[i]; // 약품명
                    dynReq.Tables["TBL_SOPR_MDS"].Rows[i]["TOT_INJC_QTY"].Value = data.RNS001_LIST[idx].QTY[i]; // 투여용량
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
                    data.RNO001_RESULT.STATUS = "E"; // 오류
                    data.RNO001_RESULT.ERR_CODE = dynRes.ErrorCode;
                    data.RNO001_RESULT.ERR_DESC = dynRes.ErrorMessage;

                    data.RNO001_RESULT.DOC_NO = ""; //문서번호
                    data.RNO001_RESULT.SUPL_DATA_FOM_CD = ""; //서식코드
                    data.RNO001_RESULT.RCV_NO = ""; //접수번호
                    data.RNO001_RESULT.SP_SNO = ""; //명세서일련번호
                    data.RNO001_RESULT.HOSP_RNO = ""; //환자등록번호
                    data.RNO001_RESULT.PAT_NM = ""; //환자성명
                    data.RNO001_RESULT.INSUP_TP_CD = ""; //참고업무구분

                }
                else
                {
                    data.RNO001_RESULT.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                    data.RNO001_RESULT.ERR_CODE = "";
                    data.RNO001_RESULT.ERR_DESC = "";

                    data.RNO001_RESULT.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                    data.RNO001_RESULT.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                    data.RNO001_RESULT.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                    data.RNO001_RESULT.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                    data.RNO001_RESULT.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                    data.RNO001_RESULT.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                    data.RNO001_RESULT.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분
                }

                // 전송내역을 저장한다.
                SaveToTI86_RNS001(data, isTmp, p_sysdt, p_systm, p_conn);
            }
        }

        private void SaveToTI86_RNS001(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
        }
    }
}
