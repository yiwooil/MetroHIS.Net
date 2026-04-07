using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RNS001_Sopr
    {
        public string OCD;
        public string ONM;
        public string PRICD;
        public string ISPCD;
        // --

        public string SOPR_NM // 수술명
        {
            get { return ONM; }
        }
        public string MDFEE_CD // 수술명
        {
            get { return ISPCD; }
        }
    }
}
