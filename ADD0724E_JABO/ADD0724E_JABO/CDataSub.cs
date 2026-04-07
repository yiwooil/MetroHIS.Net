using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0724E_JABO
{
    class CDataSub
    {
        public long EPRTNO { get; set; }
        public string PNM { get; set; }
        public string APPRNO { get; set; }
        public long JSAMT { get; set; }
        public long JSTTTAMT { get; set; }
        public long JSJBPTAMT { get; set; }
        public long JSAMT1 { get; set; }
        public long JSAMT2 { get; set; }
        public string MEMO { get; set; }
        public long MAINROW { get; set; } // 출력할 때 헤더정보를 가져오기 위한 값

        public void Clear()
        {
            EPRTNO = 0;
            PNM = "";
            APPRNO = "";
            JSAMT = 0;
            JSTTTAMT = 0;
            JSJBPTAMT = 0;
            JSAMT1 = 0;
            JSAMT2 = 0;
            MEMO = "";
            MAINROW = 0;
        }
    }
}
