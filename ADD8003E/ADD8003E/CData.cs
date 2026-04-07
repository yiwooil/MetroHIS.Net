using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD8003E
{
    class CData
    {
        public String REQYM;
        public String REQSEQ;
        public String ELINESEQ { get; set; }
        public String MKDIV { get; set; }
        public String MKDIVNM
        {
            get
            {
                if (MKDIV == "1") return "조제";
                else if (MKDIV == "2") return "제제";
                else return MKDIV;
            }
        }
        public String PHADIV { get; set; }
        public String PHADIVNM
        {
            get
            {
                if (PHADIV == "1") return "내복약";
                else if (PHADIV == "2") return "주사약";
                else if (PHADIV == "3") return "외용약";
                else return PHADIV;
            }
        }
        public String DRGEFFKND { get; set; }
        public String ITEMCD { get; set; }
        public String ITEMNM { get; set; }
        public String WRITEDT { get; set; }
        public String DEMAMT { get; set; }
        public String STDSIZE { get; set; }
        public String UNIT { get; set; }
        public String APPLDT { get; set; }
        public String DRGEFF { get; set; }
        public String DOESQY { get; set; }

        public void Clear()
        {
            REQYM = "";
            REQSEQ = "";
            ELINESEQ = "";
            MKDIV = "";
            PHADIV = "";
            DRGEFFKND = "";
            ITEMCD = "";
            ITEMNM = "";
            WRITEDT = "";
            DEMAMT = "";
            STDSIZE = "";
            UNIT = "";
            APPLDT = "";
            DRGEFF = "";
            DOESQY = "";
        }
    }

}
