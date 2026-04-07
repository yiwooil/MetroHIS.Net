using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD8003Q
{
    class CDataP
    {
        private String m_DEMCNTTOT;
        private String m_DEMQYTOT;
        private String m_EXAMCNTTOT;
        private String m_EXAMQYTOT;
        public String ACCNO { get; set; }
        public String CNTNO { get; set; }
        public String REPDT { get; set; }
        public String DEMCNTTOT
        {
            get
            {
                try
                {
                    int val = int.Parse(m_DEMCNTTOT);
                    return String.Format("{0:#,###}", val);
                }
                catch (Exception ex)
                {
                    return m_DEMCNTTOT;
                }
            }
            set { m_DEMCNTTOT = value; }
        }
        public String DEMQYTOT
        {
            get
            {
                try
                {
                    int val = int.Parse(m_DEMQYTOT);
                    return String.Format("{0:#,###}", val);
                }
                catch (Exception ex)
                {
                    return m_DEMQYTOT;
                }
            }
            set { m_DEMQYTOT = value; }
        }
        public String EXAMCNTTOT
        {
            get
            {
                try
                {
                    int val = int.Parse(m_EXAMCNTTOT);
                    return String.Format("{0:#,###}", val);
                }
                catch (Exception ex)
                {
                    return m_EXAMCNTTOT;
                }
            }
            set { m_EXAMCNTTOT = value; }
        }
        public String EXAMQYTOT
        {
            get
            {
                try
                {
                    int val = int.Parse(m_EXAMQYTOT);
                    return String.Format("{0:#,###}", val);
                }
                catch (Exception ex)
                {
                    return m_EXAMQYTOT;
                }
            }
            set { m_EXAMQYTOT = value; }
        }
        public String MEMO { get; set; }

        public void Clear()
        {
            ACCNO = "";
            CNTNO = "";
            REPDT = "";
            DEMCNTTOT = "";
            DEMQYTOT = "";
            EXAMCNTTOT = "";
            EXAMQYTOT = "";
            MEMO = "";
        }
    }
}
