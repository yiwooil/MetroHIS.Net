using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRNO001
    {
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;

        public string WDATE;
        public string WTIME;
        public string RESULT;
        public string PNURSE;
        public string PNURSE_NM;

        public string WDTM { get { return WDATE + WTIME; } }
        public string DEPT_INFO { get { return DPTCD + "(" + INSDPTCD + INSDPTCD2 + ")"; } }

        public void Clear()
        {
            DPTCD = "";
            INSDPTCD = "";
            INSDPTCD2 = "";

            WDATE = "";
            WTIME = "";
            RESULT = "";
            PNURSE = "";
            PNURSE_NM = "";
        }
    }
}
