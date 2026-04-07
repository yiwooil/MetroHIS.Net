using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RNS001_Tmcat
    {
        public string OCD;
        public string ONM;
        public string DQTY;
        public string DUNIT;
        public string PRICD;
        public string ISPCD;

        // --

        public string TMCAT_CD // 재료코드
        {
            get { return ISPCD; }
        }
        public string TMCAT_NM // 재료명
        {
            get { return ONM; }
        }
        public string NOM // 규격
        {
            get { return ""; }
        }
        public string QTY // 수량
        {
            get { return DQTY; }
        }
        public string UNIT // 단위
        {
            get { return DUNIT; }
        }
    }
}
