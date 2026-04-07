using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRMM001
    {
        public string ODT;
        public string OCD;
        public string ONM;
        public string PRICD;
        public string ISPCD;
        public string DQTY;
        public string ORDCNT;
        public string DUNIT;
        public string ODAYCNT;
        public string FLDCD4; // 투여경로
        public string DODT;
        public string DOHR;
        public string DOMN;
        public string EXDRID;
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;
        public string OKCD; // 3이면 퇴원약
        public string FLDCD1; // 99이면 병동원외

        public string DODTM { get { return DODT + DOHR + DOMN; } }
        public string DEPT_INFO { get { return DPTCD + "(" + INSDPTCD + INSDPTCD2 + ")"; } }
        public string DIV_FG // 처방분류
        {
            get
            {
                if (OKCD == "3") return "4";
                else if (FLDCD1 == "99") return "5";
                else return "1";
            }
        }
        public string EXEC_FG // 투여여부
        {
            get
            {
                if (OKCD == "3" || FLDCD1 == "99") return "0";
                else return "1";
            }
        }

        public void Clear()
        {
            ODT = "";
            OCD = "";
            ONM = "";
            PRICD = "";
            ISPCD = "";
            DQTY = "";
            ORDCNT = "";
            DUNIT = "";
            ODAYCNT = "";
            FLDCD4 = "";
            DODT = "";
            DOHR = "";
            DOMN = "";
            EXDRID = "";
            DPTCD = "";
            INSDPTCD = "";
            INSDPTCD2 = "";
            OKCD = "";
            FLDCD1 = "";
        }

    }
}
