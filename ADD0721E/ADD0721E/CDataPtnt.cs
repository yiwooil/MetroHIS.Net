using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0721E
{
    class CDataPtnt
    {
        public long NO { get; set; }

        public string JSDEMSEQ { get; set; }   // 정산통보번호
        public string JSREDAY { get; set; }   // 정산통보일자
        public string CNECNO { get; set; }   // 접수번호
        public string DCOUNT { get; set; }   // 청구서 일련번호
        public string JSYYSEQ { get; set; }   // 정산연번
        public string JSSEQNO { get; set; }   // 정산일련번호
        public string EPRTNO { get; set; }   // 명세서 일련번호
        public string PNM { get; set; }   // 수진자성명
        public string RESID { get; set; }   // 수진자주민등록번호
        public string INSNM { get; set; }   // 세대주 성명
        public string UNICD { get; set; }   // 보장기관기호
        public string JRHT { get; set; }   // 진료형태
        public string JBFG { get; set; }   // 의료급여종별구분
        public string GUBUN { get; set; }   // 구분(1.환급 2.환수 3.환수+환급)
        public string INSID { get; set; }   // 증번호
        public string JAJR { get; set; }   // 정액정률구분(0.정액 9.정률 1.장기요양정액수가 2.장기요양행위별수가)
        public string STEDT { get; set; }   // 당월요양개시일
        public string SKTJKH { get; set; }   // 이전심결사항-특정기호
        public string DACD { get; set; }   // 상병분류기호
        public long JJSOGE1 { get; set; }   // 조정소계1
        public long JJSOGE2 { get; set; }   // 조정소계2
        public long SKPMGUM { get; set; }   // 이전심결사항-본인부담환급금
        public long SKPPGUM { get; set; }   // 이전심결사항-본인추가부담금
        public long SKTTAMT { get; set; }   // 이전심결사항-의료급여비용총액
        public long SKPTAMT { get; set; }   // 이전심결사항-본인부담금
        public long SKRELAM { get; set; }   // 이전심결사항-대불금
        public long SKJAM { get; set; }   // 이전심결사항-장애인의료비
        public long SKUNAMT { get; set; }   // 이전심결사항-보장기관부담금
        public long SKTTTAMT { get; set; }   // 이전심결사항-진료비총액
        public long SKBHUNAMT { get; set; }   // 이전심결사항-보훈청구액
        public long SKRSTAMT { get; set; }   // 이전심결사항-심사결정액
        public long SKSUTAKAMT { get; set; }   // 이전심결사항-위탁검사직접지급금
        public long SKEXAMC { get; set; }   // 이전심결사항-입원일수(내원일수)
        public long SKORDDAYS { get; set; }   // 이전심결사항-요양급여일수
        public long SKORDCNT { get; set; }   // 이전심결사항-처방횟수
        public string JSTJKH { get; set; }   // 정산심결사항-특정기호
        public long JSPMGUM { get; set; }   // 정산심결사항-본인부담환급금
        public long JSPPGUM { get; set; }   // 정산심결사항-본인추가부담금
        public long JSTTAMT { get; set; }   // 정산심결사항-의료급여비용총액
        public long JSPTAMT { get; set; }   // 정산심결사항-본인부담금
        public long JSRELAM { get; set; }   // 정산심결사항-대불금
        public long JSJAM { get; set; }   // 정산심결사항-장애인의료비
        public long JSUNAMT { get; set; }   // 정산심결사항-보장기관부담금
        public long JSTTTAMT { get; set; }   // 정산심결사항-진료비총액
        public long JSBHUNAMT { get; set; }   // 정산심결사항-보훈부담금
        public long JSRSTAMT { get; set; }   // 정산심결사항-심사결정액
        public long JSSUTAKAMT { get; set; }   // 정산심결사항-위탁검사직접지급금
        public long JSRSTCHAAMT { get; set; }   // 정산심결사항-결정차액
        public long JSEXAMC { get; set; }   // 정산심결사항-입원일수(내원일수)
        public long JSORDDAYS { get; set; }   // 정산심결사항-요양급여일수
        public long JSORDCNT { get; set; }   // 정산심결사항-처방횟수
        public string MEMO { get; set; }   // 명일련비고사항
        public string SKGONSGB { get; set; }
        public string SKSBRDNTYPE { get; set; }
        public long SKTSAMT { get; set; }
        public long SK100AMT { get; set; }
        public long SKUISAMT { get; set; }
        public string JSGONSGB { get; set; }
        public string JSSBRDNTYPE { get; set; }
        public long JSTSAMT { get; set; }
        public long JS100AMT { get; set; }
        public long JSUISAMT { get; set; }
        public long SKMKDRUGCNT { get; set; }   // 이전심결사항-직접조제횟수
        public long JSMKDRUGCNT { get; set; }   // 정산심결사항-직접조제횟수
        public long SKUPLMTCHATTAMT { get; set; }
        public long SKPTTTAMT { get; set; }
        public long JSUPLMTCHATTAMT { get; set; }
        public long JSPTTTAMT { get; set; }
        public long SKBHPMGUM { get; set; } // 보훈 본인부담환급금
        public long JSBHPMGUM { get; set; }
        public long SKBHPPGUM { get; set; }
        public long JSBHPPGUM { get; set; }
        public long SKBHPTAMT { get; set; }
        public long JSBHPTAMT { get; set; }
        public long BAKDNSKPMGUM { get; set; }   // 2014.12.16 KJW - 100/100미만 본인부담환급금
        public long BAKDNSKPPGUM { get; set; }   // 2014.12.16 KJW - 100/100미만 본인추가부담금합계
        public long BAKDNSKTTAMT { get; set; }   // 2014.12.16 KJW - 100/100미만 총액합계
        public long BAKDNSKPTAMT { get; set; }   // 2014.12.16 KJW - 100/100미만 본인일부부담금합계
        public long BAKDNSKUNAMT { get; set; }   // 2014.12.16 KJW - 100/100미만 청구액합계
        public long BAKDNSKBHUNAMT { get; set; }   // 2014.12.16 KJW - 100/100미만 보훈청구액합계
        public long BAKDNJSPMGUM { get; set; }   // 2014.12.16 KJW - 100/100미만 본인부담환급금
        public long BAKDNJSPPGUM { get; set; }   // 2014.12.16 KJW - 100/100미만 본인추가부담금합계
        public long BAKDNJSTTAMT { get; set; }   // 2014.12.16 KJW - 100/100미만 총액합계
        public long BAKDNJSPTAMT { get; set; }   // 2014.12.16 KJW - 100/100미만 본인일부부담금합계
        public long BAKDNJSUNAMT { get; set; }   // 2014.12.16 KJW - 100/100미만 청구액합계
        public long BAKDNJSBHUNAMT { get; set; }   // 2014.12.16 KJW - 100/100미만 보훈청구액합계
        public long SKSANGKYERFAMT { get; set; }   // 2014.12.16 KJW - 상계환급금(이전)
        public long SKSANGKYEADDAMT { get; set; }   // 2014.12.16 KJW - 상계추가부담금
        public long JSSANGKYERFAMT { get; set; }   // 2014.12.16 KJW - 상계환급금(정산)
        public long JSSANGKYEADDAMT { get; set; }   // 2014.12.16 KJW - 상계추가부담금
        public long SKBAKAMT { get; set; }
        public long JSBAKAMT { get; set; }
        public string DEMNO { get; set; }

        public string JJSOGE1X { get { return ""; } }   // 조정소계1X
        public string JJSOGE2X { get { return ""; } }   // 조정소계2X

        public void Clear()
        {
            NO = 0;

            JSDEMSEQ = "";   // 정산통보번호
            JSREDAY = "";   // 정산통보일자
            CNECNO = "";   // 접수번호
            DCOUNT = "";   // 청구서 일련번호
            JSYYSEQ = "";   // 정산연번
            JSSEQNO = "";   // 정산일련번호
            EPRTNO = "";   // 명세서 일련번호
            PNM = "";   // 수진자성명
            RESID = "";   // 수진자주민등록번호
            INSNM = "";   // 세대주 성명
            UNICD = "";   // 보장기관기호
            JRHT = "";   // 진료형태
            JBFG = "";   // 의료급여종별구분
            GUBUN = "";   // 구분(1.환급 2.환수 3.환수+환급)
            INSID = "";   // 증번호
            JAJR = "";   // 정액정률구분(0.정액 9.정률 1.장기요양정액수가 2.장기요양행위별수가)
            STEDT = "";   // 당월요양개시일
            SKTJKH = "";   // 이전심결사항-특정기호
            DACD = "";   // 상병분류기호
            JJSOGE1 = 0;   // 조정소계1
            JJSOGE2 = 0;   // 조정소계2
            SKPMGUM = 0;   // 이전심결사항-본인부담환급금
            SKPPGUM = 0;   // 이전심결사항-본인추가부담금
            SKTTAMT = 0;   // 이전심결사항-의료급여비용총액
            SKPTAMT = 0;   // 이전심결사항-본인부담금
            SKRELAM = 0;   // 이전심결사항-대불금
            SKJAM = 0;   // 이전심결사항-장애인의료비
            SKUNAMT = 0;   // 이전심결사항-보장기관부담금
            SKTTTAMT = 0;   // 이전심결사항-진료비총액
            SKBHUNAMT = 0;   // 이전심결사항-보훈청구액
            SKRSTAMT = 0;   // 이전심결사항-심사결정액
            SKSUTAKAMT = 0;   // 이전심결사항-위탁검사직접지급금
            SKEXAMC = 0;   // 이전심결사항-입원일수(내원일수)
            SKORDDAYS = 0;   // 이전심결사항-요양급여일수
            SKORDCNT = 0;   // 이전심결사항-처방횟수
            JSTJKH = "";   // 정산심결사항-특정기호
            JSPMGUM = 0;   // 정산심결사항-본인부담환급금
            JSPPGUM = 0;   // 정산심결사항-본인추가부담금
            JSTTAMT = 0;   // 정산심결사항-의료급여비용총액
            JSPTAMT = 0;   // 정산심결사항-본인부담금
            JSRELAM = 0;   // 정산심결사항-대불금
            JSJAM = 0;   // 정산심결사항-장애인의료비
            JSUNAMT = 0;   // 정산심결사항-보장기관부담금
            JSTTTAMT = 0;   // 정산심결사항-진료비총액
            JSBHUNAMT = 0;   // 정산심결사항-보훈부담금
            JSRSTAMT = 0;   // 정산심결사항-심사결정액
            JSSUTAKAMT = 0;   // 정산심결사항-위탁검사직접지급금
            JSRSTCHAAMT = 0;   // 정산심결사항-결정차액
            JSEXAMC = 0;   // 정산심결사항-입원일수(내원일수)
            JSORDDAYS = 0;   // 정산심결사항-요양급여일수
            JSORDCNT = 0;   // 정산심결사항-처방횟수
            MEMO = "";   // 명일련비고사항
            SKGONSGB = "";
            SKSBRDNTYPE = "";
            SKTSAMT = 0;
            SK100AMT = 0;
            SKUISAMT = 0;
            JSGONSGB = "";
            JSSBRDNTYPE = "";
            JSTSAMT = 0;
            JS100AMT = 0;
            JSUISAMT = 0;
            SKMKDRUGCNT = 0;   // 이전심결사항-직접조제횟수
            JSMKDRUGCNT = 0;   // 정산심결사항-직접조제횟수
            SKUPLMTCHATTAMT = 0;
            SKPTTTAMT = 0;
            JSUPLMTCHATTAMT = 0;
            JSPTTTAMT = 0;
            SKBHPMGUM = 0; // 보훈 본인부담환급금
            JSBHPMGUM = 0;
            SKBHPPGUM = 0;
            JSBHPPGUM = 0;
            SKBHPTAMT = 0;
            JSBHPTAMT = 0;
            BAKDNSKPMGUM = 0;   // 2014.12.16 KJW - 100/100미만 본인부담환급금
            BAKDNSKPPGUM = 0;   // 2014.12.16 KJW - 100/100미만 본인추가부담금합계
            BAKDNSKTTAMT = 0;   // 2014.12.16 KJW - 100/100미만 총액합계
            BAKDNSKPTAMT = 0;   // 2014.12.16 KJW - 100/100미만 본인일부부담금합계
            BAKDNSKUNAMT = 0;   // 2014.12.16 KJW - 100/100미만 청구액합계
            BAKDNSKBHUNAMT = 0;   // 2014.12.16 KJW - 100/100미만 보훈청구액합계
            BAKDNJSPMGUM = 0;   // 2014.12.16 KJW - 100/100미만 본인부담환급금
            BAKDNJSPPGUM = 0;   // 2014.12.16 KJW - 100/100미만 본인추가부담금합계
            BAKDNJSTTAMT = 0;   // 2014.12.16 KJW - 100/100미만 총액합계
            BAKDNJSPTAMT = 0;   // 2014.12.16 KJW - 100/100미만 본인일부부담금합계
            BAKDNJSUNAMT = 0;   // 2014.12.16 KJW - 100/100미만 청구액합계
            BAKDNJSBHUNAMT = 0;   // 2014.12.16 KJW - 100/100미만 보훈청구액합계
            SKSANGKYERFAMT = 0;   // 2014.12.16 KJW - 상계환급금(이전)
            SKSANGKYEADDAMT = 0;   // 2014.12.16 KJW - 상계추가부담금
            JSSANGKYERFAMT = 0;   // 2014.12.16 KJW - 상계환급금(정산)
            JSSANGKYEADDAMT = 0;   // 2014.12.16 KJW - 상계추가부담금
            SKBAKAMT = 0;
            JSBAKAMT = 0;
            DEMNO = "";
        }
    }
}
