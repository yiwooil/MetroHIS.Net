using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0712E_JABO
{
    class CData
    {
        public string IOFG { get; set; }
        public string JRKWAFG { get; set; }
        public string FTDAYS { get; set; }
        public long CNT1 { get; set; }
        public long CNT2 { get; set; }
        public string REDAY { get; set; }
        public string DEMSEQ { get; set; }
        public string DEMNO { get; set; }
        public string CNECNO { get; set; }
        public string GRPNO { get; set; }
        public string JBUNICD { get; set; }
        public string JBUNINM { get; set; }
        public string MEMO { get; set; }
        public string WEEKNUM { get; set; }
        public string VERSION { get; set; }
        public string REDPT1 { get; set; }
        public string REDPT2 { get; set; }
        public string REDPNM { get; set; }
        public string RETELE { get; set; }
        public string DCOUNT { get; set; }


        public string IOFGNM
        {
            get
            {
                string ret = "";
                if (IOFG == "1") ret = "외래";
                else if (IOFG == "2") ret = "입원";
                else if (IOFG == "3") ret = "정신외"; // 정신과외래
                else if (IOFG == "4") ret = "정신입"; // 정신과입원
                else if (IOFG == "5") ret = "정신낮"; // 정신과낮병동
                else ret = "기타";
                return ret;
            }
        }
        public string JRKWAFGNM
        {
            get
            {
                string ret="";
                if (JRKWAFG == "00") ret = "의과";
                else if (JRKWAFG == "01") ret = "내과";
                else if (JRKWAFG == "02") ret = "와과";
                else if (JRKWAFG == "03") ret = "산소";
                else if (JRKWAFG == "04") ret = "안이";
                else if (JRKWAFG == "05") ret = "피비";
                else if (JRKWAFG == "06") ret = "치과";
                else ret = "XXXX";
                return ret;
            }
        }
        public string INSMM
        {
            get
            {
                return (FTDAYS.Length < 6 ? FTDAYS : FTDAYS.Substring(0, 6));
            }
        }
        public string WEEKNUMNM
        {
            get
            {
                string ret = "";
                if (WEEKNUM == "0") ret = "월";
                else if (WEEKNUM == "1") ret = WEEKNUM + "주";
                else if (WEEKNUM == "2") ret = WEEKNUM + "주";
                else if (WEEKNUM == "3") ret = WEEKNUM + "주";
                else if (WEEKNUM == "4") ret = WEEKNUM + "주";
                else if (WEEKNUM == "5") ret = WEEKNUM + "주";
                else if (WEEKNUM == "6") ret = WEEKNUM + "주";
                else ret = WEEKNUM;
                return ret;
            }
        }
        public string REDPT
        {
            get
            {
                return REDPT1 +" " + REDPT2 + " " + REDPNM + " " + RETELE;
            }
        }

        public void Clear()
        {
            IOFG = "";
            JRKWAFG = "";
            FTDAYS = "";
            CNT1 = 0;
            CNT2 = 0;
            REDAY = "";
            DEMSEQ = "";
            DEMNO = "";
            CNECNO = "";
            GRPNO = "";
            JBUNICD = "";
            JBUNINM = "";
            MEMO = "";
            WEEKNUM = "";
            VERSION = "";
            REDPT1 = "";
            REDPT2 = "";
            REDPNM = "";
            RETELE = "";
            DCOUNT = "";
        }
    }
}
