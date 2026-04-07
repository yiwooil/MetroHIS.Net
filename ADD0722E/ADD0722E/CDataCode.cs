using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0722E
{
    class CDataCode
    {
        public long NO { get; set; }

        public string JSDEMSEQ; // 정산심사차수
        public string JSREDAY; // 정산통보일자
        public string CNECNO; // 접수번호
        public string DCOUNT; // 청구서 일련번호
        public string JSYYSEQ; // 정산연번
        public string JSSEQNO; // 정산일련번호
        public string EPRTNO { get; set; } // 명세서 일련번호
        public string OUTCNT { get; set; } // 처방전교부번호
        public string LNO { get; set; } // 줄번호
        public string SKJJRMK { get; set; } // 이전심결사항-조정사유
        public string SKBGIHO { get; set; } // 이전심결사항-코드
        public double SKIJDCNT { get; set; } // 이전심결사항-1회투약량 인정횟수
        public long SKIJDQTY { get; set; } // 이전심결사항-일투 인정횟수
        public long SKIJDDAY { get; set; } // 이전심결사항-총투 인정횟수
        public long SKJJAMT { get; set; } // 이전심결사항-조정금액
        public long SKRSTAMT { get; set; } // 이전심결사항-심사결정액
        public string JSJJRMK { get; set; } // 정산심결사항-조정사유
        public string JSBGIHO { get; set; } // 정산심결사항-코드
        public double JSIJDCNT { get; set; } // 정산심결사항-1회투약량 인정횟수
        public long JSIJDQTY { get; set; } // 정산심결사항-일투 인정횟수
        public long JSIJDDAY { get; set; } // 정산심결사항-총투 인정횟수
        public long JSJJAMT { get; set; } // 정산심결사항-조정금액
        public long JSRSTAMT { get; set; } // 정산심결사항-심사결정액
        public string DRUGID { get; set; } // 조제기관 기호
        public string DRUGNM { get; set; } // 조제기관 명
        public string DRUGCNECNO; // 조제기관 접수번호
        public string DRUGCNECYY; // 조제기관 접수년도
        public string DRUGEPRTNO; // 조제기관 명일련번호
        public string MEMO { get; set; } // 명일련비고사항
        public string PNM { get; set; } // 수진자명
        public string SKBGIHONM { get; set; } //
        public string JSBGIHONM { get; set; } //

        public void Clear()
        {
            NO = 0;

            JSDEMSEQ = ""; // 정산심사차수
            JSREDAY = ""; // 정산통보일자
            CNECNO = ""; // 접수번호
            DCOUNT = ""; // 청구서 일련번호
            JSYYSEQ = ""; // 정산연번
            JSSEQNO = ""; // 정산일련번호
            EPRTNO = ""; // 명세서 일련번호
            OUTCNT = ""; // 처방전교부번호
            LNO = ""; // 줄번호
            SKJJRMK = ""; // 이전심결사항-조정사유
            SKBGIHO = ""; // 이전심결사항-코드
            SKIJDCNT = 0; // 이전심결사항-1회투약량 인정횟수
            SKIJDQTY = 0; // 이전심결사항-일투 인정횟수
            SKIJDDAY = 0; // 이전심결사항-총투 인정횟수
            SKJJAMT = 0; // 이전심결사항-조정금액
            SKRSTAMT = 0; // 이전심결사항-심사결정액
            JSJJRMK = ""; // 정산심결사항-조정사유
            JSBGIHO = ""; // 정산심결사항-코드
            JSIJDCNT = 0; // 정산심결사항-1회투약량 인정횟수
            JSIJDQTY = 0; // 정산심결사항-일투 인정횟수
            JSIJDDAY = 0; // 정산심결사항-총투 인정횟수
            JSJJAMT = 0; // 정산심결사항-조정금액
            JSRSTAMT = 0; // 정산심결사항-심사결정액
            DRUGID = ""; // 조제기관 기호
            DRUGNM = ""; // 조제기관 명
            DRUGCNECNO = ""; // 조제기관 접수번호
            DRUGCNECYY = ""; // 조제기관 접수년도
            DRUGEPRTNO = ""; // 조제기관 명일련번호
            MEMO = ""; // 명일련비고사항
            PNM = ""; // 수진자명
            SKBGIHONM = ""; //
            JSBGIHONM = ""; //
        }
    }
}
