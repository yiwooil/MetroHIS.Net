using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0711E_JABO
{
    class CDataSub
    {
        public string EPRTNO { get; set; }
        public string RCODE { get; set; }
        public string SEQ { get; set; }
        public string RMEMO;
        public string CDNM;
        public string BIGO
        {
            get
            {
                return RMEMO + "{" + CDNM + "}";
            }
        }

        public void Clear()
        {
            EPRTNO = "";
            RCODE = "";
            SEQ = "";
            RMEMO = "";
            CDNM = "";
        }
    }
}
