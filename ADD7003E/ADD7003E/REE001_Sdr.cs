using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class REE001_Sdr
    {
        public string EXDT;
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;
        public string USERID;
        public string USERNM;
        public string GDRLCID;
        public string SYSDT;
        public string SYSTM;
        public string E12C_A;
        public string E12C_P;

        public string SDR_DIAG_DT // 진료일시
        {
            get { return EXDT + "0000"; }
        }
        public string SDR_DGSBJT_CD // 진료과
        {
            get { return INSDPTCD; }
        }
        public  string IFLD_DTL_SPC_SBJT_CD // 내과세부
        {
            get { return INSDPTCD=="01" ? INSDPTCD2 : ""; }
        }
        public string SDR_NM // 진료의 성명
        {
            get { return USERNM; }
        }
        public string SDR_LCS_NO // 진료의 면허번호
        {
            get { return GDRLCID; }
        }
        public string WRTP_NM // 작성자
        {
            get { return USERNM; }
        }
        public string WRT_DT // 작성일시
        {
            get { return SYSDT + SYSTM.Substring(0, 4); }
        }
        public string PRBM_LIST_TXT // 문제목록 및 평가
        {
            get { return E12C_A; }
        }
        public string TRET_PLAN_TXT // 치료계획
        {
            get { return E12C_P; }
        }

    }
}
