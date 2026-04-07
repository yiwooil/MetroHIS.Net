using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0720E
{
    class CDataCode
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
        public string SKJJRMK { get; set; } // 이전심결사항-조정사유
        public string SKJJRMK2 { get; set; } // 이전심결사항-조정사유상세
        public string SKGUBUN { get; set; } // 이전심결사항-1,2구분
        public string SKBGIHO { get; set; } // 이전심결사항-코드
        public long SKDANGA { get; set; } // 이전심결사항-단가
        public double SKDQTY { get; set; } // 이전심결사항-일투 인정횟수
        public long SKDDAY { get; set; } // 이전심결사항-총투 인정횟수
        public long SKIJAMT { get; set; } // 이전심결사항-인정금액
        public long SKJJAMT { get; set; } // 이전심결사항-조정금액
        public string SKJJSAU { get; set; } // 이전심결사항-관련근거
        public string JSJJRMK { get; set; } // 정산심결사항-조정사유
        public string JSJJRMK2 { get; set; } // 정산심결사항-조정사유상세
        public string JSBGIHO { get; set; } // 정산심결사항-코드
        public long JSDANGA { get; set; } // 정산심결사항-단가
        public double JSDQTY { get; set; } // 정산심결사항-일투 인정횟수
        public long JSDDAY { get; set; } // 정산심결사항-총투 인정횟수
        public long JSIJAMT { get; set; } // 정산심결사항-인정금액
        public long JSJJAMT { get; set; } // 정산심결사항-조정금액
        public string JSMEMO { get; set; } // 정산심결사항-비고(조정사유내역)
        public string PNM { get; set; } // 수진자명
        public string SKBGIHONM { get; set; }
        public string JSBGIHONM { get; set; }
        public double SKCNTQTY { get; set; }
        public double JSCNTQTY { get; set; }
        public long SKUPLMTAMT { get; set; }
        public long SKIJUPLMTCHAAMT { get; set; }
        public long SKJJUPLMTCHAAMT { get; set; }
        public long JSUPLMTAMT { get; set; }
        public long JSIJUPLMTCHAAMT { get; set; }
        public long JSJJUPLMTCHAAMT { get; set; }
        public string HANGNO { get; set; }
        public string DEMNO { get; set; }

        public string SKJJREMARK { get { return SKJJRMK + SKJJRMK2; } }
        public string JSJJREMARK { get { return JSJJRMK + JSJJRMK2; } }

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
            SKJJRMK = ""; // 이전심결사항-조정사유
            SKJJRMK2 = ""; // 이전심결사항-조정사유상세
            SKGUBUN = ""; // 이전심결사항-1,2구분
            SKBGIHO = ""; // 이전심결사항-코드
            SKDANGA = 0; // 이전심결사항-단가
            SKDQTY = 0; // 이전심결사항-일투 인정횟수
            SKDDAY = 0; // 이전심결사항-총투 인정횟수
            SKIJAMT = 0; // 이전심결사항-인정금액
            SKJJAMT = 0; // 이전심결사항-조정금액
            SKJJSAU = ""; // 이전심결사항-관련근거
            JSJJRMK = ""; // 정산심결사항-조정사유
            JSJJRMK2 = ""; // 정산심결사항-조정사유상세
            JSBGIHO = ""; // 정산심결사항-코드
            JSDANGA = 0; // 정산심결사항-단가
            JSDQTY = 0; // 정산심결사항-일투 인정횟수
            JSDDAY = 0; // 정산심결사항-총투 인정횟수
            JSIJAMT = 0; // 정산심결사항-인정금액
            JSJJAMT = 0; // 정산심결사항-조정금액
            JSMEMO = ""; // 정산심결사항-비고(조정사유내역)
            PNM = ""; // 수진자명
            SKBGIHONM = "";
            JSBGIHONM = "";
            SKCNTQTY = 0;
            JSCNTQTY = 0;
            SKUPLMTAMT = 0;
            SKIJUPLMTCHAAMT = 0;
            SKJJUPLMTCHAAMT = 0;
            JSUPLMTAMT = 0;
            JSIJUPLMTCHAAMT = 0;
            JSJJUPLMTCHAAMT = 0;
            HANGNO = "";
            DEMNO = "";
        }
    }
}
