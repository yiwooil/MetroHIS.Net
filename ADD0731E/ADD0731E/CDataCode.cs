using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0731E
{
    class CDataCode
    {
        public string CODE { get; set; }
        public string CODENAME { get; set; }

        public void Clear()
        {
            CODE = "";
            CODENAME = "";
        }
    }
}
