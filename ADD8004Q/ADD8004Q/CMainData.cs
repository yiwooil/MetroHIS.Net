using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD8004Q
{
    class CMainData
    {
        public String REPYM;
        public String REPSEQ;
        public String REQYM { get; set; }
        public String REQSEQ { get; set; }
        public String REPDIV { get; set; }
        public String ELINENO { get; set; }
        public String MKDIV { get; set; }
        public String INFODIV { get; set; }
        public String ITEMCD { get; set; }
        public String ITEMNM { get; set; }
        public String INFOSTMT { get; set; }
        public String REQSTMT { get; set; }
        public String BUYQTY { get; set; }
        public String REQDIV { get; set; }

        public void Clear()
        {
            REPYM = "";
            REPSEQ = "";
            REQYM = "";
            REQSEQ = "";
            REPDIV = "";
            ELINENO = "";
            MKDIV = "";
            INFODIV = "";
            ITEMCD = "";
            ITEMNM = "";
            INFOSTMT = "";
            REQSTMT = "";
            BUYQTY = "";
            REQDIV = "";
        }
    }
}
