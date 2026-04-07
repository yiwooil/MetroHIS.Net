using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CHiraRNE001
    {
        private string m_Hosid;
        private string m_Demno;
        private string m_Cnecno;
        private string m_Cnectdd;
        private string m_BillSno;

        public CHiraRNE001(string p_hosid, string p_demno, string p_cnecno, string p_cnectdd, string p_billsno)
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
            dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "RNE001"; //서식코드: 응급간호기록기록자료
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
            dynReq.Elements["EMRRM_IPAT_DT"].Value = data.RNE001_LIST[0].BEDEDTM; // 응급실 도착일시
            dynReq.Elements["EMRRM_DSCG_DT"].Value = data.RNE001_LIST[0].BEDODTM; // 응급실 퇴실일시
            dynReq.Elements["EMRRM_VST_PTH_CD"].Value = data.RNE001_LIST[0].VST_PTH_CD; // 내원경로

            // B.응급 간호기록

            // 2.활력징후
            dynReq.Tables["TBL_VTSG"].Columns.Add("MASR_DT"); // 측정일시
            dynReq.Tables["TBL_VTSG"].Columns.Add("BPRSU"); // 혈압
            dynReq.Tables["TBL_VTSG"].Columns.Add("PULS"); // 맥박
            dynReq.Tables["TBL_VTSG"].Columns.Add("BRT"); // 호흡
            dynReq.Tables["TBL_VTSG"].Columns.Add("TMPR"); // 체온
            dynReq.Tables["TBL_VTSG"].Columns.Add("OXY_STRT"); // 산소포화도
            dynReq.Tables["TBL_VTSG"].Columns.Add("VTSG_TXT"); // 특이사항

            for (int i = 0; i < data.RNE001_LIST[0].CHKDT.Count; i++)
            {
                dynReq.Tables["TBL_VTSG"].AddRow();
                dynReq.Tables["TBL_VTSG"].Rows[i]["MASR_DT"].Value = data.RNE001_LIST[0].CHKDTM(i); // 측정일시
                dynReq.Tables["TBL_VTSG"].Rows[i]["BPRSU"].Value = data.RNE001_LIST[0].BP[i]; // 혈압
                dynReq.Tables["TBL_VTSG"].Rows[i]["PULS"].Value = data.RNE001_LIST[0].PR[i]; // 맥박
                dynReq.Tables["TBL_VTSG"].Rows[i]["BRT"].Value = data.RNE001_LIST[0].RR[i]; // 호흡
                dynReq.Tables["TBL_VTSG"].Rows[i]["TMPR"].Value = data.RNE001_LIST[0].TMP[i]; // 체온
                dynReq.Tables["TBL_VTSG"].Rows[i]["OXY_STRT"].Value = data.RNE001_LIST[0].SPO2[i]; // 산소포화도
                dynReq.Tables["TBL_VTSG"].Rows[i]["VTSG_TXT"].Value = data.RNE001_LIST[0].RMK[i]; // 특이사항
            }

            // 3.처치 및 간호기록
            dynReq.Tables["TBL_CARE_RCD"].Columns.Add("RCT_DT"); // 기록일시
            dynReq.Tables["TBL_CARE_RCD"].Columns.Add("RCT_TXT"); // 간호기록
            dynReq.Tables["TBL_CARE_RCD"].Columns.Add("NURSE_NM"); // 간호사 성명

            for (int i = 0; i < data.RNE001_LIST[0].WDATE.Count; i++)
            {
                dynReq.Tables["TBL_CARE_RCD"].AddRow();
                dynReq.Tables["TBL_CARE_RCD"].Rows[i]["RCD_DT"].Value = data.RNE001_LIST[0].WDTM(i); // 길혹일시
                dynReq.Tables["TBL_CARE_RCD"].Rows[i]["RCD_TXT"].Value = data.RNE001_LIST[0].RESULT[i]; // 길혹일시
                dynReq.Tables["TBL_CARE_RCD"].Rows[i]["NURSE_NM"].Value = data.RNE001_LIST[0].PNURES[i]; // 길혹일시
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
            SaveToTI86_RNE001(data, isTmp, p_sysdt, p_systm, p_conn);

        }

        private void SaveToTI86_RNE001(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
        }
    }
}
