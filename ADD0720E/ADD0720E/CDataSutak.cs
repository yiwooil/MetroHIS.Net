using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0720E
{
    class CDataSutak
    {
        public long NO;

        public string JSDEMSEQ; // 정산심사차수
        public string JSREDAY; // 정산통보일자
        public string CNECNO; // 접수번호
        public string DCOUNT; // 청구서 일련번호
        public string JSYYSEQ; // 정산연번
        public string JSSEQNO; // 정산일련번호
        public string EPRTNO; // 명세서 일련번호
        public string LNO; // 줄번호
        public string SUTAKID; // 수탁기관기호
        public string SUTAKAMT; // 위탁검사직접지급금
        public string OPRCD; // 처리코드(미사용)
        public string MEMO; // 비고사항
        public string DEMNO;

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
            LNO = ""; // 줄번호
            SUTAKID = ""; // 수탁기관기호
            SUTAKAMT = ""; // 위탁검사직접지급금
            OPRCD = ""; // 처리코드(미사용)
            MEMO = ""; // 비고사항
            DEMNO = "";
        }
    }
}
