using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RAR001_Emss
    {
        public string WDATE;
        public string WTIME;
        public string NRS;

        // 

        public string EXEC_DT // 평가일시
        {
            get { return WDATE + WTIME; }
        }
        public string ASM_RST_TXT // 결과
        {
            get { return NRS; }
        }

    }
}
