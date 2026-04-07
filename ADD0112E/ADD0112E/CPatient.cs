using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0112E
{
    class CPatient
    {
        
        public string PID { get; set; }
        public string PNM { get; set; }
        private string m_RESID;
        public string RESID
        {
            get
            {
                return m_RESID.Substring(0, 6) + "-" + m_RESID.Substring(6, 1) + "******";
            }
            set
            {
                m_RESID = (value + "             ").Substring(0, 13);
            }
        }
        public string RESID_NO_MASK
        {
            get
            {
                return m_RESID.Substring(0, 6) + "-" + m_RESID.Substring(6, 7);
            }
        }
        public string LSTDT { get; set; }
        public string QFYNM { get; set; }
        public string BEDEDT { get; set; }
        public string HTELNO { get; set; }
        public string OTELNO { get; set; }
        public string ADDR { get; set; }
        public string INSNM { get; set; }
        public string RMK { get; set; }
    }
}
