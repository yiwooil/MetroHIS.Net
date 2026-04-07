using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RNH001_Rcd
    {
        public string WDATE; // 일자
		public string ILL; // 가호기록
		public string EID; // 간호사
        public string ENM;

        // ---

        public string RCD_DT // 기록일시
        {
            get { return WDATE + "0000"; }
        }
        public string RCD_TXT // 기록
        {
            get { return ILL; }
        }
        public string NURSE_NM // 간호사
        {
            get { return ENM; }
        }
    }
}
