using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD8002Q
{
    class CDataSub
    {
        private string m_FTDAYS;

        public string PNM { get; set; }
        public string EPRTNO { get; set; }
        public string JRGB { get; set; }
        public string GENDT { get; set; }
        public string FTDAYS
        {
            get
            {
                if (m_FTDAYS.Length <= 8)
                {
                    return m_FTDAYS;
                }
                else
                {
                    return m_FTDAYS.Substring(0, 8) + "-" + m_FTDAYS.Substring(8);
                }
            }
            set { m_FTDAYS = value; }

        }
        public long UNAMT { get; set; }
        public long JJAMT { get; set; }
        public long PAYAMT { get; set; }
        public long BULAMT { get; set; }
        public long BOAMT { get; set; }
        public long ORDDAYS { get; set; }
        public string BULRMK;
        public string MEMO;
        public string BULRMK_MEMO
        {
            get
            {
                string ret = "";
                if (BULRMK != "")
                {
                    ret = "불능사유:" + BULRMK;
                }
                if (MEMO != "")
                {
                    if (ret != "") ret += Environment.NewLine;
                    ret += "비고:" + MEMO;
                }
                return ret;
            }
        }

        public void Clear()
        {
            PNM = "";
            EPRTNO = "";
            JRGB = "";
            GENDT = "";
            FTDAYS = "";
            UNAMT = 0;
            JJAMT = 0;
            PAYAMT = 0;
            BULAMT = 0;
            BOAMT = 0;
            ORDDAYS = 0;
            BULRMK = "";
            MEMO = "";
        }
    }
}
