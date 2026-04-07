using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RNO001_Info
    {
        public int R_CNT;
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;

        public string DGSBJT_CD // 진료과
        {
            get { return INSDPTCD; }
        }
        public string IFLD_DTL_SPC_SBJT_CD // 내과세부
        {
            get { return INSDPTCD == "01" ? INSDPTCD2 : ""; }
        }

        // ---

        public void Clear()
        {
            R_CNT = 0;
            DPTCD = "";
            INSDPTCD = "";
            INSDPTCD2 = "";
        }
    }
}
