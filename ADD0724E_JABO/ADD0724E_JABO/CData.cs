using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0724E_JABO
{
    class CData
    {
        public string JSDEMSEQ { get; set; }
        public string JSREDAY { get; set; }
        public long JSTOTAMT { get; set; }
        public long JSTTTAMT { get; set; }
        public long JSJBPTAMT { get; set; }
        public string CNECNO { get; set; }
        public string DEMSEQ { get; set; }
        public string DEMNO { get; set; }
        public string JBUNICD { get; set; }
        public string JSREDPT1;
        public string JSREDPNM;
        public string JSRETELE;
        public string JSREDEPT
        {
            get
            {
                return JSREDPT1 + " " + JSREDPNM + " " + JSRETELE;
            }
        }
        public string JSSAU;
        public string MEMO;
        public string JSSAU_MEMO
        {
            get
            {
                return JSSAU + " " + MEMO;
            }
        }
        public string FMGBN { get; set; }
        public string FMGBNNM
        {
            get
            {
                if (FMGBN == "N130") return "이의제기";
                if (FMGBN == "N150") return "정산심사";
                return FMGBN;
            }
        }
        public string JRFG { get; set; }
        public string VERSION { get; set; }
        public string DCOUNT { get; set; }
        public string HOSID { get; set; }
        public string HOSNM { get; set; }
        public string JSYYSEQ { get; set; }

        public void Clear()
        {
            JSDEMSEQ = "";
            JSREDAY = "";
            JSTOTAMT = 0;
            JSTTTAMT = 0;
            JSJBPTAMT = 0;
            CNECNO = "";
            DEMSEQ = "";
            DEMNO = "";
            JBUNICD = "";
            JSREDPT1 = "";
            JSREDPNM = "";
            JSRETELE = "";
            JSSAU = "";
            MEMO = "";
            FMGBN = "";
            JRFG = "";
            VERSION = "";
            DCOUNT = "";
            HOSID = "";
            HOSNM = "";
            JSYYSEQ = "";
        }
    }
}
