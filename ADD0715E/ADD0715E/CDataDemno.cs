using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0715E
{
    class CDataDemno
    {
        public string DEMNO { get; set; }
        public string MEMO { get; set; }

        public void Clear()
        {
            DEMNO = "";
            MEMO = "";
        }
    }
}
