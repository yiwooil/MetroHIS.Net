using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RCC001_Diag
    {
        public string PTYSQ;
        public string ROFG;
        public string DXD;
        public string DACD;
        public string ZCD10CD;

        public string SICK_TP_CD // 상병분류구분
        {
            get
            {
                string ret = "2";
                if (PTYSQ == "1") ret = "1";
                else if (PTYSQ == "") ret = "1";
                return ret;
            }
        }
        public string DIAG_NM // 진단명
        {
            get { return DXD == "" ? "-" : DXD; }
        }
        public string SICK_SYM // 상병분류기호
        {
            get { return ZCD10CD == "" ? "-" : ZCD10CD; }
        }

        public void Clear()
        {
            PTYSQ = "";
            ROFG = "";
            DXD = "";
            DACD = "";
            ZCD10CD = "";
        }
    }
}
