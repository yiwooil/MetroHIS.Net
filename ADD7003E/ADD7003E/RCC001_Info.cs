using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RCC001_Info
    {
        public int R_CNT;

        public string ODT; // 의뢰일자
        public string FLDCD9; // 의뢰내용
        public string REQDPTCD; // 의뢰과
        public string REQINSDPTCD;
        public string REQINSDPTCD2;
        public string REQDID; // 의뢰의사
        public string REQDNM;
        public string REPLYDT; // 회신일자
        public string REPLY; // 회신내용
        public string CSTDPTCD; // 회신과
        public string CSTINSDPTCD;
        public string CSTINSDPTCD2;
        public string CSTDRID; // 회신의사
        public string CSTDRNM;




        // ------------------------------

        public string REQ_DT //의뢰일시
        {
            get { return ODT == "" ? "" : ODT + "0000"; }
        }
        public string REQ_TXT //의뢰내용
        {
            get { return FLDCD9; }
        }
        public string REQ_DGSBJT_CD //의뢰과
        {
            get { return REQINSDPTCD; }
        }
        public string REQ_IFLD_DTL_SPC_SBJT_CD //내과세부
        {
            get { return REQINSDPTCD == "01" ? REQINSDPTCD2 : ""; }
        }
        public string REQ_DR_NM //의뢰의사
        {
            get { return REQDNM; }
        }
        public string RPY_DT //회신일시
        {
            get { return REPLY == "" ? "" : REPLYDT + "0000"; }
        }
        public string RPY_TXT //회신내용
        {
            get { return REPLY; }
        }
        public string RPY_DGSBJT_CD //회신과
        {
            get { return CSTINSDPTCD; }
        }
        public string RPY_IFLD_DTL_SPC_SBJT_CD //내과세부
        {
            get { return CSTINSDPTCD=="01" ?  CSTINSDPTCD2 : ""; }
        }
        public string RPY_DR_NM //회신의사
        {
            get { return CSTDRNM; }
        }


        public void Clear()
        {
            R_CNT = 0;

            ODT = ""; // 의뢰일자
            FLDCD9 = ""; // 의뢰내용
            REQDPTCD = ""; // 의뢰과
            REQINSDPTCD = "";
            REQINSDPTCD2 = "";
            REQDID = ""; // 의뢰의사
            REQDNM = "";
            REPLYDT = ""; // 회신일자
            REPLY = ""; // 회신내용
            CSTDPTCD = ""; // 회신과
            CSTINSDPTCD = "";
            CSTINSDPTCD2 = "";
            CSTDRID = ""; // 회신의사
            CSTDRNM = "";
        }
    
    }
}
