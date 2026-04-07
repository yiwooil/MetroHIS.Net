using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0547R
{
    class CData
    {
        public string EPRTNO { get; set; }
        public string PID { get; set; }
        public string PNM { get; set; }
        public string RESID { get; set; }
        public string GENDT { get; set; }
        public string STARTDT { get; set; }
        public string ENDDT { get; set; }
        public string UNICD { get; set; }
        public string APPRNO { get; set; }
        public string UNINM { get; set; }
        public long UNAMT { get; set; }

        public void Clear()
        {
            EPRTNO = "";
            PID = "";
            PNM = "";
            RESID = "";
            GENDT = "";
            STARTDT = "";
            ENDDT = "";
            UNICD = "";
            APPRNO = "";
            UNINM = "";
            UNAMT = 0;
        }
    }
}
