using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD8002Q
{
    class CDataP
    {
        public String ACCNO { get; set; }
        public String CNTNO { get; set; }
        public String REPDT { get; set; }
        public String PAYDT { get; set; }
        public String CNTTOT { get; set; }
        public String DEMTOT { get; set; }
        public String REALPAYAMT { get; set; }
        public String MEMO { get; set; }

        public void Clear()
        {
            ACCNO = "";
            CNTNO = "";
            REPDT = "";
            PAYDT = "";
            CNTTOT = "";
            DEMTOT = "";
            REALPAYAMT = "";
            MEMO = "";
        }
    }
}
