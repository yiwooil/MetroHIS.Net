using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0712E_JABO
{
    class CDataTjcd
    {
        public string TJCD { get; set; }
        public string TJCDRMK { get; set; }

        public void Clear()
        {
            TJCD = "";
            TJCDRMK = "";
        }
    }
}
