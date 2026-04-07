using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CHiraRAA001
    {
        private string m_Hosid;
        private string m_Demno;
        private string m_Cnecno;
        private string m_Cnectdd;
        private string m_BillSno;

        public CHiraRAA001(string p_hosid, string p_demno, string p_cnecno, string p_cnectdd, string p_billsno)
        {
            m_Hosid = p_hosid;
            m_Demno = p_demno;
            m_Cnecno = p_cnecno;
            m_Cnectdd = p_cnectdd;
            m_BillSno = p_billsno;
        }

        public void Send(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
            if (data.RAA001_LIST.Count < 1) return;

            for (int idx = 0; idx < data.RAA001_LIST.Count; idx++)
            {
                HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

                string qfycd = data.QFYCD;
                string insup_tp_cd = "";
                if (qfycd.StartsWith("29")) insup_tp_cd = "7"; // 보훈
                else if (qfycd.StartsWith("6")) insup_tp_cd = "8"; // 자보
                else if (qfycd.StartsWith("3")) insup_tp_cd = "5"; // 보호
                else insup_tp_cd = "4"; // 보험

                // 문서 공통 정보
                dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "RAA001"; //서식코드: 중환자실기록자료
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
                dynReq.Elements["NCT_STA_DT"].Value = data.RAA001_LIST[idx].ANSDTM; // 마취 시작일시
                dynReq.Elements["NCT_END_DT"].Value = data.RAA001_LIST[idx].ANEDTM; // 마취 종료일시
                dynReq.Elements["SOPR_STA_DT"].Value = data.RAA001_LIST[idx].OPSDTM; // 수술 시작일시
                dynReq.Elements["SOPR_END_DT"].Value = data.RAA001_LIST[idx].OPEDTM; // 수술 종료일시

                // 마취통증의학과전문의
                dynReq.Tables["TBL_NCT_SDR"].Columns.Add("NCT_SDR_NM"); // 성명
                dynReq.Tables["TBL_NCT_SDR"].AddRow();
                dynReq.Tables["TBL_NCT_SDR"].Rows[0]["NCT_SDR_NM"].Value = data.RAA001_LIST[idx].ANEDRNM; // 성명

                dynReq.Elements["WRTP_NM"].Value = data.RAA001_LIST[idx].USRNM; // 작성자 성명
                dynReq.Elements["WRT_DT"].Value = data.RAA001_LIST[idx].ENTDTM; // 작성일시

                // B.마취 전 정보

                // 수술
                dynReq.Tables["TBL_SOPR"].Columns.Add("SOPR_NM"); // 수술명
                dynReq.Tables["TBL_SOPR"].Columns.Add("MDFEE_CD"); // 수술코드

                for (int i = 0; i < data.RAA001_LIST[idx].OPNM.Count; i++)
                {
                    dynReq.Tables["TBL_SOPR"].AddRow();
                    dynReq.Tables["TBL_SOPR"].Rows[i]["SOPR_NM"].Value = data.RAA001_LIST[idx].OPNM[i]; // 수술명
                    dynReq.Tables["TBL_SOPR"].Rows[i]["DIMDFEE_CDAG_NM"].Value = data.RAA001_LIST[idx].ISPCD[i]; // 수가코드
                }

                // 진단
                dynReq.Tables["TBL_DIAG"].Columns.Add("DIAG_NM"); // 진단명
                //dynReq.Tables["TBL_DIAG"].Columns.Add("SICK_SYM"); // 상병분류기호

                for (int i = 0; i < data.RAA001_LIST[idx].DXD.Count; i++)
                {
                    dynReq.Tables["TBL_DIAG"].AddRow();
                    dynReq.Tables["TBL_DIAG"].Rows[i]["DIAG_NM"].Value = data.RAA001_LIST[idx].DXD[i]; // 진단명
                    //dynReq.Tables["TBL_DIAG"].Rows[i]["SICK_SYM"].Value = data.RAA001_LIST[idx].DACD[i]; // 상병분류기호
                }

                dynReq.Elements["NCT_FRM_CD"].Value = data.RAA001_LIST[idx].NCT_FRM_CD; // 마취형태
                dynReq.Elements["ASA_PNT"].Value = data.RAA001_LIST[idx].ASA_PNT; // ASA 점수

                // C.마취 중 정보
                dynReq.Elements["NCT_MTH_CD"].Value = data.RAA001_LIST[idx].NCT_MTH_CD; // 마취방법
                dynReq.Elements["NCT_MTH_ETC_TXT"].Value = data.RAA001_LIST[idx].NCT_MTH_ETC_TXT; // 마취방법상세
                dynReq.Elements["NCT_MIDD_MNTR_YN"].Value = data.RAA001_LIST[idx].NCT_MIDD_MNTR_YN; // 마취 중 감시여부
                dynReq.Elements["NCT_MNTR_KND_CD"].Value = data.RAA001_LIST[idx].NCT_MNTR_KND_CD; // 마취 중 감시여부 종류
                dynReq.Elements["MNTR_ETC_TXT"].Value = data.RAA001_LIST[idx].NCT_MTH_ETC_TXT; // 마취 중 감시여부 종류상세

                // 활력징후
                dynReq.Tables["TBL_VTSG"].Columns.Add("MASR_DT"); // 활력징후 측정일시
                dynReq.Tables["TBL_VTSG"].Columns.Add("BPRSU"); // 활력징후 혈압
                dynReq.Tables["TBL_VTSG"].Columns.Add("PULS"); // 활력징후 맥박
                dynReq.Tables["TBL_VTSG"].Columns.Add("BRT"); // 활력징후 호흡
                dynReq.Tables["TBL_VTSG"].Columns.Add("TMPR"); // 활력징후 체온

                for (int i = 0; i < data.RAA001_LIST[idx].VTSG_MASR_DT.Count; i++)
                {
                    dynReq.Tables["TBL_VTSG"].AddRow();
                    dynReq.Tables["TBL_VTSG"].Rows[i]["MASR_DT"].Value = data.RAA001_LIST[idx].VTSG_MASR_DT[i]; // 활력징후 측정일시
                    dynReq.Tables["TBL_VTSG"].Rows[i]["BPRSU"].Value = data.RAA001_LIST[idx].BPRSU[i]; // 활력징후 혈압
                    dynReq.Tables["TBL_VTSG"].Rows[i]["PULS"].Value = data.RAA001_LIST[idx].PULS[i]; // 활력징후 맥박
                    dynReq.Tables["TBL_VTSG"].Rows[i]["BRT"].Value = data.RAA001_LIST[idx].BRT[i]; // 활력징후 호흡
                    dynReq.Tables["TBL_VTSG"].Rows[i]["TMPR"].Value = data.RAA001_LIST[idx].TMPR[i]; // 활력징후 체온
                }

                // 마취 중 감시측정
                dynReq.Tables["TBL_MIDD_MNTR"].Columns.Add("MASR_DT"); // 마취 중 감시측정 측정일시
                dynReq.Tables["TBL_MIDD_MNTR"].Columns.Add("OXY_STRT"); // 마취 중 감시측정 산소포화도
                dynReq.Tables["TBL_MIDD_MNTR"].Columns.Add("CRBR_OXY_STRT"); // 마취 중 감시측정 대뇌산소포화도
                dynReq.Tables["TBL_MIDD_MNTR"].Columns.Add("NRRT_CNDC_CD"); // 마취 중 감시측정 TOF
                dynReq.Tables["TBL_MIDD_MNTR"].Columns.Add("NRRT_CNDC_RT"); // 마취 중 감시측정 ratio 상세
                dynReq.Tables["TBL_MIDD_MNTR"].Columns.Add("NRRT_CNDC_CNT"); // 마취 중 감시측정 count 상세
                dynReq.Tables["TBL_MIDD_MNTR"].Columns.Add("BIS_CNT"); // 마취 중 감시측정 BIS
                dynReq.Tables["TBL_MIDD_MNTR"].Columns.Add("CROT_CNT"); // 마취 중 감시측정 CO
                dynReq.Tables["TBL_MIDD_MNTR"].Columns.Add("CVP_CNT"); // 마취 중 감시측정 CVP
                dynReq.Tables["TBL_MIDD_MNTR"].Columns.Add("RMK_TXT"); // 마취 중 감시측정 특이사항

                for (int i = 0; i < data.RAA001_LIST[idx].MNTR_MASR_DT.Count; i++)
                {
                    dynReq.Tables["TBL_MIDD_MNTR"].AddRow();
                    dynReq.Tables["TBL_MIDD_MNTR"].Rows[i]["MASR_DT"].Value = data.RAA001_LIST[idx].MNTR_MASR_DT[i]; // 마취 중 감시측정 측정일시
                    dynReq.Tables["TBL_MIDD_MNTR"].Rows[i]["OXY_STRT"].Value = data.RAA001_LIST[idx].OXY_STRT[i]; // 마취 중 감시측정 산소포화도
                    dynReq.Tables["TBL_MIDD_MNTR"].Rows[i]["CRBR_OXY_STRT"].Value = data.RAA001_LIST[idx].CRBR_OXY_STRT[i]; // 마취 중 감시측정 대뇌산소포화도
                    dynReq.Tables["TBL_MIDD_MNTR"].Rows[i]["NRRT_CNDC_CD"].Value = data.RAA001_LIST[idx].NRRT_CNDC_CD[i]; // 마취 중 감시측정 TOF
                    dynReq.Tables["TBL_MIDD_MNTR"].Rows[i]["NRRT_CNDC_RT"].Value = data.RAA001_LIST[idx].NRRT_CNDC_RT[i]; // 마취 중 감시측정 ratio 상세
                    dynReq.Tables["TBL_MIDD_MNTR"].Rows[i]["NRRT_CNDC_CNT"].Value = data.RAA001_LIST[idx].NRRT_CNDC_CNT[i]; // 마취 중 감시측정 count 상세
                    dynReq.Tables["TBL_MIDD_MNTR"].Rows[i]["BIS_CNT"].Value = data.RAA001_LIST[idx].BIS_CNT[i]; // 마취 중 감시측정 BIS
                    dynReq.Tables["TBL_MIDD_MNTR"].Rows[i]["CROT_CNT"].Value = data.RAA001_LIST[idx].CROT_CNT[i]; // 마취 중 감시측정 CO
                    dynReq.Tables["TBL_MIDD_MNTR"].Rows[i]["CVP_CNT"].Value = data.RAA001_LIST[idx].CVP_CNT[i]; // 마취 중 감시측정 CVP
                    dynReq.Tables["TBL_MIDD_MNTR"].Rows[i]["RMK_TXT"].Value = data.RAA001_LIST[idx].RMK_TXT[i]; // 마취 중 감시측정 특이사항
                }

                // 마취 중 투약
                dynReq.Tables["TBL_SOPR_MIDD_MDCT"].Columns.Add("NCT_KND_CD"); // 마취 중 투약 분류
                dynReq.Tables["TBL_SOPR_MIDD_MDCT"].Columns.Add("MDCT_DT"); // 마취 중 투약 투약일시
                dynReq.Tables["TBL_SOPR_MIDD_MDCT"].Columns.Add("CNTN_MDCT_END_DT"); // 마취 중 투약 종료일시
                dynReq.Tables["TBL_SOPR_MIDD_MDCT"].Columns.Add("MDS_NM"); // 마취 중 투약 약품명
                dynReq.Tables["TBL_SOPR_MIDD_MDCT"].Columns.Add("FQ1_MDCT_QTY"); // 마취 중 투약 1회투약량
                dynReq.Tables["TBL_SOPR_MIDD_MDCT"].Columns.Add("MDS_UNIT_TXT"); // 마취 중 투약 단위

                for (int i = 0; i < data.RAA001_LIST[idx].KND_CD.Count; i++)
                {
                    dynReq.Tables["TBL_SOPR_MIDD_MDCT"].AddRow();
                    dynReq.Tables["TBL_SOPR_MIDD_MDCT"].Rows[i]["NCT_KND_CD"].Value = data.RAA001_LIST[idx].KND_CD[i]; // 마취 중 투약 분류
                    dynReq.Tables["TBL_SOPR_MIDD_MDCT"].Rows[i]["MDCT_DT"].Value = data.RAA001_LIST[idx].MDCT_DT(i); // 마취 중 투약 투약일시
                    dynReq.Tables["TBL_SOPR_MIDD_MDCT"].Rows[i]["CNTN_MDCT_END_DT"].Value = data.RAA001_LIST[idx].CNTN_MDCT_END_DT(i); // 마취 중 투약 종료일시
                    dynReq.Tables["TBL_SOPR_MIDD_MDCT"].Rows[i]["MDS_NM"].Value = data.RAA001_LIST[idx].MDS_NM[i]; // 마취 중 투약 약품명
                    dynReq.Tables["TBL_SOPR_MIDD_MDCT"].Rows[i]["FQ1_MDCT_QTY"].Value = data.RAA001_LIST[idx].OQTY[i]; // 마취 중 투약 1회투약량
                    dynReq.Tables["TBL_SOPR_MIDD_MDCT"].Rows[i]["MDS_UNIT_TXT"].Value = data.RAA001_LIST[idx].UNIT[i]; // 마취 중 투약 단위
                }

                // 섭취량 & 배설량
                dynReq.Elements["IGSN_TOT_QTY"].Value = data.RAA001_LIST[idx].IN_TOT_QTY.ToString();  // 섭취량 총량
                dynReq.Elements["IGSN_IFSL_QTY"].Value = data.RAA001_LIST[idx].IN_IFSL_QTY.ToString(); // 섭취량 수액

                // 섭취량 수혈
                dynReq.Tables["TBL_BLTS"].Columns.Add("BLTS_KND_TXT"); // 혈액종류
                dynReq.Tables["TBL_BLTS"].Columns.Add("BLTS_QTY"); // 수혈량

                for (int i = 0; i< data.RAA001_LIST[idx].BLTS_KND.Count; i++)
                {
                    dynReq.Tables["TBL_BLTS"].AddRow();
                    dynReq.Tables["TBL_BLTS"].Rows[i]["BLTS_KND_TXT"].Value = data.RAA001_LIST[idx].BLTS_KND[i]; // 혈액종류
                    dynReq.Tables["TBL_BLTS"].Rows[i]["BLTS_QTY"].Value = data.RAA001_LIST[idx].BLTS_QTY[i].ToString(); // 수혈량
                }

                dynReq.Elements["PROD_TOT_QTY"].Value = data.RAA001_LIST[idx].OUT_TOT_QTY.ToString(); // 배설량 총량
                dynReq.Elements["PROD_URNN_QTY"].Value = data.RAA001_LIST[idx].OUT_URNN_QTY.ToString(); // 배설량 배뇨
                dynReq.Elements["PROD_HMRHG_QTY"].Value = data.RAA001_LIST[idx].OUT_BLD_QTY.ToString(); // 배설량 실혈
                dynReq.Elements["PROD_ETC_QTY"].Value = data.RAA001_LIST[idx].OUT_ETC_QTY.ToString(); // 배설량 기타

                // 마취관련 기록
                dynReq.Tables["TBL_NCT_RCD"].Columns.Add("OCUR_DT"); // 발생일시
                dynReq.Tables["TBL_NCT_RCD"].Columns.Add("RCD_TXT"); // 내용

                for (int i = 0; i < data.RAA001_LIST[idx].OCUR_DT.Count; i++)
                {
                    dynReq.Tables["TBL_NCT_RCD"].AddRow();
                    dynReq.Tables["TBL_NCT_RCD"].Rows[i]["OCUR_DT"].Value = data.RAA001_LIST[idx].OCUR_DTM(i); // 발생일시
                    dynReq.Tables["TBL_NCT_RCD"].Rows[i]["RCD_TXT"].Value = data.RAA001_LIST[idx].RCD_TXT[i]; // 내용
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
                    data.RAA001_RESULT.STATUS = "E"; // 오류
                    data.RAA001_RESULT.ERR_CODE = dynRes.ErrorCode;
                    data.RAA001_RESULT.ERR_DESC = dynRes.ErrorMessage;

                    data.RAA001_RESULT.DOC_NO = ""; //문서번호
                    data.RAA001_RESULT.SUPL_DATA_FOM_CD = ""; //서식코드
                    data.RAA001_RESULT.RCV_NO = ""; //접수번호
                    data.RAA001_RESULT.SP_SNO = ""; //명세서일련번호
                    data.RAA001_RESULT.HOSP_RNO = ""; //환자등록번호
                    data.RAA001_RESULT.PAT_NM = ""; //환자성명
                    data.RAA001_RESULT.INSUP_TP_CD = ""; //참고업무구분

                }
                else
                {
                    data.RAA001_RESULT.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                    data.RAA001_RESULT.ERR_CODE = "";
                    data.RAA001_RESULT.ERR_DESC = "";

                    data.RAA001_RESULT.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                    data.RAA001_RESULT.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                    data.RAA001_RESULT.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                    data.RAA001_RESULT.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                    data.RAA001_RESULT.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                    data.RAA001_RESULT.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                    data.RAA001_RESULT.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분
                }

                // 전송내역을 저장한다.
                SaveToTI86_RAA001(data, isTmp, p_sysdt, p_systm, p_conn);
            }

        }

        private void SaveToTI86_RAA001(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
        }
    }
}
