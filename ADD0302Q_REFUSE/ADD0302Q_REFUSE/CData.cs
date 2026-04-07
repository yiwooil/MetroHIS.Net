using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0302Q_REFUSE
{
    class CData
    {
        public bool OP { get; set; }
        public string REQMM { get; set; }
        public string PID { get; set; }
        public string PNM { get; set; }
        public string RESID;
        public string RESID_MASK
        {
            get
            {
                string ret = "";
                if (RESID.Length < 7)
                {
                    ret = RESID;
                }
                else
                {
                    ret = RESID.Substring(0, 6) + "-" + RESID.Substring(6, 1) + "******";
                }
                return ret;
            }
        }
        public string DONFG;
        public string DONFG_NM
        {
            get
            {
                string ret = DONFG;
                if (DONFG == "") ret = "기초";
                else if (DONFG == "N") ret = "미완";
                else if (DONFG == "P") ret = "보류";
                else if (DONFG == "Y") ret = "완료";
                return ret;
            }
        }
        public string SIMNO { get; set; }
        public string EPRTNO { get; set; }
        public string STEDT { get; set; }
        public string QFYCD { get; set; }
        public string DPTCD { get; set; }
        public string GONSGB { get; set; }
        public string DAETC { get; set; }
        public string TJKH { get; set; }
        public string RMPID { get; set; }
        public string RMPDT { get; set; }
        public string RMPTM { get; set; }
        public string RSN { get; set; }
        public string K1 { get; set; }
        public string K2 { get; set; }
        public string K3 { get; set; }
        public string K4 { get; set; }
        public string K5 { get; set; }
        public string K6 { get; set; }
        public string DELID { get; set; }
        public string SIMFG { get; set; }

        public void Clear()
        {
            OP = false;
            REQMM = "";
            PID = "";
            PNM = "";
            RESID = "";
            DONFG = "";
            SIMNO = "";
            EPRTNO = "";
            STEDT = "";
            QFYCD = "";
            DPTCD = "";
            GONSGB = "";
            DAETC = "";
            TJKH = "";
            RMPID = "";
            RMPDT = "";
            RMPTM = "";
            RSN = "";
            K1 = "";
            K2 = "";
            K3 = "";
            K4 = "";
            K5 = "";
            K6 = "";
            DELID = "";
            SIMFG = "";
        }
    }
}
