using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class REE001_Info
    {
        public int R_CNT;

        public string PTMIINDT; // 응급실 도착일자
        public string PTMIINTM; //        도착시간
        public string PTMIOTDT; // 응급실 퇴실일자
        public string PTMIOTTM; //        퇴실시간
        public string PTMIKTDT; // 응급실 KTAS 분류일자
        public string PTMIKTTM; //        KTAS 분류시간
        public string PTMIKTS1; //        KTAS 등급
        public string PTMIINCD; //        전원기관코드
        public string PTMIEMRT; // 응급시 퇴실형태

        public string E12C_CC;  // 주호소
        public string E12C_PT;  // 현병력
        public string E12C_PHX; // 과거력
        public string E12C_FHX; // 가족력
        public string E12C_ROS; // 계통문진
        public string E12C_PE;  // 신체검사

        public string EMRRM_IPAT_DT // 응급실 도착일시
        {
            get { return PTMIINDT + PTMIINTM; }
        }
        public string EMRRM_DSCG_DT // 응급실 퇴시일시
        {
            get { return PTMIOTDT + PTMIOTTM; }
        }
        public string KTAS_GRD_DIV_DT // KTAS 분류일시
        {
            get { return PTMIKTDT + PTMIKTTM; }
        }
        public string KTAS_GRD_TXT // KTAS 등급
        {
            get { return PTMIKTS1; }
        }
        public string DHI_YN // 전원여부
        {
            get { return PTMIINCD == "" ? "2" : "1"; }
        }
        public string DHI_YN_NM // 전원여부
        {
            get { return PTMIINCD == "" ? "No" : "Yes"; }
        }
        public string OIST_TRET_TXT // 타병원 진료내용
        {
            get { return DHI_YN_NM == "Yes" ? "-" : ""; }
        }
        public string CC_TXT // 주호소
        {
            get { return E12C_CC; }
        }
        public string CUR_HOC_TXT // 현병력
        {
            get { return E12C_PT; }
        }
        public string ALRG_YN // 약물 이상반응여부
        {
            get { return "3"; } // 획인불가
        }
        public string ALRG_YN_NM // 약물 이상반응여부
        {
            get { return "확인불가"; } // 획인불가
        }
        public string ALRG_TXT // 약물이상반응내용
        {
            get { return ""; }
        }
        public string ANMN_TXT // 과거력
        {
            get { return E12C_PHX == "" ? "-" : E12C_PHX; }
        }
        public string MDS_DOS_YN // 약물복용여부
        {
            get { return "3"; }
        }
        public string MDS_DOS_YN_NM // 약물복용여부
        {
            get { return "확인불가"; }
        }
        public string MDS_KND_CD // 약물종류
        {
            get { return ""; }
        }
        public string MDS_ETC_TXT // 약물종류상세
        {
            get { return ""; }
        }
        public string DRNK_YN // 음주여부
        {
            get { return "3"; }
        }
        public string DRNK_YN_NM // 음주여부
        {
            get { return "확인불가"; }
        }
        public string DRNK_TXT // 음주내용
        {
            get { return ""; }
        }
        public string SMKN_YN // 흡연여부
        {
            get { return "3"; }
        }
        public string SMKN_YN_NM // 흡연여부
        {
            get { return "확인불가"; }
        }
        public string SMKN_TXT // 흡연내용
        {
            get { return ""; }
        }
        public string FMHS_YN // 가족력여부
        {
            get { return E12C_FHX == "" ? "2" : "1"; }
        }
        public string FMHS_YN_NM // 가족력여부
        {
            get { return E12C_FHX == "" ? "No" : "Yes"; }
        }
        public string FMHS_TXT // 가족력 내용
        {
            get { return E12C_FHX == "" ? "" : E12C_FHX; }
        }
        public string ROS_TXT // 계통문진
        {
            get { return E12C_ROS; }
        }
        public string PHBD_MEDEXM_TXT // 신체검사
        {
            get { return E12C_PE; }
        }
        public string EMY_DSCG_FRM_CD // 퇴실형태
        {
            get
            {
                if (PTMIEMRT == "11") return "01";
                if (PTMIEMRT == "12") return "01";
                if (PTMIEMRT == "13") return "01";
                if (PTMIEMRT == "15") return "01";
                if (PTMIEMRT == "18") return "01";

                if (PTMIEMRT == "14") return "02";

                if (PTMIEMRT == "31") return "03";
                if (PTMIEMRT == "32") return "03";
                if (PTMIEMRT == "33") return "03";
                if (PTMIEMRT == "34") return "03";
                if (PTMIEMRT == "38") return "03";

                if (PTMIEMRT == "21") return "04";
                if (PTMIEMRT == "22") return "04";
                if (PTMIEMRT == "23") return "04";
                if (PTMIEMRT == "24") return "04";
                if (PTMIEMRT == "25") return "04";
                if (PTMIEMRT == "26") return "04";
                if (PTMIEMRT == "27") return "04";
                if (PTMIEMRT == "28") return "04";

                if (PTMIEMRT == "41") return "06";
                if (PTMIEMRT == "42") return "06";
                if (PTMIEMRT == "43") return "06";
                if (PTMIEMRT == "44") return "06";
                if (PTMIEMRT == "45") return "06";
                if (PTMIEMRT == "48") return "06";

                return "99";
            }
        }
        public string EMY_DSCG_FRM_CD_NM // 퇴실형태
        {
            get
            {
                if (EMY_DSCG_FRM_CD == "01") return "정상퇴원";
                if (EMY_DSCG_FRM_CD == "02") return "자의퇴원";
                if (EMY_DSCG_FRM_CD == "03") return "(본원)입원";
                if (EMY_DSCG_FRM_CD == "04") return "(타병원)전원";
                if (EMY_DSCG_FRM_CD == "05") return "외래추적관찰";
                if (EMY_DSCG_FRM_CD == "06") return "사망";
                if (EMY_DSCG_FRM_CD == "99") return "기타";

                return "";
            }
        }
        public string DSCG_FRM_TXT // 퇴실형태상세
        {
            get { return ""; }
        }

        // ----

        public void Clear()
        {
            R_CNT = 0;

            PTMIINDT = ""; // 응급실 도착일자
            PTMIINTM = ""; //        도착시간
            PTMIOTDT = ""; // 응급실 퇴실일자
            PTMIOTTM = ""; //        퇴실시간
            PTMIKTDT = ""; // 응급실 KTAS 분류일자
            PTMIKTTM = ""; //        KTAS 분류시간
            PTMIKTS1 = ""; //        KTAS 등급
            PTMIINCD = ""; //        전원기관코드
            PTMIEMRT = ""; // 응급시 퇴실형태

            E12C_CC = "";  // 주호소
            E12C_PT = "";  // 현병력
            E12C_PHX = ""; // 과거력
            E12C_FHX = ""; // 가족력
            E12C_ROS = ""; // 계통문진
            E12C_PE = "";  // 신체검사
        }
    }
}
