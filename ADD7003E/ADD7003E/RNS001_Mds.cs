using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RNS001_Mds
    {
        public string OCD;
        public string ONM;
        public string DQTY;
        public string DUNIT;
        public string PRICD;
        public string ISPCD;

        public string MDS_CD // 약품코드
        {
            get { return ISPCD; }
        }
        public string MDS_NM // 약품명
        {
            get { return ONM; }
        }
        public string TOT_INJC_QTY // 투여용량
        {
            get { return DQTY; }
        }
        public string MDS_UNIT_TXT // 단위
        {
            get { return DUNIT; }
        }
        public string INJC_MTH_TXT // 투여경로
        {
            get { return ""; }
        }
        

    }
}
