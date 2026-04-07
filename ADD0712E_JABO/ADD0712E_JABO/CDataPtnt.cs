using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0712E_JABO
{
    class CDataPtnt
    {
        public string EPRTNO { get; set; }
        public string PNM { get; set; }
        public string STEDT { get; set; }
        public string MDACD { get; set; }
        public long UNAMT { get; set; }
        public long TTTAMT { get; set; }
        public long JBPTAMT { get; set; }
        public long RQUPLMTCHATTAMT { get; set; }
        public long ACTGUM { get; set; }
        public long ATTTAMT { get; set; }
        public long ASTAMT { get; set; }
        public long AJBPTAMT { get; set; }
        public long SKUPLMTCHATTAMT { get; set; }
        public long CHOGUM { get; set; }
        public long CHOCNT { get; set; }
        public long CHONCNT { get; set; }
        public long JAEGUM { get; set; }
        public long JAECNT { get; set; }
        public long JAENCNT { get; set; }
        public long EXAMC { get; set; }
        public long ORDDAYS { get; set; }
        public long ORDCNT { get; set; }
        public string APPRNO { get; set; }
        public string DEMNO { get; set; }
        public string MEMO { get; set; }
        public string IOFG { get; set; }

        public void Clear()
        {
            EPRTNO = "";
            PNM = "";
            STEDT = "";
            MDACD = "";
            UNAMT = 0;
            TTTAMT = 0;
            JBPTAMT = 0;
            RQUPLMTCHATTAMT = 0;
            ACTGUM = 0;
            ATTTAMT = 0;
            ASTAMT = 0;
            AJBPTAMT = 0;
            SKUPLMTCHATTAMT = 0;
            CHOGUM = 0;
            CHOCNT = 0;
            CHONCNT = 0;
            JAEGUM = 0;
            JAECNT = 0;
            JAENCNT = 0;
            EXAMC = 0;
            ORDDAYS = 0;
            ORDCNT = 0;
            APPRNO = "";
            DEMNO = "";
            MEMO = "";
            IOFG = "";
        }
    }
}
