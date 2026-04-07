using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0548R
{
    class CData
    {
        public string NO { get; set; }

        public string EPRTNO { get; set; }
        public string PNM { get; set; }
        public string RESID { get; set; }
        public string REFNM { get; set; }
        public string REFCD { get; set; }
        public string BGIHO { get; set; }
        public string PRKNM { get; set; }
        public double DQTY { get; set; }
        public long DDAY { get; set; }
        public string RESID_FMT
        {
            get
            {
                string ret = "";
                ret = RESID.Substring(0, 6) + "-" + RESID.Substring(6, 7);
                return ret;
            }
        }

        public void Clear()
        {
            NO = "";

            EPRTNO = "";
            PNM = "";
            RESID = "";
            REFNM = "";
            REFCD = "";
            BGIHO = "";
            PRKNM = "";
            DQTY = 0;
            DDAY = 0;
        }
    }
}
