using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0712E_JABO
{
    class CDataComb
    {
        private string m_LNO;
        private string m_F_PRICD;
        private string m_F_BGIHO;
        private string m_F_PRKNM;
        private string m_F_DQTY;
        private string m_F_DDAY;
        private string m_F_GUMAK;
        private string m_BGIHO;
        private string m_DQTY;
        private string m_DDAY;
        private string m_JJGUMAK;
        private string m_JJGAGUMAK;
        private string m_UPLMTAMT;
        private string m_JJUPLMTCHAAMT;

        public string EPRTNO { get; set; }
        public string PID;
        public string PNM;
        public string RESID;
        public string DPTCD;
        public string TTTAMT;
        public string UNAMT;
        public string JBPTAMT;
        public string RQUPLMTCHATTAMT;
        public string ACTGUM;
        public string AJBPTAMT;
        public string SKUPLMTCHATTAMT;
        public string ASTAMT;
        public string SAKGAMAMT;
        public string SAKGAMAMT2;
        public string MEMO;
        public string RETEAM { get; set; }
        public string DEMNO { get; set; }

        public string LNO
        {
            get { return DATA_FG == "1" ? PID : m_LNO; }
            set { m_LNO = value; }
        }
        public string F_PRICD
        {
            get { return DATA_FG == "1" ? PNM : m_F_PRICD; }
            set { m_F_PRICD = value; }
        }
        public string F_BGIHO
        {
            get { return DATA_FG == "1" ? RESID : m_F_BGIHO; }
            set { m_F_BGIHO = value; }
        }
        public string F_PRKNM
        {
            get { return DATA_FG == "1" ? DPTCD : m_F_PRKNM; }
            set { m_F_PRKNM = value; }
        }
        public string F_DQTY
        {
            get { return DATA_FG == "1" ? TTTAMT : m_F_DQTY; }
            set { m_F_DQTY = value; }
        }
        public string F_DDAY
        {
            get { return DATA_FG == "1" ? UNAMT : m_F_DDAY; }
            set { m_F_DDAY = value; }
        }
        public string F_GUMAK
        {
            get { return DATA_FG == "1" ? JBPTAMT : m_F_GUMAK; }
            set { m_F_GUMAK = value; }
        }
        public string BGIHO
        {
            get { return DATA_FG == "1" ? RQUPLMTCHATTAMT : m_BGIHO; }
            set { m_BGIHO = value; }
        }
        public string DQTY
        {
            get { return DATA_FG == "1" ? ACTGUM : m_DQTY; }
            set { m_DQTY = value; }
        }
        public string DDAY
        {
            get { return DATA_FG == "1" ? AJBPTAMT : m_DDAY; }
            set { m_DDAY = value; }
        }
        public string JJGUMAK
        {
            get { return DATA_FG == "1" ? SKUPLMTCHATTAMT : m_JJGUMAK; }
            set { m_JJGUMAK = value; }
        }
        public string JJGAGUMAK
        {
            get { return DATA_FG == "1" ? ASTAMT : m_JJGAGUMAK; }
            set { m_JJGAGUMAK = value; }
        }
        public string UPLMTAMT
        {
            get { return DATA_FG == "1" ? SAKGAMAMT : m_UPLMTAMT; }
            set { m_UPLMTAMT = value; }
        }
        public string JJUPLMTCHAAMT
        {
            get { return DATA_FG == "1" ? SAKGAMAMT2 : m_JJUPLMTCHAAMT; }
            set { m_JJUPLMTCHAAMT = value; }
        }
        public string JJRMK1;
        public string JJRMK2;
        public string JJDETAIL;
        public string JJTEXT;
        public string JJRMK
        {
            get
            {
                string ret = "";
                int result = 0;
                if (int.TryParse(JJRMK1, out result))
                {
                    ret = JJRMK1 + JJDETAIL;
                }
                else
                {
                    ret = JJRMK1 + JJRMK2;
                }
                return ret;
            }
        }
        public string JJRMKDOC;
        public string DATA_FG { get; set; }
        public string REMARK
        {
            get
            {
                string ret = "";
                if (DATA_FG == "1")
                {
                    ret = MEMO;
                }
                else
                {
                    if (JJRMK != "" && JJRMKDOC != "") ret = JJRMK + "." + JJRMKDOC;
                    else if (JJRMK != "") ret = JJRMK;
                    else if (JJRMKDOC != "") ret = JJRMKDOC;
                    if (JJTEXT != "")
                    {
                        if (ret != "") ret += "/" + JJTEXT;
                        else ret = JJTEXT;
                    }
                }
                return ret;
            }
        }

        public void Clear()
        {
            EPRTNO = "";
            PNM = "";
            RESID = "";
            DPTCD = "";
            TTTAMT = "";
            UNAMT = "";
            JBPTAMT = "";
            RQUPLMTCHATTAMT = "";
            ACTGUM = "";
            AJBPTAMT = "";
            SKUPLMTCHATTAMT = "";
            ASTAMT = "";
            SAKGAMAMT = "";
            SAKGAMAMT2 = "";
            MEMO = "";
            RETEAM = "";

            m_LNO = "";
            m_F_PRICD = "";
            m_F_BGIHO = "";
            m_F_PRKNM = "";
            m_F_DQTY = "";
            m_F_DDAY = "";
            m_F_GUMAK = "";
            m_BGIHO = "";
            m_DQTY = "";
            m_DDAY = "";
            m_JJGUMAK = "";
            m_JJGAGUMAK = "";
            m_UPLMTAMT = "";
            m_JJUPLMTCHAAMT = "";
            JJRMK1 = "";
            JJRMK2 = "";
            JJDETAIL = "";
            JJTEXT = "";
            JJRMKDOC = "";
            DATA_FG = "";
        }
    }
}
