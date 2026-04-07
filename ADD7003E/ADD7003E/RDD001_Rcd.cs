using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RDD001_Rcd
    {
        public string ODT; // 처방일
        public string OTM; // 처방시간
        public string RMK; // 비고
        public string DPTCD; // 처방과
        public string INSDPTCD;
        public string INSDPTCD2;
        public string EXDRID; // 처방의
        public string EXDRNM;
        public string ODIVCD; // 처방종류

        public string OCD; // 처방코드
        public string ONM;
        public string OQTY; // 용량
        public string ORDCNT; // 횟수
        public string FLDCD8; // 일수
        public string FLDCD4; // 용법
        public string OUNIT; // 단위

        // -----------------------------

        public string PRSC_DT // 처방일시
        {
            get { return ODT + OTM; }
        }
        public string PRSC_TXT // 처방내역
        {
            get
            {
                string ret = "";
                if (ODIVCD == "S")
                {
                    ret = RMK.Replace('$', ' ').Trim();
                }
                else
                {
                    float oqty = 0;
                    float.TryParse(OQTY, out oqty);
                    string oqty_string = "";
                    string ounit_string = "";
                    if (oqty != 0)
                    {
                        oqty_string = oqty.ToString();
                        ounit_string = OUNIT;
                    }
                    string day_string = FLDCD8 == "" ? "1" : FLDCD8;
                    ret = ONM + " " + oqty_string + " " + ounit_string + " " + ORDCNT + " 회 " + day_string + " 일 " + FLDCD4;
                }
                return ret;
            }
        }
        public string RMK_TXT // 비고
        {
            get
            {
                string ret = "";
                if (ODIVCD == "S")
                {
                    // 메시지 처방은 처방내역으로 출력
                }
                else
                {
                    ret = RMK.Replace('$', ' ').Trim();
                }
                return ret; ;
            }
        }
        public string DGSBJT_CD // 진료과목
        {
            get { return INSDPTCD; }
        }
        public string IFLD_DTL_SPC_SBJT_CD // 내과세부
        {
            get { return INSDPTCD == "01" ? INSDPTCD2 : ""; }
        }
        public string PRSC_DR_NM // 처방의사
        {
            get { return EXDRNM; }
        }

    }
}
