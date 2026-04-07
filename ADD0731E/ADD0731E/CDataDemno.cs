using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0731E
{
    class CDataDemno
    {
        public string DEMNO { get; set; }
        public int TOTCNT { get; set; }
        public string VERSION { get; set; }

        public void Clear()
        {
            DEMNO = "";
            TOTCNT = 0;
            VERSION = "";
        }
    }
}
