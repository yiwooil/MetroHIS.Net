using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RNP001_Info
    {
        public int R_CNT;
        // TA04
        public string BEDEDT; // 입원일자
        public string BEDEHM; // 입원시간
        public string DPTCD; // 진료과
        public string INSDPTCD;
        public string INSDPTCD2;
        // TV95_10
        public string EMPID; // 작성자
        public string EMPNM;
        public string WDATE; // 작성일자
        public string WTIME; // 직성시간
        public string InCondiQ1_1; // 입원경로 외래 여부
        public string InCondiQ1_2; // 입원경로 응급실 여부
        public string InCondiQ1_3; // 입원경로 기타 여부
        public string InCondiQ7; // 전원여부(1 or 2 이면 전원)
        public string GUBUN_3; // 신생아 여부(1.신생아 ELSE.성인)
        public string Society1_ADOBJ; // 입원동기
        public string Society2_INHIS; // 수술력
        public string InCondiQ6_ETC; // 최근 투약 상태
        public string Society2_Q3_2; // 알레르기 유무(1.있음)
        public string Society2_Q3_ETC_1; // 알레르기 약물 여부
        public string Society2_Q3_ETC_2; // 알레르기 음식 여부
        public string FAQ1_1; // 부 가족력여부
        public string FAQ1_2; // 부 고혈압 여부
        public string FAQ1_3; // 부 당뇨
        public string FAQ1_4; // 부 결핵
        public string FAQ1_5; // 부 간질환
        public string FAQ1_6; // 부 암/기타
        public string FAQ1_7; // 부 신장질활
        public string FAQ1_ETC; // 부 기타내용
        public string FAQ2_1; // 모 가족력여부
        public string FAQ2_2; // 모 고혈압 여부
        public string FAQ2_3; // 모 당뇨
        public string FAQ2_4; // 모 결핵
        public string FAQ2_5; // 모 간질환
        public string FAQ2_6; // 모 암/기타
        public string FAQ2_7; // 모 신장질활
        public string FAQ2_ETC; // 모 기타내용
        public string FAQ3_1; // 형제 가족력여부
        public string FAQ3_2; // 형제 고혈압 여부
        public string FAQ3_3; // 형제 당뇨
        public string FAQ3_4; // 형제 결핵
        public string FAQ3_5; // 형제 간질환
        public string FAQ3_6; // 형제 암/기타
        public string FAQ3_7; // 형제 신장질활
        public string FAQ3_ETC; // 형제 기타내용
        public string FAQ4_1; // 조부모/기타 가족력여부
        public string FAQ4_2; // 조부모/기타 고혈압 여부
        public string FAQ4_3; // 조부모/기타 당뇨
        public string FAQ4_4; // 조부모/기타 결핵
        public string FAQ4_5; // 조부모/기타 간질환
        public string FAQ4_6; // 조부모/기타 암/기타
        public string FAQ4_7; // 조부모/기타 신장질활
        public string FAQ4_ETC; // 조부모/기타 기타내용
        public string HABITQ3_3; // 음주여부(1.여)
        public string HABITQ3_ETC_1; // 음주 양
        public string HABITQ3_ETC_2; // 음주 횟수
        public string HABITQ3_ETC_3; // 마지막 음주
        public string HABITQ4_3; // 흡연여부(1.여)
        public string HABITQ4_ETC_1; // 흡연 양
        public string HABITQ4_ETC_2; // 흡연 기간
        public string HABITQ4_ETC_3; // 금연시작일
        public string NewBornQ13_1; // 자연분만 여부
        public string NewBornQ13_2; // 제왕절개 여부
        public string NewBornQ13_ETC; // 제왕절개 상세
        public string InCondiTPR_1; // 혈압
        public string InCondiTPR_2; // 체온
        public string InCondiTPR_3; // 맥박
        public string InCondiTPR_4; // 호흡
        public string InCondiTPR_5; // 체중(출생,입실)
        public string InCondiTPR_8; // 산소포화도
        // EMR290
        public string PHX; // 과거력
        public string PE; // 신체검진
        // TU64
        public string HT; // 신장
        public string WT; // 체중

        // ---------------------
        public string IPAT_DT // 입원일시
        {
            get { return BEDEDT + BEDEHM; }
        }
        public string IPAT_DGSBJT_CD // 진료과
        {
            get { return INSDPTCD; }
        }
        public string IFLD_DTL_SPC_SBJT_CD // 내과세부
        {
            get { return INSDPTCD == "01" ? INSDPTCD2 : ""; }
        }
        public string WRTP_NM // 작성자
        {
            get { return EMPNM; }
        }
        public string WRT_DT // 작성일시
        {
            get { return WDATE + WTIME.Substring(0, 4); }
        }
        public string VST_PTH_CD // 입원경로
        {
            get
            {
                if (InCondiQ7 == "1") return "3";
                if (InCondiQ1_1 == "1") return "1";
                if (InCondiQ1_2 == "1") return "2";
                if (InCondiQ1_3 == "1") return "3";
                return "1";
            }
        }
        public string VST_PTH_CD_NM
        {
            get
            {
                if (InCondiQ7 == "1") return "전원";
                if (InCondiQ1_1 == "1") return "외래";
                if (InCondiQ1_2 == "1") return "응급실";
                if (InCondiQ1_3 == "1") return "기타";
                return "외래";
            }
        }
        public string VST_PTH_ETC_TXT // 입원결로상세
        {
            get { return ""; }
        }
        public string PTNT_TP_CD // 환자구분
        {
            get { return GUBUN_3=="1" ? "2" : "1"; }
        }
        public string PTNT_TP_CD_NM
        {
            get { return GUBUN_3 == "1" ? "신생아" : "일반"; }
        }
        public string CC_TXT // 입원동기
        {
            get { return Society1_ADOBJ; }
        }
        public string ANMN_TXT // 과거력
        {
            get { return PHX; }
        }
        public string SOPR_HIST_TXT // 수술력
        {
            get { return Society2_INHIS == "" ? "-" : Society2_INHIS; }
        }
        public string MDCT_STAT_TXT // 최근 투약 상태
        {
            get { return InCondiQ6_ETC == "" ? "-" : InCondiQ6_ETC; }
        }
        public string ALRG_YN // 알레르기 여부
        {
            get { return Society2_Q3_2 == "1" ? "1" : "2"; }
        }
        public string ALRG_YN_NM
        {
            get { return Society2_Q3_2 == "1" ? "Yes" : "No"; }
        }
        public string ALRG_TXT // 알레르기 내용
        {
            get
            {
                if (Society2_Q3_ETC_1 == "1") return "약물";
                if (Society2_Q3_ETC_2 == "1") return "음식";
                return "";
            }
        }
        public string FMHS_TXT // 가족력
        {
            get
            {
                string ret = "";
                if (FAQ1_1 == "1" && FAQ1_2 == "1" && FAQ1_3 == "1" && FAQ1_4 == "1" && FAQ1_5 == "1" && FAQ1_6 == "1" && FAQ1_7 == "1")
                {
                    ret += "부 ";
                    if (FAQ1_2 == "1") ret += "고혈압,";
                    if (FAQ1_3 == "1") ret += "당뇨,";
                    if (FAQ1_4 == "1") ret += "결핵,";
                    if (FAQ1_5 == "1") ret += "간질환,";
                    if (FAQ1_6 == "1") ret += FAQ1_ETC=="" ? "암" : FAQ1_ETC;
                    if (FAQ1_7 == "1") ret += "신장질환,";
                }
                if (FAQ2_1 == "1" && FAQ2_2 == "1" && FAQ2_3 == "1" && FAQ2_4 == "1" && FAQ2_5 == "1" && FAQ2_6 == "1" && FAQ2_7 == "1")
                {
                    ret += "모 ";
                    if (FAQ2_2 == "1") ret += "고혈압,";
                    if (FAQ2_3 == "1") ret += "당뇨,";
                    if (FAQ2_4 == "1") ret += "결핵,";
                    if (FAQ2_5 == "1") ret += "간질환,";
                    if (FAQ2_6 == "1") ret += FAQ2_ETC == "" ? "암" : FAQ2_ETC;
                    if (FAQ2_7 == "1") ret += "신장질환,";
                }
                if (FAQ3_1 == "1" && FAQ3_2 == "1" && FAQ3_3 == "1" && FAQ3_4 == "1" && FAQ3_5 == "1" && FAQ3_6 == "1" && FAQ3_7 == "1")
                {
                    ret += "형제 ";
                    if (FAQ3_2 == "1") ret += "고혈압,";
                    if (FAQ3_3 == "1") ret += "당뇨,";
                    if (FAQ3_4 == "1") ret += "결핵,";
                    if (FAQ3_5 == "1") ret += "간질환,";
                    if (FAQ3_6 == "1") ret += FAQ3_ETC == "" ? "암" : FAQ3_ETC;
                    if (FAQ3_7 == "1") ret += "신장질환,";
                }
                if (FAQ4_1 == "1" && FAQ4_2 == "1" && FAQ4_3 == "1" && FAQ4_4 == "1" && FAQ4_5 == "1" && FAQ4_6 == "1" && FAQ4_7 == "1")
                {
                    ret += "기타 ";
                    if (FAQ4_2 == "1") ret += "고혈압,";
                    if (FAQ4_3 == "1") ret += "당뇨,";
                    if (FAQ4_4 == "1") ret += "결핵,";
                    if (FAQ4_5 == "1") ret += "간질환,";
                    if (FAQ4_6 == "1") ret += FAQ4_ETC == "" ? "암" : FAQ4_ETC;
                    if (FAQ4_7 == "1") ret += "신장질환,";
                }
                return ret;
            }
        }
        public string DRNK_YN // 음주여부
        {
            get { return HABITQ3_3 == "1" ? "1" : "2"; }
        }
        public string DRNK_YN_NM
        {
            get { return HABITQ3_3 == "1" ? "Yes" : "No"; }
        }
        public string DRNK_TXT // 음주내용
        {
            get
            {
                string ret = "";
                if (HABITQ3_3 == "1")
                {
                    ret = "음주양 : " + HABITQ3_ETC_1 + ", 음주회수 : " + HABITQ3_ETC_2 + ", 마지막음주 : " + HABITQ3_ETC_3;
                }
                return ret;
            }
        }
        public string SMKN_YN // 흡연여부
        {
            get { return HABITQ4_3 == "1" ? "1" : "2"; }
        }
        public string SMKN_YN_NM
        {
            get { return HABITQ4_3 == "1" ? "Yes" : "No"; }
        }
        public string SMKN_TXT // 흡연내용
        {
            get
            {
                string ret = "";
                if (HABITQ4_3 == "1")
                {
                    ret = "흡연양 : " + HABITQ4_ETC_1 + ", 흡연기간 : " + HABITQ4_ETC_2 + ", 금연시작일 : " + HABITQ4_ETC_3;
                }
                return ret;
            }
        }
        public string HEIG // 신장
        {
            get { return HT; }
        }
        public string BWGT // 입원시체중
        {
            get { return WT; }
        }
        public string PHBD_MEDEXM_TXT // 신체검진
        {
            get { return PE; }
        }
        public string BIRTH_DT // 출생일시
        {
            get { return ""; }
        }
        public string FTUS_DEV_TRM // 재태기간
        {
            get { return ""; }
        }
        public string PARTU_FRM_TXT // 분만형태
        {
            get
            {
                if (NewBornQ13_1 == "1") return "자연분만" + (NewBornQ13_ETC == "" ? "" : " " + NewBornQ13_ETC);
                if (NewBornQ13_1 == "2") return "제왕절개" + (NewBornQ13_ETC == "" ? "" : " " + NewBornQ13_ETC);
                return NewBornQ13_ETC;
            }
        }
        public string APSC_PNT // Apgar Score
        {
            get { return ""; }
        }
        public string PARTU_TXT // 분만관련 특이사항
        {
            get { return ""; }
        }
        public string NBY_BPRSU // 혈압
        {
            get { return InCondiTPR_1; }
        }
        public string NBY_PULS // 맥박
        {
            get { return InCondiTPR_3; }
        }
        public string NBY_BRT // 호흡
        {
            get { return InCondiTPR_4; }
        }
        public string NBY_TMPR // 체온
        {
            get { return InCondiTPR_2; }
        }
        public string NBY_OXY_STRT // 산소포화도
        {
            get { return InCondiTPR_8; }
        }
        public string NBY_BIRTH_BWGT // 출생시 체중
        {
            get { return InCondiTPR_5; }
        }
        public string NBY_IPAT_BWGT // 입실시 체중
        {
            get { return InCondiTPR_5; }
        }
        public string NBY_PHBD_MEDEXM_TXT // 신체검진
        {
            get { return PE; }
        }

        // --

        public void Clear()
        {
            R_CNT = 0;
            // TA04
            BEDEDT = ""; // 입원일자
            BEDEHM = ""; // 입원시간
            DPTCD = ""; // 진료과
            INSDPTCD = "";
            INSDPTCD2 = "";
            // TV95_10
            EMPID = ""; // 작성자
            EMPNM = "";
            WDATE = ""; // 작성일자
            WTIME = ""; // 직성시간
            InCondiQ1_1 = ""; // 입원경로 외래 여부
            InCondiQ1_2 = ""; // 입원경로 응급실 여부
            InCondiQ1_3 = ""; // 입원경로 기타 여부
            InCondiQ7 = ""; // 전원여부(1 or 2 이면 전원)
            GUBUN_3 = ""; // 신생아 여부(1.신생아 ELSE.성인)
            Society1_ADOBJ = ""; // 입원동기
            Society2_INHIS = ""; // 수술력
            InCondiQ6_ETC = ""; // 최근 투약 상태
            Society2_Q3_2 = ""; // 알레르기 유무(1.있음)
            Society2_Q3_ETC_1 = ""; // 알레르기 약물 여부
            Society2_Q3_ETC_2 = ""; // 알레르기 음식 여부
            FAQ1_1 = ""; // 부 가족력여부
            FAQ1_2 = ""; // 부 고혈압 여부
            FAQ1_3 = ""; // 부 당뇨
            FAQ1_4 = ""; // 부 결핵
            FAQ1_5 = ""; // 부 간질환
            FAQ1_6 = ""; // 부 암/기타
            FAQ1_7 = ""; // 부 신장질활
            FAQ1_ETC = ""; // 부 기타내용
            FAQ2_1 = ""; // 모 가족력여부
            FAQ2_2 = ""; // 모 고혈압 여부
            FAQ2_3 = ""; // 모 당뇨
            FAQ2_4 = ""; // 모 결핵
            FAQ2_5 = ""; // 모 간질환
            FAQ2_6 = ""; // 모 암/기타
            FAQ2_7 = ""; // 모 신장질활
            FAQ2_ETC = ""; // 모 기타내용
            FAQ3_1 = ""; // 형제 가족력여부
            FAQ3_2 = ""; // 형제 고혈압 여부
            FAQ3_3 = ""; // 형제 당뇨
            FAQ3_4 = ""; // 형제 결핵
            FAQ3_5 = ""; // 형제 간질환
            FAQ3_6 = ""; // 형제 암/기타
            FAQ3_7 = ""; // 형제 신장질활
            FAQ3_ETC = ""; // 형제 기타내용
            FAQ4_1 = ""; // 조부모/기타 가족력여부
            FAQ4_2 = ""; // 조부모/기타 고혈압 여부
            FAQ4_3 = ""; // 조부모/기타 당뇨
            FAQ4_4 = ""; // 조부모/기타 결핵
            FAQ4_5 = ""; // 조부모/기타 간질환
            FAQ4_6 = ""; // 조부모/기타 암/기타
            FAQ4_7 = ""; // 조부모/기타 신장질활
            FAQ4_ETC = ""; // 조부모/기타 기타내용
            HABITQ3_3 = ""; // 음주여부(1.여)
            HABITQ3_ETC_1 = ""; // 음주 양
            HABITQ3_ETC_2 = ""; // 음주 횟수
            HABITQ3_ETC_3 = ""; // 마지막 음주
            HABITQ4_3 = ""; // 흡연여부(1.여)
            HABITQ4_ETC_1 = ""; // 흡연 양
            HABITQ4_ETC_2 = ""; // 흡연 기간
            HABITQ4_ETC_3 = ""; // 금연시작일
            NewBornQ13_1 = ""; // 자연분만 여부
            NewBornQ13_2 = ""; // 제왕절개 여부
            NewBornQ13_ETC = ""; // 제왕절개 상세
            InCondiTPR_1 = ""; // 혈압
            InCondiTPR_2 = ""; // 체온
            InCondiTPR_3 = ""; // 맥박
            InCondiTPR_4 = ""; // 호흡
            InCondiTPR_5 = ""; // 체중(출생,입실)
            InCondiTPR_8 = ""; // 산소포화도
            // EMR290
            PHX = ""; // 과거력
            PE = ""; // 신체검진
            // TU64
            HT = ""; // 신장
            WT = ""; // 체중
        }

    }
}
