using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CHiraRWI001
    {
        private string m_Hosid;
        private string m_Demno;
        private string m_Cnecno;
        private string m_Cnectdd;
        private string m_BillSno;

        public CHiraRWI001(string p_hosid, string p_demno, string p_cnecno, string p_cnectdd, string p_billsno)
        {
            m_Hosid = p_hosid;
            m_Demno = p_demno;
            m_Cnecno = p_cnecno;
            m_Cnectdd = p_cnectdd;
            m_BillSno = p_billsno;
        }

        public void Send(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
            if (data.RWI001_LIST.Count < 1) return;

            for (int idx = 0; idx < data.RWI001_LIST.Count; idx++)
            {
                HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

                string qfycd = data.QFYCD;
                string insup_tp_cd = "";
                if (qfycd.StartsWith("29")) insup_tp_cd = "7"; // 보훈
                else if (qfycd.StartsWith("6")) insup_tp_cd = "8"; // 자보
                else if (qfycd.StartsWith("3")) insup_tp_cd = "5"; // 보호
                else insup_tp_cd = "4"; // 보험

                // 문서 공통 정보
                dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "RWI001"; //서식코드: 중환자실기록자료
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
                dynReq.Elements["FST_IPAT_DT"].Value = data.RWI001_LIST[idx].FST_IPAT_DT; // 최초 입실일시

                // 2. 진단
                dynReq.Tables["TBL_DIAG"].Columns.Add("DIAG_NM"); // 진단명

                for (int i = 0; i < data.RWI001_LIST[idx].DXD.Count; i++)
                {
                    dynReq.Tables["TBL_DIAG"].AddRow();
                    dynReq.Tables["TBL_DIAG"].Rows[i]["DIAG_NM"].Value = data.RWI001_LIST[idx].DXD[i]; // 진단명
                }

                // B. 중환자실 입.퇴실 정보
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("CHRG_DR_NM"); // 담당의사 성명
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("DGSBJT_CD"); // 진료과
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("IFLD_DTL_SPC_SBJT_CD"); // 내과세부진료과목
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("WRTP_NM"); // 중환자실기록자료를 작성한 의사의 성명
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("SPRM_IPAT_DT"); // 중환자실 입실일시
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("SPRM_IPAT_PTH_CD"); // 중환자실 입실경로
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("IPAT_PTH_ETC_TXT"); // 입실경로 기타상세
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("SPRM_IPAT_RS_CD"); // 입실사유
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("RE_IPAT_RS_TXT"); // 입실사유 재입실사유
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("IPAT_RS_ETC_TXT"); // 입실사유 기타상세
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("SPRM_DSCG_RST_CD"); // 퇴실형태
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("DSCG_RST_TXT"); // 퇴실현황 기타 상세
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("DEATH_DT"); // 사망일시
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("DEATH_SICK_SYM"); // 원사인 상병분류기호
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("DEATH_DIAG_NM"); // 사망 진단명
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("SPRM_DSCG_DT"); // 퇴실일시


                dynReq.Tables["TBL_IPAT_DSCG"].AddRow();
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["CHRG_DR_NM"].Value = data.RWI001_LIST[idx].CHRG_DR_NM; // 담당의사 성명
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["DGSBJT_CD"].Value = data.RWI001_LIST[idx].INSDPTCD; // 진료과
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["IFLD_DTL_SPC_SBJT_CD"].Value = data.RWI001_LIST[idx].INSDPTCD2; // 내과세부진료과목
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["WRTP_NM"].Value = data.RWI001_LIST[idx].WRTP_NM; // 중환자실기록자료를 작성한 의사의 성명
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["SPRM_IPAT_DT"].Value = data.RWI001_LIST[idx].SPRM_IPAT_DT; // 중환자실 입실일시
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["SPRM_IPAT_PTH_CD"].Value = data.RWI001_LIST[idx].SPRM_IPAT_PTH_CD; // 중환자실 입실경로
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["IPAT_PTH_ETC_TXT"].Value = data.RWI001_LIST[idx].IPAT_PTH_ETC_TXT; // 입실경로 기타상세
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["SPRM_IPAT_RS_CD"].Value = data.RWI001_LIST[idx].SPRM_IPAT_RS_CD; // 입실사유
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["RE_IPAT_RS_TXT"].Value = data.RWI001_LIST[idx].RE_IPAT_RS_TXT; // 입실사유 재입실사유
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["IPAT_RS_ETC_TXT"].Value = data.RWI001_LIST[idx].IPAT_RS_ETC_TXT; // 입실사유 기타상세
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["SPRM_DSCG_RST_CD"].Value = data.RWI001_LIST[idx].SPRM_DSCG_RST_CD; // 퇴실형태
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["DSCG_RST_TXT"].Value = data.RWI001_LIST[idx].DSCG_RST_TXT; // 퇴실현황 기타 상세
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["DEATH_DT"].Value = data.RWI001_LIST[idx].DEATH_DT; // 사망일시
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["DEATH_SICK_SYM"].Value = data.RWI001_LIST[idx].DEATH_SICK_SYM; // 원사인 상병분류기호
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["DEATH_DIAG_NM"].Value = data.RWI001_LIST[idx].DEATH_DIAG_NM; // 사망 진단명
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["SPRM_DSCG_DT"].Value = data.RWI001_LIST[idx].SPRM_DSCG_DT; // 퇴실일시

                // C.중환자실 관찰 기록

                // D.기타 정보
                dynReq.Elements["ATFL_RPRT_ENFC_YN"].Value = data.RWI001_LIST[idx].ATFL_RPRT_ENFC_YN; // 인공호흡기 적용여부
                dynReq.Elements["OXY_CURE_YN"].Value = data.RWI001_LIST[idx].OXY_CURE_YN; // 산소요법 적용여부
                dynReq.Elements["CNNL_ENFC_YN"].Value = data.RWI001_LIST[idx].CNNL_ENFC_YN; // 삽입관 적용여부
                dynReq.Elements["DRN_ENFC_YN"].Value = data.RWI001_LIST[idx].DRN_ENFC_YN; // 배액관 적용여부

                // 5.특수처치
                dynReq.Tables["TBL_SPCL_TRET"].Columns.Add("SPCL_TRET_CD"); // 특수처치
                dynReq.Tables["TBL_SPCL_TRET"].Columns.Add("SPCL_TRET_ETC_TXT"); // 특수처치 기타 상세

                dynReq.Tables["TBL_SPCL_TRET"].AddRow();
                dynReq.Tables["TBL_SPCL_TRET"].Rows[0]["SPCL_TRET_CD"].Value = data.RWI001_LIST[idx].SPCL_TRET_CD;
                dynReq.Tables["TBL_SPCL_TRET"].Rows[0]["SPCL_TRET_ETC_TXT"].Value = data.RWI001_LIST[idx].SPCL_TRET_ETC_TXT;

                //6. Continuous Monitor
                dynReq.Tables["TBL_CNTN_MNTR"].Columns.Add("MNTR_KND_CD");

                dynReq.Tables["TBL_CNTN_MNTR"].AddRow();
                dynReq.Tables["TBL_CNTN_MNTR"].Rows[0]["MNTR_KND_CD"].Value = data.RWI001_LIST[idx].MNTR_KND_CD;

                // 8. 중증도 분류
                dynReq.Elements["SGRD_PNT_YN"].Value = data.RWI001_LIST[idx].SGRD_PNT_YN; // 중증도 점수 여부
                dynReq.Elements["SGRD_RVSN_TL_CD"].Value = data.RWI001_LIST[idx].SGRD_RVSN_TL_CD; // 종류
                dynReq.Elements["SGRD_RVSN_TL_TXT"].Value = data.RWI001_LIST[idx].SGRD_RVSN_TL_TXT; // 중증도 보정도구 종류상세




                // requestData - createDoc    최종 제출
                //               createTmpDoc 임시 제출
                //               updateTmpDoc 수정 제출
                //               selectDocList 목록 조회
                //               selectDocLink 미리보기
                //               selectDoc     상세조회
                HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(m_Hosid, isTmp ? "createTmpDoc" : "createDoc");
                if (!dynRes.Result)
                {
                    data.RWI001_RESULT.STATUS = "E"; // 오류
                    data.RWI001_RESULT.ERR_CODE = dynRes.ErrorCode;
                    data.RWI001_RESULT.ERR_DESC = dynRes.ErrorMessage;

                    data.RWI001_RESULT.DOC_NO = ""; //문서번호
                    data.RWI001_RESULT.SUPL_DATA_FOM_CD = ""; //서식코드
                    data.RWI001_RESULT.RCV_NO = ""; //접수번호
                    data.RWI001_RESULT.SP_SNO = ""; //명세서일련번호
                    data.RWI001_RESULT.HOSP_RNO = ""; //환자등록번호
                    data.RWI001_RESULT.PAT_NM = ""; //환자성명
                    data.RWI001_RESULT.INSUP_TP_CD = ""; //참고업무구분

                }
                else
                {
                    data.RWI001_RESULT.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                    data.RWI001_RESULT.ERR_CODE = "";
                    data.RWI001_RESULT.ERR_DESC = "";

                    data.RWI001_RESULT.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                    data.RWI001_RESULT.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                    data.RWI001_RESULT.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                    data.RWI001_RESULT.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                    data.RWI001_RESULT.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                    data.RWI001_RESULT.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                    data.RWI001_RESULT.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분
                }

                // 전송내역을 저장한다.
                SaveToTI86_RWI001(data, isTmp, p_sysdt, p_systm, p_conn);
            }

        }

        private void SaveToTI86_RWI001(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
        }
    }
}
