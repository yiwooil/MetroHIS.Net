using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRSS001
    {
        // 병변의위치,수술소견 없음.

        // TU01
        public string OPSDT;
        public string OPSHR;
        public string OPSMN;
        public string OPEDT;
        public string OPEHR;
        public string OPEMN;
        // 집도의
        public string DR_GUBUN; // 1.집도의 2.보조의
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;
        public string DRID;
        public string DRNM;
        public string GDRLCID; // 수술의면허번호
        public string DEPT_INFO { get { return DPTCD + "(" + INSDPTCD + INSDPTCD2 + ")"; } }
        // 보조의1
        public string DR_GUBUN_SUB1; // 1.집도의 2.보조의
        public string DPTCD_SUB1;
        public string INSDPTCD_SUB1;
        public string INSDPTCD2_SUB1;
        public string DRID_SUB1;
        public string DRNM_SUB1;
        public string GDRLCID_SUB1; // 수술의면허번호
        public string DEPT_INFO_SUB1 { get { return DPTCD_SUB1 + "(" + INSDPTCD_SUB1 + INSDPTCD2_SUB1 + ")"; } }
        // 보조의2
        public string DR_GUBUN_SUB2; // 1.집도의 2.보조의
        public string DPTCD_SUB2;
        public string INSDPTCD_SUB2;
        public string INSDPTCD2_SUB2;
        public string DRID_SUB2;
        public string DRNM_SUB2;
        public string GDRLCID_SUB2; // 수술의면허번호
        public string DEPT_INFO_SUB2 { get { return DPTCD_SUB2 + "(" + INSDPTCD_SUB2 + INSDPTCD2_SUB2 + ")"; } }
        // 보조의3
        public string DR_GUBUN_SUB3; // 1.집도의 2.보조의
        public string DPTCD_SUB3;
        public string INSDPTCD_SUB3;
        public string INSDPTCD2_SUB3;
        public string DRID_SUB3;
        public string DRNM_SUB3;
        public string GDRLCID_SUB3; // 수술의면허번호
        public string DEPT_INFO_SUB3 { get { return DPTCD_SUB3 + "(" + INSDPTCD_SUB3 + INSDPTCD2_SUB3 + ")"; } }

        public string STAFG; // 응급여부(0.정규 1.초응급 2.중응급 3.응급)

        // TU02
        public List<string> ONM = new List<string>(); // 수술명
        public List<string> ISPCD = new List<string>(); // 수가코드(EDI코드)

        // TU03
        public string ANETP; // 마취종류
        public string ANETPNM; // 마취종류명칭

        // TU90
        public string EMPID; // 작성의사(꼭의사는아님)
        public string EMPNM; // 작성의사명
        public string WDATE; // 작성일자(OPDT)
        public string WTIME; // 작성시간
        public string PREDX; // 수술전진단
        public string POSDX; // 수술후진단
        public string POS; // 수술체위
        public string LESION; // 병변의 위치
        public string INDIOFSURGERY; // 수술소견
        public string SURFNDNPRO; // 수술절차
        public string REMARK; // 특이사항

        public string OPSDTM { get { return OPSDT + (OPSHR.Length == 1 ? "0" + OPSHR : OPSHR) + (OPSMN.Length == 1 ? "0" + OPSMN : OPSMN); } }
        public string OPEDTM { get { return OPEDT + (OPEHR.Length == 1 ? "0" + OPEHR : OPEHR) + (OPEMN.Length == 1 ? "0" + OPEMN : OPEMN); } }
        public string STAFG_YN { get { return STAFG == "0" ? "1" : "2"; } }
        public string WRTDTM { get { return WDATE + WTIME; } }

        public void Clear()
        {
            // TU01
            OPSDT = "";
            OPSHR = "";
            OPSMN = "";
            OPEDT = "";
            OPEHR = "";
            OPEMN = "";
            // 집도의
            DR_GUBUN = "";
            DPTCD = "";
            INSDPTCD = "";
            INSDPTCD2 = "";
            DRID = "";
            DRNM = "";
            GDRLCID = ""; // 수술의 면허번호
            // 보조의1
            DR_GUBUN_SUB1 = "";
            DPTCD_SUB1 = "";
            INSDPTCD_SUB1 = "";
            INSDPTCD2_SUB1 = "";
            DRID_SUB1 = "";
            DRNM_SUB1 = "";
            GDRLCID_SUB1 = ""; // 수술의 면허번호
            // 보조의2
            DR_GUBUN_SUB2 = "";
            DPTCD_SUB2 = "";
            INSDPTCD_SUB2 = "";
            INSDPTCD2_SUB2 = "";
            DRID_SUB2 = "";
            DRNM_SUB2 = "";
            GDRLCID_SUB2 = ""; // 수술의 면허번호
            // 보조의3
            DR_GUBUN_SUB3 = "";
            DPTCD_SUB3 = "";
            INSDPTCD_SUB3 = "";
            INSDPTCD2_SUB3 = "";
            DRID_SUB3 = "";
            DRNM_SUB3 = "";
            GDRLCID_SUB3 = ""; // 수술의 면허번호

            STAFG = ""; // 응급여부(0.정규 1.초응급 2.중응급 3.응급)
            // TU02
            ONM.Clear();
            ISPCD.Clear();

            // TU03
            ANETP = ""; // 마취종류
            ANETPNM = ""; // 마취종류명칭
            // TU90
            EMPID = ""; // 작성의사
            EMPNM = ""; // 작성의사명
            WDATE = ""; // 작성일자
            WTIME = ""; // 작성시간
            PREDX = ""; // 수술전진단
            POSDX = ""; // 수술후진단
            POS = ""; // 수술체위
            INDIOFSURGERY = ""; // 수술소견
            SURFNDNPRO = ""; // 수술절차
            REMARK = ""; // 특이사항
        }
    }
}
