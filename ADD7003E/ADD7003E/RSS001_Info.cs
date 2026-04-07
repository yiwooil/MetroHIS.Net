using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RSS001_Info
    {
        public int R_CNT;

        // TU01 KEY
        public string PID;
        public string OPDT_KEY;
        public string DPTCD; // 수술과
        public string OPSEQ;
        public string SEQ;

        // TU01
        public string OPSDT; // 수술시작일자
        public string OPSHR; // 수술시작시간
        public string OPSMN; // 수술시작분
        public string OPEDT; // 수술종료일자
        public string OPEHR; // 수술종료시간
        public string OPEMN; // 수술종료분
        public string INSDPTCD;  // 수술과
        public string INSDPTCD2; // 수술과
        public string DRID;  // 수술의
        public string DRNM;  // 수술의명
        public string GDRLCID;  // 수술의면허번호
        public string STAFG; // 응급여부(0.정규 1.초응급 2.중응급 3.응급)

        // TU03
        public string ANETP; // 마취내역 마취종류
        public string ANTTPNM; // 마취내역 마취종류 명칭

        // TU90
        public string EMPID; // 수술기록지 작성자
        public string EMPNM; // 수술기록지 작성자명
        public string OPDT; // 수술기록지 작성일자
        public string WTIME; // 수술기록지 작성시간
        public string PREDX; // 수술기록지 수술전진단
        public string POSDX; // 수술기록지 수술후진단
        public string POS; // 수술기록지 수술체외
        public string INDIOFSURGERY; // 수술기록지 수술소견
        public string SURFNDNPRO; // 수술기록지 수술절차



        // --------------------

        public string SOPR_STA_DT // 수술 시작일시
        {
            get
            {
                string opshr = OPSHR;
                if (opshr.Length == 1) opshr = "0" + opshr;
                string opsmn = OPSMN;
                if (opsmn.Length == 1) opsmn = "0" + opsmn;
                return OPSDT + opshr + opsmn;
            }
        }
        public string SOPR_STA_DT_NM
        {
            get { return CFmtHelper.ToDateTimeString(SOPR_STA_DT); }
        }
        public string SOPR_END_DT // 수술 종료일시
        {
            get
            {
                string opehr = OPEHR;
                if (opehr.Length == 1) opehr = "0" + opehr;
                string opemn = OPEMN;
                if (opemn.Length == 1) opemn = "0" + opemn;
                return OPEDT + opehr + opemn;
            }
        }
        public string SOPR_END_DT_NM
        {
            get { return CFmtHelper.ToDateTimeString(SOPR_END_DT); }
        }
        public string SOPR_DR_CD // 구분(1.집도의 2.보조의)
        {
            get { return "1"; }
        }
        public string SOPR_DR_CD_NM
        {
            get { return "집도의"; }
        }
        public string SOPR_DGSBJT_CD // 진료과
        {
            get { return INSDPTCD; }
        }
        public string IFLD_DTL_SPC_SBJT_CD // 내과 세부진료과목
        {
            get { return (INSDPTCD == "01" ? INSDPTCD2 : ""); }
        }
        public string SOPR_DR_NM // 성명
        {
            get { return DRNM; }
        }
        public string SOPR_DR_LCS_NO // 면허번호(선택)
        {
            get { return GDRLCID; }
        }
        public string WRTP_NM // 작성자 성명
        {
            get { return EMPNM; }
        }
        public string WRT_DT // 작성일시
        {
            get { return OPDT + WTIME; }
        }
        public string WRT_DT_NM
        {
            get { return CFmtHelper.ToDateTimeString(WRT_DT); }
        }
        public string EMY_YN // 응급여부
        {
            get { return (STAFG == "0" ? "1" : "2"); }
        }
        public string EMY_YN_NM
        {
            get { return (STAFG == "0" ? "정규" : "응급"); }
        }
        public string EMY_RS_TXT // 응급사유(선택)
        {
            get { return ""; }
        }
        public string NCT_KND_TXT // 마취종류
        {
            get { return ANTTPNM; }
        }
        public string SOPR_BF_DIAG_NM // 수술전 진단명
        {
            get { return PREDX; }
        }
        public string SOPR_AF_DIAG_NM // 수술후 진단명
        {
            get { return POSDX; }
        }
        public string SOPR_PHSQ_TXT // 수술체위
        {
            get { return POS; }
        }
        public string LSN_LOCA_TXT // 병변의 위치
        {
            get { return "-"; }
        }
        public string SOPR_RST_TXT // 수술소견
        {
            get { return INDIOFSURGERY == "" ? "-" : INDIOFSURGERY; }
        }

        public string SOPR_PROC_TXT // 수술절차
        {
            get { return SURFNDNPRO; }
        }

        public string SOPR_SPCL_TXT // 중요(특이)사항
        {
            get { return "-"; }
        }

        // ---

        public void Clear()
        {
            R_CNT = 0;

            // TU01 KEY
            PID = "";
            OPDT_KEY = "";
            DPTCD = ""; // 수술과
            OPSEQ = "";
            SEQ = "";

            // TU01
            OPSDT = ""; // 수술시작일자
            OPSHR = ""; // 수술시작시간
            OPSMN = ""; // 수술시작분
            OPEDT = ""; // 수술종료일자
            OPEHR = ""; // 수술종료시간
            OPEMN = ""; // 수술종료분
            INSDPTCD = "";  // 수술과
            INSDPTCD2 = ""; // 수술과
            DRID = "";  // 수술의
            DRNM = "";  // 수술의명
            GDRLCID = "";  // 수술의면허번호
            STAFG = ""; // 응급여부(0.정규 1.초응급 2.중응급 3.응급)

            // TU03
            ANETP = ""; // 마취내역 마취종류
            ANTTPNM = ""; // 마취내역 마취종류 명칭

            // TU90
            EMPID = ""; // 수술기록지 작성자
            EMPNM = ""; // 수술기록지 작성자명
            OPDT = ""; // 수술기록지 작성일자
            WTIME = ""; // 수술기록지 작성시간
            PREDX = ""; // 수술기록지 수술전진단
            POSDX = ""; // 수술기록지 수술후진단
            POS = ""; // 수술기록지 수술체외
            INDIOFSURGERY = ""; // 수술기록지 수술소견
            SURFNDNPRO = ""; // 수술기록지 수술절차
        }


    }
}
