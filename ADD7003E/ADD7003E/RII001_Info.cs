using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RII001_Info
    {
        public int R_CNT;
        // EMR290
        public string BEDEDT; // 입원일자
        public string BEDEHM; // 입원시간
        public string DPTCD; // 과코드
        public string INSDPTCD;
        public string INSDPTCD2;
        public string DRID; // 의사ID
        public string DRNM;
        public string EMPID; // 작성자ID
        public string EMPNM;
        public string WDATE; // 작성일
        public string WTIME; // 작성시간
        public string MJ_HOSO; // 주호소
        public string ONSET; // 발병시기
        public string PI; // 현병력
        public string PHX; // 과거력
        public string ROS; // 계통문진
        public string PE; // 신체검진
        public string CUREPLAN; // 치료계획

        // TV95_10
        public string InCondiQ1_fg; // 입원경로(0.외래 1.응급 2.기타)
        public string InCondiQ7_fg; // 전원여부(1.Yes 0.No)
        public string Society2_Q3_fg; // 약물이상 반영여부(1.Yes 0.No)
        public string Society2_Q3_ETC_txt; // 약물이상 반응내용
        public string InCondiQ6_fg; // 약물복용여부(1.Yes 0.No)
        public string InCondiQ6_ETC_txt; // 약물종류상세
        public string HABITQ3_fg; // 응주여부(1.Yes 0.No)
        public string HABITQ3_ETC_qty; // 음주양
        public string HABITQ3_ETC_cnt; // 음주횟수
        public string HABITQ3_ETC_last; // 마지막음주
        public string HABITQ4_fg; // 흡연여부
        public string HABITQ4_ETC_qty; // 흡연양
        public string HABITQ4_ETC_period; // 흡연기간
        public string HABITQ4_ETC_stop; // 금연시작일
        public string FAQ1_fg; // 부 가족력여부
        public string FAQ1_txt; // 부 가족력내용
        public string FAQ2_fg; // 모 가족력여부
        public string FAQ2_txt; // 모 가족력내용
        public string FAQ3_fg; // 형제 가족력여부
        public string FAQ3_txt; // 형제 가족력내용
        public string FAQ4_fg; // 기타 가족력여부
        public string FAQ4_txt; // 기타 가족력내용


        // ------------

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
            get { return INSDPTCD=="01" ? INSDPTCD2 : ""; }
        }
        public string CHRG_DR_NM // 담당의사 성명
        {
            get { return DRNM; }
        }
        public string WRTP_NM // 작성자 성명
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
                if (InCondiQ7_fg == "1") return "3"; // 전원
                if (InCondiQ1_fg == "0") return "1"; // 외래
                if (InCondiQ1_fg == "1") return "2"; // 응급
                if (InCondiQ1_fg == "2") return "9"; // 기타
                return "1"; // 기본외래
            }
        }
        public string VST_PTH_NM
        {
            get
            {
                if (VST_PTH_CD == "1") return "외래";
                if (VST_PTH_CD == "2") return "응급실";
                if (VST_PTH_CD == "3") return "전원";
                if (VST_PTH_CD == "9") return "기타";
                return "외래"; // 기본외래
            }
        }
        public string VST_PTH_ETC_TXT // 입원경로상세
        {
            get { return ""; }
        }
        public string CC_TXT // 주호소
        {
            get { return MJ_HOSO; }
        }
        public string OCUR_ERA_TXT // 발병시기
        {
            get { return ONSET; }
        }
        public string CUR_HOC_TXT // 현병력
        {
            get { return PI; }
        }
        public string ALRG_YN // 약물이상반응여부
        {
            get { return Society2_Q3_fg=="1" ? "1" : "2"; }
        }
        public string ALRG_YN_NM
        {
            get { return Society2_Q3_fg == "1" ? "Yes" : "No"; }
        }
        public string ALRG_TXT // 약물이상반응내용
        {
            get { return Society2_Q3_fg=="1" ? Society2_Q3_ETC_txt : ""; }
        }
        public string ANMN_TXT // 과거력
        {
            get { return PHX; }
        }
        public string MDS_DOS_YN // 약물복용여부
        {
            get { return InCondiQ6_fg == "1" ? "1" : "2"; }
        }
        public string MDS_DOS_YN_NM
        {
            get { return InCondiQ6_fg == "1" ? "Yes" : "No"; }
        }
        public string MDS_KND_CD // 약물종류
        {
            get { return InCondiQ6_fg == "1" ? "9" : ""; }
        }
        public string MDS_ETC_TXT // 약물종류상세
        {
            get { return InCondiQ6_ETC_txt; }
        }
        public string DRNK_YN // 음주여부
        {
            get { return HABITQ3_fg == "1" ? "1" : "2"; }
        }
        public string DRNK_YN_NM
        {
            get { return HABITQ3_fg == "1" ? "Yes" : "No"; }
        }
        public string DRNK_TXT // 음주내용
        {
            get { return ""; }
        }
        public string SMKN_YN // 흡연여부
        {
            get { return HABITQ4_fg == "1" ? "1" : "2"; }
        }
        public string SMKN_YN_NM
        {
            get { return HABITQ4_fg == "1" ? "Yes" : "No"; }
        }
        public string SMKN_TXT // 흡연내용
        {
            get { return ""; }
        }
        public string FMHS_YN // 가족력
        {
            get { return FAQ1_fg == "1" || FAQ2_fg == "1" || FAQ3_fg == "1" || FAQ4_fg == "1" ? "1" : "2"; }
        }
        public string FMHS_YN_NM
        {
            get { return FAQ1_fg == "1" || FAQ2_fg == "1" || FAQ3_fg == "1" || FAQ4_fg == "1" ? "Yes" : "No"; }
        }
        public string FMHS_TXT // 가족력내용
        {
            get
            {
                string ret = "";
                if (FMHS_YN == "1")
                {
                    if (FAQ1_txt != "") ret += FAQ1_txt + "(부),";
                    if (FAQ2_txt != "") ret += FAQ2_txt + "(모),";
                    if (FAQ3_txt != "") ret += FAQ3_txt + "(형제),";
                    if (FAQ4_txt != "") ret += FAQ4_txt + "(조부모),";
                }
                return ret;
            }
        }
        public string ROS_TXT // 계통문진
        {
            get { return ROS; }
        }
        public string PHBD_MEDEXM_TXT // 신체검진
        {
            get { return PE; }
        }
        public string PRBM_LIST_TXT // 문제목록 및 평가
        {
            get { return "-"; }
        }
        public string TRET_PLAN_TXT // 치료계획
        {
            get { return CUREPLAN; }
        }

        // --
        public void Clear()
        {
            R_CNT = 0;
            // EMR290
            BEDEDT = ""; // 입원일자
            BEDEHM = ""; // 입원시간
            DPTCD = ""; // 과코드
            INSDPTCD = "";
            INSDPTCD2 = "";
            DRID = ""; // 의사ID
            DRNM = "";
            EMPID = ""; // 작성자ID
            EMPNM = "";
            WDATE = ""; // 작성일
            WTIME = ""; // 작성시간
            MJ_HOSO = ""; // 주호소
            ONSET = ""; // 발병시기
            PI = ""; // 현병력
            PHX = ""; // 과거력
            ROS = ""; // 계통문진
            PE = ""; // 신체검진
            CUREPLAN = ""; // 치료계획

            // TV95_10
            InCondiQ1_fg = ""; // 입원경로(0.외래 1.응급 2.기타)
            InCondiQ7_fg = ""; // 전원여부(1.Yes 0.No)
            Society2_Q3_fg = ""; // 약물이상 반영여부(1.Yes 0.No)
            Society2_Q3_ETC_txt = ""; // 약물이상 반응내용
            InCondiQ6_fg = ""; // 약물복용여부(1.Yes 0.No)
            InCondiQ6_ETC_txt = ""; // 약물종류상세
            HABITQ3_fg = ""; // 응주여부(1.Yes 0.No)
            HABITQ3_ETC_qty = ""; // 음주양
            HABITQ3_ETC_cnt = ""; // 음주횟수
            HABITQ3_ETC_last = ""; // 마지막음주
            HABITQ4_fg = ""; // 흡연여부
            HABITQ4_ETC_qty = ""; // 흡연양
            HABITQ4_ETC_period = ""; // 흡연기간
            HABITQ4_ETC_stop = ""; // 금연시작일
            FAQ1_fg = ""; // 부 가족력여부
            FAQ1_txt = ""; // 부 가족력내용
            FAQ2_fg = ""; // 모 가족력여부
            FAQ2_txt = ""; // 모 가족력내용
            FAQ3_fg = ""; // 형제 가족력여부
            FAQ3_txt = ""; // 형제 가족력내용
            FAQ4_fg = ""; // 기타 가족력여부
            FAQ4_txt = ""; // 기타 가족력내용
        }

    }
}
