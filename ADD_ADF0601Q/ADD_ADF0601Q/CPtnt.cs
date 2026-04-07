using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD_ADF0601Q
{
    class CPtnt
    {
        public string PID { get; set; }
        public string PNM { get; set; }
        public string RESID { get; set; }
        public string MYPIN { get; set; }
        public string SAGE { get; set; }
        public string WARD { get; set; }
        public string DPTNM { get; set; }
        public string PDRNM { get; set; }
        public string BEDEDT { get; set; }
        public string HTELNO { get; set; }
        public string QFYNM { get; set; }
        public string RESID_MASK
        {
            get
            {
                return RESID.Substring(0,6) + "-" + RESID.Substring(6,1) + "******";
            }
        }

        public void Clear()
        {
            PID = "";
            PNM = "";
            RESID = "";
            MYPIN = "";
            SAGE = "";
            WARD = "";
            DPTNM = "";
            PDRNM = "";
            BEDEDT = "";
            HTELNO = "";
            QFYNM = "";
        }
    }
}
