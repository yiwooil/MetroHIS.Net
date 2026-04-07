using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0251Q
{
    class CData
    {
        private String m_UNAMT_B;
        private String m_UNAMT_A;
        public String CANCELDT { get; set; }
        public String PID { get; set; }
        public String PNM { get; set; }
        public String BEDEDT { get; set; }
        public String BEDODT { get; set; }
        public String QFYCD_B { get; set; }
        public String DPTCD_B { get; set; }
        public String RPDT_B { get; set; }
        public String RPID_B { get; set; }
        public String UNAMT_B
        {
            get
            {
                try
                {
                    int val = int.Parse(m_UNAMT_B);
                    return String.Format("{0:#,###}", val);
                }
                catch (Exception ex)
                {
                    return m_UNAMT_B;
                }
            }
            set { m_UNAMT_B = value; }
        }
        public String SIMNO_B { get; set; }
        public String QFYCD_A { get; set; }
        public String DPTCD_A { get; set; }
        public String RPDT_A { get; set; }
        public String RPID_A { get; set; }
        public String UNAMT_A
        {
            get
            {
                try
                {
                    int val = int.Parse(m_UNAMT_A);
                    return String.Format("{0:#,###}", val);
                }
                catch (Exception ex)
                {
                    return m_UNAMT_A;
                }
            }
            set { m_UNAMT_A = value; }
        }

        public void Clear()
        {
            CANCELDT = "";
            PID = "";
            PNM = "";
            BEDEDT = "";
            BEDODT = "";
            QFYCD_B = "";
            DPTCD_B = "";
            RPDT_B = "";
            RPID_B = "";
            UNAMT_B = "";
            SIMNO_B = "";
            QFYCD_A = "";
            DPTCD_A = "";
            RPDT_A = "";
            RPID_A = "";
            UNAMT_A = "";
        }
    }
}
