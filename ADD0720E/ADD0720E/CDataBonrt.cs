using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0720E
{
    class CDataBonrt
    {
        public long NO { get; set; }

        public string JSDEMSEQ { get; set; } // 정산심사차수
        public string JSREDAY { get; set; } // 정산통보일자
        public string CNECNO { get; set; } // 접수번호
        public string DCOUNT { get; set; } // 청구서 일련번호
        public string JSYYSEQ { get; set; } // 정산연번
        public string JSSEQNO { get; set; } // 정산일련번호
        public string EPRTNO { get; set; } // 명세서 일련번호
        public string LNO { get; set; } // 줄번호
        public string SKJKRTBKRMK { get; set; } // 이전심결사항-본인부담률변경사유
        public string SKHANGNO { get; set; } // 이전심결사항-항번호
        public string SKBGIHO { get; set; } // 이전심결사항-코드
        public string SKDANGA { get; set; } // 이전심결사항-단가
        public string SKCNT { get; set; } // 이전심결사항-1회투약 인정횟수
        public string SKDQTY { get; set; } // 이전심결사항-일투 인정횟수
        public string SKDDAY { get; set; } // 이전심결사항-총투 인정횟수
        public string SKJKRTIJAMT { get; set; } // 이전심결사항-본인부담률변경 인정금액
        public string JSJKRTBKRMK { get; set; } // 정산심결사항-본인부담률변경사유
        public string JSHANGNO { get; set; } // 정산심결사항-항번호
        public string JSBGIHO { get; set; } // 정산심결사항-코드
        public string JSDANGA { get; set; } // 정산심결사항-단가
        public string JSCNT { get; set; } // 정산심결사항-1회투약 인정횟수
        public string JSDQTY { get; set; } // 정산심결사항-일투 인정횟수
        public string JSDDAY { get; set; } // 정산심결사항-총투 인정횟수
        public string JSJKRTIJAMT { get; set; } // 정산심결사항-본인부담률변경 인정금액
        public string MEMO { get; set; } // 정산심결사항-비고(본인부담률변경내역)
        public string PNM { get; set; } // 수진자명
        public string SKBGIHONM { get; set; }
        public string JSBGIHONM { get; set; }
        public string DEMNO { get; set; }

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
            LNO = ""; // 줄번호
            SKJKRTBKRMK = ""; // 이전심결사항-본인부담률변경사유
            SKHANGNO = ""; // 이전심결사항-항번호
            SKBGIHO = ""; // 이전심결사항-코드
            SKDANGA = ""; // 이전심결사항-단가
            SKCNT = ""; // 이전심결사항-1회투약 인정횟수
            SKDQTY = ""; // 이전심결사항-일투 인정횟수
            SKDDAY = ""; // 이전심결사항-총투 인정횟수
            SKJKRTIJAMT = ""; // 이전심결사항-본인부담률변경 인정금액
            JSJKRTBKRMK = ""; // 정산심결사항-본인부담률변경사유
            JSHANGNO = ""; // 정산심결사항-항번호
            JSBGIHO = ""; // 정산심결사항-코드
            JSDANGA = ""; // 정산심결사항-단가
            JSCNT = ""; // 정산심결사항-1회투약 인정횟수
            JSDQTY = ""; // 정산심결사항-일투 인정횟수
            JSDDAY = ""; // 정산심결사항-총투 인정횟수
            JSJKRTIJAMT = ""; // 정산심결사항-본인부담률변경 인정금액
            MEMO = ""; // 정산심결사항-비고(본인부담률변경내역)
            PNM = ""; // 수진자명
            SKBGIHONM = "";
            JSBGIHONM = "";
            DEMNO = "";
        }
    }
}
