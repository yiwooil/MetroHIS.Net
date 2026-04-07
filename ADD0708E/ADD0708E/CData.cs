using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0708E
{
    class CData
    {
        public int ELINENO { get; set; }
        public string MKDIVNM { get; set; }
        public string MKDIV
        {
            get
            {
                if (MKDIVNM == "조제") return "1";
                if (MKDIVNM == "제제") return "2";
                return "1";
            }
        }
        public string PHADIVNM { get; set; }
        public string PHADIV
        {
            get
            {
                if (PHADIVNM == "내복약") return "1";
                if (PHADIVNM == "주사약") return "2";
                if (PHADIVNM == "외용약") return "3";
                return "1";
            }
        }
        public string DRGEFKND { get; set; }
        public string ITEMCD { get; set; }
        public string ITEMNM { get; set; }
        public string WRITEDT { get; set; }
        public int DEMAMT { get; set; }
        public string PTYPE { get; set; }
        public string PDUT { get; set; }
        public string ADTDT { get; set; }
        public string DRGEF { get; set; }
        public string DOESQY { get; set; }

        public string BUSINESSCD { get; set; }
        public string TRADENM { get; set; }
        public string CODEDIVNM { get; set; }
        public string CODEDIV
        {
            get
            {
                if (CODEDIVNM == "보험등재약") return "1";
                if (CODEDIVNM == "원료약") return "2";
                return "1";
            }
        }
        public string DRGCD { get; set; }
        public string DRGNM { get; set; }
        public string PTYPE_3 { get; set; }
        public string PDUT_3 { get; set; }
        public string BUYDT { get; set; }
        public float UTAMT { get; set; }
        public float DRGQTY { get; set; }
        public string QTYUT { get; set; }
        public float QTYAMT { get; set; }

        public void Clear()
        {
            ELINENO = 0;
            MKDIVNM = "";
            PHADIVNM = "";
            DRGEFKND = "";
            ITEMCD = "";
            ITEMNM = "";
            WRITEDT = "";
            DEMAMT = 0;
            PTYPE = "";
            PDUT = "";
            ADTDT = "";
            DRGEF = "";
            DOESQY = "";

            BUSINESSCD = "";
            TRADENM = "";
            CODEDIVNM = "";
            DRGCD = "";
            DRGNM = "";
            PTYPE_3 = "";
            PDUT_3 = "";
            BUYDT = "";
            UTAMT = 0;
            DRGQTY = 0;
            QTYUT = "";
            QTYAMT = 0;
        }
    }
}
