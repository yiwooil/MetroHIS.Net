using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0712E_JABO
{
    class CDataBul
    {
        public string EPRTNO { get; set; }
        public string PID { get; set; }
        public string A_PNM { get; set; }
        public string N2_PNM { get; set; }
        public string DPTCD { get; set; }
        public string DRNM { get; set; }
        public string MDACD { get; set; }
        public string JJRMK1 { get; set; }
        public string JJRMK2 { get; set; }
        public string JJDETAIL { get; set; }
        public long JJGUMAK { get; set; }
        public string JJRMKNM { get; set; }
        public string JJTEXT { get; set; }
        public string DEMNO { get; set; }
        public string REDAY { get; set; }

        public string PNM
        {
            get { return (A_PNM == "" ? N2_PNM : A_PNM); }
        }
        public string DOCTOR
        {
            get { return DPTCD + "-" + DRNM; }
        }
        public string JJRMK
        {
            get { return JJRMK1 + JJRMK2 + JJDETAIL; }
        }
        public string BIGO
        {
            get { return JJRMKNM + "/" + JJTEXT; }
        }

        public void Clear()
        {
            EPRTNO = "";
            PID = "";
            A_PNM = "";
            N2_PNM = "";
            DPTCD = "";
            DRNM = "";
            MDACD = "";
            JJRMK1 = "";
            JJRMK2 = "";
            JJDETAIL = "";
            JJGUMAK = 0;
            JJRMKNM = "";
            JJTEXT = "";
            DEMNO = "";
            REDAY = "";
        }
    }
}
