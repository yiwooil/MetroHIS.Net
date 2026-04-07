using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0720E
{
    class CData
    {
        public long NO { get; set; }

        public string VERSION { get; set; } // 버전구분
        public string JSDEMSEQ { get; set; } // 정산심사차수
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
        public string JRFG { get; set; } // 보험자종별구분
        public string JSYYSEQ { get; set; } // 정산연번
        public string SIMGBN { get; set; } // 심사구분
        public string JSREDPT1 { get; set; } // 정산담당부명
        public string JSREDPT2 { get; set; } // 정산담당조명
        public string JSREDPNM { get; set; } // 정산담당자명
        public string JSREDPNO { get; set; } // 정산담당자번호
        public string JSRETELE { get; set; } // 정산담당자전화번호
        public string JSBUSSCD { get; set; } // 정산업무코드
        public string JSBUSSNM { get; set; } // 정산업무명
        public long SKPMGUM { get; set; } // 이전심결사항-본인부담환급금 합계
        public long SKPPGUM { get; set; } // 이전심결사항-본인추가부담금 합계
        public long SKTTAMT { get; set; } // 이전심결사항-요양급여비용총액 합계
        public long SKPTAMT { get; set; } // 이전심결사항-본인부담금 합계
        public long SKJIWONAMT { get; set; } // 이전심결사항-지원금
        public long SKJAM { get; set; } // 이전심결사항-장애인의료비
        public long SKMAXPTAMT { get; set; } // 이전심결사항-본인부담상한초과금 합계
        public long SKMAXPTCHAAMT { get; set; } //  이전심결사항-본인부담상한액차액 합계
        public long SKUNAMT { get; set; } // 이전심결사항-보험자부담금 합계
        public long SKBHUNAMT { get; set; } // 이전심결사항-보훈청구액 합계
        public long SKRSTAMT { get; set; } // 이전심결사항-심사결정액 합계
        public long SKSUTAKAMT { get; set; } // 이전심결사항-위탁검사직접지급금 합계
        public long SKCNT { get; set; } // 이전심결사항-건수합계
        public long JSPMGUM { get; set; } // 정산심결사항-본인부담환급금 합계
        public long JSPPGUM { get; set; } // 정산심결사항-본인추가부담금 합계
        public long JSTTAMT { get; set; } // 정산심결사항-요양급여비용총액 합계
        public long JSPTAMT { get; set; } // 정산심결사항-본인부담금 합계
        public long JSJIWONAMT { get; set; } // 정산심결사항-지원금
        public long JSJAM { get; set; } // 정산심결사항-장애인의료비
        public long JSMAXPTAMT { get; set; } // 정산심결사항-본인부담상한초과금 합계
        public long JSMAXPTCHAAMT { get; set; } // 정산심결사항-본인부담상한액차액 합계
        public long JSUNAMT { get; set; } // 정산심결사항-보험자부담금 합계
        public long JSBHUNAMT { get; set; } // 정산심결사항-보훈부담금 합계
        public long JSRSTAMT { get; set; } // 정산심결사항-심사결정액 합계
        public long JSSUTAKAMT { get; set; } // 정산심결사항-위탁검사직접지급금 합계
        public long JSRSTCHAAMT { get; set; } // 정산심결사항-결정차액 합계
        public long JSCNT { get; set; } // 정산심결사항-건수 합계
        public long JSSKAMT { get; set; } // 정산심사결과인정(조정)금액합계
        public long JSSKCNT { get; set; } // 정산심사결과인정(조정)건수합계
        public double SKCDJS { get; set; } // 이전심결사항-차등지수
        public long SKCJJCNT { get; set; } // 이전심결사항-진찰(조제)횟수
        public long SKCJDAYS { get; set; } // 이전심결사항-진료(조제)일수
        public long SKCJDR { get; set; } // 이전심결사항-의(약)사수
        public long SKCJJAMT { get; set; } // 이전심결사항-진찰(조제)료
        public long SKCJJMAMT { get; set; } // 이전심결사항-진찰료(조제료)차감액
        public double JSCDJS { get; set; } // 정산심결사항-차등지수
        public long JSCJJCNT { get; set; } // 정산심결사항-진찰(조제)횟수
        public long JSCJDAYS { get; set; } // 정산심결사항-진료(조제)일수
        public long JSCJDR { get; set; } // 정산심결사항-의(약)사수
        public long JSCJJAMT { get; set; } // 정산심결사항-진찰(조제)료
        public long JSCJJMAMT { get; set; } // 정산심결사항-진찰료(조제료)차감액
        public long JSCJRSTCHAAMT { get; set; } // 정산심결사항-차등지수결정차액
        public string MEMO { get; set; } // 명일련비고사항
        public long SKUPLMTCHATTAMT { get; set; } // 이전심결사항-약제상한차액
        public long SKPTTTAMT { get; set; } // 이전심결사항-수진자총액
        public long JSUPLMTCHATTAMT { get; set; } // 정산심결사항-약제상한차액
        public long JSPTTTAMT { get; set; } // 정산심결사항-수진자총액
        public long JJUPLMTCHATTAMT { get; set; } // 정산심결사항-상한차액조정금액
        public long SKBAKAMT { get; set; } // 이전심결사항-100/100총액
        public long JSBAKAMT { get; set; } // 정산심결사항-100/100총액
        public long SKBHPMGUM { get; set; } // 이전심결사항-보훈본인부담환급금
        public long JSBHPMGUM { get; set; } // 정산심결사항-보훈본인부담환급금
        public long SKBHPPGUM { get; set; } // 이전심결사항-보훈본인추가부담금
        public long JSBHPPGUM { get; set; } // 정산심결사항-보훈본인추가부담금
        public long SKBHPTAMT { get; set; } // 이전심결사항-보훈본인일부부담금
        public long JSBHPTAMT { get; set; } // 정산심결사항-보훈본인일부부담금
        public long BAKDNSKPMGUM { get; set; } // 2014.03.21 KJW - 이전심결사항-100/100미만 본인부담환급금합계
        public long BAKDNSKPPGUM { get; set; } // 2014.03.21 KJW - 이전심결사항-100/100미만 본인추가부담금합계
        public long BAKDNSKTTAMT { get; set; } // 2014.03.21 KJW - 이전심결사항-100/100미만 총액합계
        public long BAKDNSKPTAMT { get; set; } // 2014.03.21 KJW - 이전심결사항-100/100미만 본인일부부담금합계
        public long BAKDNSKUNAMT { get; set; } // 2014.03.21 KJW - 이전심결사항-100/100미만 청구액합계
        public long BAKDNSKBHUNAMT { get; set; } // 2014.03.21 KJW - 이전심결사항-100/100미만 보훈청구액합계
        public long BAKDNJSPMGUM { get; set; } // 2014.03.21 KJW - 정산심결사항-100/100미만 본인부담환급금합계
        public long BAKDNJSPPGUM { get; set; } // 2014.03.21 KJW - 정산심결사항-100/100미만 본인추가부담금합계
        public long BAKDNJSTTAMT { get; set; } // 2014.03.21 KJW - 정산심결사항-100/100미만 총액합계
        public long BAKDNJSPTAMT { get; set; } // 2014.03.21 KJW - 정산심결사항-100/100미만 본인일부부담금합계
        public long BAKDNJSUNAMT { get; set; } // 2014.03.21 KJW - 정산심결사항-100/100미만 청구액합계
        public long BAKDNJSBHUNAMT { get; set; } // 2014.03.21 KJW - 정산심결사항-100/100미만 보훈청구액합계
        public long SKSANGKYERFAMT { get; set; } // 2014.09.24 KJW - 이전심결사항-상계환급금
        public long SKSANGKYEADDAMT { get; set; } // 2014.09.24 KJW - 이전심결사항-상계추가부담금
        public long JSSANGKYERFAMT { get; set; } // 2014.09.24 KJW - 정산심결사항-상계환급금
        public long JSSANGKYEADDAMT { get; set; } // 2014.09.24 KJW - 정산심결사항-상계추가부담금
        public long JSCJRSTCHAAMT_X { get { return 0; } }

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
            JSDEMSEQ = ""; // 정산심사차수
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
            JRFG = ""; // 보험자종별구분
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
            SKTTAMT = 0; // 이전심결사항-요양급여비용총액 합계
            SKPTAMT = 0; // 이전심결사항-본인부담금 합계
            SKJIWONAMT = 0; // 이전심결사항-지원금
            SKJAM = 0; // 이전심결사항-장애인의료비
            SKMAXPTAMT = 0; // 이전심결사항-본인부담상한초과금 합계
            SKMAXPTCHAAMT = 0; //  이전심결사항-본인부담상한액차액 합계
            SKUNAMT = 0; // 이전심결사항-보험자부담금 합계
            SKBHUNAMT = 0; // 이전심결사항-보훈청구액 합계
            SKRSTAMT = 0; // 이전심결사항-심사결정액 합계
            SKSUTAKAMT = 0; // 이전심결사항-위탁검사직접지급금 합계
            SKCNT = 0; // 이전심결사항-건수합계
            JSPMGUM = 0; // 정산심결사항-본인부담환급금 합계
            JSPPGUM = 0; // 정산심결사항-본인추가부담금 합계
            JSTTAMT = 0; // 정산심결사항-요양급여비용총액 합계
            JSPTAMT = 0; // 정산심결사항-본인부담금 합계
            JSJIWONAMT = 0; // 정산심결사항-지원금
            JSJAM = 0; // 정산심결사항-장애인의료비
            JSMAXPTAMT = 0; // 정산심결사항-본인부담상한초과금 합계
            JSMAXPTCHAAMT = 0; // 정산심결사항-본인부담상한액차액 합계
            JSUNAMT = 0; // 정산심결사항-보험자부담금 합계
            JSBHUNAMT = 0; // 정산심결사항-보훈부담금 합계
            JSRSTAMT = 0; // 정산심결사항-심사결정액 합계
            JSSUTAKAMT = 0; // 정산심결사항-위탁검사직접지급금 합계
            JSRSTCHAAMT = 0; // 정산심결사항-결정차액 합계
            JSCNT = 0; // 정산심결사항-건수 합계
            JSSKAMT = 0; // 정산심사결과인정(조정)금액합계
            JSSKCNT = 0; // 정산심사결과인정(조정)건수합계
            SKCDJS = 0; // 이전심결사항-차등지수
            SKCJJCNT = 0; // 이전심결사항-진찰(조제)횟수
            SKCJDAYS = 0; // 이전심결사항-진료(조제)일수
            SKCJDR = 0; // 이전심결사항-의(약)사수
            SKCJJAMT = 0; // 이전심결사항-진찰(조제)료
            SKCJJMAMT = 0; // 이전심결사항-진찰료(조제료)차감액
            JSCDJS = 0; // 정산심결사항-차등지수
            JSCJJCNT = 0; // 정산심결사항-진찰(조제)횟수
            JSCJDAYS = 0; // 정산심결사항-진료(조제)일수
            JSCJDR = 0; // 정산심결사항-의(약)사수
            JSCJJAMT = 0; // 정산심결사항-진찰(조제)료
            JSCJJMAMT = 0; // 정산심결사항-진찰료(조제료)차감액
            JSCJRSTCHAAMT = 0; // 정산심결사항-차등지수결정차액
            MEMO = ""; // 명일련비고사항
            SKUPLMTCHATTAMT = 0; // 이전심결사항-약제상한차액
            SKPTTTAMT = 0; // 이전심결사항-수진자총액
            JSUPLMTCHATTAMT = 0; // 정산심결사항-약제상한차액
            JSPTTTAMT = 0; // 정산심결사항-수진자총액
            JJUPLMTCHATTAMT = 0; // 정산심결사항-상한차액조정금액
            SKBAKAMT = 0; // 이전심결사항-100/100총액
            JSBAKAMT = 0; // 정산심결사항-100/100총액
            SKBHPMGUM = 0; // 이전심결사항-보훈본인부담환급금
            JSBHPMGUM = 0; // 정산심결사항-보훈본인부담환급금
            SKBHPPGUM = 0; // 이전심결사항-보훈본인추가부담금
            JSBHPPGUM = 0; // 정산심결사항-보훈본인추가부담금
            SKBHPTAMT = 0; // 이전심결사항-보훈본인일부부담금
            JSBHPTAMT = 0; // 정산심결사항-보훈본인일부부담금
            BAKDNSKPMGUM = 0; // 2014.03.21 KJW - 이전심결사항-100/100미만 본인부담환급금합계
            BAKDNSKPPGUM = 0; // 2014.03.21 KJW - 이전심결사항-100/100미만 본인추가부담금합계
            BAKDNSKTTAMT = 0; // 2014.03.21 KJW - 이전심결사항-100/100미만 총액합계
            BAKDNSKPTAMT = 0; // 2014.03.21 KJW - 이전심결사항-100/100미만 본인일부부담금합계
            BAKDNSKUNAMT = 0; // 2014.03.21 KJW - 이전심결사항-100/100미만 청구액합계
            BAKDNSKBHUNAMT = 0; // 2014.03.21 KJW - 이전심결사항-100/100미만 보훈청구액합계
            BAKDNJSPMGUM = 0; // 2014.03.21 KJW - 정산심결사항-100/100미만 본인부담환급금합계
            BAKDNJSPPGUM = 0; // 2014.03.21 KJW - 정산심결사항-100/100미만 본인추가부담금합계
            BAKDNJSTTAMT = 0; // 2014.03.21 KJW - 정산심결사항-100/100미만 총액합계
            BAKDNJSPTAMT = 0; // 2014.03.21 KJW - 정산심결사항-100/100미만 본인일부부담금합계
            BAKDNJSUNAMT = 0; // 2014.03.21 KJW - 정산심결사항-100/100미만 청구액합계
            BAKDNJSBHUNAMT = 0; // 2014.03.21 KJW - 정산심결사항-100/100미만 보훈청구액합계
            SKSANGKYERFAMT = 0; // 2014.09.24 KJW - 이전심결사항-상계환급금
            SKSANGKYEADDAMT = 0; // 2014.09.24 KJW - 이전심결사항-상계추가부담금
            JSSANGKYERFAMT = 0; // 2014.09.24 KJW - 정산심결사항-상계환급금
            JSSANGKYEADDAMT = 0; // 2014.09.24 KJW - 정산심결사항-상계추가부담금
        }
    }
}
