using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRIY001
    {
        public string BEDEDT; // 입원일자
        public string BEDEHM; // 입원시간
        public string DPTCD; // 입원과
        public string INSDPTCD; // 입원 진료과목
        public string INSDPTCD2; // 입원 내과세부진료과목
        public string TDATE; // 전과일
        public string GR_DPTCD; // 전입과
        public string GR_INSDPTCD; // 전입 진료과목
        public string GR_INSDPTCD2; // 전입 내과세부진료과목
        public string HM_DPTCD; // 전출과
        public string HM_INSDPTCD; // 전출 진료과목
        public string HM_INSDPTCD2; // 전출 내과세부진료과목
        public string TDRID; // 담당의사(환자를 받은과의 담당의사)
        public string TDRNM; // 담당의명(환자를 받은과의 담당의사)
        public string EMPID; // 작성자
        public string EMPNM; // 작성자성명
        public string SYSDT; // 작성일자
        public string SYSTM; // 작성시간
        public string NOWT; // 현병력
        public string PE; // 신체검진
        public string PROBLEMS; // 문제목록
        public List<string> DXD = new List<string>(); // 진단명
        public string APLAN; // 치료계획

        public string BEDEDTM { get { return BEDEDT + BEDEHM; } }
        public string DEPT_INFO { get { return DPTCD + "(" + INSDPTCD + INSDPTCD2 + ")"; } }
        public string GR_DEPT_INFO { get { return GR_DPTCD + "(" + GR_INSDPTCD + GR_INSDPTCD2 + ")"; } }
        public string HM_DEPT_INFO { get { return HM_DPTCD + "(" + HM_INSDPTCD + HM_INSDPTCD2 + ")"; } }
        public string SYSDTM { get { return SYSDT + SYSTM; } }

        public string DX_INFO
        {
            get
            {
                string ret = "";
                for (int i = 0; i < DXD.Count; i++)
                {
                    ret += DXD[i] + Environment.NewLine;
                }
                return ret;
            }
        }

        public void Clear()
        {
            BEDEDT = ""; // 입원일자
            BEDEHM = ""; // 입원시간
            DPTCD = ""; // 입원과
            INSDPTCD = ""; // 입원 진료과목
            INSDPTCD2 = ""; // 입원 내과세부진료과목
            TDATE = ""; // 전과일
            GR_DPTCD = ""; // 전입과
            GR_INSDPTCD = ""; // 전입 진료과목
            GR_INSDPTCD2 = ""; // 전입 내과세부진료과목
            HM_DPTCD = ""; // 전출과
            HM_INSDPTCD = ""; // 전출 진료과목
            HM_INSDPTCD2 = ""; // 전출 내과세부진료과목
            TDRID = ""; // 담당의사(환자를 받은과의 담당의사)
            TDRNM = ""; // 담당의명(환자를 받은과의 담당의사)
            EMPID = ""; // 작성자
            EMPNM = ""; // 작성자성명
            SYSDT = ""; // 작성일자
            SYSTM = ""; // 작성시간
            NOWT = ""; // 현병력
            PE = ""; // 신체검진
            PROBLEMS = ""; // 문제목록
            DXD.Clear(); // 진단명
            APLAN = ""; // 치료계획
        }
    }
}
