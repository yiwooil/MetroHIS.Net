using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RAR001_Rcov
    {
        public string WDATE;
        public string WTIME;
        public string Q1_POINT;
        public string Q2_POINT;
        public string Q3_POINT;
        public string Q4_POINT;
        public string Q5_POINT;
        public string TOTAL;
        // 

        public string MASR_DT // 측정일시
        {
            get { return (WDATE + WTIME) == "" ? "-" : (WDATE + WTIME); }
        }
        public string ACTV_PNT // 활동성
        {
            get { return Q1_POINT == "" ? "0" : Q1_POINT; }
        }
        public string BRT_PNT // 호흡
        {
            get { return Q2_POINT == "" ? "0" : Q2_POINT; }
        }
        public string CRCL_PNT // 순환
        {
            get { return Q3_POINT == "" ? "0" : Q3_POINT; }
        }
        public string CNSCS_PNT // 의식
        {
            get { return Q4_POINT == "" ? "0" : Q4_POINT; }
        }
        public string SKN_COLR_PNT // 피부색
        {
            get { return Q5_POINT == "" ? "0" : Q5_POINT; }
        }
        public string TOT_PNT // 합계
        {
            get { return TOTAL == "" ? "0" : TOTAL; }
        }
        
    }
}
