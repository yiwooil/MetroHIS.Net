using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RNS001_Info
    {
        // TU01
        public int R_CNT;

        public string OPDT; // 수술일자
        public string PTAHR; // 입실시
        public string PTAMN; // 입실분
        public string OPODT; // 퇴실일자
        public string OPOHR; // 퇴실시
        public string OPOMN; // 퇴실분
        public string OPSDT; // 수술시작일자
        public string OPSHR; // 수술시작시
        public string OPSMN; // 수술시작분
        public string OPEDT; // 수술종료일자
        public string OPEHR; // 수술종료시
        public string OPEMN; // 수술종료분
        public string SCNR1; // 소독간호사
        public string SCNRNM1;
        public string CRNR1; // 순회간호사
        public string CRNRNM1;
        public int EMR109_3_CNT;
        
        // -----

        public string OPRM_IPAT_DT // 수술실 입실일시
        {
            get
            {
                string ptahr = PTAHR;
                string ptamn = PTAMN;
                if (OPDT != "")
                {
                    ptahr = ptahr.PadLeft(2, '0');
                    ptamn = ptamn.PadLeft(2, '0');
                }
                return OPDT + ptahr + ptamn;
            }
        }
        public string OPRM_DSCG_DT // 수술실 퇴실일시
        {
            get
            {
                string opohr = OPOHR;
                string opomn = OPOMN;
                if (opohr.Length == 1) opohr = "0" + opohr;
                if (opomn.Length == 1) opomn = "0" + opomn;
                return OPODT + opohr + opomn;
            }
        }
        public string SOPR_STA_DT // 수술 시작일시
        {
            get
            {
                string opshr = OPSHR;
                string opsmn = OPSMN;
                if (opshr.Length == 1) opshr = "0" + opshr;
                if (opsmn.Length == 1) opsmn = "0" + opsmn;
                return OPSDT + opshr + opsmn;
            }
        }
        public string SOPR_END_DT // 수술 종료일시
        {
            get
            {
                string opehr = OPEHR;
                string opemn = OPEMN;
                if (opehr.Length == 1) opehr = "0" + opehr;
                if (opemn.Length == 1) opemn = "0" + opemn;
                return OPEDT + opehr + opemn;
            }
        }
        public string DSFN_NURSE_NM // 소독 간호사
        {
            get { return SCNRNM1 == "" ? "-" : ""; }
        }
        public string CRCL_NURSE_NM // 순회 간호사
        {
            get { return CRNRNM1 == "" ? "-" : ""; }
        }
        public string WRT_DT // 작성일시
        {
            get
            {
                string opohr = OPOHR;
                string opomn = OPOMN;
                if (opohr.Length == 1) opohr = "0" + opohr;
                if (opomn.Length == 1) opomn = "0" + opomn;
                return OPODT + opohr + opomn;  // 작성일자가 별도로 없음.
            }
        }
        public string PTNT_POSI_CFR_YN // Time Out 여부
        {
            get { return EMR109_3_CNT > 0 ? "1" : "2"; }
        }
        public string PTNT_POSI_CFR_YN_NM
        {
            get { return EMR109_3_CNT > 0 ? "Yes" : "No"; }
        }

        // --
        public void Clear()
        {
            R_CNT = 0;

            OPDT = ""; // 수술일자
            PTAHR = ""; // 입실시
            PTAMN = ""; // 입실분
            OPODT = ""; // 퇴실일자
            OPOHR = ""; // 퇴실시
            OPOMN = ""; // 퇴실분
            OPSDT = ""; // 수술시작일자
            OPSHR = ""; // 수술시작시
            OPSMN = ""; // 수술시작분
            OPEDT = ""; // 수술종료일자
            OPEHR = ""; // 수술종료시
            OPEMN = ""; // 수술종료분
            SCNR1 = ""; // 소독간호사
            SCNRNM1 = "";
            CRNR1 = ""; // 순회간호사
            CRNRNM1 = "";
            EMR109_3_CNT = 0;
        }
    }
}
