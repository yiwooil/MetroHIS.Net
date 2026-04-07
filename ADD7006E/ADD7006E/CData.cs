using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CData
    {
        // TI2A
        private string m_EPRTNO;

        public string DEMNO { get; set; }
        public string EPRTNO
        {
            set { m_EPRTNO = value; }
            get
            {
                string ret = m_EPRTNO;
                if (m_EPRTNO.Length == 4) ret = "0" + m_EPRTNO;
                else if (m_EPRTNO.Length == 3) ret = "00" + m_EPRTNO;
                else if (m_EPRTNO.Length == 2) ret = "000" + m_EPRTNO;
                else if (m_EPRTNO.Length == 1) ret = "0000" + m_EPRTNO;
                return ret;
            }
        }
        public string PID { get; set; }
        public string PNM { get; set; }
        public string PSEX { get; set; }
        public string PSEX_MF
        {
            get { return (PSEX == "1" ? "M" : "F"); }
        }
        public string DPTCD { get; set; }
        public string STEDT { get; set; }
        public string BDEDT { get; set; }
        public string RESID { get; set; }
        public string RESID1
        {
            get { return CUtil.GetSubstring(RESID, 0, 6); }
        }
        public string QFYCD { get; set; }

        public string A_KEY;
        public string IOFG;
        public string EXAMC;

        public string FRDT
        {
            get { return IOFG == "2" ? BDEDT : STEDT; }
        }
        public string TODT
        {
            get
            {
                string todt = FRDT;
                try
                {
                    int iExamc = 0;
                    int.TryParse(EXAMC, out iExamc);
                    if (iExamc > 0) iExamc--;
                    DateTime dtStedt = DateTime.ParseExact(FRDT, "yyyyMMdd", null);
                    todt = dtStedt.AddDays(iExamc).ToString("yyyyMMdd");
                }
                catch (Exception ex) { }
                return todt;
            }
        }

        // 퇴원요약자료
        public List<CDataRID001> RID001_LIST = new List<CDataRID001>();
        public MySendResult RID001_RESULT = new MySendResult(); // 송신후 결과
        public string RID001_STATUS_NM { get { return RID001_RESULT.STATUS_NM; } }

        // 진단검사결과지
        public List<CDataERD001> ERD001_LIST = new List<CDataERD001>();
        public MySendResult ERD001_RESULT = new MySendResult(); // 송신후 결과
        public string ERD001_STATUS_NM { get { return ERD001_RESULT.STATUS_NM; } }

        // 영상검사결과지
        public List<CDataERR001> ERR001_LIST = new List<CDataERR001>();
        public MySendResult ERR001_RESULT = new MySendResult(); // 송신후 결과
        public string ERR001_STATUS_NM { get { return ERR001_RESULT.STATUS_NM; } }

        // 병리검사결과지
        public string ERP001_STATUS_NM { get { return ""; } }

        // 수술기록자료
        public List<CDataRSS001> RSS001_LIST = new List<CDataRSS001>();
        public MySendResult RSS001_RESULT = new MySendResult(); // 송신후 결과
        public string RSS001_STATUS_NM { get { return RSS001_RESULT.STATUS_NM; } }

        // 응급기록자료
        public List<CDataREE001> REE001_LIST = new List<CDataREE001>();
        public MySendResult REE001_RESULT = new MySendResult(); // 송신후 결과
        public string REE001_STATUS_NM { get { return REE001_RESULT.STATUS_NM; } }

        // 입원초진기록자료
        public List<CDataRII001> RII001_LIST = new List<CDataRII001>();
        public MySendResult RII001_RESULT = new MySendResult(); // 송신후 결과
        public string RII001_STATUS_NM { get { return RII001_RESULT.STATUS_NM; } }

        // 입원경과기록자료
        public List<CDataRIP001> RIP001_LIST = new List<CDataRIP001>();
        public MySendResult RIP001_RESULT = new MySendResult(); // 송신후 결과
        public string RIP001_STATUS_NM { get { return RIP001_RESULT.STATUS_NM; } }

        // 외래초진기록자료
        public List<CDataROO001> ROO001_LIST = new List<CDataROO001>();
        public MySendResult ROO001_RESULT = new MySendResult(); // 송신후 결과
        public string ROO001_STATUS_NM { get { return ROO001_RESULT.STATUS_NM; } }

        // 외래경과기록자료
        public List<CDataROP001> ROP001_LIST = new List<CDataROP001>();
        public MySendResult ROP001_RESULT = new MySendResult(); // 송신후 결과
        public string ROP001_STATUS_NM { get { return ROP001_RESULT.STATUS_NM; } }

        // 중환자실기록기록자료
        public List<CDataRWI001> RWI001_LIST = new List<CDataRWI001>();
        public MySendResult RWI001_RESULT = new MySendResult(); // 송신후 결과
        public string RWI001_STATUS_NM { get { return RWI001_RESULT.STATUS_NM; } }

        // 협의진료기록기록자료
        public List<CDataRCC001> RCC001_LIST = new List<CDataRCC001>();
        public MySendResult RCC001_RESULT = new MySendResult(); // 송신후 결과
        public string RCC001_STATUS_NM { get { return RCC001_RESULT.STATUS_NM; } }

        // 마취기록자료
        public List<CDataRAA001> RAA001_LIST = new List<CDataRAA001>();
        public MySendResult RAA001_RESULT = new MySendResult(); // 송신후 결과
        public string RAA001_STATUS_NM { get { return RAA001_RESULT.STATUS_NM; } }

        // 회복기록자료
        public List<CDataRAR001> RAR001_LIST = new List<CDataRAR001>();
        public MySendResult RAR001_RESULT = new MySendResult(); // 송신후 결과
        public string RAR001_STATUS_NM { get { return RAR001_RESULT.STATUS_NM; } }

        // 간호정보조사자료
        public List<CDataRNP001> RNP001_LIST = new List<CDataRNP001>();
        public MySendResult RNP001_RESULT = new MySendResult(); // 송신후 결과
        public string RNP001_STATUS_NM { get { return RNP001_RESULT.STATUS_NM; } }

        // 기타간호기록자료
        public List<CDataRNO001> RNO001_LIST = new List<CDataRNO001>();
        public MySendResult RNO001_RESULT = new MySendResult(); // 송신후 결과
        public string RNO001_STATUS_NM { get { return RNO001_RESULT.STATUS_NM; } }

        // 수술간호기록자료
        public List<CDataRNS001> RNS001_LIST = new List<CDataRNS001>();
        public MySendResult RNS001_RESULT = new MySendResult(); // 송신후 결과
        public string RNS001_STATUS_NM { get { return RNS001_RESULT.STATUS_NM; } }

        // 응급간호기록자료
        public List<CDataRNE001> RNE001_LIST = new List<CDataRNE001>();
        public MySendResult RNE001_RESULT = new MySendResult(); // 송신후 결과
        public string RNE001_STATUS_NM { get { return RNE001_RESULT.STATUS_NM; } }

        // 의사지시기록자료
        public List<CDataRDD001> RDD001_LIST = new List<CDataRDD001>();
        public MySendResult RDD001_RESULT = new MySendResult(); // 송신후 결과
        public string RDD001_STATUS_NM { get { return RDD001_RESULT.STATUS_NM; } }

        // 임상관찰기록자료
        public List<CDataRWW001> RWW001_LIST = new List<CDataRWW001>();
        public MySendResult RWW001_RESULT = new MySendResult(); // 송신후 결과
        public string RWW001_STATUS_NM { get { return RWW001_RESULT.STATUS_NM; } }

        // 투석기록자료
        public List<CDataRNH001> RNH001_LIST = new List<CDataRNH001>();
        public MySendResult RNH001_RESULT = new MySendResult(); // 송신후 결과
        public string RNH001_STATUS_NM { get { return RNH001_RESULT.STATUS_NM; } }

        // 전입기록자료
        public List<CDataRIY001> RIY001_LIST = new List<CDataRIY001>();
        public MySendResult RIY001_RESULT = new MySendResult(); // 송신후 결과
        public string RIY001_STATUS_NM { get { return RIY001_RESULT.STATUS_NM; } }

        // 전출기록자료
        public List<CDataRIZ001> RIZ001_LIST = new List<CDataRIZ001>();
        public MySendResult RIZ001_RESULT = new MySendResult(); // 송신후 결과
        public string RIZ001_STATUS_NM { get { return RIZ001_RESULT.STATUS_NM; } }

        // 시술기록자료
        public List<CDataRTT001> RTT001_LIST = new List<CDataRTT001>();
        public MySendResult RTT001_RESULT = new MySendResult(); // 송신후 결과
        public string RTT001_STATUS_NM { get { return RTT001_RESULT.STATUS_NM; } }

        // 투약기록자료
        public List<CDataRMM001> RMM001_LIST = new List<CDataRMM001>();
        public MySendResult RMM001_RESULT = new MySendResult(); // 송신후 결과
        public string RMM001_STATUS_NM { get { return RMM001_RESULT.STATUS_NM; } }

        // 신생아중환자실기록자료
        public List<CDataRWN001> RWN001_LIST = new List<CDataRWN001>();
        public MySendResult RWN001_RESULT = new MySendResult(); // 송신후 결과
        public string RWN001_STATUS_NM { get { return RWN001_RESULT.STATUS_NM; } }

        // 의원급진료기록자료

        // 방사선치료기록자료

        public void Clear()
        {
            // TI2A
            DEMNO = "";
            EPRTNO = "";
            PID = "";
            PNM = "";
            PSEX = "";
            DPTCD = "";
            STEDT = "";
            BDEDT = "";
            RESID = "";
            QFYCD = "";

            A_KEY = "";
            IOFG = "";
            EXAMC = "";


            // 퇴원요약자료
            RID001_LIST.Clear();
            RID001_RESULT.Clear(); // 송신후 결과

            // 진단검사결과지
            ERD001_LIST.Clear();
            ERD001_RESULT.Clear(); // 송신후 결과

            // 진단검사결과지
            ERR001_LIST.Clear();
            ERR001_RESULT.Clear(); // 송신후 결과

            // 병리검사결과지

            // 수술기록자료
            RSS001_LIST.Clear();
            RSS001_RESULT.Clear(); // 송신후 결과

            // 응급기록자료
            REE001_LIST.Clear();
            REE001_RESULT.Clear(); // 송신후 결과

            // 입원초진기록자료
            RII001_LIST.Clear();
            RII001_RESULT.Clear(); // 송신후 결과

            // 입원경과기록자료
            RIP001_LIST.Clear();
            RIP001_RESULT.Clear(); // 송신후 결과

            // 외래초진기록자료
            ROO001_LIST.Clear();
            ROO001_RESULT.Clear(); // 송신후 결과

            // 외래경과기록자료
            ROP001_LIST.Clear();
            ROP001_RESULT.Clear(); // 송신후 결과

            // 중환자실기록자료
            RWI001_LIST.Clear();
            RWI001_RESULT.Clear(); // 송신후 결과

            // 협의진료기록자료
            RCC001_LIST.Clear();
            RCC001_RESULT.Clear(); // 송신후 결과

            // 마취기록자료
            RAA001_LIST.Clear();
            RAA001_RESULT.Clear(); // 송신후 결과

            // 회복기록자료
            RAR001_LIST.Clear();
            RAR001_RESULT.Clear(); // 송신후 결과

            // 간호정보조사자료
            RNP001_LIST.Clear();
            RNP001_RESULT.Clear(); // 송신후 결과

            // 기타간호기록자료
            RNO001_LIST.Clear();
            RNO001_RESULT.Clear(); // 송신후 결과

            // 수술간호기록자료
            RNS001_LIST.Clear();
            RNS001_RESULT.Clear(); // 송신후 결과

            // 응급간호기록자료
            RNE001_LIST.Clear();
            RNE001_RESULT.Clear(); // 송신후 결과

            // 의사지시기록자료
            RDD001_LIST.Clear();
            RDD001_RESULT.Clear(); // 송신후 결과

            // 임상관찰기록자료
            RWW001_LIST.Clear();
            RWW001_RESULT.Clear(); // 송신후 결과

            // 투석기록자료
            RNH001_LIST.Clear();
            RNH001_RESULT.Clear(); // 송신후 결과

            // 전입기록자료
            RIY001_LIST.Clear();
            RIY001_RESULT.Clear(); // 송신후 결과

            // 전출기록자료
            RIZ001_LIST.Clear();
            RIZ001_RESULT.Clear(); // 송신후 결과

            // 시술기록자료
            RTT001_LIST.Clear();
            RTT001_RESULT.Clear(); // 송신후 결과

            // 투약기록자료
            RMM001_LIST.Clear();
            RMM001_RESULT.Clear(); // 송신후 결과

            // 신생아중환자실기록자료
            RWN001_LIST.Clear();
            RWN001_RESULT.Clear(); // 송신후 결과

            // 의원급진료기록자료

            // 방사선치료기록자료

        }

    }
}
