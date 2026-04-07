using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RMM001_Data
    {
        public string ODT;
        public string OCD;
        public string PRICD;
        public string ISPCD;
        public string ONM;
        public string FLDCD4; // 투여경로
        public string DQTY;
        public string DUNIT;
        public string ORDCNT;
        public string DDAY;
        public string DSTSCD;
        public string RSN; // 미실시사유
        public string DODT;
        public string DOHR;
        public string DOMN;
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;
        public string RMK; // 비고

        public string PRSC_DD // 처방일자
        {
            get { return ODT; }
        }
        public string PRSC_DIV_CD // 처방분류
        {
            get { return "1"; }
        }
        public string MDS_CD // 약품코드(EDI코드)
        {
            get { return ISPCD; }
        }
        public string MDS_NM // 약품명
        {
            get { return ONM; }
        }
        public string INJC_PTH_TXT // 투여경로
        {
            get { return FLDCD4; }
        }
        public string FQ1_MDCT_QTY // 1회투약량(소수 넷째 자리까지 기재)
        {
            get { return DQTY; }
        }
        public string MDS_UNIT_TXT // 단위
        {
            get { return DUNIT; }
        }
        public string DY1_INJC_FQ // 1일투여횟수
        {
            get { return ORDCNT; }
        }
        public string TOT_INJC_DDCNT // 총투약일수
        {
            get { return DDAY; }
        }
        public string INJC_EXEC_CD // 투여여부
        {
            get { return DSTSCD=="Y" ? "1" : "2"; }
        }
        public string INJC_EXEC_CD_NM // 투여여부
        {
            get { return DSTSCD == "Y" ? "투여" : "미실시"; }
        }
        public string NEXEC_RS_TXT // 미시행사유
        {
            get { return RSN; }
        }
        public string EXEC_DT // 시행일시(CCYYMMDDHHMM)
        {
            get { return DODT + DOHR + DOMN; }
        }
        public string DGSBJT_CD // 진료과목(처방과)
        {
            get { return INSDPTCD; }
        }
        public string IFLD_DTL_SPC_SBJT_CD // 내과세부진료과목
        {
            get { return (INSDPTCD=="01" ? INSDPTCD2 : ""); }
        }
        public string RMK_TXT // 특이사항
        {
            get { return RMK; }
        }

    }
}
