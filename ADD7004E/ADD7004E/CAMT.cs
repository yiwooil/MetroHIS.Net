using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7004E
{
    class CAMT
    {
        public string PDRID;
        public string PDRNM;
        public int SEQ1 { get; set; }
        //
        public int JA_GUMAK1 { get; set; }
        public int JA_GUMAK2 { get; set; }
        public int JA_GS_GUM { get; set; }
        public int JA_TOT_GUM
        {
            get { return JA_GUMAK1 + JA_GUMAK2 + JA_GS_GUM; }
        }
        //
        public int JR_GUMAK1 { get; set; }
        public int JR_GUMAK2 { get; set; }
        public int JR_GS_GUM { get; set; }
        public int JR_TOT_GUM
        {
            get { return JR_GUMAK1 + JR_GUMAK2 + JR_GS_GUM; }
        }
        public int CHA_GUM
        {
            get { return JA_TOT_GUM - JR_TOT_GUM; }
        }

        public string JA_NM;
        public string SEQ1_NM
        {
            get
            {
                if (SEQ1 == 0) return PDRNM;

                if (SEQ1 == 11) return "진찰료";
                if (SEQ1 == 12) return "입원료";
                if (SEQ1 == 13) return "투약료";
                if (SEQ1 == 14) return "주사료";
                if (SEQ1 == 15) return "마취료";
                if (SEQ1 == 16) return "이학요법료";
                if (SEQ1 == 17) return "정신요법료";
                if (SEQ1 == 18) return "처치수술료";
                if (SEQ1 == 19) return "검사료";
                if (SEQ1 == 20) return "방사선료";
                if (SEQ1 == 22) return "" + JA_NM + "정액";
                if (SEQ1 == 25) return "100미만";
                if (SEQ1 == 31) return "100/100";
                if (SEQ1 == 32) return "비급여";

                if (SEQ1 == 99) return "합계";

                return SEQ1.ToString();
            }
        }


        public void Clear()
        {
            SEQ1 = 0;
            //
            JA_GUMAK1 = 0;
            JA_GUMAK2 = 0;
            JA_GS_GUM = 0;
            //
            JR_GUMAK1 = 0;
            JR_GUMAK2 = 0;
            JR_GS_GUM = 0;
            //
            JA_NM = "";
        }
    }
}
