using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataROO001
    {
        public string EXDT;
        public string HMS;
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;
        public string DRID;
        public string DRNM;

        public string CC; // 주호소
        public string CC_DATE; // 주호소일자
        public string PI; // 현병력
        public string ALRG; // 약물이상반응여부(없음)
        public string ALRG_TXT; // 약물이상반응내용(없음)
        public string PHX; // 과거력
        public string MDS_DOS; // 약물복용여부(없음)
        public string MDS_KND;// 약물종류(없음)
        // 약물종류상세(없음)
        public string FHX; // 가족력
        public string ROS; // 계통문진
        public string PE; // 신체검진
        public string PRBM_LIST; // 문제목록 및 평가
        // 문제목록및평가
        // 초기진단
        public List<string> PTYSQ = new List<string>();
        public List<string> ROFG = new List<string>();
        public List<string> DXD = new List<string>();
        public string TYPE_123(int idx)
        {
            string ret = "";
            if (PTYSQ[idx] == "1") ret = "1";
            else if (ROFG[idx] == "1") ret = "3";
            else ret = "2";
            return ret;
        }
        public string ROFG_12(int idx)
        {
            return ROFG[idx] == "1" ? "2" : "1";
        }

        public string TXPLAN; // 치료계획

        public string EMPID; // 등록자ID
        public string EMPNM; // 등록자
        public string SYSDT; // 등록일자
        public string SYSTM; // 등록시간

        public string ALRG_YN {
            get
            {
                if (ALRG == "Y") return "1";
                else if (ALRG == "N") return "2";
                else if (ALRG == "U") return "3";
                else return "";
            }
         
        }
        public string MDS_DOS_YN
        {
            get
            {
                if (MDS_DOS == "Y") return "1";
                else if (MDS_DOS == "N") return "2";
                else if (MDS_DOS == "N") return "3";
                else return "";
            }

        }
        public string EXDTM { get { return EXDT + CUtil.GetSubstring(HMS, 0, 4); } }
        public string DEPT_INFO { get { return DPTCD + "(" + INSDPTCD + INSDPTCD2 + ")"; } }
        public string FHX_YN { get { return (FHX != "" ? "1" : "2"); } }
        public string SYSDTM { get { return SYSDT + CUtil.GetSubstring(SYSTM, 0, 4); } }
        public string DACD_INFO
        {
            get
            {
                string ret = "";
                for (int i = 0; i < PTYSQ.Count; i++)
                {
                    ret += ROFG_12(i) + " " + DXD[i] + Environment.NewLine;
                }
                return ret;
            }
        }

        public void Clear()
        {
            EXDT = "";
            HMS = "";
            DPTCD = "";
            INSDPTCD = "";
            INSDPTCD2 = "";
            DRID = "";
            DRNM = "";

            CC = ""; // 주호소
            CC_DATE = ""; // 주호소일자
            PI = ""; // 현병력
            ALRG = ""; // 약물이상반응여부(없음)
            ALRG_TXT = ""; // 약물이상반응내용(없음)
            PHX = ""; // 과거력
            MDS_DOS = ""; // 약물복용여부(없음)
            MDS_KND = ""; // 약물종류(없음)
            // 약물종류상세(없음)
            FHX = ""; // 가족력
            ROS = ""; // 계통문진
            PE = ""; // 신체검진
            PRBM_LIST = ""; // 문제목록 및 평가
            // 문제목록및평가
            // 초기진단
            PTYSQ.Clear();
            ROFG.Clear();
            DXD.Clear();

            TXPLAN = ""; // 치료계획

            EMPID = ""; // 등록자ID
            EMPNM = ""; // 등록자
            SYSDT = ""; // 등록일자
            SYSTM = ""; // 등록시간
        }
    }
}
