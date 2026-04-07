using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0546R
{
    class CDataPtnt
    {
        public string PID { get; set; }
        public string PNM { get; set; }
        public string BGIHO { get; set; }
        public string PRICD { get; set; }
        public string PRKNM { get; set; }
        public double TQTY { get; set; }
        public long GUMAK { get; set; }

        public void Clear()
        {
            PID = "";
            PNM = "";
            BGIHO = "";
            PRICD = "";
            PRKNM = "";
            TQTY = 0;
            GUMAK = 0;
        }
    }
}
