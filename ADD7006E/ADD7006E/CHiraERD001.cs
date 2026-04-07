using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CHiraERD001
    {
        private string m_Hosid;
        private string m_Demno;
        private string m_Cnecno;
        private string m_Cnectdd;
        private string m_BillSno;

        public CHiraERD001(string p_hosid, string p_demno, string p_cnecno, string p_cnectdd, string p_billsno)
        {
            m_Hosid = p_hosid;
            m_Demno = p_demno;
            m_Cnecno = p_cnecno;
            m_Cnectdd = p_cnectdd;
            m_BillSno = p_billsno;
        }

        public void Send(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
            if (data.ERD001_LIST.Count < 1) return;

            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

            string qfycd = data.QFYCD;
            string insup_tp_cd = "";
            if (qfycd.StartsWith("29")) insup_tp_cd = "7"; // 보훈
            else if (qfycd.StartsWith("6")) insup_tp_cd = "8"; // 자보
            else if (qfycd.StartsWith("3")) insup_tp_cd = "5"; // 보호
            else insup_tp_cd = "4"; // 보험

            // 문서 공통 정보
            dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "ERD001"; //서식코드: 진단검사결과지
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
            dynReq.Elements["DGSBJT_CD"].Value = data.ERD001_LIST[0].INSDPTCD; // 진료과
            dynReq.Elements["IFLD_DTL_SPC_SBJT_CD"].Value = data.ERD001_LIST[0].INSDPTCD2; // 내과세부진료과
            dynReq.Elements["PRSC_DR_NM"].Value = data.ERD001_LIST[0].DRNM; // 처방의사명

            // B.Text형 검사결과
            dynReq.Tables["TBL_TXT_EXM"].Columns.Add("EXM_PRSC_DT"); // 처방일시
            dynReq.Tables["TBL_TXT_EXM"].Columns.Add("EXM_GAT_DT"); // 채취일시
            dynReq.Tables["TBL_TXT_EXM"].Columns.Add("EXM_RCV_DT"); // 접수일시
            dynReq.Tables["TBL_TXT_EXM"].Columns.Add("EXM_RST_DT"); // 판독일시
            dynReq.Tables["TBL_TXT_EXM"].Columns.Add("EXM_SPCM_CD"); // 검체종류(01.형액 02.소변 03.대변 04.체액및조직 05.골수 99.기타)
            dynReq.Tables["TBL_TXT_EXM"].Columns.Add("EXM_SPCM_ETC_TXT"); // 검체종류상사(검제종류가 99.기타인 경우)
            dynReq.Tables["TBL_TXT_EXM"].Columns.Add("EXM_MDFEE_CD"); // 수가코드(EDI코드, 매핑되지 않으믄 00)
            dynReq.Tables["TBL_TXT_EXM"].Columns.Add("EXM_CD"); // 검사코드(병원에서부여한코드)
            dynReq.Tables["TBL_TXT_EXM"].Columns.Add("EXM_NM"); // 검사명(병원에서부여한명)
            dynReq.Tables["TBL_TXT_EXM"].Columns.Add("EXM_RST_TXT"); // 검사결과
            dynReq.Tables["TBL_TXT_EXM"].Columns.Add("DCT_DR_NM"); // 판독의사성명
            dynReq.Tables["TBL_TXT_EXM"].Columns.Add("DCT_DR_LCS_NO"); // 판독의사면허번호

            dynReq.Tables["TBL_TXT_EXM"].AddRow();
            dynReq.Tables["TBL_TXT_EXM"].Rows[0]["EXM_PRSC_DT"].Value = "-"; // 처방일시
            dynReq.Tables["TBL_TXT_EXM"].Rows[0]["EXM_GAT_DT"].Value = "-"; // 채취일시
            dynReq.Tables["TBL_TXT_EXM"].Rows[0]["EXM_RCV_DT"].Value = "-"; // 접수일시
            dynReq.Tables["TBL_TXT_EXM"].Rows[0]["EXM_RST_DT"].Value = "-"; // 판독일시
            dynReq.Tables["TBL_TXT_EXM"].Rows[0]["EXM_SPCM_CD"].Value = "-"; // 검체종류(01.형액 02.소변 03.대변 04.체액및조직 05.골수 99.기타)
            dynReq.Tables["TBL_TXT_EXM"].Rows[0]["EXM_SPCM_ETC_TXT"].Value = "-"; // 검체종류상사(검제종류가 99.기타인 경우)
            dynReq.Tables["TBL_TXT_EXM"].Rows[0]["EXM_MDFEE_CD"].Value = "-"; // 수가코드(EDI코드, 매핑되지 않으믄 00)
            dynReq.Tables["TBL_TXT_EXM"].Rows[0]["EXM_CD"].Value = "-"; // 검사코드(병원에서부여한코드)
            dynReq.Tables["TBL_TXT_EXM"].Rows[0]["EXM_NM"].Value = "-"; // 검사명(병원에서부여한명)
            dynReq.Tables["TBL_TXT_EXM"].Rows[0]["EXM_RST_TXT"].Value = "-"; // 검사결과
            dynReq.Tables["TBL_TXT_EXM"].Rows[0]["DCT_DR_NM"].Value = "-"; // 판독의사성명
            dynReq.Tables["TBL_TXT_EXM"].Rows[0]["DCT_DR_LCS_NO"].Value = "-"; // 판독의사면허번호


            // C.Grid형 검사결과
            dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_PRSC_DT"); // 처방일시
            dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_GAT_DT"); // 채취일시
            dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_RCV_DT"); // 접수일시
            dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_RST_DT"); // 결과일시
            dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_SPCM_CD"); // 검체종류(01.형액 02.소변 03.대변 04.체액및조직 05.골수 99.기타)
            dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_SPCM_ETC_TXT"); // 검체종류상사(검제종류가 99.기타인 경우)
            dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_MDFEE_CD"); // 수가코드(EDI코드, 매핑되지 않으믄 00)
            dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_CD"); // 검사코드(병원에서부여한코드)
            dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_NM"); // 검사명(병원에서부여한명)
            dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_RST_TXT"); // 검사결과
            dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_REF_TXT"); // 참고치(검사의 정상범위를 기재)
            dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_UNIT"); // 검사단위
            dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_ADD_TXT"); // 추가정보

            for (int i = 0; i < data.ERD001_LIST.Count; i++)
            {
                dynReq.Tables["TBL_GRID_EXM"].AddRow();
                dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_PRSC_DT"].Value = data.ERD001_LIST[i].ORDDTM; // 처방일시
                dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_GAT_DT"].Value = data.ERD001_LIST[i].BLOODDTM; // 채취일시
                dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_RCV_DT"].Value = data.ERD001_LIST[i].RCVDTM; // 접수일시
                dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_RST_DT"].Value = data.ERD001_LIST[i].VFYDTM; // 결과일시
                dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_SPCM_CD"].Value = data.ERD001_LIST[i].SPCCD; // 검체종류(01.형액 02.소변 03.대변 04.체액및조직 05.골수 99.기타)
                dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_SPCM_ETC_TXT"].Value = data.ERD001_LIST[i].SPCNM; // 검체종류상사(검제종류가 99.기타인 경우)
                dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_MDFEE_CD"].Value = data.ERD001_LIST[i].EDICD; // 수가코드(EDI코드, 매핑되지 않으믄 00)
                dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_CD"].Value = data.ERD001_LIST[i].TESTCD; // 검사코드(병원에서부여한코드)
                dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_NM"].Value = data.ERD001_LIST[i].TESTNM; // 검사명(병원에서부여한명)
                dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_RST_TXT"].Value = data.ERD001_LIST[i].RSTVAL; // 검사결과
                dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_REF_TXT"].Value = data.ERD001_LIST[i].REFERENCE; // 참고치(검사의 정상범위를 기재)
                dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_UNIT"].Value = data.ERD001_LIST[i].UNIT; // 검사단위
                dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_ADD_TXT"].Value = ""; // 추가정보
            }

            // D.기타정보
            // E.추가정보

            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(m_Hosid, isTmp ? "createTmpDoc" : "createDoc");
            if (!dynRes.Result)
            {
                data.ERD001_RESULT.STATUS = "E"; // 오류
                data.ERD001_RESULT.ERR_CODE = dynRes.ErrorCode;
                data.ERD001_RESULT.ERR_DESC = dynRes.ErrorMessage;

                data.ERD001_RESULT.DOC_NO = ""; //문서번호
                data.ERD001_RESULT.SUPL_DATA_FOM_CD = ""; //서식코드
                data.ERD001_RESULT.RCV_NO = ""; //접수번호
                data.ERD001_RESULT.SP_SNO = ""; //명세서일련번호
                data.ERD001_RESULT.HOSP_RNO = ""; //환자등록번호
                data.ERD001_RESULT.PAT_NM = ""; //환자성명
                data.ERD001_RESULT.INSUP_TP_CD = ""; //참고업무구분

            }
            else
            {
                data.ERD001_RESULT.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.ERD001_RESULT.ERR_CODE = "";
                data.ERD001_RESULT.ERR_DESC = "";

                data.ERD001_RESULT.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                data.ERD001_RESULT.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                data.ERD001_RESULT.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                data.ERD001_RESULT.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                data.ERD001_RESULT.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                data.ERD001_RESULT.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                data.ERD001_RESULT.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분
            }

            // 전송내역을 저장한다.
            SaveToTI86_ERD001(data, isTmp, p_sysdt, p_systm, p_conn);

        }

        private void SaveToTI86_ERD001(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
        }
    }
}
