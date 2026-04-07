using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RAR001_Info
    {
        public int R_CNT;

        public string RCVDT2; // 회복실 도착일자
        public string RCVHR2; // 회복실 도착시간
        public string RCVMN2; // 회복실 도착분
        public string RCVDT; // 회복실 퇴실일자
        public string RCVHR; // 회복실 퇴신시간
        public string RCVMN; // 회복실 퇴실분
        public string ANEDR; // 퇴실 결정 의사
        public string ANENM;
        public string USRID; // 등록자ID
        public string USRNM;
        public string ENTDT; // 등록일자
        public string ENTMS; // 등록시분


        // ----------------------

        public string RCRM_IPAT_DT // 회복실 도착일시
        {
            get
            {
                string rcv_hr2 = RCVHR2;
                if (rcv_hr2.Length == 1) rcv_hr2 = "0" + rcv_hr2;
                string rcv_mn2 = RCVMN2;
                if (rcv_mn2.Length == 1) rcv_mn2 = "0" + rcv_mn2;
                return RCVDT2 + rcv_hr2 + rcv_mn2;
            }
        }
        public string RCRM_DSCG_DT // 회복실 퇴실일시
        {
            get
            {
                string rcv_hr = RCVHR;
                if (rcv_hr.Length == 1) rcv_hr = "0" + rcv_hr;
                string rcv_mn = RCVMN;
                if (rcv_mn.Length == 1) rcv_mn = "0" + rcv_mn;
                return RCVDT + rcv_hr + rcv_mn;
            }
        }
        public string DSCG_DEC_DR_NM // 퇴실 결정 의사 성명
        {
            get { return ANENM; }
        }
        public string WRTP_NM // 작성자 성명
        {
            get { return USRNM; }
        }
        public string WRT_DT // 작성일시
        {
            get
            {
                string ret = ENTDT + ENTMS;
                if (ret.Length > 12) ret = ret.Substring(0, 12);
                return ret;
            }
        }

        // ---

        public void Clear()
        {
            R_CNT = 0;

            RCVDT2 = ""; // 회복실 도착일자
            RCVHR2 = ""; // 회복실 도착시간
            RCVMN2 = ""; // 회복실 도착분
            RCVDT = ""; // 회복실 퇴실일자
            RCVHR = ""; // 회복실 퇴신시간
            RCVMN = ""; // 회복실 퇴실분
            ANEDR = ""; // 퇴실 결정 의사
            ANENM = "";
            USRID = ""; // 등록자ID
            USRNM = "";
            ENTDT = ""; // 등록일자
            ENTMS = ""; // 등록시분
        }
    }
}
