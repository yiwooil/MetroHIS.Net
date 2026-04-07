using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0722E
{
    class CDataPtnt
    {
        public long NO { get; set; }

        public string JSDEMSEQ; // 정산심사차수
        public string JSREDAY; // 정산통보일자
        public string CNECNO; // 접수번호
        public string DCOUNT; // 청구서 일련번호
        public string JSYYSEQ; // 정산연번
        public string JSSEQNO; // 정산일련번호
        public string EPRTNO { get; set; } // 명세서 일련번호
        public string GUBUN { get; set; } // 구분(1.환급 2.환수 3.환수+환급)
        public string PNM { get; set; } // 수진자성명
        public string INSNM; // 가입자 성명
        public string UNICD { get; set; } // 보장기관기호
        public string INSID { get; set; } // 증번호
        public string JBFG { get; set; } // 의료급여종별구분
        public string SKGONSGB { get; set; } //
        public long SKPMGUM { get; set; } // 이전심결사항-본인부담환급금
        public long SKPMGUM1 { get; set; } // 이전심결사항-본인부담환급금1
        public long SKPMGUM2 { get; set; } // 이전심결사항-본인부담환급금2
        public long SKHOSRETAMT { get; set; } // 이전심결사항-요양기관환수금 합계
        public long SKUNAMT { get; set; } // 이전심결사항-보험자부담금
        public long SKBHUNAMT { get; set; } // 이전심결사항-보훈청구액
        public long SKRSTAMT { get; set; } // 이전심결사항-심사결정액
        public string JSGONSGB { get; set; } //
        public long JSPMGUM { get; set; } // 정산심결사항-본인부담환급금
        public long JSPMGUM1 { get; set; } // 정산심결사항-본인부담환급금1
        public long JSPMGUM2 { get; set; } // 정산심결사항-본인부담환급금2
        public long JSHOSRETAMT { get; set; } // 정산심결사항-요양기관환수금 합계
        public long JSUNAMT { get; set; } // 정산심결사항-보험자부담금 합계
        public long JSBHUNAMT { get; set; } // 정산심결사항-보훈부담금
        public long JSRSTAMT { get; set; } // 정산심결사항-심사결정액
        public long JSRSTCHAAMT { get; set; } // 정산심결사항-결정차액
        public long SKBHPMGUM { get; set; }
        public long JSBHPMGUM { get; set; }
        public string MEMO { get; set; } // 명일련비고사항

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
            GUBUN = ""; // 구분(1.환급 2.환수 3.환수+환급)
            PNM = ""; // 수진자성명
            INSNM = ""; // 가입자 성명
            UNICD = ""; // 보장기관기호
            INSID = ""; // 증번호
            JBFG = ""; // 의료급여종별구분
            SKGONSGB = ""; //
            SKPMGUM = 0; // 이전심결사항-본인부담환급금
            SKPMGUM1 = 0; // 이전심결사항-본인부담환급금1
            SKPMGUM2 = 0; // 이전심결사항-본인부담환급금2
            SKHOSRETAMT = 0; // 이전심결사항-요양기관환수금 합계
            SKUNAMT = 0; // 이전심결사항-보험자부담금
            SKBHUNAMT = 0; // 이전심결사항-보훈청구액
            SKRSTAMT = 0; // 이전심결사항-심사결정액
            JSGONSGB = ""; //
            JSPMGUM = 0; // 정산심결사항-본인부담환급금
            JSPMGUM1 = 0; // 정산심결사항-본인부담환급금1
            JSPMGUM2 = 0; // 정산심결사항-본인부담환급금2
            JSHOSRETAMT = 0; // 정산심결사항-요양기관환수금 합계
            JSUNAMT = 0; // 정산심결사항-보험자부담금 합계
            JSBHUNAMT = 0; // 정산심결사항-보훈부담금
            JSRSTAMT = 0; // 정산심결사항-심사결정액
            JSRSTCHAAMT = 0; // 정산심결사항-결정차액
            SKBHPMGUM = 0;
            JSBHPMGUM = 0;
            MEMO = ""; // 명일련비고사항
        }
    }
}
