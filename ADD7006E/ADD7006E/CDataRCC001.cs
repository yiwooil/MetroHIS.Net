using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRCC001
    {
        private string m_FLDCD9;

        // TT05
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

        // 의뢰(TV01A, TE01A)
        public string ODT;
        public string OTM;
        public string EXDPTCD;
        public string EXINSDPTCD;
        public string EXINSDPTCD2;
        public string EXDRID;
        public string EXDRNM;
        public string FLDCD9 // 의뢰내용
        {
            get { return m_FLDCD9.Replace(Convert.ToString((char)21),Environment.NewLine); }
            set { m_FLDCD9 = value; }  
        }

        // 회신(TU07)
        public string REPLYDT;
        public string REPLYTM;
        public string CSTDPTCD;
        public string CSTINSDPTCD;
        public string CSTINSDPTCD2;
        public string CSTDRID;
        public string CSTDRNM;
        public string REPLY; // 회신내용

        public string ODTM { get { return ODT + OTM; } }
        public string EX_DEPT_INFO { get { return EXDPTCD + "(" + EXINSDPTCD + EXINSDPTCD2 + ")"; } }
        public string REPLYDTM { get { return REPLYDT + CUtil.GetSubstring(REPLYTM, 0, 4); } }
        public string CST_DEPT_INFO { get { return CSTDPTCD + "(" + CSTINSDPTCD + CSTINSDPTCD2 + ")"; } }
        public string DACD_INFO
        {
            get
            {
                string ret = "";
                for (int i = 0; i < PTYSQ.Count; i++)
                {
                    ret += (ret != "" ? Environment.NewLine : "") + TYPE_123(i) + " " + DACD[i].PadRight(6, ' ') + " " + DXD[i];
                }
                return ret;
            }
        }

        public void Clear()
        {
            PTYSQ.Clear();
            ROFG.Clear();
            DXD.Clear();
            DACD.Clear();

            // 의뢰(TV01A, TE01A)
            ODT = "";
            OTM = "";
            EXDPTCD = "";
            EXINSDPTCD = "";
            EXINSDPTCD2 = "";
            EXDRID = "";
            EXDRNM = "";
            FLDCD9 = ""; // 의뢰내용

            // 회신(TU07)
            REPLYDT = "";
            REPLYTM = "";
            CSTDPTCD = "";
            CSTINSDPTCD = "";
            CSTINSDPTCD2 = "";
            CSTDRID = "";
            CSTDRNM = "";
            REPLY = ""; // 회신내용
        }
    }
}
