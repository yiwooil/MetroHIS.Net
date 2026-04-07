using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0602E
{
    class CData
    {
        public string EPRTNO { get; set; }
        public string PID { get; set; }
        public string PNM { get; set; }
        public string DONFG { get; set; }
        public float SAKAMT1 { get; set; }
        public float SAKAMT2 { get; set; }
        public float OBJAMT1 { get; set; }
        public float OBJAMT2 { get; set; }
        public string DOCUNO { get; set; }
        public string PRTDT { get; set; }
        public string IOFG;
        public string IOFGNM
        {
            get { return IOFG == "1" ? "외래" : "입원"; }
        }
    }
}
