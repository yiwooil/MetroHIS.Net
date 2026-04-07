using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class REE001_Diag
    {
        public string ROFG;
        public string DXD;
        public string DACD;
        public string ZCD10CD;

        public string FDEC_DIAG_YN // 확진여부
        {
            get { return ROFG == "1" ? "2" : "1"; }
        }
        public string DIAG_NM // 진단명
        {
            get { return DXD; }
        }
        public string DIAG_SICK_SYM // 상병분류기호
        {
            get { return ZCD10CD; }
        }
    }
}
