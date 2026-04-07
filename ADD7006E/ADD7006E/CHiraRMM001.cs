using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CHiraRMM001
    {
        private string m_Hosid;
        private string m_Demno;
        private string m_Cnecno;
        private string m_Cnectdd;
        private string m_BillSno;

        public CHiraRMM001(string p_hosid, string p_demno, string p_cnecno, string p_cnectdd, string p_billsno)
        {
            m_Hosid = p_hosid;
            m_Demno = p_demno;
            m_Cnecno = p_cnecno;
            m_Cnectdd = p_cnectdd;
            m_BillSno = p_billsno;
        }

        public void Send(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
            if (data.RMM001_LIST.Count < 1) return;

            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

            string qfycd = data.QFYCD;
            string insup_tp_cd = "";
            if (qfycd.StartsWith("29")) insup_tp_cd = "7"; // 보훈
            else if (qfycd.StartsWith("6")) insup_tp_cd = "8"; // 자보
            else if (qfycd.StartsWith("3")) insup_tp_cd = "5"; // 보호
            else insup_tp_cd = "4"; // 보험

            // 문서 공통 정보
            dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "RMM001"; //서식코드: 중환자실기록자료
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
            dynReq.Elements["IPAT_OPAT_TP_CD"].Value = (data.IOFG == "2" ? "1" : "2"); // 진료형태(1.입원 2.외래)


            // B.투약정보

            dynReq.Tables["TBL_MDS_PRSC"].Columns.Add("PRSC_DD"); // 처방일자
            dynReq.Tables["TBL_MDS_PRSC"].Columns.Add("PRSC_DIV_CD"); // 처방분류(1.원내 2.지참약 3.D/C(투여중단) 4.퇴원약 5.원외)
            //dynReq.Tables["TBL_MDS_PRSC"].Columns.Add("MDS_CD"); // 약품코드
            dynReq.Tables["TBL_MDS_PRSC"].Columns.Add("MDS_NM"); // 약품명
            dynReq.Tables["TBL_MDS_PRSC"].Columns.Add("INJC_PTH_TXT"); // 투여경로
            dynReq.Tables["TBL_MDS_PRSC"].Columns.Add("FQ1_MDCT_QTY"); // 1회투약량
            dynReq.Tables["TBL_MDS_PRSC"].Columns.Add("MDS_UNIT_TXT"); // 단위
            dynReq.Tables["TBL_MDS_PRSC"].Columns.Add("DY1_INJC_FQ"); // 1일투여횟수
            dynReq.Tables["TBL_MDS_PRSC"].Columns.Add("TOT_INJC_DDCNT"); // 총투약일수(처방분류가 4.퇴원약 5.원외인 경우 필수로 기재)
            dynReq.Tables["TBL_MDS_PRSC"].Columns.Add("INJC_EXEC_CD"); // 투여여부(0.해당없음 1.정상투여 2.미시행)
            dynReq.Tables["TBL_MDS_PRSC"].Columns.Add("INJC_PTH_TXT"); // 투여경로
            dynReq.Tables["TBL_MDS_PRSC"].Columns.Add("NEXEC_RS_TXT"); // 미시행사유(투여여부가 2.미시행인 경우 사유)
            dynReq.Tables["TBL_MDS_PRSC"].Columns.Add("EXEC_DT"); // 실시일시(투여여부가 1.정상투여인 경우)
            dynReq.Tables["TBL_MDS_PRSC"].Columns.Add("DGSBJT_CD"); // 진료과목
            dynReq.Tables["TBL_MDS_PRSC"].Columns.Add("IFLD_DTL_SPC_SBJT_CD"); // 내과세부 진료과목
            dynReq.Tables["TBL_MDS_PRSC"].Columns.Add("RMK_TXT"); // 비고

            for (int i = 0; i < data.RMM001_LIST.Count; i++)
            {
                dynReq.Tables["TBL_MDS_PRSC"].AddRow();
                dynReq.Tables["TBL_MDS_PRSC"].Rows[i]["PRSC_DD"].Value = data.RMM001_LIST[i].ODT; // 처방일자
                dynReq.Tables["TBL_MDS_PRSC"].Rows[i]["PRSC_DIV_CD"].Value = data.RMM001_LIST[i].DIV_FG; // 처방분류(1.원내 2.지참약 3.D/C(투여중단) 4.퇴원약 5.원외)
                //dynReq.Tables["TBL_MDS_PRSC"].Rows[i]["MDS_CD"].Value = data.RMM001_LIST[i].ISPCD; // 약품코드
                dynReq.Tables["TBL_MDS_PRSC"].Rows[i]["MDS_NM"].Value = data.RMM001_LIST[i].ONM; // 약품명
                dynReq.Tables["TBL_MDS_PRSC"].Rows[i]["INJC_PTH_TXT"].Value = data.RMM001_LIST[i].FLDCD4; // 투여경로
                dynReq.Tables["TBL_MDS_PRSC"].Rows[i]["FQ1_MDCT_QTY"].Value = data.RMM001_LIST[i].DQTY; // 1회투약량
                dynReq.Tables["TBL_MDS_PRSC"].Rows[i]["MDS_UNIT_TXT"].Value = data.RMM001_LIST[i].DUNIT; // 단위
                dynReq.Tables["TBL_MDS_PRSC"].Rows[i]["DY1_INJC_FQ"].Value = data.RMM001_LIST[i].ORDCNT; // 1일투여횟수
                dynReq.Tables["TBL_MDS_PRSC"].Rows[i]["TOT_INJC_DDCNT"].Value = data.RMM001_LIST[i].ODAYCNT; // 총투약일수(처방분류가 4.퇴원약 5.원외인 경우 필수로 기재)
                dynReq.Tables["TBL_MDS_PRSC"].Rows[i]["INJC_EXEC_CD"].Value = data.RMM001_LIST[i].EXEC_FG; // 투여여부(0.해당없음 1.정상투여 2.미시행)
                dynReq.Tables["TBL_MDS_PRSC"].Rows[i]["NEXEC_RS_TXT"].Value = ""; // 미시행사유(투여여부가 2.미시행인 경우 사유)
                dynReq.Tables["TBL_MDS_PRSC"].Rows[i]["EXEC_DT"].Value = data.RMM001_LIST[i].DODT; // 실시일시(투여여부가 1.정상투여인 경우)
                dynReq.Tables["TBL_MDS_PRSC"].Rows[i]["DGSBJT_CD"].Value = data.RMM001_LIST[i].INSDPTCD; // 진료과목
                dynReq.Tables["TBL_MDS_PRSC"].Rows[i]["IFLD_DTL_SPC_SBJT_CD"].Value = data.RMM001_LIST[i].INSDPTCD2; // 내과세부 진료과목
                dynReq.Tables["TBL_MDS_PRSC"].Rows[i]["RMK_TXT"].Value = ""; // 비고
            }

            // C.항암화학요법
            dynReq.Elements["ANCR_TRET_YN"].Value = "2"; // 항암치료여부(1.Yes 2.No) 우리 사이트에서는 하지 않는 것으로 간주하고 No로 설정.



            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(m_Hosid, isTmp ? "createTmpDoc" : "createDoc");
            if (!dynRes.Result)
            {
                data.RMM001_RESULT.STATUS = "E"; // 오류
                data.RMM001_RESULT.ERR_CODE = dynRes.ErrorCode;
                data.RMM001_RESULT.ERR_DESC = dynRes.ErrorMessage;

                data.RMM001_RESULT.DOC_NO = ""; //문서번호
                data.RMM001_RESULT.SUPL_DATA_FOM_CD = ""; //서식코드
                data.RMM001_RESULT.RCV_NO = ""; //접수번호
                data.RMM001_RESULT.SP_SNO = ""; //명세서일련번호
                data.RMM001_RESULT.HOSP_RNO = ""; //환자등록번호
                data.RMM001_RESULT.PAT_NM = ""; //환자성명
                data.RMM001_RESULT.INSUP_TP_CD = ""; //참고업무구분

            }
            else
            {
                data.RMM001_RESULT.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.RMM001_RESULT.ERR_CODE = "";
                data.RMM001_RESULT.ERR_DESC = "";

                data.RMM001_RESULT.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                data.RMM001_RESULT.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                data.RMM001_RESULT.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                data.RMM001_RESULT.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                data.RMM001_RESULT.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                data.RMM001_RESULT.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                data.RMM001_RESULT.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분
            }

            // 전송내역을 저장한다.
            SaveToTI86_RMM001(data, isTmp, p_sysdt, p_systm, p_conn);

        }

        private void SaveToTI86_RMM001(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
        }
    }
}
