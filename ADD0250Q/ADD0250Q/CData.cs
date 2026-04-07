using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0250Q
{
    class CData
    {
        public String NO { get; set; }
        public String BTHDT;
        public String QFYCD;
        public String PDIV;
        public String QFYSB;
        public String PDIVS;
        public string DRGNO { get; set; }

        private String m_PID;
        private String m_BEDEDT;
        private String m_PNM;
        private String m_PSEX;
        private String m_PAGE;
        private String m_BEDODT;
        private String m_CREDT;
        private String m_QFYCDNM;
        private String m_PDIVNM;
        private String m_RPOK;
        private String m_QFYSBNM;
        private String m_PDIVSNM;
        private String m_RPOKS;
        private String m_PDRNM;
        private String m_DPTCD;
        private String m_WARD;
        private String m_BEDODIVNM;
        private String m_SIMSANM;

        public String PID
        {
            get
            {
                return m_PID;
            }
            set
            {
                m_PID = value;
            }
        }
        public String BEDEDT
        {
            get
            {
                return m_BEDEDT;
            }
            set
            {
                m_BEDEDT = value;
            }
        }
        public String PNM
        {
            get
            {
                return m_PNM;
            }
            set
            {
                m_PNM = value;
            }
        }
        public String PSEX
        {
            set
            {
                m_PSEX = value;
            }
        }
        public String PAGE
        {
            set
            {
                m_PAGE = value;
            }
        }
        public String PSEXAGE
        {
            get
            {
                return m_PSEX + "/" + m_PAGE;
            }
        }
        public String BEDODT
        {
            get
            {
                return m_BEDODT;
            }
            set
            {
                m_BEDODT = value;
            }
        }
        public String CREDT
        {
            get
            {
                return m_CREDT;
            }
            set
            {
                m_CREDT = value;
            }
        }
        public String QFYCDNM
        {
            get
            {
                return (m_QFYCDNM == "" ? QFYCD : m_QFYCDNM);
            }
            set
            {
                m_QFYCDNM = value;
            }
        }
        public String PDIVNM
        {
            get{
                return (m_PDIVNM == "" ? PDIV : m_PDIVNM);
            }
            set
            {
                m_PDIVNM = value;
            }
        }
        public String RPOK
        {
            get
            {
                return m_RPOK;
            }
            set
            {
                m_RPOK = value;
            }
        }
        public String QFYSBNM
        {
            get
            {
                return (m_QFYSBNM==""?QFYSB: m_QFYSBNM);
            }
            set
            {
                m_QFYSBNM = value;
            }
        }
        public String PDIVSNM
        {
            get
            {
                return (m_PDIVSNM==""?PDIVS: m_PDIVSNM);
            }
            set
            {
                m_PDIVSNM = value;
            }
        }
        public String RPOKS
        {
            get
            {
                return m_RPOKS;
            }
            set
            {
                m_RPOKS = value;
            }
        }
        public String PDRNM
        {
            get
            {
                return m_PDRNM;
            }
            set
            {
                m_PDRNM = value;
            }
        }
        public String DPTCD
        {
            get
            {
                return m_DPTCD;
            }
            set
            {
                m_DPTCD = value;
            }
        }
        public String WARD
        {
            get
            {
                return m_WARD;
            }
            set
            {
                m_WARD = value;
            }
        }
        public String BEDODIVNM
        {
            get
            {
                return m_BEDODIVNM;
            }
            set
            {
                m_BEDODIVNM = value;
            }
        }
        public String SIMSANM
        {
            get
            {
                return m_SIMSANM;
            }
            set
            {
                m_SIMSANM = value;
            }
        }

        public void Clear()
        {
            NO = "";
            BTHDT = "";
            QFYCD = "";
            PDIV = "";
            QFYSB = "";
            PDIVS = "";
            DRGNO = "";

            m_PID = "";
            m_BEDEDT = "";
            m_PNM = "";
            m_PSEX = "";
            m_PAGE = "";
            m_BEDODT = "";
            m_CREDT = "";
            m_QFYCDNM = "";
            m_PDIVNM = "";
            m_RPOK = "";
            m_QFYSBNM = "";
            m_PDIVSNM = "";
            m_RPOKS = "";
            m_PDRNM = "";
            m_DPTCD = "";
            m_WARD = "";
            m_BEDODIVNM = "";
            m_SIMSANM = "";
        }
    }
}
