using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataERD001
    {
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;
        public string DRID;
        public string DRNM;

        public string ORDDT;
        public string ORDTM;
        public string SPCNM;
        public string BLOODDT;
        public string BLOODTM;
        public string RCVDT;
        public string RCVTM;

        public string VFYDT;
        public string VFYTM;
        public string TESTCD;
        public string TESTNM;
        public string RSTVAL;
        public string REFERCHK;
        public string PANICCHK;
        public string DELTACHK;
        public string UNIT;
        public string REFERFR;
        public string REFERTO;
        public string SIGNFR;
        public string SIGNTO;

        public string EDICD { get { return "00"; } }
        public string SPCCD { get { return "99"; } }
        public string ORDDTM { get { return ORDDT + ORDTM; } }
        public string BLOODDTM { get { return BLOODDT + BLOODTM; } }
        public string RCVDTM { get { return RCVDT + RCVTM; } }
        public string VFYDTM { get { return VFYDT + VFYTM; } }

        public string REFERENCE
        {
            get
            {
                string ret = "";
                if (REFERFR != "" && REFERTO != "")
                {
                    ret = REFERFR + " - " + REFERTO;
                }
                else if (REFERFR != "" && REFERTO == "")
                {
                    ret = REFERFR + SIGNFR;
                }
                else if (REFERFR == "" && REFERTO != "")
                {
                    ret = SIGNTO + REFERTO;
                }
                return ret;
            }
        }
        public string DEPT_INFO { get { return DPTCD + "(" + INSDPTCD + INSDPTCD2 + ")"; } }


        public void Clear()
        {
            DPTCD = "";
            INSDPTCD = "";
            INSDPTCD2 = "";
            DRID = "";
            DRNM = "";

            ORDDT = "";
            ORDTM = "";
            SPCNM = "";
            BLOODDT = "";
            BLOODTM = "";
            RCVDT = "";
            RCVTM = "";

            VFYDT = "";
            VFYTM = "";
            TESTCD = "";
            TESTNM = "";
            RSTVAL = "";
            REFERCHK = "";
            PANICCHK = "";
            DELTACHK = "";
            UNIT = "";
            REFERFR = "";
            REFERTO = "";
            SIGNFR = "";
            SIGNTO = "";
        }
    }
}
