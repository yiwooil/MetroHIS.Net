using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RNE001_Stat
    {
        public string EXDT; // 측정일자
        public string EO_SCORE_1; // 4점
        public string EO_SCORE_2;
        public string EO_SCORE_3;
        public string EO_SCORE_4;
        public string VR_SCORE_1; // 5점
        public string VR_SCORE_2;
        public string VR_SCORE_3;
        public string VR_SCORE_4;
        public string VR_SCORE_5;
        public string BMR_SCORE_1; // 6점
        public string BMR_SCORE_2;
        public string BMR_SCORE_3;
        public string BMR_SCORE_4;
        public string BMR_SCORE_5;
        public string BMR_SCORE_6;


        // -----------

        public string MASR_DT // 측정일시
        {
            get { return EXDT + "0000"; }
        }
        public string MASR_TL_NM // 측정도구
        {
            get { return ""; }
        }
        public string MASR_RST_RTXT // 결과
        {
            get
            {
                int eo_score = 0;
                int vr_score = 0;
                int bmr_score = 0;

                if (EO_SCORE_1 == "1") eo_score = 4;
                else if (EO_SCORE_2 == "1") eo_score = 3;
                else if (EO_SCORE_3 == "1") eo_score = 2;
                else if (EO_SCORE_4 == "1") eo_score = 1;

                if (VR_SCORE_1 == "1") vr_score = 5;
                else if (VR_SCORE_2 == "1") vr_score = 4;
                else if (VR_SCORE_3 == "1") vr_score = 3;
                else if (VR_SCORE_4 == "1") vr_score = 2;
                else if (VR_SCORE_5 == "1") vr_score = 1;

                if (BMR_SCORE_1 == "1") bmr_score = 6;
                else if (BMR_SCORE_2 == "1") bmr_score = 5;
                else if (BMR_SCORE_3 == "1") bmr_score = 4;
                else if (BMR_SCORE_4 == "1") bmr_score = 3;
                else if (BMR_SCORE_5 == "1") bmr_score = 2;
                else if (BMR_SCORE_6 == "1") bmr_score = 1;

                return (eo_score + vr_score + bmr_score).ToString();
            }
        }
        
    }
}
