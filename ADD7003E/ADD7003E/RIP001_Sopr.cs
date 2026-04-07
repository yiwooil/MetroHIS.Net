using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RIP001_Sopr
    {
        public string ODT;
        public string OTM;
        public string OCD;
        public string ONM;
        public string PRICD;

        public string SOPR_ENFC_DT // 시행일시
        {
            get { return ODT + OTM; }
        }
        public string SOPR_NM // 시술 처치 및 수술명
        {
            get { return ONM; }
        }
    }
}
