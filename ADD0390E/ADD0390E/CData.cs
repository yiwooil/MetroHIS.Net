using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0390E
{
    class CData
    {
        public bool OP { get; set; }
        public string DEMNO { get; set; }
        public string COMDT { get; set; }
        public string CNECTNO { get; set; }
        public string EPRTNO { get; set; }
        public string SIMNO { get; set; }
        public string PID { get; set; }
        public string PNM { get; set; }
        public string QFYCD { get; set; }
        public string DPTCD { get; set; }
        public string JJRMK { get; set; }
        public string BO_CNT { get; set; }
        public string BO_YN
        {
            get
            {
                int bo_cnt;
                int.TryParse(BO_CNT, out bo_cnt);
                return (bo_cnt < 1 ? "" : "Y");
            }
        }
        public string BO_DEMNO { get; set; }
        public string CHU_DEMNO { get; set; }
        public string K1 { get; set; }
        public string K2 { get; set; }
        public string K3 { get; set; }
        public string K4 { get; set; }
        public string K5 { get; set; }
        public string K6 { get; set; }
        public string IOFG { get; set; }
    }
}
