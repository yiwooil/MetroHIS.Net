using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CODE902E.NET
{
    class CTI09
    {
        public string PCODE;
        public string ADTDT;
        public string GUBUN;
        public long KUMAK1;
        public long KUMAK2;
        public long KUMAK3;
        public long KUMAK6;
        public string BUNCD;
        public string EDISCORE;
        public string MAFG;
        public string PCODENM;
        public string SPEC;

        public void Clear()
        {
            PCODE = "";
            ADTDT = "";
            GUBUN = "";
            KUMAK1 = 0;
            KUMAK2 = 0;
            KUMAK3 = 0;
            KUMAK6 = 0;
            BUNCD = "";
            EDISCORE = "";
            MAFG = "";
            PCODENM = "";
            SPEC = "";
        }
    }
}
