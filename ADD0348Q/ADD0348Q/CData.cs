using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0348Q
{
    class CData
    {
        private String m_OUTSEQ;
        private String m_PRINM;
        private String m_DANGA;
        public String PID { get; set; }
        public String PNM { get; set; }
        public String BEDEDT { get; set; }
        public String BEDODT { get; set; }
        public String QFYCD { get; set; }
        public String EXDT;
        public String S41QFYCD { get; set; }
        public String S41DRID { get; set; }
        public String S41DRNM { get; set; }
        public String OUTSEQ
        {
            get
            {
                try
                {
                    return m_OUTSEQ.Substring(0, 8) + "-" + m_OUTSEQ.Substring(8);
                }
                catch (Exception ex)
                {
                    return m_OUTSEQ;
                }
            }
            set { m_OUTSEQ = value; }
        }
        public String PRICD { get; set; }
        public String EDICODE { get; set; }
        public String PRINM
        {
            get
            {
                return (IsDC ? "[D/C]" : "") + m_PRINM;
            }
            set { m_PRINM = value; }
        }
        public String DANGA
        {
            get
            {
                try
                {
                    int val = int.Parse(m_DANGA);
                    return String.Format("{0:#,###}", val);
                }
                catch (Exception ex)
                {
                    return m_DANGA;
                }
            }
            set { m_DANGA = value; }
        }
        public String DQTY { get; set; }
        public String DDAY { get; set; }
        public String ORDCNT;
        public String GUMAK;
        public String KYSTR;
        public Boolean IsDC;
        public String FRFG;
        public String DPTCD;
        public String BDIV;

        public void Clear()
        {
            PID = "";
            PNM = "";
            BEDEDT = "";
            BEDODT = "";
            QFYCD = "";
            EXDT = "";
            S41QFYCD = "";
            S41DRID = "";
            S41DRNM = "";
            OUTSEQ = "";
            PRICD = "";
            EDICODE = "";
            PRINM = "";
            DANGA = "";
            DQTY = "";
            ORDCNT = "";
            DDAY = "";
            GUMAK = "";
            KYSTR = "";
            IsDC = false;
            FRFG = "";
            DPTCD = "";
            BDIV = "";
        }
    }
}
