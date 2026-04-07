using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0707E
{
    class CDataHx
    {
        public string DEMNO { get; set; }
        public string BUYDT { get; set; }
        public long BUYQTY { get; set; }
        public long BUYAMT { get; set; }
        public string BUSINESSCD { get; set; }
        public string TRADENM { get; set; }

        public void Clear()
        {
            DEMNO = "";
            BUYDT = "";
            BUYQTY = 0;
            BUYAMT = 0;
            BUSINESSCD = "";
            TRADENM = "";
        }
    }
}
