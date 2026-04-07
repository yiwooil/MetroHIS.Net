using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0723E
{
    class CData
    {
        public string VERSION { get; set; } // 버전구분
        public string JSDEMSEQ { get; set; } // 정산심사차수
        public string JSREDAY { get; set; } // 정산통보일자
        public string CNECNO { get; set; } // 접수번호
        public string DCOUNT { get; set; } // 청구서 일련번호
        public string FMNO { get; set; } // 서식번호
        public string HOSID { get; set; } // 요양기관 기호
        public string JIWONCD { get; set; } // 지원코드
        public string DEMSEQ { get; set; } // 심사차수
        public string DEMNO { get; set; } // 청구번호
        public string GRPNO { get; set; } // 묶음번호
        public string CNECYY { get; set; } // 접수년도(CCYY)
        public string DEMUNITFG { get; set; } // 청구단위구분
        public string JRFG { get; set; } // 보험자종별구분
        public string JSYYSEQ { get; set; } // 정산연번
        public string JSREDPT1 { get; set; } //
        public string JSREDPT2 { get; set; } //
        public string JSREDPNM { get; set; } //
        public string JSREDPNO { get; set; } //
        public string JSRETELE { get; set; } // 정산담당
        public string JSREDEPT
        {
            get
            {
                string ret = "";
                if (JSREDPT1 != "") ret += JSREDPT1 + " ";
                if (JSREDPT2 != "") ret += JSREDPT2 + " ";
                if (JSREDPNM != "") ret += JSREDPNM + " ";
                if (JSREDPNO != "") ret += JSREDPNO + " ";
                if (JSRETELE != "") ret += JSRETELE + " ";
                return ret.TrimEnd();
            }
        }
        public string JSBUSSCD { get; set; } // 정산업무코드
        public string JSBUSSNM { get; set; } // 정산업무명
        public long PMGUM { get; set; } // 본인부담환급금 합계
        public long PMGUM1 { get; set; } // 본인부담환급금1 합계
        public long PMGUM2 { get; set; } // 본인부담환급금2 합계
        public long BHPMGUM { get; set; } // 보훈 본인부담환급금 합계
        public long PPGUM { get; set; } // 환수금액 합계
        public long HOSRETAMT { get; set; } // 요양기관환수금 합계
        public long UNAMT { get; set; } // 보험자부담금 합계
        public long BHUNAMT { get; set; } // 보훈부담금 합계
        public long RSTAMT { get; set; } // 심사결정액 합계
        public long RSTCNT { get; set; } // 건수합계
        public string MEMO { get; set; } // 참조란
        public string MEMO_JSREDEPT// 참조란_정산담당
        {
            get
            {
                string ret = MEMO;
                if (ret != "") ret += " ";
                ret += JSREDEPT;
                return ret;
            }
        }

        public void Clear()
        {
            VERSION = ""; // 버전구분
            JSDEMSEQ = ""; // 정산심사차수
            JSREDAY = ""; // 정산통보일자
            CNECNO = ""; // 접수번호
            DCOUNT = ""; // 청구서 일련번호
            FMNO = ""; // 서식번호
            HOSID = ""; // 요양기관 기호
            JIWONCD = ""; // 지원코드
            DEMSEQ = ""; // 심사차수
            DEMNO = ""; // 청구번호
            GRPNO = ""; // 묶음번호
            CNECYY = ""; // 접수년도(CCYY)
            DEMUNITFG = ""; // 청구단위구분
            JRFG = ""; // 보험자종별구분
            JSYYSEQ = ""; // 정산연번
            JSREDPT1 = ""; //
            JSREDPT2 = ""; //
            JSREDPNM = ""; //
            JSREDPNO = ""; //
            JSRETELE = ""; // 정산담당
            JSBUSSCD = ""; // 정산업무코드
            JSBUSSNM = ""; // 정산업무명
            PMGUM = 0; // 본인부담환급금 합계
            PMGUM1 = 0; // 본인부담환급금1 합계
            PMGUM2 = 0; // 본인부담환급금2 합계
            BHPMGUM = 0; // 보훈 본인부담환급금 합계
            PPGUM = 0; // 환수금액 합계
            HOSRETAMT = 0; // 요양기관환수금 합계
            UNAMT = 0; // 보험자부담금 합계
            BHUNAMT = 0; // 보훈부담금 합계
            RSTAMT = 0; // 심사결정액 합계
            RSTCNT = 0; // 건수합계
            MEMO = ""; // 참조란
        }
    }
}
