using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0721E
{
    class CData
    {
        public long NO { get; set; }

        public string VERSION; // 버전구분
        public string JSDEMSEQ { get; set; } // 정산통보번호
        public string JSREDAY { get; set; } // 정산통보일자
        public string CNECNO { get; set; } // 접수번호
        public string DCOUNT { get; set; } // 청구서 일련번호
        public string FMNO { get; set; } // 서식번호
        public string HOSID { get; set; } // 요양기관 기호
        public string JIWONCD { get; set; } // 지원코드
        public string DEMSEQ { get; set; } // 심사차수(yyyymm+seq(2))
        public string DEMNO { get; set; } // 청구번호
        public string GRPNO { get; set; } // 묶음번호
        public string DEMUNITFG { get; set; } // 청구단위구분
        public string JSYYSEQ { get; set; } // 정산연번
        public string SIMGBN { get; set; } // 심사구분
        public string JSREDPT1; // 정산담당부명
        public string JSREDPT2; // 정산담당조명
        public string JSREDPNM; // 정산담당자명
        public string JSREDPNO; // 정산담당자번호
        public string JSRETELE; // 정산담당자전화번호
        public string JSBUSSCD; // 정산업무코드
        public string JSBUSSNM; // 정산업무명
        public long SKPMGUM { get; set; } // 이전심결사항-본인부담환급금 합계
        public long SKPPGUM { get; set; } // 이전심결사항-본인추가부담금 합계
        public long SKTTAMT { get; set; } // 이전심결사항-의료급여비용총액 합계
        public long SKPTAMT { get; set; } // 이전심결사항-본인부담금 합계
        public long SKRELAM { get; set; } // 이전심결사항-대불금 합계
        public long SKJAM { get; set; } // 이전심결사항-장애인의료비
        public long SKUNAMT { get; set; } // 이전심결사항-보장기관부담금 합계
        public long SKBHUNAMT { get; set; } // 이전심결사항-보훈청구액 합계
        public long SKRSTAMT { get; set; } // 이전심결사항-심사결정액 합계
        public long SKTTTAMT { get; set; } // 이전심결사항-진료비총액
        public long SKSUTAKAMT { get; set; } // 이전심결사항-위탁검사직접지급금 합계
        public long SKQFDSCNT { get; set; } // 이전심결사항-수급권확인대상건수 합계
        public long SKCNT { get; set; } // 이전심결사항-건수합계
        public long JSPMGUM { get; set; } // 정산심결사항-본인부담환급금 합계
        public long JSPPGUM { get; set; } // 정산심결사항-본인추가부담금 합계
        public long JSTTAMT { get; set; } // 정산심결사항-의료급여비용총액 합계
        public long JSPTAMT { get; set; } // 정산심결사항-본인부담금 합계
        public long JSRELAM { get; set; } // 정산심결사항-대불금 합계
        public long JSJAM { get; set; } // 정산심결사항-장애인의료비
        public long JSUNAMT { get; set; } // 정산심결사항-보장기관부담금 합계
        public long JSBHUNAMT { get; set; } // 정산심결사항-보훈부담금 합계
        public long JSRSTAMT { get; set; } // 정산심결사항-심사결정액 합계
        public long JSTTTAMT { get; set; } // 정산심결사항-진료비총액 합계
        public long JSSUTAKAMT { get; set; } // 정산심결사항-위탁검사직접지급금 합계
        public long JSRSTCHAAMT { get; set; } // 정산심결사항-결정차액 합계
        public long JSQFDSCNT { get; set; } // 정산심결사항-수급권확인대상건수 합계
        public long JSCNT { get; set; } // 정산심결사항-건수 합계
        public long JSSKAMT { get; set; } // 정산심사결과인정(조정)금액합계
        public long JSSKCNT { get; set; } // 정산심사결과인정(조정)건수합계
        public string MEMO { get; set; } // 참조란
        public long SKUPLMTCHATTAMT { get; set; } //
        public long SKPTTTAMT { get; set; } //
        public long JSUPLMTCHATTAMT { get; set; } //
        public long JSPTTTAMT { get; set; } //
        public long JJUPLMTCHATTAMT { get; set; } //
        public long SKBAKAMT { get; set; } //
        public long JSBAKAMT { get; set; } //
        public long SKBHPMGUM { get; set; } //
        public long JSBHPMGUM { get; set; } //
        public long SKBHPPGUM { get; set; } //
        public long JSBHPPGUM { get; set; } //
        public long SKBHPTAMT { get; set; } // 이전심결사항-보훈본인일부부담금
        public long JSBHPTAMT { get; set; } //
        public long BAKDNSKPMGUM { get; set; } // 2014.12.04 KJW - 100/100미만 본인부담환급금
        public long BAKDNSKPPGUM { get; set; } // 2014.12.04 KJW - 100/100미만 본인추가부담금합계
        public long BAKDNSKTTAMT { get; set; } // 2014.12.04 KJW - 100/100미만 총액합계
        public long BAKDNSKPTAMT { get; set; } // 2014.12.04 KJW - 100/100미만 본인일부부담금합계
        public long BAKDNSKUNAMT { get; set; } // 2014.12.04 KJW - 100/100미만 청구액합계
        public long BAKDNSKBHUNAMT { get; set; } // 2014.12.04 KJW - 100/100미만 보훈청구액합계
        public long BAKDNJSPMGUM { get; set; } // 2014.12.04 KJW - 100/100미만 본인부담환급금
        public long BAKDNJSPPGUM { get; set; } // 2014.12.04 KJW - 100/100미만 본인추가부담금합계
        public long BAKDNJSTTAMT { get; set; } // 2014.12.04 KJW - 100/100미만 총액합계
        public long BAKDNJSPTAMT { get; set; } // 2014.12.04 KJW - 100/100미만 본인일부부담금합계
        public long BAKDNJSUNAMT { get; set; } // 2014.12.04 KJW - 100/100미만 청구액합계
        public long BAKDNJSBHUNAMT { get; set; } // 2014.12.04 KJW - 100/100미만 보훈청구액합계
        public long SKSANGKYERFAMT { get; set; } // 2014.12.04 KJW - 상계환급금(이전)
        public long SKSANGKYEADDAMT { get; set; } // 2014.12.04 KJW - 상계추가부담금
        public long JSSANGKYERFAMT { get; set; } // 2014.12.04 KJW - 상계환급금(정산)
        public long JSSANGKYEADDAMT { get; set; } // 2014.12.04 KJW - 상계추가부담금

        public string JSREDEPT
        {
            get
            {
                string ret = "";// JSREDPT1; // 정산담당부명
                ret += (ret != "" ? " " : "") + JSREDPT2; // 정산담당조명
                ret += (ret != "" ? " " : "") + JSREDPNM; // 정산담당자명
                ret += (ret != "" ? " " : "") + JSREDPNO; // 정산담당자번호
                ret += (ret != "" ? " " : "") + JSRETELE; // 정산담당자전화번호
                return ret;
            }
        }
        public string JSBUSS
        {
            get
            {
                string ret = JSBUSSCD + "." + JSBUSSNM;
                return ret;
            }
        }

        public void Clear()
        {
            NO = 0;

            VERSION = ""; // 버전구분
            JSDEMSEQ = ""; // 정산통보번호
            JSREDAY = ""; // 정산통보일자
            CNECNO = ""; // 접수번호
            DCOUNT = ""; // 청구서 일련번호
            FMNO = ""; // 서식번호
            HOSID = ""; // 요양기관 기호
            JIWONCD = ""; // 지원코드
            DEMSEQ = ""; // 심사차수(yyyymm+seq(2))
            DEMNO = ""; // 청구번호
            GRPNO = ""; // 묶음번호
            DEMUNITFG = ""; // 청구단위구분
            JSYYSEQ = ""; // 정산연번
            SIMGBN = ""; // 심사구분
            JSREDPT1 = ""; // 정산담당부명
            JSREDPT2 = ""; // 정산담당조명
            JSREDPNM = ""; // 정산담당자명
            JSREDPNO = ""; // 정산담당자번호
            JSRETELE = ""; // 정산담당자전화번호
            JSBUSSCD = ""; // 정산업무코드
            JSBUSSNM = ""; // 정산업무명
            SKPMGUM = 0; // 이전심결사항-본인부담환급금 합계
            SKPPGUM = 0; // 이전심결사항-본인추가부담금 합계
            SKTTAMT = 0; // 이전심결사항-의료급여비용총액 합계
            SKPTAMT = 0; // 이전심결사항-본인부담금 합계
            SKRELAM = 0; // 이전심결사항-대불금 합계
            SKJAM = 0; // 이전심결사항-장애인의료비
            SKUNAMT = 0; // 이전심결사항-보장기관부담금 합계
            SKBHUNAMT = 0; // 이전심결사항-보훈청구액 합계
            SKRSTAMT = 0; // 이전심결사항-심사결정액 합계
            SKTTTAMT = 0; // 이전심결사항-진료비총액
            SKSUTAKAMT = 0; // 이전심결사항-위탁검사직접지급금 합계
            SKQFDSCNT = 0; // 이전심결사항-수급권확인대상건수 합계
            SKCNT = 0; // 이전심결사항-건수합계
            JSPMGUM = 0; // 정산심결사항-본인부담환급금 합계
            JSPPGUM = 0; // 정산심결사항-본인추가부담금 합계
            JSTTAMT = 0; // 정산심결사항-의료급여비용총액 합계
            JSPTAMT = 0; // 정산심결사항-본인부담금 합계
            JSRELAM = 0; // 정산심결사항-대불금 합계
            JSJAM = 0; // 정산심결사항-장애인의료비
            JSUNAMT = 0; // 정산심결사항-보장기관부담금 합계
            JSBHUNAMT = 0; // 정산심결사항-보훈부담금 합계
            JSRSTAMT = 0; // 정산심결사항-심사결정액 합계
            JSTTTAMT = 0; // 정산심결사항-진료비총액 합계
            JSSUTAKAMT = 0; // 정산심결사항-위탁검사직접지급금 합계
            JSRSTCHAAMT = 0; // 정산심결사항-결정차액 합계
            JSQFDSCNT = 0; // 정산심결사항-수급권확인대상건수 합계
            JSCNT = 0; // 정산심결사항-건수 합계
            JSSKAMT = 0; // 정산심사결과인정(조정)금액합계
            JSSKCNT = 0; // 정산심사결과인정(조정)건수합계
            MEMO = ""; // 참조란
            SKUPLMTCHATTAMT = 0; //
            SKPTTTAMT = 0; //
            JSUPLMTCHATTAMT = 0; //
            JSPTTTAMT = 0; //
            JJUPLMTCHATTAMT = 0; //
            SKBAKAMT = 0; // 이전심결사항-의료급여 100분의100 본인부담금총액
            JSBAKAMT = 0; //
            SKBHPMGUM = 0; // 이전심결사항-보훈본인부담환급금
            JSBHPMGUM = 0; //
            SKBHPPGUM = 0; // 이전심결사항-보훈본인추가부담금
            JSBHPPGUM = 0; //
            SKBHPTAMT = 0; // 이전심결사항-보훈 본인일부부담금
            JSBHPTAMT = 0; //
            BAKDNSKPMGUM = 0; // 2014.12.04 KJW - 100/100미만 본인부담환급금
            BAKDNSKPPGUM = 0; // 2014.12.04 KJW - 100/100미만 본인추가부담금합계
            BAKDNSKTTAMT = 0; // 2014.12.04 KJW - 100/100미만 총액합계
            BAKDNSKPTAMT = 0; // 2014.12.04 KJW - 100/100미만 본인일부부담금합계
            BAKDNSKUNAMT = 0; // 2014.12.04 KJW - 100/100미만 청구액합계
            BAKDNSKBHUNAMT = 0; // 2014.12.04 KJW - 100/100미만 보훈청구액합계
            BAKDNJSPMGUM = 0; // 2014.12.04 KJW - 100/100미만 본인부담환급금
            BAKDNJSPPGUM = 0; // 2014.12.04 KJW - 100/100미만 본인추가부담금합계
            BAKDNJSTTAMT = 0; // 2014.12.04 KJW - 100/100미만 총액합계
            BAKDNJSPTAMT = 0; // 2014.12.04 KJW - 100/100미만 본인일부부담금합계
            BAKDNJSUNAMT = 0; // 2014.12.04 KJW - 100/100미만 청구액합계
            BAKDNJSBHUNAMT = 0; // 2014.12.04 KJW - 100/100미만 보훈청구액합계
            SKSANGKYERFAMT = 0; // 2014.12.04 KJW - 상계환급금(이전)
            SKSANGKYEADDAMT = 0; // 2014.12.04 KJW - 상계추가부담금
            JSSANGKYERFAMT = 0; // 2014.12.04 KJW - 상계환급금(정산)
            JSSANGKYEADDAMT = 0; // 2014.12.04 KJW - 상계추가부담금
        }
    }
}
