using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class ERR001_Data
    {
        public string ODT;
        public string ONO;
        public string ACPTNO;
        public string ACPTDT;
        public string ACPTTM;
        public string PHTDT;
        public string PHTTM;
        public string OCD;
        public string OSTSCD;
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;
        public string WARDID;
        public string RMID;
        public string BEDID;
        public string RPTPTFG;
        public string ONM;
        public string EXDRNM;
        public string RPTDT;
        public string RPTTM;
        public string RPTNO;
        public string RDRID1;
        public string RDRNM1;
        public string GDRLCID1;
        public string RPTXT_RESULT_REC;
        public string RPTXT_RESULT_REC_ORG;

        public string DGSBJT_CD // 진료과
        {
            get { return INSDPTCD; }
        }
        public string IFLD_DTL_SPC_SBJT_CD // 내보세부전문과목
        {
            get { return (INSDPTCD == "01" ? INSDPTCD2 : ""); }
        }
        public string PRSC_DR_NM // 처방의사명
        {
            get { return EXDRNM; }
        }
        public string EXM_PRSC_DT // 처방일시(CCYYMMDDHHMM)
        {
            get { return ODT == "" ? "" : (ODT + "0000"); }
        }
        public string EXM_EXEC_DT // 검사일시(CCYYMMDDHHMM)
        {
            get { return (PHTDT + PHTTM) == "" ? EXM_PRSC_DT : (PHTDT + PHTTM); }
        }
        public string EXM_RST_DT // 판독일시(CCYYMMDDHHMM)
        {
            get { return RPTDT + RPTTM; }
        }
        public string DCT_DR_NM // 판독의사성명
        {
            get { return RDRNM1; }
        }
        public string DCT_DR_LCS_NO // 판독의사 면허번호
        {
            get { return GDRLCID1; }
        }
        public string EXM_MDFEE_CD // 수가코드(EDI코드)
                    {
            get { return OCD=="" ? "" : "00"; }
        }
        public string EXM_CD // 검사코드
                    {
            get { return OCD; }
        }
        public string EXM_NM // 검사명
                    {
            get { return ONM; }
        }
        public string EXM_RST_TXT // 판독결과
        {
            get { return RPTXT_RESULT_REC; }
        }
        public string EXM_RST_TXT_ORG // 판독결과
        {
            get { return RPTXT_RESULT_REC_ORG; }
        }

        // ---

        public void Clear()
        {
            ODT = "";
            ONO = "";
            ACPTNO = "";
            ACPTDT = "";
            ACPTTM = "";
            PHTDT = "";
            PHTTM = "";
            OCD = "";
            OSTSCD = "";
            DPTCD = "";
            INSDPTCD = "";
            INSDPTCD2 = "";
            WARDID = "";
            RMID = "";
            BEDID = "";
            RPTPTFG = "";
            ONM = "";
            EXDRNM = "";
            RPTDT = "";
            RPTTM = "";
            RPTNO = "";
            RDRID1 = "";
            RDRNM1 = "";
            GDRLCID1 = "";
            RPTXT_RESULT_REC = "";
            RPTXT_RESULT_REC_ORG = "";
        }

    }
}
