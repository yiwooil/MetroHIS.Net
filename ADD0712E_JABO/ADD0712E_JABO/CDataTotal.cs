using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0712E_JABO
{
    class CDataTotal
    {
        private string m_FTDAYS;

        public string DEMNO { get; set; }
        public string FTDAYS
        {
            get
            {
                if (m_FTDAYS.Length == 16)
                {
                    return m_FTDAYS.Substring(0, 8) + "-" + m_FTDAYS.Substring(8);
                }
                else
                {
                    return m_FTDAYS;
                }
            }
            set { m_FTDAYS = value; }
        }
        public string DEMSEQ { get; set; }
        public string REDAY { get; set; }
        public string CNECNO { get; set; }
        public string GRPNO { get; set; }
        public string DCOUNT { get; set; }
        public string HOSID { get; set; }
        public string JBUNICD { get; set; }
        public string MEMO { get; set; }
        public string REDPT1 { get; set; }
        public string REDPT2 { get; set; }
        public string REDPNM { get; set; }
        public string RETELE { get; set; }
        public string REDPT
        {
            get { return REDPT1 + " " + REDPT2 + " " + REDPNM + " " + RETELE; }
        }
        public long CNT1 { get; set; } //청구건수
        public long UNAMT1 { get; set; } // 청구금액
        public long TTTAMT1 { get; set; } // 진료비총액
        public long JBPTAMT1 { get; set; } // 환자납부총액
        public long RQUPLMTCHATTAMT { get; set; } // 약제상한차액총액
        public long CNT2 { get; set; } // 심사건수
        public long ARSTAMT2 { get; set; } // 심사결정액
        public long TTTAMT2 { get; set; } // 진료비총액
        public long JBPTAMT2 { get; set; } // 환자납부총액
        public long ASUTAMT2 { get; set; } // 수탁기관직접지급액
        public long SKUPLMTCHATTAMT { get; set; } // 약제상한차액총액
        public long SKJJUPLMTCHATTAMT { get; set; } // 약제상한차액조정금액
        public long RCNT1 { get; set; }
        public long RTTAMT1 { get; set; }
        public long RJBPTAMT1 { get; set; }
        public long RCNT2 { get; set; }
        public long RCALCAMT2 { get; set; }
        public long RJBPTAMT2 { get; set; }
        public string OBJDT1
        {
            get
            {
                try
                {
                    DateTime dtReday = DateTime.ParseExact(REDAY, "yyyyMMdd", null);
                    string ret = dtReday.AddDays(24).ToString("yyyyMMdd");
                    return ret;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
        }
        public string OBJDT2 { get { return OBJDT1; } }

        public void Clear()
        {
            DEMNO = "";
            FTDAYS = "";
            DEMSEQ = "";
            REDAY = "";
            CNECNO = "";
            GRPNO = "";
            DCOUNT = "";
            HOSID = "";
            JBUNICD = "";
            MEMO = "";
            REDPT1 = "";
            REDPT2 = "";
            REDPNM = "";
            RETELE = "";
            CNT1 = 0; //청구건수
            UNAMT1 = 0; // 청구금액
            TTTAMT1 = 0; // 진료비총액
            JBPTAMT1 = 0; // 환자납부총액
            RQUPLMTCHATTAMT = 0; // 약제상한차액총액
            CNT2 = 0; // 심사건수
            ARSTAMT2 = 0; // 심사결정액
            TTTAMT2 = 0; // 진료비총액
            JBPTAMT2 = 0; // 환자납부총액
            ASUTAMT2 = 0; // 수탁기관직접지급액
            SKUPLMTCHATTAMT = 0; // 약제상한차액총액
            SKJJUPLMTCHATTAMT = 0; // 약제상한차액조정금액
            RCNT1 = 0;
            RTTAMT1 = 0;
            RJBPTAMT1 = 0;
            RCNT2 = 0;
            RCALCAMT2 = 0;
            RJBPTAMT2 = 0;
        }
    }
}
