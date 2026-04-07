using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataERR001
    {
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;
        public string DRID;
        public string DRNM;
        public string ODT;
        public string OTM;
        public string PHTDT; // 검사일자
        public string PHTTM; // 검사시간
        public string RPTDT; // 판독일자
        public string RPTTM; // 판독시간
        public string RDRID; // 판독의ID
        public string RDRNM; // 판독의명
        public string GDRLCID; // 판독의면허번호
        public string OCD; // 처방코드
        public string ONM; // 처방명
        public string EDICD { get { return "00"; } } // EDI코드
        public string RPTXT; // 판독결과

        public string DEPT_INFO { get { return DPTCD + "(" + INSDPTCD + INSDPTCD2 + ")"; } }
        public string ODTM { get { return ODT + OTM; } }
        public string PHTDTM { get { return PHTDT + PHTTM; } }
        public string RPTDTM { get { return RPTDT + RPTTM; } }

        public void Clear()
        {
            DPTCD = "";
            INSDPTCD = "";
            INSDPTCD2 = "";
            DRID = "";
            DRNM = "";
            ODT = "";
            OTM = "";
            PHTDT = ""; // 검사일자
            PHTTM = ""; // 검사시간
            RPTDT = ""; // 판독일자
            RPTTM = ""; // 판독시간
            RDRID = ""; // 판독의ID
            RDRNM = ""; // 판독의명
            GDRLCID = ""; // 판독의면허번호
            OCD = ""; // 처방코드
            ONM = ""; // 처방명
            RPTXT = ""; // 판독결과
        }
    }
}
