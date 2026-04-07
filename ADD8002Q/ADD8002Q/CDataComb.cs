using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD8002Q
{
    class CDataComb
    {
        public string PNM { get; set; }
        public string LNO { get; set; }
        public string HANGMOKNO { get; set; }
        public string CDGB { get; set; }
        public string BGIHO { get; set; }
        public string BGIHONM { get; set; }
        public string DANGA { get; set; }
        public string DQTY { get; set; }
        public string DDAY { get; set; }
        public string GUMAK { get; set; }
        public string JJBGIHO { get; set; }
        public string JJBGIHONM { get; set; }
        public string JJDANGA { get; set; }
        public string IJDQTY { get; set; }
        public string IJDAY { get; set; }
        public string TIJQTY { get; set; }
        public string JJGUMAK { get; set; }
        public string JJCD { get; set; }
        public string JJRMK { get; set; }
        public string REMARK
        {
            get
            {
                return (JJCD + " " + JJRMK).TrimStart();
            }
        }
        public string DATA_FG { get; set; }

        public void Clear()
        {
            PNM = "";
            LNO = "";
            HANGMOKNO = "";
            CDGB = "";
            BGIHO = "";
            BGIHONM = "";
            DANGA = "";
            DQTY = "";
            DDAY = "";
            GUMAK = "";
            JJBGIHO = "";
            JJBGIHONM = "";
            JJDANGA = "";
            IJDQTY = "";
            IJDAY = "";
            TIJQTY = "";
            JJGUMAK = "";
            JJCD = "";
            JJRMK = "";
            DATA_FG = "";
        }
    }
}
