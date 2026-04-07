using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0721E
{
    class CDataSutak
    {
        public long NO { get; set; }

        public string JSDEMSEQ { get; set; } // 정산통보번호
        public string JSREDAY { get; set; } // 정산통보일자
        public string CNECNO { get; set; } // 접수번호
        public string DCOUNT { get; set; } // 청구서 일련번호
        public string JSYYSEQ { get; set; } // 정산연번
        public string JSSEQNO { get; set; } // 정산일련번호
        public string EPRTNO { get; set; } // 명세서 일련번호
        public string LNO { get; set; } // 줄번호
        public string SUTAKID { get; set; } // 수탁기관기호
        public string SUTAKAMT { get; set; } // 위탁검사직접지급금
        public string OPRCD { get; set; } // 처리코드(미사용)
        public string MEMO { get; set; } // 비고사항
        public string DEMNO { get; set; }

        public void Clear()
        {
            NO = 0;

            JSDEMSEQ = ""; // 정산통보번호
            JSREDAY = ""; // 정산통보일자
            CNECNO = ""; // 접수번호
            DCOUNT = ""; // 청구서 일련번호
            JSYYSEQ = ""; // 정산연번
            JSSEQNO = ""; // 정산일련번호
            EPRTNO = ""; // 명세서 일련번호
            LNO = ""; // 줄번호
            SUTAKID = ""; // 수탁기관기호
            SUTAKAMT = ""; // 위탁검사직접지급금
            OPRCD = ""; // 처리코드(미사용)
            MEMO = ""; // 비고사항
            DEMNO = "";
        }
    }
}
