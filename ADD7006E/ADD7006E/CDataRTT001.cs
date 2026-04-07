using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRTT001
    {
        // TU01
        public string OPSDT; // 시술시작일자
        public string OPSHR; // 시술시작시간
        public string OPSMN; // 시술시작분
        public string OPSDTM { get { return OPSDT + OPSHR + OPSMN; } }
        public string OPEDT; // 시술종료일자
        public string OPEHR; // 시술종료시간
        public string OPEMN; // 시술종료분
        public string OPEDTM { get { return OPEDT + OPEHR + OPEMN; } }
        public string DR_GUBUN; // 시술의사구분(1.집도의 2.보조의)
        public string DPTCD; // 진료과
        public string INSDPTCD; // 진료과목
        public string INSDPTCD2; // 내과세부진료과목
        public string DEPT_INFO { get { return DPTCD + "(" + INSDPTCD + INSDPTCD2 + ")"; } }
        public string DRID; // 진료의사
        public string DRNM; // 진료의사명

        // TU02
        public List<string> ONM = new List<string>(); // 처방명(시술명)
        public List<string> ISPCD = new List<string>(); // 수가코드(EDI코드)

        // TU90
        public string EMPID; // 적성자
        public string EMPNM; // 작성자명
        public string WDATE; // 작성일자
        public string WTIME; // 작성시간
        public string WDTM { get { return WDATE + WTIME; } }
        public string PREDX; // 시술전진단
        public string POSDX; // 수술후진단
        public string SURFNDNPRO; // 시술절차
        public string RMKONAPP; // 특이사항

        public void Clear()
        {
            // TU01
            OPSDT = ""; // 시술시작일자
            OPSHR = ""; // 시술시작시간
            OPSMN = ""; // 시술시작분
            OPEDT = ""; // 시술종료일자
            OPEHR = ""; // 시술종료시간
            OPEMN = ""; // 시술종료분
            DR_GUBUN = ""; // 시술의사구분(1.집도의 2.보조의)
            DPTCD = ""; // 진료과
            INSDPTCD = ""; // 진료과목
            INSDPTCD2 = ""; // 내과세부진료과목
            DRID = ""; // 진료의사
            DRNM = ""; // 진료의사명

            // TU02
            ONM.Clear(); // 처방명(시술명)
            ISPCD.Clear(); // 수가코드(EDI코드)

            // TU90
            EMPID = ""; // 적성자
            EMPNM = ""; // 작성자명
            WDATE = ""; // 작성일자
            WTIME = ""; // 작성시간
            PREDX = ""; // 시술전진단
            POSDX = ""; // 수술후진단
            SURFNDNPRO = ""; // 시술절차
            RMKONAPP = ""; // 특이사항
        }

    }
}
