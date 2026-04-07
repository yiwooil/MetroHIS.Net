using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0712E_JABO
{
    class CDataSak
    {
        private double m_GSRT;
        private string m_BGIHO;
        private string m_DANGA;
        private string m_JJGUMAK;

        public string EPRTNO { get; set; }
        public string PID { get; set; }
        public string A_PNM { get; set; }
        public string N2_PNM { get; set; }
        public string DPTCD { get; set; }
        public string DRNM { get; set; }
        public string MDACD { get; set; }
        public string LNO { get; set; }
        public string JJRMK1 { get; set; }
        public string JJRMK2 { get; set; }
        public string JJDETAIL { get; set; }
        public string JJTEXT { get; set; }
        public string F_BGIHO { get; set; }
        public string PRKNM { get; set; }
        public long F_DANGA { get; set; }
        public string F_CNTQTY { get; set; }
        public string F_DQTY { get; set; }
        public long F_DDAY { get; set; }
        public long F_GUMAK { get; set; }
        public string MAFG { get; set; }
        public double GSRT
        {
            get { return MAFG == "1" ? 1 : 1 + m_GSRT / 100; }
            set { m_GSRT = value; }
        }
        public string BGIHO
        {
            get { return F_BGIHO == m_BGIHO ? "" : m_BGIHO; }
            set { m_BGIHO = value; }
        }
        public string DANGA
        {
            get
            {
                if (m_DANGA == "")
                {
                    return "";

                }
                else
                {
                    long danga = 0;
                    long.TryParse(m_DANGA, out danga);
                    return F_DANGA == danga ? "" : String.Format("{0:#,###}", danga);
                }
            }
            set
            {
                if (value == "") m_DANGA = "";
                else m_DANGA = Convert.ToInt64(Convert.ToDouble(value)).ToString();
            }
        }
        public string CNTQTY { get; set; }
        public string DQTY { get; set; }
        public string DDAY { get; set; }
        public string JJGUMAK
        {
            get
            {
                if (m_JJGUMAK == "")
                {
                    return "";
                }
                else
                {
                    long jjgumak = 0;
                    long.TryParse(m_JJGUMAK, out jjgumak);
                    return String.Format("{0:#,###}", jjgumak);
                }
            }
            set { m_JJGUMAK = value; }
        }
        public string DEMNO { get; set; }
        public string REDAY { get; set; }
        public string JJRMKNM { get; set; }

        public string PNM
        {
            get { return (A_PNM == "" ? N2_PNM : A_PNM); }
        }
        public string DOCTOR
        {
            get { return DPTCD + "-" + DRNM; }
        }
        public string JJRMK
        {
            get { return JJRMK1 + JJRMK2 + JJDETAIL; }
        }
        public string BIGO
        {
            get
            {
                string ret = "";
                if (JJRMKNM != "" && JJTEXT != "") ret = JJRMKNM + "/" + JJTEXT;
                else if (JJRMKNM != "") ret = JJRMKNM;
                else if (JJTEXT != "") ret = JJTEXT;
                return ret;
            }
        }
        public long F_GUMAK_GA
        {
            get { return Convert.ToInt64(F_GUMAK * GSRT); }
        }
        public string JJGUMAK_GA
        {
            get
            {
                if (m_JJGUMAK == "")
                {
                    return "";
                }
                else
                {
                    long jjgumak = 0;
                    long.TryParse(m_JJGUMAK, out jjgumak);
                    long jjgumak_ga = Convert.ToInt64(jjgumak * GSRT);
                    return String.Format("{0:#,###}", jjgumak_ga);
                }
            }
        }

        public void Clear()
        {
            EPRTNO = "";
            PID = "";
            A_PNM = "";
            N2_PNM = "";
            DPTCD = "";
            DRNM = "";
            MDACD = "";
            LNO = "";
            JJRMK1 = "";
            JJRMK2 = "";
            JJDETAIL = "";
            JJTEXT = "";
            F_BGIHO = "";
            PRKNM = "";
            F_DANGA = 0;
            F_CNTQTY = "";
            F_DQTY = "";
            F_DDAY = 0;
            F_GUMAK = 0;
            MAFG = "";
            GSRT = 0;
            BGIHO = "";
            DANGA = "";
            CNTQTY = "";
            DQTY = "";
            DDAY = "";
            JJGUMAK = "";
            DEMNO = "";
            REDAY = "";
            JJRMKNM = "";
        }
    }
}
