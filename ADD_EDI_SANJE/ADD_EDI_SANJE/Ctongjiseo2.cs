using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD_EDI_SANJE
{
    class Ctongjiseo2
    {
        public string jeopsuNo { get; set; } // 접수번호
        public string jeopsuNoSaengseongFg { get; set; } // 접수번호생성구분
        public string mssSer { get; set; } // 명일련
        public string jaehaejaNm { get; set; } // 환자명
        public string jinryoFg { get; set; } // 진료구분
        public string jaehaeDt { get; set; } // 재해발생일자
        public string jinryoGigan { get; set; } // 진료기간
        public string cheongguPrc { get; set; } // 청구액
        public string jojeongPrc { get; set; } // 조정액
        public string jigeupPrc { get; set; } // 지급액
        public string bulneungPrc { get; set; } // 불능액
        public string boryuPrc { get; set; } // 보류액
        public string changePrc { get; set; } // 급여종류변경액
        public string silJinryoIlsu { get; set; } // 신진료일수
        public string bulneungSayu { get; set; } // 불능사유
        public string tongjiSahang { get; set; } // 통지사항
    }
}
