using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RNO001_Rcd
    {
        public string WDATE; // 기록일자
        public string WTIME; // 기록시간
        public string RESULT; // 간호기록
        public string PNURES; // 간호호사
        public string EMPNM;
        public string RESULT_ORG;
        //

        public string RCD_DT // 기록일시
        {
            get { return WDATE + WTIME; }
        }
        public string RCD_TXT // 간호기록
        {
            get { return RESULT; }
        }
        public string RCD_TXT_ORG // 간호기록
        {
            get { return RESULT_ORG; }
        }
        public string NURSE_NM // 간호사
        {
            get { return EMPNM; }
        }
    }
}
