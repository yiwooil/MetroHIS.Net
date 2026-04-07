using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CHiraERR001
    {
        private string m_Hosid;
        private string m_Demno;
        private string m_Cnecno;
        private string m_Cnectdd;
        private string m_BillSno;

        public CHiraERR001(string p_hosid, string p_demno, string p_cnecno, string p_cnectdd, string p_billsno)
        {
            m_Hosid = p_hosid;
            m_Demno = p_demno;
            m_Cnecno = p_cnecno;
            m_Cnectdd = p_cnectdd;
            m_BillSno = p_billsno;
        }

        public void Send(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
            if (data.ERR001_LIST.Count < 1) return;

            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

            string qfycd = data.QFYCD;
            string insup_tp_cd = "";
            if (qfycd.StartsWith("29")) insup_tp_cd = "7"; // 보훈
            else if (qfycd.StartsWith("6")) insup_tp_cd = "8"; // 자보
            else if (qfycd.StartsWith("3")) insup_tp_cd = "5"; // 보호
            else insup_tp_cd = "4"; // 보험

            // 문서 공통 정보
            dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "ERR001"; //서식코드: 진단검사결과지
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

            // A. 검사 정보 및 겨과
            dynReq.Tables["TBL_EXM_RST"].Columns.Add("DGSBJT_CD"); // 진료과
            dynReq.Tables["TBL_EXM_RST"].Columns.Add("IFLD_DTL_SPC_SBJT_CD"); // 내과세부진료과
            dynReq.Tables["TBL_EXM_RST"].Columns.Add("PRSC_DR_NM"); // 처방의사성명
            dynReq.Tables["TBL_EXM_RST"].Columns.Add("EXM_PRSC_DT"); // 처방일시
            dynReq.Tables["TBL_EXM_RST"].Columns.Add("EXM_EXEC_DT"); // 검사일시
            dynReq.Tables["TBL_EXM_RST"].Columns.Add("EXM_RST_DT"); // 판독일시
            dynReq.Tables["TBL_EXM_RST"].Columns.Add("DCT_DR_NM"); // 판독의사성명
            //dynReq.Tables["TBL_EXM_RST"].Columns.Add("DCT_DR_LCS_NO"); // 판독의사 면허번호
            dynReq.Tables["TBL_EXM_RST"].Columns.Add("EXM_MDFEE_CD"); // 수가코드(EDI코드, 매핑되지 않으믄 00)
            dynReq.Tables["TBL_EXM_RST"].Columns.Add("EXM_CD"); // 검사코드(병원에서부여한코드)
            dynReq.Tables["TBL_EXM_RST"].Columns.Add("EXM_NM"); // 검사명(병원에서부여한명)
            dynReq.Tables["TBL_EXM_RST"].Columns.Add("EXM_RST_TXT"); // 판독결과

            for (int i = 0; i < data.ERR001_LIST.Count; i++)
            {
                dynReq.Tables["TBL_GRID_EXM"].AddRow();
                dynReq.Tables["TBL_EXM_RST"].Rows[i]["DGSBJT_CD"].Value = data.ERR001_LIST[i].INSDPTCD; // 진료과
                dynReq.Tables["TBL_EXM_RST"].Rows[i]["IFLD_DTL_SPC_SBJT_CD"].Value = data.ERR001_LIST[i].INSDPTCD2; // 내과세부진료과
                dynReq.Tables["TBL_EXM_RST"].Rows[i]["PRSC_DR_NM"].Value = data.ERR001_LIST[i].DRNM; // 처방의사성명
                dynReq.Tables["TBL_EXM_RST"].Rows[i]["EXM_PRSC_DT"].Value = data.ERR001_LIST[i].ODTM; // 처방일시
                dynReq.Tables["TBL_EXM_RST"].Rows[i]["EXM_EXEC_DT"].Value = data.ERR001_LIST[i].PHTDTM; // 검사일시
                dynReq.Tables["TBL_EXM_RST"].Rows[i]["EXM_RST_DT"].Value = data.ERR001_LIST[i].RPTDTM; // 판독일시
                dynReq.Tables["TBL_EXM_RST"].Rows[i]["DCT_DR_NM"].Value = data.ERR001_LIST[i].RDRNM; // 판독의사성명
                //dynReq.Tables["TBL_EXM_RST"].Rows[i]["DCT_DR_LCS_NO"].Value = data.ERR001_LIST[i].GDRLCID; // 판독의사 면허번호
                dynReq.Tables["TBL_EXM_RST"].Rows[i]["EXM_MDFEE_CD"].Value = data.ERR001_LIST[i].EDICD; // 수가코드(EDI코드, 매핑되지 않으믄 00)
                dynReq.Tables["TBL_EXM_RST"].Rows[i]["EXM_CD"].Value = data.ERR001_LIST[i].OCD; // 검사코드(병원에서부여한코드)
                dynReq.Tables["TBL_EXM_RST"].Rows[i]["EXM_NM"].Value = data.ERR001_LIST[i].ONM; // 검사명(병원에서부여한명)
                dynReq.Tables["TBL_EXM_RST"].Rows[i]["EXM_RST_TXT"].Value = data.ERR001_LIST[i].RPTXT; // 판독결과
            }

            // B.추가정보

            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(m_Hosid, isTmp ? "createTmpDoc" : "createDoc");
            if (!dynRes.Result)
            {
                data.ERR001_RESULT.STATUS = "E"; // 오류
                data.ERR001_RESULT.ERR_CODE = dynRes.ErrorCode;
                data.ERR001_RESULT.ERR_DESC = dynRes.ErrorMessage;

                data.ERR001_RESULT.DOC_NO = ""; //문서번호
                data.ERR001_RESULT.SUPL_DATA_FOM_CD = ""; //서식코드
                data.ERR001_RESULT.RCV_NO = ""; //접수번호
                data.ERR001_RESULT.SP_SNO = ""; //명세서일련번호
                data.ERR001_RESULT.HOSP_RNO = ""; //환자등록번호
                data.ERR001_RESULT.PAT_NM = ""; //환자성명
                data.ERR001_RESULT.INSUP_TP_CD = ""; //참고업무구분

            }
            else
            {
                data.ERR001_RESULT.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.ERR001_RESULT.ERR_CODE = "";
                data.ERR001_RESULT.ERR_DESC = "";

                data.ERR001_RESULT.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                data.ERR001_RESULT.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                data.ERR001_RESULT.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                data.ERR001_RESULT.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                data.ERR001_RESULT.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                data.ERR001_RESULT.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                data.ERR001_RESULT.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분
            }

            // 전송내역을 저장한다.
            SaveToTI86_ERR001(data, isTmp, p_sysdt, p_systm, p_conn);

        }

        private void SaveToTI86_ERR001(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
        }
    }
}
