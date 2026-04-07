using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RAA001_Mdct
    {
        public string ENTDT; // 투약일자
        public string ENTMS; // 투약시간
        public string ONM; // 약품명
        public string DQTY; // 1회투약량
        public string DUNIT; // 단위
        public string OCD; // 처방코드
        public string PRICD; // 수가코드

        public string NCT_KND_CD // 분류
        {
            get { return "9"; }
        }
        public string NCT_KND_CD_NM
        {
            get { return "기타"; }
        }
        public string MDCT_DT // 투약일시
        {
            get { return (ENTDT + ENTMS).Substring(0, 12); }
        }
        public string MDS_NM // 약품명
        {
            get { return ONM; }
        }
        public string FQ1_MDCT_QTY // 1회투약량
        {
            get { return DQTY; }
        }
        public string MDS_UNIT_TXT // 단위
        {
            get { return DUNIT; }
        }
    }
}
