using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7008E
{
    public class PtntChangedEventArgs : EventArgs
    {
        public int no;
        public string hosid; // 요양기관기호
        public string hosnm; // 요양기관명
        public string stemm; // 요양개시일(월)
        public string pnm; // 수진자 성명
        public string cnecno; // 접수변호
        public string cnecyy; // 접수년도
        public string hq_code; // 본부코드
        public string eprtno; // 명일련
        public string hang; // 항코드
        public string edicode; // 제품코드(edi코드)
        public string ediname; // 품목명(제품명)
        public string tqty; // 총 사용건수 및 사용량;

        // 결과 리턴용
        public bool Success { get; set; }
        public string FailureMessage { get; set; }

        public PtntChangedEventArgs(int no, string hosid, string hosnm, string stemm, string pnm, string cnecno, string cnecyy, string hq_code, string eprtno, string hang, string edicode, string ediname, string tqty)
        {
            this.no = no;
            this.hosid = hosid; ; // 요양기관기호
            this.hosnm = hosnm; // 요양기관명
            this.stemm = stemm; // 요양개시일(월)
            this.pnm = pnm; // 수진자 성명
            this.cnecno = cnecno; // 접수변호
            this.cnecyy = cnecyy; // 접수년도
            this.hq_code = hq_code; // 본부코드
            this.eprtno = eprtno; // 명일련
            this.hang = hang; // 항코드
            this.edicode = edicode; // 제품코드(edi코드)
            this.ediname = ediname; // 품목명(제품명)
            this.tqty = tqty; // 총 사용건수 및 사용량;

            this.Success = false;
            this.FailureMessage = "";
        }
    }
}
