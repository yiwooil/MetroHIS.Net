using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0704E
{
    class CDataBuss
    {
        public string BUSSCD { get; set; }
        public string BUSSNM { get; set; }

        public void Clear()
        {
            BUSSCD = "";
            BUSSNM = "";
        }
    }
}
