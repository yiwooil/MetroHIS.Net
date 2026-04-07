using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD8002Q
{
    class CDataDetail
    {
        public String PNM { get; set; }
        public String EPRTNO { get; set; }
        public String LNO { get; set; }
        public String HANGMOKNO { get; set; }
        public String CDGB { get; set; }
        public String BGIHO { get; set; }
        public String BGIHONM { get; set; }
        public long DANGA { get; set; }
        public double DQTY { get; set; }
        public long DDAY { get; set; }
        public double TQTY { get; set; }
        public long GUMAK  { get; set; }
        public String JJBGIHO  { get; set; }
        public String JJBGIHONM { get; set; }
        public long JJDANGA { get; set; }
        public double IJDQTY { get; set; }
        public long IJDAY { get; set; }
        public double TIJQTY { get; set; }
        public long JJGUMAK { get; set; }
        public String JJCD { get; set; }
        public String JJRMK { get; set; }
        public String JJCD_JJRMK
        {
            get
            {
                string ret = "";
                if (JJCD != "") ret = JJCD + ".";
                if (JJRMK != "") ret += JJRMK;
                
                return ret;
            }
        }

        public void Clear()
        {
            PNM = "";
            EPRTNO = "";
            LNO = "";
            HANGMOKNO = "";
            CDGB = "";
            BGIHO = "";
            BGIHONM = "";
            DANGA = 0;
            DQTY = 0;
            DDAY = 0;
            TQTY = 0;
            GUMAK = 0;
            JJBGIHO = "";
            JJBGIHONM = "";
            JJDANGA = 0;
            IJDQTY = 0;
            IJDAY = 0;
            TIJQTY = 0;
            JJGUMAK = 0;
            JJCD = "";
            JJRMK = "";
        }
    }
}
