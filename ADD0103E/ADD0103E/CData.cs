using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0103E
{
    class CData
    {
        public bool SEL { get; set; }
        public string PID { get; set; }
        public string PNM { get; set; }
        public string DPTCD { get; set; }
        public string DRID { get; set; }
        public string EXDT { get; set; }
        public string FINDT { get; set; }
        public string QFYCD;
        public string PDIV;
        public string QFYCDPDIV
        {
            get
            {
                return QFYCD + "-" + PDIV;
            }
        }
        public string RPID { get; set; }
        public string BIGO { get; set; }
        public string HKEY { get; set; }

        public void Clear()
        {
            SEL = false;
            PID = "";
            PNM = "";
            DPTCD = "";
            DRID = "";
            EXDT = "";
            FINDT = "";
            QFYCD = "";
            PDIV = "";
            RPID = "";
            BIGO = "";
            HKEY = "";
        }
    }
}
