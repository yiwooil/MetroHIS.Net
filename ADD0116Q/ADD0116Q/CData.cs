using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0116Q
{
    class CData
    {
        public string KIND { get; set; }
        public string EXPCNT { get; set; }
        public string EXPDTM { get; set; }
        public string EXPRSN { get; set; }
        public string EMPNM { get; set; }
        //
        //public string EMPID;
        //public string EXPSEQ;

        public void Clear()
        {
            KIND = "";
            EXPCNT = "";
            EXPDTM = "";
            EXPRSN = "";
            EMPNM = "";
            //
            //EMPID = "";
            //EXPSEQ = "";
        }
    }
}
