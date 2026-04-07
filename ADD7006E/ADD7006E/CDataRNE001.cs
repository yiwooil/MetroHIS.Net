using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRNE001
    {
        // TV94_ER
        public string BEDEDT; // 응급실 도착일자
        public string INTM; // 응급실 도작시간
        public string BEDEDTM { get { return BEDEDT + INTM; } }
        public string BEDODT; // 응급실 퇴실일자
        public string DCTM; // 응급실 퇴실시간
        public string BEDODTM { get { return BEDODT + DCTM; } }
        public string INRT; // 내원경로(1.직접 내원 2.전원 3.외래에서 의뢰 9.기타)
        public string VST_PTH_CD { get { return INRT == "8" ? "9" : INRT; } } // 기타인 경우 INRT에 8이 들어감.

        // TU64(BDIV=3)
        public List<string> CHKDT = new List<string>(); // 측정일자
        public List<string> CHKTM = new List<string>(); // 측정시간
        public string CHKDTM(int idx) { return CHKDT[idx] + CHKTM[idx]; }
        public List<string> BP = new List<string>(); // 혈압
        public List<string> PR = new List<string>(); // 맥박
        public List<string> RR = new List<string>(); // 호흡
        public List<string> TMP = new List<string>(); // 체온
        public List<string> SPO2 = new List<string>(); // 산소포화도
        public List<string> RMK = new List<string>(); // 특이사항

        // TV92(BDIV=3)
        public List<string> WDATE = new List<string>(); // 기록일자
        public List<string> WTIME = new List<string>(); // 기록시간
        public string WDTM(int idx) { return WDATE[idx] + WTIME[idx]; }
        public List<string> RESULT = new List<string>(); // 간호기록
        public List<string> PNURES = new List<string>(); // 작성자ID
        public List<string> PNURES_NM = new List<string>(); // 작성자명

        public void Clear()
        {
            // TV94_ER
            BEDEDT = ""; // 응급실 도착일자
            INTM = ""; // 응급실 도작시간
            BEDODT = ""; // 응급실 퇴실일자
            DCTM = ""; // 응급실 퇴실시간
            INRT = ""; // 내원경로(1.직접 내원 2.전원 3.외래에서 의뢰 9.기타)

            // TU64(BDIV=3)
            CHKDT.Clear(); // 측정일자
            CHKTM.Clear(); // 측정시간
            BP.Clear(); // 혈압
            PR.Clear(); // 맥박
            RR.Clear(); // 호흡
            TMP.Clear(); // 체온
            SPO2.Clear(); // 산소포화도
            RMK.Clear(); // 특이사항

            // TV92(BDIV=3)
            WDATE.Clear(); // 기록일자
            WTIME.Clear(); // 기록시간
            RESULT.Clear(); // 간호기록
            PNURES.Clear(); // 작성자ID
            PNURES_NM.Clear(); // 작성자명
        }
    }
}
