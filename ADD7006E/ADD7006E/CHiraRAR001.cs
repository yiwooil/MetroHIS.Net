using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CHiraRAR001
    {
        private string m_Hosid;
        private string m_Demno;
        private string m_Cnecno;
        private string m_Cnectdd;
        private string m_BillSno;

        public CHiraRAR001(string p_hosid, string p_demno, string p_cnecno, string p_cnectdd, string p_billsno)
        {
            m_Hosid = p_hosid;
            m_Demno = p_demno;
            m_Cnecno = p_cnecno;
            m_Cnectdd = p_cnectdd;
            m_BillSno = p_billsno;
        }

        public void Send(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
            if (data.RAR001_LIST.Count < 1) return;

            for (int idx = 0; idx < data.RAR001_LIST.Count; idx++)
            {
                HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

                string qfycd = data.QFYCD;
                string insup_tp_cd = "";
                if (qfycd.StartsWith("29")) insup_tp_cd = "7"; // 보훈
                else if (qfycd.StartsWith("6")) insup_tp_cd = "8"; // 자보
                else if (qfycd.StartsWith("3")) insup_tp_cd = "5"; // 보호
                else insup_tp_cd = "4"; // 보험

                // 문서 공통 정보
                dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "RAR001"; //서식코드: 중환자실기록자료
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
                dynReq.Elements["RCRM_IPAT_DT"].Value = data.RAR001_LIST[idx].PT_INDTM; // 회복실 도착일시
                dynReq.Elements["RCRM_DSCG_DT"].Value = data.RAR001_LIST[idx].PT_OUTDTM; // 회복실 퇴실일시
                dynReq.Elements["DSCG_DEC_DR_NM"].Value = data.RAR001_LIST[idx].ANDRNM; // 퇴실 결정의사 성명
                dynReq.Elements["WRTP_NM"].Value = data.RAR001_LIST[idx].EMPNM; // 작성자 성명
                dynReq.Elements["WRT_DT"].Value = data.RAR001_LIST[idx].WDTM; // 작성일시

                // B.세부정보
                // 1.활력징후
                dynReq.Tables["TBL_VTSG"].Columns.Add("MASR_DT"); // 측정일시
                dynReq.Tables["TBL_VTSG"].Columns.Add("BPRSU"); // 혈압
                dynReq.Tables["TBL_VTSG"].Columns.Add("PULS"); // 맥박
                dynReq.Tables["TBL_VTSG"].Columns.Add("BRT"); // 호흡
                dynReq.Tables["TBL_VTSG"].Columns.Add("TMPR"); // 체온
                dynReq.Tables["TBL_VTSG"].Columns.Add("OXY_STRT"); // 산소포화도
                dynReq.Tables["TBL_VTSG"].Columns.Add("VTSG_TXT"); // 특이사항

                if (data.RAR001_LIST[idx].VTSG_DT.Count < 1)
                {
                    dynReq.Tables["TBL_VTSG"].AddRow();
                    dynReq.Tables["TBL_VTSG"].Rows[0]["MASR_DT"].Value = "-";
                    dynReq.Tables["TBL_VTSG"].Rows[0]["BPRSU"].Value = "-";
                    dynReq.Tables["TBL_VTSG"].Rows[0]["PULS"].Value = "-";
                    dynReq.Tables["TBL_VTSG"].Rows[0]["BRT"].Value = "-";
                    dynReq.Tables["TBL_VTSG"].Rows[0]["TMPR"].Value = "-";
                    dynReq.Tables["TBL_VTSG"].Rows[0]["OXY_STRT"].Value = "-";
                    dynReq.Tables["TBL_VTSG"].Rows[0]["VTSG_TXT"].Value = "-";
                }
                else
                {
                    for (int i = 0; i < data.RAR001_LIST[idx].VTSG_DT.Count; i++)
                    {
                        dynReq.Tables["TBL_VTSG"].AddRow();
                        dynReq.Tables["TBL_VTSG"].Rows[i]["MASR_DT"].Value = data.RAR001_LIST[idx].MASR_DT(i);
                        dynReq.Tables["TBL_VTSG"].Rows[i]["BPRSU"].Value = data.RAR001_LIST[idx].BP(i);
                        dynReq.Tables["TBL_VTSG"].Rows[i]["PULS"].Value = data.RAR001_LIST[idx].HR[i];
                        dynReq.Tables["TBL_VTSG"].Rows[i]["BRT"].Value = data.RAR001_LIST[idx].RR[i];
                        dynReq.Tables["TBL_VTSG"].Rows[i]["TMPR"].Value = data.RAR001_LIST[idx].BT[i];
                        dynReq.Tables["TBL_VTSG"].Rows[i]["OXY_STRT"].Value = data.RAR001_LIST[idx].SPO2[i];
                        dynReq.Tables["TBL_VTSG"].Rows[i]["VTSG_TXT"].Value = data.RAR001_LIST[idx].RMK[i];
                    }
                }

                // 2.약제투여

                // 3.평가
                // 1)통증평가
                dynReq.Elements["PAIN_ASM_YN"].Value = data.RAR001_LIST[idx].PAINCASE_YN; // 통증평가실시여부

                // 통증평가상세
                if (data.RAR001_LIST[idx].PAINDT1 != "" || data.RAR001_LIST[idx].PAINDT2 != "")
                {
                    dynReq.Tables["TBL_PAIN_ASM"].Columns.Add("EXEC_DT"); // 평가일시
                    dynReq.Tables["TBL_PAIN_ASM"].Columns.Add("PAIN_ASM_TL_CD"); // 통증평가도구
                    dynReq.Tables["TBL_PAIN_ASM"].Columns.Add("ASM_TL_ETC_TXT"); // 도구상세
                    dynReq.Tables["TBL_PAIN_ASM"].Columns.Add("ASM_RST_TXT"); // 결과

                    int ii = 0;
                    if (data.RAR001_LIST[idx].PAINDT1 != "")
                    {
                        dynReq.Tables["TBL_PAIN_ASM"].AddRow();
                        dynReq.Tables["TBL_PAIN_ASM"].Rows[ii]["EXEC_DT"].Value = data.RAR001_LIST[idx].PAINDT1;
                        dynReq.Tables["TBL_PAIN_ASM"].Rows[ii]["PAIN_ASM_TL_CD"].Value = data.RAR001_LIST[idx].PAINCASE_TOOL1;
                        dynReq.Tables["TBL_PAIN_ASM"].Rows[ii]["ASM_TL_ETC_TXT"].Value = data.RAR001_LIST[idx].PAINCASE_TOOL_DETAIL1;
                        dynReq.Tables["TBL_PAIN_ASM"].Rows[ii]["ASM_RST_TXT"].Value = data.RAR001_LIST[idx].PAINCASE_RESULT1;
                        ii++;
                    }
                    if (data.RAR001_LIST[idx].PAINDT2 != "")
                    {
                        dynReq.Tables["TBL_PAIN_ASM"].AddRow();
                        dynReq.Tables["TBL_PAIN_ASM"].Rows[ii]["EXEC_DT"].Value = data.RAR001_LIST[idx].PAINDT2;
                        dynReq.Tables["TBL_PAIN_ASM"].Rows[ii]["PAIN_ASM_TL_CD"].Value = data.RAR001_LIST[idx].PAINCASE_TOOL2;
                        dynReq.Tables["TBL_PAIN_ASM"].Rows[ii]["ASM_TL_ETC_TXT"].Value = data.RAR001_LIST[idx].PAINCASE_TOOL_DETAIL2;
                        dynReq.Tables["TBL_PAIN_ASM"].Rows[ii]["ASM_RST_TXT"].Value = data.RAR001_LIST[idx].PAINCASE_RESULT2;
                    }
                }

                // 2)오심구토평가
                dynReq.Elements["EMSS_ASM_YN"].Value = data.RAR001_LIST[idx].VOM_YN; // 오심구토평가실시여부

                // 오심구토평가상세
                if (data.RAR001_LIST[idx].EMSSDT1 != "" || data.RAR001_LIST[idx].EMSSDT2 != "")
                {
                    dynReq.Tables["TBL_EMSS_ASM"].Columns.Add("EXEC_DT"); // 평가일시
                    dynReq.Tables["TBL_EMSS_ASM"].Columns.Add("ASM_RST_TXT"); // 결과

                    int ii = 0;
                    if (data.RAR001_LIST[idx].EMSSDT1 != "")
                    {
                        dynReq.Tables["TBL_EMSS_ASM"].AddRow();
                        dynReq.Tables["TBL_EMSS_ASM"].Rows[ii]["EXEC_DT"].Value = data.RAR001_LIST[idx].EMSSDT1;
                        dynReq.Tables["TBL_EMSS_ASM"].Rows[ii]["ASM_RST_TXT"].Value = data.RAR001_LIST[idx].ASM_RST_TXT1;
                        ii++;
                    }
                    if (data.RAR001_LIST[idx].EMSSDT2 != "")
                    {
                        dynReq.Tables["TBL_EMSS_ASM"].AddRow();
                        dynReq.Tables["TBL_EMSS_ASM"].Rows[ii]["EXEC_DT"].Value = data.RAR001_LIST[idx].EMSSDT2;
                        dynReq.Tables["TBL_EMSS_ASM"].Rows[ii]["ASM_RST_TXT"].Value = data.RAR001_LIST[idx].ASM_RST_TXT2;
                    }
                }

                // 4.PCA
                dynReq.Elements["PCA_TXT"].Value = data.RAR001_LIST[idx].PCA; // PCA

                // C.마취회복점수
                if (data.RAR001_LIST[idx].PT_INDTM != "" || data.RAR001_LIST[idx].PT_OUTDTM != "")
                {
                    dynReq.Tables["TBL_NCT_RCOV"].Columns.Add("MASR_DT"); // 측정일시
                    dynReq.Tables["TBL_NCT_RCOV"].Columns.Add("ACTV_PNT"); // 활동성 점수(0~2)
                    dynReq.Tables["TBL_NCT_RCOV"].Columns.Add("BRT_PNT"); // 호흡 점수(0~2)
                    dynReq.Tables["TBL_NCT_RCOV"].Columns.Add("CRCL_PNT"); // 순환 점수(0~2)
                    dynReq.Tables["TBL_NCT_RCOV"].Columns.Add("CNSCS_PNT"); // 의식 점수(0~2)
                    dynReq.Tables["TBL_NCT_RCOV"].Columns.Add("SKN_COLR_PNT"); // 피부색 점수(0~2)
                    dynReq.Tables["TBL_NCT_RCOV"].Columns.Add("TOT_PNT"); // 합계

                    int ii = 0;
                    if (data.RAR001_LIST[idx].PT_INDTM != "")
                    {
                        dynReq.Tables["TBL_NCT_RCOV"].AddRow();
                        dynReq.Tables["TBL_NCT_RCOV"].Rows[ii]["MASR_DT"].Value = data.RAR001_LIST[idx].PT_INDTM;
                        dynReq.Tables["TBL_NCT_RCOV"].Rows[ii]["ACTV_PNT"].Value = data.RAR001_LIST[idx].PARSCR1_1;
                        dynReq.Tables["TBL_NCT_RCOV"].Rows[ii]["BRT_PNT"].Value = data.RAR001_LIST[idx].PARSCR1_2;
                        dynReq.Tables["TBL_NCT_RCOV"].Rows[ii]["CRCL_PNT"].Value = data.RAR001_LIST[idx].PARSCR1_3;
                        dynReq.Tables["TBL_NCT_RCOV"].Rows[ii]["CNSCS_PNT"].Value = data.RAR001_LIST[idx].PARSCR1_4;
                        dynReq.Tables["TBL_NCT_RCOV"].Rows[ii]["SKN_COLR_PNT"].Value = data.RAR001_LIST[idx].PARSCR1_5;
                        dynReq.Tables["TBL_NCT_RCOV"].Rows[ii]["TOT_PNT"].Value = data.RAR001_LIST[idx].PARSCR1_SUM;
                        ii++;
                    }
                    if (data.RAR001_LIST[idx].PT_OUTDTM != "")
                    {
                        dynReq.Tables["TBL_NCT_RCOV"].AddRow();
                        dynReq.Tables["TBL_NCT_RCOV"].Rows[ii]["MASR_DT"].Value = data.RAR001_LIST[idx].PT_OUTDTM;
                        dynReq.Tables["TBL_NCT_RCOV"].Rows[ii]["ACTV_PNT"].Value = data.RAR001_LIST[idx].PARSCR2_1;
                        dynReq.Tables["TBL_NCT_RCOV"].Rows[ii]["BRT_PNT"].Value = data.RAR001_LIST[idx].PARSCR2_2;
                        dynReq.Tables["TBL_NCT_RCOV"].Rows[ii]["CRCL_PNT"].Value = data.RAR001_LIST[idx].PARSCR2_3;
                        dynReq.Tables["TBL_NCT_RCOV"].Rows[ii]["CNSCS_PNT"].Value = data.RAR001_LIST[idx].PARSCR2_4;
                        dynReq.Tables["TBL_NCT_RCOV"].Rows[ii]["SKN_COLR_PNT"].Value = data.RAR001_LIST[idx].PARSCR2_5;
                        dynReq.Tables["TBL_NCT_RCOV"].Rows[ii]["TOT_PNT"].Value = data.RAR001_LIST[idx].PARSCR2_SUM;
                    }
                }

                // D.회복중 특이사항


                // requestData - createDoc    최종 제출
                //               createTmpDoc 임시 제출
                //               updateTmpDoc 수정 제출
                //               selectDocList 목록 조회
                //               selectDocLink 미리보기
                //               selectDoc     상세조회
                HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(m_Hosid, isTmp ? "createTmpDoc" : "createDoc");
                if (!dynRes.Result)
                {
                    data.RAR001_RESULT.STATUS = "E"; // 오류
                    data.RAR001_RESULT.ERR_CODE = dynRes.ErrorCode;
                    data.RAR001_RESULT.ERR_DESC = dynRes.ErrorMessage;

                    data.RAR001_RESULT.DOC_NO = ""; //문서번호
                    data.RAR001_RESULT.SUPL_DATA_FOM_CD = ""; //서식코드
                    data.RAR001_RESULT.RCV_NO = ""; //접수번호
                    data.RAR001_RESULT.SP_SNO = ""; //명세서일련번호
                    data.RAR001_RESULT.HOSP_RNO = ""; //환자등록번호
                    data.RAR001_RESULT.PAT_NM = ""; //환자성명
                    data.RAR001_RESULT.INSUP_TP_CD = ""; //참고업무구분

                }
                else
                {
                    data.RAR001_RESULT.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                    data.RAR001_RESULT.ERR_CODE = "";
                    data.RAR001_RESULT.ERR_DESC = "";

                    data.RAR001_RESULT.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                    data.RAR001_RESULT.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                    data.RAR001_RESULT.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                    data.RAR001_RESULT.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                    data.RAR001_RESULT.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                    data.RAR001_RESULT.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                    data.RAR001_RESULT.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분
                }

                // 전송내역을 저장한다.
                SaveToTI86_RAR001(data, isTmp, p_sysdt, p_systm, p_conn);
            }

        }

        private void SaveToTI86_RAR001(CData data, bool isTmp, string p_sysdt, string p_systm, OleDbConnection p_conn)
        {
        }
    }
}
