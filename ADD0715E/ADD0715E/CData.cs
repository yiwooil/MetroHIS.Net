using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0715E
{
    class CData
    {
        public string DEMNO { get; set; }
        public string CALLNO { get; set; }
        public string CALLPAFG;
        public string CALLPAFGNM
        {
            get
            {
                if (CALLPAFG.Trim() == "A") return "치료재료";
                if (CALLPAFG.Trim() == "C") return "조제.제제약";
                if (CALLPAFG.Trim() == "D") return "비급여약제";
                if (CALLPAFG.Trim() == "E") return "비급여행위등";
                return CALLPAFG.Trim();
            }
        }
        public int LNO { get; set; }
        public string MKDIV;
        public string MKDIVNM
        {
            get
            {
                if (MKDIV.Trim() == "1") return "조제";
                if (MKDIV.Trim() == "2") return "제제";
                return MKDIV.Trim();
            }
        }
        public string ABTFG;
        public string ABTFGNM
        {
            get
            {
                if (ABTFG.Trim() == "1") return "신규코드통보";
                if (ABTFG.Trim() == "2") return "코드확인불가";
                if (ABTFG.Trim() == "3") return "품목별 규격별 개당단가 산출불가";
                if (ABTFG.Trim() == "4") return "구입량 착오";
                if (ABTFG.Trim() == "5") return "약사법 제33조 동법 시행규칙 규정의 제제범위 품목이 아님";
                if (ABTFG.Trim() == "6") return "기타";
                return ABTFG.Trim();
            }
        }
        public string ITEMCD { get; set; }
        public string ITEMNM { get; set; }
        public string ABTTXT { get; set; }
        public string CALLTXT { get; set; }
        public int BYEQTY { get; set; }

        public void Clear()
        {
            DEMNO = "";
            CALLNO = "";
            CALLPAFG = "";
            LNO = 0;
            MKDIV = "";
            ABTFG = "";
            ITEMCD = "";
            ITEMNM = "";
            ABTTXT = "";
            CALLTXT = "";
            BYEQTY = 0;
        }
    }
}
