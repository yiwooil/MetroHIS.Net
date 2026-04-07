using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0706E
{
    class CDataSub
    {
        public string BUSINESSCD { get; set; }
        public string TRADENM { get; set; }
        public string BUYDT { get; set; }
        public double BUYQTY { get; set; }
        public long BUYAMT { get; set; }
        public long BUYTOTAMT { get; set; }
        public string TRADEMEMO { get; set; }

        public void Clear()
        {
            BUSINESSCD = "";
            TRADENM = "";
            BUYDT = "";
            BUYQTY = 0;
            BUYAMT = 0;
            BUYTOTAMT = 0;
            TRADEMEMO = "";
        }
    }
}
