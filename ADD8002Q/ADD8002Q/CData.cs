using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD8002Q
{
    class CData
    {
        public String BUSSDIVNM { get; set; }
        public String DEMNO { get; set; }
        public String SENDNO { get; set; }
        private String m_ACCNO;
        public String ACCNO
        {
            get
            {
                if (m_ACCNO.Length != 15)
                {
                    return m_ACCNO;
                }
                else
                {
                    return m_ACCNO.Substring(0, 4) + "-" + m_ACCNO.Substring(4, 4) + "-" + m_ACCNO.Substring(8);
                }
            }
            set { m_ACCNO = value; }
        }
        private String m_ACCDT;
        public String ACCDT
        {
            get
            {
                if (m_ACCDT.Length != 8)
                {
                    return m_ACCDT;
                }
                else
                {
                    return m_ACCDT.Substring(0, 4) + "-" + m_ACCDT.Substring(4, 2) + "-" + m_ACCDT.Substring(6, 2);
                }
            }
            set { m_ACCDT = value; }
        }
        public String HOSID { get; set; }
        public String HOSNM { get; set; }
        public String CEONM { get; set; }
        private String m_CNTTOT;
        public String CNTTOT
        {
            get
            {
                return MetroLib.StrHelper.ToNumberWithComma(m_CNTTOT) + " 건";
            }
            set { m_CNTTOT = value; }
        }
        private String m_DEMTOT;
        public String DEMTOT
        {
            get
            {
                return MetroLib.StrHelper.ToNumberWithComma(m_DEMTOT) + " 건";
            }
            set { m_DEMTOT = value; }
        }
        private String m_PAYTOT;
        public String PAYTOT
        {
            get
            {
                return MetroLib.StrHelper.ToNumberWithComma(m_PAYTOT) + " 건";
            }
            set { m_PAYTOT = value; }
        }
        private String m_PAYQYTOT;
        public String PAYQYTOT
        {
            get
            {
                return MetroLib.StrHelper.ToNumberWithComma(m_PAYQYTOT) + " 원";
            }
            set { m_PAYQYTOT = value; }
        }
        private String m_BULCNT;
        public String BULCNT
        {
            get
            {
                return MetroLib.StrHelper.ToNumberWithComma(m_BULCNT) + " 건";
            }
            set { m_BULCNT = value; }
        }
        private String m_BULAMT;
        public String BULAMT
        {
            get
            {
                return MetroLib.StrHelper.ToNumberWithComma(m_BULAMT) + " 원";
            }
            set { m_BULAMT = value; }
        }
        public String JJCNT
        {
            get
            {
                long bulcnt = 0;
                long payrsvcnttot = 0;
                long.TryParse(m_BULCNT, out bulcnt);
                long.TryParse(m_PAYRSVCNTTOT, out payrsvcnttot);
                long retvalue = I0203_COUNT - bulcnt - payrsvcnttot;
                return MetroLib.StrHelper.ToNumberWithComma(retvalue.ToString()) + " 건";
            }
        }
        public String JJAMT
        {
            get
            {
                long demtot = 0;
                long payqytot = 0;
                long bulamt = 0;
                long payrsvqytot = 0;
                long.TryParse(m_DEMTOT, out demtot);
                long.TryParse(m_PAYQYTOT, out payqytot);
                long.TryParse(m_BULAMT, out bulamt);
                long.TryParse(m_PAYRSVQYTOT, out payrsvqytot);
                long retvalue = demtot - payqytot - bulamt - payrsvqytot;
                return MetroLib.StrHelper.ToNumberWithComma(retvalue.ToString()) + " 원";
            }
        }
        private String m_INCOMETAX;
        public String INCOMETAX
        {
            get
            {
                return MetroLib.StrHelper.ToNumberWithComma(m_INCOMETAX) + " 원";
            }
            set { m_INCOMETAX = value; }
        }
        private String m_INHABITAX;
        public String INHABITAX
        {
            get
            {
                return MetroLib.StrHelper.ToNumberWithComma(m_INHABITAX) + " 원";
            }
            set { m_INHABITAX = value; }
        }
        private String m_TAXTOT;
        public String TAXTOT
        {
            get
            {
                return MetroLib.StrHelper.ToNumberWithComma(m_TAXTOT) + " 원";
            }
            set { m_TAXTOT = value; }
        }
        private String m_PAYRSVCNTTOT;
        public String PAYRSVCNTTOT
        {
            get
            {
                return MetroLib.StrHelper.ToNumberWithComma(m_PAYRSVCNTTOT) + " 건";
            }
            set { m_PAYRSVCNTTOT = value; }
        }
        private String m_PAYRSVQYTOT;
        public String PAYRSVQYTOT
        {
            get
            {
                return MetroLib.StrHelper.ToNumberWithComma(m_PAYRSVQYTOT) + " 원";
            }
            set { m_PAYRSVQYTOT = value; }
        }
        private String m_PREPAYAMT;
        public String PREPAYAMT
        {
            get
            {
                return MetroLib.StrHelper.ToNumberWithComma(m_PREPAYAMT) + " 원";
            }
            set { m_PREPAYAMT = value; }
        }
        private String m_PAYAMTTOT;
        public String PAYAMTTOT
        {
            get
            {
                return MetroLib.StrHelper.ToNumberWithComma(m_PAYAMTTOT) + " 원";
            }
            set { m_PAYAMTTOT = value; }
        }
        private String m_PREPAYDT;
        public String PREPAYDT
        {
            get
            {
                if (m_PREPAYDT.Length != 8)
                {
                    return m_PREPAYDT;
                }
                else
                {
                    return m_PREPAYDT.Substring(0, 4) + "-" + m_PREPAYDT.Substring(4, 2) + "-" + m_PREPAYDT.Substring(6, 2);
                }
            }
            set { m_PREPAYDT = value; }
        }
        private String m_REALPAYAMT;
        public String REALPAYAMT
        {
            get
            {
                return MetroLib.StrHelper.ToNumberWithComma(m_REALPAYAMT) + " 원";
            }
            set { m_REALPAYAMT = value; }
        }
        public String BUNMEMO { get; set; }
        public String MEMO { get; set; }
        public String BANKCD;
        private String m_BANKNM;
        public String BANKNM
        {
            get
            {
                return m_BANKNM == "" ? BANKCD : m_BANKNM;
            }
            set { m_BANKNM = value; }
        }
        public String ACCOUNT { get; set; }
        private String m_REPDT;
        public String REPDT
        {
            get
            {
                if (m_REPDT.Length != 8)
                {
                    return m_REPDT;
                }
                else
                {
                    return m_REPDT.Substring(0, 4) + "-" + m_REPDT.Substring(4, 2) + "-" + m_REPDT.Substring(6, 2);
                }
            }
            set { m_REPDT = value; }
        }
        public long I0203_COUNT;

        public void Clear()
        {
            BUSSDIVNM = "";
            DEMNO = "";
            SENDNO = "";
            ACCNO = "";
            ACCDT = "";
            HOSID = "";
            HOSNM = "";
            CEONM = "";
            CNTTOT = "";
            DEMTOT = "";
            PAYTOT = "";
            PAYQYTOT = "";
            BULCNT = "";
            BULAMT = "";
            INCOMETAX = "";
            INHABITAX = "";
            TAXTOT = "";
            PAYRSVCNTTOT = "";
            PAYRSVQYTOT = "";
            PREPAYAMT = "";
            PAYAMTTOT = "";
            PREPAYDT = "";
            REALPAYAMT = "";
            BUNMEMO = "";
            MEMO = "";
            BANKCD = "";
            BANKNM = "";
            ACCOUNT = "";
            REPDT = "";
            I0203_COUNT = 0;
        }
    }
}
