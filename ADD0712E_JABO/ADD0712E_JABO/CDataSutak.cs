using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0712E_JABO
{
    class CDataSutak
    {
        public string EPRTNO { get; set; }
        public string LNO { get; set; }
        public string STAKID { get; set; }
        public string STAKAMT { get; set; }
        public string MEMO { get; set; }

        public void Clear()
        {
            EPRTNO = "";
            LNO = "";
            STAKID = "";
            STAKAMT = "";
            MEMO = "";
        }
    }
}
