using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD_DRG
{
    class CData
    {
        public string PID { get; set; }
        public string PNM { get; set; }
        public string QFYCD { get; set; }
        public string RESID { get; set; }
        public string STEDT { get; set; }
        public string EXAMC { get; set; }
        public string RSLT { get; set; }
        public string DACD1 { get; set; }
        public string DACD2 { get; set; }
        public string DACD3 { get; set; }
        public string DACD4 { get; set; }
        public string DACD5 { get; set; }
        public string DACD6 { get; set; }
        public string DACD7 { get; set; }
        public string DACD8 { get; set; }
        public string DACD9 { get; set; }
        public string DACD10 { get; set; }
        public string OPR1 { get; set; }
        public string OPR2 { get; set; }
        public string OPR3 { get; set; }
        public string OPR4 { get; set; }
        public string OPR5 { get; set; }
        public string OPR6 { get; set; }
        public string OPR7 { get; set; }
        public string OPR8 { get; set; }
        public string OPR9 { get; set; }
        public string OPR10 { get; set; }
        public string EXAM1 { get; set; }
        public string EXAM2 { get; set; }
        public string EXAM3 { get; set; }
        public string EXAM4 { get; set; }
        public string EXAM5 { get; set; }
        public string XRAY1 { get; set; }
        public string XRAY2 { get; set; }
        public string XRAY3 { get; set; }
        public string XRAY4 { get; set; }
        public string XRAY5 { get; set; }
        public string INJ1 { get; set; }
        public string INJ2 { get; set; }
        public string INJ3 { get; set; }
        public string INJ4 { get; set; }
        public string INJ5 { get; set; }
        public string ANE1 { get; set; }
        public string ANE2 { get; set; }
        public string ANE3 { get; set; }
        public string ANE4 { get; set; }
        public string ANE5 { get; set; }
        public string ALCOL { get; set; }
        public string ADD1 { get; set; }
        public string ADD2 { get; set; }
        public string ADD3 { get; set; }
        public string ADD4 { get; set; }
        public string ADD5 { get; set; }
        public string WEIGHT { get; set; }
        public string BREATH { get; set; }
        public string MDC { get; set; }
        public string ADRG { get; set; }
        public string PCCL { get; set; }
        public string DRGNO { get; set; }
        public string VERSION { get; set; }
        public string REMARK { get; set; }
        //
        public string BDODT;
        public string JRBY;
        public int UNISQ;
        public int SIMCS;

        public void Clear()
        {
            PID = "";
            PNM = "";
            QFYCD = "";
            RESID = "";
            STEDT = "";
            EXAMC = "";
            RSLT = "";
            DACD1 = "";
            DACD2 = "";
            DACD3 = "";
            DACD4 = "";
            DACD5 = "";
            DACD6 = "";
            DACD7 = "";
            DACD8 = "";
            DACD9 = "";
            DACD10 = "";
            OPR1 = "";
            OPR2 = "";
            OPR3 = "";
            OPR4 = "";
            OPR5 = "";
            OPR6 = "";
            OPR7 = "";
            OPR8 = "";
            OPR9 = "";
            OPR10 = "";
            EXAM1 = "";
            EXAM2 = "";
            EXAM3 = "";
            EXAM4 = "";
            EXAM5 = "";
            XRAY1 = "";
            XRAY2 = "";
            XRAY3 = "";
            XRAY4 = "";
            XRAY5 = "";
            INJ1 = "";
            INJ2 = "";
            INJ3 = "";
            INJ4 = "";
            INJ5 = "";
            ANE1 = "";
            ANE2 = "";
            ANE3 = "";
            ANE4 = "";
            ANE5 = "";
            ALCOL = "";
            ADD1 = "";
            ADD2 = "";
            ADD3 = "";
            ADD4 = "";
            ADD5 = "";
            WEIGHT = "";
            BREATH = "";
            MDC = "";
            ADRG = "";
            PCCL = "";
            DRGNO = "";
            VERSION = "";
            REMARK = "";

            BDODT = "";
            JRBY = "";
            UNISQ = 0;
            SIMCS = 0;
        }
    }
}
