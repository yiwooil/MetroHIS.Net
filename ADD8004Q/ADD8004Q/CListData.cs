using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD8004Q
{
    class CListData
    {
        public String REPYM { get; set; }
        public String REPSEQ { get; set; }
        public String FMNO;
        public String HOSID;
        public String DPTNM { get; set; }
        public String MEMO;

        public void Clear()
        {
            REPYM = "";
            REPSEQ = "";
            FMNO = "";
            HOSID = "";
            DPTNM = "";
            MEMO = "";
        }
    }
}
