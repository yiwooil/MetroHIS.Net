using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0711E_JABO
{
    class CData
    {
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
        public string ASTAR { get; set; }
        public long H010_DEMCNT { get; set; }
        public long H010_TTAMT { get; set; }
        public long H010_JBPTAMT { get; set; }
        public long H010_UNAMT { get; set; }
        public string JBUNICD { get; set; }
        //
        public string VERSION { get; set; }
        public string FMNO { get; set; }
        public string HOSID { get; set; }
        public string CNECTMM { get; set; }

        //
        public string CALLFGNM
        {
            get
            {
                string ret = CALLFG;
                if (CALLFG == "0") ret = "0.반송";
                else if (CALLFG == "1") ret = "1.청구";
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
                return JBFG == "8" ? "자보" : "";
            }
        }
        public string JRDIVNM
        {
            get
            {
                string ret = JRDIV;
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
                    else ret = "기타";
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
                string ret = ADDZ1;
                if (DEMNO.Length == 10)
                {
                    if (ADDZ1 == "0") ret = "원청구";
                    else if (ADDZ1 == "1") ret = "보완청구";
                    else if (ADDZ1 == "2") ret = "추가청구";
                    else if (ADDZ1 == "3") ret = "분리청구";
                    else if (ADDZ1 == "8") ret = "약제상한차액추가청구";
                }
                return ret;
            }
        }
        public string CNECTFGNM
        {
            get
            {
                string ret = "";
                if (CNECTFG == "2") ret = "2.진료수가청구서";
                else if (CNECTFG == "3") ret = "3.검체검사공급내역통보서";
                else if (CNECTFG == "4") ret = "4.치료재료 및 비급여약제 구입내역통보서";
                else if (CNECTFG == "5") ret = "5.PACS";
                else if (CNECTFG == "7") ret = "7.의료기관 자체 조제·제제약 내역통보서";
                else if (CNECTFG == "8") ret = "8.요양병원 장기환자";
                else if (CNECTFG == "9") ret = "9.요양병원 환자평가표";
                return ret;
            }
        }

        public void Clear()
        {
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
            ASTAR = "";
            H010_DEMCNT = 0;
            H010_TTAMT = 0;
            H010_JBPTAMT = 0;
            H010_UNAMT = 0;
            JBUNICD = "";
            VERSION = "";
            FMNO = "";
            HOSID = "";
            CNECTMM = "";
        }
    }
}
