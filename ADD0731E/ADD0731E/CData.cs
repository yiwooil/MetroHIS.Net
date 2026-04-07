using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0731E
{
    class CData
    {
        public string CDGBNM { get; set; }
        public string JRGBNM { get; set; }
        public string PCODE { get; set; }
        public string PCODENM { get; set; }
        public int AMT { get; set; }
        public string APPLYDT { get; set; }
        public string ADDFILEFGNM { get; set; }
        public string REMARK { get; set; }

        public void Clear()
        {
            CDGBNM = "";
            JRGBNM = "";
            PCODE = "";
            PCODENM = "";
            AMT = 0;
            APPLYDT = "";
            ADDFILEFGNM = "";
            REMARK = "";
        }
    }
}
