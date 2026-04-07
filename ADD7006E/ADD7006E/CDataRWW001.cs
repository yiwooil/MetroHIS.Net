using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRWW001
    {
        public string BEDEDT;
        public string BEDEHM;
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;

        public string BEDEDTM { get { return BEDEDT + BEDEHM; } }
        public string DEPT_INFO { get { return DPTCD + "(" + INSDPTCD + INSDPTCD2 + ")"; } }

        public List<string> CHKDT = new List<string>();
        public List<string> CHKTM = new List<string>();
        public List<string> BP = new List<string>(); // 혈압 117/67
        public List<string> PR = new List<string>(); // 맥박
        public List<string> RR = new List<string>(); // 호흡
        public List<string> TMP = new List<string>(); // 체온
        public string CHKDTM(int idx) { return CHKDT[idx] + CHKTM[idx]; }


        public List<string> IO_CHKDT = new List<string>();
        public List<string> IO_CHKTM = new List<string>();
        public List<string> ORAL_C = new List<string>();
        public List<string> ORAL_V = new List<string>();
        public List<string> PATE_C = new List<string>();
        public List<string> PATE_V = new List<string>();
        public List<string> BLOOD_C = new List<string>();
        public List<string> BLOOD_V = new List<string>();
        public List<string> URINE = new List<string>();
        public List<string> DR_SU = new List<string>();
        public List<string> S_V_O_C = new List<string>();
        public List<string> S_V_O_V = new List<string>();
        public List<string> RMK = new List<string>();
        public List<string> EID = new List<string>();
        public List<string> UPDID = new List<string>();
        public List<string> STOOL = new List<string>();
        public List<string> VOMIT = new List<string>();
        public List<string> OTHERS = new List<string>();

        public string IO_CHKDTM(int idx) { return IO_CHKDT[idx] + IO_CHKTM[idx]; }
        public long INTAKE_TOT(int idx) { return INTAKE_VIN(idx) + INTAKE_ETC(idx); }
        public long INTAKE_VIN(int idx) { return 0;}
        public long INTAKE_ETC(int idx) { return Convert.ToInt64(MetroLib.StrHelper.ToDouble(PATE_V[idx]) + MetroLib.StrHelper.ToDouble(ORAL_V[idx]) + MetroLib.StrHelper.ToDouble(BLOOD_V[idx])); }
        public long OUTPUT_TOT(int idx) { return OUTPUT_URN(idx) + OUTPUT_ETC(idx); }
        public long OUTPUT_URN(int idx) { return Convert.ToInt64(MetroLib.StrHelper.ToDouble(URINE[idx])); }
        public long OUTPUT_ETC(int idx)
        {
            return Convert.ToInt64(MetroLib.StrHelper.ToDouble(DR_SU[idx])) +
                   Convert.ToInt64(MetroLib.StrHelper.ToDouble(S_V_O_C[idx])) +
                   Convert.ToInt64(MetroLib.StrHelper.ToDouble(STOOL[idx])) +
                   Convert.ToInt64(MetroLib.StrHelper.ToDouble(VOMIT[idx])) +
                   Convert.ToInt64(MetroLib.StrHelper.ToDouble(OTHERS[idx]));


        }

        public void Clear()
        {
            BEDEDT = "";
            BEDEHM = "";
            DPTCD = "";
            INSDPTCD = "";
            INSDPTCD2 = "";

            CHKDT.Clear();
            CHKTM.Clear();
            BP.Clear();
            PR.Clear();
            RR.Clear();
            TMP.Clear();
            
            IO_CHKDT.Clear();
            IO_CHKTM.Clear();
            ORAL_C.Clear();
            ORAL_V.Clear();
            PATE_C.Clear();
            PATE_V.Clear();
            BLOOD_C.Clear();
            BLOOD_V.Clear();
            URINE.Clear();
            DR_SU.Clear();
            S_V_O_C.Clear();
            S_V_O_V.Clear();
            RMK.Clear();
            EID.Clear();
            UPDID.Clear();
            STOOL.Clear();
            VOMIT.Clear();
            OTHERS.Clear();
        }
    }
}
