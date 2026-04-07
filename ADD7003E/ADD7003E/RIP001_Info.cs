using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RIP001_Info
    {
        public int R_CNT;

        public string BEDEDT; // 입원일자
        public string BEDEHM; // 입원시간
        public string Society2_Q3_fg; // 약물이상 반영여부(1.Yes 0.No)
        public string Society2_Q3_ETC_txt; // 약물이상 반응내용


        // -----------------------------------
        public string IPAT_DT // 최초 입원일시
        {
            get { return BEDEDT + BEDEHM; }
        }
        public string ALRG_YN // 약물이상반응여부
        {
            get { return Society2_Q3_fg=="1" ? "1" : "2"; }
        }
        public string ALRG_YN_NM
        {
            get { return Society2_Q3_fg == "1" ? "Yes" : "No"; }
        }
        public string ALRG_TXT // 약물이상반응내용
        {
            get { return Society2_Q3_ETC_txt; }
        }

        // ---

        public void Clear()
        {
            R_CNT = 0;

            BEDEDT = ""; // 입원일자
            BEDEHM = ""; // 입원시간
            Society2_Q3_fg = ""; // 약물이상 반영여부(1.Yes 0.No)
            Society2_Q3_ETC_txt = ""; // 약물이상 반응내용
        }
    }
}
