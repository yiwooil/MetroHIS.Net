using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CHiraRWN001
    {
        private string m_Hosid;
        private string m_Demno;
        private string m_Cnecno;
        private string m_Cnectdd;
        private string m_BillSno;

        public CHiraRWN001(string p_hosid, string p_demno, string p_cnecno, string p_cnectdd, string p_billsno)
        {
            m_Hosid = p_hosid;
            m_Demno = p_demno;
            m_Cnecno = p_cnecno;
            m_Cnectdd = p_cnectdd;
            m_BillSno = p_billsno;
        }

        public void Send(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
            if (data.RWN001_LIST.Count < 1) return;

            for (int idx = 0; idx < data.RWN001_LIST.Count; idx++)
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

                // A. 신생아중환자실 정보
                dynReq.Elements["BIRTH_YN"].Value = data.RWN001_LIST[idx].BIRTH_YN; // 출생일 확인 여부(1.Yes 2.No)
                dynReq.Elements["BIRTH_DT"].Value = data.RWN001_LIST[idx].BIRTH_DT; // 출생일시
                dynReq.Elements["BIRTH_PLC_CD"].Value = data.RWN001_LIST[idx].BIRTH_PLC_CD; // 출생장소(1.본원 2.타기관 9.기타)
                dynReq.Elements["BIRTH_PLC_ETC_TXT"].Value = data.RWN001_LIST[idx].BIRTH_PLC_ETC_TXT; //출생장소 기타상세(출생장소가 9 기타일 경우)
                dynReq.Elements["PARTU_FRM_CD"].Value = data.RWN001_LIST[idx].PARTU_FRM_CD; // 분만형태(1.자연분문 2.제왕절개 9.기타)
                dynReq.Elements["PARTU_FRM_ETC_TXT"].Value = data.RWN001_LIST[idx].PARTU_FRM_ETC_TXT; // 분만형태 기타상세(분만형태가 9 기타일 경우)
                dynReq.Elements["FTUS_DEV_TRM"].Value = data.RWN001_LIST[idx].FTUS_DEV_TRM; // 재태기간(  주  일)형태로
                dynReq.Elements["MEMB_YN"].Value = data.RWN001_LIST[idx].MEMB_YN; // 다태아여부(1.Yes 2.No)
                dynReq.Elements["MEMB_TXT"].Value = data.RWN001_LIST[idx].MEMB_TXT; // 다태아내용(다태아여부 1 Yes 일경우)(예시 세 쌍둥이 중 첫째 아기인 경우 3/1)
                dynReq.Elements["APSC_YN"].Value = data.RWN001_LIST[idx].APSC_YN; // Apgar Score(1.Yes 2.No)
                dynReq.Elements["APSC_PNT"].Value = data.RWN001_LIST[idx].APSC_PNT; // Apgar Score 내용 (Apgar Score 1 Yes 일경우)(예시 1분 2점, 5분 8점인 경우:2/8)
                dynReq.Elements["NBY_BIRTH_BWGT"].Value = data.RWN001_LIST[idx].NBY_BIRTH_BWGT; // 출생시체중
                dynReq.Elements["FST_IPAT_DT"].Value = data.RWN001_LIST[idx].FST_IPAT_DT; // 최초입실일시(ccyymmddhhmm)

                // B. 신생아중환자실 입.퇴실 정보
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("CHRG_DR_NM"); // 담당의 성명
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("CHRG_DR_NM"); // 담당의 성명
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("WRTP_NM"); // 작성자성명
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("SPRM_IPAT_DT"); // 입실일시(ccyymmddhhmm)
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("SPRM_IPAT_PTH_CD"); // 입실경로(1.수술실 2.응급실 3.외래 4.분만실 5.타병동에서 전실, 9.기타)
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("IPAT_PTH_ETC_TXT"); // 입실경로 기타상세(입실경로 9 기타일경우 입실경로 평문기재)
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("NBY_IPAT_RS_CD"); // 입실사유(1.미숙아 집중관찰 2.저체중 출생아 집중관찰 3.재태기간이나 출생체중과 관계없이 환아의 상태가 위중한 경우 4.특별한 처치 또는 관리가 필요한 경우 5.의료진의 치료 계획에 따라 예정된 재입실 9.기타)
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("RE_IPAT_RS_TXT"); // 입실사유 재입실상세(입실사유 5 일경우 평문)
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("IPAT_RS_ETC_TXT"); // 입실사유 기타상세(입실사유 9 기타일경우 평문)
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("IPAT_NBY_BWGT"); // 입실시 체중
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("SPRM_DSCG_RST_CD"); // 퇴실상태(01.퇴원 02.전실(전동) 03.전실(ICU) 04.전실(신생아실) 05.전원 06.사망 07.뇌사판정(이식) 08.계속 입원 99.기타)
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("DSCG_RST_ETC_TXT"); //퇴실상태 기타상세(퇴실상태 99 기타일경우 평문)
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("DEATH_DT"); // 사망일시(ccyymmddhhmm)(퇴실상태가 06.사망인 경우)
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("DEATH_SICK_SYM"); // 원사인 상병분류기호(퇴실상태가 06.사망인 경우)
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("DEATH_DIAG_NM"); // 사망진단명(퇴실상태가 06.사망인 경우)
                dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("SPRM_DSCG_DT"); //퇴실일시(ccyymmddhhmm)

                dynReq.Tables["TBL_IPAT_DSCG"].AddRow();
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["CHRG_DR_NM"].Value = data.RWN001_LIST[idx].CHRG_DR_NM; // 담당의 성명
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["WRTP_NM"].Value = data.RWN001_LIST[idx].WRTP_NM; // 작성자성명
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["SPRM_IPAT_DT"].Value = data.RWN001_LIST[idx].SPRM_IPAT_DT; // 입실일시(ccyymmddhhmm)
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["SPRM_IPAT_PTH_CD"].Value = data.RWN001_LIST[idx].SPRM_IPAT_PTH_CD; // 입실경로(1.수술실 2.응급실 3.외래 4.분만실 5.타병동에서 전실, 9.기타)
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["IPAT_PTH_ETC_TXT"].Value = data.RWN001_LIST[idx].IPAT_PTH_ETC_TXT; // 입실경로 기타상세(입실경로 9 기타일경우 입실경로 평문기재)
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["NBY_IPAT_RS_CD"].Value = data.RWN001_LIST[idx].NBY_IPAT_RS_CD; // 입실사유(1.미숙아 집중관찰 2.저체중 출생아 집중관찰 3.재태기간이나 출생체중과 관계없이 환아의 상태가 위중한 경우 4.특별한 처치 또는 관리가 필요한 경우 5.의료진의 치료 계획에 따라 예정된 재입실 9.기타)
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["RE_IPAT_RS_TXT"].Value = data.RWN001_LIST[idx].RE_IPAT_RS_TXT; // 입실사유 재입실상세(입실사유 5 일경우 평문)
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["IPAT_RS_ETC_TXT"].Value = data.RWN001_LIST[idx].IPAT_RS_ETC_TXT; // 입실사유 기타상세(입실사유 9 기타일경우 평문)
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["IPAT_NBY_BWGT"].Value = data.RWN001_LIST[idx].IPAT_NBY_BWGT; // 입실시 체중
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["SPRM_DSCG_RST_CD"].Value = data.RWN001_LIST[idx].SPRM_DSCG_RST_CD; // 퇴실상태(01.퇴원 02.전실(전동) 03.전실(ICU) 04.전실(신생아실) 05.전원 06.사망 07.뇌사판정(이식) 08.계속 입원 99.기타)
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["DSCG_RST_ETC_TXT"].Value = data.RWN001_LIST[idx].DSCG_RST_ETC_TXT; //퇴실상태 기타상세(퇴실상태 99 기타일경우 평문)
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["DEATH_DT"].Value = data.RWN001_LIST[idx].DEATH_DT; // 사망일시(ccyymmddhhmm)(퇴실상태가 06.사망인 경우)
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["DEATH_SICK_SYM"].Value = data.RWN001_LIST[idx].DEATH_SICK_SYM; // 원사인 상병분류기호(퇴실상태가 06.사망인 경우)
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["DEATH_DIAG_NM"].Value = data.RWN001_LIST[idx].DEATH_DIAG_NM; // 사망진단명(퇴실상태가 06.사망인 경우)
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[0]["SPRM_DSCG_DT"].Value = data.RWN001_LIST[idx].SPRM_DSCG_DT; //퇴실일시(ccyymmddhhmm)

                // C. 신생아중환자실 관찰 기록

                // D. 기타 정보
                dynReq.Elements["ATFL_RPRT_YN"].Value = data.RWN001_LIST[idx].ATFL_RPRT_YN; // 인공호흡기 적용 여부(1.Yes 2.No)
                dynReq.Elements["OXY_CURE_YN"].Value = data.RWN001_LIST[idx].OXY_CURE_YN; // 산소요법 적용 여부(1.Yes 2.No)
                dynReq.Elements["CNNL_YN"].Value = data.RWN001_LIST[idx].CNNL_YN; // 삽입관 및 배액관 적용여부(1.Yes 2.No)

                dynReq.Tables["TBL_CNNL"].Columns.Add("CNNL_KND_CD"); // 삽입관 및 배액관 종류(01.Um....)
                dynReq.Tables["TBL_CNNL"].Columns.Add("CNNL_KND_ETC_TXT"); // 삽입관 및 배액관유형-기타상세(99기타인경우 평문)
                dynReq.Tables["TBL_CNNL"].Columns.Add("CNNL_INS_DT"); // 삽입일시(ccyymmddhhmm)
                dynReq.Tables["TBL_CNNL"].Columns.Add("CNNL_DEL_DT"); // 제거일시(ccyymmddhhmm)

                for (int i = 0; i < data.RWN001_LIST[idx].CNNL_KND_CD.Count; i++)
                {
                    dynReq.Tables["TBL_CNNL"].AddRow();
                    dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["CNNL_KND_CD"].Value = data.RWN001_LIST[idx].CNNL_KND_CD[i]; // 삽입관 및 배액관 종류(01.Umbilical venous catheter 02.Umbilical arterial catheter 03.Peripherally inserted central catheter 04.Arterial catheter 05.Central venous catheter 06.Tracheostomy 07.Endotracheal tube 99.기타)
                    dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["CNNL_KND_ETC_TXT"].Value = data.RWN001_LIST[idx].CNNL_KND_ETC_TXT[i]; // 삽입관 및 배액관유형-기타상세(99기타인경우 평문)
                    dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["CNNL_INS_DT"].Value = data.RWN001_LIST[idx].CNNL_INS_DT[i]; // 삽입일시(ccyymmddhhmm)
                    dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["CNNL_DEL_DT"].Value = data.RWN001_LIST[idx].CNNL_DEL_DT[i]; // 제거일시(ccyymmddhhmm)
                }

                dynReq.Elements["OSTM_YN"].Value = data.RWN001_LIST[idx].OSTM_YN; // 장루유무(1.Yes 2.No 9.기타)
                dynReq.Elements["OSTM_ETC_TXT"].Value = data.RWN001_LIST[idx].OSTM_ETC_TXT; // 장루유무-기타상세(장루유무가 9기타인경우 평문)
                dynReq.Elements["ETC_DSPL_CD"].Value = data.RWN001_LIST[idx].ETC_DSPL_CD; // 기타처치 시행여부(00.해당없음 01.광선요법 02.저체온요법 03.하기도 증기흡입요법 04.교환수혈 05.심폐소생술 99.기타)


                // E. 추가 정보


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
                SaveToTI86_RWN001(data, isTmp, p_sysdt, p_systm, p_conn);
            }
        }

        private void SaveToTI86_RWN001(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
        }
    }
}
