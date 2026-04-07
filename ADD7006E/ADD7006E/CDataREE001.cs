using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataREE001
    {
        // EDIS.DBO.EMIHPTMI
        public string PTMIINDT; // 응급실 도착일자
        public string PTMIINTM; // 응급실 도착시간
        public string PTMIOTDT; // 응급실 퇴실일자
        public string PTMIOTTM; // 응급실 퇴실시간
        public string PTMIKTDT; // KTAS 중증도 분류일자
        public string PTMIKTTM; // KTAS 중증도 분류시간
        public string PTMIKTS1; // KTAS 중증도 등급
        public string PTMIINCD; // 전원 요양기관기호
        public string PTMIEMRT; // 퇴실형태

        // EMR293
        public string MJ_HOSO; // 주호소
        public string ONSET; // 발병시기
        public string PI; // 현병력
        public string ALRG; // 약물이상반응여부(Y.Yes N.No U.확인불가)
        public string ALRG_YN
        {
            get
            {
                if (ALRG == "Y") return "1";
                else if (ALRG == "N") return "2";
                else return "3";
            }
        }
        public string ALRG_TXT; // 약물이상반응내용
        public string PHX; // 과거력
        public string MDS_DOS; // 약물복용여부(Y.Yes N.No U.확인불가)
        public string MDS_DOS_YN
        {
            get
            {
                if (MDS_DOS == "Y") return "1";
                else if (MDS_DOS == "N") return "2";
                else return "3";
            }
        }
        public string MDS_KND; // 약물종류(고혈압약제, 당뇨병용제,항암제 동시복용시= 1/2/7)
        public string MDS_KND_ETC; // 약물종류기타
        public string DRNK; // 음주여부(Y.Yes N.No U.확인불가)
        public string DRNK_YN
        {
            get
            {
                if (DRNK == "Y") return "1";
                else if (DRNK == "N") return "2";
                else return "3";
            }
        }
        public string DRNK_TXT; // 음주내용
        public string SMKN; // 흡연여부(Y.Yes N.No U.확인불가)
        public string SMKN_YN
        {
            get
            {
                if (SMKN == "Y") return "1";
                else if (SMKN == "N") return "2";
                else return "3";
            }
        }
        public string SMKN_TXT; // 흡연내용
        public string FHX; // 가족력
        public string FHX_YN { get { return FHX != "" ? "1" : "2"; } } // 가족력여부
        public string ROS; // 계통문진
        public string PE; // 신체검진

        // TK71BU
        public string DEATHDT; // 사망일
        public string DEATHHHMM; // 사망시분
        public string DEATH_SICK_SYM; // 원사인 상병분류기호
        public string DEATH_DIAG_NM; // 진단명

        public string DEATHDTM { get { return DEATHDT + DEATHHHMM; } }

        // TE12C(BDIV='3' 인 경우)
        public List<string> EXDT = new List<string>();
        public List<string> DPTCD = new List<string>();
        public List<string> INSDPTCD = new List<string>();
        public List<string> INSDPTCD2 = new List<string>();
        public List<string> USERID = new List<string>();
        public List<string> USERNM = new List<string>();
        public List<string> GDRLCID = new List<string>();
        public List<string> SYSDT = new List<string>();
        public List<string> SYSTM = new List<string>();
        public List<string> ENTDT = new List<string>();
        public List<string> ENTTM = new List<string>();
        public List<string> PN = new List<string>();
        public List<string> S = new List<string>();
        public List<string> O = new List<string>();
        public List<string> A = new List<string>();
        public List<string> P = new List<string>();

        public string EXDTM(int idx) { return EXDT[idx] + "0000"; }
        public string PRBM_TXT(int idx)
        {
                string ret = "";
                ret = PN[idx];
                if (S[idx] != "")
                {
                    if (ret != "") ret += Environment.NewLine + S[idx];
                    else ret = S[idx];
                }
                if (O[idx] != "")
                {
                    if (ret != "") ret += Environment.NewLine + O[idx];
                    else ret = O[idx];
                }
                if (A[idx] != "")
                {
                    if (ret != "") ret += Environment.NewLine + A[idx];
                    else ret = A[idx];
                }
                return ret;
        }
        public string PLAN_TXT(int idx) { return P[idx]; }

        public string DEPT_INFO(int idx) { return DPTCD[idx] + "(" + INSDPTCD[idx] + INSDPTCD2[idx] + ")"; }
        public string WRTDTM(int idx) { return (ENTDT[idx] == "" ? SYSDT[idx] : ENTDT[idx]) + (ENTTM[idx] == "" ? SYSTM[idx] : ENTTM[idx]).Substring(0, 4); }

        // ---------------------------------------------------------

        // TS06(DPTCD='ER' 인경우)
        public List<string> PTYSQ = new List<string>(); // 주진단여부
        public List<string> ROFG = new List<string>(); // 확진여부
        public List<string> DXD = new List<string>(); // 진단명
        public string TYPE_123(int idx)
        {
            string ret = "";
            if (PTYSQ[idx] == "1") ret = "1";
            else if (ROFG[idx] == "1") ret = "3";
            else ret = "2";
            return ret;
        }
        public string ROFG_12(int idx) { return ROFG[idx] == "1" ? "2" : "1"; }

        // TV01
        public List<string> ODT = new List<string>(); // 시행일자
        public List<string> OTM = new List<string>(); // 시행시간
        public List<string> ONM = new List<string>(); // 시술.처치 및 수술명
        public string ODTM(int idx) { return ODT[idx] + OTM[idx]; }


        public string PTMIINDTM { get { return PTMIINDT + PTMIINTM; } }
        public string PTMIOTDTM { get { return PTMIOTDT + PTMIOTTM; } }
        public string PTMIKTDTM { get { return PTMIKTDT + PTMIKTTM; } }
        public string PTMIEMRT_NM
        {
            get
            {
                string ret = "";
                if (PTMIEMRT == "14") ret = "02"; // 자의퇴원
                else if (PTMIEMRT.StartsWith("1")) ret = "01"; // 정상퇴원
                else if (PTMIEMRT.StartsWith("3")) ret = "03"; // 본원입원
                else if (PTMIEMRT.StartsWith("2")) ret = "04"; // 타병원전원
                else if (PTMIEMRT.StartsWith("4")) ret = "06"; // 사망
                else ret = "99"; // 기타
                return ret;
            }
        }
        public string DHI_YN { get { return PTMIINCD != "" ? "1" : "2"; } }

        public void Clear()
        {
            // EDIS.DBO.EMIHPTMI
            PTMIINDT = ""; // 응급실 도착일자
            PTMIINTM = ""; // 응급실 도착시간
            PTMIOTDT = ""; // 응급실 퇴실일자
            PTMIOTTM = ""; // 응급실 퇴실시간
            PTMIKTDT = ""; // KTAS 중증도 분류일자
            PTMIKTTM = ""; // KTAS 중증도 분류시간
            PTMIKTS1 = ""; // KTAS 중증도 등급
            PTMIEMRT = ""; // 퇴실형태

            // TE12C
            MJ_HOSO = ""; // 주호소
            ONSET = ""; // 발병시기
            PI = ""; // 현병력
            ALRG = ""; // 약물이상반응여부(Y.Yes N.No U.확인불가)
            ALRG_TXT = ""; // 약물이상반응내용
            PHX = ""; // 과거력
            MDS_DOS = ""; // 약물복용여부(Y.Yes N.No U.확인불가)
            MDS_KND = ""; // 약물종류(고혈압약제, 당뇨병용제,항암제 동시복용시= 1/2/7)
            MDS_KND_ETC = ""; // 약물종류기타
            DRNK = ""; // 음주여부(Y.Yes N.No U.확인불가)
            DRNK_TXT = ""; // 음주내용
            SMKN = ""; // 흡연여부(Y.Yes N.No U.확인불가)
            SMKN_TXT = ""; // 흡연내용
            FHX = ""; // 가족력
            ROS = ""; // 계통문진
            PE = ""; // 신체검진

            // TK71BU
            DEATHDT = ""; // 사망일
            DEATHHHMM = ""; // 사망시분
            DEATH_SICK_SYM = ""; // 원사인 상병분류기호
            DEATH_DIAG_NM = ""; // 진단명

            // TE12C
            EXDT.Clear();
            DPTCD.Clear();
            INSDPTCD.Clear();
            INSDPTCD2.Clear();
            USERID.Clear();
            USERNM.Clear();
            GDRLCID.Clear();
            SYSDT.Clear();
            SYSTM.Clear();
            ENTDT.Clear();
            ENTTM.Clear();
            PN.Clear();
            S.Clear();
            O.Clear();
            A.Clear();
            P.Clear();

            // TS06
            PTYSQ.Clear();
            ROFG.Clear();
            DXD.Clear();

            // TV01
            ODT.Clear();
            OTM.Clear();
            ONM.Clear();

        }

    }

}
