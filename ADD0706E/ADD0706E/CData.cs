using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0706E
{
    class CData
    {
        public string PRODNO { get; set; }
        public string PRODCM { get; set; }
        public string ITEMCD { get; set; }
        public string ITEMINFO { get; set; }
        public string STDSIZE { get; set; }
        public string UNIT { get; set; }
        public string LINEFG { get; set; }
        public long ADDAVR { get; set; }
        public string FSTBUYFG { get; set; }

        public void Clear()
        {
            PRODNO = "";
            PRODCM = "";
            ITEMCD = "";
            ITEMINFO = "";
            STDSIZE = "";
            UNIT = "";
            LINEFG = "";
            ADDAVR = 0;
            FSTBUYFG = "";
        }
    }
}
