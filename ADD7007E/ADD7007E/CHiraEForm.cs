using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    class CHiraEForm
    {
        public string Send(CData data, bool isTmp, string p_sysdt, string p_systm, string p_user, string hosid, OleDbConnection p_conn)
        {
            if (data.SEL == false) return "";
            if (data.DELYN == "Y") return "";

            data.STATUS = "N"; // 진행중

            if (data is CDataASM002_002)
            {
                // 관상동맥우회술
                return Send((CDataASM002_002)data, isTmp, p_sysdt, p_systm, p_user, hosid, p_conn);
            }
            else if (data is CDataASM003_002)
            {
                // 급성기뇌졸증
                return Send((CDataASM003_002)data, isTmp, p_sysdt, p_systm, p_user, hosid, p_conn);
            }
            else if (data is CDataASM035_003)
            {
                // 마취
                return Send((CDataASM035_003)data, isTmp, p_sysdt, p_systm, p_user, hosid, p_conn);
            }
            else if (data is CDataASM010_002)
            {
                // 수술의예방학적항생제사용
                return Send((CDataASM010_002)data, isTmp, p_sysdt, p_systm, p_user, hosid, p_conn);
            }
            else if (data is CDataASM037_003)
            {
                // 수혈
                return Send((CDataASM037_003)data, isTmp, p_sysdt, p_systm, p_user, hosid, p_conn);
            }
            else if (data is CDataASM033_003)
            {
                // 신생아중환자실
                return Send((CDataASM033_003)data, isTmp, p_sysdt, p_systm, p_user, hosid, p_conn);
            }
            else if (data is CDataASM049_001)
            {
                // 영상검사
                return Send((CDataASM049_001)data, isTmp, p_sysdt, p_systm, p_user, hosid, p_conn);
            }
            else if (data is CDataASM014_001)
            {
                // 의료급여정신과
                return Send((CDataASM014_001)data, isTmp, p_sysdt, p_systm, p_user, hosid, p_conn);
            }
            else if (data is CDataASM036_002)
            {
                // 정신건강입원영역
                return Send((CDataASM036_002)data, isTmp, p_sysdt, p_systm, p_user, hosid, p_conn);
            }
            else if (data is CDataASM024_002)
            {
                // 중환자실
                return Send((CDataASM024_002)data, isTmp, p_sysdt, p_systm, p_user, hosid, p_conn);
            }
            else if (data is CDataASM023_002)
            {
                // 폐렴
                return Send((CDataASM023_002)data, isTmp, p_sysdt, p_systm, p_user, hosid, p_conn);
            }
            else if (data is CDataASM008_002)
            {
                // 혈액투석
                string ret = "";
                ret = Send((CDataASM008_002)data, isTmp, p_sysdt, p_systm, p_user, hosid, p_conn);
                ret = Send_ERD001((CDataASM008_002)data, isTmp, p_sysdt, p_systm, p_user, hosid, p_conn);
                ret = Send_ERR001((CDataASM008_002)data, isTmp, p_sysdt, p_systm, p_user, hosid, p_conn);
                return ret;
            }
            else
            {
                throw new ArgumentException("알 수 없는 양식입니다.");
            }

        }

        // 관상동맥우회술
        public string Send(CDataASM002_002 data, bool isTmp, string p_sysdt, string p_systm, string p_user, string hosid, OleDbConnection p_conn)
        {
            if (data.SEL == false) return "";

            data.STATUS = "N"; // 진행중

            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

            // 문서 공통 정보
            //dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = data.form; //서식코드
            //dynReq.Metadata["FOM_VER"].Value = data.ver; // 서식버전
            //dynReq.Metadata["YKIHO"].Value = hosid; // 요양기관기호
            //dynReq.Metadata["DMD_NO"].Value = data.DEMNO; // 청구번호
            //dynReq.Metadata["RCV_NO"].Value = data.CNECNO; // 접수번호(없는 경우 0000000)
            //dynReq.Metadata["RCV_YR"].Value = data.CNECTDD.Substring(0, 4); // 접수년도
            //dynReq.Metadata["BILL_SNO"].Value = data.BILLSNO; // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)
            //dynReq.Metadata["SP_SNO"].Value = data.EPRTNO; // 명일련
            //dynReq.Metadata["INSUP_TP_CD"].Value = data.INSUP_TP_CD; // 보험자구분코드(4.보험 5.보호 7.보훈 8.자보)
            //dynReq.Metadata["FOM_REF_BIZ_TP_CD"].Value = data.buss_cd; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
            //dynReq.Metadata["DTL_BIZ_CD"].Value = data.buss_detail; // 업무상세코드
            //dynReq.Metadata["REQ_DATA_NO"].Value = ""; // 요청번호(참고업무 구분코드가 '03: 이의신청, 05: 진료비민원, 06:신포괄, 09: 진료의뢰회송‘인 경우 필수 기재)
            //dynReq.Metadata["RE_RV_RCV_ID"].Value = ""; // 재심사접수ID(1)업무상세코드가 이의신청: SUP(보완)인 경우 필수 기재)
            //dynReq.Metadata["HOSP_RNO"].Value = data.PID; // 환자등록번호.병원에서 부여한 번호
            //dynReq.Metadata["PAT_NM"].Value = data.PNM; // 환자명
            //dynReq.Metadata["PAT_JNO"].Value = data.RESID; // 주민번호

            // 문서 공통 정보
            SetCommonMetadata(dynReq, data, hosid);


            // A. 환자정보
            dynReq.Elements["ASM_IPAT_DT"].Value = data.ASM_IPAT_DT;
            dynReq.Elements["ASM_VST_PTH_CD"].Value = data.ASM_VST_PTH_CD;
            dynReq.Elements["DSCG_YN"].Value = data.DSCG_YN;
            dynReq.Elements["ASM_DSCG_DT"].Value = data.ASM_DSCG_DT;
            dynReq.Elements["DSCG_STAT_CD"].Value = data.DSCG_STAT_CD;
            dynReq.Elements["DEATH_DT"].Value = data.DEATH_DT;
            dynReq.Elements["HEIG_MASR_CD"].Value = data.HEIG_MASR_CD;
            dynReq.Elements["HEIG"].Value = data.HEIG;
            dynReq.Elements["BWGT_MASR_CD"].Value = data.BWGT_MASR_CD;
            dynReq.Elements["BWGT"].Value = data.BWGT;

            // B. 과거력 및 시술경험
            dynReq.Elements["SMKN_YN"].Value = data.SMKN_YN;
            dynReq.Elements["NTSM_CD"].Value = data.NTSM_CD;
            dynReq.Elements["HYTEN_YN"].Value = data.HYTEN_YN;
            dynReq.Elements["DBML_YN"].Value = data.DBML_YN;
            dynReq.Elements["ACTE_MCDI_OCCUR_YN"].Value = data.ACTE_MCDI_OCCUR_YN;
            dynReq.Elements["ACTE_MCDI_OCCUR_RCD_CD"].Value = data.ACTE_MCDI_OCCUR_RCD_CD;
            dynReq.Elements["ACTE_MCDI_OCCUR_DD"].Value = data.ACTE_MCDI_OCCUR_DD;
            dynReq.Elements["ISTBY_AP_OCCUR_YN"].Value = data.ISTBY_AP_OCCUR_YN;
            dynReq.Elements["ASM_ETC_PAST_DS_CD"].Value = data.ASM_ETC_PAST_DS_CD;
            dynReq.Elements["PCI_MOPR_YN"].Value = data.PCI_MOPR_YN;
            dynReq.Elements["PCI_MOPR_ADM_CD"].Value = data.PCI_MOPR_ADM_CD;
            dynReq.Elements["ASM_PCI_MOPR_DT"].Value = data.ASM_PCI_MOPR_DT;
            dynReq.Elements["LEFT_MAIN_ENFC_YN"].Value = data.LEFT_MAIN_ENFC_YN;
            dynReq.Elements["3VSS_ENFC_YN"].Value = data.THREE_VSS_ENFC_YN;
            dynReq.Elements["STENT_INS_TOT_CNT"].Value = data.STENT_INS_TOT_CNT;
            dynReq.Elements["CABG_EXPR_YN"].Value = data.CABG_EXPR_YN;
            dynReq.Elements["ETC_HRT_SOPR_EXPR_YN"].Value = data.ETC_HRT_SOPR_EXPR_YN;

            // C. 수술 전 진료정보
            dynReq.Elements["BPRSU_MASR_CD"].Value = data.BPRSU_MASR_CD;
            dynReq.Elements["BPRSU"].Value = data.BPRSU;
            dynReq.Elements["PULS_MASR_CD"].Value = data.PULS_MASR_CD;
            dynReq.Elements["PULS"].Value = data.PULS;
            dynReq.Elements["CHOL"].Value = data.CHOL;
            dynReq.Elements["NTFT"].Value = data.NTFT;
            dynReq.Elements["HDL"].Value = data.HDL;
            dynReq.Elements["LDL"].Value = data.LDL;
            dynReq.Elements["SRU_CRAT"].Value = data.SRU_CRAT;
            dynReq.Elements["CRHM"].Value = data.CRHM;
            dynReq.Elements["HCT"].Value = data.HCT;
            dynReq.Elements["LVEF"].Value = data.LVEF;
            dynReq.Elements["ASM_ECG_OPN_CD"].Value = data.ASM_ECG_OPN_CD;
            dynReq.Elements["ASM_INVS_BLDVE_CD"].Value = data.ASM_INVS_BLDVE_CD;
            dynReq.Elements["LEFT_MAIN_ILNS_YN"].Value = data.LEFT_MAIN_ILNS_YN;
            dynReq.Elements["SOPR_BF_IMPT_CLI_STAT_CD"].Value = data.SOPR_BF_IMPT_CLI_STAT_CD;
            dynReq.Elements["VNTR_SUP_DV_CD"].Value = data.VNTR_SUP_DV_CD;

            // D. CABG 수술 반복 필드
            dynReq.Tables["TBL_CABG"].Columns.Add("SOPR_STA_DT");
            dynReq.Tables["TBL_CABG"].Columns.Add("SOPR_END_DT");
            dynReq.Tables["TBL_CABG"].Columns.Add("EMY_YN");
            dynReq.Tables["TBL_CABG"].Columns.Add("ASM_EMY_RS_CD");
            dynReq.Tables["TBL_CABG"].Columns.Add("VNTR_SUP_DV_CD");
            dynReq.Tables["TBL_CABG"].Columns.Add("ASM_CATH_DT");
            dynReq.Tables["TBL_CABG"].Columns.Add("EMY_RS_ETC_TXT");
            dynReq.Tables["TBL_CABG"].Columns.Add("USE_BLDVE_CD");
            dynReq.Tables["TBL_CABG"].Columns.Add("ASM_ITAY_USE_RGN_CD");
            dynReq.Tables["TBL_CABG"].Columns.Add("ASM_ITAY_UNUS_RS_CD");
            dynReq.Tables["TBL_CABG"].Columns.Add("ITAY_UNUS_RS_ETC_TXT");
            dynReq.Tables["TBL_CABG"].Columns.Add("ASM_HRT_BLDVE_SAME_SOPR_CD");
            dynReq.Tables["TBL_CABG"].Columns.Add("ETC_SAME_SOPR_CD");
            dynReq.Tables["TBL_CABG"].Columns.Add("ETC_SAME_ST1_MDFEE_CD");
            dynReq.Tables["TBL_CABG"].Columns.Add("ETC_SAME_ST1_SOPR_NM");
            dynReq.Tables["TBL_CABG"].Columns.Add("ETC_SAME_ND2_MDFEE_CD");
            dynReq.Tables["TBL_CABG"].Columns.Add("ETC_SAME_ND2_SOPR_NM");
            dynReq.Tables["TBL_CABG"].Columns.Add("ASM_EXTB_DT");

            for (int i = 0; i < data.SOPR_STA_DT.Count; i++)
            {
                dynReq.Tables["TBL_CABG"].AddRow();
                dynReq.Tables["TBL_CABG"].Rows[i]["SOPR_STA_DT"].Value = data.SOPR_STA_DT[i];
                dynReq.Tables["TBL_CABG"].Rows[i]["SOPR_END_DT"].Value = data.SOPR_END_DT[i];
                dynReq.Tables["TBL_CABG"].Rows[i]["EMY_YN"].Value = data.EMY_YN[i];
                dynReq.Tables["TBL_CABG"].Rows[i]["ASM_EMY_RS_CD"].Value = data.ASM_EMY_RS_CD[i];
                dynReq.Tables["TBL_CABG"].Rows[i]["VNTR_SUP_DV_CD"].Value = data.ASM_VNTR_SUP_DV_CD[i];
                dynReq.Tables["TBL_CABG"].Rows[i]["ASM_CATH_DT"].Value = data.ASM_CATH_DT[i];
                dynReq.Tables["TBL_CABG"].Rows[i]["EMY_RS_ETC_TXT"].Value = data.EMY_RS_ETC_TXT[i];
                dynReq.Tables["TBL_CABG"].Rows[i]["USE_BLDVE_CD"].Value = data.USE_BLDVE_CD[i];
                dynReq.Tables["TBL_CABG"].Rows[i]["ASM_ITAY_USE_RGN_CD"].Value = data.ASM_ITAY_USE_RGN_CD[i];
                dynReq.Tables["TBL_CABG"].Rows[i]["ASM_ITAY_UNUS_RS_CD"].Value = data.ASM_ITAY_UNUS_RS_CD[i];
                dynReq.Tables["TBL_CABG"].Rows[i]["ITAY_UNUS_RS_ETC_TXT"].Value = data.ITAY_UNUS_RS_ETC_TXT[i];
                dynReq.Tables["TBL_CABG"].Rows[i]["ASM_HRT_BLDVE_SAME_SOPR_CD"].Value = data.ASM_HRT_BLDVE_SAME_SOPR_CD[i];
                dynReq.Tables["TBL_CABG"].Rows[i]["ETC_SAME_SOPR_CD"].Value = data.ETC_SAME_SOPR_CD[i];
                dynReq.Tables["TBL_CABG"].Rows[i]["ETC_SAME_ST1_MDFEE_CD"].Value = data.ETC_SAME_ST1_MDFEE_CD[i];
                dynReq.Tables["TBL_CABG"].Rows[i]["ETC_SAME_ST1_SOPR_NM"].Value = data.ETC_SAME_ST1_SOPR_NM[i];
                dynReq.Tables["TBL_CABG"].Rows[i]["ETC_SAME_ND2_MDFEE_CD"].Value = data.ETC_SAME_ND2_MDFEE_CD[i];
                dynReq.Tables["TBL_CABG"].Rows[i]["ETC_SAME_ND2_SOPR_NM"].Value = data.ETC_SAME_ND2_SOPR_NM[i];
                dynReq.Tables["TBL_CABG"].Rows[i]["ASM_EXTB_DT"].Value = data.ASM_EXTB_DT[i];
            }

            // E. 개흉술 반복 필드
            dynReq.Elements["THRC_YN"].Value = data.THRC_YN;

            dynReq.Tables["TBL_THRC"].Columns.Add("THRC_STA_DT");
            dynReq.Tables["TBL_THRC"].Columns.Add("THRC_SOPR_CD");
            dynReq.Tables["TBL_THRC"].Columns.Add("THRC_ST1_MDFEE_CD");
            dynReq.Tables["TBL_THRC"].Columns.Add("THRC_ST1_SOPR_NM");
            dynReq.Tables["TBL_THRC"].Columns.Add("THRC_ND2_MDFEE_CD");
            dynReq.Tables["TBL_THRC"].Columns.Add("THRC_ND2_SOPR_NM");
            dynReq.Tables["TBL_THRC"].Columns.Add("ASM_THRC_RS_CD");
            dynReq.Tables["TBL_THRC"].Columns.Add("THRC_RS_ETC_TXT");

            for (int i = 0; i < data.THRC_STA_DT.Count; i++)
            {
                dynReq.Tables["TBL_THRC"].AddRow();
                dynReq.Tables["TBL_THRC"].Rows[i]["THRC_STA_DT"].Value = data.THRC_STA_DT[i];
                dynReq.Tables["TBL_THRC"].Rows[i]["THRC_SOPR_CD"].Value = data.THRC_SOPR_CD[i];
                dynReq.Tables["TBL_THRC"].Rows[i]["THRC_ST1_MDFEE_CD"].Value = data.THRC_ST1_MDFEE_CD[i];
                dynReq.Tables["TBL_THRC"].Rows[i]["THRC_ST1_SOPR_NM"].Value = data.THRC_ST1_SOPR_NM[i];
                dynReq.Tables["TBL_THRC"].Rows[i]["THRC_ND2_MDFEE_CD"].Value = data.THRC_ND2_MDFEE_CD[i];
                dynReq.Tables["TBL_THRC"].Rows[i]["THRC_ND2_SOPR_NM"].Value = data.THRC_ND2_SOPR_NM[i];
                dynReq.Tables["TBL_THRC"].Rows[i]["ASM_THRC_RS_CD"].Value = data.ASM_THRC_RS_CD[i];
                dynReq.Tables["TBL_THRC"].Rows[i]["THRC_RS_ETC_TXT"].Value = data.THRC_RS_ETC_TXT[i];
            }

            // F. 퇴원시 약제처방
            dynReq.Elements["ANDR_PRSC_YN"].Value = data.ANDR_PRSC_YN;
            dynReq.Elements["MDS_CD"].Value = data.MDS_CD;
            dynReq.Elements["MDS_NM"].Value = data.MDS_NM;
            dynReq.Elements["ASM_ANDR_NOPRS_RS_CD"].Value = data.ASM_ANDR_NOPRS_RS_CD;
            dynReq.Elements["ANDR_NOPRS_RS_ETC_TXT"].Value = data.ANDR_NOPRS_RS_ETC_TXT;

            // G. 기타
            //dynReq.Elements["APND_DATA_NO"].Value = "";


            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(hosid, isTmp ? "createTmpDoc" : "createDoc");

            //if (dynRes.ErrorMessage.StartsWith("(Token_Error)로그인 정보를 초기화합니다.(인증되지 않았거나 타기관 인증서 사용)")) return "re";
            if (!dynRes.Result)
            {
                data.STATUS = (isTmp == true ? "F" : "E"); // 오류(임시저장이 실패한 것이면 F로 표시한다)
                data.ERR_CODE = dynRes.ErrorCode;
                data.ERR_DESC = dynRes.ErrorMessage;

                data.DOC_NO = ""; //문서번호
                //data.SUPL_DATA_FOM_CD = ""; //서식코드
                //data.RCV_NO = ""; //접수번호
                //data.SP_SNO = ""; //명세서일련번호
                //data.HOSP_RNO = ""; //환자등록번호
                //data.PAT_NM = ""; //환자성명
                //data.INSUP_TP_CD = ""; //참고업무구분

            }
            else
            {
                data.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.ERR_CODE = "";
                data.ERR_DESC = "";

                data.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                //data.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                //data.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                //data.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                //data.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                //data.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                //data.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분

            }

            // 전송내역을 저장한다.
            data.Upd_STATUS(p_sysdt, p_systm, p_user, p_conn);

            return "";
        }

        // 급성기 뇌졸중
        public string Send(CDataASM003_002 data, bool isTmp, string p_sysdt, string p_systm, string p_user, string hosid, OleDbConnection p_conn)
        {
            if (data.SEL == false) return "";

            data.STATUS = "N"; // 진행중

            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

            // 문서 공통 정보
            //dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = data.form; //서식코드
            //dynReq.Metadata["FOM_VER"].Value = data.ver; // 서식버전
            //dynReq.Metadata["YKIHO"].Value = hosid; // 요양기관기호
            //dynReq.Metadata["DMD_NO"].Value = data.DEMNO; // 청구번호
            //dynReq.Metadata["RCV_NO"].Value = data.CNECNO; // 접수번호(없는 경우 0000000)
            //dynReq.Metadata["RCV_YR"].Value = data.CNECTDD.Substring(0, 4); // 접수년도
            //dynReq.Metadata["BILL_SNO"].Value = data.BILLSNO; // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)
            //dynReq.Metadata["SP_SNO"].Value = data.EPRTNO; // 명일련
            //dynReq.Metadata["INSUP_TP_CD"].Value = data.INSUP_TP_CD; // 보험자구분코드(4.보험 5.보호 7.보훈 8.자보)
            //dynReq.Metadata["FOM_REF_BIZ_TP_CD"].Value = data.buss_cd; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
            //dynReq.Metadata["DTL_BIZ_CD"].Value = data.buss_detail; // 업무상세코드
            //dynReq.Metadata["REQ_DATA_NO"].Value = ""; // 요청번호(참고업무 구분코드가 '03: 이의신청, 05: 진료비민원, 06:신포괄, 09: 진료의뢰회송‘인 경우 필수 기재)
            //dynReq.Metadata["RE_RV_RCV_ID"].Value = ""; // 재심사접수ID(1)업무상세코드가 이의신청: SUP(보완)인 경우 필수 기재)
            //dynReq.Metadata["HOSP_RNO"].Value = data.PID; // 환자등록번호.병원에서 부여한 번호
            //dynReq.Metadata["PAT_NM"].Value = data.PNM; // 환자명
            //dynReq.Metadata["PAT_JNO"].Value = data.RESID; // 주민번호

            // 문서 공통 정보
            SetCommonMetadata(dynReq, data, hosid);

            // A. 입퇴원 정보 Elements
            dynReq.Elements["ASM_HOSP_ARIV_DT"].Value = data.ASM_HOSP_ARIV_DT; // 병원 도착일시
            dynReq.Elements["DSCG_YN"].Value = data.DSCG_YN; // 퇴원 여부
            dynReq.Elements["ASM_DSCG_DT"].Value = data.ASM_DSCG_DT; // 퇴원일시
            dynReq.Elements["DSCG_STAT_RCD_YN"].Value = data.DSCG_STAT_RCD_YN; // 퇴원상태 기록 여부
            dynReq.Elements["DSCG_STAT_CD"].Value = data.DSCG_STAT_CD; // 퇴원상태 코드
            dynReq.Elements["ACPH_DHI_RS_CD"].Value = data.ACPH_DHI_RS_CD; // 전원사유
            dynReq.Elements["ACPH_CRST_DIAG"].Value = data.ACPH_CRST_DIAG; // 최종진단 코드(/로 구분된 다중값)
            dynReq.Elements["ACPH_CRST_DIAG_ETC_TXT"].Value = data.ACPH_CRST_DIAG_ETC_TXT; // 기타 진단명 상세
            dynReq.Elements["ACPH_CRST_REL_CD"].Value = data.ACPH_CRST_REL_CD; // 급성기 뇌졸중과의 관련성
            dynReq.Elements["CRST_SYMT_OCUR_CFR_CD"].Value = data.CRST_SYMT_OCUR_CFR_CD; // 증상발생 시각 구분
            dynReq.Elements["SYMT_OCUR_DT"].Value = data.SYMT_OCUR_DT; // 증상발생일시
            dynReq.Elements["STAT_CFR_DT"].Value = data.STAT_CFR_DT; // 발견일시
            dynReq.Elements["LAST_NRM_CFR_DT"].Value = data.LAST_NRM_CFR_DT; // 최종 정상 확인일시
            dynReq.Elements["ASM_FST_IPAT_PTH_CD"].Value = data.ASM_FST_IPAT_PTH_CD; // 내원장소
            dynReq.Elements["ASM_VST_PTH_CD"].Value = data.ASM_VST_PTH_CD; // 내원경로
            dynReq.Elements["VST_MTH_CD"].Value = data.VST_MTH_CD; // 내원방법
            dynReq.Elements["RHBLTN_DGSBJT_TRFR_YN"].Value = data.RHBLTN_DGSBJT_TRFR_YN; // 재활의학과 전과 여부
            dynReq.Elements["TRFR_DD"].Value = data.TRFR_DD; // 전과일
            dynReq.Elements["WLST_RCD_YN"].Value = data.WLST_RCD_YN; // 연명의료중단 결정여부
            dynReq.Elements["DEATH_PCS_PTNT_RCD_DT"].Value = data.DEATH_PCS_PTNT_RCD_DT; // 판단서 작성일자
            dynReq.Elements["BRN_DEATH_YN"].Value = data.BRN_DEATH_YN; // 뇌사 여부
            dynReq.Elements["BRN_DEATH_DD"].Value = data.BRN_DEATH_DD; // 뇌사일자

            // B. 진료 정보
            dynReq.Elements["CRST_SCL_ENFC_YN"].Value = data.CRST_SCL_ENFC_YN;
            dynReq.Elements["CRST_SCL_FST_EXEC_DD"].Value = data.CRST_SCL_FST_EXEC_DD;
            dynReq.Elements["CRST_SCL_KND_CD"].Value = data.CRST_SCL_KND_CD;
            dynReq.Elements["ETC_CRST_SCL_NM"].Value = data.ETC_CRST_SCL_NM;
            dynReq.Elements["ETC_CRST_SCL_HGH_PNT"].Value = data.ETC_CRST_SCL_HGH_PNT;
            dynReq.Elements["NIHSS_PNT"].Value = data.NIHSS_PNT;
            dynReq.Elements["GCS_PNT"].Value = data.GCS_PNT;
            dynReq.Elements["ETC_CRST_SCL_ASM_PNT"].Value = data.ETC_CRST_SCL_ASM_PNT;

            dynReq.Elements["FCLT_ASM_TL_ENFC_YN"].Value = data.FCLT_ASM_TL_ENFC_YN;
            dynReq.Elements["LAST_FCLT_ASM_TL_ENFC_DD"].Value = data.LAST_FCLT_ASM_TL_ENFC_DD;
            dynReq.Elements["DSCG_FCLT_ASM_TL_KND_CD"].Value = data.DSCG_FCLT_ASM_TL_KND_CD;
            dynReq.Elements["ETC_FCLT_ASM_TL_TXT"].Value = data.ETC_FCLT_ASM_TL_TXT;
            dynReq.Elements["ETC_FCLT_ASM_TL_HGH_PNT"].Value = data.ETC_FCLT_ASM_TL_HGH_PNT;
            dynReq.Elements["KMBI_PNT"].Value = data.KMBI_PNT;
            dynReq.Elements["MBI_PNT"].Value = data.MBI_PNT;
            dynReq.Elements["BI_PNT"].Value = data.BI_PNT;
            dynReq.Elements["FIM_PNT"].Value = data.FIM_PNT;
            dynReq.Elements["MRS_GRD"].Value = data.MRS_GRD;
            dynReq.Elements["GOS_GRD"].Value = data.GOS_GRD;
            dynReq.Elements["ETC_FCLT_ASM_PNT"].Value = data.ETC_FCLT_ASM_PNT;

            dynReq.Elements["RHBLTN_DDIAG_REQ_YN"].Value = data.RHBLTN_DDIAG_REQ_YN;
            dynReq.Elements["FST_REQ_DD"].Value = data.FST_REQ_DD;
            dynReq.Elements["RHBLTN_DDIAG_FST_RPY_YN"].Value = data.RHBLTN_DDIAG_FST_RPY_YN;
            dynReq.Elements["RPY_DD"].Value = data.RPY_DD;
            dynReq.Elements["RHBLTN_TRET_YN"].Value = data.RHBLTN_TRET_YN;
            dynReq.Elements["FST_TRET_DD"].Value = data.FST_TRET_DD;
            dynReq.Elements["FCLT_HDP_YN"].Value = data.FCLT_HDP_YN;
            dynReq.Elements["CLI_ISTBY_RS_CD"].Value = data.CLI_ISTBY_RS_CD;
            dynReq.Elements["CLI_ISTBY_RS_ETC_TXT"].Value = data.CLI_ISTBY_RS_ETC_TXT;
            dynReq.Elements["CLI_ISTBY_RS_RCD_DD"].Value = data.CLI_ISTBY_RS_RCD_DD;
            dynReq.Elements["FCLT_HDP_ASM_YN"].Value = data.FCLT_HDP_ASM_YN;
            dynReq.Elements["FCLT_HDP_ASM_TL_KND_CD"].Value = data.FCLT_HDP_ASM_TL_KND_CD;
            dynReq.Elements["FCLT_HDP_ASM_ETC_TL_TXT"].Value = data.FCLT_HDP_ASM_ETC_TL_TXT;
            dynReq.Elements["HDP_MRS_GRD"].Value = data.HDP_MRS_GRD;
            dynReq.Elements["HDP_NIHSS_PNT"].Value = data.HDP_NIHSS_PNT;
            dynReq.Elements["FCLT_HDP_ETC_ASM_PNT"].Value = data.FCLT_HDP_ETC_ASM_PNT;
            dynReq.Elements["FCLT_HDP_ASM_TL_EXEC_DD"].Value = data.FCLT_HDP_ASM_TL_EXEC_DD;

            // C. 폐렴 정보
            dynReq.Elements["HR48_AF_PNEM_SICK_YN"].Value = data.HR48_AF_PNEM_SICK_YN;
            dynReq.Elements["PNEM_KND_CD"].Value = data.PNEM_KND_CD;
            dynReq.Elements["PNEM_KND_ETC_TXT"].Value = data.PNEM_KND_ETC_TXT;
            dynReq.Elements["DIAG_SICK_SYM"].Value = data.DIAG_SICK_SYM;
            dynReq.Elements["DIAG_NM"].Value = data.DIAG_NM;
            dynReq.Elements["ATFL_RPRT_YN"].Value = data.ATFL_RPRT_YN;
            dynReq.Elements["ATFL_RPRT_FST_STA_DD"].Value = data.ATFL_RPRT_FST_STA_DD;
            dynReq.Elements["ATFL_RPRT_FST_END_DD"].Value = data.ATFL_RPRT_FST_END_DD;

            // D. 허혈성 뇌졸중
            dynReq.Elements["DGM_INJC_YN"].Value = data.DGM_INJC_YN;
            dynReq.Elements["MDS_INJC_DT"].Value = data.MDS_INJC_DT;
            dynReq.Elements["MDS_INJC_NEXEC_RS_CD"].Value = data.MDS_INJC_NEXEC_RS_CD;
            dynReq.Elements["MDS_INJC_NEXEC_RS_ETC_TXT"].Value = data.MDS_INJC_NEXEC_RS_ETC_TXT;
            dynReq.Elements["MN60_ECS_INJC_RS_CD2"].Value = data.MN60_ECS_INJC_RS_CD2;

            dynReq.Elements["INARTR_THBE_EXEC_YN"].Value = data.INARTR_THBE_EXEC_YN;
            dynReq.Elements["THBE_EXEC_DT"].Value = data.THBE_EXEC_DT;
            dynReq.Elements["MN120_ECS_EXEC_RS_CD"].Value = data.MN120_ECS_EXEC_RS_CD;
            dynReq.Elements["MN120_ECS_EXEC_RS_ETC_TXT"].Value = data.MN120_ECS_EXEC_RS_ETC_TXT;

            // E. 출혈성 뇌졸중
            dynReq.Elements["SBRC_HMRHG_LAST_TRET_EXEC_YN"].Value = data.SBRC_HMRHG_LAST_TRET_EXEC_YN;
            dynReq.Elements["LAST_TRET_EXEC_DT"].Value = data.LAST_TRET_EXEC_DT;
            dynReq.Elements["HR24_ECS_LAST_TRET_EXEC_RS_CD"].Value = data.HR24_ECS_LAST_TRET_EXEC_RS_CD;
            dynReq.Elements["HR24_ECS_LAST_TRET_EXEC_RS_ETC_TXT"].Value = data.HR24_ECS_LAST_TRET_EXEC_RS_ETC_TXT;

            // F. 기타
            //dynReq.Elements["APND_DATA_NO"].Value = ""; // 첨부파일 정보


            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(hosid, isTmp ? "createTmpDoc" : "createDoc");

            //if (dynRes.ErrorMessage.StartsWith("(Token_Error)로그인 정보를 초기화합니다.(인증되지 않았거나 타기관 인증서 사용)")) return "re";
            if (!dynRes.Result)
            {
                data.STATUS = (isTmp == true ? "F" : "E"); // 오류(임시저장이 실패한 것이면 F로 표시한다)
                data.ERR_CODE = dynRes.ErrorCode;
                data.ERR_DESC = dynRes.ErrorMessage;

                data.DOC_NO = ""; //문서번호
                //data.SUPL_DATA_FOM_CD = ""; //서식코드
                //data.RCV_NO = ""; //접수번호
                //data.SP_SNO = ""; //명세서일련번호
                //data.HOSP_RNO = ""; //환자등록번호
                //data.PAT_NM = ""; //환자성명
                //data.INSUP_TP_CD = ""; //참고업무구분

            }
            else
            {
                data.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.ERR_CODE = "";
                data.ERR_DESC = "";

                data.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                //data.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                //data.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                //data.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                //data.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                //data.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                //data.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분

            }

            // 전송내역을 저장한다.
            data.Upd_STATUS(p_sysdt, p_systm, p_user, p_conn);

            return "";
        }

        // 혈액투석
        public string Send(CDataASM008_002 data, bool isTmp, string p_sysdt, string p_systm, string p_user, string hosid, OleDbConnection p_conn)
        {
            if (data.SEL == false) return "";

            data.STATUS = "N"; // 진행중

            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

            // 문서 공통 정보
            //dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = data.form; //서식코드
            //dynReq.Metadata["FOM_VER"].Value = data.ver; // 서식버전
            //dynReq.Metadata["YKIHO"].Value = hosid; // 요양기관기호
            //dynReq.Metadata["DMD_NO"].Value = data.DEMNO; // 청구번호
            //dynReq.Metadata["RCV_NO"].Value = data.CNECNO; // 접수번호(없는 경우 0000000)
            //dynReq.Metadata["RCV_YR"].Value = data.CNECTDD.Substring(0, 4); // 접수년도
            //dynReq.Metadata["BILL_SNO"].Value = data.BILLSNO; // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)
            //dynReq.Metadata["SP_SNO"].Value = data.EPRTNO; // 명일련
            //dynReq.Metadata["INSUP_TP_CD"].Value = data.INSUP_TP_CD; // 보험자구분코드(4.보험 5.보호 7.보훈 8.자보)
            //dynReq.Metadata["FOM_REF_BIZ_TP_CD"].Value = data.buss_cd; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
            //dynReq.Metadata["DTL_BIZ_CD"].Value = data.buss_detail; // 업무상세코드
            //dynReq.Metadata["REQ_DATA_NO"].Value = ""; // 요청번호(참고업무 구분코드가 '03: 이의신청, 05: 진료비민원, 06:신포괄, 09: 진료의뢰회송‘인 경우 필수 기재)
            //dynReq.Metadata["RE_RV_RCV_ID"].Value = ""; // 재심사접수ID(1)업무상세코드가 이의신청: SUP(보완)인 경우 필수 기재)
            //dynReq.Metadata["HOSP_RNO"].Value = data.PID; // 환자등록번호.병원에서 부여한 번호
            //dynReq.Metadata["PAT_NM"].Value = data.PNM; // 환자명
            //dynReq.Metadata["PAT_JNO"].Value = data.RESID; // 주민번호

            // 문서 공통 정보
            SetCommonMetadata(dynReq, data, hosid);


            // A. 기본 정보
            dynReq.Elements["RECU_EQP_ADM_YN"].Value = data.RECU_EQP_ADM_YN; // 요양병원 여부
            dynReq.Elements["IPAT_OPAT_TP_CD"].Value = data.IPAT_OPAT_TP_CD; // 진료형태
            dynReq.Elements["IPAT_DT"].Value = data.IPAT_DT;                 // 입원일시
            dynReq.Elements["DSCG_YN"].Value = data.DSCG_YN;                 // 퇴원여부
            dynReq.Elements["DSCG_FRM_CD"].Value = data.DSCG_FRM_CD;         // 퇴원형태
            dynReq.Elements["VST_STAT_CD"].Value = data.VST_STAT_CD;         // 외래 내원상태
            dynReq.Elements["BLDD_FST_STA_DD"].Value = data.BLDD_FST_STA_DD;             // 최초 투석일
            dynReq.Elements["BLDD_HOFC_FST_STA_DD"].Value = data.BLDD_HOFC_FST_STA_DD;   // 본원 최초 투석일

            // B. 환자 정보
            dynReq.Elements["CHRON_RNFL_CUZ_SICK_CD"].Value = data.CHRON_RNFL_CUZ_SICK_CD; // 만성신부전 원인

            // B-1 심장질환
            dynReq.Tables["TBL_HRT1"].Columns.Add("CCM_SICK_YN"); // 동반상병 유무
            dynReq.Tables["TBL_HRT1"].Columns.Add("SICK_SYM"); // 상병분류기호
            dynReq.Tables["TBL_HRT1"].Columns.Add("CDFL_YN"); // 심부전
            dynReq.Tables["TBL_HRT1"].Columns.Add("CDFL_CD"); // 심부전 상세
            dynReq.Tables["TBL_HRT1"].Columns.Add("LVEF_CNT"); // 좌심실 구혈율 상세
            dynReq.Tables["TBL_HRT1"].Columns.Add("CT_RT_CNT"); // 심흉곽비 상세
            dynReq.Tables["TBL_HRT1"].Columns.Add("ATFB_YN"); // 심방조동 및 세동
            dynReq.Tables["TBL_HRT1"].Columns.Add("ATFB_CD"); // 심방조동 및 세동 상세
            dynReq.Tables["TBL_HRT1"].Columns.Add("ISMA_HRT_DS_YN"); // 허혈성 심장병
            dynReq.Tables["TBL_HRT1"].Columns.Add("ISMA_HRT_DS_CD"); // 허혈성 심장병 상세
            dynReq.Tables["TBL_HRT1"].Columns.Add("OHS_YN"); // 심장판막 치환 등 개심수술 여부

            dynReq.Tables["TBL_HRT1"].AddRow(); // 새 행 생성
            dynReq.Tables["TBL_HRT1"].Rows[0]["CCM_SICK_YN"].Value = data.HRT_CCM_SICK_YN; // 동반상병 유무
            dynReq.Tables["TBL_HRT1"].Rows[0]["SICK_SYM"].Value = data.HRT_SICK_SYM; // 상병분류기호
            dynReq.Tables["TBL_HRT1"].Rows[0]["CDFL_YN"].Value = data.CDFL_YN; // 심부전
            dynReq.Tables["TBL_HRT1"].Rows[0]["CDFL_CD"].Value = data.CDFL_CD; // 심부전 상세
            dynReq.Tables["TBL_HRT1"].Rows[0]["LVEF_CNT"].Value = data.LVEF_CNT; // 좌심실 구혈율 상세
            dynReq.Tables["TBL_HRT1"].Rows[0]["CT_RT_CNT"].Value = data.CT_RT_CNT; // 심흉곽비 상세
            dynReq.Tables["TBL_HRT1"].Rows[0]["ATFB_YN"].Value = data.ATFB_YN; // 심방조동 및 세동
            dynReq.Tables["TBL_HRT1"].Rows[0]["ATFB_CD"].Value = data.ATFB_CD; // 심방조동 및 세동 상세
            dynReq.Tables["TBL_HRT1"].Rows[0]["ISMA_HRT_DS_YN"].Value = data.ISMA_HRT_DS_YN; // 허혈성 심장병
            dynReq.Tables["TBL_HRT1"].Rows[0]["ISMA_HRT_DS_CD"].Value = data.ISMA_HRT_DS_CD; // 허혈성 심장병 상세
            dynReq.Tables["TBL_HRT1"].Rows[0]["OHS_YN"].Value = data.OHS_YN; // 심장판막 치환 등 개심수술 여부


            // B-2 뇌혈관질환
            dynReq.Tables["TBL_CRBL_BLDVE1"].Columns.Add("CCM_SICK_YN"); // 동반상병 유무
            dynReq.Tables["TBL_CRBL_BLDVE1"].Columns.Add("SICK_SYM"); // 상병분류기호
            dynReq.Tables["TBL_CRBL_BLDVE1"].Columns.Add("HDP_YN"); // 장애발생
            dynReq.Tables["TBL_CRBL_BLDVE1"].Columns.Add("ASTC_REQR_YN"); // 타인의 도움 필도

            dynReq.Tables["TBL_CRBL_BLDVE1"].AddRow(); // 새 행 생성
            dynReq.Tables["TBL_CRBL_BLDVE1"].Rows[0]["CCM_SICK_YN"].Value = data.CRBL_CCM_SICK_YN; // 동반상병 유무
            dynReq.Tables["TBL_CRBL_BLDVE1"].Rows[0]["SICK_SYM"].Value = data.CRBL_SICK_SYM; // 상병분류기호
            dynReq.Tables["TBL_CRBL_BLDVE1"].Rows[0]["HDP_YN"].Value = data.CRBL_HDP_YN; // 장애발생
            dynReq.Tables["TBL_CRBL_BLDVE1"].Rows[0]["ASTC_REQR_YN"].Value = data.ASTC_REQR_YN; // 타인의 도움 필도

            // B-3 간경변증
            dynReq.Tables["TBL_LVCR1"].Columns.Add("CCM_SICK_YN"); // 동반상병 유무
            dynReq.Tables["TBL_LVCR1"].Columns.Add("SICK_SYM"); // 상병분류기호
            dynReq.Tables["TBL_LVCR1"].Columns.Add("LVCR_SYMT_CD"); // 간경병증 상세
            dynReq.Tables["TBL_LVCR1"].Columns.Add("REMN_LVR_FCLT_EXM_PNT"); // Child-Pugh score 점수

            dynReq.Tables["TBL_LVCR1"].AddRow(); // 새 행 생성
            dynReq.Tables["TBL_LVCR1"].Rows[0]["CCM_SICK_YN"].Value = data.LVCR_CCM_SICK_YN; // 동반상병 유무
            dynReq.Tables["TBL_LVCR1"].Rows[0]["SICK_SYM"].Value = data.LVCR_SICK_SYM; // 상병분류기호
            dynReq.Tables["TBL_LVCR1"].Rows[0]["LVCR_SYMT_CD"].Value = data.LVCR_SYMT_CD; // 간경병증 상세
            dynReq.Tables["TBL_LVCR1"].Rows[0]["REMN_LVR_FCLT_EXM_PNT"].Value = data.REMN_LVR_FCLT_EXM_PNT; // Child-Pugh score 점수

            // B-4 출혈성위장관질환
            dynReq.Tables["TBL_HMRHG_GIT1"].Columns.Add("CCM_SICK_YN"); // 동반상병 유무
            dynReq.Tables["TBL_HMRHG_GIT1"].Columns.Add("SICK_SYM"); // 상병분류기호
            dynReq.Tables["TBL_HMRHG_GIT1"].Columns.Add("HMRHG_GIT_DS_CD"); // 출혈성 위장관 질환 상세

            dynReq.Tables["TBL_HMRHG_GIT1"].AddRow(); // 새 행 생성
            dynReq.Tables["TBL_HMRHG_GIT1"].Rows[0]["CCM_SICK_YN"].Value = data.HMRHG_CCM_SICK_YN; // 동반상병 유무
            dynReq.Tables["TBL_HMRHG_GIT1"].Rows[0]["SICK_SYM"].Value = data.HMRHG_SICK_SYM; // 상병분류기호
            dynReq.Tables["TBL_HMRHG_GIT1"].Rows[0]["HMRHG_GIT_DS_CD"].Value = data.HMRHG_GIT_DS_CD; // 출혈성 위장관 질환 상세

            // B-5 만성폐질환
            dynReq.Tables["TBL_CHRON_LUNG1"].Columns.Add("CCM_SICK_YN"); // 동반상병 유무
            dynReq.Tables["TBL_CHRON_LUNG1"].Columns.Add("SICK_SYM"); // 상병분류기호
            dynReq.Tables["TBL_CHRON_LUNG1"].Columns.Add("ARTR_BLDVE_OXY_PART_PRES"); // 동맥혈 산소분압

            dynReq.Tables["TBL_CHRON_LUNG1"].AddRow(); // 새 행 생성
            dynReq.Tables["TBL_CHRON_LUNG1"].Rows[0]["CCM_SICK_YN"].Value = data.LUNG_CCM_SICK_YN; // 동반상병 유무
            dynReq.Tables["TBL_CHRON_LUNG1"].Rows[0]["SICK_SYM"].Value = data.LUNG_SICK_SYM; // 상병분류기호
            dynReq.Tables["TBL_CHRON_LUNG1"].Rows[0]["ARTR_BLDVE_OXY_PART_PRES"].Value = data.ARTR_BLDVE_OXY_PART_PRES; // 동맥혈 산소분압

            // B-6 악성종양
            dynReq.Tables["TBL_MNPLS_TMR1"].Columns.Add("CCM_SICK_YN"); // 동반상병 유무
            dynReq.Tables["TBL_MNPLS_TMR1"].Columns.Add("SICK_SYM"); // 상병분류기호
            dynReq.Tables["TBL_MNPLS_TMR1"].Columns.Add("MNPLS_TMR_TRET_CD"); // 악성종양 상세

            dynReq.Tables["TBL_MNPLS_TMR1"].AddRow(); // 새 행 생성
            dynReq.Tables["TBL_MNPLS_TMR1"].Rows[0]["CCM_SICK_YN"].Value = data.TMR_CCM_SICK_YN; // 동반상병 유무
            dynReq.Tables["TBL_MNPLS_TMR1"].Rows[0]["SICK_SYM"].Value = data.TMR_SICK_SYM; // 상병분류기호
            dynReq.Tables["TBL_MNPLS_TMR1"].Rows[0]["MNPLS_TMR_TRET_CD"].Value = data.MNPLS_TMR_TRET_CD; // 악성종양 상세

            // B-7 당뇨병
            dynReq.Tables["TBL_DBML1"].Columns.Add("CCM_SICK_YN"); // 동반상병 유무
            dynReq.Tables["TBL_DBML1"].Columns.Add("SICK_SYM"); // 상병분류기호
            dynReq.Tables["TBL_DBML1"].Columns.Add("INSL_IJCT_INJC_YN"); // 지속적인 인슐린 주사 투여

            dynReq.Tables["TBL_DBML1"].AddRow(); // 새 행 생성
            dynReq.Tables["TBL_DBML1"].Rows[0]["CCM_SICK_YN"].Value = data.DBML_CCM_SICK_YN; // 동반상병 유무
            dynReq.Tables["TBL_DBML1"].Rows[0]["SICK_SYM"].Value = data.DBML_SICK_SYM; // 상병분류기호
            dynReq.Tables["TBL_DBML1"].Rows[0]["INSL_IJCT_INJC_YN"].Value = data.INSL_IJCT_INJC_YN; // 지속적인 인슐린 주사 투여

            // B-8 3급 이상의 장애인
            dynReq.Elements["HDP_YN"].Value = data.HDP_YN; // 장애여부
            dynReq.Elements["HDP_TY_CD"].Value = data.HDP_TY_CD; // 장애유형

            // C. 투석 정보
            dynReq.Elements["BLDD_STA_DT"].Value = data.BLDD_STA_DT;
            dynReq.Elements["HEIG"].Value = data.HEIG;
            dynReq.Elements["DLYS_BWGT_YN"].Value = data.DLYS_BWGT_YN;
            dynReq.Elements["ASM_DLYS_BWGT"].Value = data.ASM_DLYS_BWGT;
            dynReq.Elements["BF_BWGT_YN"].Value = data.BF_BWGT_YN;
            dynReq.Elements["ASM_BF_BWGT"].Value = data.ASM_BF_BWGT;
            dynReq.Elements["AF_BWGT_YN"].Value = data.AF_BWGT_YN;
            dynReq.Elements["ASM_AF_BWGT"].Value = data.ASM_AF_BWGT;

            dynReq.Elements["BLDD_PPRT_ASM_YN"].Value = data.BLDD_PPRT_ASM_YN;
            dynReq.Elements["BLDD_PPRT_ASM_CD"].Value = data.BLDD_PPRT_ASM_CD;
            dynReq.Elements["PPRT_NUV"].Value = data.PPRT_NUV;
            dynReq.Elements["BLUR_DCR_RT"].Value = data.BLUR_DCR_RT;

            dynReq.Elements["MAIN_BLDVE_CH_DECS_CD"].Value = data.MAIN_BLDVE_CH_DECS_CD;
            dynReq.Elements["USE_BLDVE_CH_CD"].Value = data.USE_BLDVE_CH_CD;

            dynReq.Elements["HMTP_INJC_YN"].Value = data.HMTP_INJC_YN;
            dynReq.Elements["HMTP_INJC_DT"].Value = data.HMTP_INJC_DT;

            dynReq.Elements["ECG_ENFC_YN"].Value = data.ECG_ENFC_YN;
            dynReq.Elements["ECG_ENFC_DT"].Value = data.ECG_ENFC_DT;

            // 8. 타기관 검사
            dynReq.Elements["OIST_EXM_ENFC_YN"].Value = data.OIST_EXM_ENFC_YN; // 타기관검사 시행 여부

            // 타기관 검사 정보
            dynReq.Tables["TBL_OIST_EXM"].Columns.Add("EXM_NM");           // 검사명
            dynReq.Tables["TBL_OIST_EXM"].Columns.Add("MDFEE_CD");     // 수가코드
            dynReq.Tables["TBL_OIST_EXM"].Columns.Add("ENFC_DD");      // 시행일자
            dynReq.Tables["TBL_OIST_EXM"].Columns.Add("EXM_RST_TXT");      // 결과

            for (int i = 0; i < data.OIST_EXM_NM.Count; i++)
            {
                dynReq.Tables["TBL_OIST_EXM"].AddRow(); // 새 행 생성
                dynReq.Tables["TBL_OIST_EXM"].Rows[i]["EXM_NM"].Value = data.OIST_EXM_NM[i];
                dynReq.Tables["TBL_OIST_EXM"].Rows[i]["MDFEE_CD"].Value = data.OIST_MDFEE_CD[i];
                dynReq.Tables["TBL_OIST_EXM"].Rows[i]["ENFC_DD"].Value = data.OIST_ENFC_DD[i];
                dynReq.Tables["TBL_OIST_EXM"].Rows[i]["EXM_RST_TXT"].Value = data.OIST_EXM_RST_TXT[i];
            }

            // 9. 추적관리
            dynReq.Elements["CHS_ENFC_YN"].Value = data.CHS_ENFC_YN;           // 추적관리 시행 여부

            // 추적관리 검사 정보
            dynReq.Tables["TBL_CHS_EXM"].Columns.Add("EXM_NM");             // 검사명
            dynReq.Tables["TBL_CHS_EXM"].Columns.Add("MDFEE_CD");       // 수가코드
            dynReq.Tables["TBL_CHS_EXM"].Columns.Add("ENFC_DD");        // 시행일자
            dynReq.Tables["TBL_CHS_EXM"].Columns.Add("OIST_ENFC_YN");       // 타기관 시행 여부

            for (int i = 0; i < data.CHS_EXM_NM.Count; i++)
            {
                dynReq.Tables["TBL_CHS_EXM"].AddRow();
                dynReq.Tables["TBL_CHS_EXM"].Rows[i]["EXM_NM"].Value = data.CHS_EXM_NM[i];
                dynReq.Tables["TBL_CHS_EXM"].Rows[i]["MDFEE_CD"].Value = data.CHS_MDFEE_CD[i];
                dynReq.Tables["TBL_CHS_EXM"].Rows[i]["ENFC_DD"].Value = data.CHS_ENFC_DD[i];
                dynReq.Tables["TBL_CHS_EXM"].Rows[i]["OIST_ENFC_YN"].Value = data.CHS_OIST_ENFC_YN[i];
            }


            // (옵션) 첨부자료 식별번호
            // dynReq.Elements["APND_DATA_NO"].Value = data.APND_DATA_NO;  // 주석 처리됨


            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(hosid, isTmp ? "createTmpDoc" : "createDoc");

            //if (dynRes.ErrorMessage.StartsWith("(Token_Error)로그인 정보를 초기화합니다.(인증되지 않았거나 타기관 인증서 사용)")) return "re";
            if (!dynRes.Result)
            {
                data.STATUS = (isTmp == true ? "F" : "E"); // 오류(임시저장이 실패한 것이면 F로 표시한다)
                data.ERR_CODE = dynRes.ErrorCode;
                data.ERR_DESC = dynRes.ErrorMessage;

                data.DOC_NO = ""; //문서번호
                //data.SUPL_DATA_FOM_CD = ""; //서식코드
                //data.RCV_NO = ""; //접수번호
                //data.SP_SNO = ""; //명세서일련번호
                //data.HOSP_RNO = ""; //환자등록번호
                //data.PAT_NM = ""; //환자성명
                //data.INSUP_TP_CD = ""; //참고업무구분

            }
            else
            {
                data.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.ERR_CODE = "";
                data.ERR_DESC = "";

                data.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                //data.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                //data.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                //data.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                //data.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                //data.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                //data.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분

            }

            // 전송내역을 저장한다.
            data.Upd_STATUS(p_sysdt, p_systm, p_user, p_conn);

            return "";
        }

        // 혈액투석 - 진단검사결과
        public string Send_ERD001(CDataASM008_002 data, bool isTmp, string p_sysdt, string p_systm, string p_user, string hosid, OleDbConnection p_conn)
        {
            if (data.SEL == false) return "";

            data.STATUS_ERD001 = "N"; // 진행중

            if (data.ERD_DGSBJT_CD.Count < 1)
            {
                // 보낼 자료가 없는 경우임.
                data.STATUS_ERD001 = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.ERR_CODE_ERD001 = "";
                data.ERR_DESC_ERD001 = "";

                data.DOC_NO_ERD001 = ""; //문서번호
            }
            else
            {
                HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

                // 문서 공통 정보
                SetCommonMetadata(dynReq, data, hosid);
                dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "ERD001"; //서식코드

                // A. 기본 정보
                dynReq.Elements["DGSBJT_CD"].Value = data.ERD_DGSBJT_CD[0]; // 진료과
                dynReq.Elements["IFLD_DTL_SPC_SBJT_CD"].Value = data.ERD_IFLD_DTL_SPC_SBJT_CD[0]; // 내과세부
                dynReq.Elements["PRSC_DR_NM"].Value = data.ERD_PRSC_DR_NM[0]; // 처방의 성명


                // C.Grid형 검사 결과
                dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_PRSC_DT"); // 처방일시
                dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_GAT_DT"); // 채취일시
                dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_RCV_DT"); // 접수일시
                dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_RST_DT"); // 결과일시
                dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_SPCM_CD"); // 검체종류
                dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_SPCM_ETC_TXT"); // 검체종류상세
                dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_MDFEE_CD"); // 수가코드
                dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_CD"); // 검사코드
                dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_NM"); // 검사명
                dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_RST_TXT"); // 검사결과
                dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_REF_TXT"); // 참고치
                dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_UNIT"); // 단위
                dynReq.Tables["TBL_GRID_EXM"].Columns.Add("EXM_ADD_TXT"); // 추가정보

                for (int i = 0; i < data.ERD_EXM_PRSC_DT.Count; i++)
                {
                    dynReq.Tables["TBL_GRID_EXM"].AddRow();
                    dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_PRSC_DT"].Value = data.ERD_EXM_PRSC_DT[i]; // 처방일시
                    dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_GAT_DT"].Value = data.ERD_EXM_GAT_DT[i]; // 채취일시
                    dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_RCV_DT"].Value = data.ERD_EXM_RCV_DT[i]; // 접수일시
                    dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_RST_DT"].Value = data.ERD_EXM_RST_DT[i]; // 결과일시
                    dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_SPCM_CD"].Value = data.ERD_EXM_SPCM_CD[i]; // 검체종류
                    dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_SPCM_ETC_TXT"].Value = data.ERD_EXM_SPCM_ETC_TXT[i]; // 검체종류상세
                    dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_MDFEE_CD"].Value = data.ERD_EXM_MDFEE_CD[i]; // 수가코드
                    dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_CD"].Value = data.ERD_EXM_CD[i]; // 검사코드
                    dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_NM"].Value = data.ERD_EXM_NM[i]; // 검사명
                    dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_RST_TXT"].Value = data.ERD_EXM_RST_TXT[i]; // 검사결과
                    dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_REF_TXT"].Value = data.ERD_EXM_REF_TXT[i]; // 참고치
                    dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_UNIT"].Value = data.ERD_EXM_UNIT[i]; // 단위
                    dynReq.Tables["TBL_GRID_EXM"].Rows[i]["EXM_ADD_TXT"].Value = data.ERD_EXM_ADD_TXT[i]; // 추가정보
                }

                // requestData - createDoc    최종 제출
                //               createTmpDoc 임시 제출
                //               updateTmpDoc 수정 제출
                //               selectDocList 목록 조회
                //               selectDocLink 미리보기
                //               selectDoc     상세조회
                HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(hosid, isTmp ? "createTmpDoc" : "createDoc");

                //if (dynRes.ErrorMessage.StartsWith("(Token_Error)로그인 정보를 초기화합니다.(인증되지 않았거나 타기관 인증서 사용)")) return "re";
                if (!dynRes.Result)
                {
                    data.STATUS_ERD001 = (isTmp == true ? "F" : "E"); // 오류(임시저장이 실패한 것이면 F로 표시한다)
                    data.ERR_CODE_ERD001 = dynRes.ErrorCode;
                    data.ERR_DESC_ERD001 = dynRes.ErrorMessage;

                    data.DOC_NO_ERD001 = ""; //문서번호

                }
                else
                {
                    data.STATUS_ERD001 = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                    data.ERR_CODE_ERD001 = "";
                    data.ERR_DESC_ERD001 = "";

                    data.DOC_NO_ERD001 = dynRes.Datas["DOC_NO"].Value; //문서번호

                }
            }

            // 전송내역을 저장한다.
            data.Upd_STATUS_ERD001(p_sysdt, p_systm, p_user, p_conn);

            return "";
        }

        // 혈액투석 - 영상검사결과
        public string Send_ERR001(CDataASM008_002 data, bool isTmp, string p_sysdt, string p_systm, string p_user, string hosid, OleDbConnection p_conn)
        {
            if (data.SEL == false) return "";

            data.STATUS_ERR001 = "N"; // 진행중

            if (data.ERR_DGSBJT_CD.Count < 1)
            {
                // 보낼 자료 없음.
                data.STATUS_ERR001 = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.ERR_CODE_ERR001 = "";
                data.ERR_DESC_ERR001 = "";

                data.DOC_NO_ERR001 = ""; //문서번호
            }
            else
            {
                HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

                // 문서 공통 정보
                SetCommonMetadata(dynReq, data, hosid);
                dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = "ERR001"; //서식코드

                // A. 검사 정보 및 결과
                dynReq.Tables["TBL_EXM_RST"].Columns.Add("DGSBJT_CD"); // 진료과
                dynReq.Tables["TBL_EXM_RST"].Columns.Add("IFLD_DTL_SPC_SBJT_CD"); // 내과세부
                dynReq.Tables["TBL_EXM_RST"].Columns.Add("PRSC_DR_NM"); // 처방의 성명
                dynReq.Tables["TBL_EXM_RST"].Columns.Add("EXM_PRSC_DT"); // 처방일시
                dynReq.Tables["TBL_EXM_RST"].Columns.Add("EXM_EXEC_DT"); // 검사일시
                dynReq.Tables["TBL_EXM_RST"].Columns.Add("EXM_RST_DT"); // 판독일시
                dynReq.Tables["TBL_EXM_RST"].Columns.Add("EXM_RST_DT"); // 판독일시
                dynReq.Tables["TBL_EXM_RST"].Columns.Add("DCT_DR_NM"); // 판독의 성명
                dynReq.Tables["TBL_EXM_RST"].Columns.Add("DCT_DR_LCS_NO"); // 판독의 면허번호
                dynReq.Tables["TBL_EXM_RST"].Columns.Add("EXM_MDFEE_CD"); // 수가코드
                dynReq.Tables["TBL_EXM_RST"].Columns.Add("EXM_CD"); // 검사코드
                dynReq.Tables["TBL_EXM_RST"].Columns.Add("EXM_NM"); // 검사명
                dynReq.Tables["TBL_EXM_RST"].Columns.Add("EXM_RST_TXT"); // 판독결과

                for (int i = 0; i < data.ERR_DGSBJT_CD.Count; i++)
                {
                    dynReq.Tables["TBL_EXM_RST"].AddRow();
                    dynReq.Tables["TBL_EXM_RST"].Rows[i]["DGSBJT_CD"].Value = data.ERR_DGSBJT_CD[i]; // 진료과
                    dynReq.Tables["TBL_EXM_RST"].Rows[i]["IFLD_DTL_SPC_SBJT_CD"].Value = data.ERR_IFLD_DTL_SPC_SBJT_CD[i]; // 내과세부
                    dynReq.Tables["TBL_EXM_RST"].Rows[i]["PRSC_DR_NM"].Value = data.ERR_PRSC_DR_NM[i]; // 처방의 성명
                    dynReq.Tables["TBL_EXM_RST"].Rows[i]["EXM_PRSC_DT"].Value = data.ERR_EXM_PRSC_DT[i]; // 처방일시
                    dynReq.Tables["TBL_EXM_RST"].Rows[i]["EXM_EXEC_DT"].Value = data.ERR_EXM_EXEC_DT[i]; // 채취일시
                    dynReq.Tables["TBL_EXM_RST"].Rows[i]["EXM_RST_DT"].Value = data.ERR_EXM_RST_DT[i]; // 판독일시
                    dynReq.Tables["TBL_EXM_RST"].Rows[i]["DCT_DR_NM"].Value = data.ERR_DCT_DR_NM[i]; // 판독의 성명
                    dynReq.Tables["TBL_EXM_RST"].Rows[i]["DCT_DR_LCS_NO"].Value = data.ERR_DCT_DR_LCS_NO[i]; // 판독의 면허번호
                    dynReq.Tables["TBL_EXM_RST"].Rows[i]["EXM_MDFEE_CD"].Value = data.ERR_EXM_MDFEE_CD[i]; // 수가코드
                    dynReq.Tables["TBL_EXM_RST"].Rows[i]["EXM_CD"].Value = data.ERR_EXM_CD[i]; // 검사코드
                    dynReq.Tables["TBL_EXM_RST"].Rows[i]["EXM_NM"].Value = data.ERR_EXM_NM[i]; // 검사명
                    dynReq.Tables["TBL_EXM_RST"].Rows[i]["EXM_RST_TXT"].Value = data.ERR_EXM_RST_TXT[i]; // 판독결과
                }

                // requestData - createDoc    최종 제출
                //               createTmpDoc 임시 제출
                //               updateTmpDoc 수정 제출
                //               selectDocList 목록 조회
                //               selectDocLink 미리보기
                //               selectDoc     상세조회
                HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(hosid, isTmp ? "createTmpDoc" : "createDoc");

                //if (dynRes.ErrorMessage.StartsWith("(Token_Error)로그인 정보를 초기화합니다.(인증되지 않았거나 타기관 인증서 사용)")) return "re";
                if (!dynRes.Result)
                {
                    data.STATUS_ERR001 = (isTmp == true ? "F" : "E"); // 오류(임시저장이 실패한 것이면 F로 표시한다)
                    data.ERR_CODE_ERR001 = dynRes.ErrorCode;
                    data.ERR_DESC_ERR001 = dynRes.ErrorMessage;

                    data.DOC_NO_ERR001 = ""; //문서번호

                }
                else
                {
                    data.STATUS_ERR001 = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                    data.ERR_CODE_ERR001 = "";
                    data.ERR_DESC_ERR001 = "";

                    data.DOC_NO_ERR001 = dynRes.Datas["DOC_NO"].Value; //문서번호

                }
            }

            // 전송내역을 저장한다.
            data.Upd_STATUS_ERR001(p_sysdt, p_systm, p_user, p_conn);

            return "";
        }

        // 수술의예방학적항생제사용
        public string Send(CDataASM010_002 data, bool isTmp, string p_sysdt, string p_systm, string p_user, string hosid, OleDbConnection p_conn)
        {
            if (data.SEL == false) return "";

            data.STATUS = "N"; // 진행중

            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

            // 문서 공통 정보
            //dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = data.form; //서식코드
            //dynReq.Metadata["FOM_VER"].Value = data.ver; // 서식버전
            //dynReq.Metadata["YKIHO"].Value = hosid; // 요양기관기호
            //dynReq.Metadata["DMD_NO"].Value = data.DEMNO; // 청구번호
            //dynReq.Metadata["RCV_NO"].Value = data.CNECNO; // 접수번호(없는 경우 0000000)
            //dynReq.Metadata["RCV_YR"].Value = data.CNECTDD != "" ? data.CNECTDD.Substring(0, 4) : data.DEMNO.Substring(0, 4); // 접수년도(접수년도에 값이 없으면 청구번호의 년도를 사용)
            //dynReq.Metadata["BILL_SNO"].Value = data.BILLSNO; // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)
            //dynReq.Metadata["SP_SNO"].Value = data.EPRTNO; // 명일련
            //dynReq.Metadata["INSUP_TP_CD"].Value = data.INSUP_TP_CD; // 보험자구분코드(4.보험 5.보호 7.보훈 8.자보)
            //dynReq.Metadata["FOM_REF_BIZ_TP_CD"].Value = data.buss_cd; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
            //dynReq.Metadata["DTL_BIZ_CD"].Value = data.buss_detail; // 업무상세코드
            //dynReq.Metadata["REQ_DATA_NO"].Value = ""; // 요청번호(참고업무 구분코드가 '03: 이의신청, 05: 진료비민원, 06:신포괄, 09: 진료의뢰회송‘인 경우 필수 기재)
            //dynReq.Metadata["RE_RV_RCV_ID"].Value = ""; // 재심사접수ID(1)업무상세코드가 이의신청: SUP(보완)인 경우 필수 기재)
            //dynReq.Metadata["HOSP_RNO"].Value = data.PID; // 환자등록번호.병원에서 부여한 번호
            //dynReq.Metadata["PAT_NM"].Value = data.PNM; // 환자명
            //dynReq.Metadata["PAT_JNO"].Value = data.RESID; // 주민번호

            // 문서 공통 정보
            SetCommonMetadata(dynReq, data, hosid);


            // A. 기본정보
            dynReq.Elements["ASM_IPAT_DT"].Value = data.ASM_IPAT_DT; // 입원일시(YYYYMMDDHHMM)
            dynReq.Elements["DSCG_YN"].Value = data.DSCG_YN; // 퇴원여부(1.Yes 2.No)
            dynReq.Elements["ASM_DSCG_DT"].Value = data.ASM_DSCG_DT; // 퇴원일시(YYYYMMDDHHMM)


            // B.수술 및 감염 정보
            // 1. 수술 관련 환자 상태
            dynReq.Elements["MDFEE_CD"].Value = data.MDFEE_CD; // 수가코드
            dynReq.Elements["ASM_SOPR_STA_DT"].Value = data.ASM_SOPR_STA_DT; // 수술 시작일시(YYYYMMDDHHMM)
            dynReq.Elements["ASM_SOPR_END_DT"].Value = data.ASM_SOPR_END_DT; // 수술 종료일시(YYYYMMDDHHMM)
            dynReq.Elements["EMY_CD"].Value = data.EMY_CD; // 응급여부(1.정규 2.응급)
            dynReq.Elements["KNJN_RPMT"].Value = data.KNJN_RPMT; // 슬관절치환술(1.Yes 2.No)
            dynReq.Elements["HMRHG_CTRL_YN"].Value = ""; // 토니켓 적용 여부(1.Yes 2.No)
            dynReq.Elements["HMRHG_CTRL_DT"].Value = ""; // 토니켓 팽창 시작일시(YYYYMMDDHHMM)
            if (data.KNJN_RPMT == "1")
            {
                dynReq.Elements["HMRHG_CTRL_YN"].Value = data.HMRHG_CTRL_YN; // 토니켓 적용 여부(1.Yes 2.No)
                if (data.HMRHG_CTRL_YN == "1")
                {
                    dynReq.Elements["HMRHG_CTRL_DT"].Value = data.HMRHG_CTRL_DT; // 토니켓 팽창 시작일시(YYYYMMDDHHMM)
                }
            }
            dynReq.Elements["CAESR_YN"].Value = data.CAESR_YN; // 제왕절개술 시행 여부(1.Yes 2.No)
            dynReq.Elements["NBY_PARTU_DT"].Value = ""; // 제대결찰(태아만출)일시(YYYYMMDDHHMM)
            dynReq.Elements["CRVD_YN"].Value = ""; // 자궁경부 4cm이상 기대 여부(1.Yes 2.No)
            if (data.CAESR_YN == "1")
            {
                dynReq.Elements["NBY_PARTU_DT"].Value = data.NBY_PARTU_DT; // 제대결찰(태아만출)일시(YYYYMMDDHHMM)
                dynReq.Elements["CRVD_YN"].Value = data.CRVD_YN; // 자궁경부 4cm이상 기대 여부(1.Yes 2.No)
            }
            dynReq.Elements["BSE_NCT_YN"].Value = data.BSE_NCT_YN; // 기본마취 여부(1.Yes 2.No)
            if (data.BSE_NCT_YN == "1")
            {
                dynReq.Elements["ASA_PNT"].Value = data.ASA_PNT; // ASA 점수
            }
            // 2.수술 전 항생제 투여
            dynReq.Elements["SOPR_BF_ANBO_INJC_YN"].Value = data.SOPR_BF_ANBO_INJC_YN; // 수술 전 항생제 투여 여부(1.Yes 2.No)
            dynReq.Elements["SOPR_BF_INFC_SICK_YN"].Value = ""; // 감염상병 확진 여부(1.Yes 2.No)
            dynReq.Elements["SOPR_BF_INFC_SICK_CD"].Value = ""; // 감염상병 확진명
            dynReq.Elements["SOPR_BF_DDIAG_YN"].Value = ""; // 감염내과 협진여부(1.Yes 2.No)
            dynReq.Elements["SOPR_BF_ASM_REQ_DT"].Value = ""; // 의뢰일시(YYYYMMDDHHMM)
            dynReq.Elements["SOPR_BF_RPY_YN"].Value = ""; // 회신 여부(1.Yes 2.No)
            dynReq.Elements["SOPR_BF_ASM_RPY_DT"].Value = ""; // 회신일시(YYYYMMDDHHMM)
            dynReq.Elements["SOPR_BF_ANBO_DR_RCD_YN"].Value = ""; // 항생제 필요 의사기록 여부(1.Yes 2.No)
            dynReq.Elements["SOPR_BF_ASM_RCD_DT"].Value = ""; // 기록일시(YYYYMMDDHHMM)
            dynReq.Elements["SOPR_BF_ANBO_DR_RCDC_CD"].Value = ""; // 기록지 종류(YYYYMMDDHHMM)
            dynReq.Elements["SOPR_BF_REQR_RS_CD"].Value = ""; // 필요사유
            dynReq.Elements["SOPR_BF_DR_RCD_TXT"].Value = ""; // 기록 상세 내용(평문)
            if (data.SOPR_BF_ANBO_INJC_YN == "1")
            {
                dynReq.Elements["SOPR_BF_INFC_SICK_YN"].Value = data.SOPR_BF_INFC_SICK_YN; // 감염상병 확진 여부(1.Yes 2.No)
                if (data.SOPR_BF_INFC_SICK_YN == "1")
                {
                    dynReq.Elements["SOPR_BF_INFC_SICK_CD"].Value = data.SOPR_BF_INFC_SICK_CD; // 감염상병 확진명
                }
                dynReq.Elements["SOPR_BF_DDIAG_YN"].Value = data.SOPR_BF_DDIAG_YN; // 감염내과 협진여부(1.Yes 2.No)
                if (data.SOPR_BF_DDIAG_YN == "1")
                {
                    dynReq.Elements["SOPR_BF_ASM_REQ_DT"].Value = data.SOPR_BF_ASM_REQ_DT; // 의뢰일시(YYYYMMDDHHMM)
                    dynReq.Elements["SOPR_BF_RPY_YN"].Value = data.SOPR_BF_RPY_YN; // 회신 여부(1.Yes 2.No)
                    if (data.SOPR_BF_RPY_YN == "1")
                    {
                        dynReq.Elements["SOPR_BF_ASM_RPY_DT"].Value = data.SOPR_BF_ASM_RPY_DT; // 회신일시(YYYYMMDDHHMM)
                    }
                }
                dynReq.Elements["SOPR_BF_ANBO_DR_RCD_YN"].Value = data.SOPR_BF_ANBO_DR_RCD_YN; // 항생제 필요 의사기록 여부(1.Yes 2.No)
                if (data.SOPR_BF_ANBO_DR_RCD_YN == "1")
                {
                    dynReq.Elements["SOPR_BF_ASM_RCD_DT"].Value = data.SOPR_BF_ASM_RCD_DT; // 기록일시(YYYYMMDDHHMM)
                    dynReq.Elements["SOPR_BF_ANBO_DR_RCDC_CD"].Value = data.SOPR_BF_ANBO_DR_RCDC_CD; // 기록지 종류(YYYYMMDDHHMM)
                    dynReq.Elements["SOPR_BF_REQR_RS_CD"].Value = data.SOPR_BF_REQR_RS_CD; // 필요사유
                    dynReq.Elements["SOPR_BF_DR_RCD_TXT"].Value = data.SOPR_BF_DR_RCD_TXT; // 기록 상세 내용(평문)
                }
            }
            // 3.평가 제외 수술
            dynReq.Elements["ASM_TGT_SOPR_SAME_ENFC_YN"].Value = data.ASM_TGT_SOPR_SAME_ENFC_YN; // 대상 수술과 동시에 다른 수술 시행 여부(1.Yes 2.No)
            dynReq.Elements["FQ2_GT_SOPR_ENFC_YN"].Value = data.FQ2_GT_SOPR_ENFC_YN; // 동일 입원기간 내에 2회 이상 수술 시행 여부(1.Yes 2.No)

            // 4.수술 후 항셍제 투여
            //   수술부위 감염으로 항생제 투여
            dynReq.Elements["SOPR_RGN_INFC_ANBO_INJC_YN"].Value = data.SOPR_RGN_INFC_ANBO_INJC_YN; // 수술 후 수술부위 감염으로 항생제 투여 여부(1.Yes 2.No)
            dynReq.Elements["SOPR_RGN_INFC_CD"].Value = ""; // 수술부위 감영 유형
            dynReq.Elements["ASM_RCD_DT"].Value = ""; // 기록일시(YYYYMMDDHHMM)
            dynReq.Elements["SOPR_RGN_INFC_DR_RCDC_CD"].Value = ""; // 기록지 종류
            dynReq.Elements["SOPR_RGN_INFC_DR_RCD_TXT"].Value = ""; // 수술 부위 감염 사유 상세(평문)
            if (data.SOPR_RGN_INFC_ANBO_INJC_YN == "1")
            {
                dynReq.Elements["SOPR_RGN_INFC_CD"].Value = data.SOPR_RGN_INFC_CD; // 수술부위 감영 유형
                if (data.SOPR_RGN_INFC_CD == "5")
                {
                    dynReq.Elements["ASM_RCD_DT"].Value = data.ASM_RCD_DT; // 기록일시(YYYYMMDDHHMM)
                    dynReq.Elements["SOPR_RGN_INFC_DR_RCDC_CD"].Value = data.SOPR_RGN_INFC_DR_RCDC_CD; // 기록지 종류
                    dynReq.Elements["SOPR_RGN_INFC_DR_RCD_TXT"].Value = data.SOPR_RGN_INFC_DR_RCD_TXT; // 수술 부위 감염 사유 상세(평문)
                }
            }

            //   수술부위 외 감염 등으로 항생제 투여
            dynReq.Elements["INFC_ANBO_INJC_YN"].Value = data.INFC_ANBO_INJC_YN; // 수술 후 수술부위 외 감염으로 항생제 투여 여부(1.Yes 2.No)
            dynReq.Elements["CLTR_STRN_YN"].Value = ""; // 혈액,뇌척수액 배양에서 균 분리 여부(1.Yes 2.No)
            dynReq.Elements["ASM_GAT_DT"].Value = ""; // 채취일시(YYYYMMDDHHMM)
            dynReq.Elements["INFC_SICK_DIAG"].Value = ""; // 감염 상병 화진후 항생제 투여 여부(1.Yes 2.No)
            dynReq.Elements["SOPR_AF_INFC_SICK_CD"].Value = ""; // 감염 상병 확진명
            dynReq.Elements["SOPR_AF_DDIAG_YN"].Value = ""; // 감염내과 협진 여부 여부(1.Yes 2.No)
            dynReq.Elements["SOPR_AF_ASM_REQ_DT"].Value = ""; // 의뢰일시(YYYYMMDDHHMM)
            dynReq.Elements["SOPR_AF_RPY_YN"].Value = ""; // 회신 여부(1.Yes 2.No)
            dynReq.Elements["SOPR_AF_ASM_RPY_DT"].Value = ""; // 회신일시(YYYYMMDDHHMM)
            dynReq.Elements["SOPR_AF_ANBO_DR_RCD_YN"].Value = ""; // 항생제 필요 의사기록이 있고 항생제 투여 여부(1.Yes 2.No)
            dynReq.Elements["SOPR_AF_ASM_RCD_DT"].Value = ""; // 기록일시(YYYYMMDDHHMM)
            dynReq.Elements["SOPR_AF_ANBO_DR_RCDC_CD"].Value = ""; // 기록지 종류
            dynReq.Elements["SOPR_AF_REQR_RS_CD"].Value = ""; // 필요사유
            dynReq.Elements["SOPR_AF_DR_RCD_TXT"].Value = ""; // 기록 상세 내용(평문)
            if (data.INFC_ANBO_INJC_YN == "1")
            {
                dynReq.Elements["CLTR_STRN_YN"].Value = data.CLTR_STRN_YN; // 혈액,뇌척수액 배양에서 균 분리 여부(1.Yes 2.No)
                if (data.CLTR_STRN_YN == "1")
                {
                    dynReq.Elements["ASM_GAT_DT"].Value = data.ASM_GAT_DT; // 채취일시(YYYYMMDDHHMM)
                }
                dynReq.Elements["INFC_SICK_DIAG"].Value = data.INFC_SICK_DIAG; // 감염 상병 확진후 항생제 투여 여부(1.Yes 2.No)
                if (data.INFC_SICK_DIAG == "1")
                {
                    dynReq.Elements["SOPR_AF_INFC_SICK_CD"].Value = data.SOPR_AF_INFC_SICK_CD; // 감염 상병 확진명
                }
                dynReq.Elements["SOPR_AF_DDIAG_YN"].Value = data.SOPR_AF_DDIAG_YN; // 감염내과 협진 여부(1.Yes 2.No)
                if (data.SOPR_AF_DDIAG_YN == "1")
                {
                    dynReq.Elements["SOPR_AF_ASM_REQ_DT"].Value = data.SOPR_AF_ASM_REQ_DT; // 의뢰일시(YYYYMMDDHHMM)
                    dynReq.Elements["SOPR_AF_RPY_YN"].Value = data.SOPR_AF_RPY_YN; // 회신 여부(1.Yes 2.No)
                    if (data.SOPR_AF_RPY_YN == "1")
                    {
                        dynReq.Elements["SOPR_AF_ASM_RPY_DT"].Value = data.SOPR_AF_ASM_RPY_DT; // 회신일시(YYYYMMDDHHMM)
                    }
                }
                dynReq.Elements["SOPR_AF_ANBO_DR_RCD_YN"].Value = data.SOPR_AF_ANBO_DR_RCD_YN; // 항생제 필요 의사기록이 있고 항생제 투여 여부(1.Yes 2.No)
                if (data.SOPR_AF_ANBO_DR_RCD_YN == "1")
                {
                    dynReq.Elements["SOPR_AF_ASM_RCD_DT"].Value = data.SOPR_AF_ASM_RCD_DT; // 기록일시(YYYYMMDDHHMM)
                    dynReq.Elements["SOPR_AF_ANBO_DR_RCDC_CD"].Value = data.SOPR_AF_ANBO_DR_RCDC_CD; // 기록지 종류
                    dynReq.Elements["SOPR_AF_REQR_RS_CD"].Value = data.SOPR_AF_REQR_RS_CD; // 필요사유
                    dynReq.Elements["SOPR_AF_DR_RCD_TXT"].Value = data.SOPR_AF_DR_RCD_TXT; // 기록 상세 내용(평문)
                }
            }

            // 항생제 알러지
            dynReq.Elements["ANBO_ALRG_YN"].Value = data.ANBO_ALRG_YN; // 항생제 알러지 여부(1.Yes 2.No)
            dynReq.Elements["WHBL_RBC_BLTS_YN"].Value = data.WHBL_RBC_BLTS_YN; // 수술시작 후 전혈 및 적혈구제제 수혈 여부(1.Yes 2.No)
            if (data.WHBL_RBC_BLTS_YN == "1")
            {
                // 수혈
                dynReq.Tables["TBL_BLTS"].Columns.Add("BLTS_STA_DT"); // 수혈시작일시
                dynReq.Tables["TBL_BLTS"].Columns.Add("BLTS_END_DT"); // 수혈종료일시
                dynReq.Tables["TBL_BLTS"].Columns.Add("MDFEE_CD"); // 수가코드
                dynReq.Tables["TBL_BLTS"].Columns.Add("BLTS_DGM_NM"); // 수혈제제명

                for (int i = 0; i < data.BLTS_STA_DT.Count; i++)
                {
                    dynReq.Tables["TBL_BLTS"].AddRow();
                    dynReq.Tables["TBL_BLTS"].Rows[i]["BLTS_STA_DT"].Value = data.BLTS_STA_DT[i]; // 수혈시작일시
                    dynReq.Tables["TBL_BLTS"].Rows[i]["BLTS_END_DT"].Value = data.BLTS_END_DT[i]; // 수혈종료일시
                    dynReq.Tables["TBL_BLTS"].Rows[i]["MDFEE_CD"].Value = data.BLTS_MDFEE_CD[i]; // 수가코드
                    dynReq.Tables["TBL_BLTS"].Rows[i]["BLTS_DGM_NM"].Value = data.BLTS_DGM_NM[i]; // 수혈제제명
                }
            }

            // C.항생제 투여 여부
            dynReq.Elements["ANBO_USE_YN"].Value = data.ANBO_USE_YN; // 항생제 투여 여부(1.Yes 2.No)
            if (data.ANBO_USE_YN == "1")
            {
                // 항생제 투여
                dynReq.Tables["TBL_ANBO_INJC"].Columns.Add("INJC_STA_DT"); // 투여시작일시
                dynReq.Tables["TBL_ANBO_INJC"].Columns.Add("INJC_END_DT"); // 투여종료일시
                dynReq.Tables["TBL_ANBO_INJC"].Columns.Add("MDS_CD"); // 약품코드
                dynReq.Tables["TBL_ANBO_INJC"].Columns.Add("MDS_NM"); // 약품명
                dynReq.Tables["TBL_ANBO_INJC"].Columns.Add("ANBO_INJC_PTH_CD"); // 투여경로

                for (int i = 0; i < data.INJC_STA_DT.Count; i++)
                {
                    dynReq.Tables["TBL_ANBO_INJC"].AddRow();
                    dynReq.Tables["TBL_ANBO_INJC"].Rows[i]["INJC_STA_DT"].Value = data.INJC_STA_DT[i]; // 투여시작일시
                    dynReq.Tables["TBL_ANBO_INJC"].Rows[i]["INJC_END_DT"].Value = data.INJC_END_DT[i]; // 투여종료일시
                    dynReq.Tables["TBL_ANBO_INJC"].Rows[i]["MDS_CD"].Value = data.INJC_MDS_CD[i]; // 약품코드
                    dynReq.Tables["TBL_ANBO_INJC"].Rows[i]["MDS_NM"].Value = data.INJC_MDS_NM[i]; // 약품명
                    dynReq.Tables["TBL_ANBO_INJC"].Rows[i]["ANBO_INJC_PTH_CD"].Value = data.ANBO_INJC_PTH_CD[i]; // 투여경로
                }
            }

            // 퇴원 시 항생제 처방
            dynReq.Elements["DSCG_ANBO_PRSC_YN"].Value = data.DSCG_ANBO_PRSC_YN; // 퇴원시 항생제 처방 여부(1.Yes 2.No)
            if (data.DSCG_ANBO_PRSC_YN == "1")
            {
                dynReq.Tables["TBL_DSCG_ANBO_PRSC"].Columns.Add("MDS_CD"); // 약품코드
                dynReq.Tables["TBL_DSCG_ANBO_PRSC"].Columns.Add("MDS_NM"); // 약푸명
                dynReq.Tables["TBL_DSCG_ANBO_PRSC"].Columns.Add("PRSC_TOT_INJC_DDCNT"); // 총 투약일수

                for (int i = 0; i < data.PRSC_MDS_CD.Count; i++)
                {
                    dynReq.Tables["TBL_DSCG_ANBO_PRSC"].AddRow();
                    dynReq.Tables["TBL_DSCG_ANBO_PRSC"].Rows[i]["MDS_CD"].Value = data.PRSC_MDS_CD[i]; // 약품코드
                    dynReq.Tables["TBL_DSCG_ANBO_PRSC"].Rows[i]["MDS_NM"].Value = data.PRSC_MDS_NM[i]; // 약품명
                    dynReq.Tables["TBL_DSCG_ANBO_PRSC"].Rows[i]["PRSC_TOT_INJC_DDCNT"].Value = data.PRSC_TOT_INJC_DDCNT[i]; // 총 투약일수
                }
            }



            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(hosid, isTmp ? "createTmpDoc" : "createDoc");

            //if (dynRes.ErrorMessage.StartsWith("(Token_Error)로그인 정보를 초기화합니다.(인증되지 않았거나 타기관 인증서 사용)")) return "re";
            if (!dynRes.Result)
            {
                data.STATUS = (isTmp == true ? "F" : "E"); // 오류(임시저장이 실패한 것이면 F로 표시한다)
                data.ERR_CODE = dynRes.ErrorCode;
                data.ERR_DESC = dynRes.ErrorMessage;

                data.DOC_NO = ""; //문서번호
                //data.SUPL_DATA_FOM_CD = ""; //서식코드
                //data.RCV_NO = ""; //접수번호
                //data.SP_SNO = ""; //명세서일련번호
                //data.HOSP_RNO = ""; //환자등록번호
                //data.PAT_NM = ""; //환자성명
                //data.INSUP_TP_CD = ""; //참고업무구분

            }
            else
            {
                data.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.ERR_CODE = "";
                data.ERR_DESC = "";

                data.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                //data.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                //data.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                //data.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                //data.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                //data.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                //data.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분

            }

            // 전송내역을 저장한다.
            data.Upd_STATUS(p_sysdt, p_systm, p_user, p_conn);

            return "";
        }

        // 의료급여정신과
        public string Send(CDataASM014_001 data, bool isTmp, string p_sysdt, string p_systm, string p_user, string hosid, OleDbConnection p_conn)
        {
            if (data.SEL == false) return "";

            data.STATUS = "N"; // 진행중

            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

            // 문서 공통 정보
            //dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = data.form; // 서식코드
            //dynReq.Metadata["FOM_VER"].Value = data.ver; // 서식버전
            //dynReq.Metadata["YKIHO"].Value = hosid; // 요양기관기호
            //dynReq.Metadata["DMD_NO"].Value = data.DEMNO; // 청구번호
            //dynReq.Metadata["RCV_NO"].Value = data.CNECNO; // 접수번호(없는 경우 0000000)
            //dynReq.Metadata["RCV_YR"].Value = data.CNECTDD.Substring(0, 4); // 접수년도
            //dynReq.Metadata["BILL_SNO"].Value = data.BILLSNO; // 청구서일련번호
            //dynReq.Metadata["SP_SNO"].Value = data.EPRTNO; // 명일련
            //dynReq.Metadata["INSUP_TP_CD"].Value = data.INSUP_TP_CD; // 보험자구분코드
            //dynReq.Metadata["FOM_REF_BIZ_TP_CD"].Value = data.buss_cd; // 참고업무구분코드
            //dynReq.Metadata["DTL_BIZ_CD"].Value = data.buss_detail; // 업무상세코드
            //dynReq.Metadata["REQ_DATA_NO"].Value = ""; // 요청번호
            //dynReq.Metadata["RE_RV_RCV_ID"].Value = ""; // 재심사접수ID
            //dynReq.Metadata["HOSP_RNO"].Value = data.PID; // 환자등록번호
            //dynReq.Metadata["PAT_NM"].Value = data.PNM; // 환자명
            //dynReq.Metadata["PAT_JNO"].Value = data.RESID; // 주민번호

            // 문서 공통 정보
            SetCommonMetadata(dynReq, data, hosid);

            // A. 기본정보
            dynReq.Elements["TRFR_YN"].Value = data.TRFR_YN;                             // 전과 여부

            // 반복 테이블: 전과일자 (TBL_TRFR)
            dynReq.Tables["TBL_TRFR"].Columns.Add("TRFR_DD"); // 전과일자
            dynReq.Tables["TBL_TRFR"].Columns.Add("MVOT_DGSBJT_CD");
            dynReq.Tables["TBL_TRFR"].Columns.Add("MVIN_DGSBJT_CD");
            for (int i = 0; i < data.TRFR_DD.Count; i++)
            {
                dynReq.Tables["TBL_TRFR"].AddRow();
                dynReq.Tables["TBL_TRFR"].Rows[i]["TRFR_DD"].Value = data.TRFR_DD[i];
                dynReq.Tables["TBL_TRFR"].Rows[i]["MVOT_DGSBJT_CD"].Value = data.MVOT_DGSBJT_CD[i];
                dynReq.Tables["TBL_TRFR"].Rows[i]["MVIN_DGSBJT_CD"].Value = data.MVIN_DGSBJT_CD[i];
            }

            dynReq.Elements["INSUP_QLF_CHG_YN"].Value = data.INSUP_QLF_CHG_YN;           // 자격변동 여부

            // 반복 테이블: 자격변동일자 (TBL_QLF)
            dynReq.Tables["TBL_QLF"].Columns.Add("QLF_CHG_DD");
            for (int i = 0; i < data.QLF_CHG_DD.Count; i++)
            {
                dynReq.Tables["TBL_QLF"].AddRow();
                dynReq.Tables["TBL_QLF"].Rows[i]["QLF_CHG_DD"].Value = data.QLF_CHG_DD[i];
            }

            dynReq.Elements["DSCG_YN"].Value = data.DSCG_YN;                             // 퇴원 여부
            dynReq.Elements["DSCG_DD"].Value = data.DSCG_DD;                             // 퇴원일자
            dynReq.Elements["DSCG_STAT_CD"].Value = data.DSCG_STAT_CD;                  // 퇴원상태
            dynReq.Elements["SPNT_IPAT_YN"].Value = data.SPNT_IPAT_YN;                   // 자발적 입원 여부

            // B. 지역사회서비스 연계 의뢰
            dynReq.Elements["DSCG_POTM_PSYCHI_DS_DIAG_YN"].Value = data.DSCG_POTM_PSYCHI_DS_DIAG_YN; // 퇴원시 조현병 여부
            dynReq.Elements["PLC_SCTY_SVC_CONN_REQ_YN"].Value = data.PLC_SCTY_SVC_CONN_REQ_YN;
            dynReq.Elements["PLC_SCTY_SVC_CONN_NREQ_RS_CD"].Value = data.PLC_SCTY_SVC_CONN_NREQ_RS_CD;
            dynReq.Elements["PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT"].Value = data.PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT;

            // C. 퇴원시 환자 경험도 조사
            dynReq.Elements["DSCG_EXPR_INVT_ENFC_YN"].Value = data.DSCG_EXPR_INVT_ENFC_YN;
            dynReq.Elements["DSCG_EXPR_NOPER_RS_CD"].Value = data.DSCG_EXPR_NOPER_RS_CD;
            dynReq.Elements["NOPER_RS_ETC_TXT"].Value = data.NOPER_RS_ETC_TXT;



            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(hosid, isTmp ? "createTmpDoc" : "createDoc");

            //if (dynRes.ErrorMessage.StartsWith("(Token_Error)로그인 정보를 초기화합니다.(인증되지 않았거나 타기관 인증서 사용)")) return "re";
            if (!dynRes.Result)
            {
                data.STATUS = (isTmp == true ? "F" : "E"); // 오류(임시저장이 실패한 것이면 F로 표시한다)
                data.ERR_CODE = dynRes.ErrorCode;
                data.ERR_DESC = dynRes.ErrorMessage;

                data.DOC_NO = ""; //문서번호
                //data.SUPL_DATA_FOM_CD = ""; //서식코드
                //data.RCV_NO = ""; //접수번호
                //data.SP_SNO = ""; //명세서일련번호
                //data.HOSP_RNO = ""; //환자등록번호
                //data.PAT_NM = ""; //환자성명
                //data.INSUP_TP_CD = ""; //참고업무구분

            }
            else
            {
                data.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.ERR_CODE = "";
                data.ERR_DESC = "";

                data.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                //data.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                //data.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                //data.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                //data.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                //data.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                //data.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분

            }

            // 전송내역을 저장한다.
            data.Upd_STATUS(p_sysdt, p_systm, p_user, p_conn);

            return "";

        }

        // 폐렴
        public string Send(CDataASM023_002 data, bool isTmp, string p_sysdt, string p_systm, string p_user, string hosid, OleDbConnection p_conn)
        {
            if (data.SEL == false) return "";

            data.STATUS = "N"; // 진행중

            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

            // 문서 공통 정보
            //dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = data.form; //서식코드
            //dynReq.Metadata["FOM_VER"].Value = data.ver; // 서식버전
            //dynReq.Metadata["YKIHO"].Value = hosid; // 요양기관기호
            //dynReq.Metadata["DMD_NO"].Value = data.DEMNO; // 청구번호
            //dynReq.Metadata["RCV_NO"].Value = data.CNECNO; // 접수번호(없는 경우 0000000)
            //dynReq.Metadata["RCV_YR"].Value = data.CNECTDD.Substring(0, 4); // 접수년도
            //dynReq.Metadata["BILL_SNO"].Value = data.BILLSNO; // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)
            //dynReq.Metadata["SP_SNO"].Value = data.EPRTNO; // 명일련
            //dynReq.Metadata["INSUP_TP_CD"].Value = data.INSUP_TP_CD; // 보험자구분코드(4.보험 5.보호 7.보훈 8.자보)
            //dynReq.Metadata["FOM_REF_BIZ_TP_CD"].Value = data.buss_cd; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
            //dynReq.Metadata["DTL_BIZ_CD"].Value = data.buss_detail; // 업무상세코드
            //dynReq.Metadata["REQ_DATA_NO"].Value = ""; // 요청번호(참고업무 구분코드가 '03: 이의신청, 05: 진료비민원, 06:신포괄, 09: 진료의뢰회송‘인 경우 필수 기재)
            //dynReq.Metadata["RE_RV_RCV_ID"].Value = ""; // 재심사접수ID(1)업무상세코드가 이의신청: SUP(보완)인 경우 필수 기재)
            //dynReq.Metadata["HOSP_RNO"].Value = data.PID; // 환자등록번호.병원에서 부여한 번호
            //dynReq.Metadata["PAT_NM"].Value = data.PNM; // 환자명
            //dynReq.Metadata["PAT_JNO"].Value = data.RESID; // 주민번호

            // 문서 공통 정보
            SetCommonMetadata(dynReq, data, hosid);


            //  A. 기본정보
            dynReq.Elements["ASM_HOSP_ARIV_DT"].Value = data.ASM_HOSP_ARIV_DT;           // 병원도착일시
            dynReq.Elements["DSCG_YN"].Value = data.DSCG_YN;                             // 퇴원여부
            dynReq.Elements["ASM_DSCG_DT"].Value = data.ASM_DSCG_DT;                     // 퇴원일시
            dynReq.Elements["IPAT_TRM_PNEM_SICK_YN"].Value = data.IPAT_TRM_PNEM_SICK_YN; // 입원 중 폐렴상병 유무
            dynReq.Elements["DSCG_STAT_RCD_YN"].Value = data.DSCG_STAT_RCD_YN;           // 퇴원상태 기록 여부
            dynReq.Elements["DSCG_STAT_CD"].Value = data.DSCG_STAT_CD;                   // 퇴원상태

            dynReq.Elements["RECU_HOSP_VST_YN"].Value = data.RECU_HOSP_VST_YN;           // 시설 내원 여부
            dynReq.Elements["RECU_HOSP_NM"].Value = data.RECU_HOSP_NM;                   // 시설명
            dynReq.Elements["ASM_VST_PTH_CD"].Value = data.ASM_VST_PTH_CD;               // 내원경로

            dynReq.Elements["ASM_PLC_SCTY_OBTN_PNEM_CD2"].Value = data.ASM_PLC_SCTY_OBTN_PNEM_CD2; // 폐렴 관련성 코드
            dynReq.Elements["PLC_SCTY_OBTN_PNEM_TXT"].Value = data.PLC_SCTY_OBTN_PNEM_TXT;         // 폐렴 관련 사유 상세

            // B. 검사정보

            dynReq.Elements["ASM_EXM_YN"].Value = data.ASM_EXM_YN;                       // 산소포화도 검사 여부

            // 산소포화도 검사 상세
            dynReq.Tables["TBL_OXY_STRT"].Columns.Add("ASM_EXM_DT");
            dynReq.Tables["TBL_OXY_STRT"].Columns.Add("ASM_OXY_STRT");
            for (int i = 0; i < data.ASM_EXM_DT.Count; i++)
            {
                dynReq.Tables["TBL_OXY_STRT"].AddRow();
                dynReq.Tables["TBL_OXY_STRT"].Rows[i]["ASM_EXM_DT"].Value = data.ASM_EXM_DT[i];
                dynReq.Tables["TBL_OXY_STRT"].Rows[i]["ASM_OXY_STRT"].Value = data.ASM_OXY_STRT[i];
            }

            dynReq.Elements["ASM_PRSC_YN"].Value = data.ASM_PRSC_YN;                     // 객담배양 처방 여부

            // 객담배양 검사 상세
            dynReq.Tables["TBL_SPTUM_CLTR"].Columns.Add("ASM_PRSC_DT");
            for (int i = 0; i < data.ASM_PRSC_DT.Count; i++)
            {
                dynReq.Tables["TBL_SPTUM_CLTR"].AddRow();
                dynReq.Tables["TBL_SPTUM_CLTR"].Rows[i]["ASM_PRSC_DT"].Value = data.ASM_PRSC_DT[i];
            }

            dynReq.Elements["ASM_GAT_YN"].Value = data.ASM_GAT_YN;                       // 혈액배양 채취 여부

            // 혈액배양 검사 상세
            dynReq.Tables["TBL_BLD_CLTR"].Columns.Add("ASM_GAT_DT");
            for (int i = 0; i < data.ASM_GAT_DT.Count; i++)
            {
                dynReq.Tables["TBL_BLD_CLTR"].AddRow();
                dynReq.Tables["TBL_BLD_CLTR"].Rows[i]["ASM_GAT_DT"].Value = data.ASM_GAT_DT[i];
            }

            dynReq.Elements["ST1_ANBO_INJC_BF_GAT_YN"].Value = data.ST1_ANBO_INJC_BF_GAT_YN; // 첫 항생제 투여 전 검사 채취 여부
            dynReq.Elements["ANBO_CHG_BF_GAT_YN"].Value = data.ANBO_CHG_BF_GAT_YN;
            dynReq.Elements["ANBO_CHG_BF_GAT_RS_CD"].Value = data.ANBO_CHG_BF_GAT_RS_CD;

            dynReq.Elements["ASM_USE_YN"].Value = data.ASM_USE_YN;                       // 판정도구 사용 여부

            // 중증도 판정도구 상세
            dynReq.Tables["TBL_SGRD_JDGM_TL"].Columns.Add("ASM_USE_DT");
            dynReq.Tables["TBL_SGRD_JDGM_TL"].Columns.Add("ASM_SGRD_JDGM_TL_KND_CD");
            dynReq.Tables["TBL_SGRD_JDGM_TL"].Columns.Add("ASM_SGRD_JDGM_TL_TOT_PNT");
            for (int i = 0; i < data.ASM_USE_DT.Count; i++)
            {
                dynReq.Tables["TBL_SGRD_JDGM_TL"].AddRow();
                dynReq.Tables["TBL_SGRD_JDGM_TL"].Rows[i]["ASM_USE_DT"].Value = data.ASM_USE_DT[i];
                dynReq.Tables["TBL_SGRD_JDGM_TL"].Rows[i]["ASM_SGRD_JDGM_TL_KND_CD"].Value = data.ASM_SGRD_JDGM_TL_KND_CD[i];
                dynReq.Tables["TBL_SGRD_JDGM_TL"].Rows[i]["ASM_SGRD_JDGM_TL_TOT_PNT"].Value = data.ASM_SGRD_JDGM_TL_TOT_PNT[i];
            }

            dynReq.Elements["CNFS_YN"].Value = data.CNFS_YN;                             // 혼돈 여부
            dynReq.Elements["BLUR_UNIT"].Value = data.BLUR_UNIT;                         // BUN 단위
            dynReq.Elements["BLUR_MG_CNT"].Value = data.BLUR_MG_CNT;
            dynReq.Elements["BLUR_MMOL_CNT"].Value = data.BLUR_MMOL_CNT;
            dynReq.Elements["BRT"].Value = data.BRT;
            dynReq.Elements["BPRSU"].Value = data.BPRSU;

            // C. 투약정보
            dynReq.Elements["ANBO_USE_YN"].Value = data.ANBO_USE_YN;                     // 항생제 투여 여부

            // 정맥내 항생제 투여정보
            dynReq.Tables["TBL_ANBO_INJC"].Columns.Add("MDS_INJC_DT");
            dynReq.Tables["TBL_ANBO_INJC"].Columns.Add("MDS_CD");
            dynReq.Tables["TBL_ANBO_INJC"].Columns.Add("MDS_NM");
            for (int i = 0; i < data.MDS_INJC_DT.Count; i++)
            {
                dynReq.Tables["TBL_ANBO_INJC"].AddRow();
                dynReq.Tables["TBL_ANBO_INJC"].Rows[i]["MDS_INJC_DT"].Value = data.MDS_INJC_DT[i];
                dynReq.Tables["TBL_ANBO_INJC"].Rows[i]["MDS_CD"].Value = data.MDS_CD[i];
                dynReq.Tables["TBL_ANBO_INJC"].Rows[i]["MDS_NM"].Value = data.MDS_NM[i];
            }



            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(hosid, isTmp ? "createTmpDoc" : "createDoc");

            //if (dynRes.ErrorMessage.StartsWith("(Token_Error)로그인 정보를 초기화합니다.(인증되지 않았거나 타기관 인증서 사용)")) return "re";
            if (!dynRes.Result)
            {
                data.STATUS = (isTmp == true ? "F" : "E"); // 오류(임시저장이 실패한 것이면 F로 표시한다)
                data.ERR_CODE = dynRes.ErrorCode;
                data.ERR_DESC = dynRes.ErrorMessage;

                data.DOC_NO = ""; //문서번호
                //data.SUPL_DATA_FOM_CD = ""; //서식코드
                //data.RCV_NO = ""; //접수번호
                //data.SP_SNO = ""; //명세서일련번호
                //data.HOSP_RNO = ""; //환자등록번호
                //data.PAT_NM = ""; //환자성명
                //data.INSUP_TP_CD = ""; //참고업무구분

            }
            else
            {
                data.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.ERR_CODE = "";
                data.ERR_DESC = "";

                data.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                //data.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                //data.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                //data.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                //data.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                //data.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                //data.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분

            }

            // 전송내역을 저장한다.
            data.Upd_STATUS(p_sysdt, p_systm, p_user, p_conn);

            return "";
        }

        // 중환자실
        public string Send(CDataASM024_002 data, bool isTmp, string p_sysdt, string p_systm, string p_user, string hosid, OleDbConnection p_conn)
        {
            if (data.SEL == false) return "";

            data.STATUS = "N"; // 진행중

            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

            // 문서 공통 정보
            //dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = data.form; //서식코드
            //dynReq.Metadata["FOM_VER"].Value = data.ver; // 서식버전
            //dynReq.Metadata["YKIHO"].Value = hosid; // 요양기관기호
            //dynReq.Metadata["DMD_NO"].Value = data.DEMNO; // 청구번호
            //dynReq.Metadata["RCV_NO"].Value = data.CNECNO; // 접수번호(없는 경우 0000000)
            //dynReq.Metadata["RCV_YR"].Value = data.CNECTDD.Substring(0, 4); // 접수년도
            //dynReq.Metadata["BILL_SNO"].Value = data.BILLSNO; // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)
            //dynReq.Metadata["SP_SNO"].Value = data.EPRTNO; // 명일련
            //dynReq.Metadata["INSUP_TP_CD"].Value = data.INSUP_TP_CD; // 보험자구분코드(4.보험 5.보호 7.보훈 8.자보)
            //dynReq.Metadata["FOM_REF_BIZ_TP_CD"].Value = data.buss_cd; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
            //dynReq.Metadata["DTL_BIZ_CD"].Value = data.buss_detail; // 업무상세코드
            //dynReq.Metadata["REQ_DATA_NO"].Value = ""; // 요청번호(참고업무 구분코드가 '03: 이의신청, 05: 진료비민원, 06:신포괄, 09: 진료의뢰회송‘인 경우 필수 기재)
            //dynReq.Metadata["RE_RV_RCV_ID"].Value = ""; // 재심사접수ID(1)업무상세코드가 이의신청: SUP(보완)인 경우 필수 기재)
            //dynReq.Metadata["HOSP_RNO"].Value = data.PID; // 환자등록번호.병원에서 부여한 번호
            //dynReq.Metadata["PAT_NM"].Value = data.PNM; // 환자명
            //dynReq.Metadata["PAT_JNO"].Value = data.RESID; // 주민번호

            // 문서 공통 정보
            SetCommonMetadata(dynReq, data, hosid);

            // A. 검사정보
            dynReq.Elements["IPAT_DD"].Value = data.IPAT_DD; // 입원일자

            // B.중환자실 입퇴실 정보
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("SPRM_IPAT_DT"); // 입실일시
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("SPRM_IPAT_RS_CD"); // 입실사유
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("RE_IPAT_RS_TXT"); // 입실사유 재입실 상세
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("IPAT_RS_ETC_TXT"); // 입실사유 기타 상세
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("ASM_SPRM_DSCG_RST_CD"); // 퇴실현황
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("SPRM_DSCG_DT"); // 퇴실일시
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("SPRM_RE_IPAT_PLAN_YN"); // 재입실계획여부

            for (int i = 0; i < data.SPRM_IPAT_DT.Count; i++)
            {
                dynReq.Tables["TBL_IPAT_DSCG"].AddRow();
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["SPRM_IPAT_DT"].Value = data.SPRM_DSCG_DT[i]; // 입실일시
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["SPRM_IPAT_RS_CD"].Value = data.SPRM_IPAT_RS_CD[i]; // 입실사유
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["RE_IPAT_RS_TXT"].Value = data.RE_IPAT_RS_TXT[i]; // 입실사유 재입실 상세
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["IPAT_RS_ETC_TXT"].Value = data.IPAT_RS_ETC_TXT[i]; // 입실사유 기타 상세
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["ASM_SPRM_DSCG_RST_CD"].Value = data.ASM_SPRM_DSCG_RST_CD[i]; // 퇴실현황
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["SPRM_DSCG_DT"].Value = data.SPRM_DSCG_DT[i]; // 퇴실일시
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["SPRM_RE_IPAT_PLAN_YN"].Value = data.SPRM_RE_IPAT_PLAN_YN[i]; // 재입실계획여부
            }

            // C. 사망 현황
            dynReq.Elements["ASM_DEATH_YN"].Value = data.ASM_DEATH_YN; // 사망여부
            dynReq.Elements["DEATH_DT"].Value = data.DEATH_DT; // 사망일시
            dynReq.Elements["WLST_RCD_YN"].Value = data.WLST_RCD_YN; // 연명의료중단등결정이행서작성여부
            dynReq.Elements["WLST_RCD_DT"].Value = data.WLST_RCD_DT; // 작성일자
            dynReq.Elements["WLST_RCD_CD"].Value = data.WLST_RCD_CD; // 이행내용
            dynReq.Elements["WLST_RCD_ETC_TXT"].Value = data.WLST_RCD_ETC_TXT; // 그 밖의 연명의료 상세


            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(hosid, isTmp ? "createTmpDoc" : "createDoc");

            //if (dynRes.ErrorMessage.StartsWith("(Token_Error)로그인 정보를 초기화합니다.(인증되지 않았거나 타기관 인증서 사용)")) return "re";
            if (!dynRes.Result)
            {
                data.STATUS = (isTmp == true ? "F" : "E"); // 오류(임시저장이 실패한 것이면 F로 표시한다)
                data.ERR_CODE = dynRes.ErrorCode;
                data.ERR_DESC = dynRes.ErrorMessage;

                data.DOC_NO = ""; //문서번호
                //data.SUPL_DATA_FOM_CD = ""; //서식코드
                //data.RCV_NO = ""; //접수번호
                //data.SP_SNO = ""; //명세서일련번호
                //data.HOSP_RNO = ""; //환자등록번호
                //data.PAT_NM = ""; //환자성명
                //data.INSUP_TP_CD = ""; //참고업무구분

            }
            else
            {
                data.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.ERR_CODE = "";
                data.ERR_DESC = "";

                data.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                //data.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                //data.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                //data.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                //data.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                //data.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                //data.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분

            }

            // 전송내역을 저장한다.
            data.Upd_STATUS(p_sysdt, p_systm, p_user, p_conn);

            return "";
        }

        // 신생아중환자실
        public string Send(CDataASM033_003 data, bool isTmp, string p_sysdt, string p_systm, string p_user, string hosid, OleDbConnection p_conn)
        {
            if (data.SEL == false) return "";

            data.STATUS = "N"; // 진행중

            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

            // 문서 공통 정보
            //dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = data.form; // 서식코드
            //dynReq.Metadata["FOM_VER"].Value = data.ver; // 서식버전
            //dynReq.Metadata["YKIHO"].Value = hosid; // 요양기관기호
            //dynReq.Metadata["DMD_NO"].Value = data.DEMNO; // 청구번호
            //dynReq.Metadata["RCV_NO"].Value = data.CNECNO; // 접수번호(없는 경우 0000000)
            //dynReq.Metadata["RCV_YR"].Value = data.CNECTDD.Substring(0, 4); // 접수년도
            //dynReq.Metadata["BILL_SNO"].Value = data.BILLSNO; // 청구서일련번호
            //dynReq.Metadata["SP_SNO"].Value = data.EPRTNO; // 명일련
            //dynReq.Metadata["INSUP_TP_CD"].Value = data.INSUP_TP_CD; // 보험자구분코드
            //dynReq.Metadata["FOM_REF_BIZ_TP_CD"].Value = data.buss_cd; // 참고업무구분코드
            //dynReq.Metadata["DTL_BIZ_CD"].Value = data.buss_detail; // 업무상세코드
            //dynReq.Metadata["REQ_DATA_NO"].Value = ""; // 요청번호
            //dynReq.Metadata["RE_RV_RCV_ID"].Value = ""; // 재심사접수ID
            //dynReq.Metadata["HOSP_RNO"].Value = data.PID; // 환자등록번호
            //dynReq.Metadata["PAT_NM"].Value = data.PNM; // 환자명
            //dynReq.Metadata["PAT_JNO"].Value = data.RESID; // 주민번호

            // 문서 공통 정보
            SetCommonMetadata(dynReq, data, hosid);


            // --- A. 기본 정보 ---
            dynReq.Elements["IPAT_DD"].Value = data.IPAT_DD; // 입원일
            dynReq.Elements["BIRTH_PTH_CD"].Value = data.BIRTH_PTH_CD; // 최초 입실 경로
            dynReq.Elements["BIRTH_YN"].Value = data.BIRTH_YN; // 출생일 확인 여부
            dynReq.Elements["BIRTH_DT"].Value = data.BIRTH_DT; // 출생일시
            dynReq.Elements["BIRTH_PLC_CD"].Value = data.BIRTH_PLC_CD; // 출생장소
            dynReq.Elements["BIRTH_PLC_ETC_TXT"].Value = data.BIRTH_PLC_ETC_TXT; // 출생장소 기타상세
            dynReq.Elements["ASM_PARTU_FRM_CD"].Value = data.ASM_PARTU_FRM_CD; // 분만형태
            dynReq.Elements["FTUS_DEV_TRM"].Value = data.FTUS_DEV_TRM; // 재태기간
            dynReq.Elements["MEMB_YN"].Value = data.MEMB_YN; // 다태아여부
            dynReq.Elements["MEMB_TXT"].Value = data.MEMB_TXT; // 다태아 내용
            dynReq.Elements["NBY_BIRTH_BWGT"].Value = data.NBY_BIRTH_BWGT; // 출생 시 체중

            // --- B. 입실 및 퇴실 관련 항목 (반복) ---
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("SPRM_IPAT_DT");
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("CLTR_ENFC_YN");
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("CLTR_RGN_CD");
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("CLTR_RGN_ETC_TXT");
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("CLTR_ISLTN_CD");
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("INFC_CFR_YN");
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("CLTR_AF_ISLTN_CD");
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("CLTR_NEXEC_RS_CD");
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("NBY_IPAT_RS_CD");
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("RE_IPAT_RS_TXT");
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("IPAT_RS_ETC_TXT");
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("DSCG_DT");
            dynReq.Tables["TBL_IPAT_DSCG"].Columns.Add("NBY_DSCG_PTH_CD");

            for (int i = 0; i < data.SPRM_IPAT_DT.Count; i++)
            {
                dynReq.Tables["TBL_IPAT_DSCG"].AddRow();
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["SPRM_IPAT_DT"].Value = data.SPRM_IPAT_DT[i];
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["CLTR_ENFC_YN"].Value = data.CLTR_ENFC_YN[i];
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["CLTR_RGN_CD"].Value = data.CLTR_RGN_CD[i];
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["CLTR_RGN_ETC_TXT"].Value = data.CLTR_RGN_ETC_TXT[i];
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["CLTR_ISLTN_CD"].Value = data.CLTR_ISLTN_CD[i];
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["INFC_CFR_YN"].Value = data.INFC_CFR_YN[i];
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["CLTR_AF_ISLTN_CD"].Value = data.CLTR_AF_ISLTN_CD[i];
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["CLTR_NEXEC_RS_CD"].Value = data.CLTR_NEXEC_RS_CD[i];
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["NBY_IPAT_RS_CD"].Value = data.NBY_IPAT_RS_CD[i];
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["RE_IPAT_RS_TXT"].Value = data.RE_IPAT_RS_TXT[i];
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["IPAT_RS_ETC_TXT"].Value = data.IPAT_RS_ETC_TXT[i];
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["DSCG_DT"].Value = data.DSCG_DT[i];
                dynReq.Tables["TBL_IPAT_DSCG"].Rows[i]["NBY_DSCG_PTH_CD"].Value = data.NBY_DSCG_PTH_CD[i];
            }

            // --- C. 진료 관련 항목 ---
            dynReq.Elements["SGRD_ASM_ENFC_YN"].Value = data.SGRD_ASM_ENFC_YN; // 중증도 평가 시행 여부

            // --- C-1. 중증도 평가 시행 내용 (반복) ---
            dynReq.Tables["TBL_SGRD_ASM"].Columns.Add("SGRD_ASM_MASR_DT");
            dynReq.Tables["TBL_SGRD_ASM"].Columns.Add("SGRD_ASM_KND_CD");
            dynReq.Tables["TBL_SGRD_ASM"].Columns.Add("SGRD_ASM_KND_ETC_TXT");

            for (int i = 0; i < data.SGRD_ASM_MASR_DT.Count; i++)
            {
                dynReq.Tables["TBL_SGRD_ASM"].AddRow();
                dynReq.Tables["TBL_SGRD_ASM"].Rows[i]["SGRD_ASM_MASR_DT"].Value = data.SGRD_ASM_MASR_DT[i];
                dynReq.Tables["TBL_SGRD_ASM"].Rows[i]["SGRD_ASM_KND_CD"].Value = data.SGRD_ASM_KND_CD[i];
                dynReq.Tables["TBL_SGRD_ASM"].Rows[i]["SGRD_ASM_KND_ETC_TXT"].Value = data.SGRD_ASM_KND_ETC_TXT[i];
            }

            // --- D. 집중영양치료 관련 항목 ---
            dynReq.Elements["TPN_ENFC_YN"].Value = data.TPN_ENFC_YN; // TPN 시행 여부

            // --- D-1. TPN 투여일자 및 협진여부 (반복) ---
            dynReq.Tables["TBL_TPN_INJC"].Columns.Add("INJC_STA_DD");
            dynReq.Tables["TBL_TPN_INJC"].Columns.Add("INJC_END_DD");
            dynReq.Tables["TBL_TPN_INJC"].Columns.Add("TPN_DDIAG_YN");
            dynReq.Tables["TBL_TPN_INJC"].Columns.Add("DDIAG_NEXEC_RS_TXT");

            for (int i = 0; i < data.INJC_STA_DD.Count; i++)
            {
                dynReq.Tables["TBL_TPN_INJC"].AddRow();
                dynReq.Tables["TBL_TPN_INJC"].Rows[i]["INJC_STA_DD"].Value = data.INJC_STA_DD[i];
                dynReq.Tables["TBL_TPN_INJC"].Rows[i]["INJC_END_DD"].Value = data.INJC_END_DD[i];
                dynReq.Tables["TBL_TPN_INJC"].Rows[i]["TPN_DDIAG_YN"].Value = data.TPN_DDIAG_YN[i];
                dynReq.Tables["TBL_TPN_INJC"].Rows[i]["DDIAG_NEXEC_RS_TXT"].Value = data.DDIAG_NEXEC_RS_TXT[i];
            }

            // --- E. 기타 사항 ---
            //dynReq.Elements["APND_DATA_NO"].Value = ""; // 첨부



            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(hosid, isTmp ? "createTmpDoc" : "createDoc");

            //if (dynRes.ErrorMessage.StartsWith("(Token_Error)로그인 정보를 초기화합니다.(인증되지 않았거나 타기관 인증서 사용)")) return "re";
            if (!dynRes.Result)
            {
                data.STATUS = (isTmp == true ? "F" : "E"); // 오류(임시저장이 실패한 것이면 F로 표시한다)
                data.ERR_CODE = dynRes.ErrorCode;
                data.ERR_DESC = dynRes.ErrorMessage;

                data.DOC_NO = ""; //문서번호
                //data.SUPL_DATA_FOM_CD = ""; //서식코드
                //data.RCV_NO = ""; //접수번호
                //data.SP_SNO = ""; //명세서일련번호
                //data.HOSP_RNO = ""; //환자등록번호
                //data.PAT_NM = ""; //환자성명
                //data.INSUP_TP_CD = ""; //참고업무구분

            }
            else
            {
                data.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.ERR_CODE = "";
                data.ERR_DESC = "";

                data.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                //data.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                //data.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                //data.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                //data.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                //data.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                //data.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분

            }

            // 전송내역을 저장한다.
            data.Upd_STATUS(p_sysdt, p_systm, p_user, p_conn);

            return "";
        }

        // 마취
        public string Send(CDataASM035_003 data, bool isTmp, string p_sysdt, string p_systm, string p_user, string hosid, OleDbConnection p_conn)
        {
            if (data.SEL == false) return "";

            data.STATUS = "N"; // 진행중

            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

            // 문서 공통 정보
            //dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = data.form; // 서식코드
            //dynReq.Metadata["FOM_VER"].Value = data.ver; // 서식버전
            //dynReq.Metadata["YKIHO"].Value = hosid; // 요양기관기호
            //dynReq.Metadata["DMD_NO"].Value = data.DEMNO; // 청구번호
            //dynReq.Metadata["RCV_NO"].Value = data.CNECNO; // 접수번호(없는 경우 0000000)
            //dynReq.Metadata["RCV_YR"].Value = data.CNECTDD.Substring(0, 4); // 접수년도
            //dynReq.Metadata["BILL_SNO"].Value = data.BILLSNO; // 청구서일련번호
            //dynReq.Metadata["SP_SNO"].Value = data.EPRTNO; // 명일련
            //dynReq.Metadata["INSUP_TP_CD"].Value = data.INSUP_TP_CD; // 보험자구분코드
            //dynReq.Metadata["FOM_REF_BIZ_TP_CD"].Value = data.buss_cd; // 참고업무구분코드
            //dynReq.Metadata["DTL_BIZ_CD"].Value = data.buss_detail; // 업무상세코드
            //dynReq.Metadata["REQ_DATA_NO"].Value = ""; // 요청번호
            //dynReq.Metadata["RE_RV_RCV_ID"].Value = ""; // 재심사접수ID
            //dynReq.Metadata["HOSP_RNO"].Value = data.PID; // 환자등록번호
            //dynReq.Metadata["PAT_NM"].Value = data.PNM; // 환자명
            //dynReq.Metadata["PAT_JNO"].Value = data.RESID; // 주민번호

            // 문서 공통 정보
            SetCommonMetadata(dynReq, data, hosid);


            // --- A. 기본 정보 ---
            dynReq.Elements["IPAT_DD"].Value = data.IPAT_DD; // 입원일자(YYYYMMDD)
            dynReq.Elements["DSCG_YN"].Value = data.DSCG_YN; // 퇴원여부(1.Yes 2.No)
            dynReq.Elements["DSCG_DD"].Value = data.DSCG_YN == "1" ? data.DSCG_DD : ""; // 퇴원일자(YYYYMMDD)

            // --- B. 마취 정보 ---
            dynReq.Elements["NCT_STA_DT"].Value = data.NCT_STA_DT; // 마취 시작일시(YYYYMMDDHHMM)
            dynReq.Elements["NCT_END_DT"].Value = data.NCT_END_DT; // 마취 종료일시(YYYYMMDDHHMM)
            dynReq.Elements["NCT_FRM_CD"].Value = data.NCT_FRM_CD; // 마취형태 구분코드(1.정규 2.응급)
            dynReq.Elements["ASM_NCT_MTH_CD"].Value = data.ASM_NCT_MTH_CD; // 마취방법 구분코드(다중선택, 예: 1/3/7/8)
            dynReq.Elements["NCT_RS_CD"].Value = data.NCT_RS_CD; // 마취사유 구분코드(01~99)
            dynReq.Elements["MDFEE_CD"].Value = data.MDFEE_CD; // 수가코드(마취사유가 02/03/04일 때 필수)
            dynReq.Elements["MDFEE_CD_NM"].Value = data.MDFEE_CD_NM; // 수가코드명(마취사유가 02/03/04일 때 필수)

            // --- C. 마취 전 ---
            dynReq.Elements["PTNT_ASM_YN"].Value = data.PTNT_ASM_YN; // 마취 전 환자평가 시행여부(1.Yes 2.No)

            // --- D. 마취 중 ---
            dynReq.Elements["LBT_TRET_YN"].Value = data.LBT_TRET_YN; // 의도적 저체온증 적용 여부(1.Yes 2.No)
            if (data.LBT_TRET_YN == "2")
            {
                dynReq.Elements["CNTR_TMPR_MASR_YN"].Value = data.CNTR_TMPR_MASR_YN; // 연속적 체온 측정 및 기록 여부(1.Yes 2.No)
                dynReq.Elements["TMPR_RGN_CD"].Value = data.TMPR_RGN_CD; // 체온 측정방법 구분코드(다중선택, 예: 05/06/07)
                if (data.TMPR_RGN_CD.Contains("99"))
                {
                    dynReq.Elements["TMPR_RGN_ETC_TXT"].Value = data.TMPR_RGN_ETC_TXT; // 체온 측정방법 기타 상세(99일 때)
                }
                else
                {
                    dynReq.Elements["TMPR_RGN_ETC_TXT"].Value = "";
                }
                dynReq.Elements["LWET_TMPR"].Value = data.LWET_TMPR; // 최저체온(℃, 소수점 첫째자리)
            }
            else
            {
                dynReq.Elements["CNTR_TMPR_MASR_YN"].Value = "";
                dynReq.Elements["TMPR_RGN_CD"].Value = "";
                dynReq.Elements["TMPR_RGN_ETC_TXT"].Value = "";
                dynReq.Elements["LWET_TMPR"].Value = "";
            }
            
            dynReq.Elements["NRRT_BLCK_USE_YN"].Value = data.NRRT_BLCK_USE_YN; // 신경근 차단제 사용 여부(1.Yes 2.No)
            if (data.NRRT_BLCK_USE_YN == "1")
            {
                dynReq.Elements["NRRT_MNTR_YN"].Value = data.NRRT_MNTR_YN; // 신경근 감시 여부(1.Yes 2.No, 차단제 Yes일 때)
            }
            else
            {
                dynReq.Elements["NRRT_MNTR_YN"].Value = "";
            }


            // --- E. 마취 후(회복실) ---
            dynReq.Elements["RCRM_IPAT_YN"].Value = data.RCRM_IPAT_YN; // 회복실 입실 여부(1.Yes 2.No)
            if (data.RCRM_IPAT_YN == "2")
            {
                dynReq.Elements["RCRM_DSU_RS_CD"].Value = data.RCRM_DSU_RS_CD; // 회복실 미입실 사유(1~5, 입실 No일 때)
            }
            else
            {
                dynReq.Elements["RCRM_DSU_RS_CD"].Value = "";
            }
            if (data.RCRM_IPAT_YN == "1")
            {
                dynReq.Elements["EMSS_ASM_EXEC_FQ_CD"].Value = data.EMSS_ASM_EXEC_FQ_CD; // 오심 및 구토평가 실시횟수(0.미실시 1.1회 2.2회 이상)
                if (data.EMSS_ASM_EXEC_FQ_CD == "0" || data.EMSS_ASM_EXEC_FQ_CD == "1")
                {
                    dynReq.Elements["EMSS_ASM_RS_TXT"].Value = data.EMSS_ASM_RS_TXT; // 오심 및 구토평가 미실시/1회 사유
                }
                else
                {
                    dynReq.Elements["EMSS_ASM_RS_TXT"].Value = "";
                }
                dynReq.Elements["PAIN_ASM_EXEC_FQ_CD"].Value = data.PAIN_ASM_EXEC_FQ_CD; // 통증평가 실시횟수(0.미실시 1.1회 2.2회 이상)
                if (data.PAIN_ASM_EXEC_FQ_CD == "0" || data.PAIN_ASM_EXEC_FQ_CD == "1")
                {
                    dynReq.Elements["PAIN_ASM_RS_TXT"].Value = data.PAIN_ASM_RS_TXT; // 통증평가 미실시/1회 사유
                }
                else
                {
                    dynReq.Elements["PAIN_ASM_RS_TXT"].Value = "";
                }
            }
            else
            {
                dynReq.Elements["EMSS_ASM_EXEC_FQ_CD"].Value = "";
                dynReq.Elements["EMSS_ASM_RS_TXT"].Value = "";
                dynReq.Elements["PAIN_ASM_EXEC_FQ_CD"].Value = "";
                dynReq.Elements["PAIN_ASM_RS_TXT"].Value = "";
            }
            
            



            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(hosid, isTmp ? "createTmpDoc" : "createDoc");

            //if (dynRes.ErrorMessage.StartsWith("(Token_Error)로그인 정보를 초기화합니다.(인증되지 않았거나 타기관 인증서 사용)")) return "re";
            if (!dynRes.Result)
            {
                data.STATUS = (isTmp == true ? "F" : "E"); // 오류(임시저장이 실패한 것이면 F로 표시한다)
                data.ERR_CODE = dynRes.ErrorCode;
                data.ERR_DESC = dynRes.ErrorMessage;

                data.DOC_NO = ""; //문서번호
                //data.SUPL_DATA_FOM_CD = ""; //서식코드
                //data.RCV_NO = ""; //접수번호
                //data.SP_SNO = ""; //명세서일련번호
                //data.HOSP_RNO = ""; //환자등록번호
                //data.PAT_NM = ""; //환자성명
                //data.INSUP_TP_CD = ""; //참고업무구분

            }
            else
            {
                data.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.ERR_CODE = "";
                data.ERR_DESC = "";

                data.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                //data.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                //data.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                //data.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                //data.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                //data.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                //data.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분

            }

            // 전송내역을 저장한다.
            data.Upd_STATUS(p_sysdt, p_systm, p_user, p_conn);

            return "";
        }

        // 정신건강입원영역
        public string Send(CDataASM036_002 data, bool isTmp, string p_sysdt, string p_systm, string p_user, string hosid, OleDbConnection p_conn)
        {
            if (data.SEL == false) return "";

            data.STATUS = "N"; // 진행중

            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

            // 문서 공통 정보
            //dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = data.form; // 서식코드
            //dynReq.Metadata["FOM_VER"].Value = data.ver; // 서식버전
            //dynReq.Metadata["YKIHO"].Value = hosid; // 요양기관기호
            //dynReq.Metadata["DMD_NO"].Value = data.DEMNO; // 청구번호
            //dynReq.Metadata["RCV_NO"].Value = data.CNECNO; // 접수번호(없는 경우 0000000)
            //dynReq.Metadata["RCV_YR"].Value = data.CNECTDD.Substring(0, 4); // 접수년도
            //dynReq.Metadata["BILL_SNO"].Value = data.BILLSNO; // 청구서일련번호
            //dynReq.Metadata["SP_SNO"].Value = data.EPRTNO; // 명일련
            //dynReq.Metadata["INSUP_TP_CD"].Value = data.INSUP_TP_CD; // 보험자구분코드
            //dynReq.Metadata["FOM_REF_BIZ_TP_CD"].Value = data.buss_cd; // 참고업무구분코드
            //dynReq.Metadata["DTL_BIZ_CD"].Value = data.buss_detail; // 업무상세코드
            //dynReq.Metadata["REQ_DATA_NO"].Value = ""; // 요청번호
            //dynReq.Metadata["RE_RV_RCV_ID"].Value = ""; // 재심사접수ID
            //dynReq.Metadata["HOSP_RNO"].Value = data.PID; // 환자등록번호
            //dynReq.Metadata["PAT_NM"].Value = data.PNM; // 환자명
            //dynReq.Metadata["PAT_JNO"].Value = data.RESID; // 주민번호

            // 문서 공통 정보
            SetCommonMetadata(dynReq, data, hosid);

            // A. 기본정보
            dynReq.Elements["FST_IPAT_DD"].Value = data.FST_IPAT_DD;                       // 최초 입원일자
            dynReq.Elements["IPAT_DGSBJT_CD"].Value = data.IPAT_DGSBJT_CD;               // 진료과
            dynReq.Elements["TRFR_YN"].Value = data.TRFR_YN;                             // 전과 여부

            // 반복 테이블: 전과일자 (TBL_TRFR)
            dynReq.Tables["TBL_TRFR"].Columns.Add("TRFR_DD"); // 전과일자
            for (int i = 0; i < data.TRFR_DD.Count; i++)
            {
                dynReq.Tables["TBL_TRFR"].AddRow();
                dynReq.Tables["TBL_TRFR"].Rows[i]["TRFR_DD"].Value = data.TRFR_DD[i];
            }

            dynReq.Elements["INSUP_QLF_CHG_YN"].Value = data.INSUP_QLF_CHG_YN;           // 자격변동 여부

            // 반복 테이블: 자격변동일자 (TBL_QLF)
            dynReq.Tables["TBL_QLF"].Columns.Add("QLF_CHG_DD");
            for (int i = 0; i < data.QLF_CHG_DD.Count; i++)
            {
                dynReq.Tables["TBL_QLF"].AddRow();
                dynReq.Tables["TBL_QLF"].Rows[i]["QLF_CHG_DD"].Value = data.QLF_CHG_DD[i];
            }

            dynReq.Elements["DSCG_YN"].Value = data.DSCG_YN;                             // 퇴원 여부
            dynReq.Elements["DSCG_DD"].Value = data.DSCG_DD;                             // 퇴원일자
            dynReq.Elements["DSCG_STAT_CD"].Value = data.DSCG_STAT_CD;                  // 퇴원상태
            dynReq.Elements["DEATH_DD"].Value = data.DEATH_DD;                           // 사망일자
            dynReq.Elements["SPNT_IPAT_YN"].Value = data.SPNT_IPAT_YN;                   // 자발적 입원 여부

            // B.기능평가
            dynReq.Elements["FCLT_ASM_YN"].Value = data.FCLT_ASM_YN;                     // 기능평가 시행 여부

            // 반복 테이블: 기능평가 시행일자 및 도구 (TBL_FCLT_ASM)
            dynReq.Tables["TBL_FCLT_ASM"].Columns.Add("FCLT_ASM_TL_ENFC_DD");        // 시행일자
            dynReq.Tables["TBL_FCLT_ASM"].Columns.Add("PSYCHI_HTH_FCLT_ASM_TL_CD");  // 시행도구 코드
            dynReq.Tables["WHO12_FCLT_ASM_PNT"].Columns.Add("WHO12_FCLT_ASM_PNT");       // WHODAS-12 점수
            dynReq.Tables["WHO36_FCLT_ASM_PNT"].Columns.Add("WHO36_FCLT_ASM_PNT");       // WHODAS-36 점수
            dynReq.Tables["HONOS_FCLT_ASM_PNT"].Columns.Add("HONOS_FCLT_ASM_PNT");       // HoNOS 점수
            dynReq.Tables["GAF_FCLT_ASM_PNT"].Columns.Add("GAF_FCLT_ASM_PNT");           // GAF 점수
            dynReq.Tables["CGI_FCLT_ASM_PNT"].Columns.Add("CGI_FCLT_ASM_PNT");           // CGI 점수

            for (int i = 0; i < data.FCLT_ASM_TL_ENFC_DD.Count; i++)
            {
                dynReq.Tables["TBL_FCLT_ASM"].AddRow();
                dynReq.Tables["TBL_FCLT_ASM"].Rows[i]["FCLT_ASM_TL_ENFC_DD"].Value = data.FCLT_ASM_TL_ENFC_DD[i];
                dynReq.Tables["TBL_FCLT_ASM"].Rows[i]["PSYCHI_HTH_FCLT_ASM_TL_CD"].Value = data.PSYCHI_HTH_FCLT_ASM_TL_CD[i];
                dynReq.Tables["WHO12_FCLT_ASM_PNT"].Rows[i]["WHO12_FCLT_ASM_PNT"].Value = data.WHO12_FCLT_ASM_PNT[i];       // WHODAS-12 점수
                dynReq.Tables["WHO36_FCLT_ASM_PNT"].Rows[i]["WHO36_FCLT_ASM_PNT"].Value = data.WHO36_FCLT_ASM_PNT[i];       // WHODAS-36 점수
                dynReq.Tables["HONOS_FCLT_ASM_PNT"].Rows[i]["HONOS_FCLT_ASM_PNT"].Value = data.HONOS_FCLT_ASM_PNT[i];       // HoNOS 점수
                dynReq.Tables["GAF_FCLT_ASM_PNT"].Rows[i]["GAF_FCLT_ASM_PNT"].Value = data.GAF_FCLT_ASM_PNT[i];           // GAF 점수
                dynReq.Tables["CGI_FCLT_ASM_PNT"].Rows[i]["CGI_FCLT_ASM_PNT"].Value = data.CGI_FCLT_ASM_PNT[i];           // CGI 점수
            }

            // C. 지역사회서비스 연계 의뢰
            dynReq.Elements["DSCG_POTM_PSYCHI_DS_DIAG_YN"].Value = data.DSCG_POTM_PSYCHI_DS_DIAG_YN; // 퇴원시 조현병 여부
            dynReq.Elements["PLC_SCTY_SVC_CONN_REQ_YN"].Value = data.PLC_SCTY_SVC_CONN_REQ_YN;
            dynReq.Elements["PLC_SCTY_SVC_CONN_NREQ_RS_CD"].Value = data.PLC_SCTY_SVC_CONN_NREQ_RS_CD;
            dynReq.Elements["PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT"].Value = data.PLC_SCTY_SVC_CONN_NREQ_RS_ETC_TXT;

            // D. 퇴원시 환자 경험도 조사
            dynReq.Elements["DSCG_EXPR_INVT_ENFC_YN"].Value = data.DSCG_EXPR_INVT_ENFC_YN;
            dynReq.Elements["DSCG_EXPR_NOPER_RS_CD"].Value = data.DSCG_EXPR_NOPER_RS_CD;
            dynReq.Elements["NOPER_RS_ETC_TXT"].Value = data.NOPER_RS_ETC_TXT;



            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(hosid, isTmp ? "createTmpDoc" : "createDoc");

            //if (dynRes.ErrorMessage.StartsWith("(Token_Error)로그인 정보를 초기화합니다.(인증되지 않았거나 타기관 인증서 사용)")) return "re";
            if (!dynRes.Result)
            {
                data.STATUS = (isTmp == true ? "F" : "E"); // 오류(임시저장이 실패한 것이면 F로 표시한다)
                data.ERR_CODE = dynRes.ErrorCode;
                data.ERR_DESC = dynRes.ErrorMessage;

                data.DOC_NO = ""; //문서번호
                //data.SUPL_DATA_FOM_CD = ""; //서식코드
                //data.RCV_NO = ""; //접수번호
                //data.SP_SNO = ""; //명세서일련번호
                //data.HOSP_RNO = ""; //환자등록번호
                //data.PAT_NM = ""; //환자성명
                //data.INSUP_TP_CD = ""; //참고업무구분

            }
            else
            {
                data.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.ERR_CODE = "";
                data.ERR_DESC = "";

                data.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                //data.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                //data.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                //data.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                //data.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                //data.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                //data.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분

            }

            // 전송내역을 저장한다.
            data.Upd_STATUS(p_sysdt, p_systm, p_user, p_conn);

            return "";

        }

        // 수혈
        public string Send(CDataASM037_003 data, bool isTmp, string p_sysdt, string p_systm, string p_user, string hosid, OleDbConnection p_conn)
        {
            if (data.SEL == false) return "";

            data.STATUS = "N"; // 진행중

            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

            // 문서 공통 정보
            //dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = data.form; // 서식코드
            //dynReq.Metadata["FOM_VER"].Value = data.ver; // 서식버전
            //dynReq.Metadata["YKIHO"].Value = hosid; // 요양기관기호
            //dynReq.Metadata["DMD_NO"].Value = data.DEMNO; // 청구번호
            //dynReq.Metadata["RCV_NO"].Value = data.CNECNO; // 접수번호(없는 경우 0000000)
            //dynReq.Metadata["RCV_YR"].Value = data.CNECTDD.Substring(0, 4); // 접수년도
            //dynReq.Metadata["BILL_SNO"].Value = data.BILLSNO; // 청구서일련번호
            //dynReq.Metadata["SP_SNO"].Value = data.EPRTNO; // 명일련
            //dynReq.Metadata["INSUP_TP_CD"].Value = data.INSUP_TP_CD; // 보험자구분코드
            //dynReq.Metadata["FOM_REF_BIZ_TP_CD"].Value = data.buss_cd; // 참고업무구분코드
            //dynReq.Metadata["DTL_BIZ_CD"].Value = data.buss_detail; // 업무상세코드
            //dynReq.Metadata["REQ_DATA_NO"].Value = ""; // 요청번호
            //dynReq.Metadata["RE_RV_RCV_ID"].Value = ""; // 재심사접수ID
            //dynReq.Metadata["HOSP_RNO"].Value = data.PID; // 환자등록번호
            //dynReq.Metadata["PAT_NM"].Value = data.PNM; // 환자명
            //dynReq.Metadata["PAT_JNO"].Value = data.RESID; // 주민번호

            // 문서 공통 정보
            SetCommonMetadata(dynReq, data, hosid);

            // A. 기본 정보
            dynReq.Elements["IPAT_DD"].Value = data.IPAT_DD; // 입원일자(YYYYMMDD)
            dynReq.Elements["DSCG_DD"].Value = data.DSCG_DD; // 퇴원일자(YYYYMMDD)

            // B. 수술 정보

            // 1. 수술
            dynReq.Tables["TBL_SOPR"].Columns.Add("ASM_OPRM_IPAT_DT"); // 수술실 입실일시
            dynReq.Tables["TBL_SOPR"].Columns.Add("ASM_OPRM_DSCG_DT"); // 수술실 퇴실일시
            dynReq.Tables["TBL_SOPR"].Columns.Add("ASM_RCRM_DSCG_DT"); // 회복실 퇴실일시
            dynReq.Tables["TBL_SOPR"].Columns.Add("SOPR_NM"); // 수술명
            dynReq.Tables["TBL_SOPR"].Columns.Add("MDFEE_CD"); // 수가코드

            for (int i = 0; i < data.ASM_OPRM_IPAT_DT.Count; i++)
            {
                dynReq.Tables["TBL_SOPR"].AddRow();
                dynReq.Tables["TBL_SOPR"].Rows[i]["ASM_OPRM_IPAT_DT"].Value = data.ASM_OPRM_IPAT_DT[i];
                dynReq.Tables["TBL_SOPR"].Rows[i]["ASM_OPRM_DSCG_DT"].Value = data.ASM_OPRM_DSCG_DT[i];
                dynReq.Tables["TBL_SOPR"].Rows[i]["ASM_RCRM_DSCG_DT"].Value = data.ASM_RCRM_DSCG_DT[i];
                dynReq.Tables["TBL_SOPR"].Rows[i]["SOPR_NM"].Value = data.SOPR_NM[i];
                dynReq.Tables["TBL_SOPR"].Rows[i]["MDFEE_CD"].Value = data.SOPR_MDFEE_CD[i];
            }

            dynReq.Elements["LFB_FS_YN"].Value = data.LFB_FS_YN; // 척추후방고정술 실시여부
            dynReq.Elements["LFB_FS_LVL"].Value = data.LFB_FS_LVL; // 척추후방고정술 Level
            dynReq.Elements["KNJN_RPMT_YN"].Value = data.KNJN_RPMT_YN; // 슬관절치환술 실시여부
            dynReq.Elements["KNJN_RPMT_RGN_CD"].Value = data.KNJN_RPMT_RGN_CD; // 슬관절치환술 부위

            // C. 수혈 체크리스트 사용 현황
            dynReq.Elements["ASM_PRSC_YN"].Value = data.ASM_PRSC_YN; // 처방여부
            
            dynReq.Tables["TBL_PRSC_TXT"].Columns.Add("ASM_PRSC_DT"); // 처방일시
            dynReq.Tables["TBL_PRSC_TXT"].Columns.Add("ASM_PRSC_UNIT_CNT"); // 처방량
            dynReq.Tables["TBL_PRSC_TXT"].Columns.Add("ASM_BLTS_CHKLST_USE_YN"); // 체크리스트 사용여부
            dynReq.Tables["TBL_PRSC_TXT"].Columns.Add("ASM_BLTS_STA_DT"); // 수혈시작일시
            dynReq.Tables["TBL_PRSC_TXT"].Columns.Add("BLTS_DGM_NM"); // 수혈제제명
            dynReq.Tables["TBL_PRSC_TXT"].Columns.Add("MDFEE_CD"); // 수가코드
            dynReq.Tables["TBL_PRSC_TXT"].Columns.Add("BLTS_UNIT_CNT"); // 수혈량

            for (int i = 0; i < data.ASM_PRSC_DT.Count; i++)
            {
                dynReq.Tables["TBL_PRSC_TXT"].AddRow();
                dynReq.Tables["TBL_PRSC_TXT"].Rows[i]["ASM_PRSC_DT"].Value = data.ASM_PRSC_DT[i];
                dynReq.Tables["TBL_PRSC_TXT"].Rows[i]["ASM_PRSC_UNIT_CNT"].Value = data.ASM_PRSC_UNIT_CNT[i];
                dynReq.Tables["TBL_PRSC_TXT"].Rows[i]["ASM_BLTS_CHKLST_USE_YN"].Value = data.ASM_BLTS_CHKLST_USE_YN[i];
                dynReq.Tables["TBL_PRSC_TXT"].Rows[i]["ASM_BLTS_STA_DT"].Value = data.ASM_BLTS_STA_DT[i];
                dynReq.Tables["TBL_PRSC_TXT"].Rows[i]["BLTS_DGM_NM"].Value = data.ASM_PRSC_BLTS_DGM_NM[i];
                dynReq.Tables["TBL_PRSC_TXT"].Rows[i]["MDFEE_CD"].Value = data.ASM_PRSC_MDFEE_CD[i];
                dynReq.Tables["TBL_PRSC_TXT"].Rows[i]["BLTS_UNIT_CNT"].Value = data.ASM_BLTS_UNIT_CNT[i];
            }


            // D. 투약 정보
            dynReq.Elements["ANM_DIAG_YN"].Value = data.ANM_DIAG_YN; // 빈혈 진단
            
            dynReq.Tables["TBL_ANM_DIAG"].Columns.Add("SICK_SYM"); // 상병분류기호
            dynReq.Tables["TBL_ANM_DIAG"].Columns.Add("DIAG_NM"); // 진단명
            for (int i = 0; i < data.SICK_SYM.Count; i++)
            {
                dynReq.Tables["TBL_ANM_DIAG"].AddRow();
                dynReq.Tables["TBL_ANM_DIAG"].Rows[i]["SICK_SYM"].Value = data.SICK_SYM[i];
                dynReq.Tables["TBL_ANM_DIAG"].Rows[i]["DIAG_NM"].Value = data.DIAG_NM[i];
            }

            dynReq.Elements["ANM_REFM_YN"].Value = data.ANM_REFM_YN; // 빈혈교정 유무

            dynReq.Tables["TBL_ANM_REFM_MDS"].Columns.Add("MDS_NM"); // 약품명
            dynReq.Tables["TBL_ANM_REFM_MDS"].Columns.Add("MDS_CD"); // 약품코드

            for (int i = 0; i < data.SICK_SYM.Count; i++)
            {
                dynReq.Tables["TBL_ANM_REFM_MDS"].AddRow();
                dynReq.Tables["TBL_ANM_REFM_MDS"].Rows[i]["MDS_NM"].Value = data.MDS_NM[i];
                dynReq.Tables["TBL_ANM_REFM_MDS"].Rows[i]["MDS_CD"].Value = data.MDS_CD[i];
            }

            // E. 검사 정보
            dynReq.Elements["HG_EXM_ENFC_YN"].Value = data.HG_EXM_ENFC_YN; // Hb검사 시행여부
            
            dynReq.Tables["TBL_HG_EXM"].Columns.Add("ASM_EXM_RST_DT"); // 검사결과일시
            dynReq.Tables["TBL_HG_EXM"].Columns.Add("MDFEE_CD"); // 수가코드
            dynReq.Tables["TBL_HG_EXM"].Columns.Add("EXM_NM"); // 검사명
            dynReq.Tables["TBL_HG_EXM"].Columns.Add("HG_NUV"); // 검사결과

            for (int i = 0; i < data.ASM_EXM_RST_DT.Count; i++)
            {
                dynReq.Tables["TBL_HG_EXM"].AddRow();
                dynReq.Tables["TBL_HG_EXM"].Rows[i]["ASM_EXM_RST_DT"].Value = data.ASM_EXM_RST_DT[i];
                dynReq.Tables["TBL_HG_EXM"].Rows[i]["MDFEE_CD"].Value = data.EXM_MDFEE_CD[i];
                dynReq.Tables["TBL_HG_EXM"].Rows[i]["EXM_NM"].Value = data.EXM_NM[i];
                dynReq.Tables["TBL_HG_EXM"].Rows[i]["HG_NUV"].Value = data.HG_NUV[i];
            }



            // F. 수혈 정보
            dynReq.Elements["BLTS_YN"].Value = data.BLTS_YN; // 수혈 시행여부
            
            dynReq.Tables["TBL_BLTS_ENFC_TXT"].Columns.Add("ASM_BLTS_STA_DT"); // 수혈시작일시
            dynReq.Tables["TBL_BLTS_ENFC_TXT"].Columns.Add("ASM_BLTS_END_DT"); // 수혈종료일시
            dynReq.Tables["TBL_BLTS_ENFC_TXT"].Columns.Add("BLTS_DGM_NM"); // 수혈제제명
            dynReq.Tables["TBL_BLTS_ENFC_TXT"].Columns.Add("MDFEE_CD"); // 수가코드
            dynReq.Tables["TBL_BLTS_ENFC_TXT"].Columns.Add("BLTS_UNIT_CNT"); // 수혈량
            dynReq.Tables["TBL_BLTS_ENFC_TXT"].Columns.Add("HG_DCR_YN"); // Hb저하 발생 여부
            dynReq.Tables["TBL_BLTS_ENFC_TXT"].Columns.Add("OPRM_HMRHG_OCUR_YN_CD"); // 수술 관련 실혈 발생 여부
            dynReq.Tables["TBL_BLTS_ENFC_TXT"].Columns.Add("OPRM_MIDD_HMRHG_QTY"); // 수술 중 실혈량
            dynReq.Tables["TBL_BLTS_ENFC_TXT"].Columns.Add("OPRM_AF_DRN_QTY"); // 수술 후 배액량
            dynReq.Tables["TBL_BLTS_ENFC_TXT"].Columns.Add("BLTS_RS_ETC_YN"); // 그 외 수혈사유 여부
            dynReq.Tables["TBL_BLTS_ENFC_TXT"].Columns.Add("BLTS_RS_ETC_TXT"); // 수혈사유 기타 상세

            for (int i = 0; i < data.BLTS_STA_DT.Count; i++)
            {
                dynReq.Tables["TBL_BLTS_ENFC_TXT"].AddRow();
                dynReq.Tables["TBL_BLTS_ENFC_TXT"].Rows[i]["ASM_BLTS_STA_DT"].Value = data.BLTS_STA_DT[i];
                dynReq.Tables["TBL_BLTS_ENFC_TXT"].Rows[i]["ASM_BLTS_END_DT"].Value = data.BLTS_END_DT[i];
                dynReq.Tables["TBL_BLTS_ENFC_TXT"].Rows[i]["BLTS_DGM_NM"].Value = data.BLTS_DGM_NM[i];
                dynReq.Tables["TBL_BLTS_ENFC_TXT"].Rows[i]["MDFEE_CD"].Value = data.BLTS_MDFEE_CD[i];
                dynReq.Tables["TBL_BLTS_ENFC_TXT"].Rows[i]["BLTS_UNIT_CNT"].Value = data.BLTS_UNIT_CNT[i];
                dynReq.Tables["TBL_BLTS_ENFC_TXT"].Rows[i]["HG_DCR_YN"].Value = data.HG_DCR_YN[i];
                dynReq.Tables["TBL_BLTS_ENFC_TXT"].Rows[i]["OPRM_HMRHG_OCUR_YN_CD"].Value = data.OPRM_HMRHG_OCUR_YN_CD[i];
                dynReq.Tables["TBL_BLTS_ENFC_TXT"].Rows[i]["OPRM_MIDD_HMRHG_QTY"].Value = data.OPRM_MIDD_HMRHG_QTY[i];
                dynReq.Tables["TBL_BLTS_ENFC_TXT"].Rows[i]["OPRM_AF_DRN_QTY"].Value = data.OPRM_AF_DRN_QTY[i];
                dynReq.Tables["TBL_BLTS_ENFC_TXT"].Rows[i]["BLTS_RS_ETC_YN"].Value = data.BLTS_RS_ETC_YN[i];
                dynReq.Tables["TBL_BLTS_ENFC_TXT"].Rows[i]["BLTS_RS_ETC_TXT"].Value = data.BLTS_RS_ETC_TXT[i];
            }

            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(hosid, isTmp ? "createTmpDoc" : "createDoc");

            //if (dynRes.ErrorMessage.StartsWith("(Token_Error)로그인 정보를 초기화합니다.(인증되지 않았거나 타기관 인증서 사용)")) return "re";
            if (!dynRes.Result)
            {
                data.STATUS = (isTmp == true ? "F" : "E"); // 오류(임시저장이 실패한 것이면 F로 표시한다)
                data.ERR_CODE = dynRes.ErrorCode;
                data.ERR_DESC = dynRes.ErrorMessage;

                data.DOC_NO = ""; //문서번호
                //data.SUPL_DATA_FOM_CD = ""; //서식코드
                //data.RCV_NO = ""; //접수번호
                //data.SP_SNO = ""; //명세서일련번호
                //data.HOSP_RNO = ""; //환자등록번호
                //data.PAT_NM = ""; //환자성명
                //data.INSUP_TP_CD = ""; //참고업무구분

            }
            else
            {
                data.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.ERR_CODE = "";
                data.ERR_DESC = "";

                data.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                //data.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                //data.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                //data.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                //data.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                //data.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                //data.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분

            }

            // 전송내역을 저장한다.
            data.Upd_STATUS(p_sysdt, p_systm, p_user, p_conn);

            return "";
        }

        // 영상검사
        public string Send(CDataASM049_001 data, bool isTmp, string p_sysdt, string p_systm, string p_user, string hosid, OleDbConnection p_conn)
        {
            if (data.SEL == false) return "";

            data.STATUS = "N"; // 진행중

            HIRA.DynamicEntry.Model.DynamicRequest dynReq = new HIRA.DynamicEntry.Model.DynamicRequest();

            // 문서 공통 정보
            //dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = data.form; // 서식코드
            //dynReq.Metadata["FOM_VER"].Value = data.ver; // 서식버전
            //dynReq.Metadata["YKIHO"].Value = hosid; // 요양기관기호
            //dynReq.Metadata["DMD_NO"].Value = data.DEMNO; // 청구번호
            //dynReq.Metadata["RCV_NO"].Value = data.CNECNO; // 접수번호(없는 경우 0000000)
            //dynReq.Metadata["RCV_YR"].Value = data.CNECTDD.Substring(0, 4); // 접수년도
            //dynReq.Metadata["BILL_SNO"].Value = data.BILLSNO; // 청구서일련번호
            //dynReq.Metadata["SP_SNO"].Value = data.EPRTNO; // 명일련
            //dynReq.Metadata["INSUP_TP_CD"].Value = data.INSUP_TP_CD; // 보험자구분코드
            //dynReq.Metadata["FOM_REF_BIZ_TP_CD"].Value = data.buss_cd; // 참고업무구분코드
            //dynReq.Metadata["DTL_BIZ_CD"].Value = data.buss_detail; // 업무상세코드
            //dynReq.Metadata["REQ_DATA_NO"].Value = ""; // 요청번호
            //dynReq.Metadata["RE_RV_RCV_ID"].Value = ""; // 재심사접수ID
            //dynReq.Metadata["HOSP_RNO"].Value = data.PID; // 환자등록번호
            //dynReq.Metadata["PAT_NM"].Value = data.PNM; // 환자명
            //dynReq.Metadata["PAT_JNO"].Value = data.RESID; // 주민번호

            // 문서 공통 정보
            SetCommonMetadata(dynReq, data, hosid);

            // --- A. 환자 정보 확인 ---
            dynReq.Elements["IPAT_OPAT_TP_CD"].Value = data.IPAT_OPAT_TP_CD; // 청구유형
            dynReq.Elements["IPAT_DD"].Value = data.IPAT_DD; // 입원일
            dynReq.Elements["EMRRM_PASS_YN"].Value = data.EMRRM_PASS_YN; // 응급실을 통한 입원 여부
            dynReq.Elements["EMRRM_VST_DD"].Value = data.EMRRM_VST_DD; // 응급실 내원일
            dynReq.Elements["DSCG_DD"].Value = data.DSCG_DD; // 퇴원일
            dynReq.Elements["DIAG_DD"].Value = data.DIAG_DD; // 내원일

            // --- B. 검사정보 ---
            dynReq.Elements["IMG_EXM_KND_CD"].Value = data.IMG_EXM_KND_CD; // 검사종류
            dynReq.Elements["PET_KND_CD"].Value = data.PET_KND_CD; // PET 종류

            // --- C. CT 검사 ---
            dynReq.Elements["CT_DIAG_TY_CD"].Value = data.CT_DIAG_TY_CD; // CT 진료유형
            dynReq.Elements["CT_PGP_INTNT_TP_CD"].Value = data.CT_PGP_INTNT_TP_CD; // CT 촬영목적
            // CT 검사정보
            dynReq.Tables["TBL_CT_EXM"].Columns.Add("MDFEE_CD");
            dynReq.Tables["TBL_CT_EXM"].Columns.Add("MDFEE_CD_NM");
            dynReq.Tables["TBL_CT_EXM"].Columns.Add("EXM_EXEC_DT");
            dynReq.Tables["TBL_CT_EXM"].Columns.Add("RD_SDR_DCT_YN");
            dynReq.Tables["TBL_CT_EXM"].Columns.Add("DCT_RST_DD");
            for (int i = 0; i < data.CT_MDFEE_CD.Count; i++)
            {
                dynReq.Tables["TBL_CT_EXM"].AddRow();
                dynReq.Tables["TBL_CT_EXM"].Rows[i]["MDFEE_CD"].Value = data.CT_MDFEE_CD[i];
                dynReq.Tables["TBL_CT_EXM"].Rows[i]["MDFEE_CD_NM"].Value = data.CT_MDFEE_CD_NM[i];
                dynReq.Tables["TBL_CT_EXM"].Rows[i]["EXM_EXEC_DT"].Value = data.CT_EXM_EXEC_DT[i];
                dynReq.Tables["TBL_CT_EXM"].Rows[i]["RD_SDR_DCT_YN"].Value = data.CT_RD_SDR_DCT_YN[i];
                dynReq.Tables["TBL_CT_EXM"].Rows[i]["DCT_RST_DD"].Value = data.CT_DCT_RST_DD[i];
            }

            dynReq.Elements["CT_CTRST_USE_YN"].Value = data.CT_CTRST_USE_YN; // CT 조영제 사용
            dynReq.Elements["CT_CTRST_MDCT_PTH_CD"].Value = data.CT_CTRST_MDCT_PTH_CD; // CT 조영제 투약 경로
            dynReq.Elements["CT_CTRST_PTNT_ASM_RCD_YN"].Value = data.CT_CTRST_PTNT_ASM_RCD_YN; // CT 환자 평가 기록 유무
            dynReq.Elements["CT_CTRST_PTNT_ASM_RCD_DD"].Value = data.CT_CTRST_PTNT_ASM_RCD_DD; // CT 환자 평가 기록 일자
            dynReq.Elements["CT_KDNY_FCLT_EXM_YN"].Value = data.CT_KDNY_FCLT_EXM_YN; // CT 신장기능검사 유무


            // --- D. MRI 검사 ---
            dynReq.Elements["MRI_DIAG_TY_CD"].Value = data.MRI_DIAG_TY_CD; // MRI 진료유형
            // MRI 검사정보
            dynReq.Tables["TBL_MRI_EXM"].Columns.Add("MDFEE_CD");
            dynReq.Tables["TBL_MRI_EXM"].Columns.Add("MDFEE_CD_NM");
            dynReq.Tables["TBL_MRI_EXM"].Columns.Add("EXM_EXEC_DT");
            dynReq.Tables["TBL_MRI_EXM"].Columns.Add("RD_SDR_DCT_YN");
            dynReq.Tables["TBL_MRI_EXM"].Columns.Add("DCT_RST_DD");
            for (int i = 0; i < data.MRI_MDFEE_CD.Count; i++)
            {
                dynReq.Tables["TBL_MRI_EXM"].AddRow();
                dynReq.Tables["TBL_MRI_EXM"].Rows[i]["MDFEE_CD"].Value = data.MRI_MDFEE_CD[i];
                dynReq.Tables["TBL_MRI_EXM"].Rows[i]["MDFEE_CD_NM"].Value = data.MRI_MDFEE_CD_NM[i];
                dynReq.Tables["TBL_MRI_EXM"].Rows[i]["EXM_EXEC_DT"].Value = data.MRI_EXM_EXEC_DT[i];
                dynReq.Tables["TBL_MRI_EXM"].Rows[i]["RD_SDR_DCT_YN"].Value = data.MRI_RD_SDR_DCT_YN[i];
                dynReq.Tables["TBL_MRI_EXM"].Rows[i]["DCT_RST_DD"].Value = data.MRI_DCT_RST_DD[i];
            }

            dynReq.Elements["MRI_CTRST_USE_YN"].Value = data.MRI_CTRST_USE_YN; // MRI 조영제 사용
            dynReq.Elements["MRI_CTRST_PTNT_ASM_RCD_YN"].Value = data.MRI_CTRST_PTNT_ASM_RCD_YN; // MRI 환자 평가 기록 유무
            dynReq.Elements["MRI_CTRST_PTNT_ASM_RCD_DD"].Value = data.MRI_CTRST_PTNT_ASM_RCD_DD; // MRI 환자 평가 기록 일자
            dynReq.Elements["MRI_KDNY_FCLT_EXM_YN"].Value = data.MRI_KDNY_FCLT_EXM_YN; // MRI 신장기능검사 유무
            dynReq.Elements["BF_MRI_PTNT_ASM_RCD_YN"].Value = data.BF_MRI_PTNT_ASM_RCD_YN; // MRI전 환자 평가 기록 유무
            dynReq.Elements["BF_MRI_PTNT_ASM_RCD_DD"].Value = data.BF_MRI_PTNT_ASM_RCD_DD; // MRI전 환자 평가 기록 일자


            // --- E. PET 검사 ---
            dynReq.Tables["TBL_PET_EXM"].Columns.Add("MDFEE_CD");
            dynReq.Tables["TBL_PET_EXM"].Columns.Add("MDFEE_CD_NM");
            dynReq.Tables["TBL_PET_EXM"].Columns.Add("EXM_EXEC_DT");
            for (int i = 0; i < data.PET_MDFEE_CD.Count; i++)
            {
                dynReq.Tables["TBL_PET_EXM"].AddRow();
                dynReq.Tables["TBL_PET_EXM"].Rows[i]["MDFEE_CD"].Value = data.PET_MDFEE_CD[i];
                dynReq.Tables["TBL_PET_EXM"].Rows[i]["MDFEE_CD_NM"].Value = data.PET_MDFEE_CD_NM[i];
                dynReq.Tables["TBL_PET_EXM"].Rows[i]["EXM_EXEC_DT"].Value = data.PET_EXM_EXEC_DT[i];
            }

            dynReq.Elements["FDG_INJC_QTY_RCD_YN"].Value = data.FDG_INJC_QTY_RCD_YN; // F-18 FDG 투여량 기록 유무
            dynReq.Elements["FDG_TOT_INJC_QTY"].Value = data.FDG_TOT_INJC_QTY; // F-18 FDG 투여량
            dynReq.Elements["FDG_UNIT"].Value = data.FDG_UNIT; // 단위
            dynReq.Elements["HEIG"].Value = data.HEIG; // 키
            dynReq.Elements["BWGT"].Value = data.BWGT; // 몸무게

            // --- F. 기타 사항 ---
            //dynReq.Elements["APND_DATA_NO"].Value = ""; // 첨부
            
            
           
            // requestData - createDoc    최종 제출
            //               createTmpDoc 임시 제출
            //               updateTmpDoc 수정 제출
            //               selectDocList 목록 조회
            //               selectDocLink 미리보기
            //               selectDoc     상세조회
            HIRA.DynamicEntry.ResponseModel.DynamicResponse dynRes = dynReq.requestData(hosid, isTmp ? "createTmpDoc" : "createDoc");

            //if (dynRes.ErrorMessage.StartsWith("(Token_Error)로그인 정보를 초기화합니다.(인증되지 않았거나 타기관 인증서 사용)")) return "re";
            if (!dynRes.Result)
            {
                data.STATUS = (isTmp == true ? "F" : "E"); // 오류(임시저장이 실패한 것이면 F로 표시한다)
                data.ERR_CODE = dynRes.ErrorCode;
                data.ERR_DESC = dynRes.ErrorMessage;

                data.DOC_NO = ""; //문서번호
                //data.SUPL_DATA_FOM_CD = ""; //서식코드
                //data.RCV_NO = ""; //접수번호
                //data.SP_SNO = ""; //명세서일련번호
                //data.HOSP_RNO = ""; //환자등록번호
                //data.PAT_NM = ""; //환자성명
                //data.INSUP_TP_CD = ""; //참고업무구분

            }
            else
            {
                data.STATUS = (isTmp == true ? "T" : "Y"); // 성공(임시저장이 성공한 것이면 T로 표시한다)
                data.ERR_CODE = "";
                data.ERR_DESC = "";

                data.DOC_NO = dynRes.Datas["DOC_NO"].Value; //문서번호
                //data.SUPL_DATA_FOM_CD = dynRes.Datas["SUPL_DATA_FOM_CD"].Value; //서식코드
                //data.RCV_NO = dynRes.Datas["RCV_NO"].Value; //접수번호
                //data.SP_SNO = dynRes.Datas["SP_SNO"].Value; //명세서일련번호
                //data.HOSP_RNO = dynRes.Datas["HOSP_RNO"].Value; //환자등록번호
                //data.PAT_NM = dynRes.Datas["PAT_NM"].Value; //환자성명
                //data.INSUP_TP_CD = dynRes.Datas["INSUP_TP_CD"].Value; //참고업무구분

            }

            // 전송내역을 저장한다.
            data.Upd_STATUS(p_sysdt, p_systm, p_user, p_conn);

            return "";
        }

        private void SetCommonMetadata(HIRA.DynamicEntry.Model.DynamicRequest dynReq, CData data, string hosid)
        {
            // 문서 공통 정보
            dynReq.Metadata["SUPL_DATA_FOM_CD"].Value = data.form; //서식코드
            dynReq.Metadata["FOM_VER"].Value = data.ver; // 서식버전
            dynReq.Metadata["YKIHO"].Value = hosid; // 요양기관기호
            dynReq.Metadata["DMD_NO"].Value = data.DEMNO; // 청구번호
            dynReq.Metadata["RCV_NO"].Value = data.CNECNO; // 접수번호(없는 경우 0000000)
            dynReq.Metadata["RCV_YR"].Value = data.CNECTDD.Substring(0, 4); // 접수년도
            dynReq.Metadata["BILL_SNO"].Value = data.BILLSNO; // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)
            dynReq.Metadata["SP_SNO"].Value = data.EPRTNO; // 명일련
            dynReq.Metadata["INSUP_TP_CD"].Value = data.INSUP_TP_CD; // 보험자구분코드(4.보험 5.보호 7.보훈 8.자보)
            dynReq.Metadata["FOM_REF_BIZ_TP_CD"].Value = data.buss_cd; // 참고업무구분코드(01.1차심사 02.심사보완 03.이의신청 04.평가 05.진료비민원 06.신포괄 08.난임시술 09.진료의뢰회송 11.분석심사)
            dynReq.Metadata["DTL_BIZ_CD"].Value = data.buss_detail; // 업무상세코드
            dynReq.Metadata["REQ_DATA_NO"].Value = ""; // 요청번호(참고업무 구분코드가 '03: 이의신청, 05: 진료비민원, 06:신포괄, 09: 진료의뢰회송‘인 경우 필수 기재)
            dynReq.Metadata["RE_RV_RCV_ID"].Value = ""; // 재심사접수ID(1)업무상세코드가 이의신청: SUP(보완)인 경우 필수 기재)
            dynReq.Metadata["HOSP_RNO"].Value = data.PID; // 환자등록번호.병원에서 부여한 번호
            dynReq.Metadata["PAT_NM"].Value = data.PNM; // 환자명
            dynReq.Metadata["PAT_JNO"].Value = data.RESID; // 주민번호
        }
    }
}
