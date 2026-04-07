using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RAR001_Pain
    {
        public string WDATE; // 실시일자
        public string WTIME; // 실시시분
        public string TOOL; // 도구
        public string POW; // 결과

        public string EXEC_DT // 평가일시
        {
            get { return WDATE + WTIME; }
        }
        public string PAIN_ASM_TL_CD // 평가도구
        {
            get { return "9"; }
        }
        public string ASM_TL_ETC_TXT // 도구 상세
        {
            get { return TOOL; }
        }
        public string ASM_RST_TXT // 결과
        {
            get { return POW; }
        }
    }
}
