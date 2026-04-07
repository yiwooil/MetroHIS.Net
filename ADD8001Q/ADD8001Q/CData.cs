using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD8001Q
{
    class CData
    {
        public String ACCDIV { get; set; }
        public String DEMNO { get; set; }
        public String ACCBACKDIV;
        public String FMNO { get; set; }
        public String HOSID { get; set; }
        public String REPDT { get; set; }
        public String ACCNO { get; set; }
        private String m_DEMCNT;
        public String DEMCNT
        {
            get { return MetroLib.StrHelper.ToNumberWithComma(m_DEMCNT); }
            set { m_DEMCNT = value; }
        }
        private String m_DEMAMT;
        public String DEMAMT
        {
            get { return MetroLib.StrHelper.ToNumberWithComma(m_DEMAMT); }
            set { m_DEMAMT = value; }
        }
        public String YYMM { get; set; }
        public String BUSSDIV;
        public String RSNCD { get; set; }
        public String MEMO { get; set; }
        public String ISI020 { get; set; }
        public String ISI030 { get; set; }
        public String DEMGB;

        public String ACCBACKDIVNM
        {
            get
            {
                if (ACCBACKDIV == "1")
                    return "접수";
                else if (ACCBACKDIV == "2")
                    return "반송";
                else if (ACCBACKDIV == "3")
                    return "반려";
                else
                    return ACCBACKDIV;
            }
        }

        public String BUSSNM
        {
            get
            {
                if (BUSSDIV == "1")
                    return "외래";
                else
                    return "입원";
            }
        }

        public String ETCFG
        {
            get
            {
                if (DEMNO.Length > 4 && DEMNO.Substring(4, 1) == "A")
                    return "후유";
                else
                    return "";
            }
        }

        public String DEMGBNM
        {
            get
            {
                if (DEMGB == "0")
                    return "원";
                else if (DEMGB == "1")
                    return "보완";
                else if (DEMGB == "2")
                    return "추가";
                else
                    return DEMGB;
            }
        }

        public void Clear()
        {
            ACCDIV = "";
            DEMNO = "";
            ACCBACKDIV = "";
            FMNO = "";
            HOSID = "";
            REPDT = "";
            ACCNO = "";
            DEMCNT = "";
            DEMAMT = "";
            YYMM = "";
            BUSSDIV = "";
            RSNCD = "";
            MEMO = "";
            ISI020 = "";
            ISI030 = "";
            DEMGB = "";
        }
    }
}
