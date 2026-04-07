using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0707E
{
    class CData
    {
        public string DEMDIVNM { get; set; }
        public string ITEMCD { get; set; }
        public string ITEMINFO1 { get; set; }
        public string ITEMINFO2 { get; set; }
        public string ITEMINFO3 { get; set; }
        public string PTYPE { get; set; }
        public string PDUT { get; set; }
        public string BUSINESSCD { get; set; }
        public string TRADENM { get; set; }
        public string SNDIVNM { get; set; }
        public string BUYDT { get; set; }
        public long BUYQTY { get; set; }
        public long BUYAMT { get; set; }
        public long EADANGA { get; set; }
        public string PRICD { get; set; }
        public string RCVRESID { get; set; }
        public string RCVNM { get; set; }
        public string USEDT { get; set; }
        public string ELINENO { get; set; }
        public string EXPDT { get; set; }

        public string DEMDIV
        {
            get
            {
                if (DEMDIVNM == "치료재료") return "A";
                else if (DEMDIVNM == "원료약") return "B";
                else if (DEMDIVNM == "비급여약제") return "C";
                else return "A";
            }
        }
        public string SNDIV
        {
            get
            {
                if (SNDIVNM == "선납") return "B";
                else if (SNDIVNM == "2년경과") return "2";
                else return "";
            }
        }

        public string ITEMINFO
        {
            get
            {
                return ITEMINFO1 + "\\" + ITEMINFO2 + "\\" + ITEMINFO3;
            }
            set
            {
                char[] deli = { '\\' };
                string[] infos = (value + "\\\\").Split(deli);
                ITEMINFO1 = infos[0];
                ITEMINFO2 = infos[1];
                ITEMINFO3 = infos[2];
            }
        }

        public string GetDEMDIVNM(string demdiv)
        {
            if (demdiv == "A") return "치료재료";
            else if (demdiv == "B") return "원료약";
            else if (demdiv == "C") return "비급여약제";
            else return "치료재료";
        }

        public string GetSNDiVNM(string sndiv)
        {
            if (sndiv == "B") return "선납";
            else if (sndiv == "2") return "2년경과";
            else return "";
        }

        public void Clear()
        {
            DEMDIVNM = "";
            ITEMCD = "";
            ITEMINFO1 = "";
            ITEMINFO2 = "";
            ITEMINFO3 = "";
            PTYPE = "";
            PDUT = "";
            BUSINESSCD = "";
            TRADENM = "";
            SNDIVNM = "";
            BUYDT = "";
            BUYQTY = 0;
            BUYAMT = 0;
            EADANGA = 0;
            PRICD = "";
            RCVRESID = "";
            RCVNM = "";
            USEDT = "";
            ELINENO = "";
            EXPDT = "";
        }
    }
}
