using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    public class PtntChangedEventArgs : EventArgs
    {
        public string frdt { get; private set; }
        public string todt { get; private set; }
        public int no { get; private set; }
        public string cnecno { get; private set; }
        public string cnecdd { get; private set; }
        public string eprtno { get; private set; }
        public string pnm { get; private set; }
        public string dacd1 { get; private set; }
        public string opcode1 { get; private set; }

        // 결과 리턴용
        public bool Success { get; set; }
        public string FailureMessage { get; set; }

        public PtntChangedEventArgs(string frdt, string todt, int no, string cnecno, string cnecdd, string eprtno, string pnm, string dacd1, string opcode1)
        {
            this.frdt = frdt;
            this.todt = todt;
            this.no = no;
            this.cnecno = cnecno;
            this.cnecdd = cnecdd;
            this.eprtno = eprtno;
            this.pnm = pnm;
            this.dacd1 = dacd1;
            this.opcode1 = opcode1;

            this.Success = false;
            this.FailureMessage = "";
        }
    }
}
