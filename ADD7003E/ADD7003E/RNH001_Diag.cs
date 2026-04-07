using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RNH001_Diag
    {
        public string DXD;
        public string DACD;
        public string ZCD10CD;

        public string DIAG_NM // 진단명
        {
            get { return DXD; }
        }
        public string SICK_SYM // 상병분류기호
        {
            get { return ZCD10CD; }
        }
    }
}
