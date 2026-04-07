using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0720E_JABO
{
    class CData
    {
        public int SKJSDIV; // 0.심결 1.정산

        public string JSDEMSEQ { get; set; }              // 정산심사차수
        public string JSREDAY { get; set; }               // 정산통보일자
        public string DEMSEQ { get; set; }                // 심사차수(yyyymm+seq(2))
        public string CNECNO { get; set; }                // 접수번호
        public string DEMNO { get; set; }                 // 청구번호
        public string SIMGBN { get; set; }                // 심사구분

        public long SKTTTAMT;              // 이전심결사항-진료비총액 합계
        public long SKRSTAMT;              // 이전심결사항-심사결정액 합계
        public long SKSUTAKAMT;            // 이전심결사항-위탁검사직접지급금 합계
        public long SKRSTCHAAMT;           // ***** 운율을 맞추기위한용도임.
        public long SKUPLMTCHATTAMT;       // 약제상한차액총액 합계
        public long SKJBPTAMT;             // 환자납부총액 합계

        public long SKCNT;                 // 이전심결사항-건수합계
        public float SKCDJS;               // 이전심결사항-차등지수
        public long SKCJJCNT;              // 이전심결사항-진찰(조제)횟수
        public long SKCJDAYS;              // 이전심결사항-진료(조제)일수
        public long SKCJDR;                // 이전심결사항-의(약)사수
        public long SKCJJAMT;              // 이전심결사항-진찰(조제)료
        public long SKCJJMAMT;             // 이전심결사항-진찰료(조제료)차감액
        public long SKCJRSTCHAAMT;         // ***** 운율을 맞추기위한용도임.

        public long JSTTTAMT;              // 정산심결사항-진료비총액 합계
        public long JSRSTAMT;              // 정산심결사항-심사결정액 합계
        public long JSSUTAKAMT;            // 정산심결사항-위탁검사직접지급금 합계
        public long JSRSTCHAAMT;           // 정산심결사항-결정차액 합계
        public long JSUPLMTCHATTAMT;       // 약제상한차액총액 합계
        public long JSJBPTAMT;             // 환자납부총액 합계

        public long JSCNT;                 // 정산심결사항-건수 합계
        public float JSCDJS;               // 정산심결사항-차등지수
        public long JSCJJCNT;              // 정산심결사항-진찰(조제)횟수
        public long JSCJDAYS;              // 정산심결사항-진료(조제)일수
        public long JSCJDR;                // 정산심결사항-의(약)사수
        public long JSCJJAMT;              // 정산심결사항-진찰(조제)료
        public long JSCJJMAMT;             // 정산심결사항-진찰료(조제료)차감액
        public long JSCJRSTCHAAMT;         // 정산심결사항-차등지수결정차액

        public string SKJS_FG { get { return SKJSDIV == 0 ? "심결" : "정산"; } }
        public long SKJS_TTTAMT { get { return SKJSDIV == 0 ? SKTTTAMT : JSTTTAMT; } }
        public long SKJS_RSTAMT { get { return SKJSDIV == 0 ? SKRSTAMT : JSRSTAMT; } }
        public long SKJS_SUTAKAMT { get { return SKJSDIV == 0 ? SKSUTAKAMT : JSSUTAKAMT; } }
        public long SKJS_RSTCHAAMT { get { return SKJSDIV == 0 ? SKRSTCHAAMT : JSRSTCHAAMT; } }
        public long SKJS_UPLMTCHATTAMT { get { return SKJSDIV == 0 ? SKUPLMTCHATTAMT : JSUPLMTCHATTAMT; } }
        public long SKJS_JBPTAMT { get { return SKJSDIV == 0 ? SKJBPTAMT : JSJBPTAMT; } }
        public long SKJS_CNT { get { return SKJSDIV == 0 ? SKCNT : JSCNT; } }
        public float SKJS_CDJS { get { return SKJSDIV == 0 ? SKCDJS : JSCDJS; } }
        public long SKJS_CJJCNT { get { return SKJSDIV == 0 ? SKCJJCNT : JSCJJCNT; } }
        public long SKJS_CJDAYS { get { return SKJSDIV == 0 ? SKCJDAYS : JSCJDAYS; } }
        public long SKJS_CJDR { get { return SKJSDIV == 0 ? SKCJDR : JSCJDR; } }
        public long SKJS_CJJAMT { get { return SKJSDIV == 0 ? SKCJJAMT : JSCJJAMT; } }
        public long SKJS_CJJMAMT { get { return SKJSDIV == 0 ? SKCJJMAMT : JSCJJMAMT; } }
        public long SKJS_CJRSTCHAAMT { get { return SKJSDIV == 0 ? SKCJRSTCHAAMT : JSCJRSTCHAAMT; } }

        public string MEMO { get; set; }                // 명일련비고사항
        public string JSREDPT1;              // 정산담당부명
        public string JSREDPT2;              // 정산담당조명
        public string JSREDPNM;              // 정산담당자명
        public string JSREDPNO;              // 정산담당자번호
        public string JSRETELE;              // 정산담당자전화번호
        public string JSREDEPT               // 정산담당(정산담당부명+정산담당조명+정산담당자명+정산담당자번호+정산담당자전화번호)
        {
            get
            {
                return JSREDPT2 + " " + JSREDPNM + " " + JSREDPNO + " " + JSRETELE + " " + JSREDPT1; // JSREDPT1은 건강보험심사평가원장 이라는 문구라서 맨 뒤로 보냈음.
            }
        }
        public string JSYYSEQ { get; set; }     // 정산연번
        public string JBUNICD { get; set; }     // 보험회사코드
        public string HOSID { get; set; }       // 요양기관 기호
        public string FMNO { get; set; }        // 서식번호
        public string GRPNO { get; set; }       // 묶음번호
        public string VERSION { get; set; }     // 버전구분
        public string DCOUNT { get; set; }      // 청구서 일련번호
        public string DEMUNITFG { get; set; }   // 청구단위구분
        public string JRFG { get; set; }        // 보험자종별구분
        public string JSBUSSCD;                 // 정산업무코드
        public string JSBUSSNM;                 // 정산업무명
        public string JSBUSS { get { return JSBUSSCD == "" && JSBUSSNM == "" ? "" : JSBUSSCD + "." + JSBUSSNM; } }

        public string JSREDEPT_JSBUSS { get { return SKJSDIV == 0 ? JSREDEPT : JSBUSS; } }
        public string JSYYSEQ_JBUNICD { get { return SKJSDIV == 0 ? JSYYSEQ : JBUNICD; } }
        public string GRPNO_VERSION { get { return SKJSDIV == 0 ? GRPNO : VERSION; } }
        public string DCOUNT_DEMUNITFG { get { return SKJSDIV == 0 ? DCOUNT : DEMUNITFG; } }


        public void Clear()
        {
            SKJSDIV = 0;
            JSDEMSEQ = "";              // 정산심사차수
            JSREDAY = "";               // 정산통보일자
            DEMSEQ = "";                // 심사차수(yyyymm+seq(2))
            CNECNO = "";                // 접수번호
            DEMNO = "";                 // 청구번호
            SIMGBN = "";                // 심사구분

            SKTTTAMT = 0;              // 이전심결사항-진료비총액 합계
            SKRSTAMT = 0;              // 이전심결사항-심사결정액 합계
            SKSUTAKAMT = 0;            // 이전심결사항-위탁검사직접지급금 합계
            SKRSTCHAAMT = 0;           // ***** 운율을 맞추기위한용도임.
            SKUPLMTCHATTAMT = 0;       // 약제상한차액총액 합계
            SKJBPTAMT = 0;             // 환자납부총액 합계

            SKCNT = 0;                 // 이전심결사항-건수합계
            SKCDJS = 0;                // 이전심결사항-차등지수
            SKCJJCNT = 0;              // 이전심결사항-진찰(조제)횟수
            SKCJDAYS = 0;              // 이전심결사항-진료(조제)일수
            SKCJDR = 0;                // 이전심결사항-의(약)사수
            SKCJJAMT = 0;              // 이전심결사항-진찰(조제)료
            SKCJJMAMT = 0;             // 이전심결사항-진찰료(조제료)차감액
            SKCJRSTCHAAMT = 0;         // ***** 운율을 맞추기위한용도임.

            JSTTTAMT = 0;              // 정산심결사항-진료비총액 합계
            JSRSTAMT = 0;              // 정산심결사항-심사결정액 합계
            JSSUTAKAMT = 0;            // 정산심결사항-위탁검사직접지급금 합계
            JSRSTCHAAMT = 0;           // 정산심결사항-결정차액 합계
            JSUPLMTCHATTAMT = 0;       // 약제상한차액총액 합계
            JSJBPTAMT = 0;             // 환자납부총액 합계

            JSCNT = 0;                 // 정산심결사항-건수 합계
            JSCDJS = 0;                // 정산심결사항-차등지수
            JSCJJCNT = 0;              // 정산심결사항-진찰(조제)횟수
            JSCJDAYS = 0;              // 정산심결사항-진료(조제)일수
            JSCJDR = 0;                // 정산심결사항-의(약)사수
            JSCJJAMT = 0;              // 정산심결사항-진찰(조제)료
            JSCJJMAMT = 0;             // 정산심결사항-진찰료(조제료)차감액
            JSCJRSTCHAAMT = 0;         // 정산심결사항-차등지수결정차액

            MEMO = "";                  // 명일련비고사항
            JSREDPT1 = "";              // 정산담당부명
            JSREDPT2 = "";              // 정산담당조명
            JSREDPNM = "";              // 정산담당자명
            JSREDPNO = "";              // 정산담당자번호
            JSRETELE = "";              // 정산담당자전화번호
            JSYYSEQ = "";               // 정산연번
            JBUNICD = "";               // 보험회사코드
            HOSID = "";                 // 요양기관 기호
            FMNO = "";                  // 서식번호
            GRPNO = "";                 // 묶음번호
            VERSION = "";               // 버전구분
            DCOUNT = "";                // 청구서 일련번호
            DEMUNITFG = "";             // 청구단위구분
            JRFG = "";                  // 보험자종별구분
            JSBUSSCD = "";              // 정산업무코드
            JSBUSSNM = "";              // 정산업무명
        }
    }
}
