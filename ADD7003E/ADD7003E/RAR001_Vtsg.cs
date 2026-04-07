using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RAR001_Vtsg
    {
        public string CHKDT; // 활력징후 측정일자
        public string CHKTM; //          측정시분
        public string BP; // 혈압
        public string PR; // 맥박
        public string RR; // 호흡
        public string TMP; // 체옴
        public string RMK; // 비고

        // --------------------------------

        public string MASR_DT // 측정일시
        {
            get { return CHKDT + CHKTM; }
        }
        public string BPRSU // 혈압
        {
            get { return BP == "/" ? "" : BP; }
        }
        public string PULS // 맥박
        {
            get { return PR; }
        }
        public string BRT // 호흡
        {
            get { return PR; }
        }
        public string TMPR // 체온
        {
            get { return TMP; }
        }
        public string OXY_STRT // 산소포화도
        {
            get { return ""; }
        }
        public string VTSG_TXT // 특이사항
        {
            get { return RMK; }
        }

    }
}
