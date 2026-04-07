using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CHiraRNH001
    {
        private string m_Hosid;
        private string m_Demno;
        private string m_Cnecno;
        private string m_Cnectdd;
        private string m_BillSno;

        public CHiraRNH001(string p_hosid, string p_demno, string p_cnecno, string p_cnectdd, string p_billsno)
        {
            m_Hosid = p_hosid;
            m_Demno = p_demno;
            m_Cnecno = p_cnecno;
            m_Cnectdd = p_cnectdd;
            m_BillSno = p_billsno;
        }

        public void Send(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
            if (data.RNH001_LIST.Count < 1) return;

            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

            string qfycd = data.QFYCD;
            string insup_tp_cd = "";
            if (qfycd.StartsWith("29")) insup_tp_cd = "7"; // 보훈
            else if (qfycd.StartsWith("6")) insup_tp_cd = "8"; // 자보
            else if (qfycd.StartsWith("3")) insup_tp_cd = "5"; // 보호
            else insup_tp_cd = "4"; // 보험

            // 문서 공통 정보
            dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "RNH001"; //서식코드: 진단검사결과지
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
            dynReq.Elements["DGSBJT_CD"].Value = data.RNH001_LIST[0].INSDPTCD; // 진료과
            dynReq.Elements["IFLD_DTL_SPC_SBJT_CD"].Value = data.RNH001_LIST[0].INSDPTCD2; // 내과세부진료과
            dynReq.Elements["CHRG_DR_NM"].Value = data.RNH001_LIST[0].DRNM; // 처방의사명

            // B.환자정보
            dynReq.Tables["TBL_DIAG"].Columns.Add("DIAG_NM"); // 진단명
            for (int i = 0; i < data.RNH001_LIST[0].DXD.Count; i++)
            {
                dynReq.Tables["TBL_DIAG"].AddRow();
                dynReq.Tables["TBL_DIAG"].Rows[i]["DIAG_NM"].Value = data.RNH001_LIST[0].DXD[i]; // 진단명
            }

            dynReq.Elements["DLYS_KND_CD"].Value = "1"; // 투석종류(1.혈액투석 2.지속적대체요법)


            // C.혈액투석

            // 1.혈액투석
            dynReq.Tables["TBL_BLDD"].Columns.Add("BLDD_STA_DT"); // 시작일시
            dynReq.Tables["TBL_BLDD"].Columns.Add("BLDD_END_DT"); // 종료일시
            dynReq.Tables["TBL_BLDD"].Columns.Add("DLYS_BWGT"); // 건체중
            dynReq.Tables["TBL_BLDD"].Columns.Add("BF_BWGT"); // 투석 전 체중
            dynReq.Tables["TBL_BLDD"].Columns.Add("AF_BWGT"); // 투석 후 체중
            dynReq.Tables["TBL_BLDD"].Columns.Add("BLDVE_CH_CD"); // 혈관통로
            dynReq.Tables["TBL_BLDD"].Columns.Add("CATH_TXT"); // 카테터 내용
            dynReq.Tables["TBL_BLDD"].Columns.Add("GL_WTR_DEL_QTY"); // 목표 수분 제거량
            dynReq.Tables["TBL_BLDD"].Columns.Add("ATCG_ERYY_TXT"); // 항응고요법 초기
            dynReq.Tables["TBL_BLDD"].Columns.Add("ATCG_MNTN_TXT"); // 항응고요법 유지
            dynReq.Tables["TBL_BLDD"].Columns.Add("DLYS_DV_TXT"); // 투석기
            dynReq.Tables["TBL_BLDD"].Columns.Add("DLYS_LQD_TXT"); // 투석액
            dynReq.Tables["TBL_BLDD"].Columns.Add("WRTP_NM"); // 작성자성명

            for (int i = 0; i < data.RNH001_LIST[0].CHKDT.Count; i++)
            {
                dynReq.Tables["TBL_BLDD"].AddRow();
                dynReq.Tables["TBL_BLDD"].Rows[i]["BLDD_STA_DT"].Value = data.RNH001_LIST[0].SDTM(i); // 시작일시
                dynReq.Tables["TBL_BLDD"].Rows[i]["BLDD_END_DT"].Value = data.RNH001_LIST[0].EDTM(i); // 종료일시
                dynReq.Tables["TBL_BLDD"].Rows[i]["DLYS_BWGT"].Value = data.RNH001_LIST[0].LastWt[i]; // 건체중
                dynReq.Tables["TBL_BLDD"].Rows[i]["BF_BWGT"].Value = data.RNH001_LIST[0].HMBeCurWt[i]; // 투석 전 체중
                dynReq.Tables["TBL_BLDD"].Rows[i]["AF_BWGT"].Value = data.RNH001_LIST[0].HMAfCurWt[i]; // 투석 후 체중
                dynReq.Tables["TBL_BLDD"].Rows[i]["BLDVE_CH_CD"].Value = data.RNH001_LIST[0].BLDVE_CH_CD(i); // 혈관통로
                dynReq.Tables["TBL_BLDD"].Rows[i]["CATH_TXT"].Value = ""; // 카테터 내용
                dynReq.Tables["TBL_BLDD"].Rows[i]["GL_WTR_DEL_QTY"].Value = data.RNH001_LIST[0].UFTOTAL[i]; // 목표 수분 제거량
                dynReq.Tables["TBL_BLDD"].Rows[i]["ATCG_ERYY_TXT"].Value = data.RNH001_LIST[0].AntiBaseOqty[i]; // 항응고요법 초기
                dynReq.Tables["TBL_BLDD"].Rows[i]["ATCG_MNTN_TXT"].Value = data.RNH001_LIST[0].MaintOqty[i]; // 항응고요법 유지
                dynReq.Tables["TBL_BLDD"].Rows[i]["DLYS_DV_TXT"].Value = data.RNH001_LIST[0].HMMachine[i]; // 투석기
                dynReq.Tables["TBL_BLDD"].Rows[i]["DLYS_LQD_TXT"].Value = data.RNH001_LIST[0].HMFluid[i]; // 투석액
                dynReq.Tables["TBL_BLDD"].Rows[i]["WRTP_NM"].Value = data.RNH001_LIST[0].ENM[i]; // 작성자성명
            }

            // 2.혈액투석 상세
            dynReq.Tables["TBL_BLDD_VTSG"].Columns.Add("MASR_DT"); // 측정일시
            dynReq.Tables["TBL_BLDD_VTSG"].Columns.Add("BPRSU"); // 혈압
            dynReq.Tables["TBL_BLDD_VTSG"].Columns.Add("PULS"); // 맥박
            dynReq.Tables["TBL_BLDD_VTSG"].Columns.Add("BRT"); // 호흡
            dynReq.Tables["TBL_BLDD_VTSG"].Columns.Add("TMPR"); // 체온
            dynReq.Tables["TBL_BLDD_VTSG"].Columns.Add("BLFL_RT"); // 혈류속도
            dynReq.Tables["TBL_BLDD_VTSG"].Columns.Add("ARTR_PRES"); // 동맥압
            dynReq.Tables["TBL_BLDD_VTSG"].Columns.Add("VIN_PRES"); // 정맥압

            for (int i = 0; i < data.RNH001_LIST[0].VCHKDT.Count; i++)
            {
                dynReq.Tables["TBL_BLDD_VTSG"].AddRow();
                dynReq.Tables["TBL_BLDD_VTSG"].Rows[i]["MASR_DT"].Value = data.RNH001_LIST[0].VCHKDTM(i); // 측정일시
                dynReq.Tables["TBL_BLDD_VTSG"].Rows[i]["BPRSU"].Value = data.RNH001_LIST[0].VTM[i]; // 혈압
                dynReq.Tables["TBL_BLDD_VTSG"].Rows[i]["PULS"].Value = data.RNH001_LIST[0].Vpressure[i]; // 맥박
                dynReq.Tables["TBL_BLDD_VTSG"].Rows[i]["BRT"].Value = ""; // 호흡
                dynReq.Tables["TBL_BLDD_VTSG"].Rows[i]["TMPR"].Value = ""; // 체온
                dynReq.Tables["TBL_BLDD_VTSG"].Rows[i]["BLFL_RT"].Value = data.RNH001_LIST[0].Vpulsation[i]; // 혈류속도
                dynReq.Tables["TBL_BLDD_VTSG"].Rows[i]["ARTR_PRES"].Value = data.RNH001_LIST[0].Vvein[i]; // 동맥압
                dynReq.Tables["TBL_BLDD_VTSG"].Rows[i]["VIN_PRES"].Value = data.RNH001_LIST[0].VSPEED[i]; // 정맥압
            }

            dynReq.Elements["BLDVE_STNS_MNTR_YN"].Value = "2"; // 협착모니터링 실시여부


            // E.간호기록
            dynReq.Tables["TBL_CARE_RCD"].Columns.Add("RCD_DT"); // 기록일시
            dynReq.Tables["TBL_CARE_RCD"].Columns.Add("RCD_TXT"); // 간호기록
            dynReq.Tables["TBL_CARE_RCD"].Columns.Add("NURSE_NM"); // 간호사 성명

            for (int i = 0; i < data.RNH001_LIST[0].N_CHKDT.Count; i++)
            {
                dynReq.Tables["TBL_CARE_RCD"].AddRow();
                dynReq.Tables["TBL_CARE_RCD"].Rows[i]["RCD_DT"].Value = data.RNH001_LIST[0].N_CHKDTM(i); // 기록일시
                dynReq.Tables["TBL_CARE_RCD"].Rows[i]["RCD_TXT"].Value = data.RNH001_LIST[0].N_Nursing[i]; // 간호기록
                dynReq.Tables["TBL_CARE_RCD"].Rows[i]["NURSE_NM"].Value = data.RNH001_LIST[0].N_ENM[i]; // 간호사 성명
            }


            // F.추가정보

            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(m_Hosid, isTmp ? "createTmpDoc" : "createDoc");
            if (!dynRes.Result)
            {
                data.RNH001_RESULT.STATUS = "E"; // 오류
                data.RNH001_RESULT.ERR_CODE = dynRes.ErrorCode;
                data.RNH001_RESULT.ERR_DESC = dynRes.ErrorMessage;

                data.RNH001_RESULT.DOC_NO = ""; //문서번호
                data.RNH001_RESULT.SUPL_DATA_FOM_CD = ""; //서식코드
                data.RNH001_RESULT.RCV_NO = ""; //접수번호
                data.RNH001_RESULT.SP_SNO = ""; //명세서일련번호
                data.RNH001_RESULT.HOSP_RNO = ""; //환자등록번호
                data.RNH001_RESULT.PAT_NM = ""; //환자성명
                data.RNH001_RESULT.INSUP_TP_CD = ""; //참고업무구분

            }
            else
            {
                data.RNH001_RESULT.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.RNH001_RESULT.ERR_CODE = "";
                data.RNH001_RESULT.ERR_DESC = "";

                data.RNH001_RESULT.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                data.RNH001_RESULT.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                data.RNH001_RESULT.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                data.RNH001_RESULT.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                data.RNH001_RESULT.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                data.RNH001_RESULT.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                data.RNH001_RESULT.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분
            }

            // 전송내역을 저장한다.
            SaveToTI86_RNH001(data, isTmp, p_sysdt, p_systm, p_conn);

        }

        private void SaveToTI86_RNH001(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
        }
    }
}
