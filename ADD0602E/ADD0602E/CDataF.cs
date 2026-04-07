using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0602E
{
    class CDataF
    {
        private string m_CNTQTY;
        private string m_DQTY;
        private string m_GASAN;
        private string m_GUMAK;
        private string m_SAKAMT;
        private string m_OBJAMT;

        public string LNO { get; set; }
        public string HANGNO { get; set; }
        public string MOKNO { get; set; }
        public string PRICD { get; set; }
        public string BGIHO { get; set; }
        public string PRKNM { get; set; }
        public string DANGA { get; set; }
        public string CNTQTY
        {
            set { m_CNTQTY = TrimRightZero(value); }
            get { return m_CNTQTY; }
        }
        public string DQTY
        {
            set { m_DQTY = TrimRightZero(value); }
            get { return m_DQTY; }
        }
        public string DDAY { get; set; }
        public string GASAN
        {
            set { m_GASAN = TrimRightZero(value); }
            get { return m_GASAN; }
        }
        public string GUMAK
        {
            set { m_GUMAK = TrimRightZero(value); }
            get { return m_GUMAK; }
        }
        public string SAKAMT
        {
            set { m_SAKAMT = TrimRightZero(value); }
            get { return m_SAKAMT; }
        }
        public string OBJAMT
        {
            set { m_OBJAMT = TrimRightZero(value); }
            get { return m_OBJAMT; }
        }
        public string JJRMK { get; set; }
        public string JJBGIHO { get; set; }
        public string OUTSEQ { get; set; }
        public string JJDETAIL { get; set; }
        public string GUBUN { get; set; }
        public string JJTEXT { get; set; }
        public string JJRMKNM { get; set; }

        public void Clear()
        {
            LNO = "";
            HANGNO = "";
            MOKNO = "";
            PRICD = "";
            BGIHO = "";
            PRKNM = "";
            DANGA = "";
            CNTQTY = "";
            DQTY = "";
            DDAY = "";
            m_GASAN = "";
            GUMAK = "";
            SAKAMT = "";
            OBJAMT = "";
            JJRMK = "";
            JJBGIHO = "";
            OUTSEQ = "";
            JJDETAIL = "";
            GUBUN = "";
            JJTEXT = "";
            JJRMKNM = "";
        }

        private string TrimRightZero(string p_value)
        {
            string ret = p_value;
            string[] a_val = (p_value + ".").Split('.');
            string l_value = a_val[0];
            string r_value = a_val[1];
            r_value = r_value.TrimEnd('0');
            if (r_value == "")
            {
                ret = l_value;
            }
            else
            {
                ret = l_value + "." + r_value;
            }
            return ret;
        }
    }
}
