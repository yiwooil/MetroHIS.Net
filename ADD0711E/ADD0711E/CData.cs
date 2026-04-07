using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0711E
{
    class CData
    {
        public long H0601_CNT { get; set; }
        public long H0801_CNT { get; set; }

        public string CALLFG { get; set; }
        public string CNECTDD { get; set; }
        public string JSBS { get; set; }
        public string CNECTNO { get; set; }
        public string JBFG { get; set; }
        public string JRDIV { get; set; }
        public string IOFG { get; set; }
        public string JRKWAFG { get; set; }
        public string DEMNO { get; set; }
        public string SPTID { get; set; }
        public string CNECTFG { get; set; }
        public long DEMCNT { get; set; }
        public long UNAMT { get; set; }
        public string YYMM { get; set; }
        public string ADDZ1 { get; set; }
        public long H010_DEMCNT { get; set; }
        public long H010_TTAMT { get; set; }
        public long H010_PTAMT { get; set; }
        public long H010_UNAMT { get; set; }
        public long H010_BAKDNTTAMT { get; set; }
        public long H010_BAKDNPTAMT { get; set; }
        public long H010_BAKDNUNAMT { get; set; }

        public string VERSION { get; set; }
        public string FMNO { get; set; }
        public string HOSID { get; set; }
        public string JIWONCD { get; set; }
        public string CNECTMM { get; set; }

        public string CALLFGNM
        {
            get
            {
                string ret = "";
                if (CALLFG == "0") ret = "반송";
                else if (CALLFG == "1") ret = "청구";
                else ret = CALLFGNM;
                return ret;
            }
        }

        public string JSBSNM
        {
            get
            {
                return JSBS == "7ET" ? "반송" : "접수";
            }
        }

        public string JBFGNM
        {
            get
            {
                string ret = "";
                if (JBFG == "4") ret = "보험";
                else if (JBFG == "5") ret = "보호";
                else if (JBFG == "7") ret = "보훈";
                else if (H0601_CNT > 0) ret = "재료";
                else if (H0801_CNT > 0) ret = "약품";
                return ret;
            }
        }

        public string JRDIVNM
        {
            get
            {
                string ret = "";
                if (JRDIV == "1") ret = "의과";
                else if (JRDIV == "3") ret = "정신과";
                return ret;
            }
        }

        public string IOFGNM
        {
            get
            {
                string ret = "";
                if (DEMNO.Length == 10)
                {
                    if (IOFG == "1") ret = "외래";
                    else if (IOFG == "2") ret = "입원";
                }
                return ret;
            }
        }

        public string JRKWAFGNM
        {
            get
            {
                string ret = "";
                if (JRKWAFG == "00") ret = "의과";
                else if (JRKWAFG == "01") ret = "내과";
                else if (JRKWAFG == "02") ret = "외과";
                else if (JRKWAFG == "03") ret = "산소";
                else if (JRKWAFG == "04") ret = "안이";
                else if (JRKWAFG == "05") ret = "피비";
                else if (JRKWAFG == "06") ret = "치과";
                return ret;
            }
        }

        public string ADDZ1NM
        {
            get
            {
                string ret = "";
                if (DEMNO.Length == 10)
                {
                    if (ADDZ1 == "0") ret = "원";
                    else if (ADDZ1 == "1") ret = "보완";
                    else if (ADDZ1 == "2") ret = "추가";
                    else if (ADDZ1 == "3") ret = "분리";
                }
                return ret;
            }
        }

        public long H010_BAKDNUNAMT_2
        {
            get
            {
                return H010_BAKDNUNAMT;
            }
        }

        public void Clear()
        {
            H0601_CNT = 0;
            H0801_CNT = 0;

            CALLFG = "";
            CNECTDD = "";
            JSBS = "";
            CNECTNO = "";
            JBFG = "";
            JRDIV = "";
            IOFG = "";
            JRKWAFG = "";
            DEMNO = "";
            SPTID = "";
            CNECTFG = "";
            DEMCNT = 0;
            UNAMT = 0;
            YYMM = "";
            ADDZ1 = "";
            H010_DEMCNT = 0;
            H010_TTAMT = 0;
            H010_PTAMT = 0;
            H010_UNAMT = 0;
            H010_BAKDNTTAMT = 0;
            H010_BAKDNPTAMT = 0;
            H010_BAKDNUNAMT = 0;

            VERSION = "";
            FMNO = "";
            HOSID = "";
            JIWONCD = "";
            CNECTMM = "";

        }

    }
}
