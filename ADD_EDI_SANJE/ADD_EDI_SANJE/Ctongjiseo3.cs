using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD_EDI_SANJE
{
    class Ctongjiseo3
    {
        public string jeopsuNo { get; set; } // 접수번호
        public string jeopsuNoSaengseongFg { get; set; } // 접수번호생성구분
        public string mssSer { get; set; } // 명일련
        public string julNo { get; set; } // 줄번호
        public string hangmokNo { get; set; } // 항목번호
        public string cdFg { get; set; } // 코드구분
        public string cd { get; set; } // 코드
        public string cdNm { get; set; } // 코드명
        public string jeongjeongCd { get; set; } // 정정코드
        public string jeongjeongCdNm { get; set; } // 정정코드명
        public string danga { get; set; } // 단가
        public string jeongjeongDanga { get; set; } // 정정단가
        public string qty { get; set; } // 수량
        public string injeongQty { get; set; } // 인정수량
        public string ilsu { get; set; } // 일수
        public string injeongIlsu { get; set; } // 인정일수
        public string totInjeongQty { get; set; } // 총인정수량
        public string prc { get; set; } // 금액
        public string jojeongPrc { get; set; } // 조정금액
        public string jojeongBulneungCd { get; set; } // 조정불능코드
        public string jojeongSayu { get; set; } // 조정사유
    }
}
