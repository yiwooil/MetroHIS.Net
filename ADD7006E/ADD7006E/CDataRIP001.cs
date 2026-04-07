using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRIP001
    {
        public string BEDEDT;
        public string BEDEHM;
        public string DRID;
        public string DRNM;
        public string ALLERGY_DRUGCHK;
        public string ALLERGY_DRUG;

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

        public List<string> ODT = new List<string>(); // 시행일자
        public List<string> OTM = new List<string>(); // 시행시간
        public List<string> ONM = new List<string>(); // 시술.처치 및 수술명(TA18)
        public string ODTM(int idx) { return ODT[idx] + OTM[idx].Substring(0, 4); }

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

        // ----------------------------------------------------------------------------

        public string BEDEDTM { get { return BEDEDT + BEDEHM; } }
        public string ALRG_YN
        {
            get
            {
                if (ALLERGY_DRUGCHK == "1") return "1";
                else if (ALLERGY_DRUGCHK == "2") return "2";
                else return "3";
            }

        }
        public string ALRG_TXT { get { return ALLERGY_DRUG; } }

        public void Clear()
        {
            BEDEDT = "";
            BEDEHM = "";
            DRID = "";
            DRNM = "";
            ALLERGY_DRUGCHK = "";
            ALLERGY_DRUG = "";
            PTYSQ.Clear();
            ROFG.Clear();
            DXD.Clear();
            ODT.Clear();
            OTM.Clear();
            ONM.Clear();

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

        }
    }
}
