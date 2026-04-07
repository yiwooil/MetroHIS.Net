using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD_EDI_SANJE
{
    class Cresult
    {
        public string minwonFg { get; set; } // 민원구분
        public string jeonsongNo { get; set; } // 전송번호
        public string cheongguCnt { get; set; } // 청구건수
        public string normalCnt { get; set; } // 정상명세서건수
        public string bulneungMss { get; set; } // 확인대상명세서건수
        public string stateFgNm { get; set; } // 상태명
    }
}
