using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0113E
{
    class CData
    {
        public bool OP { get; set; }
        public string REQMM
        {
            get
            {
                if (K1.Length < 6) return K1;
                else return K1.Substring(0, 6);
            }
        }
        public string SIMNO { get; set; }
        public string EPRTNO { get; set; }
        public string PID { get; set; }
        public string PNM { get; set; }
        public string QFYCD { get; set; }
        public string PDIV { get; set; }
        public string DPTCD { get; set; }
        public string DONFG { get; set; }
        public string UNICD { get; set; }
        public string STEDT { get; set; }
        public string GONSGB { get; set; }
        public string K1 { get; set; }
        public string K2 { get; set; }
        public string K3 { get; set; }
        public string K4 { get; set; }
        public string K5 { get; set; }
        public string K6 { get; set; }
        public string IOFG { get; set; }
        public string SIMFG { get; set; }
    }
}
