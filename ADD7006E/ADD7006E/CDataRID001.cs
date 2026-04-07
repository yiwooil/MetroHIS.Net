using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRID001
    {
        public string BEDEDT;
        public string BEDEHM;

        public string DPTCD_IN; // 입원진료과코드
        public string INSDPTCD_IN; // 입원진료과코드(보험코드)
        public string INSDPTCD2_IN; // 입원내과세부진료과목(보험코드)
        public string DRID_IN; // 입원의사ID
        public string DRNM_IN; // 입원의사명

        public string BEDODT;  // 퇴원일자
        public string BEDOHM;  // 퇴원시간

        public string DPTCD_OUT; // 퇴원진료과코드
        public string INSDPTCD_OUT; // 퇴원진료과코드(보험코드)
        public string INSDPTCD2_OUT; // 퇴원내과세부진료과목(보험코드)
        public string DRID_OUT; // 퇴원의사ID
        public string DRNM_OUT; // 퇴원의사명

        public string EMPID;
        public string EMPNM;
        public string WDATE;
        public string WTIME;

        public string REBED; // 30일이내 재입원 여부(1.Yes 2.No 3.확인불가) 
        public string REBED_REASON; // 30일 이재 재입원사유
        public string REBEDPLAN; // 재입원 계획 여부(1.계획에 있는 재입원 2.계획에 없는 재입원) 
        public string PREOUT; // 직전퇴원

        public List<string> COMPLAINT = new List<string>(); // 주호소
        public List<string> ERA = new List<string>(); // 발병시기

        public string HOPI; // 입원사유 및 현병력
        public string COT; // 입원경과 및 치료과정

        // 처치 및 수술
        public List<string> OPDT = new List<string>(); // 처치및수술 - 시행일시
        public List<string> OPNAME = new List<string>(); // 처치및수술 - 수술명
        public List<string> ICD9CM = new List<string>(); // 처치및수술 - ICD9CM
        public List<string> PRICD = new List<string>(); // 처치및수술 - 수가코드(EDI코드)

        // 검사소견
        public List<string> GUMDT = new List<string>(); // 검사소견 - 검사일시
        public List<string> RSDT = new List<string>(); // 검사소견 - 결과일시
        public List<string> GUMNM = new List<string>(); // 검사소견 - 검사명
        public List<string> GUMRESULT = new List<string>(); // 검사소견 - 검사결과

        // 최종진단
        public List<string> ROFG = new List<string>();	// 최종진단 - 확진여부
        public List<string> DISECD = new List<string>(); // 최종진단 - 상병분류기호
        public List<string> DXD = new List<string>(); // 최종진단 - 진단명
        public List<string> DPTCD = new List<string>(); // 최종진단 - 진료과
        public List<string> POA = new List<string>(); // 최종진단 - 입원시 상병여부
        public List<string> INSDPTCD = new List<string>();
        public List<string> INSDPTCD2 = new List<string>(); // 최종진단 - 내과 세부진료과목
        public string POA_CD(int idx)
        {
            string ret = "";
            if ("YES".Equals(POA[idx])) ret = "1";
            else if ("NO".Equals(POA[idx])) ret = "2";
            else if ("UNKNOWN".Equals(POA[idx])) ret = "3";
            else if ("UNDETERMINED".Equals(POA[idx])) ret = "4";
            else if ("기타(예외상병)".Equals(POA[idx])) ret = "5";
            else if ("".Equals(POA[idx])) ret = "2";

            if ("1".Equals(ret) == false && "2".Equals(ret) == false && "3".Equals(ret) == false && "4".Equals(ret) == false && "5".Equals(ret) == false)
            {
                ret = "2";
            }
            return ret;
        }
        public string DEPT_INFO(int idx) { return DPTCD[idx] + "(" + INSDPTCD[idx] + INSDPTCD2[idx] + ")"; }

        // 전과
        public List<string> TR_DATE = new List<string>(); // 전과일시
        public List<string> TR_OUT_DPT = new List<string>();	// 의뢰과
        public List<string> TR_OUT_INSDPTCD = new List<string>();
        public List<string> TR_OUT_INSDPTCD2 = new List<string>();
        public string TR_OUT_DEPT_INFO(int idx) {return TR_OUT_DPT[idx] + "(" + TR_OUT_INSDPTCD[idx] + TR_OUT_INSDPTCD2[idx] + ")";}
        public List<string> TR_OUT_DRNM = new List<string>(); // 의뢰의사 성명
        public List<string> TR_OUT_DR = new List<string>(); // 의뢰의사 면허종류
        public List<string> TR_OUT_DRLCID = new List<string>(); // 의뢰의사 면허번호
        public List<string> TR_OUT_REASON = new List<string>(); // 전과사유
        public List<string> TR_IN_DPT = new List<string>(); // 회신과
        public List<string> TR_IN_INSDPTCD = new List<string>();
        public List<string> TR_IN_INSDPTCD2 = new List<string>();
        public string TR_IN_DEPT_INFO(int idx) { return TR_IN_DPT[idx] + "(" + TR_IN_INSDPTCD[idx] + TR_IN_INSDPTCD2[idx] + ")"; }
        public List<string> TR_IN_DRNM = new List<string>(); // 회신의사 성명
        public List<string> TR_IN_DR = new List<string>(); // 회신의사 면허종류
        public List<string> TR_IN_DRLCID = new List<string>(); // 회신의사 면허번호

        public string ALLERGY; // 약물이상반응

        // 환자 상태 척도
        public List<string> ERA_CD = new List<string>(); // 측정시기
        public List<string> ERA_ETC_TXT = new List<string>(); // 측정시기 기타 상세
        public List<string> TL_NM = new List<string>(); // 도구
        public List<string> RST_TXT = new List<string>(); // 결과
        public List<string> RMK_TXT = new List<string>(); // 참고사항

        public string HEPA; // 감염
        public string COMPLICATION; // 합병증
        public string PTNT_YN; // 환자 안전관리 발생여부
        public string PTNT_TXT; // 환자 안전관리 상세내용
        public string OUTREASON; // 퇴원 형태
        public string OUTSTATUS; // 퇴원시 환자상태
        public string DEATHDATE; // 사망일자
        public string DEATHTIME; // 사망시간
        public string TRHOSREASON; // 전원사유
        public string TRHOSREASON_TXT; // 전원사유. 기타
        public string DHI_RS_TXT // 전원사유.평문
        {
            get
            {
                string ret = "";
                if (TRHOSREASON == "01") ret = "병실 부족";
                else if (TRHOSREASON == "02") ret = "중환자실 부족";
                else if (TRHOSREASON == "03") ret = "응급수술 처치불가";
                else if (TRHOSREASON == "04") ret = "1,2차 기관 전원";
                else if (TRHOSREASON == "05") ret = "3차 기관 전원";
                else if (TRHOSREASON == "06") ret = "장기시설로 전원";
                else if (TRHOSREASON == "07") ret = "방사선치료";
                else if (TRHOSREASON == "08") ret = "급성기 치료";
                else if (TRHOSREASON == "09") ret = "급성기 치료 후";
                else if (TRHOSREASON == "10") ret = "환자사정";
                else if (TRHOSREASON == "99") ret = TRHOSREASON_TXT;
                return ret;
            }
        }
        public string OUTCARE; // 퇴원후진료계획

        // 퇴원 처방
        public List<string> ORDER_TYPE = new List<string>(); // 처방구분
        public List<string> ONM = new List<string>(); // 약품명
        public List<string> OUNIT = new List<string>(); // 용법
        public List<string> OQTY = new List<string>(); // 1회 투약량
        public List<string> ORDCNT = new List<string>(); // 1일 투여횟수
        public List<string> ODAYCNT = new List<string>(); // 총 투약일수

        public string DEDX_DISECD; //원사인 상병분류기호(사망한 경우)
        public string DEDX_DXD; // 진단면(사망한 경우)

        public string BEDEDTM { get { return BEDEDT + BEDEHM; } }
        public string BEDODTM { get { return BEDODT + BEDOHM; } }
        public string DEPTINFO_IN { get { return DPTCD_IN + "(" + INSDPTCD_IN + INSDPTCD2_IN + ")"; } }
        public string DEPTINFO_OUT { get { return DPTCD_OUT + "(" + INSDPTCD_OUT + INSDPTCD2_OUT + ")"; } }
        public string WDTM { get { return WDATE + WTIME; } }
        public string PREOUT_CD { get { return (REBED == "1" ? GetSepString(PREOUT, 1) : ""); } } // 직진퇴워일(1.알고있음 2.모름) 
        public string PREOUT_DT { get { return (REBED == "1" ? GetSepString(PREOUT, 2) : ""); } } // 직전퇴원일자
        public string ALLERGY_CD { get { return GetSepString(ALLERGY, 1); } } // 약 알러지여부
        public string ALLERGY_DESC { get { return GetSepString(ALLERGY, 2); } } // 약 알러지내용
        public string HEPA_CD { get { return GetSepString(HEPA, 1); } } // 감염 여부
        public string HEPA_DESC { get { return GetSepString(HEPA, 2); } } // 감염 내용
        public string COMPLICATION_CD { get { return GetSepString(COMPLICATION, 1); } } // 합병증 여부
        public string COMPLICATION_DESC { get { return GetSepString(COMPLICATION, 2); } } // 합병증 내용
        public string OUTREASON_CD { get { return GetSepString(OUTREASON, 1); } } // 퇴원 형태
        public string OUTREASON_DESC { get { return OUTREASON_CD == "99" ? GetSepString(OUTREASON, 2) : ""; } } // 퇴원 형태가 기타인 경우 상세
        public string OUTSTATUS_CD { get { return OUTREASON_CD == "06" ? "" : GetSepString(OUTSTATUS, 1); } } // 퇴원시 환자 상태
        public string OUTSTATUS_DESC { get { return OUTSTATUS_CD == "9" ? GetSepString(OUTSTATUS, 2) : ""; } } // 퇴원시 환자 상태가 기타인 경우 상세
        public string DEATHDTM { get { return DEATHDATE + DEATHTIME; } } // 사망일시
        public string DEATH_SICK { get { return DEDX_DISECD; } } // 원사인
        public string DEATH_DIAG { get { return DEDX_DXD; } } // 진단명
        public string OUTCARE_CD // 퇴원 후 진료계획
        {
            get
            {
                string ret = "";
                if (OUTCARE == "0") ret = "0";
                else if (OUTCARE == "1") ret = "1";
                else if (OUTCARE == "2") ret = "2";
                else ret = "9";
                return ret;
            }
        }
        public string OUTCARE_DESC // 퇴원 후 진료계획 상세
        {
            get
            {
                string ret = "";
                if (OUTCARE == "0") ret = "";
                else if (OUTCARE == "1") ret = "";
                else if (OUTCARE == "2") ret = "";
                else ret = OUTCARE;
                return ret;
            }
        }

        public void Clear()
        {
            BEDEDT = "";
            BEDEHM = "";

            DPTCD_IN = ""; // 입원진료과코드
            INSDPTCD_IN = ""; // 입원진료과코드(보험코드)
            INSDPTCD2_IN = ""; // 입원내과세부진료과목(보험코드)
            DRID_IN = ""; // 입원의사ID
            DRNM_IN = ""; // 입원의사명

            BEDODT = "";
            BEDOHM = "";

            DPTCD_OUT = ""; // 퇴원진료과코드
            INSDPTCD_OUT = ""; // 퇴원진료과코드(보험코드)
            INSDPTCD2_OUT = ""; // 퇴원내과세부진료과목(보험코드)
            DRID_OUT = ""; // 퇴원의사ID
            DRNM_OUT = ""; // 퇴원의사명

            EMPID = "";
            EMPNM = "";
            WDATE = "";
            WTIME = "";

            REBED = ""; // 30일이내 재입원 여부(1.Yes 2.No 3.확인불가) 
            REBED_REASON = ""; // 30일 이재 재입원사유
            REBEDPLAN = ""; // 재입원 계획 여부(1.계획에 있는 재입원 2.계획에 없는 재입원) 
            PREOUT = ""; // 직전퇴원일

            COMPLAINT.Clear(); // 주호소
            ERA.Clear(); // 발병시기

            HOPI = ""; // 입원사유 및 현병력
            COT = ""; // 입원경과 및 치료과정

            // 처치및수술
            OPDT.Clear(); // 처치및수술 - 시행일시
            OPNAME.Clear(); // 처치및수술 - 수술명
            ICD9CM.Clear(); // 처치및수술 - ICD9CM
            PRICD.Clear(); // 처치및수술 - 수가코드(EDI코드)

            // 검사소견
            GUMDT.Clear(); // 검사소견 - 검사일시
            RSDT.Clear(); // 검사소견 - 결과일시
            GUMNM.Clear(); // 검사소견 - 검사명
            GUMRESULT.Clear(); // 검사소견 - 검사결과

            // 최종 진단
            ROFG.Clear();	// 최종진단 - 확진여부
            DISECD.Clear(); // 최종진단 - 상병분류기호
            DXD.Clear(); // 최종진단 - 진단명
            DPTCD.Clear(); // 최종진단 - 진료과
            POA.Clear(); // 최종진단 - 입원시 상병여부
            INSDPTCD.Clear();
            INSDPTCD2.Clear(); // 최종진단 - 내과 세부진료과목

            // 전과
            TR_DATE.Clear(); // 전과일시
            TR_OUT_DPT.Clear();	// 의뢰과
            TR_OUT_INSDPTCD.Clear();
            TR_OUT_INSDPTCD2.Clear();
            TR_OUT_DRNM.Clear(); // 의뢰의사 성명
            TR_OUT_DR.Clear(); // 의뢰의사 면허종류
            TR_OUT_DRLCID.Clear(); // 의뢰의사 면허번호
            TR_OUT_REASON.Clear(); // 전과사유
            TR_IN_DPT.Clear(); // 회신과
            TR_IN_INSDPTCD.Clear();
            TR_IN_INSDPTCD2.Clear();
            TR_IN_DRNM.Clear(); // 회신의사 성명
            TR_IN_DR.Clear(); // 회신의사 면허종류
            TR_IN_DRLCID.Clear(); // 회신의사 면허번호

            ALLERGY = ""; // 약물이상반응여부

            // 환자 상태 척도
            ERA_CD.Clear(); // 측정시기
            ERA_ETC_TXT.Clear(); // 측정시기 기타 상세
            TL_NM.Clear(); // 도구
            RST_TXT.Clear(); // 결과
            RMK_TXT.Clear(); // 참고사항

            HEPA = ""; // 감염
            COMPLICATION = ""; // 합병증
            PTNT_YN = ""; // 환자 안전관리 발생여부
            PTNT_TXT = ""; // 환자 안전관리 상세내용

            OUTREASON = ""; // 퇴원 형태
            OUTSTATUS = ""; // 퇴원시 환자상태
            DEATHDATE = ""; // 사망일자
            DEATHTIME = ""; // 사망시간
            TRHOSREASON = ""; // 전원사유
            OUTCARE = ""; // 퇴원후진료계획

            // 퇴원 처방
            ORDER_TYPE.Clear(); // 처방구분
            ONM.Clear(); // 약품명
            OUNIT.Clear(); // 용법
            OQTY.Clear(); // 1회 투약량
            ORDCNT.Clear(); // 1일 투여횟수
            ODAYCNT.Clear(); // 총 투약일수

            DEDX_DISECD = ""; //원사인 상병분류기호(사망한 경우)
            DEDX_DXD = ""; // 진단면(사망한 경우)
        }

        private string GetSepString(string p_val, int pos)
        {
            String[] tmp;
            tmp = (p_val + (char)22).Split((char)22);
            return tmp[pos-1];
        }
    }
}
