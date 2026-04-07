using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0712E_JABO
{
    class CDataOutYak
    {
        public string EPRTNO { get; set; }
        public string OUTCNT { get; set; }
        public string LNO { get; set; }
        public string REMARK { get; set; }
        public string JJCOUNT { get; set; }
        public string MEMO { get; set; }

        public void Clear()
        {
            EPRTNO = "";
            OUTCNT = "";
            LNO = "";
            REMARK = "";
            JJCOUNT = "";
            MEMO = "";
        }
    }
}
