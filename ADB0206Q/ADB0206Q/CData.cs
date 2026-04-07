using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADB0206Q
{
    class CData
    {
        private String m_PNM;

        public String PID { get; set; }
        public String PNM
        {
            get
            {
                String strRet = m_PNM;
                if (PnmFg == "1")
                {
                    strRet = (m_PNM+"   ").Substring(0, 1) + "*" + (m_PNM+"   ").Substring(2, 1);
                }
                return strRet;
            }
            set
            {
                m_PNM = value;
            }
        }
        public String RESID { get; set; }
        public String PSEX { get; set; }
        public String PAGE { get; set; }
        public String PSEXAGE
        {
            get
            {
                return PSEX + "/" + PAGE;
            }
        }
        public String WARDID { get; set; }
        public String RMID { get; set; }
        public String BEDID { get; set; }
        public String WARD
        {
            get
            {
                return WARDID + "-" + RMID + "-" + BEDID;
            }
        }
        public String BEDGRD { get; set; }
        public String WARDSTATUS { get; set; }
        public String TEL { get; set; }
        public String BEDEDT { get; set; }
        public String BEDODT { get; set; }
        public String ILSU { get; set; }
        public String BEDIPTHCD { get; set; }
        public String BEDIPTHNM
        {
            get
            {
                String strRet = "";
			    if(BEDIPTHCD =="2") {
				    strRet = "응급";
			    } else if (BEDIPTHCD =="A") {
				    strRet = "자의";
			    } else if (BEDIPTHCD =="B") {
				    strRet = "보호";
                }
                else if (BEDIPTHCD == "C")
                {
                    strRet = "동의";
                }
                else
                {
                    strRet = "외래";
                }
                return strRet;
            }
        }
        public String DPTNM { get; set; }
        public String PDRNM { get; set; }
        public String CASEWORKERNM { get; set; }
        public String QFYCD { get; set; }
        public String QFYNM { get; set; }
        public String UNINM { get; set; }
        public String ADDR { get; set; }
        public String HTELNO { get; set; }
        public String DRRMK { get; set; }
        public String FAMNM { get; set; }
        public String FTEL { get; set; }
        public String FADDR { get; set; }
        public String DISENM { get; set; }
        public String JINRMK { get; set; }
        public String OPDT { get; set; }
        public String RSVOP { get; set; }
        public String OTELNO { get; set; }
        public String ILLST { get; set; }
        public String PSTS { get; set; }
        public String BEDINTENTDT { get; set; }
        public String BEDINTENTDT2 { get; set; }
        public String ADLRT { get; set; }
        public String DRRMK2 { get; set; }
        public String WONRMK { get; set; }
        public String SCHBEDODT { get; set; }
        public String QFYSBNM { get; set; }
        public String SIMRMK { get; set; }
        public String PnmFg;

        public void Clear()
        {
            PID = "";
            PNM = "";
            RESID = "";
            PSEX = "";
            PAGE = "";
            WARDID = "";
            RMID = "";
            BEDID = "";;
            BEDGRD = "";
            WARDSTATUS = "";
            TEL = "";
            BEDEDT = "";
            BEDODT = "";
            ILSU = "";
            BEDIPTHCD = "";
            DPTNM = "";
            PDRNM = "";
            CASEWORKERNM = "";
            QFYCD = "";
            QFYNM = "";
            UNINM = "";
            ADDR = "";
            HTELNO = "";
            DRRMK = "";
            FAMNM = "";
            FTEL = "";
            FADDR = "";
            DISENM = "";
            JINRMK = "";
            OPDT = "";
            RSVOP = "";
            OTELNO = "";
            ILLST = "";
            PSTS = "";
            BEDINTENTDT = "";
            BEDINTENTDT2 = "";
            ADLRT = "";
            DRRMK2 = "";
            WONRMK = "";
            SCHBEDODT = "";
            QFYSBNM = "";
            SIMRMK = "";
            PnmFg = "";
        }
    }
}
