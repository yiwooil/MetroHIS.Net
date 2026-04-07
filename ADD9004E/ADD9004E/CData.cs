using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD9004E
{
    class CData
    {
        public string CODE { get; set; }
        public string DESC { get; set; }
        public string IOFG { get; set; }
        public string EMPID { get; set; }
        public string SORTNO { get; set; }
        public bool READONLY { get; set; }

        public void Clear()
        {
            CODE = "";
            DESC = "";
            IOFG = "";
            EMPID = "";
            SORTNO = "";
            READONLY = false;
        }
    }
}
