using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0705E
{
    class CData
    {
        public bool SEL { get; set; }

        public string BUYDT { get; set; }
        public string ITEMCD { get; set; }
        public string ITEMINFO { get; set; }
        public string STDSIZE { get; set; }
        public string UNIT { get; set; }
        public double BUYQTY { get; set; }
        public long BUYTOTAMT { get; set; }
        public long BUYAMT { get; set; }
        public string BUSSCD { get; set; }
        public string BUSSNM { get; set; }
        public string FSTBUYFG { get; set; }
        public string MEMO { get; set; }
        public string PRODCM { get; set; }
        public string EMPID { get; set; }
        public string ENTDT { get; set; }
        public string ENTTM { get; set; }
        public string UPDID { get; set; }
        public string UPDDT { get; set; }
        public string UPDTM { get; set; }
        public double ADDAVR { get; set; }

        public void Clear()
        {
            SEL = false;

            BUYDT = "";
            ITEMCD = "";
            ITEMINFO = "";
            STDSIZE = "";
            UNIT = "";
            BUYQTY = 0;
            BUYTOTAMT = 0;
            BUYAMT = 0;
            BUSSCD = "";
            BUSSNM = "";
            FSTBUYFG = "";
            MEMO = "";
            PRODCM = "";
            EMPID = "";
            ENTDT = "";
            ENTTM = "";
            UPDID = "";
            UPDDT = "";
            UPDTM = "";
            ADDAVR = 0;
        }

    }
}
