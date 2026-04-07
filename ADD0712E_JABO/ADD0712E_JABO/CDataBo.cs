using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0712E_JABO
{
    class CDataBo
    {
        public bool SEL { get; set; }
        public string PID { get; set; }
        public string PNM { get; set; }
        public string CNECNO { get; set; }
        public string JJRMK { get; set; }
        public string BO_CNT { get; set; }
        public string EXDATE { get; set; }
        public string QFYCD { get; set; }
        public string JRBY { get; set; }
        public string UNISQ { get; set; }
        public string SIMCS { get; set; }
        public string SIMNO { get; set; }

        public void Clear()
        {
            SEL = false;
            PID = "";
            PNM = "";
            CNECNO = "";
            JJRMK = "";
            BO_CNT = "";
            EXDATE = "";
            QFYCD = "";
            JRBY = "";
            UNISQ = "";
            SIMCS = "";
            SIMNO = "";
        }
    }
}
