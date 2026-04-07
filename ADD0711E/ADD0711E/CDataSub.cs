using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0711E
{
    class CDataSub
    {
        public string EPRTNO { get; set; }
        public string RCODE { get; set; }
        public string SEQ { get; set; }
        public string RMEMO { get; set; }
        public string RMEMO_CDNM { get; set; }
        public string REMARK
        {
            get
            {
                return RMEMO + " " + RMEMO_CDNM;
            }
        }

        public void Clear()
        {
            EPRTNO = "";
            RCODE = "";
            SEQ = "";
            RMEMO = "";
            RMEMO_CDNM = "";
        }
    }
}
