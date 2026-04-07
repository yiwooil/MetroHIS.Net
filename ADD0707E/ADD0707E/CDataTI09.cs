using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0707E
{
    class CDataTI09
    {
        public string PCODE { get; set; }
        public string ADTDT { get; set; }
        public string PCODENM { get; set; }
        public string MKCNM { get; set; }
        public string MKCNMK { get; set; }
        public string PTYPE { get; set; }
        public string PDUT { get; set; }
        public long KUMAK { get; set; }
        public string PRICD { get; set; }

        public void Clear()
        {
            PCODE = "";
            ADTDT = "";
            PCODENM = "";
            MKCNM = "";
            MKCNMK = "";
            PTYPE = "";
            PDUT = "";
            KUMAK = 0;
            PRICD = "";
        }
    }
}
