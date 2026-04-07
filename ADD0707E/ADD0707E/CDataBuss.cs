using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0707E
{
    class CDataBuss
    {
        public string BUSINESSCD { get; set; }
        public string TRADENM { get; set; }

        public void Clear()
        {
            BUSINESSCD = "";
            TRADENM = "";
        }
    }
}
