using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0105E
{
    class CData
    {
        public string BDODT { get; set; }
        public string SIMNO { get; set; }
        public string EPRTNO { get; set; }
        public string PID { get; set; }
        public string PNM { get; set; }
        public string QFYCD { get; set; }
        public string DPTCD { get; set; }
        public string STEDT { get; set; }
        public string DONFG { get; set; }
        public string DEMNO { get; set; }
        public string JRBY { get; set; }
        public string UNISQ { get; set; }
        public string SIMCS { get; set; }

        public void Clear()
        {
            BDODT = "";
            SIMNO = "";
            EPRTNO = "";
            PID = "";
            PNM = "";
            QFYCD = "";
            DPTCD = "";
            STEDT = "";
            DONFG = "";
            DEMNO = "";
            JRBY = "";
            UNISQ = "";
            SIMCS = "";
        }
    }
}
