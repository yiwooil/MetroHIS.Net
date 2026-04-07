using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RDD001_Info
    {
        public int R_CNT;

        public string BEDEDT;
        public string BEDEHM;

        public string IPAT_OPAT_TP_CD // 진료형태
        {
            get { return "1"; }
        }
        public string IPAT_OPAT_TP_CD_NM
        {
            get { return "입원"; }
        }
        public string IPAT_DT // 입원일시
        {
            get { return BEDEDT + BEDEHM; }
        }

        public void Clear()
        {
            R_CNT = 0;
            BEDEDT = "";
            BEDEHM = "";
        }

    }
}
