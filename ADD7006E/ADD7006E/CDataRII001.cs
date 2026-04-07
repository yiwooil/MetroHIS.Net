using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRII001
    {
        public string BEDEDT;
        public string BEDEHM;
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;
        public string DRID;
        public string DRNM;
        public string BEDIPTHCD;

        public string CC; // 주호소
        public string CC_DATE; // 주호소일자
        public string PI; // 현병력
        public string ALRG; // 약물이상반응여부(YES=Y,NO=N,확인불가=U)
        public string ALRG_TXT; // 약물이상반응내용
        public string PHX; // 과거력
        public string MDS_DOS; // 약물복용여부(YES=Y,NO=N,확인불가=U)
        public string MDS_KND; // 약물종류(고혈압약제, 당뇨병용제,항암제 동시복용시= 1/2/7)
        public string MDS_KND_ETC; // 약물종류상세
        public string FHX; // 가족력
        public string ROS; // 계통문진
        public string PE; // 신체검진
        public string PRBM_LIST; // 문제목록및 평가
        public string IMP; // 추정진단
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


        public string BEDEDTM { get { return BEDEDT + CUtil.GetSubstring(BEDEHM, 0, 4); } }
        public string DEPT_INFO { get { return DPTCD + "(" + INSDPTCD + INSDPTCD2 + ")"; } }
        public string PATHCD
        {
            get
            {
                string ret = ""; // 1.외래 2.응급실 3.전원 4.기타
                if (BEDIPTHCD == "2") ret = "2"; // 응급실
                else if (BEDIPTHCD == "3") ret = "4"; // 기타-분만실
                else ret = "1";
                return ret;
            }
        }
        public string PATH_DETAIL
        {
            get
            {
                string ret = ""; // 1.외래 2.응급실 3.전원 4.기타
                if (BEDIPTHCD == "3") ret = "분만실";
                return ret;
            }
        }
        public string FHX_YN { get { return (FHX != "" ? "1" : "2"); } }
        public string SYSDTM { get { return SYSDT + CUtil.GetSubstring(SYSTM, 0, 4); } }
        public string DACD_INFO
        {
            get
            {
                string ret = "";
                for(int i = 0 ; i< DXD.Count; i++)
                {
                    ret += ROFG_12(i) + " " + DXD[i] + Environment.NewLine;
                }
                return ret;
            }
        }
        public string ALRG_YN
        {
            get
            {
                if (ALRG == "Y") return "1";
                else if (ALRG == "N") return "2";
                else return "3";
            }
        }
        public string MDS_DOS_YN
        {
            get
            {
                if (MDS_DOS == "Y") return "1";
                else if (MDS_DOS == "N") return "2";
                else return "3";
            }
        }

        public void Clear()
        {
            BEDEDT = "";
            BEDEHM = "";
            DPTCD = "";
            INSDPTCD = "";
            INSDPTCD2 = "";
            DRID = "";
            DRNM = "";
            BEDIPTHCD = "";

            CC = ""; // 주호소
            CC_DATE = ""; // 주호소일자
            PI = ""; // 현병력
            ALRG = ""; // 약물이상반응여부(YES=Y,NO=N,확인불가=U)
            ALRG_TXT = ""; // 약물이상반응내용
            PHX = ""; // 과거력
            MDS_DOS = ""; // 약물복용여부(YES=Y,NO=N,확인불가=U)
            MDS_KND = ""; // 약물종류(고혈압약제, 당뇨병용제,항암제 동시복용시= 1/2/7)
            MDS_KND_ETC = ""; // 약물종류상세
            FHX = ""; // 가족력
            ROS = ""; // 계통문진
            PE = ""; // 신체검진
            PRBM_LIST = ""; // 문제목록및 평가
            IMP = ""; // 추정진단
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
