using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RNH001_Vtsg
    {
        public string CHKDT; // 측정일자
        public string CHKTM; // 측정시간
        public string Vtm; // 혈압
        public string Vpressure; // 맥박
        public string Vpulsation; // 혈류속도
        public string Vvein; // 동맥압
        public string VSPEED; // 정맥압

        //---

        public string MASR_DT // 측정일시
        {
            get { return CHKDT + CHKTM; }
        }
        public string BPRSU // 혈압
        {
            get { return Vtm; }
        }
        public string PULS // 맥박
        {
            get { return Vpressure; }
        }
        public string BRT // 호흡
        {
            get { return ""; }
        }
        public string TMPR // 체온
        {
            get { return ""; }
        }
        public string BLFL_RT // 혈류속도
        {
            get { return Vpulsation; }
        }
        public string ARTR_PRES // 동맥압
        {
            get { return Vvein; }
        }
        public string VIN_PRES // 정맥압
        {
            get { return VSPEED; }
        }

    }
}
