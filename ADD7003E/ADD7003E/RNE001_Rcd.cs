using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RNE001_Rcd
    {

        public string WDATE;
        public string WTIME;
        public string RESULT;
        public string RESULT_ORG;
        public string PNURES;
        public string EMPNM;

        // -----

        public string RCD_DT // 기록일시
        {
            get { return WDATE + WTIME; }
        }
        public string RCD_TXT // 간호기록
        {
            get { return RESULT; }
        }
        public string RCD_TXT_ORG
        {
            get { return RESULT_ORG; }
        }
        public string NURSE_NM // 간호사성명
        {
            get { return EMPNM; }
        }

        // ---

        public void Clear()
        {
            WDATE = "";
            WTIME = "";
            RESULT = "";
            RESULT_ORG = "";
            PNURES = "";
            EMPNM = "";
        }
    }
}
