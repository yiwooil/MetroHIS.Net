using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RNE001_Info
    {
        public int R_CNT;

        // NEDIS.DBO.EMIHPTMI
        public string PTMIINDT; // 도착일자
        public string PTMIINTM; //     시간
        public string PTMIOTDT; // 퇴실일자
        public string PTMIOTTM; //     시간
        public string PTMIINRT; // 내원경로
        public string PTMIHIBP; // 수축기 혈압
        public string PTMILOBP; // 이완기 혈압
        public string PTMIPULS; // 맥박
        public string PTMIBRTH; // 호흡
        public string PTMIBDHT; // 체온
        public string PTMIVOXS; // 산소포화도

        // ---

        public string EMRRM_IPAT_DT // 응급실 도착일시
        {
            get { return PTMIINDT + PTMIINTM; }
        }
        public string EMRRM_DSCG_DT // 응급실 퇴실일시
        {
            get { return PTMIOTDT + PTMIOTTM; }
        }
        public string EMRRM_VST_PTH_CD // 내원경로
        {
            get
            {
                if (R_CNT < 1)
                {
                    return "";
                }
                else
                {
                    if (PTMIINRT == "1") return "1";
                    if (PTMIINRT == "2") return "2";
                    if (PTMIINRT == "3") return "3";
                    if (PTMIINRT == "8") return "9";
                    return "1";
                }
            }
        }
        public string EMRRM_VST_PTH_CD_NM
        {
            get
            {
                if (R_CNT < 1)
                {
                    return "";
                }
                else
                {
                    if (PTMIINRT == "1") return "직접 내원";
                    if (PTMIINRT == "2") return "전원";
                    if (PTMIINRT == "3") return "외래에서 의뢰";
                    if (PTMIINRT == "8") return "기타";
                    return "직접 내원";
                }
            }
        }
        public string VST_PTH_ETC_TXT // 내원경로 상세
        {
            get { return ""; }
        }
        public string PTNT_STAT_TXT // 내원동기 및 현상태
        {
            get { return ""; }
        }
        public string MASR_DT // 활력징후 측정일시
        {
            get { return ""; }
        }
        public string BPRSU // 혈압
        {
            get { return R_CNT < 1 ? "" : PTMIHIBP + "/" + PTMILOBP; }
        }
        public string PULS // 맥박
        {
            get { return PTMIPULS; }
        }
        public string BRT // 호흡
        {
            get { return PTMIBRTH; }
        }
        public string TMPR // 체온
        {
            get { return PTMIBDHT; }
        }
        public string OXY_STRT // 산소포화도
        {
            get
            {
                int val = 0;
                int.TryParse(PTMIVOXS, out val);
                return val == -1 || val == 0 ? "" : val.ToString();  // -1이면 측정불가임.
            }
        }

        public void Clear()
        {
            R_CNT = 0;

            PTMIINDT = ""; // 도착일자
            PTMIINTM = ""; //     시간
            PTMIOTDT = ""; // 퇴실일자
            PTMIOTTM = ""; //     시간
            PTMIINRT = ""; // 내원경로
            PTMIHIBP = ""; // 수축기 혈압
            PTMILOBP = ""; // 이완기 혈압
            PTMIPULS = ""; // 맥박
            PTMIBRTH = ""; // 호흡
            PTMIBDHT = ""; // 체온
            PTMIVOXS = ""; // 산소포화도
        }
    }
}
