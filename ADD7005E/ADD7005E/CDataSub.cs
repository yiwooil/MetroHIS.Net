using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7005E
{
    class CDataSub
    {
        public CDataSub(string testcd, string testvalue)
        {
            TEST_CD = testcd;
            TEST_VALUE = testvalue;
        }

        public string TEST_CD { get; set; }
        public string TEST_NM
        {
            get
            {
                string ret = "";
                switch (TEST_CD)
                {
                    case "EPRTNO":
                        ret = "명일련";
                        break;
                    case "PNM":
                        ret = "환자명";
                        break;
                    case "STEDT":
                        ret = "진료개시일";
                        break;
                    case "ENDDT":
                        ret = "진료종료일(미전송)";
                        break;
                    case "RDATE":
                        ret = "검사일자";
                        break;
                    case "Q1":
                        ret = "   Axial pain";
                        break;
                    case "Q2":
                        ret = "   Radicular pain";
                        break;
                    case "Q3":
                        ret = "   Upper extremity 좌";
                        break;
                    case "Q4":
                        ret = "   Upper extremity 우";
                        break;
                    case "Q5":
                        ret = "   Lower extremity 좌";
                        break;
                    case "Q6":
                        ret = "   Lower extremity 우";
                        break;
                    case "Q7":
                        ret = "   Upper extremity";
                        break;
                    case "Q8":
                        ret = "   Lower extremity";
                        break;
                    case "Q9":
                        ret = "   Upper extremity 좌";
                        break;
                    case "Q10":
                        ret = "   Upper extremity 우";
                        break;
                    case "Q11":
                        ret = "   Lower extremity 좌";
                        break;
                    case "Q12":
                        ret = "   Lower extremity 우";
                        break;
                    case "Q13":
                        ret = "   Babinski sign";
                        break;
                    case "Q14":
                        ret = "   Ankle clonus";
                        break;
                    case "Q15":
                        ret = "   Hoffmann sign";
                        break;
                    case "OTHERSYN":
                        ret = "   Others 여부";
                        break;
                    case "OTHERS":
                        ret = "    Others 검사명";
                        break;
                    case "Q16":
                        ret = "   Spurling sign";
                        break;
                    case "Q17":
                        ret = "   Lhermitte sign";
                        break;
                    case "Q18":
                        ret = "   Shoulder abuction sign";
                        break;
                    case "Q19":
                        ret = "   Straight Leg Raising Test";
                        break;
                    case "Q20":
                        ret = "   Femoral Nerve Stretch Test";
                        break;
                    case "Q21":
                        ret = "   Tip-toe gait";
                        break;
                    case "Q22":
                        ret = "   Calcaneal gait";
                        break;
                    case "Q23":
                        ret = "   Limping gait";
                        break;
                    case "Q24":
                        ret = "   Intermittent claudication";
                        break;
                    case "Q25":
                        ret = "   초기에 비해 악화된 방사통과 감각마비";
                        break;
                    case "Q26":
                        ret = "   초기에 없던 병적 반사";
                        break;
                    case "DIAG":
                        ret = "   진료결과 주요 이상 소견";
                        break;
                    default:
                        ret = TEST_CD;
                        break;                        
                }
                return ret;
            }
        }
        public string TEST_VALUE { get; set; }
        public string TEST_VALUE_NM
        {
            get
            {
                string ret = "";
                switch (TEST_CD)
                {
                    case "Q9":
                        if (TEST_VALUE == "0") ret = "-";
                        else if (TEST_VALUE == "1") ret = "+";
                        else if (TEST_VALUE == "2") ret = "++";
                        else if (TEST_VALUE == "3") ret = "+++";
                        else ret = "";
                        break;
                    case "Q10":
                        if (TEST_VALUE == "0") ret = "-";
                        else if (TEST_VALUE == "1") ret = "+";
                        else if (TEST_VALUE == "2") ret = "++";
                        else if (TEST_VALUE == "3") ret = "+++";
                        else ret = "";
                        break;
                    case "Q11":
                        if (TEST_VALUE == "0") ret = "-";
                        else if (TEST_VALUE == "1") ret = "+";
                        else if (TEST_VALUE == "2") ret = "++";
                        else if (TEST_VALUE == "3") ret = "+++";
                        else ret = "";
                        break;
                    case "Q12":
                        if (TEST_VALUE == "0") ret = "-";
                        else if (TEST_VALUE == "1") ret = "+";
                        else if (TEST_VALUE == "2") ret = "++";
                        else if (TEST_VALUE == "3") ret = "+++";
                        else ret = "";
                        break;
                    case "Q13":
                        if (TEST_VALUE == "1") ret = "Y";
                        else ret = "N";
                        break;
                    case "Q14":
                        if (TEST_VALUE == "1") ret = "Y";
                        else ret = "N";
                        break;
                    case "Q15":
                        if (TEST_VALUE == "1") ret = "Y";
                        else ret = "N";
                        break;
                    case "Q16":
                        if (TEST_VALUE == "1") ret = "Y";
                        else ret = "N";
                        break;
                    case "Q17":
                        if (TEST_VALUE == "1") ret = "Y";
                        else ret = "N";
                        break;
                    case "Q18":
                        if (TEST_VALUE == "1") ret = "Y";
                        else ret = "N";
                        break;
                    case "Q19":
                        if (TEST_VALUE == "1") ret = "Y";
                        else ret = "N";
                        break;
                    case "Q20":
                        if (TEST_VALUE == "1") ret = "Y";
                        else ret = "N";
                        break;
                    case "Q21":
                        if (TEST_VALUE == "1") ret = "Y";
                        else ret = "N";
                        break;
                    case "Q22":
                        if (TEST_VALUE == "1") ret = "Y";
                        else ret = "N";
                        break;
                    case "Q23":
                        if (TEST_VALUE == "1") ret = "Y";
                        else ret = "N";
                        break;
                    case "Q24":
                        if (TEST_VALUE == "1") ret = "Y";
                        else ret = "N";
                        break;
                    case "Q25":
                        if (TEST_VALUE == "1") ret = "Y";
                        else ret = "N";
                        break;
                    case "Q26":
                        if (TEST_VALUE == "1") ret = "Y";
                        else ret = "N";
                        break;
                    default:
                        ret = TEST_VALUE;
                        break;
                }
                return ret;
            }
        }

    }
}
