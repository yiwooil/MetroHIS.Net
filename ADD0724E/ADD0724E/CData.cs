using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0724E
{
    class CData
    {
        public string JSDEMSEQ { get; set; }
        public string JSREDAY { get; set; }
        public long JSTOTAMT { get; set; }
        public string CNECNO { get; set; }
        public string DEMSEQ { get; set; }
        public string DEMNO { get; set; }
        public string F1_DOCUNO { get; set; }
        public string F2_DOCUNO { get; set; }
        public string DOCUNO
        {
            get { return F1_DOCUNO + F2_DOCUNO; }
        }
        public string F1_DOCUDT { get; set; }
        public string F2_DOCUDT { get; set; }
        public string DOCUDT
        {
            get { return F1_DOCUDT + F2_DOCUDT; }
        }
        public string F1_JSREDPT1;
        public string F1_JSREDPNM;
        public string F1_JSRETELE;
        public string F2_JSREDPT1;
        public string F2_JSREDPNM;
        public string F2_JSRETELE;
        public string JSREDEPT
        {
            get { return F1_JSREDPT1 + F1_JSREDPNM + F1_JSRETELE + F2_JSREDPT1 + F2_JSREDPNM + F2_JSRETELE; }
        }
        public string JSSAU { get; set; }
        public string MEMO { get; set; }
        public string REMARK
        {
            get
            {
                return JSSAU + MEMO;
                //string jssau_memo = JSSAU + MEMO;
                //string ret = "";
                //string line_text = "";
                //int text_len = jssau_memo.Length;
                //for (int i = 0; i < text_len; i++)
                //{
                //    string str1 = jssau_memo.Substring(i, 1);
                //    if (MetroLib.StrHelper.LengthH(line_text + str1) > 165)
                //    {
                //        ret += (ret != "" ? Environment.NewLine : "") + line_text;
                //        line_text = "";
                //    }
                //    line_text += str1;
                //}
                //if (line_text != "") ret += (ret != "" ? Environment.NewLine : "") + line_text;
                //return ret;
            }
        }
        public string IOFG { get; set; }
        public string IOFGNM
        {
            get
            {
                string ret = "";
                if (IOFG == "1") ret = "외래";
                else if (IOFG == "2") ret = "입원";
                else if (IOFG == "3") ret = "정신과외래";
                else if (IOFG == "4") ret = "정신과입원";
                else if (IOFG == "5") ret = "정신과낮";
                else ret = "기타";
                return ret;
            }
        }
        public string FMGBN { get; set; }
        public string JRFG { get; set; }
        public string VERSION { get; set; }
        public string DCOUNT { get; set; }
        public string HOSID { get; set; }
        public string HOSNM { get; set; }
        public string JIWONCD { get; set; }
        public string JSYYSEQ { get; set; }
        public string DEADLINE
        {
            get
            {
                string ret = "";
                DateTime dtReday;
                if (DateTime.TryParseExact(JSREDAY, "yyyyMMdd",null,System.Globalization.DateTimeStyles.None, out dtReday) == true)
                {
                    ret = dtReday.AddDays(89).ToString("yyyyMMdd");
                }
                return ret;
            }
        }

        public void Clear()
        {
            JSDEMSEQ = "";
            JSREDAY = "";
            JSTOTAMT = 0;
            CNECNO = "";
            DEMSEQ = "";
            DEMNO = "";
            F1_DOCUNO = "";
            F2_DOCUNO = "";
            F1_DOCUDT = "";
            F2_DOCUDT = "";
            F1_JSREDPT1 = "";
            F1_JSREDPNM = "";
            F1_JSRETELE = "";
            F2_JSREDPT1 = "";
            F2_JSREDPNM = "";
            F2_JSRETELE = "";
            JSSAU = "";
            MEMO = "";
            IOFG = "";
            FMGBN = "";
            JRFG = "";
            VERSION = "";
            DCOUNT = "";
            HOSID = "";
            HOSNM = "";
            JIWONCD = "";
            JSYYSEQ = "";
        }
    }
}
