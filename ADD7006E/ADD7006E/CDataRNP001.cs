using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRNP001
    {
        public string TV95_VER;

        public string BEDEDT;
        public string BEDEHM;
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;

        // 여기서부터 TV95
        public string EMPID; // 작성자
        public string EMPNM;
        public string WDATE; // 작성일자
        public string WTIME; // 작성시간
        public string AD_PATH; // 입원경로
        public string AD_PATH_TXT; // 입원경로상세(입원경로가 9.기타인경우)
        public string PTNT_TP_CD; // 환자구분(1.일반 2.신생아(28일이내))
        public string SVC_NM; // 정보제공자(성명)
                              // 정보제공자(관계)
                              // 정보제공자(전화번호)
        public string AD_OBJ; // 입원동기
        public string OLD_ILL; // 과거력
        public string AD_OP_HIS; // 수술력
        public string MED_ST; // 약물 알레르기 여부
        public string C_FOOD; // 음식 알레르기 여부
        public string C_ANTI; // 항생제 알레르기 여부
        public string ANTI; // 항생제 알리르기 내용
        public string FHX; // 가족력
        public string FH_DREAM_CHK; // 가족력기타
        public string FH_DREAM_ETC; // 가족력기타내용
        public string ALGOL; // 음주내용
        public string SMOKING; // 흡연내용
        public string HEIGHT; // 신장
        public string AD_AD_TPR; // 입원시 체중등
        public string SO_DESC; // 신체검진

        public string BIRTH_DT; // 신생아 출생일시
        public string FTUS_DEV_WEEK; // 신생아 재태기간 주
        public string FTUS_DEV_DAY; // 신생아 재태기간 일
        public string PARTU_FRM_TXT; // 신생아 분만형태(평문)
        public string APSC_PNT1; // 신생아 Apgar Score 1분 점수
        public string APSC_PNT2; // 신생아 Apgar Score 5분 점수
        // 여기까지 TV95

        // 여기서부터 TV95_10
        public string V95_10_EMPID; // 작성자
        public string V95_10_EMPNM;
        public string V95_10_WDATE; // 작성일자
        public string V95_10_WTIME; // 작성시간
        public string V95_10_InCondiQ1; // 입원경로
        public string V95_10_AD_PATH_TXT; // 입원경로상세(입원경로가 9.기타인경우)
        public string V95_10_GUBUN; // 환자구분
                                    // 정보제공자성명
        public string V95_10_Society1_ADOBJ; // 입원동기
        public string V95_10_Society2_Q1; // 과거력
        public string V95_10_Society2_INHIS; // 수술력
        public string V95_10_MDCT_STAT_TXT; // 최근 투약 상태
        public string V95_10_Society2_Q3; // 알레르기 여부
        public string V95_10_Society2_Q3_ETC; // 알레르기 내용
        public string V95_10_Society2_Q3_TXT; // 알레르기 기타 내용
        public string V95_10_FAQ1; // 가족력(부)
        public string V95_10_FAQ2; // 가족력(모)
        public string V95_10_FAQ3; // 가족력(형제자매)
        public string V95_10_FAQ4; // 가족력(조부모기타
        public string V95_10_FAQ1_ETC; // 가족력기타내용(부)
        public string V95_10_FAQ2_ETC; // 가족력기타내용(모)
        public string V95_10_FAQ3_ETC; // 가족력기타내용(형제자매)
        public string V95_10_FAQ4_ETC; // 가족력가타내용(조부모기타)
        public string V95_10_HabitQ3; // 음주여부
        public string V95_10_HabitQ3_ETC; // 음주내용
        public string V95_10_HabitQ4; // 흡연여부
        public string V95_10_HabitQ4_ETC; // 흡연내용
        public string V95_10_InCondiTPR; // 입원시 체증,신장 등
        public string V95_10_BIRTH_DT; // 신생아 출생일시
        public string V95_10_FTUS_DEV_WEEK; // 신생아 재태기간 주
        public string V95_10_FTUS_DEV_DAY; // 신생아 재태기간 일
        public string V95_10_NewBornQ13; // 분만형태
        public string V95_10_NewBornQ13_ETC; // 분만형태기타
        public string V95_10_MEDEXM_TXT; // 신체검진
        public string V95_10_APSC_PNT1; // 신생아 Apgar Score 1분 점수
        public string V95_10_APSC_PNT2; // 신생아 Apgar Score 5분 점수
        // 여기까지 TV95_10

        public string DEPT_INFO { get { return DPTCD + "(" + INSDPTCD + INSDPTCD2 + ")"; } }
        public string IPAT_DT { get { return BEDEDT + BEDEHM; } }
        public string IPAT_DGSBJT_CD { get { return INSDPTCD; } }
        public string IFLD_DTL_SPC_SBJT_CD { get { return INSDPTCD2; } }
        public string WRTP_NM
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    ret = V95_10_EMPNM;
                }
                else
                {
                    ret = EMPNM;
                }
                return ret;
            }
        }
        public string WRT_DT
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    ret = V95_10_WDATE + V95_10_WTIME;
                }
                else
                {
                    ret = WDATE + WTIME;
                }
                return ret;
            }
        }
        public string VST_PTH_CD// 입원경로
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    string[] val = (V95_10_InCondiQ1 + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    if (val[0] == "1") ret = "1"; // 외래
                    else if (val[1] == "1") ret = "2"; // 응급실
                    else if (val[2] == "1") ret = "9"; // 기타
                    else if (val[3] == "1") ret = "3"; // 전원
                }
                else
                {
                    string[] val = (AD_PATH + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    if (val[0] == "1") ret = "1"; // 외래
                    else if (val[1] == "1") ret = "2"; // 응급실
                    else if (val[2] == "1") ret = "9"; // 기타
                    else if (val[3] == "1") ret = "3"; // 전원
                }
                return ret;

            }
        }
        public string VST_PTH_ETC_TXT // 입원경로상세
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    ret = V95_10_AD_PATH_TXT;
                }
                else
                {
                    ret = AD_PATH_TXT;
                }
                return ret;
            }
        
        }
        public string PTNT_TP_CD_12 // 환자구분(1.일반 2.신생아(28일이내))
        {
            get{
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    string[] val = (V95_10_GUBUN + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    if (val[2] == "1") ret = "2"; // 신생아
                    else ret = "1"; // 일반
                    return ret;
                }
                else
                {
                    ret = PTNT_TP_CD;
                }
                return ret;
            }
        }
        public string INFM_OFFRR_NM // 정보제공자성명
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    ret = "";
                }
                else
                {
                    ret = SVC_NM;
                }
                return ret;
            }
        }
        public string CC_TXT // 입원동기
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    ret = V95_10_Society1_ADOBJ;
                }
                else
                {
                    ret = AD_OBJ;
                }
                return ret;
            }
        }
        public string ANMN_TXT //과거력
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    string[] val = (V95_10_Society2_Q1 + (char)21 + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    if (val[0] == "1") ret += (ret != "" ? "," : "") + "당뇨";
                    if (val[1] == "1") ret += (ret != "" ? "," : "") + "고혈압";
                    if (val[2] == "1") ret += (ret != "" ? "," : "") + "간염";
                    if (val[3] == "1") ret += (ret != "" ? "," : "") + "결핵";
                    if (val[4] == "1") ret += (ret != "" ? "," : "") + "기타";
                    if (val[5] == "1") ret += (ret != "" ? "," : "") + "없음";
                }
                else
                {
                    string[] val = (OLD_ILL + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    if (val[0] == "1") ret += (ret != "" ? "," : "") + "당뇨";
                    if (val[1] == "1") ret += (ret != "" ? "," : "") + "고혈압";
                    if (val[2] == "1") ret += (ret != "" ? "," : "") + "간염";
                    if (val[3] == "1") ret += (ret != "" ? "," : "") + "결핵";
                    if (val[4] == "1" && val[9] != "") ret += (ret != "" ? "," : "") + val[9]; // 기타.직접입력
                }
                return ret;
            }
        }
        public string SOPR_HIST_TXT // 수술력
        {
            get{
                string ret="";
                if (TV95_VER == "TV95_10")
                {
                    ret = V95_10_Society2_INHIS;
                }
                else
                {
                    ret = AD_OP_HIS;
                }
                return ret;
            }
        }
        public string MDCT_STAT_TXT // 최근 투약상태
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    ret = V95_10_MDCT_STAT_TXT;
                }
                else
                {
                    string[] val = (MED_ST + (char)21 + (char)21).Split((char)21);
                    ret = val[2];
                }
                return ret;
            }
        }
        public string ALRG_YN
        {
            get
            {
                string ret = "";

                if (TV95_VER == "TV95_10")
                {
                    string[] val = (V95_10_Society2_Q3 + (char)21 + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    if (val[1] == "1") ret = "1"; // Yes
                    else if (val[0] == "1") ret = "2"; // No
                    else ret = "3"; // 확인불가
                }
                else
                {
                    string[] val_med_st = (MED_ST + (char)21).Split((char)21);
                    string[] val_c_foot = (C_FOOD + (char)21).Split((char)21);
                    string[] val_c_anit = (C_ANTI + (char)21).Split((char)21);

                    if (val_med_st[1] == "1" || val_c_foot[1] == "1" || val_c_anit[1] == "1") ret = "1"; // Yes
                    else if (val_med_st[0] == "1" || val_c_foot[0] == "1" || val_c_anit[0] == "1") ret = "2"; // No
                    else ret = "3"; // 확인불가
                }
                return ret;
            }
        }
        public string ALRG_TXT
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    if (ALRG_YN == "1")
                    {
                        string[] val = (V95_10_Society2_Q3_ETC + (char)21 + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                        if (val[0] != "") ret += (ret != "" ? "," : "") + val[0];
                        if (val[1] != "") ret += (ret != "" ? "," : "") + val[1];
                        if (V95_10_Society2_Q3_TXT != "") ret += (ret != "" ? "," : "") + V95_10_Society2_Q3_TXT;
                    }
                }
                else
                {
                    if (ALRG_YN == "1")
                    {
                        string[] val_med_st = (MED_ST + (char)21 + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                        string[] val_c_foot = (C_FOOD + (char)21 + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                        string[] val_c_anit = (C_ANTI + (char)21 + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);

                        if (val_med_st[3] != "") ret += (ret != "" ? "," : "") + val_med_st[3];
                        if (val_c_foot[4] != "") ret += (ret != "" ? "," : "") + val_c_foot[4];
                        if (val_c_anit[2] == "1") ret += (ret != "" ? "," : "") + "페니실린계";
                        if (val_c_anit[3] == "1") ret += (ret != "" ? "," : "") + "기타베타락탐계";
                        if (val_c_anit[4] == "1" && ANTI != "") ret += (ret != "" ? "," : "") + ANTI;

                    }
                }
                return ret;
            }
        }
        public string FMHS_TXT // 가족력
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    string[] val_faq1 = (V95_10_FAQ1 + (char)21 + (char)21 + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    string[] val_faq2 = (V95_10_FAQ2 + (char)21 + (char)21 + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    string[] val_faq3 = (V95_10_FAQ3 + (char)21 + (char)21 + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    string[] val_faq4 = (V95_10_FAQ4 + (char)21 + (char)21 + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);

                    //if (val_faq1[0] == "1") ret += (ret != "" ? "," : "") + "없음(부)";
                    if (val_faq1[1] == "1") ret += (ret != "" ? "," : "") + "고혈압(부)";
                    if (val_faq1[2] == "1") ret += (ret != "" ? "," : "") + "당뇨(부)";
                    if (val_faq1[3] == "1") ret += (ret != "" ? "," : "") + "결핵(부)";
                    if (val_faq1[4] == "1") ret += (ret != "" ? "," : "") + "간질환(부)";
                    if (val_faq1[5] == "1" && V95_10_FAQ1_ETC == "") ret += (ret != "" ? "," : "") + "암(부)";
                    if (val_faq1[5] == "1" && V95_10_FAQ1_ETC != "") ret += (ret != "" ? "," : "") + V95_10_FAQ1_ETC + "(부)";
                    if (val_faq1[6] == "1") ret += (ret != "" ? "," : "") + "신장질한(부)";

                    //if (val_faq2[0] == "1") ret += (ret != "" ? "," : "") + "없음(모)";
                    if (val_faq2[1] == "1") ret += (ret != "" ? "," : "") + "고혈압(모)";
                    if (val_faq2[2] == "1") ret += (ret != "" ? "," : "") + "당뇨(모)";
                    if (val_faq2[3] == "1") ret += (ret != "" ? "," : "") + "결핵(모)";
                    if (val_faq2[4] == "1") ret += (ret != "" ? "," : "") + "간질환(모)";
                    if (val_faq2[5] == "1" && V95_10_FAQ2_ETC == "") ret += (ret != "" ? "," : "") + "암(모)";
                    if (val_faq2[5] == "1" && V95_10_FAQ2_ETC != "") ret += (ret != "" ? "," : "") + V95_10_FAQ2_ETC + "(모)";
                    if (val_faq2[6] == "1") ret += (ret != "" ? "," : "") + "신장질한(모)";

                    //if (val_faq3[0] == "1") ret += (ret != "" ? "," : "") + "없음(형제/자매)";
                    if (val_faq3[1] == "1") ret += (ret != "" ? "," : "") + "고혈압(형제/자매)";
                    if (val_faq3[2] == "1") ret += (ret != "" ? "," : "") + "당뇨(형제/자매)";
                    if (val_faq3[3] == "1") ret += (ret != "" ? "," : "") + "결핵(형제/자매)";
                    if (val_faq3[4] == "1") ret += (ret != "" ? "," : "") + "간질환(형제/자매)";
                    if (val_faq3[5] == "1" && V95_10_FAQ3_ETC == "") ret += (ret != "" ? "," : "") + "암(형제/자매)";
                    if (val_faq3[5] == "1" && V95_10_FAQ3_ETC != "") ret += (ret != "" ? "," : "") + V95_10_FAQ3_ETC + "(형제/자매)";
                    if (val_faq3[6] == "1") ret += (ret != "" ? "," : "") + "신장질한(형제/자매)";

                    //if (val_faq4[0] == "1") ret += (ret != "" ? "," : "") + "없음(기타)";
                    if (val_faq4[1] == "1") ret += (ret != "" ? "," : "") + "고혈압(기타)";
                    if (val_faq4[2] == "1") ret += (ret != "" ? "," : "") + "당뇨(기타)";
                    if (val_faq4[3] == "1") ret += (ret != "" ? "," : "") + "결핵(기타)";
                    if (val_faq4[4] == "1") ret += (ret != "" ? "," : "") + "간질환(기타)";
                    if (val_faq4[5] == "1" && V95_10_FAQ4_ETC == "") ret += (ret != "" ? "," : "") + "암(기타)";
                    if (val_faq4[5] == "1" && V95_10_FAQ4_ETC != "") ret += (ret != "" ? "," : "") + V95_10_FAQ4_ETC + "(기타)";
                    if (val_faq4[6] == "1") ret += (ret != "" ? "," : "") + "신장질한(기타)";
                }
                else
                {
                    string[] val = (FHX + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    if (val[0] == "1") ret += (ret != "" ? "," : "") + "당뇨";
                    if (val[1] == "1") ret += (ret != "" ? "," : "") + "고혈압";
                    if (val[2] == "1") ret += (ret != "" ? "," : "") + "간염";
                    if (val[3] == "1") ret += (ret != "" ? "," : "") + "결핵";
                    if (val[4] == "1") ret += (ret != "" ? "," : "") + "암";
                    if (FH_DREAM_CHK == "1" && FH_DREAM_ETC != "") ret += (ret != "" ? "," : "") + FH_DREAM_ETC; // 기타.직접입력
                }
                return ret;
            }
        }
        public string DRNK_YN// 음주
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    string[] val = (V95_10_HabitQ3 + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    if (val[0] == "1") ret = "2"; // 무
                    else if (val[1] == "1") ret = "2"; // 금주
                    else if (val[2] == "1") ret = "1"; // 유
                    else ret = "3"; // 확인불가

                }
                else
                {
                    ret = ALGOL != "" ? "1" : "2";
                }
                return ret;
            }
        }
        public string DRNK_TXT // 음주내용
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    string[] val = (V95_10_HabitQ3 + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    string[] val_etc = (V95_10_HabitQ3_ETC + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    if (val[1] == "1" || val[2] == "2")
                    {
                        if (val_etc[0] != "") ret += (ret != "" ? "," : "") + val_etc[0] + "병/하루";
                        if (val_etc[1] != "") ret += (ret != "" ? "," : "") + val_etc[1] + "회/월";
                        if (val_etc[2] != "") ret += (ret != "" ? "," : "") + "마지막 음주:" + val_etc[2];
                    }
                }
                else
                {
                    ret = ALGOL;
                }
                return ret;
            }
        }
        public string SMKN_YN // 흡연
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    string[] val = (V95_10_HabitQ4 + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    if (val[0] == "1") ret = "2"; // 무
                    else if (val[1] == "1") ret = "2"; // 금주
                    else if (val[2] == "1") ret = "1"; // 유
                    else ret = "3"; // 확인불가
                }
                else
                {
                    ret = SMOKING != "" ? "1" : "2";
                }
                return ret;
            }
        }
        public string SMKN_TXT // 흡연내용
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    string[] val = (V95_10_HabitQ4 + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    string[] val_etc = (V95_10_HabitQ4_ETC + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    if (val[1] == "1" || val[2] == "2")
                    {
                        if (val_etc[0] != "") ret += (ret != "" ? "," : "") + val_etc[0] + "갑/하루";
                        if (val_etc[1] != "") ret += (ret != "" ? "," : "") + val_etc[1] + "년";
                        if (val_etc[2] != "") ret += (ret != "" ? "," : "") + "금연 시작일:" + val_etc[2];
                    }
                }
                else
                {
                    ret = SMOKING;
                }
                return ret;
            }
        }
        public string HEIG // 신장
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    string[] val = (V95_10_InCondiTPR + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    ret = val[4];
                }
                else
                {
                    ret = HEIGHT;
                }
                return ret;
            }
        
        }
        public string BWGT // 체중
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    string[] val = (V95_10_InCondiTPR + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    ret = val[3];
                }
                else
                {
                    string[] val = (AD_AD_TPR + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                    ret = val[4];
                }
                return ret;
            }
        }
        public string PHBD_MEDEXM_TXT
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    ret = V95_10_MEDEXM_TXT;
                }
                else
                {
                    ret = SO_DESC;
                }
                return ret;
            }
        }
        public string BIRTH_DTM
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    ret = PTNT_TP_CD_12 == "2" ? V95_10_BIRTH_DT : "";
                }
                else
                {
                    ret = PTNT_TP_CD_12 == "2" ? BIRTH_DT : "";
                }
                return ret;
            }
        }
        public string FTUS_DEV_TRM
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    ret = PTNT_TP_CD_12 == "2" ? V95_10_FTUS_DEV_WEEK + "/" + V95_10_FTUS_DEV_DAY : "";
                }
                else
                {
                    ret = PTNT_TP_CD_12 == "2" ? FTUS_DEV_WEEK + "/" + FTUS_DEV_DAY : "";
                }
                return ret;
            }
        }
        public string PARTU_FRM_TXT_HIRA
        {
            get
            {
                string ret = "";
                if (PTNT_TP_CD_12 == "2")
                {
                    if (TV95_VER == "TV95_10")
                    {
                        string[] val = (V95_10_NewBornQ13 + (char)21 + (char)21 + (char)21 + (char)21).Split((char)21);
                        if (val[0] == "1") ret += (ret != "" ? "," : "") + "자연분만";
                        if (val[1] == "1") ret += (ret != "" ? "," : "") + "재왕절개";
                        if (V95_10_NewBornQ13_ETC != "") ret += (ret != "" ? "," : "") + V95_10_NewBornQ13_ETC;
                    }
                    else
                    {
                        ret = PARTU_FRM_TXT;
                    }
                }
                return ret;
            }
        }
        public string APSC_PNT
        {
            get
            {
                string ret = "";
                if (TV95_VER == "TV95_10")
                {
                    ret = PTNT_TP_CD_12 == "2" ? V95_10_APSC_PNT1 + "/" + V95_10_APSC_PNT2 : "";
                }
                else
                {
                    ret = PTNT_TP_CD_12 == "2" ? APSC_PNT1 + "/" + APSC_PNT2 : "";
                }
                return ret;
            }
        }

        public void Clear()
        {
            TV95_VER = "";

            BEDEDT = "";
            BEDEHM = "";
            DPTCD = "";
            INSDPTCD = "";
            INSDPTCD2 = "";

            EMPID = ""; // 작성자
            EMPNM = "";
            WDATE = ""; // 작성일자
            WTIME = ""; // 작성시간
            AD_PATH = ""; // 입원경로
            AD_PATH_TXT = ""; // 입원경로상세(입원경로가 9.기타인경우)
            PTNT_TP_CD = ""; // 환자구분
            SVC_NM = ""; // 정보제공자(성명)
                         // 정보제공자(관계)
                         // 정보제공자(전화번호)
            AD_OBJ = ""; // 입원동기
            OLD_ILL = ""; // 과거력
            AD_OP_HIS = ""; // 수술력
            MED_ST = ""; // 약물 알레르기 여부
            C_FOOD = ""; // 음식 알레르기 여부
            C_ANTI = ""; // 항생제 알레르기 여부
            ANTI = ""; // 항생제 알레르기가 있는 경우 내용
            FHX = ""; // 가족력
            FH_DREAM_CHK = ""; // 가족력기타
            FH_DREAM_ETC = ""; // 가족력기타내용
            ALGOL = ""; // 음주내용
            SMOKING = ""; // 흡연내용
            HEIGHT = ""; // 신장
            AD_AD_TPR = ""; // 입원시 체중등
            SO_DESC = ""; // 신체검진

            BIRTH_DT = ""; // 신생아 출생일시
            FTUS_DEV_WEEK = ""; // 신생아 재태기간 주
            FTUS_DEV_DAY = ""; // 신생아 재태기간 일
            PARTU_FRM_TXT = ""; // 신생아 분만형태(평문)
            APSC_PNT1 = ""; // 신생아 Apgar Score 1분 점수
            APSC_PNT2 = ""; // 신생아 Apgar Score 5분 점수

            // 여기서부터 TV95_10
            V95_10_EMPID = ""; // 작성자
            V95_10_EMPNM = "";
            V95_10_WDATE = ""; // 작성일자
            V95_10_WTIME = ""; // 작성시간
            V95_10_InCondiQ1 = ""; // 입원경로
            V95_10_AD_PATH_TXT = ""; // 입원경로상세(입원경로가 9.기타인경우)
            V95_10_GUBUN = ""; // 환구구분((TV95_10)
                               // 정보제공자성명
            V95_10_Society1_ADOBJ = ""; // 입원동기
            V95_10_Society2_Q1 = ""; // 과거력
            V95_10_Society2_INHIS = ""; // 수술력
            V95_10_MDCT_STAT_TXT = ""; // 최근 투약 상태
            V95_10_Society2_Q3 = ""; // 알레르기 여부
            V95_10_Society2_Q3_ETC = ""; // 알레르기 내용
            V95_10_Society2_Q3_TXT = ""; // 알레르기 기타 내용
            V95_10_FAQ1 = ""; // 가족력(부)
            V95_10_FAQ2 = ""; // 가족력(모)
            V95_10_FAQ3 = ""; // 가족력(형제자매)
            V95_10_FAQ4 = ""; // 가족력(조부모기타
            V95_10_FAQ1_ETC = ""; // 가족력기타내용(부)
            V95_10_FAQ2_ETC = ""; // 가족력기타내용(모)
            V95_10_FAQ3_ETC = ""; // 가족력기타내용(형제자매)
            V95_10_FAQ4_ETC = ""; // 가족력가타내용(조부모기타
            V95_10_HabitQ3 = ""; // 음주여부
            V95_10_HabitQ3_ETC = ""; // 음주내용
            V95_10_HabitQ4 = ""; // 흡연여부
            V95_10_HabitQ4_ETC = ""; // 흡연내용
            V95_10_InCondiTPR = ""; // 입원시 체증,신장 등
            V95_10_BIRTH_DT = ""; // 신생아 출생일시
            V95_10_FTUS_DEV_WEEK = ""; // 신생아 재태기간 주
            V95_10_FTUS_DEV_DAY = ""; // 신생아 재태기간 일
            V95_10_NewBornQ13 = ""; // 분만형태
            V95_10_NewBornQ13_ETC = ""; // 분만형태기타
            V95_10_MEDEXM_TXT = ""; // 신체검진
            V95_10_APSC_PNT1 = ""; // 신생아 Apgar Score 1분 점수
            V95_10_APSC_PNT2 = ""; // 신생아 Apgar Score 5분 점수
            // 여기까지 TV95_10
        }
    }
}
