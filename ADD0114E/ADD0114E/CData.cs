using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0114E
{
    class CData
    {
        public bool SEL { get; set; }

        public string SIMNO { get; set; }
        public string PID { get; set; }
        public string PNM { get; set; }
        public string CNECNO { get; set; }
        public string JJRMK { get; set; }
        public string K1;
        public string K2;
        public string K3;
        public string K4;
        public string K5;
        public string K6;
        public long BO_CNT;
        public string BO_YN { get { return BO_CNT > 0 ? "Y" : ""; } }
        public string IOFG;

        public void Clear()
        {
            SEL = false;

            SIMNO = "";
            PID = "";
            PNM = "";
            CNECNO = "";
            JJRMK = "";
            K1 = "";
            K2 = "";
            K3 = "";
            K4 = "";
            K5 = "";
            K6 = "";
            BO_CNT = 0;
            IOFG = "";
        }
    }
}
