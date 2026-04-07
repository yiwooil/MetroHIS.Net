using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0714E
{
    class CData
    {
        public string CNECNO;
        public string GRPNO;
        public string HOSID;
        public string GUBUN;
        public long ACNT;
        public long ATTAMT;
        public long APTAMT;
        public long AJAM;
        public long AUNAMT;
        public long APMGUM;
        public long ASTGUM;
        public long AGUMAK;
        public long AMPAMT;
        public long RCNT;
        public long RTTAMT;
        public string MEMO;

        public void Clear()
        {
            CNECNO = "";
            GRPNO = "";
            HOSID = "";
            GUBUN = "";
            ACNT = 0;
            ATTAMT = 0;
            APTAMT = 0;
            AJAM = 0;
            AUNAMT = 0;
            APMGUM = 0;
            ASTGUM = 0;
            AGUMAK = 0;
            AMPAMT = 0;
            RCNT = 0;
            RTTAMT = 0;
            MEMO = "";
        }
    }
}
