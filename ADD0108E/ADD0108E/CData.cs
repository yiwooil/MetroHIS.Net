using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0108E
{
    class CData
    {
        public string QFYCD { get; set; }
        public string EXMM { get; set; }
        public string PID { get; set; }
        public string PNM { get; set; }
        public string EXDT { get; set; }
        public string FINDT { get; set; }
        public string DPTCD { get; set; }
        public string BDIV { get; set; }
        public string PDIV { get; set; }
        public string PDIVNM { get; set; }
        public string GONSGB { get; set; }
        public string DAETC { get; set; }
        public int TTAMT { get; set; }
        public int UNAMT { get; set; }
        public int ISPAM { get; set; }
        public int UISAM { get; set; }
        public string SUMYN
        {
            get
            {
                return (SUMCNT>0 ? "Y" : "");
            }
        }
        public string RETYN
        {
            get
            {
                return (PREVKEY != "" ? "Y" : "");
            }
        }
        public string BDIVFG
        {
            get
            {
                return (BDIV == "1" ? "" : BDIV);
            }
        }
        public string PREVKEY;
        public int SUMCNT;

        public void Clear()
        {
            QFYCD = "";
            EXMM = "";
            PID = "";
            PNM = "";
            EXDT = "";
            FINDT = "";
            DPTCD = "";
            BDIV = "";
            PDIV = "";
            PDIVNM = "";
            GONSGB = "";
            DAETC = "";
            TTAMT = 0;
            UNAMT = 0;
            ISPAM = 0;
            UISAM = 0;
            PREVKEY = "";
            SUMCNT = 0;
        }
    }
}
