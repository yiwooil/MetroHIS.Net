using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD0719E
{
    class CData
    {
        public long NO { get; set; }

        public string DEMSEQ { get; set; } // 심사차수
        public string REDAY { get; set; } // 통보일자
        public string CNECNO { get; set; } // 접수번호
        public string DEMNO { get; set; } // 청구번호
        public string JRFG { get; set; } // 보험자구분
        public long JJCNT { get; set; } // 심사조정건수
        public long JJAMT { get; set; } // 심사조정금액
        public long HOSRETAMT { get; set; } // 요양기관환수금
        public long PTRETAMT { get; set; } // 본인부담환급금
        public long PMGUM1 { get; set; } // 본인부담환급금1
        public long PMGUM2 { get; set; } // 본인부담환급금2
        public long UNRETAMT { get; set; } // 보험자부담환급금
        public long BHUNRETAMT { get; set; } // 보훈부담환수금
        public long BHPMGUM { get; set; } // 보훈본인부담환급금
        public string MEMO { get; set; } // 참조
        public string REDPT1 { get; set; }
        public string REDPT2 { get; set; }
        public string REDPNM { get; set; }
        public string RETELE { get; set; }
        public string JIWONCD { get; set; } // 지원코드
        public string HOSID { get; set; } // 요양기관기호
        public string FMNO { get; set; } // 서식번호
        public string GRPNO { get; set; } // 묶음번호
        public string CNECYY { get; set; } // 접수년도
        public string VERSION { get; set; } // 버전구분
        public string DCOUNT { get; set; } // 청구서일련번호

        public string REDEPT { get { return (REDPT2 + " " + REDPNM + " " + RETELE).Trim(); } }
        public string REMARK
        {
            get
            {
                string ret = "";
                if (MEMO != "") ret = MEMO;
                if (REDEPT != "")
                {
                    if (ret != "") ret += Environment.NewLine + REDEPT;
                    else ret = REDEPT;
                }

                return ret;
            }
        }


        public void Clear()
        {
            NO = 0;

            DEMSEQ = ""; // 심사차수
            REDAY = ""; // 통보일자
            CNECNO = ""; // 접수번호
            DEMNO = ""; // 청구번호
            JRFG = ""; // 보험자구분
            JJCNT = 0; // 심사조정건수
            JJAMT = 0; // 심사조정금액
            HOSRETAMT = 0; // 요양기관환수금
            PTRETAMT = 0; // 본인부담환급금
            PMGUM1 = 0; // 본인부담환급금1
            PMGUM2 = 0; // 본인부담환급금2
            UNRETAMT = 0; // 보험자부담환급금
            BHUNRETAMT = 0; // 보훈부담환수금
            BHPMGUM = 0; // 보훈본인부담환급금
            MEMO = ""; // 참조
            REDPT1 = "";
            REDPT2 = "";
            REDPNM = "";
            RETELE = "";
            JIWONCD = ""; // 지원코드
            HOSID = ""; // 요양기관기호
            FMNO = ""; // 서식번호
            GRPNO = ""; // 묶음번호
            CNECYY = ""; // 접수년도
            VERSION = ""; // 버전구분
            DCOUNT = ""; // 청구서일련번호
        }
    }
}
