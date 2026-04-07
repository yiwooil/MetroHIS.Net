using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0602E
{
    class CTI32
    {
        public int CNT;
        public string OBJDIV;
        public string DOCUNO;
        public string OBJCOUNT;
        public string SAKAMT1;
        public string SAKAMT2;
        public string OBJAMT1;
        public string OBJAMT2;
        public string DEMSEQ;
        public string CNECNO;
        public string GRPNO;
        public string DCOUNT;
        public string DEMNO;
        public string REDAY;
        public string ENTDT;
        public string ENTTM;
        public string EMPID;
        public string PRTDT;
        public string PRTTM;
        public string PRTID;

        public string GetOBJAMT()
        {
            int objamt1 = 0;
            int objamt2 = 0;
            int.TryParse(OBJAMT1, out objamt1);
            int.TryParse(OBJAMT2, out objamt2);
            return (objamt1 + objamt2).ToString();
        }

        public void Clear()
        {
            CNT = 0;
            OBJDIV = "";
            DOCUNO = "";
            OBJCOUNT = "";
            SAKAMT1 = "";
            SAKAMT2 = "";
            OBJAMT1 = "";
            OBJAMT2 = "";
            DEMSEQ = "";
            CNECNO = "";
            GRPNO = "";
            DCOUNT = "";
            DEMNO = "";
            REDAY = "";
            ENTDT = "";
            ENTTM = "";
            EMPID = "";
            PRTDT = "";
            PRTTM = "";
            PRTID = "";
        }
    }
}
