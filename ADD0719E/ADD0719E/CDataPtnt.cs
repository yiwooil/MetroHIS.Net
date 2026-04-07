using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0719E
{
    class CDataPtnt
    {
        public long NO { get; set; }

        public string EPRTNO { get; set; } // 명일련
        public string PNM { get; set; } // 수진자성명
        public string UNICD { get; set; } // 사업장(보장기관)기호
        public string INSID { get; set; } // 증번호(보장시설기호)
        public string JBFG { get; set; } // 의료급여종별구분
        public string GONSGB { get; set; } // 공상등구분
        public string REDPT { get; set; } // 심사담당조
        public long HOSRETAMT { get; set; } // 요양기관환수금
        public long PTRETAMT { get; set; } // 본인부담환급금
        public long PMGUM1 { get; set; } // 본인부담환급금1
        public long PMGUM2 { get; set; } // 본인부담환급금2
        public long UNRETAMT { get; set; } // 보험자(보장기관)부담환수금
        public long BHUNRETAMT { get; set; } // 보훈부담환수금
        public long BHPMGUM { get; set; } // 보훈본인부담환급금
        public string MEMO { get; set; } // 명일련비고사항
        public string DEMSEQ { get; set; } // 심사차수
        public string REDAY { get; set; } // 통보일자
        public string CNECNO { get; set; } // 접수번호
        public string DCOUNT { get; set; } // 청구서일련번호
        public string DEMNO { get; set; }

        public void Clear()
        {
            NO = 0;

            EPRTNO = ""; // 명일련
            PNM = ""; // 수진자성명
            UNICD = ""; // 사업장(보장기관)기호
            INSID = ""; // 증번호(보장시설기호)
            JBFG = ""; // 의료급여종별구분
            GONSGB = ""; // 공상등구분
            REDPT = ""; // 심사담당조
            HOSRETAMT = 0; // 요양기관환수금
            PTRETAMT = 0; // 본인부담환급금
            PMGUM1 = 0; // 본인부담환급금1
            PMGUM2 = 0; // 본인부담환급금2
            UNRETAMT = 0; // 보험자(보장기관)부담환수금
            BHUNRETAMT = 0; // 보훈부담환수금
            BHPMGUM = 0; // 보훈본인부담환급금
            MEMO = ""; // 명일련비고사항
            DEMSEQ = ""; // 심사차수
            REDAY = ""; // 통보일자
            CNECNO = ""; // 접수번호
            DCOUNT = ""; // 청구서일련번호
            DEMNO = "";
        }

    }
}
