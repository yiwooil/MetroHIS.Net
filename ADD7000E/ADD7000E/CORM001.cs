using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HIRA.EformEntry;
using HIRA.EformEntry.Model;
using HIRA.EformEntry.ResponseModel;

namespace ADD7000E
{
    class CORM001
    {
        public Document GetDocument(CDataTI2A i2a, CHosInfo p_HosInfo)
        {
            CDataTT41A t41a = new CDataTT41A();
            t41a.Clear();
            t41a.SetData(i2a);

            // 영수증
            Document doc = new Document();

            // Metadata
            doc.Metadata["SUPL_DATA_FOM_CD"].Value = "ORM001"; // 서식코드
            doc.Metadata["FOM_VER"].Value = "001"; // 서식버전
            doc.Metadata["YKIHO"].Value = p_HosInfo.GetHosId(); // 요양기관기호

            doc.Metadata["DMD_NO"].Value = i2a.DEMNO; // 청구번호
            doc.Metadata["RCV_NO"].Value = i2a.CNECNO; // 접수번호. 접수번호가 없는 경우 0000000
            doc.Metadata["RCV_YR"].Value = i2a.RCV_YR; // 접수년도 CCYY
            doc.Metadata["BILL_SNO"].Value = i2a.DCOUNT; // 청구서일련번호(접수전이면 0, 원청구는 1, 보완청구는 심결통보서에 있는 번호)
            doc.Metadata["SP_SNO"].Value = i2a.EPRTNO; // 명세서 일련번호
            doc.Metadata["INSUP_TP_CD"].Value = i2a.INSUP_TP_CD; // 보험자구분코드 (4:건강보험 5:의료급여 7:보훈 8:지동차보험)
            doc.Metadata["FOM_REF_BIZ_TP_CD"].Value = "06"; // 참고업무구분코드 (01:1차심사 02:심사보완 03:이의신청 04:평가 05:진료비민원 06:신포괄 99:기타)
            doc.Metadata["DTL_BIZ_CD"].Value = "NDR"; // 업무상세코드 참고업무의 상세 업무구분이 있는 경우(참고업부구분코드가 '04:평가'인 경우)필수기재(상세코드는 업무별로 별도 안내 받은 코드를 기재)
            doc.Metadata["HOSP_RNO"].Value = i2a.PID; // 의료기관에서 부여한 환자등록번호
            doc.Metadata["PAT_NM"].Value = i2a.PNM; // 수진자 성명
            doc.Metadata["PAT_JNO"].Value = i2a.RESID; // 수진자 주민등록번호 ("-" 생략)

            // A. 기본 정보
            doc.Elements["DGSBJT_CD"].Value = t41a.INSDPTCD; // 진료과

            // B. 입원 진료비 게산서,영수증
            doc.Elements["RECE_TY_CD"].Value = t41a.GetSunapTyCd(); // 수납유형 1.퇴원 2.중간
            doc.Elements["DIAG_STA_DD"].Value = t41a.STTDT; // 진료시작일 (신포괄질병군 진료를 위하여 그달에 최초로 입원한 날짜)
            doc.Elements["DIAG_END_DD"].Value = t41a.ENDDT; // 진료종료일 (퇴원한 날짜)
            //*****doc.Elements["NGT_DIAG_CD"].Value = ""; // 야간(공휴일)진료 1.야간(평일.18~09,토요일.13~09) 2.공휴일 3.야간및공휴일
            doc.Elements["DRG_NO"].Value = t41a.DRGNO; // 질병군(DRG) 번호
            doc.Elements["SRM_NM"].Value = t41a.WARDID; // 병실
            doc.Elements["PTNT_TY_CD"].Value = t41a.GetPtntTyCd(); // 환자구분 1.건강보험 2.의료급여1종 3.의료급여2종
            doc.Elements["RCPT_NO"].Value = t41a.RPID; // 영수증번호

            // 기본항목
            // 테이블에 컬럼추가
            doc.Tables["TBL_RCPT"].Columns.Add("RCPT_CZITM_CD"); // 항목코드
            doc.Tables["TBL_RCPT"].Columns.Add("SLF_BRDN_AMT"); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Columns.Add("HINSU_BRDN_AMT"); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Columns.Add("ALAM_SLF_BRDN_AMT"); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Columns.Add("CHIC_DIAG_AMT"); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Columns.Add("CHIC_DIAG_EXCP_AMT"); // 선택진료료 이외. 숫자만

            // ENAMT : 진찰료
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[0]["RCPT_CZITM_CD"].Value = "ENAMT"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[0]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.ENAMT, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[0]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.ENAMT, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[0]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.ENAMT, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[0]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.ENAMT, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[0]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.ENAMT, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // INAMT_BDM1 : 1인실입원료 *** 2019.01.21 추가함. ***
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[1]["RCPT_CZITM_CD"].Value = "INAMT_BDM1"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[1]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.INAMT_BDM1, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[1]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.INAMT_BDM1, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[1]["ALAM_SLF_BRDN_AMT"].Value = "0";//t41a.gumak[CDataTT41A.INAMT_BDM1, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[1]["CHIC_DIAG_AMT"].Value = "0";//t41a.gumak[CDataTT41A.INAMT_BDM1, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[1]["CHIC_DIAG_EXCP_AMT"].Value = "0";//t41a.gumak[CDataTT41A.INAMT_BDM1, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // INAMT_BDM2 : 2.3인실입원료, *** 2019.01.21 추가함. ***
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[2]["RCPT_CZITM_CD"].Value = "INAMT_BDM2"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[2]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.INAMT_BDM2, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[2]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.INAMT_BDM2, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[2]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.INAMT_BDM2, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[2]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.INAMT_BDM2, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[2]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.INAMT_BDM2, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // INAMT_BDM4 : 4인실이상입원료
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[3]["RCPT_CZITM_CD"].Value = "INAMT_BDM4"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[3]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.INAMT_BDM4, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[3]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.INAMT_BDM4, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[3]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.INAMT_BDM4, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[3]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.INAMT_BDM4, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[3]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.INAMT_BDM4, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // FOEP : 식대
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[4]["RCPT_CZITM_CD"].Value = "FOEP"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[4]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.FOEP, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[4]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.FOEP, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[4]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.FOEP, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[4]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.FOEP, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[4]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.FOEP, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // CPMD_ACTAMT : 투약 및 조제료.행위료
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[5]["RCPT_CZITM_CD"].Value = "CPMD_ACTAMT"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[5]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.CPMD_ACTAMT, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[5]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.CPMD_ACTAMT, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[5]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.CPMD_ACTAMT, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[5]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.CPMD_ACTAMT, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[5]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.CPMD_ACTAMT, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // CPMD_MDAMT : 투약 및 조제로.약품비
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[6]["RCPT_CZITM_CD"].Value = "CPMD_MDAMT"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[6]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.CPMD_MDAMT, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[6]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.CPMD_MDAMT, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[6]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.CPMD_MDAMT, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[6]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.CPMD_MDAMT, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[6]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.CPMD_MDAMT, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // IJCT_ACTAMT : 주사료.행위료
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[7]["RCPT_CZITM_CD"].Value = "IJCT_ACTAMT"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[7]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.IJCT_ACTAMT, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[7]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.IJCT_ACTAMT, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[7]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.IJCT_ACTAMT, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[7]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.IJCT_ACTAMT, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[7]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.IJCT_ACTAMT, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // IJCT_MDAMT : 주사료.약품비
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[8]["RCPT_CZITM_CD"].Value = "IJCT_MDAMT"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[8]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.IJCT_MDAMT, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[8]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.IJCT_MDAMT, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[8]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.IJCT_MDAMT, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[8]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.IJCT_MDAMT, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[8]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.IJCT_MDAMT, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // NCAMT : 마취료
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[9]["RCPT_CZITM_CD"].Value = "NCAMT"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[9]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.NCAMT, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[9]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.NCAMT, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[9]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.NCAMT, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[9]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.NCAMT, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[9]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.NCAMT, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // DPAMT : 처치 및 수술료
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[10]["RCPT_CZITM_CD"].Value = "DPAMT"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[10]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.DPAMT, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[10]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.DPAMT, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[10]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.DPAMT, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[10]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.DPAMT, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[10]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.DPAMT, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // EXAMT : 검사료
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[11]["RCPT_CZITM_CD"].Value = "EXAMT"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[11]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.EXAMT, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[11]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.EXAMT, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[11]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.EXAMT, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[11]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.EXAMT, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[11]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.EXAMT, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // IMG_DIAMT : 영상진단
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[12]["RCPT_CZITM_CD"].Value = "IMG_DIAMT"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[12]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.IMG_DIAMT, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[12]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.IMG_DIAMT, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[12]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.IMG_DIAMT, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[12]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.IMG_DIAMT, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[12]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.IMG_DIAMT, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // RADT_TRRT : 방사선치료료
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[13]["RCPT_CZITM_CD"].Value = "RADT_TRRT"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[13]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.RADT_TRRT, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[13]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.RADT_TRRT, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[13]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.RADT_TRRT, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[13]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.RADT_TRRT, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[13]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.RADT_TRRT, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // TMCAT : 치료재료대
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[14]["RCPT_CZITM_CD"].Value = "TMCAT"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[14]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.TMCAT, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[14]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.TMCAT, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[14]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.TMCAT, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[14]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.TMCAT, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[14]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.TMCAT, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // PTR : 재활 및 물리치료료
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[15]["RCPT_CZITM_CD"].Value = "PTR"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[15]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.PTR, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[15]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.PTR, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[15]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.PTR, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[15]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.PTR, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[15]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.PTR, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // PYAMT : 정신요법료
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[16]["RCPT_CZITM_CD"].Value = "PYAMT"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[16]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.PYAMT, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[16]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.PYAMT, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[16]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.PYAMT, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[16]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.PYAMT, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[16]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.PYAMT, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // BCF : 전혈 및 혈액성분제제료
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[17]["RCPT_CZITM_CD"].Value = "BCF"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[17]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.BCF, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[17]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.BCF, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[17]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.BCF, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[17]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.BCF, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[17]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.BCF, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // CT_DIAMT : CT진단료
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[18]["RCPT_CZITM_CD"].Value = "CT_DIAMT"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[18]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.CT_DIAMT, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[18]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.CT_DIAMT, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[18]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.CT_DIAMT, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[18]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.CT_DIAMT, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[18]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.CT_DIAMT, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // MRI_DIAMT : MRI진단료
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[19]["RCPT_CZITM_CD"].Value = "MRI_DIAMT"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[19]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.MRI_DIAMT, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[19]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.MRI_DIAMT, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[19]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.MRI_DIAMT, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[19]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.MRI_DIAMT, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[19]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.MRI_DIAMT, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // PET_DIAMT : PET진단료 *** 유비스병원에 없음 ***
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[20]["RCPT_CZITM_CD"].Value = "PET_DIAMT"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[20]["SLF_BRDN_AMT"].Value = "0";// t41a.gumak[CDataTT41A.PET_DIAMT, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[20]["HINSU_BRDN_AMT"].Value = "0";//t41a.gumak[CDataTT41A.PET_DIAMT, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[20]["ALAM_SLF_BRDN_AMT"].Value = "0";//t41a.gumak[CDataTT41A.PET_DIAMT, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[20]["CHIC_DIAG_AMT"].Value = "0";//t41a.gumak[CDataTT41A.PET_DIAMT, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[20]["CHIC_DIAG_EXCP_AMT"].Value = "0";//t41a.gumak[CDataTT41A.PET_DIAMT, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // ULTRS_DIAMT : 초음파진단료
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[21]["RCPT_CZITM_CD"].Value = "ULTRS_DIAMT"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[21]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.ULTRS_DIAMT, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[21]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.ULTRS_DIAMT, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[21]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.ULTRS_DIAMT, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[21]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.ULTRS_DIAMT, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[21]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.ULTRS_DIAMT, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // CRFE : 보철.교정료
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[22]["RCPT_CZITM_CD"].Value = "CRFE"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[22]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.CRFE, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[22]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.CRFE, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[22]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.CRFE, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[22]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.CRFE, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[22]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.CRFE, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // BF0 : 기타  *** 유비스병원에 없음 ***
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[23]["RCPT_CZITM_CD"].Value = "ETC_AMT"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[23]["SLF_BRDN_AMT"].Value = "0";//t41a.gumak[CDataTT41A.ETC_AMT, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[23]["HINSU_BRDN_AMT"].Value = "0";//t41a.gumak[CDataTT41A.ETC_AMT, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[23]["ALAM_SLF_BRDN_AMT"].Value = "0";//t41a.gumak[CDataTT41A.ETC_AMT, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[23]["CHIC_DIAG_AMT"].Value = "0";//t41a.gumak[CDataTT41A.ETC_AMT, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[23]["CHIC_DIAG_EXCP_AMT"].Value = "0";//t41a.gumak[CDataTT41A.ETC_AMT, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // YPAY_XPNS : <시행령 별표2 제4호의 요양급여>
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[24]["RCPT_CZITM_CD"].Value = "YPAY_XPNS"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[24]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.YPAY_XPNS, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[24]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.YPAY_XPNS, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[24]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.YPAY_XPNS, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[24]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.YPAY_XPNS, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[24]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.YPAY_XPNS, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // A65G : 65세이상등 정액
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[25]["RCPT_CZITM_CD"].Value = "A65G"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[25]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.A65G, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[25]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.A65G, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[25]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.A65G, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[25]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.A65G, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[25]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.A65G, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            // FAMT_MDFEE : 요양병원정액수가 *** 유비스에 없음 ***
            doc.Tables["TBL_RCPT"].AddRow();
            doc.Tables["TBL_RCPT"].Rows[26]["RCPT_CZITM_CD"].Value = "FAMT_MDFEE"; // 항목코드
            doc.Tables["TBL_RCPT"].Rows[26]["SLF_BRDN_AMT"].Value = "0";//t41a.gumak[CDataTT41A.FAMT_MDFEE, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[26]["HINSU_BRDN_AMT"].Value = "0";//t41a.gumak[CDataTT41A.FAMT_MDFEE, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
            doc.Tables["TBL_RCPT"].Rows[26]["ALAM_SLF_BRDN_AMT"].Value = "0";//t41a.gumak[CDataTT41A.FAMT_MDFEE, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
            doc.Tables["TBL_RCPT"].Rows[26]["CHIC_DIAG_AMT"].Value = "0";//t41a.gumak[CDataTT41A.FAMT_MDFEE, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
            doc.Tables["TBL_RCPT"].Rows[26]["CHIC_DIAG_EXCP_AMT"].Value = "0";//t41a.gumak[CDataTT41A.FAMT_MDFEE, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만

            if (i2a.HDATE.CompareTo("20230116") < 0)
            {
                // ICSN_MDFEE : 포괄수가진료비
                doc.Tables["TBL_RCPT"].AddRow();
                doc.Tables["TBL_RCPT"].Rows[27]["RCPT_CZITM_CD"].Value = "ICSN_MDFEE"; // 항목코드
                doc.Tables["TBL_RCPT"].Rows[27]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.ICSN_MDFEE, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
                doc.Tables["TBL_RCPT"].Rows[27]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.ICSN_MDFEE, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
                doc.Tables["TBL_RCPT"].Rows[27]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.ICSN_MDFEE, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
                doc.Tables["TBL_RCPT"].Rows[27]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.ICSN_MDFEE, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
                doc.Tables["TBL_RCPT"].Rows[27]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.ICSN_MDFEE, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만
            }
            else
            {
                // FAMT_PLCR_MDFEE : 완화의료정액수가 *** 유비스에 없음 ***
                doc.Tables["TBL_RCPT"].AddRow();
                doc.Tables["TBL_RCPT"].Rows[27]["RCPT_CZITM_CD"].Value = "FAMT_PLCR_MDFEE"; // 항목코드
                doc.Tables["TBL_RCPT"].Rows[27]["SLF_BRDN_AMT"].Value = "0";// 본인일부부담금. 숫자만
                doc.Tables["TBL_RCPT"].Rows[27]["HINSU_BRDN_AMT"].Value = "0";// 공단부담금. 숫자만
                doc.Tables["TBL_RCPT"].Rows[27]["ALAM_SLF_BRDN_AMT"].Value = "0";// 전액본인부담. 숫자만
                doc.Tables["TBL_RCPT"].Rows[27]["CHIC_DIAG_AMT"].Value = "0";// 선택진료료. 숫자만
                doc.Tables["TBL_RCPT"].Rows[27]["CHIC_DIAG_EXCP_AMT"].Value = "0";// 선택진료료 이외. 숫자만

                // ICSN_MDFEE : 포괄수가진료비
                doc.Tables["TBL_RCPT"].AddRow();
                doc.Tables["TBL_RCPT"].Rows[28]["RCPT_CZITM_CD"].Value = "ICSN_MDFEE"; // 항목코드
                doc.Tables["TBL_RCPT"].Rows[28]["SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.ICSN_MDFEE, CDataTT41A.PTAMT].ToString(); // 본인일부부담금. 숫자만
                doc.Tables["TBL_RCPT"].Rows[28]["HINSU_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.ICSN_MDFEE, CDataTT41A.UNAMT].ToString(); // 공단부담금. 숫자만
                doc.Tables["TBL_RCPT"].Rows[28]["ALAM_SLF_BRDN_AMT"].Value = t41a.gumak[CDataTT41A.ICSN_MDFEE, CDataTT41A.PPAMT].ToString(); // 전액본인부담. 숫자만
                doc.Tables["TBL_RCPT"].Rows[28]["CHIC_DIAG_AMT"].Value = t41a.gumak[CDataTT41A.ICSN_MDFEE, CDataTT41A.SXAMT].ToString(); // 선택진료료. 숫자만
                doc.Tables["TBL_RCPT"].Rows[28]["CHIC_DIAG_EXCP_AMT"].Value = t41a.gumak[CDataTT41A.ICSN_MDFEE, CDataTT41A.BBAMT].ToString(); // 선택진료료 이외. 숫자만
            }

            // 합계
            doc.Elements["GSUM_SLF_BRDN_AMT"].Value = t41a.GetTotPTAMT().ToString(); // 본인일부부담금
            doc.Elements["GSUM_HINSU_BRDN_AMT"].Value = t41a.GetTotUNAMT().ToString(); // 공단부담금
            doc.Elements["GSUM_ALAM_SLF_BRDN_AMT"].Value = t41a.GetTotPPAMT().ToString(); // 전액본인부담
            doc.Elements["GSUM_CHIC_DIAG_AMT"].Value = t41a.GetTotSXAMT().ToString(); // 선택진료비
            doc.Elements["GSUM_CHIC_DIAG_EXCP_AMT"].Value = t41a.GetTotBBAMT().ToString(); // 선택진료비 이외
            doc.Elements["MX_ECS_AMT"].Value = t41a.GetMaxPtAMT().ToString(); // 상한액 초과금. 숫자만 ?????
            doc.Elements["DAMT_TOT_AMT"].Value = (t41a.GetTotPTAMT()
                                                + t41a.GetTotUNAMT()
                                                + t41a.GetTotPPAMT()
                                                + t41a.GetTotSXAMT()
                                                + t41a.GetTotBBAMT()).ToString(); // 진료비총액
            doc.Elements["PTNT_BRDN_TOT_AMT"].Value = (t41a.GetTotPTAMT()
                                                     - t41a.GetMaxPtAMT()
                                                     + t41a.GetTotPPAMT()
                                                     + t41a.GetTotSXAMT()
                                                     + t41a.GetTotBBAMT()).ToString(); // 환자부담총액
            doc.Elements["PPMT_AMT"].Value = t41a.GetPrePayAmt().ToString(); // 이미 납부한 금액
            doc.Elements["PYMN_EPT_AMT"].Value = TruncateWon(t41a.GetTotPTAMT()
                                                           - t41a.GetMaxPtAMT()
                                                           + t41a.GetTotPPAMT()
                                                           + t41a.GetTotSXAMT()
                                                           + t41a.GetTotBBAMT()
                                                           - t41a.GetPrePayAmt()).ToString(); // 납부할 금액
            doc.Elements["PPMT_CARD_AMT"].Value = t41a.GetConfAmt().ToString(); // 카드
            doc.Elements["PPMT_CASH_RCPT_AMT"].Value = t41a.GetAccAmt().ToString(); // 현금영수증
            doc.Elements["PPMT_CASH_AMT"].Value = t41a.GetCashAmt().ToString(); // 현금
            doc.Elements["PPMT_GSUM_AMT"].Value = (t41a.GetConfAmt() 
                                                 + t41a.GetAccAmt() 
                                                 + t41a.GetCashAmt()).ToString(); // 납부한 금액 합계 = 카드+현금영수증+현금
            doc.Elements["PYMN_BLCE_AMT"].Value = (TruncateWon(t41a.GetTotPTAMT()
                                                             - t41a.GetMaxPtAMT()
                                                             + t41a.GetTotPPAMT()
                                                             + t41a.GetTotSXAMT()
                                                             + t41a.GetTotBBAMT()
                                                             - t41a.GetPrePayAmt())
                                                 - t41a.GetConfAmt()
                                                 - t41a.GetAccAmt()
                                                 - t41a.GetCashAmt()).ToString(); // 납부하지 않은 금액
            // 현금영수증
            doc.Elements["POSI_CFR_NO"].Value = t41a.GetIdno(); // 신분확인번호
            doc.Elements["CASH_ADMT_NO"].Value = t41a.GetAccno();; // 현금승인번호

            if (i2a.HDATE.CompareTo("20230116") < 0)
            {
                doc.Elements["PTNT_PYMN_TOT_AMT"].Value = "0"; // 환자 실지불금액
                doc.Elements["PTNT_RCVB_AMT"].Value = "0"; // 환자 미수금액
                doc.Elements["EMGY_MED_SPAMT"].Value = "0"; // 긴급의료 지원금액
                doc.Elements["INFN_CARE_SPAMT"].Value = "0"; // 무한돌봄 지원금액
                doc.Elements["HNDP_SPAMT"].Value = "0"; // 장애인 지원금액
                doc.Elements["PTNT_GRP_SPAMT"].Value = "0"; // 환우회 지원금액
                doc.Elements["HOSP_OSLF_SPAMT"].Value = "0"; // 병원자체 지원금액
                doc.Elements["ETC_SPAMT"].Value = "0"; // 기타 지원금액
                doc.Elements["RDXN_AMT"].Value = "0"; // 감면액
            }

            doc.Elements["YADM_OPT_PUSE_DTL_TXT"].Value = ""; // 요양기관 임의활용공간
            doc.Elements["CHIC_DIAG_APL_CD"].Value = "2"; // 선택진료 신청 1.유 2.무
            doc.Elements["RECU_CL_CD"].Value = p_HosInfo.GetHosType(); // 요양기관 종류 1.병원급(21) 2.종합병원(11) 3.상급종합병원(01) 4.의원.보건기관(31)
            doc.Elements["BIZRNO"].Value = p_HosInfo.GetBussNo(); // 사업자등록번호 ("-" 생략)
            doc.Elements["CPNM_NM"].Value = p_HosInfo.GetBussNm(); // 상호
            doc.Elements["TELNO"].Value = p_HosInfo.GetTelno(); // 전화번호
            doc.Elements["OFC_LOC_TXT"].Value = p_HosInfo.GetJuso(); // 사업장 소재지
            doc.Elements["RPPR_NM"].Value = p_HosInfo.GetCeoNm(); // 대표자
            doc.Elements["ISUE_DD"].Value = t41a.ENDDT; // 발급일

            doc.addDoc();
            return doc;
        }

        private long TruncateWon(long amt)
        {
            // 원단위 절사
            long sign = 1;
            long retAmt = amt;
            if (amt < 0)
            {
                sign = -1;
                retAmt *= -1;
            }
            retAmt /= 10;
            retAmt *= 10;
            retAmt *= sign;
            return retAmt;
        }

        public Document GetDocumentSample()
        {
            string strYkiho = "11111111";
            string strSuplDataFomCd = "ORM001";
            string strFomVer = "000";

            //Document 클래스 선언
            Document doc = new Document();

            //Metadata 입력
            doc.Metadata["YKIHO"].Value = strYkiho;
            doc.Metadata["SUPL_DATA_FOM_CD"].Value = strSuplDataFomCd;
            doc.Metadata["FOM_VER"].Value = strFomVer;
            doc.Metadata["DMD_NO"].Value = "2018100102";
            doc.Metadata["RCV_NO"].Value = "4000002";
            doc.Metadata["RCV_YR"].Value = "2018";
            doc.Metadata["SP_SNO"].Value = "00002";
            doc.Metadata["BILL_SNO"].Value = "1";
            doc.Metadata["HOSP_RNO"].Value = "10722812";
            doc.Metadata["PAT_NM"].Value = "신진단";
            doc.Metadata["PAT_JNO"].Value = "5509241000000";
            doc.Metadata["INSUP_TP_CD"].Value = "7";
            doc.Metadata["FOM_REF_BIZ_TP_CD"].Value = "01";

            //Elements 입력
            doc.Elements["DGSBJT_CD"].Value = "15";
            doc.Elements["PRSC_DR_NM"].Value = "오처방";
            doc.Elements["PRSC_DR_LCS_KND_CD"].Value = "1";
            doc.Elements["PRSC_DR_LCS_NO"].Value = "19651";
            doc.Elements["EXM_SPCM_NO"].Value = "18091600182";
            doc.Elements["EXM_PRSC_DT"].Value = "201809161001";
            doc.Elements["EXM_GAT_DT"].Value = "201809161001";
            doc.Elements["EXM_RCV_DT"].Value = "201809161008";
            doc.Elements["EXM_RST_DT"].Value = "201809161012";
            doc.Elements["DCT_DR_NM"].Value = "고판독";
            doc.Elements["DCT_DR_LCS_NO"].Value = "631";

            //Table 입력
            //Table Column 입력
            doc.Tables["TBL_TXT_EXM"].Columns.Add("EXM_MDFEE_CD");
            doc.Tables["TBL_TXT_EXM"].Columns.Add("EXM_CD");
            doc.Tables["TBL_TXT_EXM"].Columns.Add("EXM_NM");
            doc.Tables["TBL_TXT_EXM"].Columns.Add("EXM_RST_TXT");

            //행추가 및 데이터 입력
            doc.Tables["TBL_TXT_EXM"].Rows.AddRow();
            doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_MDFEE_CD"].Value = "D2510";
            doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_CD"].Value = "L80150";
            doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_NM"].Value = "LDH(EM)";
            doc.Tables["TBL_TXT_EXM"].Rows[0]["EXM_RST_TXT"].Value = "negative";

            //Table Column 입력
            doc.Tables["TBL_GRID_EXM"].Columns.Add("EXM_MDFEE_CD");
            doc.Tables["TBL_GRID_EXM"].Columns.Add("EXM_CD");
            doc.Tables["TBL_GRID_EXM"].Columns.Add("EXM_NM");
            doc.Tables["TBL_GRID_EXM"].Columns.Add("EXM_RST_TXT");
            doc.Tables["TBL_GRID_EXM"].Columns.Add("EXM_REF_TXT");
            doc.Tables["TBL_GRID_EXM"].Columns.Add("EXM_UNIT");
            doc.Tables["TBL_GRID_EXM"].Columns.Add("EXM_ADD_TXT");

            //행추가 및 데이터 입력
            doc.Tables["TBL_GRID_EXM"].Rows.AddRow();
            doc.Tables["TBL_GRID_EXM"].Rows[0]["EXM_MDFEE_CD"].Value = "D0002";
            doc.Tables["TBL_GRID_EXM"].Rows[0]["EXM_CD"].Value = "L80010";
            doc.Tables["TBL_GRID_EXM"].Rows[0]["EXM_NM"].Value = "Hb (EM)";
            doc.Tables["TBL_GRID_EXM"].Rows[0]["EXM_RST_TXT"].Value = "12.8";
            doc.Tables["TBL_GRID_EXM"].Rows[0]["EXM_REF_TXT"].Value = "13.0 ~ 17.3";
            doc.Tables["TBL_GRID_EXM"].Rows[0]["EXM_UNIT"].Value = "g/dL";
            doc.Tables["TBL_GRID_EXM"].Rows[0]["EXM_ADD_TXT"].Value = "L";

            //행추가 및 데이터 입력
            doc.Tables["TBL_GRID_EXM"].Rows.AddRow();
            doc.Tables["TBL_GRID_EXM"].Rows[1]["EXM_MDFEE_CD"].Value = "D0002";
            doc.Tables["TBL_GRID_EXM"].Rows[1]["EXM_CD"].Value = "L80014";
            doc.Tables["TBL_GRID_EXM"].Rows[1]["EXM_NM"].Value = "Hct(EM)";
            doc.Tables["TBL_GRID_EXM"].Rows[1]["EXM_RST_TXT"].Value = "39.1";
            doc.Tables["TBL_GRID_EXM"].Rows[1]["EXM_REF_TXT"].Value = "40.0 ~ 52.0";
            doc.Tables["TBL_GRID_EXM"].Rows[1]["EXM_UNIT"].Value = "%";
            doc.Tables["TBL_GRID_EXM"].Rows[1]["EXM_ADD_TXT"].Value = "L";

            //행추가 및 데이터 입력
            doc.Tables["TBL_GRID_EXM"].Rows.AddRow();
            doc.Tables["TBL_GRID_EXM"].Rows[2]["EXM_MDFEE_CD"].Value = "B1040";
            doc.Tables["TBL_GRID_EXM"].Rows[2]["EXM_CD"].Value = "L80058";
            doc.Tables["TBL_GRID_EXM"].Rows[2]["EXM_NM"].Value = "MCV(EM)";
            doc.Tables["TBL_GRID_EXM"].Rows[2]["EXM_RST_TXT"].Value = "96.7";
            doc.Tables["TBL_GRID_EXM"].Rows[2]["EXM_REF_TXT"].Value = "80.0 ~ 99.0";
            doc.Tables["TBL_GRID_EXM"].Rows[2]["EXM_UNIT"].Value = "fL";
            doc.Tables["TBL_GRID_EXM"].Rows[2]["EXM_ADD_TXT"].Value = "";

            //행추가 및 데이터 입력
            doc.Tables["TBL_GRID_EXM"].Rows.AddRow();
            doc.Tables["TBL_GRID_EXM"].Rows[3]["EXM_MDFEE_CD"].Value = "B1040";
            doc.Tables["TBL_GRID_EXM"].Rows[3]["EXM_CD"].Value = "L80062";
            doc.Tables["TBL_GRID_EXM"].Rows[3]["EXM_NM"].Value = "MCH(EM)";
            doc.Tables["TBL_GRID_EXM"].Rows[3]["EXM_RST_TXT"].Value = "34.6";
            doc.Tables["TBL_GRID_EXM"].Rows[3]["EXM_REF_TXT"].Value = "26.0 ~ 34.0";
            doc.Tables["TBL_GRID_EXM"].Rows[3]["EXM_UNIT"].Value = "pg";
            doc.Tables["TBL_GRID_EXM"].Rows[3]["EXM_ADD_TXT"].Value = "H";

            //행추가 및 데이터 입력
            doc.Tables["TBL_GRID_EXM"].Rows.AddRow();
            doc.Tables["TBL_GRID_EXM"].Rows[4]["EXM_MDFEE_CD"].Value = "B1040";
            doc.Tables["TBL_GRID_EXM"].Rows[4]["EXM_CD"].Value = "L80066";
            doc.Tables["TBL_GRID_EXM"].Rows[4]["EXM_NM"].Value = "MCHC(EM)";
            doc.Tables["TBL_GRID_EXM"].Rows[4]["EXM_RST_TXT"].Value = "37.1";
            doc.Tables["TBL_GRID_EXM"].Rows[4]["EXM_REF_TXT"].Value = "31.8 ~ 37.0";
            doc.Tables["TBL_GRID_EXM"].Rows[4]["EXM_UNIT"].Value = "g/dL";
            doc.Tables["TBL_GRID_EXM"].Rows[4]["EXM_ADD_TXT"].Value = "H";

            //행추가 및 데이터 입력
            doc.Tables["TBL_GRID_EXM"].Rows.AddRow();
            doc.Tables["TBL_GRID_EXM"].Rows[5]["EXM_MDFEE_CD"].Value = "D0002";
            doc.Tables["TBL_GRID_EXM"].Rows[5]["EXM_CD"].Value = "L80018";
            doc.Tables["TBL_GRID_EXM"].Rows[5]["EXM_NM"].Value = "RDW(EM)";
            doc.Tables["TBL_GRID_EXM"].Rows[5]["EXM_RST_TXT"].Value = "13.1";
            doc.Tables["TBL_GRID_EXM"].Rows[5]["EXM_REF_TXT"].Value = "11.5 ~ 15.5";
            doc.Tables["TBL_GRID_EXM"].Rows[5]["EXM_UNIT"].Value = "%";
            doc.Tables["TBL_GRID_EXM"].Rows[5]["EXM_ADD_TXT"].Value = "";

            //행추가 및 데이터 입력
            doc.Tables["TBL_GRID_EXM"].Rows.AddRow();
            doc.Tables["TBL_GRID_EXM"].Rows[6]["EXM_MDFEE_CD"].Value = "D0002";
            doc.Tables["TBL_GRID_EXM"].Rows[6]["EXM_CD"].Value = "L80026";
            doc.Tables["TBL_GRID_EXM"].Rows[6]["EXM_NM"].Value = "PLT(EM)";
            doc.Tables["TBL_GRID_EXM"].Rows[6]["EXM_RST_TXT"].Value = "182";
            doc.Tables["TBL_GRID_EXM"].Rows[6]["EXM_REF_TXT"].Value = "130 ~ 400";
            doc.Tables["TBL_GRID_EXM"].Rows[6]["EXM_UNIT"].Value = "K/uL";
            doc.Tables["TBL_GRID_EXM"].Rows[6]["EXM_ADD_TXT"].Value = "";

            //행추가 및 데이터 입력
            doc.Tables["TBL_GRID_EXM"].Rows.AddRow();
            doc.Tables["TBL_GRID_EXM"].Rows[7]["EXM_MDFEE_CD"].Value = "D0002";
            doc.Tables["TBL_GRID_EXM"].Rows[7]["EXM_CD"].Value = "L80022";
            doc.Tables["TBL_GRID_EXM"].Rows[7]["EXM_NM"].Value = "PDW(EM)";
            doc.Tables["TBL_GRID_EXM"].Rows[7]["EXM_RST_TXT"].Value = "17.4";
            doc.Tables["TBL_GRID_EXM"].Rows[7]["EXM_REF_TXT"].Value = "0.00 ~ 99.0";
            doc.Tables["TBL_GRID_EXM"].Rows[7]["EXM_UNIT"].Value = "%";
            doc.Tables["TBL_GRID_EXM"].Rows[7]["EXM_ADD_TXT"].Value = "";

            //Elements 입력
            doc.Elements["RMK_TXT"].Value = "";

            //FileInfo fileInfo = new FileInfo("1Mb.txt");

            ////ApndDatas (첨부파일) 입력
            //doc.ApndDatas.Rows.AddRow();
            //doc.ApndDatas.Rows[0]["FORM_TYPE"].Value = "IEF";
            //doc.ApndDatas.Rows[0]["LOCAL_FILE_PTH"].Value = fileInfo.DirectoryName + "\\" + fileInfo.Name;
            //doc.ApndDatas.Rows[0]["ROW_STAT"].Value = "C";

            //서식 추가
            doc.addDoc();
            return doc;

        }

    }
}
