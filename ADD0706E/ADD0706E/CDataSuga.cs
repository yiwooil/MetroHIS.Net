using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0706E
{
    class CDataSuga
    {
        public bool SEL { get; set; }
        public string EDICODE { get; set; }
        public string EDINAME { get; set; }
        public long ADDAVR { get; set; }
        public long EDIAMT { get; set; }
        public long LKUMAK { get; set; }
        public string PRICD { get; set; }
        public string PRKNM { get; set; }
        public long IPAMT_NEW { get; set; }
        public long GPAMT_NEW { get; set; }
        public string CREDT_OLD { get; set; }
        public long IPAMT_OLD { get; set; }
        public long GPAMT_OLD { get; set; }
        public string GPFIX { get; set; }
        public string MCHVAL { get; set; }

        public void Clear()
        {
            SEL = false;
            EDICODE = "";
            EDINAME = "";
            ADDAVR = 0;
            EDIAMT = 0;
            LKUMAK = 0;
            PRICD = "";
            PRKNM = "";
            IPAMT_NEW = 0;
            GPAMT_NEW = 0;
            CREDT_OLD = "";
            IPAMT_OLD = 0;
            GPAMT_OLD = 0;
            GPFIX = "";
            MCHVAL = "";
        }
    }
}
