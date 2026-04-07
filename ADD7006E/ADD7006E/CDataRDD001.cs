using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRDD001
    {
        public string BEDEDT;
        public string BEDEHM;

        public string EXDT;
        public string HMS;

        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;
        public string EXDRID;
        public string EXDRNM;

        public string ODIVCD;
        public string ODT;
        public string OTM;
        public string OCD;
        public string ONM;
        public string OQTY;
        public string OUNIT;
        public string ORDCNT;
        public string ODAYCNT;
        public string RMK;

        public string BEDEDTM { get { return BEDEDT + BEDEHM; } }
        public string EXDTM { get { return EXDT + HMS; } }

        public string DEPT_INFO { get { return DPTCD + "(" + INSDPTCD + INSDPTCD2 + ")"; } }
        public string ODTM { get { return ODT + OTM; } }
        public string ORDER_INFO
        {
            get
            {
                string ret = "";
                double oqty = MetroLib.StrHelper.ToDouble(OQTY);
                double ordcnt = MetroLib.StrHelper.ToDouble(ORDCNT);
                long odaycnt = MetroLib.StrHelper.ToLong(ODAYCNT);

                if (ODIVCD == "S")
                {
                    // 메시지 처방
                    ret = RMK.Replace("$", "").TrimEnd();
                }
                else
                {
                    ret += ONM;
                    if (oqty != 0)
                    {
                        ret += " " + oqty.ToString();
                        if (OUNIT != "") ret += " " + OUNIT;
                    }
                    if (ordcnt != 0)
                    {
                        ret += " " + ordcnt.ToString() + " 회";
                    }
                    ret += " " + odaycnt + " 일";
                }

                return ret;
            }
        }
        public string RMK_INFO
        {
            get
            {
                string ret ="";
                if (ODIVCD == "S")
                {
                    // 메시지 처방
                    ret = "";
                }
                else
                {

                    ret = RMK.Replace("$", "").TrimEnd();
                }
                return ret;
            }
        }

        public void Clear()
        {
            BEDEDT = "";
            BEDEHM = "";

            EXDT = "";
            HMS = "";

            DPTCD = "";
            INSDPTCD = "";
            INSDPTCD2 = "";
            EXDRID = "";
            EXDRNM = "";

            ODIVCD = "";
            ODT = "";
            OTM = "";
            OCD = "";
            ONM = "";
            OQTY = "";
            OUNIT = "";
            ORDCNT = "";
            ODAYCNT = "";
            RMK = "";
        }
    }
}
