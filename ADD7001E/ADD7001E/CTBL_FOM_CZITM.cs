using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7001E
{
    class CTBL_FOM_CZITM
    {
        public string SORT_SNO { get; set; }
        public string YADM_TRMN_ID { get; set; }
        public string YADM_TRMN_NM { get; set; }
        public string DTL_TXT { get; set; }
        public string LABEL_NM { get; set; }
        public string FOM_CZITM_CD { get; set; }

        public CTBL_FOM_CZITM(string p_SORT_SNO, string p_YADM_TRMN_ID, string p_YADM_TRMN_NM, string p_DTL_TXT)
        {
            SORT_SNO = p_SORT_SNO;
            YADM_TRMN_ID = p_YADM_TRMN_ID;
            YADM_TRMN_NM = p_YADM_TRMN_NM;
            DTL_TXT = p_DTL_TXT;
            LABEL_NM = "";
            FOM_CZITM_CD = "";
        }
    }
}
