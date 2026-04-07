using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RSS001_Data
    {

        public string OCD; // 수술비 수술처방코드
        public string ONM; // 수술비 수술처방명
        public string PRICD; // 수술비 수술수가코드
        public string ISPCD; // EDI 코드


        // ---------------------------------------------------------------------------------

        public string SOPR_NM // 수술명
        {
            get { return ONM; }
        }
        public string MDFEE_CD // 수술수가코드
        {
            get { return ISPCD=="" ? "00" : ISPCD; }
        }
    }
}
