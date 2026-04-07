using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRAR001
    {
        // TU93
        public string PT_INDT;  // 도착일자
        public string PT_INTM;  // 도착시간
        public string PT_OUTDT; // 퇴실일자
        public string PT_OUTTM; // 퇴실시간
        public string ANDRID;  // 퇴실결정의사
        public string ANDRNM;  // 퇴실결정의사성명(TA07)
        public string EMPID;    // 작성자
        public string EMPNM;    // 작성자성명(TA07, TA13)
        public string WDATE;    // 작성일자
        public string WTIME;    // 작성시간

        public string VOM_NRS_1; // 1차(오심평가유 + char(21) + 오심평가무 + char(21) + 통증평가결과)
        public string VOM_NRS_2; // 2차(오심평가유 + char(21) + 오심평가무 + char(21) + 통증평가결과)
        public string PAINCASE;  // 1차(통증평가도구) + char(21) + 2차(통증평가도구)

        public string PCA_1; // None
        public string PCA_2; // Epidural
        public string PCA_3; // IV
        public string PCA_TXT; // 평문

        public string PARSCR1_1; // 활동성 점수(도착시)(0~2)
        public string PARSCR1_2; // 호흡 점수(도착시)(0~2)
        public string PARSCR1_3; // 순환 점수(도착시)(0~2)
        public string PARSCR1_4; // 의식 점수(도착시)(0~2)
        public string PARSCR1_5; // 피부색 점수(도착시)(0~2)
        public string PARSCR1_SUM
        {
            get
            {
                long sum = 0;
                sum += MetroLib.StrHelper.ToLong(PARSCR1_1);
                sum += MetroLib.StrHelper.ToLong(PARSCR1_2);
                sum += MetroLib.StrHelper.ToLong(PARSCR1_3);
                sum += MetroLib.StrHelper.ToLong(PARSCR1_4);
                sum += MetroLib.StrHelper.ToLong(PARSCR1_5);
                return sum.ToString();
            }
        }

        public string PARSCR2_1; // 활동성 점수(퇴실시)(0~2)
        public string PARSCR2_2; // 호흡 점수(퇴실시)(0~2)
        public string PARSCR2_3; // 순환 점수(퇴실시)(0~2)
        public string PARSCR2_4; // 의식 점수(퇴실시)(0~2)
        public string PARSCR2_5; // 피부색 점수(퇴실시)(0~2)
        public string PARSCR2_SUM
        {
            get
            {
                long sum = 0;
                sum += MetroLib.StrHelper.ToLong(PARSCR2_1);
                sum += MetroLib.StrHelper.ToLong(PARSCR2_2);
                sum += MetroLib.StrHelper.ToLong(PARSCR2_3);
                sum += MetroLib.StrHelper.ToLong(PARSCR2_4);
                sum += MetroLib.StrHelper.ToLong(PARSCR2_5);
                return sum.ToString();
            }
        }

        public string PAINDT1; // 통증 평가일시1
        public string PAINDT2; // 통증 평가일시2
        public string EMSSDT1; // 오심구토 평가일시1
        public string EMSSDT2; // 오심구토 평가일시2
        public string ASM_RST_TXT1; // 오심구토 결과평가상세1
        public string ASM_RST_TXT2; // 오심구토 결과평가상세2

        public string PT_INDTM { get { return PT_INDT + PT_INTM.Replace(":", ""); } }
        public string PT_OUTDTM { get { return PT_OUTDT + PT_OUTTM.Replace(":", ""); } }
        public string WDTM { get { return WDATE + WTIME; } }

        public List<string> VTSG_DT = new List<string>(); // 활력징후 측정일자
        public List<string> VTSG_TM = new List<string>(); // 활력징후 측정시간
        public string MASR_DT(int idx)
        {
            return VTSG_DT[idx] + VTSG_TM[idx].Replace(":", "");
        }
        public List<string> SBP = new List<string>(); // 활력징후 혈압 수축기
        public List<string> DBP = new List<string>(); // 활력징후 혈압 이완기
        public string BP(int idx)
        {
            return SBP[idx] + "/" + DBP[idx];
        }
        public List<string> HR = new List<string>(); // 활력징후 맥박
        public List<string> RR = new List<string>(); // 활력징후 호흡
        public List<string> BT = new List<string>(); // 활력징후 체온
        public List<string> SPO2 = new List<string>(); // 활력징후 산소포화도
        public List<string> RMK = new List<string>(); // 활력징후 특이사항

        // 통증평가
        // 통증평가실시여부
        public string PAINCASE_YN { get { return Get_PainCaseTool(1) == "" && Get_PainCaseTool(2) == "" ? "2" : "1"; } } 

        // 통증평가(1차)
        // 평가일시
        public string PAINCASE_TOOL1 { get { return Get_PainCaseTool(1); } } // 통증평가도구
        public string PAINCASE_TOOL_DETAIL1 { get { return Get_PainCaseTool_Detail(1); } } // 도구상세
        public string PAINCASE_RESULT1 { get { return Get_PainCase_Result(1); } } // 결과

        // 통증평가(2차)
        // 평가일시
        public string PAINCASE_TOOL2 { get { return Get_PainCaseTool(2); } } // 통증평가도구
        public string PAINCASE_TOOL_DETAIL2 { get { return Get_PainCaseTool_Detail(2); } } // 도구상세
        public string PAINCASE_RESULT2 { get { return Get_PainCase_Result(2); } } // 결과

        // 오심구토평가실시여부
        public string VOM_YN { get { return Get_VomYn(); } } 

        // PCA
        public string PCA { get { return Get_PcaString(); } }

        public void Clear()
        {
            // TU93
            PT_INDT = "";  // 도착일자
            PT_INTM = "";  // 도착시간
            PT_OUTDT = ""; // 퇴실일자
            PT_OUTTM = ""; // 퇴실시간
            ANDRID = "";  // 퇴실결정의사
            ANDRNM = "";  // 퇴실결정의사성명(TA07)
            EMPID = "";    // 작성자
            EMPNM = "";    // 작성자성명(TA07, TA13)
            WDATE = "";     // 작성일자
            WTIME = "";    // 작성시간

            VTSG_DT.Clear(); // 활력징후 측정일자
            VTSG_TM.Clear(); // 활력징후 측정시간
            SBP.Clear(); // 활력징후 혈압 수축기
            DBP.Clear(); // 활력징후 혈압 이완기
            HR.Clear(); // 활력징후 맥박
            RR.Clear(); // 활력징후 호흡
            BT.Clear(); // 활력징후 체온
            SPO2.Clear(); // 활력징후 산소포화도
            RMK.Clear(); // 활력징후 특이사항

            VOM_NRS_1 = ""; // 1차(오심평가유 + char(21) + 오심평가무 + char(21) + 통증평가결과)
            VOM_NRS_2 = ""; // 2차(오심평가유 + char(21) + 오심평가무 + char(21) + 통증평가결과)
            PAINCASE = "";  // 1차(통증평가도구) + char(21) + 2차(통증평가도구)

            PCA_1 = ""; // None
            PCA_2 = ""; // Epidural
            PCA_3 = ""; // IV
            PCA_TXT = ""; // 평문

            PARSCR1_1 = ""; // 활동성 점수(도착시)(0~2)
            PARSCR1_2 = ""; // 호흡 점수(도착시)(0~2)
            PARSCR1_3 = ""; // 순환 점수(도착시)(0~2)
            PARSCR1_4 = ""; // 의식 점수(도착시)(0~2)
            PARSCR1_5 = ""; // 피부색 점수(도착시)(0~2)

            PARSCR2_1 = ""; // 활동성 점수(퇴실시)(0~2)
            PARSCR2_2 = ""; // 호흡 점수(퇴실시)(0~2)
            PARSCR2_3 = ""; // 순환 점수(퇴실시)(0~2)
            PARSCR2_4 = ""; // 의식 점수(퇴실시)(0~2)
            PARSCR2_5 = ""; // 피부색 점수(퇴실시)(0~2)

            PAINDT1 = ""; // 통증 평가일시1
            PAINDT2 = ""; // 통증 평가일시2
            EMSSDT1 = ""; // 오심구토 평가일시1
            EMSSDT2 = ""; // 오심구토 평가일시2
            ASM_RST_TXT1 = ""; // 오심구토 결과평가상세1
            ASM_RST_TXT2 = ""; // 오심구토 결과평가상세2

        }


        // 도움 함수 모음
        public string Get_PainCaseTool(int cha)
        {
            string ret = "";
            string[] tools = (PAINCASE + (char)21).Split((char)21);
            string tool = tools[cha - 1];
            if (tool == "") ret = "";
            else if (tool == "NRS") ret = "1";
            else if (tool == "VAS") ret = "2";
            else if (tool == "FPRS") ret = "3";
            else if (tool == "FLACC") ret = "4";
            else ret = "9";
            return ret;
        }
        public string Get_PainCaseTool_Detail(int cha)
        {
            string ret = "";
            string[] tools = (PAINCASE + (char)21).Split((char)21);
            string tool = tools[cha - 1];
            if (tool == "NRS") ret = "";
            else if (tool == "NRS") ret = "";
            else if (tool == "VAS") ret = "";
            else if (tool == "FPRS") ret = "";
            else if (tool == "FLACC") ret = "";
            else ret = tool;
            return ret;
        }
        public string Get_PainCase_Result(int cha)
        {
            string VOM_NRS = "";
            if (cha == 1) VOM_NRS = VOM_NRS_1;
            else VOM_NRS = VOM_NRS_2;
            string[] result = (VOM_NRS + (char)21 + (char)21).Split((char)21);
            return result[2];
        }
        public string Get_VomYn()
        {
            string ret = "";
            string[] result1 = (VOM_NRS_1 + (char)21 + (char)21).Split((char)21);
            string[] result2 = (VOM_NRS_2 + (char)21 + (char)21).Split((char)21);
            if (result1[0] == "1" || result2[0] == "1") ret = "1";
            else ret = "2";
            return ret;
        }
        public string Get_PcaString()
        {
            string ret = "";
            if (PCA_1 == "1") ret = "None";
            if (PCA_2 == "1") ret += (ret != "" ? " " : "") + "Epidural";
            if (PCA_3 == "1") ret += (ret != "" ? " " : "") + "IV";
            if (PCA_TXT != "") ret += (ret != "" ? " " : "") + PCA_TXT;                
            return ret;
        }

    }
}
