using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataROP001
    {
        public string EXDT;
        public string HMS;
        public string DRID;
        public string DRNM;
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;
        public string EMPID;
        public string EMPNM;
        public string SYSDT;
        public string SYSTM;
        public string PN;
        public string TXPLAN;
        public string ALLERGY_DRUGCHK;
        public string ALLERGY_DRUG;

        // TS06
        public List<string> PTYSQ = new List<string>();
        public List<string> ROFG = new List<string>();
        public List<string> DXD = new List<string>();
        public List<string> DACD = new List<string>();
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

        // TE01
        public List<string> ODT = new List<string>(); // 시행일자
        public List<string> OTM = new List<string>(); // 시행시간
        public List<string> ONM = new List<string>(); // 시술.처치 및 수술명(TA18)
        public string ODTM(int idx) { return ODT[idx] + OTM[idx].Substring(0, 4); }

        public string ALRG_YN
        {
            get
            {
                if (ALLERGY_DRUGCHK == "1") return "1";
                else if (ALLERGY_DRUGCHK == "2") return "2";
                else if (ALLERGY_DRUGCHK == "3") return "3";
                else return "";
            }

        }
        public string ALRG_TXT
        {
            get
            {
                return ALLERGY_DRUG;
            }
        }
        public string EXDTM { get { return EXDT + HMS; } }
        public string DEPT_INFO { get { return DPTCD + "(" + INSDPTCD + INSDPTCD2 + ")"; } }
        public string SYSDTM { get { return SYSDT + SYSTM; } }

        public void Clear()
        {
            EXDT = "";
            HMS = "";
            DRID = "";
            DRNM = "";
            DPTCD = "";
            INSDPTCD = "";
            INSDPTCD2 = "";
            EMPID = "";
            EMPNM = "";
            SYSDT = "";
            SYSTM = "";
            PN = "";
            TXPLAN = "";
            ALLERGY_DRUGCHK = "";
            ALLERGY_DRUG = "";

            PTYSQ.Clear();
            ROFG.Clear();
            DXD.Clear();
            DACD.Clear();

            ODT.Clear();
            OTM.Clear();
            ONM.Clear();
        }
    }
}
