using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0109E
{
    class CData
    {
        public int NO { get; set; }
        public string QFYCD { get; set; }
        public string PID { get; set; }
        public string PNM { get; set; }
        public string BEDEDT { get; set; }
        public string EXDT { get; set; }
        public string DPTCD { get; set; }
        public string DRNM { get; set; }
        public string BDIV;
        public string ERFG
        {
            get
            {
                return (BDIV == "1" ? "" : BDIV);
            }
        }
        public string PDIV { get; set; }
        public string PDIVNM { get; set; }
        public string GONSGB { get; set; }
        public string DAETC { get; set; }
        public string PRICD { get; set; }
        public string PRKNM;
        public string PRINM
        {
            get
            {
                return (DCFG == "1" ? "(D/C)" : "") + "(" + ODINC1 + ")" + PRKNM;
            }
        }
        public string CHRLT { get; set; }
        public float CALQY { get; set; }
        public int DDAY { get; set; }
        public string FINDT { get; set; }
        public string ODT { get; set; }
        public string ONO { get; set; }
        public string GRPCD { get; set; }

        public string FRFG;
        public string KYSTR;
        public string DCFG;
        public string ODINC1;

        public void Clear()
        {
            NO = 0;
            QFYCD = "";
            PID = "";
            PNM = "";
            BEDEDT = "";
            EXDT = "";
            DPTCD = "";
            DRNM = "";
            BDIV = "";
            PDIV = "";
            PDIVNM = "";
            GONSGB = "";
            DAETC = "";
            PRICD = "";
            PRKNM = "";
            CHRLT = "";
            CALQY = 0;
            DDAY = 0;
            FINDT = "";
            ODT = "";
            ONO = "";
            GRPCD = "";

            FRFG = "";
            KYSTR = "";
            DCFG = "";
            ODINC1 = "";
        }
    }
}
