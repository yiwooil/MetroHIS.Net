using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RIP001_Elaps
    {
        public string EXDT;
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;
        public string USERID;
        public string USERNM;
        public string SYSDT;
        public string SYSTM;
        public string E12C_SOA;
        public string E12C_P;
        public string E12C_SOAP;

        public string DIAG_DD // 진료일자
        {
            get { return EXDT; }
        }
        public string DGSBJT_CD // 진료과
        {
            get { return INSDPTCD; }
        }
        public string IFLD_DTL_SPC_SBJT_CD // 내과세부
        {
            get { return INSDPTCD == "01" ? INSDPTCD2 : ""; }
        }
        public string CHRG_DR_NM // 담당의 성명
        {
            get { return USERNM; }
        }
        public string WRTP_NM // 작성자
        {
            get { return USERNM; }
        }
        public string WRT_DT // 작성일시
        {
            get { return (SYSDT + SYSTM) == "" ? "" : (SYSDT + SYSTM).Substring(0, 12); }
        }
        public string PRBM_LIST_TXT // 문제목록 및 평가
        {
            get { return E12C_SOA; }
        }
        public string TRET_PLAN_TXT // 치료계획
        {
            get
            {
                string ret = "";
                if (IS_SEND_DATA == "Y")
                {
                    ret = E12C_P == "" ? "-" : E12C_P;
                }
                return ret;
            }
        }
        public string ELAPS_TEXT
        {
            get { return E12C_SOAP; }
        }
        public string IS_SEND_DATA { get; set; }

        // --

        public void Clear()
        {
            EXDT = "";
            DPTCD = "";
            INSDPTCD = "";
            INSDPTCD2 = "";
            USERID = "";
            USERNM = "";
            SYSDT = "";
            SYSTM = "";
            E12C_SOA = "";
            E12C_P = "";
            E12C_SOAP = "";
            IS_SEND_DATA = "";
        }
    }
}
