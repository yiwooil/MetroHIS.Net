using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0720E_JABO
{
    class CDataDetail
    {
        public int SKJSDIV; // 0.심결 1.정산
        public int NO { get; set; }

        public string JSDEMSEQ; // 정산심사차수
        public string JSREDAY;  // 정산통보일자
        public string CNECNO;   // 접수번호
        public string DCOUNT;   // 청구서 일련번호
        public string JSYYSEQ;  // 정산연번
        public string JSSEQNO;  // 정산일련번호
        public string EPRTNO { get; set; }   // 명세서 일련번호
        public string LNO { get; set; }      // 줄번호
        public string SKJJRMK;  // 이전심결사항-조정사유
        public string SKJJRMK2; // 이전심결사항-조정사유2
        public string SKBGIHO;  // 이전심결사항-코드
        public float SKDANGA;    // 이전심결사항-단가
        public float SKDQTY;     // 이전심결사항-일투 인정횟수
        public long SKDDAY;     // 이전심결사항-총투 인정횟수
        public long SKIJAMT;    // 이전심결사항-인정금액
        public long SKJJAMT;    // 이전심결사항-조정금액
        public string JSJJRMK;  // 정산심결사항-조정사유
        public string JSJJRMK2; // 정산심결사항-조정사유2
        public string JSBGIHO;  // 정산심결사항-코드
        public float JSDANGA;    // 정산심결사항-단가
        public float JSDQTY;     // 정산심결사항-일투 인정횟수
        public long JSDDAY;     // 정산심결사항-총투 인정횟수
        public long JSIJAMT;    // 정산심결사항-인정금액
        public long JSJJAMT;    // 정산심결사항-조정금액
        public string JSMEMO { get; set; }   // 정산심결사항-비고(조정사유내역)
        public string PNM { get; set; }      // 수진자명
        public string SKBGIHONM; 
        public string JSBGIHONM;
        public float SKCNTQTY;
        public float JSCNTQTY;
        public long SKUPLMTAMT;
        public long SKIJUPLMTCHAAMT;
        public long SKJJUPLMTCHAAMT;
        public long JSUPLMTAMT;
        public long JSIJUPLMTCHAAMT;
        public long JSJJUPLMTCHAAMT;
        public string HANGNO { get; set; }
        public string DEMNO { get; set; }

        public string SKJSFG { get { return SKJSDIV == 0 ? "심결" : "정산"; } }
        public float SKJS_DANGA { get { return SKJSDIV == 0 ? SKDANGA : JSDANGA; } }
        public float SKJS_DQTY { get { return SKJSDIV == 0 ? SKDQTY : JSDQTY; } }
        public long SKJS_DDAY { get { return SKJSDIV == 0 ? SKDDAY : JSDDAY; } }
        public long SKJS_IJAMT { get { return SKJSDIV == 0 ? SKIJAMT : JSIJAMT; } }
        public long SKJS_JJAMT { get { return SKJSDIV == 0 ? SKJJAMT : JSJJAMT; } }
        public string SKJS_REMARK { get { return SKJSDIV == 0 ? SKJJRMK + SKJJRMK2 : JSJJRMK + JSJJRMK2; } }
        public string SKJS_BGIHO { get { return SKJSDIV == 0 ? SKBGIHO : JSBGIHO; } }
        public string SKJS_BGIHONM { get { return SKJSDIV == 0 ? SKBGIHONM : JSBGIHONM; } }
        public float SKJS_CNTQTY { get { return SKJSDIV == 0 ? SKCNTQTY : JSCNTQTY; } }
        public long SKJS_UPLMTAMT { get { return SKJSDIV == 0 ? SKUPLMTAMT : JSUPLMTAMT; } }
        public long SKJS_IJUPLMTCHAAMT { get { return SKJSDIV == 0 ? SKIJUPLMTCHAAMT : JSIJUPLMTCHAAMT; } }
        public long SKJS_JJUPLMTCHAAMT { get { return SKJSDIV == 0 ? SKJJUPLMTCHAAMT : JSJJUPLMTCHAAMT; } }

        public void Clear()
        {
            SKJSDIV = 0;
            NO = 0;

            JSDEMSEQ = "";   // 정산심사차수
            JSREDAY = "";    // 정산통보일자
            CNECNO = "";     // 접수번호
            DCOUNT = "";     // 청구서 일련번호
            JSYYSEQ = "";    // 정산연번
            JSSEQNO = "";    // 정산일련번호
            EPRTNO = "";     // 명세서 일련번호
            LNO = "";        // 줄번호
            SKJJRMK = "";    // 이전심결사항-조정사유
            SKJJRMK2 = "";   // 이전심결사항-조정사유2
            SKBGIHO = "";    // 이전심결사항-코드
            SKDANGA = 0;    // 이전심결사항-단가
            SKDQTY = 0;     // 이전심결사항-일투 인정횟수
            SKDDAY = 0;     // 이전심결사항-총투 인정횟수
            SKIJAMT = 0;    // 이전심결사항-인정금액
            SKJJAMT = 0;    // 이전심결사항-조정금액
            JSJJRMK = "";    // 정산심결사항-조정사유
            JSJJRMK2 = "";   // 정산심결사항-조정사유2
            JSBGIHO = "";    // 정산심결사항-코드
            JSDANGA = 0;    // 정산심결사항-단가
            JSDQTY = 0;     // 정산심결사항-일투 인정횟수
            JSDDAY = 0;     // 정산심결사항-총투 인정횟수
            JSIJAMT = 0;    // 정산심결사항-인정금액
            JSJJAMT = 0;    // 정산심결사항-조정금액
            JSMEMO = "";     // 정산심결사항-비고(조정사유내역)
            PNM = "";        // 수진자명
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
