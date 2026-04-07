using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0545R
{
    class CData
    {
        public string DOCUNO;
        public long OBJCOUNT;
        public long OBJAMT1;
        public long OBJAMT2;
        public long OBJAMTTOT { get { return OBJAMT1 + OBJAMT2; } }
        public string PRTDT;
        public string DEMSEQ;
        public string CNECNO;
        public string DEMNO;

        void Clear()
        {
            DOCUNO = "";
            OBJCOUNT = 0;
            OBJAMT1 = 0;
            OBJAMT2 = 0;
            PRTDT = "";
            DEMSEQ = "";
            CNECNO = "";
            DEMNO = "";
        }
    }
}
