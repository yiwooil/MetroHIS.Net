using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0724E
{
    class CDataSub
    {
        public string EPRTNO { get; set; }
        public string PNM { get; set; }
        public long JSAMT { get; set; }
        public long JSAMT_JIW { get; set; }
        public long JSAMT_MAX { get; set; }
        public long JSAMT_JAE { get; set; }
        public long JSAMT_OUT { get; set; }
        public long JSAMT1 { get; set; }
        public long JSAMT2 { get; set; }
        public string MEMO { get; set; }
        public string PID { get; set; }
        public string JRKWA { get; set; }
        public string PDRID { get; set; }
        public string DRNM { get; set; }
        public string DOCTOR
        {
            get
            {
                string[] jrkwa = (JRKWA + "$$$").Split('$');
                return jrkwa[2] + "-" + DRNM;
            }
        }
        public string MAINROW { get; set; }

        public void Clear()
        {
            EPRTNO = "";
            PNM = "";
            JSAMT = 0;
            JSAMT_JIW = 0;
            JSAMT_MAX = 0;
            JSAMT_JAE = 0;
            JSAMT_OUT = 0;
            JSAMT1 = 0;
            JSAMT2 = 0;
            MEMO = "";
            PID = "";
            JRKWA = "";
            PDRID = "";
            DRNM = "";
            MAINROW = "";
        }
    }
}
