using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0714E
{
    class CDataDemseq
    {
        public string DEMSEQ { get; set; }
        public string MEMO { get; set; }

        public void Clear()
        {
            DEMSEQ = "";
            MEMO = "";
        }
    }
}
