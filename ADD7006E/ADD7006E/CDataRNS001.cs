using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRNS001
    {
        // TU94
        public string OR_INDT; // 수술실 입실일자(YYYYMMDD)
        public string OR_INTM; // 수술실 입실시간(HH:MM)
        public string OR_INDTM { get { return OR_INDT + OR_INTM; } }
        public string PT_OUTDT; // 수술실 퇴실일자(YYYYMMDD)
        public string PT_OUTTM; // 수술실 퇴실시간(HH:MM)
        public string PT_OUTDTM { get { return PT_OUTDT + PT_OUTTM; } }
        public string OP_STDT; // 수술 시작일자(YYYYMMDD)
        public string OP_STTM; // 수술 시작시간(HH:MM)
        public string OP_STDTM { get { return OP_STDT + OP_STTM; } }
        public string OP_ENDDT; // 수술 종료일자(YYYYMMDD)
        public string OP_ENDTM; // 수술 종료시간(HH:MM)
        public string OP_ENDDTM { get { return OP_ENDDT + OP_ENDTM; } }
        public string SRNURS1; // 소독 간호사 성명
        public string CIRNURS1; // 순회 간호사 성명
        public string SYSDT; // 작성일자
        public string SYSTM; // 작성시간
        public string SYSDTM { get { return SYSDT + SYSTM; } }
        public string TIMEOUTCHK_1; // Time Out 시행여부(1이면 시행)
        public string TIMEOUTCHK_2; // Time Out 시행여부(1이면 미시행)
        public string TIMEOUTCHK
        {
            get
            {
                string ret = "";
                if (TIMEOUTCHK_1 == "1") ret = "1";
                else if (TIMEOUTCHK_2 == "2") ret = "2";
                return ret;
            }
        }
        public string PREDXNM; // 수술 전 진단명
        public string POSTDXNM; // 수술 후 진단명
        public string POSTOPNM; // 수술 후 수술명

        // TU94A
        public List<string> TUBE_1 = new List<string>(); // 삽입관 종류
        public List<string> TUBE_2 = new List<string>(); // 삽입관 크기
        public List<string> TUBE_3 = new List<string>(); // 삽입관 부위
        public List<string> TUBE_4 = new List<string>(); // 삽입관 수량
        public string TUBE_RMK(int idx)
        {
            return "크기:" + TUBE_2[idx] + ",부위:" + TUBE_3[idx] + ",수량:" + TUBE_4[idx];
        }

        // TU94
        public List<string> ONM = new List<string>(); // 약품명
        public List<string> QTY = new List<string>(); // 투여량

        // TU94C
        public List<string> GUM_1 = new List<string>(); // 검체종류
        public List<string> GUM_2 = new List<string>(); // 검체부위
        public List<string> GUM_3 = new List<string>(); // 검체개수
        
        public void Clear()
        {
            // TU94
            OR_INDT = ""; // 수술실 입실일자(YYYYMMDD)
            OR_INTM = ""; // 수술실 입실시간(HH:MM)
            PT_OUTDT = ""; // 수술실 퇴실일자(YYYYMMDD)
            PT_OUTTM = ""; // 수술실 퇴실시간(HH:MM)
            OP_STDT = ""; // 수술 시작일자(YYYYMMDD)
            OP_STTM = ""; // 수술 시작시간(HH:MM)
            OP_ENDDT = ""; // 수술 종료일자(YYYYMMDD)
            OP_ENDTM = ""; // 수술 종료시간(HH:MM)
            SRNURS1 = ""; // 소독 간호사 성명
            CIRNURS1 = ""; // 순회 간호사 성명
            SYSDT = ""; // 작성일자
            SYSTM = ""; // 작성시간
            TIMEOUTCHK_1 = ""; // Time Out 시행여부(1이면 시행)
            TIMEOUTCHK_2 = ""; // Time Out 시행여부(1이면 미시행)
            PREDXNM = ""; // 수술 전 진단명
            POSTDXNM = ""; // 수술 후 진단명
            POSTOPNM = ""; // 수술 후 수술명

            // TU94A
            TUBE_1.Clear(); // 삽입관 종류
            TUBE_2.Clear(); // 삽입관 크기
            TUBE_3.Clear(); // 삽입관 부위
            TUBE_4.Clear(); // 삽입관 수량

            // TU94
            ONM.Clear(); // 약품명
            QTY.Clear(); // 투여량

            // TU94C
            GUM_1.Clear(); // 검체종류
            GUM_2.Clear(); // 검체부위
            GUM_3.Clear(); // 검체개수
        }
    }
}
