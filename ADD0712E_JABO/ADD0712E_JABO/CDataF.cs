using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0712E_JABO
{
    class CDataF
    {
        public string PRICD { get; set; }
        public string BGIHO { get; set; }
        public string PRKNM { get; set; }
        public string DANGA { get; set; }
        public string CNTQTY { get; set; }
        public string DQTY { get; set; }
        public string DDAY { get; set; }
        public string GUMAK { get; set; }
        public string EXDT { get; set; }
        public string DATA_FG { get; set; }

        public void Clear()
        {
            PRICD = "";
            BGIHO = "";
            PRKNM = "";
            DANGA = "";
            CNTQTY = "";
            DQTY = "";
            DDAY = "";
            GUMAK = "";
            EXDT = "";
            DATA_FG = "";
        }
    }
}
