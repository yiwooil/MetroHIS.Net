using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD9909E
{
    class CData
    {
        public string DRID { get; set; }
        public string DRNM { get; set; }
        public string FRDATE { get; set; }
        public string TODATE { get; set; }
        public string MEMO { get; set; }
        public string NHDID { get; set; }

        public void Clear()
        {
            DRID = "";
            DRNM = "";
            FRDATE = "";
            TODATE = "";
            MEMO = "";
            NHDID = "";
        }
    }
}
