using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RII001_Diag
    {
        public string ROFG;
        public string DXD;
        public string DACD;
        public string ZCD10CD;

        public string EARLY_FDEC_DIAG_YN // 확진여부
        {
            get
            {
                return (DACD == "" ? "" : (ROFG == "1" ? "2" : "1"));
            }
        }
        public string EARLY_DIAG_NM // 진단명
        {
            get { return DXD == "" ? "-" : DXD; }
        }
        public string EARLY_DIAG_SICK_SYM // 상병분류기호
        {
            get { return ZCD10CD; }
        }
    }
}
