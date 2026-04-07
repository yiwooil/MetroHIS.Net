using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0722E
{
    class CData
    {
        public long NO { get; set; }

        public string VERSION; // 버전구분
        public string JSDEMSEQ { get; set; } // 정산심사차수
        public string JSREDAY { get; set; } // 정산통보일자
        public string CNECNO { get; set; } // 접수번호
        public string DCOUNT { get; set; } // 청구서 일련번호
        public string FMNO; // 서식번호
        public string HOSID; // 요양기관 기호
        public string JIWONCD; // 지원코드
        public string DEMSEQ { get; set; } // 심사차수(yyyymm+seq(2))
        public string DEMNO { get; set; } // 청구번호
        public string GRPNO; // 묶음번호
        public string CNECYY; // 접수년도(CCYY)
        public string DEMUNITFG; // 청구단위구분
        public string JRFG; // 보험자종별구분
        public string JSYYSEQ { get; set; } // 정산연번
        public string SIMGBN { get; set; } // 심사구분
        public string JSREDPT1;
        public string JSREDPT2;
        public string JSREDPNM;
        public string JSREDPNO;
        public string JSRETELE; // 정산담당
        public string JSBUSSCD; // 정산업무코드
        public string JSBUSSNM; // 정산업무명
        public long SKPMGUM { get; set; } // 이전심결사항-본인부담환급금
        public long SKPMGUM1 { get; set; } // 이전심결사항-본인부담환급금1
        public long SKPMGUM2 { get; set; } // 이전심결사항-본인부담환급금2
        public long SKJJAMT { get; set; } // 이전심결사항-조정금액
        public long SKHOSRETAMT { get; set; } // 이전심결사항-요양기관환수금 합계
        public long SKUNAMT { get; set; } // 이전심결사항-보험자부담금
        public long SKBHUNAMT { get; set; } // 이전심결사항-보훈청구액
        public long SKRSTAMT { get; set; } // 이전심결사항-심사결정액
        public long SKCNT { get; set; } // 이전심결사항-건수합계
        public long JSPMGUM { get; set; } // 정산심결사항-본인부담환급금
        public long JSPMGUM1 { get; set; } // 정산심결사항-본인부담환급금1
        public long JSPMGUM2 { get; set; } // 정산심결사항-본인부담환급금2
        public long JSJJAMT { get; set; } // 정산심결사항-조정금액
        public long JSHOSRETAMT { get; set; } // 정산심결사항-요양기관환수금 합계
        public long JSUNAMT { get; set; } // 정산심결사항-보험자부담금 합계
        public long JSBHUNAMT { get; set; } // 정산심결사항-보훈부담금
        public long JSRSTAMT { get; set; } // 정산심결사항-심사결정액
        public long JSRSTCHAAMT { get; set; } // 정산심결사항-결정차액
        public long JSCNT { get; set; } // 정산심결사항-건수 합계
        public long SKBHPMGUM { get; set; }
        public long JSBHPMGUM { get; set; }
        public string MEMO { get; set; } // 명일련비고사항

        public string JSREDEPT
        {
            get
            {
                string ret = "";
                ret += JSREDPT1;
                ret += (ret != "" ? " " : "") + JSREDPT2;
                ret += (ret != "" ? " " : "") + JSREDPNM;
                ret += (ret != "" ? " " : "") + JSREDPNO;
                ret += (ret != "" ? " " : "") + JSRETELE;
                return ret;
            }
        }
        public string REMARK
        {
            get
            {
                string ret = "";
                ret += JSREDEPT;
                if (MEMO != "") ret += (ret != "" ? Environment.NewLine : "") + MEMO;
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
            CNECYY = ""; // 접수년도(CCYY)
            DEMUNITFG = ""; // 청구단위구분
            JRFG = ""; // 보험자종별구분
            JSYYSEQ = ""; // 정산연번
            SIMGBN = ""; // 심사구분
            JSREDPT1 = "";
            JSREDPT2 = "";
            JSREDPNM = "";
            JSREDPNO = "";
            JSRETELE = ""; // 정산담당
            JSBUSSCD = ""; // 정산업무코드
            JSBUSSNM = ""; // 정산업무명
            SKPMGUM = 0; // 이전심결사항-본인부담환급금
            SKPMGUM1 = 0; // 이전심결사항-본인부담환급금1
            SKPMGUM2 = 0; // 이전심결사항-본인부담환급금2
            SKJJAMT = 0; // 이전심결사항-조정금액
            SKHOSRETAMT = 0; // 이전심결사항-요양기관환수금 합계
            SKUNAMT = 0; // 이전심결사항-보험자부담금
            SKBHUNAMT = 0; // 이전심결사항-보훈청구액
            SKRSTAMT = 0; // 이전심결사항-심사결정액
            SKCNT = 0; // 이전심결사항-건수합계
            JSPMGUM = 0; // 정산심결사항-본인부담환급금
            JSPMGUM1 = 0; // 정산심결사항-본인부담환급금1
            JSPMGUM2 = 0; // 정산심결사항-본인부담환급금2
            JSJJAMT = 0; // 정산심결사항-조정금액
            JSHOSRETAMT = 0; // 정산심결사항-요양기관환수금 합계
            JSUNAMT = 0; // 정산심결사항-보험자부담금 합계
            JSBHUNAMT = 0; // 정산심결사항-보훈부담금
            JSRSTAMT = 0; // 정산심결사항-심사결정액
            JSRSTCHAAMT = 0; // 정산심결사항-결정차액
            JSCNT = 0; // 정산심결사항-건수 합계
            SKBHPMGUM = 0;
            JSBHPMGUM = 0;
            MEMO = ""; // 명일련비고사항
        }
    }
}
