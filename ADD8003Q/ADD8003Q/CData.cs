using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD8003Q
{
    class CData
    {
        private String m_DEMAMT;
        private String m_SENDAMT;
        private String m_DELAMT;
        private String m_LINEAMT;
        //private String m_LINEQTY;

        public String EPRTNO { get; set; }
        public String PNM { get; set; }
        public String DEMAMT
        {
            get
            {
                try
                {
                    int val = int.Parse(m_DEMAMT);
                    return String.Format("{0:#,###}", val);
                }
                catch (Exception ex)
                {
                    return m_DEMAMT;
                }
            }
            set { m_DEMAMT = value; }
        }
        public String SENDAMT
        {
            get
            {
                try
                {
                    int val = int.Parse(m_SENDAMT);
                    return String.Format("{0:#,###}", val);
                }
                catch (Exception ex)
                {
                    return m_SENDAMT;
                }
            }
            set { m_SENDAMT = value; }
        }
        public String DELAMT
        {
            get
            {
                try
                {
                    int val = int.Parse(m_DELAMT);
                    return String.Format("{0:#,###}", val);
                }
                catch (Exception ex)
                {
                    return m_DELAMT;
                }
            }
            set { m_DELAMT = value; }
        }
        public String ELINENO { get; set; }
        public String RECD { get; set; }
        public String RMK { get; set; }
        public String LINEAMT
        {
            get
            {
                try
                {
                    int val = int.Parse(m_LINEAMT);
                    return String.Format("{0:#,###}", val);
                }
                catch (Exception ex)
                {
                    return m_LINEAMT;
                }
            }
            set { m_LINEAMT = value; }
        }
        public String LINEQTY { get; set; }
        public String ACCNO;
        public String CNTNO;
        public String PRICD { get; set; }
        public String BGIHO { get; set; }

        public void Clear()
        {
            EPRTNO = "";
            PNM = "";
            DEMAMT = "";
            SENDAMT = "";
            DELAMT = "";
            ELINENO = "";
            RECD = "";
            RMK = "";
            LINEAMT = "";
            LINEQTY = "";
            ACCNO = "";
            CNTNO = "";
            PRICD = "";
            BGIHO = "";
        }
    }
}
