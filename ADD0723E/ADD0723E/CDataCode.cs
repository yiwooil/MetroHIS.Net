using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0723E
{
    class CDataCode
    {
        public string JSDEMSEQ { get; set; } // 정산심사차수
        public string JSREDAY { get; set; } // 정산통보일자
        public string CNECNO { get; set; } // 접수번호
        public string DCOUNT { get; set; } // 청구서 일련번호
        public string JSYYSEQ { get; set; } // 정산연번
        public string JSSEQNO { get; set; } // 정산일련번호
        public string EPRTNO { get; set; } // 명세서 일련번호
        public string OUTCNT { get; set; } // 처방전교부번호
        public string LNO { get; set; } // 줄번호
        public string JJRMK1 { get; set; } // 조정사유
        public string JJRMK2 { get; set; } // 조정사유2
        public string BGIHO { get; set; } // 코드
        public double IJDCNT { get; set; } // 1회투약량 인정횟수
        public double IJDQTY { get; set; } // 일투 인정횟수
        public long IJDDAY { get; set; } // 총투 인정횟수
        public long RETAMT { get; set; } // 환수금액
        public string DRUGID { get; set; } // 조제기관 기호
        public string DRUGNM { get; set; } // 조제기관 명
        public string DRUGCNECNO { get; set; } // 조제기관 접수번호
        public string DRUGCNECYY { get; set; } // 조제기관 접수년도
        public string DRUGEPRTNO { get; set; } // 조제기관 명일련번호
        public string MEMO { get; set; } // 정산심결 비고(조정사유내역)
        public string PNM { get; set; } // 환자명
        public string BGIHONM { get; set; } // 약품명칭

        public string JJRMK
        {
            get { return JJRMK1 + JJRMK2; }
        }

        public void Clear()
        {
            JSDEMSEQ = ""; // 정산심사차수
            JSREDAY = ""; // 정산통보일자
            CNECNO = ""; // 접수번호
            DCOUNT = ""; // 청구서 일련번호
            JSYYSEQ = ""; // 정산연번
            JSSEQNO = ""; // 정산일련번호
            EPRTNO = ""; // 명세서 일련번호
            OUTCNT = ""; // 처방전교부번호
            LNO = ""; // 줄번호
            JJRMK1 = ""; // 조정사유
            JJRMK2 = ""; // 조정사유2
            BGIHO = ""; // 코드
            IJDCNT = 0; // 1회투약량 인정횟수
            IJDQTY = 0; // 일투 인정횟수
            IJDDAY = 0; // 총투 인정횟수
            RETAMT = 0; // 환수금액
            DRUGID = ""; // 조제기관 기호
            DRUGNM = ""; // 조제기관 명
            DRUGCNECNO = ""; // 조제기관 접수번호
            DRUGCNECYY = ""; // 조제기관 접수년도
            DRUGEPRTNO = ""; // 조제기관 명일련번호
            MEMO = ""; // 정산심결 비고(조정사유내역)
            PNM = ""; // 환자명
            BGIHONM = ""; // 약품명칭
        }
    }
}
