using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD_EDI_SANJE
{
    class Ctongjiseo
    {
        public string jeopsuNo { get; set; } // 접수번호
        public string jinryoYymm { get; set; } // 진료년월
        public string saeopFg { get; set; } // 사업구분
        public string cheongguFg { get; set; } // 청구구분
        public string ndbhCheoriStatus { get; set; } // 처리상태
        public string tongboseoFg { get; set; } // 통보서명
        public string tongboseoStatus { get; set; } // 통지서상태
        public string tongboDt { get; set; } // 통보일자
    }
}
