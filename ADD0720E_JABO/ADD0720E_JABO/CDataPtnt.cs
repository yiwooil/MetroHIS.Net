using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0720E_JABO
{
    class CDataPtnt
    {
        public int SKJSDIV; // 0.심결 1.정산
        public int NO { get; set; }

        public string JSDEMSEQ;        // 정산심사차수
        public string JSREDAY;         // 정산통보일자
        public string CNECNO;          // 접수번호
        public string DCOUNT;          // 청구서 일련번호
        public string JSYYSEQ;         // 정산연번
        public string JSSEQNO;         // 정산일련번호
        public string EPRTNO { get; set; }  // 명세서 일련번호
        public string APPRNO { get; set; }  // 사고접수번호
        public string PNM { get; set; }     // 수진자성명
        public string JRHT { get; set; }    // 진료형태
        public string GUBUN { get; set; }   // 구분(1.환급 2.환수 3.환수+환급)
        public string STEDT { get; set; }   // 당월요양개시일
        public string DACD { get; set; }    // 상병분류기호
        public long SKCHOGUM;        // 이전심결사항-초진료
        public long SKJAEGUM;        // 이전심결사항-재진료
        public long SKTTTAMT;        // 이전심결사항-진료비총액
        public long SKRSTAMT;        // 이전심결사항-심사결정액
        public long SKSUTAKAMT;      // 이전심결사항-위탁검사직접지급금
        public long SKUPLMTCHATTAMT; // 이전심결사항-약제상한차액총액 합계
        public long SKJBPTAMT;       // 이전심결사항-환자납부총액 합계
        public long SKCHOCNT;        // 이전심결사항-초진횟수
        public long SKCHONCNT;       // 이전심결사항-초진가산횟수(1일분투약횟수)
        public long SKJAECNT;        // 이전심결사항-재진횟수(2일분투약횟수)
        public long SKJAENCNT;       // 이전심결사항-재진가산횟수(3일분이상투약횟수)
        public long SKEXAMC;         // 이전심결사항-입원일수(내원일수)
        public long SKORDDAYS;       // 이전심결사항-요양급여일수
        public long SKORDCNT;        // 이전심결사항-처방횟수
        public long JSCHOGUM;        // 정산심결사항-초진료
        public long JSJAEGUM;        // 정산심결사항-재진료
        public long JSTTTAMT;        // 정산심결사항-진료비총액
        public long JSRSTAMT;        // 정산심결사항-심사결정액
        public long JSSUTAKAMT;      // 정산심결사항-위탁검사직접지급금
        public long JSRSTCHAAMT;     // 정산심결사항-결정차액
        public long JSUPLMTCHATTAMT; // 정산심결사항-약제상한차액총액 합계
        public long JSJBPTAMT;       // 정산심결사항-환자납부총액 합계
        public long JSCHOCNT;        // 정산심결사항-초진횟수
        public long JSCHONCNT;       // 정산심결사항-초진가산횟수(1일분투약횟수)
        public long JSJAECNT;        // 정산심결사항-재진횟수(2일분투약횟수)
        public long JSJAENCNT;       // 정산심결사항-재진가산횟수(3일분이상투약횟수)
        public long JSEXAMC;         // 정산심결사항-입원일수(내원일수)
        public long JSORDDAYS;       // 정산심결사항-요양급여일수
        public long JSORDCNT;        // 정산심결사항-처방횟수
        public string MEMO { get; set; }  // 명일련비고사항
        public string DEMNO { get; set; } // 청구번호

        public string SKJSFG { get { return SKJSDIV == 0 ? "심결" : "정산"; } }
        public long SKJS_CHOGUM { get { return SKJSDIV == 0 ? SKCHOGUM : JSCHOGUM; } }
        public long SKJS_JAEGUM { get { return SKJSDIV == 0 ? SKJAEGUM : JSJAEGUM; } }
        public long SKJS_TTTAMT { get { return SKJSDIV == 0 ? SKTTTAMT : JSTTTAMT; } }
        public long SKJS_RSTAMT { get { return SKJSDIV == 0 ? SKRSTAMT : JSRSTAMT; } }
        public long SKJS_SUTAKAMT { get { return SKJSDIV == 0 ? SKSUTAKAMT : JSSUTAKAMT; } }
        public long SKJS_JSRSTCHAAMT { get { return SKJSDIV == 0 ? 0 : JSRSTCHAAMT; } }
        public long SKJS_UPLMTCHATTAMT { get { return SKJSDIV == 0 ? SKUPLMTCHATTAMT : JSUPLMTCHATTAMT; } }
        public long SKJS_JBPTAMT { get { return SKJSDIV == 0 ? SKJBPTAMT : JSJBPTAMT; } }
        public long SKJS_CHOCNT { get { return SKJSDIV == 0 ? SKCHOCNT : JSCHOCNT; } }
        public long SKJS_CHONCNT { get { return SKJSDIV == 0 ? SKCHONCNT : JSCHONCNT; } }
        public long SKJS_JAECNT { get { return SKJSDIV == 0 ? SKJAECNT : JSJAECNT; } }
        public long SKJS_JAENCNT { get { return SKJSDIV == 0 ? SKJAENCNT : JSJAENCNT; } }
        public long SKJS_EXAMC { get { return SKJSDIV == 0 ? SKEXAMC : JSEXAMC; } }
        public long SKJS_ORDDAYS { get { return SKJSDIV == 0 ? SKORDDAYS : JSORDDAYS; } }
        public long SKJS_ORDCNT { get { return SKJSDIV == 0 ? SKORDCNT : JSORDCNT; } }

        public void Clear()
        {
            SKJSDIV = 0;
            NO = 0;

            JSDEMSEQ = "";       // 정산심사차수
            JSREDAY = "";        // 정산통보일자
            CNECNO = "";         // 접수번호
            DCOUNT = "";         // 청구서 일련번호
            JSYYSEQ = "";        // 정산연번
            JSSEQNO = "";        // 정산일련번호
            EPRTNO = "";         // 명세서 일련번호
            APPRNO = "";         // 사고접수번호
            PNM = "";            // 수진자성명
            JRHT = "";           // 진료형태
            GUBUN = "";          // 구분(1.환급 2.환수 3.환수+환급)
            STEDT = "";          // 당월요양개시일
            DACD = "";           // 상병분류기호
            SKCHOGUM = 0;        // 이전심결사항-초진료
            SKJAEGUM = 0;        // 이전심결사항-재진료
            SKTTTAMT = 0;        // 이전심결사항-진료비총액
            SKRSTAMT = 0;        // 이전심결사항-심사결정액
            SKSUTAKAMT = 0;      // 이전심결사항-위탁검사직접지급금
            SKUPLMTCHATTAMT = 0; // 이전심결사항-약제상한차액총액 합계
            SKJBPTAMT = 0;       // 이전심결사항-환자납부총액 합계
            SKCHOCNT = 0;        // 이전심결사항-초진횟수
            SKCHONCNT = 0;       // 이전심결사항-초진가산횟수(1일분투약횟수)
            SKJAECNT = 0;        // 이전심결사항-재진횟수(2일분투약횟수)
            SKJAENCNT = 0;       // 이전심결사항-재진가산횟수(3일분이상투약횟수)
            SKEXAMC = 0;         // 이전심결사항-입원일수(내원일수)
            SKORDDAYS = 0;       // 이전심결사항-요양급여일수
            SKORDCNT = 0;        // 이전심결사항-처방횟수
            JSCHOGUM = 0;        // 정산심결사항-초진료
            JSJAEGUM = 0;        // 정산심결사항-재진료
            JSTTTAMT = 0;        // 정산심결사항-진료비총액
            JSRSTAMT = 0;        // 정산심결사항-심사결정액
            JSSUTAKAMT = 0;      // 정산심결사항-위탁검사직접지급금
            JSRSTCHAAMT = 0;     // 정산심결사항-결정차액
            JSUPLMTCHATTAMT = 0; // 정산심결사항-약제상한차액총액 합계
            JSJBPTAMT = 0;       // 정산심결사항-환자납부총액 합계
            JSCHOCNT = 0;        // 정산심결사항-초진횟수
            JSCHONCNT = 0;       // 정산심결사항-초진가산횟수(1일분투약횟수)
            JSJAECNT = 0;        // 정산심결사항-재진횟수(2일분투약횟수)
            JSJAENCNT = 0;       // 정산심결사항-재진가산횟수(3일분이상투약횟수)
            JSEXAMC = 0;         // 정산심결사항-입원일수(내원일수)
            JSORDDAYS = 0;       // 정산심결사항-요양급여일수
            JSORDCNT = 0;        // 정산심결사항-처방횟수
            MEMO = "";           // 명일련비고사항
            DEMNO = "";          // 청구번호
        }
    }
}
