using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD9908E
{
    class CData
    {
        public string UNICD { get; set; }
        public string UNINM { get; set; }
        public string JBUNICD { get; set; }
        public string JBUNINM { get; set; }

        public void Clear()
        {
            UNICD = "";
            UNINM = "";
            JBUNICD = "";
            JBUNINM = "";
        }
    }
}
