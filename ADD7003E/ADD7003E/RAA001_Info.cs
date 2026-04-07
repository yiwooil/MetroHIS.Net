using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RAA001_Info
    {
        public int R_CNT;

        public string ANSDT; // 마취 시작일자
        public string ANSHR; // 마취 시작시간
        public string ANSMN; // 마취 시작분
        public string ANEDT; // 마취 종료일자
        public string ANEHR; // 마취 종료시간
        public string ANEMN; // 마취 종료분
        public string ANEDR; // 마취 의사
        public string ANENM;
        public string USRID; // 작성자
        public string USRNM;
        public string ENTDT; // 작성일자
        public string ENTMS; // 작성시간
        public string ANETP; // 마취방법
        public string ANETPNM;

        public string PATE_V; // 수액 intake
        public string BLOOD_V; // 혈액 intake
        public string BLOOD_LIST; // 혈액 intake(혈액종류/수혈량/혈액종류/수혈량/...)

        public string URINE; // 소변 output
        public string S_V_O_V; // 기타 output

        // TU01
        public string OPSDT; // 수술시작일자
        public string OPSHR; // 수술시작시간
        public string OPSMN; // 수술시작분
        public string OPEDT; // 수술종료일자
        public string OPEHR; // 수술종료시간
        public string OPEMN; // 수술종료분


        // ---------------------------------

        public string NCT_STA_DT // 마취 시작일시
        {
            get
            {
                string ans_hr = ANSHR;
                if (ans_hr.Length == 1) ans_hr = "0" + ans_hr;
                string ans_mn = ANSMN;
                if (ans_mn.Length == 1) ans_mn = "0" + ans_mn;
                return ANSDT + ans_hr + ans_mn;
            }
        }
        public string NCT_END_DT // 마취 종료일시
        {
            get
            {
                string ane_hr = ANEHR;
                if (ane_hr.Length == 1) ane_hr = "0" + ane_hr;
                string ahe_mn = ANEMN;
                if (ahe_mn.Length == 1) ahe_mn = "0" + ahe_mn;
                return ANEDT + ane_hr + ahe_mn;
            }
        }
        public string SOPR_STA_DT // 수술 시작일시
        {
            get
            {
                string ops_hr = OPSHR;
                if (ops_hr.Length == 1) ops_hr = "0" + ops_hr;
                string ops_mn = OPSMN;
                if (ops_mn.Length == 1) ops_mn = "0" + ops_mn;
                return OPSDT + ops_hr + ops_mn;
            }
        }
        public string SOPR_END_DT // 수술 종료일시
        {
            get
            {
                string ope_hr = OPEHR;
                if (ope_hr.Length == 1) ope_hr = "0" + ope_hr;
                string ope_mn = OPEMN;
                if (ope_mn.Length == 1) ope_mn = "0" + ope_mn;
                return OPEDT + ope_hr + ope_mn;
            }
        }
        public string NCT_SDR_NM // 마취의 성명
        {
            get { return ANENM; }
        }
        public string WRTP_NM // 작성자 성명
        {
            get { return USRNM; }
        }
        public string WRT_DT // 작성일시
        {
            get { return ENTDT + ENTMS.Substring(0, 4); }
        }
        public string NCT_FRM_CD // 마취형태
        {
            get { return "1"; }
        }
        public string NCT_FRM_CD_NM
        {
            get { return "정규"; }
        }
        public string ASA_PNT // ASA 점수
        {
            get { return "7"; }
        }
        public string ASA_PNT_NM
        {
            get { return "기록없음"; }
        }
        public string NCT_MTH_CD // 마취방법
        {
            get { return "99"; }
        }
        public string NCT_MTH_CD_NM
        {
            get { return "기타"; }
        }
        public string NCT_MTH_ETC_TXT // 마취방법상세
        {
            get { return ANETPNM; }
        }
        public string NCT_MIDD_MNTR_YN // 마취중감시여부
        {
            get { return "2"; }
        }
        public string NCT_MIDD_MNTR_YN_NM
        {
            get { return "No"; }
        }
        public string NCT_MNTR_KND_CD // 마취중감시종류
        {
            get { return ""; }
        }
        public string NCT_MNTR_KND_CD_NM
        {
            get { return ""; }
        }
        public string MNTR_ETC_TXT // 마취중감시종류상세
        {
            get { return ""; }
        }
        public string IGSN_TOT_QTY // Intake 총량
        {
            get
            {
                int pate_v = 0;
                int blood_v = 0;
                int.TryParse(PATE_V, out pate_v);
                int.TryParse(BLOOD_V, out blood_v);
                return (pate_v + blood_v).ToString();
            }
        }
        public string IGSN_IFSL_QTY // Intake 수액
        {
            get { return PATE_V == "" ? "-" : PATE_V; }
        }
        public string BLTS_NM // Intake 혈액(혈액종류/수혈량/혈액종류/수혈량/...)
        {
            get { return BLOOD_LIST == "" ? "-/-" : BLOOD_LIST; }
        }
        public string PROD_TOT_QTY // Output 총량
        {
            get
            {
                int urine = 0;
                int svov = 0;
                int.TryParse(URINE, out urine);
                int.TryParse(S_V_O_V, out svov);
                return (urine + svov).ToString();
            }
        }
        public string PROD_URNN_QTY // Output 배뇨
        {
            get { return URINE == "" ? "-" : URINE; }
        }
        public string PROD_HMRHG_QTY // Output 실형
        {
            get { return "-"; }
        }
        public string PROD_ETC_QTY // Output 기타
        {
            get { return S_V_O_V; }
        }

        // --------------------

        public void Clear()
        {
            R_CNT = 0;

            ANSDT = ""; // 마취 시작일자
            ANSHR = ""; // 마취 시작시간
            ANSMN = ""; // 마취 시작분
            ANEDT = ""; // 마취 종료일자
            ANEHR = ""; // 마취 종료시간
            ANEMN = ""; // 마취 종료분
            ANEDR = ""; // 마취 의사
            ANENM = "";
            USRID = ""; // 작성자
            USRNM = "";
            ENTDT = ""; // 작성일자
            ENTMS = ""; // 작성시간
            ANETP = ""; // 마취방법
            ANETPNM = "";

            PATE_V = ""; // 수액 intake
            BLOOD_V = ""; // 혈액 intake
            BLOOD_LIST = ""; // 혈액 intake(혈액종류/수혈량/혈액종류/수혈량/...)

            URINE = ""; // 소변 output
            S_V_O_V = ""; // 기타 output

            // TU01
            OPSDT = ""; // 수술시작일자
            OPSHR = ""; // 수술시작시간
            OPSMN = ""; // 수술시작분
            OPEDT = ""; // 수술종료일자
            OPEHR = ""; // 수술종료시간
            OPEMN = ""; // 수술종료분
        }
    }

}
