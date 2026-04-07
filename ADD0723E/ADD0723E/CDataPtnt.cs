using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0723E
{
    class CDataPtnt
    {
        public string JSDEMSEQ { set; get; } // 정산심사차수
        public string JSREDAY { set; get; } // 정산통보일자
        public string CNECNO { set; get; } // 접수번호
        public string DCOUNT { set; get; } // 청구서 일련번호
        public string JSYYSEQ { set; get; } // 정산연번
        public string JSSEQNO { set; get; } // 정산일련번호
        public string EPRTNO { set; get; } // 명세서 일련번호
        public string PNM { set; get; } // 수진자성명
        public string INSNM { set; get; } // 가입자 성명
        public string UNICD { set; get; } // 사업장기호
        public string INSID { set; get; } // 증번호
        public string JBFG { set; get; } // 의료급여종별구분
        public string GONSGB { set; get; } //
        public long PMGUM { set; get; } // 본인부담환급금
        public long PMGUM1 { set; get; } // 본인부담환급금1
        public long PMGUM2 { set; get; } // 본인부담환급금2
        public long BHPMGUM { set; get; } // 보훈 본인부담환급금
        public long HOSRETAMT { set; get; } // 요양기관환수금(계)
        public long UNAMT { set; get; } // 보장기관 부담금
        public long BHUNAMT { set; get; } // 보훈부담금
        public long RSTAMT { set; get; } // 심사결정액
        public string MEMO { set; get; } // 명일련비고사항

        public void Clear()
        {
            JSDEMSEQ = ""; // 정산심사차수
            JSREDAY = ""; // 정산통보일자
            CNECNO = ""; // 접수번호
            DCOUNT = ""; // 청구서 일련번호
            JSYYSEQ = ""; // 정산연번
            JSSEQNO = ""; // 정산일련번호
            EPRTNO = ""; // 명세서 일련번호
            PNM = ""; // 수진자성명
            INSNM = ""; // 가입자 성명
            UNICD = ""; // 사업장기호
            INSID = ""; // 증번호
            JBFG = ""; // 의료급여종별구분
            GONSGB = ""; //
            PMGUM = 0; // 본인부담환급금
            PMGUM1 = 0; // 본인부담환급금1
            PMGUM2 = 0; // 본인부담환급금2
            BHPMGUM = 0; // 보훈 본인부담환급금
            HOSRETAMT = 0; // 요양기관환수금(계)
            UNAMT = 0; // 보장기관 부담금
            BHUNAMT = 0; // 보훈부담금
            RSTAMT = 0; // 심사결정액
            MEMO = ""; // 명일련비고사항
        }
    }
}
