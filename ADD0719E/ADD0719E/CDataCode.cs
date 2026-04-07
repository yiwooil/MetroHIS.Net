using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0719E
{
    class CDataCode
    {
        public long NO { get; set; }

        public string OUTCNT { get; set; } // 처방전교부번호
        public string LNO { get; set; } // 줄번호
        public string JJRMK { get; set; } // 조정사유
        public string JJRMK2 { get; set; } // 조정사유
        public double IJDCNT { get; set; } // 1회투약량
        public double IJDQTY { get; set; } // 일투
        public long IJDDAY { get; set; } // 총투
        public long JJAMT { get; set; } // 조정금액
        public string BGIHO { get; set; } // 조정의약품코드
        public string DRUGID { get; set; } // 조제기관기호
        public string DRUGNM { get; set; } // 조제기관명
        public string DRUGCNECNO { get; set; } // 조제기관접수번호
        public string DRUGCNECYY { get; set; } // 조제기관접수년도
        public string DRUGEPRTNO { get; set; } // 조제기관명일련번호
        public string MEMO { get; set; } // 비고사항
        public string DEMSEQ { get; set; } // 심사차수
        public string REDAY { get; set; } // 통보일자
        public string CNECNO { get; set; } // 접수번호
        public string DCOUNT { get; set; } // 청구서일련번호
        public string EPRTNO { get; set; } // 명세서일련번호
        public string PNM { get; set; } // 환자명
        public string BGIHONM { get; set; } // 약품명칭
        public string DEMNO { get; set; }

        public string JJREMARK { get { return JJRMK + JJRMK2; } }

        public void Clear()
        {
            NO = 0;

            OUTCNT = ""; // 처방전교부번호
            LNO = ""; // 줄번호
            JJRMK = ""; // 조정사유
            JJRMK2 = ""; // 조정사유
            IJDCNT = 0; // 1회투약량
            IJDQTY = 0; // 일투
            IJDDAY = 0; // 총투
            JJAMT = 0; // 조정금액
            BGIHO = ""; // 조정의약품코드
            DRUGID = ""; // 조제기관기호
            DRUGNM = ""; // 조제기관명
            DRUGCNECNO = ""; // 조제기관접수번호
            DRUGCNECYY = ""; // 조제기관접수년도
            DRUGEPRTNO = ""; // 조제기관명일련번호
            MEMO = ""; // 비고사항
            DEMSEQ = ""; // 심사차수
            REDAY = ""; // 통보일자
            CNECNO = ""; // 접수번호
            DCOUNT = ""; // 청구서일련번호
            EPRTNO = ""; // 명세서일련번호
            PNM = ""; // 환자명
            BGIHONM = ""; // 약품명칭
            DEMNO = "";
        }
    }
}
