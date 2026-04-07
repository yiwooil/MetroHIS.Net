using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HIRA.EformEntry;
using HIRA.EformEntry.Model;
using HIRA.EformEntry.ResponseModel;

namespace ADD7000E
{
    class CDataTI2A
    {
        private String m_EPRTNO;
        private String m_CNECNO; // 접수번호
        private String m_RCV_YR; // 접수년도
        private String m_DCOUNT; // 청구서일련번호

        public String DEMNO { get; set; }
        public String EPRTNO { 
            get{
                String strTmp = "00000" + m_EPRTNO;
                return strTmp.Substring(strTmp.Length - 5);
            } 
            set
            {
                m_EPRTNO = value;
            } 
        }
        public String PID { get; set; }
        public String PNM { get; set; }
        public String DPTCD {
            get{
                String[] arr = JRKWA.Split('$');
                return arr[2];
            } 
        }
        public String QFYCD { get; set; }
        public String GONSGB { get; set; }
        public String ADDZ1 { get; set; }
        public String CHECK_ORM001 { get; set; }
        public String CHECK_RID001 { get; set; }
        public String CHECK_OCQ001 { get; set; }
        public String BDODT;
        public String JRBY;
        public String UNISQ;
        public String SIMCS;
        public String JRKWA;
        public String RESID;
        public String BDEDT;
        public String TT41KEY;
        public String HDATE;
        public String INSUP_TP_CD
        {
            get
            {
                String strRet = "";
                if (QFYCD.StartsWith("2")) strRet = "4";//건강보험
                else if (QFYCD.StartsWith("3")) strRet = "5";//의료급여
                else if (QFYCD.StartsWith("6")) strRet = "8";//자동차보험
                //if ("4".Equals(GONSGB) || "7".Equals(GONSGB)) strRet = "7";//보훈
                if ("29".Equals(QFYCD)) strRet = "7";// 보훈일반
                return strRet;
            }
        }
        public String RCV_YR
        {
            // 접수년도 CCYY
            set
            {
                m_RCV_YR = value;
            }
            get
            {
                String strRet = ("".Equals(m_RCV_YR) ? DEMNO.Substring(0, 4) : m_RCV_YR);
                return strRet;
            }
        }
        public String CNECNO
        {
            set
            {
                m_CNECNO = value;
            }
            get
            {
                return ("".Equals(m_CNECNO.Replace(" ","")) ? "0000000" : m_CNECNO);
            }
        }
        public String DCOUNT
        {
            set
            {
                m_DCOUNT = value;
            }
            get
            {
                String strRet = "";
                if ("".Equals(m_CNECNO.Replace(" ","")))
                {
                    strRet = "0"; // 접수전
                }
                else if ("".Equals(ADDZ1) || "0".Equals(ADDZ1) || "2".Equals(ADDZ1) || "3".Equals(ADDZ1))
                {
                    strRet = "1"; // 원청구
                }
                else
                {
                    strRet = m_DCOUNT.Trim(); // 보완청구
                }
                return strRet;
            }
        }

        public Document doc_ORM001;
        public Document doc_RID001;
        public Document doc_OCQ001;

        public String error_ORM001;
        public String error_RID001;
        public String error_OCQ001;

        public String json_ORM001;
        public String json_RID001;
        public String json_OCQ001;
    }
}
