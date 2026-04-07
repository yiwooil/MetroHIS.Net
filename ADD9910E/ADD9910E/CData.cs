using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD9910E
{
    class CData
    {
        public string SEQNO { get; set; }
        public string CODE { get; set; }
        public string DACD;
        public string KORNM { get; set; }
        public string ENGNM { get; set; }

        public void Clear()
        {
            SEQNO = "";
            CODE = "";
            DACD = "";
            KORNM = "";
            ENGNM = "";
        }

    }
}
