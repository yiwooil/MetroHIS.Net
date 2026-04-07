using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7000E
{
    class CDataTV100
    {
        private Dictionary<String, String> m_Dptcd = new Dictionary<String, String>();
        private Dictionary<String, String> m_Dptcd2 = new Dictionary<String, String>();

        private String BEDEDT; // 입원일자
        private String BEDEHM; // 입원시간
        public String BEDEDTHM // 입원일자시간
        {
            get
            {
                return BEDEDT + BEDEHM;
            }
        }
        private String DRID_IN;    // 입원의사ID
        private String DPTCD_IN;   // 입원진료과코드
        private String PDPTCD_IN;  // 입원진료과코드(진료분야)
        public String INSDPTCD_IN; // 입원진료과코드(보험코드)
        public String DRTYPE_IN    // 입원의사종류(1.의사 2.치과의사 3.한의사)
        {
            get
            {
                String strRet = "";
                if ("7".Equals(PDPTCD_IN))
                {
                    strRet = "3";
                }
                else if ("6".Equals(PDPTCD_IN))
                {
                    strRet = "2";
                }
                else
                {
                    strRet = "1";
                }
                return strRet;
            }
        }
       
        public String GDRLCID_IN;  // 입원의사면허번호
        public String DRNM_IN;  // 입원의사성명
        private String BEDODT;  // 퇴원일자
        private String BEDOHM;  // 퇴원시간
        public String BEDODTHM // 입원일자시간
        {
            get
            {
                return BEDODT + BEDOHM;
            }
        }
        private String DRID_OUT;    // 퇴원의사ID
        private String DPTCD_OUT;   // 퇴원진료과코드
        private String PDPTCD_OUT;  // 퇴원진료과코드(진료분야)
        public String INSDPTCD_OUT; // 퇴원진료과코드(보험코드)
        public String DRTYPE_OUT    // 퇴원의사종류(1.의사 2.치과의사 3.한의사)
        {
            get
            {
                String strRet = "";
                if ("7".Equals(PDPTCD_OUT))
                {
                    strRet = "3";
                }
                else if ("6".Equals(PDPTCD_OUT))
                {
                    strRet = "2";
                }
                else
                {
                    strRet = "1";
                }
                return strRet;
            }
        }
        public String GDRLCID_OUT;  // 퇴원의사면허번호
        public String DRNM_OUT;  // 퇴원의사성명
        private String EMPID; // 작성자ID
        public String EMPNM; // 작성자성명
        private String WDATE;  // 작성일자
        private String WTIME;  // 작성시간
        public String WDTTM // 작성일자시간
        {
            get
            {
                return WDATE + WTIME;
            }
        }
        public String REBED; // 30일 이내 재입원여부
        private String REBEDPLAN; // 재입원 계획 여부
        public String REBEDPLAN_CD
        {
            get
            {
                if ("1".Equals(REBED) == false)
                {
                    return "";
                }
                else
                {
                    return REBEDPLAN;
                }
            }
        }
        private String PREOUT; // 직전퇴원일 아는지 여부
        public String PREOUT_CD
        {
            get
            {
                if ("1".Equals(REBED) == false)
                {
                    return "";
                }
                else
                {
                    String[] tmp;
                    tmp = (PREOUT + (char)22).Split((char)22);
                    return tmp[0];
                }
            }
        }
        public String PREOUT_DT
        {
            get
            {
                if ("1".Equals(REBED)==false)
                {
                    return "";
                }
                else
                {
                    String[] tmp;
                    tmp = (PREOUT + (char)22).Split((char)22);
                    return tmp[1];
                }
            }
        }
        public String COMPLAINT; // 주호소
        public String HOPI; // 입원사유 및 현병력
        public String COT; // 입원경과 및 치료과정 요약
        public String TRDPT; // 전과유무
        private String ALLERGY; // 약 알러지여부
        public String ALLERGY_CD // 약 알러지여부
        {
            get
            {
                String[] tmp;
                tmp = (ALLERGY + (char)22).Split((char)22);
                return tmp[0];
            }
        }
        public String ALLERGY_DESC // 약 알러지내용
        {
            get
            {
                String[] tmp;
                tmp = (ALLERGY + (char)22).Split((char)22);
                return tmp[1];
            }
        }
        private String DRUGNAME; // 의약품명칭
        public int CountDRUGNAME
        {
            get
            {
                if ("".Equals(DRUGNAME)) return 0;
                String[] tmp;
                tmp = DRUGNAME.Split((char)22);
                return tmp.Length;
            }
        }
        public String GetDRUGNAME(int index)
        {
            String[] tmp;
            tmp = DRUGNAME.Split((char)22);
            return tmp[index];
        }
        private String HEPA; // 감염 여부
        public String HEPA_CD // 감염 여부
        {
            get
            {
                String[] tmp;
                tmp = (HEPA + (char)22).Split((char)22);
                return tmp[0];
            }
        }
        public String HEPA_DESC // 감염 내용
        {
            get
            {
                String[] tmp;
                tmp = (HEPA + (char)22).Split((char)22);
                return tmp[1];
            }
        }
        private String COMPLICATION; // 합병증 여부
        public String COMPLICATION_CD // 합병증 여부
        {
            get
            {
                String[] tmp;
                tmp = (COMPLICATION + (char)22).Split((char)22);
                return tmp[0];
            }
        }
        public String COMPLICATION_DESC // 합병증 내용
        {
            get
            {
                String[] tmp;
                tmp = (COMPLICATION + (char)22).Split((char)22);
                return tmp[1];
            }
        }
        private String OUTREASON; // 퇴원 형태
        public String OUTREASON_CD // 퇴원 형태
        {
            get
            {
                String[] tmp;
                tmp = (OUTREASON + (char)22).Split((char)22);
                return tmp[0];
            }
        }
        public String OUTREASON_DESC // 퇴원 형태가 기타인 경우 상세
        {
            get
            {
                String strRet = "";
                if ("99".Equals(OUTREASON_CD))
                {
                    // 기타인 경우만 작성
                    String[] tmp;
                    tmp = (OUTREASON + (char)22).Split((char)22);
                    strRet = tmp[1];
                }
                return strRet;
            }
        }
        private String OUTSTATUS; // 퇴원시 환자 상태
        public String OUTSTATUS_CD // 퇴원시 환자 상태
        {
            get
            {
                String strRet = "";
                if ("06".Equals(OUTREASON_CD))
                {
                    // 퇴원형태가 06.사망이면 빈 값을 반환한다.
                }
                else
                {
                    String[] tmp;
                    tmp = (OUTSTATUS + (char)22).Split((char)22);
                    strRet = tmp[0];
                }
                return strRet;
            }
        }
        public String OUTSTATUS_DESC // 퇴원시 환자 상태가 기타인 경우 상세
        {
            get
            {
                String strRet = "";
                if ("9".Equals(OUTSTATUS_CD))
                {
                    // 기타인 경우만 상세를 작성
                    String[] tmp;
                    tmp = (OUTSTATUS + (char)22).Split((char)22);
                    strRet = tmp[1];
                }
                return strRet;
            }
        }

        private String DEATHDATE; // 사망일자
        private String DEATHTIME; // 사망시간
        public String DEATHDTTM // 사망일자시간
        {
            get
            {
                return DEATHDATE + DEATHTIME;
            }
        }
        public String TRHOS; // 전원요구
        public String TRHOSREASON; // 전원사유
        public String CONTICARE; // 연속적치료
        public String TRHOSNUM; // 전원보낸기관기호
        public String TRHOSNAME; // 전원보낸기관명
        public String TRHOSDPT; // 전원보낸진료과
        private String OUTCARE; // 퇴원 후 진료계획
        public String OUTCARE_CD // 퇴원 후 진료계획
        {
            get
            {
                String[] tmp;
                tmp = (OUTCARE + (char)22 + (char)22).Split((char)22);
                return tmp[0];
            }
        }
        public String OUTCARE_DPTCD // 퇴원 후 예약진료과
        {
            get
            {
                String[] tmp;
                tmp = (OUTCARE + (char)22 + (char)22).Split((char)22);
                String strRet = GetInsDptcd(tmp[1]);
                return strRet;
            }
        }
        public String OUTCARE_DT // 퇴원 후 예약일자
        {
            get
            {
                String[] tmp;
                tmp = (OUTCARE + (char)22 + (char)22).Split((char)22);
                return tmp[2];
            }
        }
        private String OTELNO; // 회사전화번호
        private String HTELNO; // 집전화번호
        public String TELNO
        {
            get
            {
                String strTelno = "";
                if ("".Equals(OTELNO)) strTelno = HTELNO;
                else strTelno = OTELNO;
                // 전화번호는 숫자와 -부호만 사용하고 다른 문자는 버린다.
                String strRet = "";
                long lngLength = strTelno.Length;
                for (int i = 1; i <= lngLength; i++)
                {
                    String s = strTelno.Substring(i - 1, 1);
                    if ("-".Equals(s))
                    {
                        strRet += s;
                    }
                    else
                    {
                        long lngNumber = 0;
                        if (long.TryParse(s, out lngNumber) == true)
                        {
                            strRet += s;
                        }
                    }
                }
                if (strRet.Length > 20) strRet = strRet.Substring(0, 20);// 20자로 제한
                return strRet; 
            }
        }
        public String NEWDACD; // 조직적 진단코드
        public String NEWDXD; // 조직적 진단명
        private String OUTFOS; // 퇴원시 Functional outcom scale
        public String OUTFOS_KIND // 퇴원시 Functional outcom scale.종류
        {
            get
            {
                String[] tmp;
                tmp = (OUTFOS + (char)22 + (char)22).Split((char)22);
                return tmp[0];
            }
        }
        public String OUTFOS_SCALE // 퇴원시 Functional outcom scale.Scale
        {
            get
            {
                String[] tmp;
                tmp = (OUTFOS + (char)22 + (char)22).Split((char)22);
                return tmp[1];
            }
        }
        public String OUTFOS_TOP // 퇴원시 Functional outcom scale.최고점
        {
            get
            {
                String[] tmp;
                tmp = (OUTFOS + (char)22 + (char)22).Split((char)22);
                return tmp[2];
            }
        }
        public String SCORE; // 퇴원시 Functional outcom scale.평가점수
        private String ASSPERSON;// 퇴원시 Functional outcom scale.평가자
        public String ASSPERSON_CD // 퇴원시 Functional outcom scale.평가자
        {
            get
            {
                String[] tmp;
                tmp = (ASSPERSON + (char)22).Split((char)22);
                return tmp[0];
            }
        }
        public String ASSPERSON_ETC // 퇴원시 Functional outcom scale.평가자
        {
            get
            {
                String[] tmp;
                tmp = (ASSPERSON + (char)22).Split((char)22);
                return tmp[1];
            }
        }
        public int TRCOUNT; // 전과건수
        public List<String> TRDATE = new List<String>(); // 전과일시
        public List<String> TROUTDPT = new List<String>(); // 의뢰과
        public List<String> TROUTDPT2 = new List<String>(); // 의뢰과 내과 세부진료과목 *** 002 버전
        public List<String> TROUTDRNM = new List<String>(); // 의뢰의사 성명
        public List<String> TROUTDR = new List<String>(); // 의뢰의사 면허종류
        public List<String> TROUTDRLCID = new List<String>(); // 의뢰의사 면허번호
        public List<String> TROUTREASON = new List<String>(); // 진과사유
        public List<String> TRINDPT = new List<String>(); // 회신과
        public List<String> TRINDPT2 = new List<String>(); // 회신과 내과 세부진료과목 *** 002 버전
        public List<String> TRINDRNM = new List<String>(); // 회신의사 면허종류 
        public List<String> TRINDR = new List<String>(); // 회신의사 면허종류 
        public List<String> TRINDRLCID = new List<String>(); // 회신의사 면허번호
        public int OPCOUNT; // 처치 및 수술건수
        public List<String> OPDT = new List<String>(); // 수술일시
        public List<String> OPNAME = new List<String>(); // 수술명
        public List<String> ICD9CM = new List<String>(); // ICD9CM
        public List<String> PRICD = new List<String>(); // 수가코드
        public int DX_COUNT; // 최종진단 건수
        public List<String> DX_ROFG = new List<String>(); // 확진여부
        public List<String> DX_DISECD = new List<String>(); // 상병분류기호
        public List<String> DX_DXD = new List<String>(); // 진단명
        public List<String> DX_DPTCD = new List<String>(); // 진료과
        public List<String> DX_INSDPTCD = new List<String>(); // 진료과
        public List<String> DX_INSDPTCD2 = new List<String>(); // 내과 세부진료과목 *** 002 버전
        public List<String> DX_POA = new List<String>(); // 입원시 상병여부
        public int DEDX_COUNT; // 원사인진단 건수
        public List<String> DEDX_DISECD = new List<String>(); // 원사인 상병분류기호
        public List<String> DEDX_DXD = new List<String>(); // 원사인 진단명
        public int DCORCOUNT; // 퇴원처방 건수
        public List<String> ORDER_TYPE = new List<String>(); // 처방구분 *** 002 버전
        public List<String> ONM = new List<String>(); // 퇴원처방 약품명
        public List<String> OUNIT = new List<String>(); // 퇴원처방 용법
        public List<String> OQTY = new List<String>(); // 퇴원처방 1회 투약량
        public List<String> ORDCNT = new List<String>(); // 퇴원처방 1일 투여횟수
        public List<String> ODAYCNT = new List<String>(); // 퇴원처방 총 투약일수
        public int GUM_COUNT; // 검사소견 건수
        public List<String> GUM_ODT = new List<String>(); // 검사소견 처방일
        public List<String> GUM_GUMDT = new List<String>(); // 검사소견 검사일시
        public List<String> GUM_RSDT = new List<String>(); // 검사소견 결과일시
        public List<String> GUM_ONM = new List<String>(); // 검사소견 검사명
        public List<String> GUM_GUMRESULT = new List<String>(); // 검사소견 검사결과

        // 이하 002 버전에서 추가된 내용
        public String VER;
        public String INSDPTCD2_IN;
        public String INSDPTCD2_OUT;
        public String REBED_REASON;
        public String PTNT_YN;
        public String PTNT_TXT;
        public int CC_COUNT; // 주호소 건수
        public List<String> CC_COMPLAINT = new List<String>(); // 주호소
        public List<String> CC_ERA = new List<String>(); // 주호소 발병시기
        public int MASR_COUNT; // 환자상태척도 건수
        public List<String> MASR_ERA_CD = new List<String>(); // 측정시기
        public List<String> MASR_ERA_ETC_TXT = new List<String>(); // 측정시기 기타 상세(평문)
        public List<String> MASR_ERA_ETC_DT = new List<String>(); // 측정시기 기타 상세(yyyymmddhhmm 형식)
        public List<String> MASR_TL_NM = new List<String>(); // 도구
        public List<String> MASR_RST_TXT = new List<String>(); // 결과
        public List<String> MASR_RMK_TXT = new List<String>(); // 참고사항
        public String TRHOSREASON_TXT
        {
            get
            {
                string strRet = "";
                if (TRHOSREASON == "1") strRet = "병실 부족";
                else if (TRHOSREASON == "2") strRet = "중환자실 부족";
                else if (TRHOSREASON == "3") strRet = "응급수술처치불가";
                else if (TRHOSREASON == "4") strRet = "1,2차 기관 전원";
                else if (TRHOSREASON == "5") strRet = "3차 기관 전원";
                else if (TRHOSREASON == "6") strRet = "장기시설로 전원";
                else if (TRHOSREASON == "7") strRet = "방사선 치료";
                else if (TRHOSREASON == "8") strRet = "급성기 치료";
                else if (TRHOSREASON == "9") strRet = "급성기 치료 후";
                else if (TRHOSREASON == "10") strRet = "환자사정";
                else strRet = TRHOSREASON;
                return strRet;
            }
        }
        public String OUTCARE_COD // 퇴원 후 진료계획
        {
            get
            {
                String strRet="";
                if (OUTCARE == "0") strRet = "0";
                else if (OUTCARE == "1") strRet = "1";
                else if (OUTCARE == "2") strRet = "2";
                else strRet = "9";
                return strRet;
            }
        }
        public String OUTCARE_TXT // 퇴원 후 진료계획 기타내용
        {
            get
            {
                String strRet = "";
                if (OUTCARE == "0") strRet = "";
                else if (OUTCARE == "1") strRet = "";
                else if (OUTCARE == "2") strRet = "";
                else strRet = OUTCARE;
                return strRet;
            }
        }


        public void Clear()
        {
            m_Dptcd.Clear();
            m_Dptcd2.Clear();

            BEDEDT = "";
            BEDEHM = "";
            DRID_IN = "";
            DPTCD_IN = "";
            PDPTCD_IN = "";
            INSDPTCD_IN = "";
            GDRLCID_IN = "";
            DRNM_IN = "";
            BEDODT = "";
            BEDOHM = "";
            DRID_OUT = "";
            DPTCD_OUT = "";
            PDPTCD_OUT = "";
            INSDPTCD_OUT = "";
            GDRLCID_OUT = "";
            DRNM_OUT = "";
            EMPID = "";
            EMPNM = "";
            WDATE = "";
            WTIME = "";
            REBED = "";
            REBEDPLAN = "";
            PREOUT = "";
            COMPLAINT = "";
            HOPI = "";
            COT = "";
            TRDPT = "";
            ALLERGY = "";
            DRUGNAME = "";
            HEPA = "";
            COMPLICATION = "";
            OUTREASON = "";
            DEATHDATE = "";
            DEATHTIME = "";
            TRHOS = "";
            TRHOSREASON = "";
            CONTICARE = "";
            TRHOSNUM = "";
            TRHOSNAME = "";
            TRHOSDPT = "";
            OUTCARE = "";
            OTELNO = "";
            HTELNO = "";
            NEWDACD = "";
            NEWDXD = "";
            OUTFOS = "";
            SCORE = "";
            ASSPERSON = "";
            TRCOUNT = 0;
            TRDATE.Clear();
            TROUTDPT.Clear();
            TROUTDPT2.Clear(); // *** 002 버전
            TROUTDRNM.Clear();
            TROUTDR.Clear();
            TROUTDRLCID.Clear();
            TROUTREASON.Clear();
            TRINDPT.Clear();
            TRINDPT2.Clear(); // *** 002 버전
            TRINDRNM.Clear();
            TRINDR.Clear();
            TRINDRLCID.Clear();
            OPCOUNT = 0;
            OPDT.Clear();
            OPNAME.Clear();
            ICD9CM.Clear();
            PRICD.Clear();
            DX_COUNT = 0;
            DX_ROFG.Clear();
            DX_DISECD.Clear();
            DX_DXD.Clear();
            DX_DPTCD.Clear();
            DX_POA.Clear();
            DX_INSDPTCD.Clear();
            DX_INSDPTCD2.Clear(); // *** 002 버전
            DEDX_COUNT = 0;
            DEDX_DISECD.Clear();
            DEDX_DXD.Clear();
            DCORCOUNT = 0;
            ORDER_TYPE.Clear(); // *** 002 버전
            ONM.Clear();
            OUNIT.Clear();
            OQTY.Clear();
            ORDCNT.Clear();
            ODAYCNT.Clear();
            GUM_COUNT = 0;
            GUM_ODT.Clear();
            GUM_GUMDT.Clear();
            GUM_RSDT.Clear();
            GUM_ONM.Clear();
            GUM_GUMRESULT.Clear();

            // 이하 002 버전에서 추가된 내용
            VER = "";
            INSDPTCD2_IN = "";
            INSDPTCD2_OUT = "";
            REBED_REASON = "";
            PTNT_YN = "";
            PTNT_TXT = "";
            CC_COUNT = 0;
            CC_COMPLAINT.Clear();
            CC_ERA.Clear();
            MASR_COUNT = 0;
            MASR_ERA_CD.Clear();
            MASR_ERA_ETC_TXT.Clear();
            MASR_ERA_ETC_DT.Clear();
            MASR_TL_NM.Clear();
            MASR_RST_TXT.Clear();
            MASR_RMK_TXT.Clear();
        }

        public void SetData(CDataTI2A i2a)
        {
            try
            {

                string sql = "";
                sql += System.Environment.NewLine + "SELECT	V100.BEDEDT";	                /* 입원일자 */
                sql += System.Environment.NewLine + "     , V100.BEDEHM";                   /* 입원시간 */
                sql += System.Environment.NewLine + "     , V100.INDRID DRID_IN";           /* 입원의사ID */
                sql += System.Environment.NewLine + "     , A09IN.DPTCD DPTCD_IN";          /* 입원과 */
                sql += System.Environment.NewLine + "     , A09IN.PRIMDPTCD PDPTCD_IN";     /* 입원과(진료분야) */
                sql += System.Environment.NewLine + "     , A09IN.INSDPTCD INSDPTCD_IN";    /* 입원과(보험분류과) */
                sql += System.Environment.NewLine + "     , A07IN.GDRLCID GDRLCID_IN";      /* 입원의사면허번호 */
                sql += System.Environment.NewLine + "     , A07IN.DRNM DRNM_IN";            /* 입원의사성명 */
                sql += System.Environment.NewLine + "     , V100.BEDODT";                   /* 퇴원일자 */
                sql += System.Environment.NewLine + "     , V100.BEDOHM";                   /* 퇴원시간 */
                sql += System.Environment.NewLine + "     , V100.OUTDRID DRID_OUT";         /* 퇴원의사ID */
                sql += System.Environment.NewLine + "     , A09OUT.DPTCD DPTCD_OUT";        /* 퇴원과 */
                sql += System.Environment.NewLine + "     , A09OUT.PRIMDPTCD PDPTCD_OUT";   /* 퇴원과(진료분야) */
                sql += System.Environment.NewLine + "     , A09OUT.INSDPTCD INSDPTCD_OUT";  /* 퇴원과(보험분류과) */
                sql += System.Environment.NewLine + "     , A07OUT.GDRLCID GDRLCID_OUT";    /* 퇴원의사면허번호 */
                sql += System.Environment.NewLine + "     , A07OUT.DRNM DRNM_OUT";          /* 퇴원의사성명 */
                sql += System.Environment.NewLine + "     , V100.EMPID";                    /* 작성자ID */
                sql += System.Environment.NewLine + "     , V100.WDATE";                    /* 작성일자 */
                sql += System.Environment.NewLine + "     , V100.WTIME";                    /* 작성시간 */
                sql += System.Environment.NewLine + "     , V100.REBED";                    /* 30일이내 재입원 여부(1.유 0.무) */
                sql += System.Environment.NewLine + "     , V100.REBEDPLAN";                /* 재입원 계획 여부(1.유 0.무) */
                sql += System.Environment.NewLine + "     , V100.PREOUT";                   /* 직전퇴원일 아는지 여부(1.유 0.무) */
                sql += System.Environment.NewLine + "     , V100.COMPLAINT";                /* 주호소 */
                sql += System.Environment.NewLine + "     , V100.HOPI";                     /* 입원사유 및 현병력 */
                sql += System.Environment.NewLine + "     , V100.COT";                      /* 입원경과 및 치료과정 요약 */
                sql += System.Environment.NewLine + "     , V100.TRDPT";                    /* 전과여부 */
                sql += System.Environment.NewLine + "     , V100.ALLERGY";                  /* 약 알러지여부 */
                sql += System.Environment.NewLine + "     , V100.DRUGNAME";                 /* 의약품명칭 */
                sql += System.Environment.NewLine + "     , V100.HEPA";                     /* 감염 여부 */
                sql += System.Environment.NewLine + "     , V100.COMPLICATION";             /* 합병증 여부 */
                sql += System.Environment.NewLine + "     , V100.OUTREASON";                /* 퇴원 형태 */
                sql += System.Environment.NewLine + "     , V100.OUTSTATUS";                /* 퇴원시 환자상태(완쾌:0, 호전:1, 호전안됨:2, 치료없이 진단만:3, 기타:4 + CHAR(22) + 기타내용 */
                sql += System.Environment.NewLine + "     , V100.DEATHDATE";                /* 사망 일자 */
                sql += System.Environment.NewLine + "     , V100.DEATHTIME";                /* 사망 시간 */
                sql += System.Environment.NewLine + "     , V100.TRHOS";                    /* 전원 요구 */
                sql += System.Environment.NewLine + "     , V100.TRHOSREASON";              /* 전원 사유 */
                sql += System.Environment.NewLine + "     , V100.CONTICARE";                /* 연속적 치료 */
                sql += System.Environment.NewLine + "     , V100.TRHOSNUM";                 /* 전원보낸 기관기호 */
                sql += System.Environment.NewLine + "     , V100.TRHOSNAME";                /* 전원보낸 기관명 */
                sql += System.Environment.NewLine + "     , V100.TRHOSDPT";                 /* 전원보낸 진료과 */
                sql += System.Environment.NewLine + "     , V100.OUTCARE";                  /* 퇴원후 진료계획 */
                sql += System.Environment.NewLine + "     , A01.OTELNO";                    /* 회사 전화번호 */
                sql += System.Environment.NewLine + "     , A01.HTELNO";                    /* 집 전화번호 */
                sql += System.Environment.NewLine + "     , V100.NEWDACD";                  /* 조직적 진단코드 */
                sql += System.Environment.NewLine + "     , V100.NEWDXD";                   /* 조직적 진단명 */
                sql += System.Environment.NewLine + "     , V100.OUTFOS";                   /* 퇴원시 Functional outcom scale */
                sql += System.Environment.NewLine + "     , V100.SCORE";                    /* 퇴원시 Functional outcom scale.Scale */
                sql += System.Environment.NewLine + "     , V100.ASSPERSON";                /* 퇴원시 Functional outcom scale.평가자 */
                                                                                            /* 이하 002버전 추가 */
                sql += System.Environment.NewLine + "     , V100.VER";                      /* 버전 */
                sql += System.Environment.NewLine + "     , A09IN.INSDPTCD2 INSDPTCD2_IN";  /* 입원과(내과세부진료과목) */
                sql += System.Environment.NewLine + "     , A09OUT.INSDPTCD2 INSDPTCD2_OUT";/* 퇴원과(내과세부진료과목) */
                sql += System.Environment.NewLine + "     , V100.REBED_REASON";             /* 10일 이내 재입원사유 */
                sql += System.Environment.NewLine + "     , V100.PTNT_YN";                  /* 환자 안전관리 발생여부 */
                sql += System.Environment.NewLine + "     , V100.PTNT_TXT";                 /* 환자 안전관리 상세내용 */
                sql += System.Environment.NewLine + "  FROM TV100 V100 LEFT JOIN TA07 A07IN ON A07IN.DRID=V100.INDRID";
                sql += System.Environment.NewLine + "                  LEFT JOIN TA09 A09IN ON A09IN.DPTCD=A07IN.DPTCD";
                sql += System.Environment.NewLine + "                  LEFT JOIN TA07 A07OUT ON A07OUT.DRID=V100.OUTDRID";
                sql += System.Environment.NewLine + "                  LEFT JOIN TA09 A09OUT ON A09OUT.DPTCD=A07OUT.DPTCD";
                sql += System.Environment.NewLine + "                  INNER JOIN TA01 A01 ON A01.PID=V100.PID";
                sql += System.Environment.NewLine + " WHERE V100.PID =	'" + i2a.PID + "'";
                sql += System.Environment.NewLine + "   AND V100.BEDEDT = '" + i2a.BDEDT + "'";
                string strConn = DBHelper.GetConnectionString();

                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    this.SetInsdptcd(conn);

                    // TSQL문장과 Connection 객체를 지정   
                    OleDbCommand cmd = new OleDbCommand(sql, conn);

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        BEDEDT = reader["BEDEDT"].ToString();
                        BEDEHM = reader["BEDEHM"].ToString();
                        DRID_IN = reader["DRID_IN"].ToString();
                        DPTCD_IN = reader["DPTCD_IN"].ToString();
                        PDPTCD_IN = reader["PDPTCD_IN"].ToString();
                        INSDPTCD_IN = reader["INSDPTCD_IN"].ToString();
                        GDRLCID_IN = reader["GDRLCID_IN"].ToString();
                        DRNM_IN = reader["DRNM_IN"].ToString();
                        BEDODT = reader["BEDODT"].ToString();
                        BEDOHM = reader["BEDOHM"].ToString();
                        DRID_OUT = reader["DRID_OUT"].ToString();
                        DPTCD_OUT = reader["DPTCD_OUT"].ToString();
                        PDPTCD_OUT = reader["PDPTCD_OUT"].ToString();
                        INSDPTCD_OUT = reader["INSDPTCD_OUT"].ToString();
                        GDRLCID_OUT = reader["GDRLCID_OUT"].ToString();
                        DRNM_OUT = reader["DRNM_OUT"].ToString();
                        EMPID = reader["EMPID"].ToString();
                        EMPNM = GetEmpnm(conn, EMPID);
                        WDATE = reader["WDATE"].ToString();
                        WTIME = reader["WTIME"].ToString();
                        REBED = reader["REBED"].ToString();
                        REBEDPLAN = reader["REBEDPLAN"].ToString();
                        PREOUT = reader["PREOUT"].ToString();
                        COMPLAINT = reader["COMPLAINT"].ToString();
                        HOPI = reader["HOPI"].ToString();
                        COT = reader["COT"].ToString();
                        TRDPT = reader["TRDPT"].ToString();
                        ALLERGY = reader["ALLERGY"].ToString();
                        DRUGNAME = reader["DRUGNAME"].ToString();
                        HEPA = reader["HEPA"].ToString();
                        COMPLICATION = reader["COMPLICATION"].ToString();
                        OUTREASON = reader["OUTREASON"].ToString();
                        OUTSTATUS = reader["OUTSTATUS"].ToString();
                        DEATHDATE = reader["DEATHDATE"].ToString();
                        DEATHTIME = reader["DEATHTIME"].ToString();
                        TRHOS = reader["TRHOS"].ToString();
                        TRHOSREASON = reader["TRHOSREASON"].ToString();
                        CONTICARE = reader["CONTICARE"].ToString();
                        TRHOSNUM = reader["TRHOSNUM"].ToString();
                        TRHOSNAME = reader["TRHOSNAME"].ToString();
                        TRHOSDPT = reader["TRHOSDPT"].ToString();
                        OUTCARE = reader["OUTCARE"].ToString();
                        OTELNO = reader["OTELNO"].ToString();
                        HTELNO = reader["HTELNO"].ToString();
                        NEWDACD = reader["NEWDACD"].ToString();
                        NEWDXD = reader["NEWDXD"].ToString();
                        OUTFOS = reader["OUTFOS"].ToString();
                        SCORE = reader["SCORE"].ToString();
                        ASSPERSON = reader["ASSPERSON"].ToString();

                        // 이하 002 버전에서 추가된 내용
                        VER = reader["VER"].ToString();
                        INSDPTCD2_IN = reader["INSDPTCD2_IN"].ToString();
                        INSDPTCD2_OUT = reader["INSDPTCD2_OUT"].ToString();
                        REBED_REASON = reader["REBED_REASON"].ToString();
                        PTNT_YN = reader["PTNT_YN"].ToString();
                        PTNT_TXT = reader["PTNT_TXT"].ToString();

                        if (INSDPTCD_IN == "01" && INSDPTCD2_IN == "") INSDPTCD2_IN = "00";
                        if (INSDPTCD_IN != "01" && INSDPTCD2_IN != "") INSDPTCD2_IN = "";
                        if (INSDPTCD_OUT == "01" && INSDPTCD2_OUT == "") INSDPTCD2_OUT = "00";
                        if (INSDPTCD_OUT != "01" && INSDPTCD2_OUT != "") INSDPTCD2_OUT = "";

                    }
                    reader.Close();

                    ReadTV100TR(conn, i2a);
                    ReadTV100OP(conn, i2a);
                    ReadTV100DX(conn, i2a);
                    ReadTV100DEDX(conn, i2a);
                    ReadTV100DCOR(conn, i2a);
                    ReadTV100GUM(conn, i2a);

                    // 이하 002 버전에서 추가된 내용
                    ReadTV100CC(conn, i2a);
                    ReadTV100MASR(conn, i2a);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private String GetEmpnm(OleDbConnection p_conn, String p_empid)
        {
            String strRet = "";
            String sql = "";
            if (p_empid.StartsWith("AA") || p_empid.StartsWith("AS"))
            {
                sql += System.Environment.NewLine + "SELECT A07.DRNM EMPNM ";
                sql += System.Environment.NewLine + "  FROM TA07 A07";
                sql += System.Environment.NewLine + " WHERE A07.DRID='" + p_empid + "' ";
            }
            else
            {
                sql += System.Environment.NewLine + "SELECT A13.EMPNM ";
                sql += System.Environment.NewLine + "  FROM TA13 A13";
                sql += System.Environment.NewLine + " WHERE A13.EMPID='" + p_empid + "' ";
            }

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                strRet = reader["EMPNM"].ToString();
            }
            reader.Close();
            return strRet;
        }

        private void ReadTV100TR(OleDbConnection p_conn, CDataTI2A i2a)
        {
            String sql = "";
            sql += System.Environment.NewLine + "SELECT TRDATE";		/*전과일시*/
            sql += System.Environment.NewLine + "     , TROUTDPT";		/*의뢰과*/
            sql += System.Environment.NewLine + "     , TROUTDRNM";		/*의뢰의사 성명*/
            sql += System.Environment.NewLine + "     , TROUTDR";		/*의뢰의사 면허종류*/
            sql += System.Environment.NewLine + "     , TROUTDRLCID";	/*의뢰의사 면허번호*/
            sql += System.Environment.NewLine + "     , TROUTREASON";	/*전과사유*/
            sql += System.Environment.NewLine + "     , TRINDPT";		/*회신과*/
            sql += System.Environment.NewLine + "     , TRINDRNM";		/*회신의사 성명*/
            sql += System.Environment.NewLine + "     , TRINDR";		/*회신의사 면허종류*/
            sql += System.Environment.NewLine + "     , TRINDRLCID";	/*회신의사 면허번호*/
            sql += System.Environment.NewLine + "  FROM TV100_TR";
            sql += System.Environment.NewLine + " WHERE PID =	'" + i2a.PID + "'";
            sql += System.Environment.NewLine + "   AND BEDEDT = '" + i2a.BDEDT + "'";
            sql += System.Environment.NewLine + " ORDER BY PID,BEDEDT,SEQ";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            TRCOUNT = 0;
            while (reader.Read())
            {
                String strTrDate = reader["TRDATE"].ToString();
                if ("".Equals(strTrDate))
                {
                    strTrDate = "";
                }
                else if (strTrDate.Length == 8)
                {
                    strTrDate += "0000";
                }

                String strTrOutDr = reader["TROUTDR"].ToString();
                String strTrOutDrCd = "1";
                if ("의사".Equals(strTrOutDr))
                {
                    strTrOutDrCd = "1";
                }
                else if ("치과의사".Equals(strTrOutDr))
                {
                    strTrOutDrCd = "2";
                }
                else if ("한의사".Equals(strTrOutDr))
                {
                    strTrOutDrCd = "3";
                }

                String strTrInDr = reader["TRINDR"].ToString();
                String strTrInDrCd = "1";
                if ("의사".Equals(strTrInDr))
                {
                    strTrInDrCd = "1";
                }
                else if ("치과의사".Equals(strTrInDr))
                {
                    strTrInDrCd = "2";
                }
                else if ("한의사".Equals(strTrInDr))
                {
                    strTrInDrCd = "3";
                }

                TRCOUNT++;
                TRDATE.Add(strTrDate);
                TROUTDPT.Add(GetInsDptcd(reader["TROUTDPT"].ToString()));
                TROUTDPT2.Add(GetInsDptcd2(reader["TROUTDPT"].ToString())); // *** 002 버전
                TROUTDRNM.Add(reader["TROUTDRNM"].ToString());
                TROUTDR.Add(strTrOutDrCd);
                TROUTDRLCID.Add(reader["TROUTDRLCID"].ToString());
                TROUTREASON.Add(reader["TROUTREASON"].ToString());
                TRINDPT.Add(GetInsDptcd(reader["TRINDPT"].ToString()));
                TRINDPT2.Add(GetInsDptcd2(reader["TRINDPT"].ToString())); // *** 002 버전
                TRINDRNM.Add(reader["TRINDRNM"].ToString());
                TRINDR.Add(strTrInDrCd);
                TRINDRLCID.Add(reader["TRINDRLCID"].ToString());
            }
            reader.Close();
        }

        private void ReadTV100OP(OleDbConnection p_conn, CDataTI2A i2a)
        {
            String sql = "";
            sql += System.Environment.NewLine + "SELECT OPDT";		/*수술일시*/
            sql += System.Environment.NewLine + "     , OPNAME";	/*처치 및 수술명*/
            sql += System.Environment.NewLine + "     , ICD9CM";	/*ICD9CM VOL.3*/
            sql += System.Environment.NewLine + "     , PRICD";	    /*수가코드*/
            sql += System.Environment.NewLine + "  FROM TV100_OP";
            sql += System.Environment.NewLine + " WHERE PID =	'" + i2a.PID + "'";
            sql += System.Environment.NewLine + "   AND BEDEDT = '" + i2a.BDEDT + "'";
            sql += System.Environment.NewLine + " ORDER BY PID,BEDEDT,SEQ";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            OPCOUNT = 0;
            while (reader.Read())
            {
                String strPricd = reader["PRICD"].ToString();
                String strOpdt = reader["OPDT"].ToString();
                if ("".Equals(strOpdt))
                {
                    strOpdt = "";
                }
                else if (strOpdt.Length == 8)
                {
                    strOpdt += "0000";
                }
                String strExdt = strOpdt; // 빈값이 있을까봐...
                if (strExdt.Length > 8) strExdt = strExdt.Substring(0, 8);
                String strIspcd = GetIspcd(p_conn, strPricd, strExdt);

                OPCOUNT++;
                OPDT.Add(strOpdt);
                OPNAME.Add(reader["OPNAME"].ToString());
                ICD9CM.Add(reader["ICD9CM"].ToString());
                PRICD.Add(strIspcd);
            }
            reader.Close();
        }

        private String GetIspcd(OleDbConnection p_conn,String p_pricd, String p_exdt)
        {
            String strIspcd = "";

            String sql = "";
            sql += System.Environment.NewLine + "SELECT A02.ISPCD";
            sql += System.Environment.NewLine + "  FROM TA02 A02";
            sql += System.Environment.NewLine + " WHERE A02.PRICD='" + p_pricd + "'";
            sql += System.Environment.NewLine + "   AND A02.CREDT=(SELECT MAX(X.CREDT)";
            sql += System.Environment.NewLine + "                    FROM TA02 X";
            sql += System.Environment.NewLine + "                   WHERE X.PRICD=A02.PRICD";
            sql += System.Environment.NewLine + "                     AND X.CREDT<='" + p_exdt + "'";
            sql += System.Environment.NewLine + "                 )";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                strIspcd = reader["ISPCD"].ToString();
            }
            reader.Close();

            return strIspcd;
        }

        private void ReadTV100DX(OleDbConnection p_conn, CDataTI2A i2a)
        {
            String sql = "";
            sql += System.Environment.NewLine + "SELECT DX.ROFG";		/*확진여부*/
            sql += System.Environment.NewLine + "     , DX.DISECD";	    /*상병분류기호*/
            sql += System.Environment.NewLine + "     , DX.DXD";	    /*진단명*/
            sql += System.Environment.NewLine + "     , DX.DPTCD";	    /*진료과*/
            sql += System.Environment.NewLine + "     , DX.POA";	    /*입원시 상병여부*/
            sql += System.Environment.NewLine + "     , A09.INSDPTCD";
            sql += System.Environment.NewLine + "     , A09.INSDPTCD2"; /* 내과 세부진료과목 */
            sql += System.Environment.NewLine + "  FROM TV100_DX DX (NOLOCK) INNER JOIN TA09 A09 (NOLOCK) ON A09.DPTCD=DX.DPTCD";
            sql += System.Environment.NewLine + " WHERE DX.PID =	'" + i2a.PID + "'";
            sql += System.Environment.NewLine + "   AND DX.BEDEDT = '" + i2a.BDEDT + "'";
            sql += System.Environment.NewLine + " ORDER BY PID,BEDEDT,SEQ";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            DX_COUNT = 0;
            while (reader.Read())
            {
                String strPoa = reader["POA"].ToString().ToUpper();
                String strPoaCd = strPoa;
                if ("YES".Equals(strPoa)) strPoaCd = "1";
                if ("NO".Equals(strPoa)) strPoaCd = "2";
                if ("UNKNOWN".Equals(strPoa)) strPoaCd = "3";
                if ("UNDETERMINED".Equals(strPoa)) strPoaCd = "4";
                if ("기타(예외상병)".Equals(strPoa)) strPoaCd = "5";
                if ("".Equals(strPoa)) strPoaCd = "2";
                if ("1".Equals(strPoaCd) == false && "2".Equals(strPoaCd) == false && "3".Equals(strPoaCd) == false && "4".Equals(strPoaCd) == false && "5".Equals(strPoaCd) == false)
                {
                    strPoaCd = "2";
                }
                String strDisecd = reader["DISECD"].ToString().Replace(".","");
                String[] tmp;
                tmp = (strDisecd + "_").Split('_');
                strDisecd = tmp[0];
                if (strDisecd.Length > 6) strDisecd = strDisecd.Substring(0, 6);
                strDisecd = strDisecd.Replace("_", "");

                String strInsDptcd = reader["INSDPTCD"].ToString();
                String strInsDptcd2 = reader["INSDPTCD2"].ToString();
                if (strInsDptcd == "01" && strInsDptcd2 == "") strInsDptcd2 = "00";
                if (strInsDptcd != "01" && strInsDptcd2 != "") strInsDptcd2 = "";

                DX_COUNT++;
                DX_ROFG.Add(reader["ROFG"].ToString());
                DX_DISECD.Add(strDisecd);
                DX_DXD.Add(reader["DXD"].ToString());
                DX_DPTCD.Add(reader["DPTCD"].ToString());
                DX_INSDPTCD.Add(strInsDptcd);
                DX_INSDPTCD2.Add(strInsDptcd2);
                DX_POA.Add(strPoaCd);
            }
            reader.Close();
        }

        private void ReadTV100DEDX(OleDbConnection p_conn, CDataTI2A i2a)
        {
            String sql = "";
            sql += System.Environment.NewLine + "SELECT DEATHDISECD";	/*상병분류기호*/
            sql += System.Environment.NewLine + "     , DEATHDXD";	    /*진단명*/
            sql += System.Environment.NewLine + "  FROM TV100_DEDX";
            sql += System.Environment.NewLine + " WHERE PID =	'" + i2a.PID + "'";
            sql += System.Environment.NewLine + "   AND BEDEDT = '" + i2a.BDEDT + "'";
            sql += System.Environment.NewLine + " ORDER BY PID,BEDEDT,SEQ";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            DEDX_COUNT = 0;
            while (reader.Read())
            {
                DEDX_COUNT++;
                DEDX_DISECD.Add(reader["DEATHDISECD"].ToString());
                DEDX_DXD.Add(reader["DEATHDXD"].ToString());
            }
            reader.Close();
            // 사용하는 쪽에서 오류가 발생하지 않도록 최소 1개는 넣는다.
            if (DEDX_COUNT == 0)
            {
                DEDX_COUNT++;
                DEDX_DISECD.Add("");
                DEDX_DXD.Add("");
            }
        }

        private void ReadTV100DCOR(OleDbConnection p_conn, CDataTI2A i2a)
        {
            String sql = "";
            sql += System.Environment.NewLine + "SELECT ONM";	/*약품명*/
            sql += System.Environment.NewLine + "     , OUNIT"; /*용법*/
            sql += System.Environment.NewLine + "     , OQTY";  /*1회투약량*/
            sql += System.Environment.NewLine + "     , ORDCNT"; /*1일투여횟수*/
            sql += System.Environment.NewLine + "     , ODAYCNT"; /*총 투약일수*/
            sql += System.Environment.NewLine + "     , ORDER_TYPE"; /*처방구분*/
            sql += System.Environment.NewLine + "  FROM TV100_DCOR";
            sql += System.Environment.NewLine + " WHERE PID =	'" + i2a.PID + "'";
            sql += System.Environment.NewLine + "   AND BEDEDT = '" + i2a.BDEDT + "'";
            sql += System.Environment.NewLine + "   AND ISNULL(ONM,'')<>''"; // 처방명이 있는 경우만
            sql += System.Environment.NewLine + " ORDER BY PID,BEDEDT,SEQ";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            DCORCOUNT = 0;
            while (reader.Read())
            {
                String strOrdcnt = reader["ORDCNT"].ToString();
                String[] tmp = (strOrdcnt + ".").Split('.');
                
                DCORCOUNT++;
                ORDER_TYPE.Add(reader["ORDER_TYPE"].ToString()); // *** 002 버전
                ONM.Add(reader["ONM"].ToString());
                OUNIT.Add(reader["OUNIT"].ToString());
                OQTY.Add(reader["OQTY"].ToString());
                ORDCNT.Add(tmp[0]);
                ODAYCNT.Add(reader["ODAYCNT"].ToString());
            }
            reader.Close();
        }

        private void ReadTV100GUM(OleDbConnection p_conn, CDataTI2A i2a)
        {
            String sql = "";
            sql += System.Environment.NewLine + "SELECT ODT";	/*처방일*/
            sql += System.Environment.NewLine + "     , GUMDT"; /*검사일시*/
            sql += System.Environment.NewLine + "     , RSDT";  /*결과일시*/
            sql += System.Environment.NewLine + "     , ONM";   /*검사명*/
            sql += System.Environment.NewLine + "     , GUMRESULT"; /*검사결과*/
            sql += System.Environment.NewLine + "  FROM TV100_GUM";
            sql += System.Environment.NewLine + " WHERE PID =	'" + i2a.PID + "'";
            sql += System.Environment.NewLine + "   AND BEDEDT = '" + i2a.BDEDT + "'";
            sql += System.Environment.NewLine + " ORDER BY PID,BEDEDT,SEQ";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            GUM_COUNT = 0;
            while (reader.Read())
            {
                String strGumdt = reader["GUMDT"].ToString();
                if ("".Equals(strGumdt)) strGumdt = reader["RSDT"].ToString(); // 검사일이 없으면 결과일자로
                if (strGumdt.Length == 8) strGumdt += "0000"; // 시분이 없는 경우 0000

                GUM_COUNT++;
                GUM_ODT.Add(reader["ODT"].ToString());
                GUM_GUMDT.Add(strGumdt);
                GUM_RSDT.Add(reader["RSDT"].ToString());
                GUM_ONM.Add(reader["ONM"].ToString());
                GUM_GUMRESULT.Add(reader["GUMRESULT"].ToString());
            }
            reader.Close();
        }

        private void ReadTV100CC(OleDbConnection p_conn, CDataTI2A i2a)
        {
            String sql = "";
            sql += System.Environment.NewLine + "SELECT COMPLAINT";	/*주호소*/
            sql += System.Environment.NewLine + "     , ERA"; /*발병시기*/
            sql += System.Environment.NewLine + "  FROM TV100_CC";
            sql += System.Environment.NewLine + " WHERE PID =	'" + i2a.PID + "'";
            sql += System.Environment.NewLine + "   AND BEDEDT = '" + i2a.BDEDT + "'";
            sql += System.Environment.NewLine + " ORDER BY PID,BEDEDT,SEQ";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            CC_COUNT = 0;
            while (reader.Read())
            {
                CC_COUNT++;
                CC_COMPLAINT.Add(reader["COMPLAINT"].ToString());
                CC_ERA.Add(reader["ERA"].ToString());
            }
            reader.Close();
        }

        private void ReadTV100MASR(OleDbConnection p_conn, CDataTI2A i2a)
        {
            String sql = "";
            sql += System.Environment.NewLine + "SELECT ERA_CD";/*측정시기*/
            sql += System.Environment.NewLine + "     , ERA_ETC_TXT"; /*측정시기 기타 상세*/
            sql += System.Environment.NewLine + "     , TL_NM"; /*도구*/
            sql += System.Environment.NewLine + "     , RST_TXT"; /*결과*/
            sql += System.Environment.NewLine + "     , RMK_TXT"; /*참고사항*/
            sql += System.Environment.NewLine + "     , ERA_ETC_DT"; /*측정시기 기타 상세(yyyymmdd)*/
            sql += System.Environment.NewLine + "     , ERA_ETC_TM"; /*측정시기 기타 상세(hhmm)*/
            sql += System.Environment.NewLine + "  FROM TV100_MASR";
            sql += System.Environment.NewLine + " WHERE PID =	'" + i2a.PID + "'";
            sql += System.Environment.NewLine + "   AND BEDEDT = '" + i2a.BDEDT + "'";
            sql += System.Environment.NewLine + " ORDER BY PID,BEDEDT,SEQ";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            MASR_COUNT = 0;
            while (reader.Read())
            {
                MASR_COUNT++;
                MASR_ERA_CD.Add(reader["ERA_CD"].ToString());
                MASR_ERA_ETC_TXT.Add(reader["ERA_ETC_TXT"].ToString());
                MASR_TL_NM.Add(reader["TL_NM"].ToString());
                MASR_RST_TXT.Add(reader["RST_TXT"].ToString());
                MASR_RMK_TXT.Add(reader["RMK_TXT"].ToString());
                MASR_ERA_ETC_DT.Add(reader["ERA_ETC_DT"].ToString() + reader["ERA_ETC_TM"].ToString());
            }
            reader.Close();
        }

        private void SetInsdptcd(OleDbConnection p_conn)
        {

            String sql = "";
            sql = "SELECT * FROM TA09 WHERE DPTDIV IN ('1','9')";
            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            GUM_COUNT = 0;
            while (reader.Read())
            {
                String strInsDptcd = reader["INSDPTCD"].ToString();
                String strInsDptcd2 = reader["INSDPTCD2"].ToString();
                if (strInsDptcd == "01" && strInsDptcd2 == "") strInsDptcd2 = "00"; // 내과인데 세부진료과목코드가 없으면 00
                if (strInsDptcd != "01" && strInsDptcd2 != "") strInsDptcd2 = "";   // 내과가 아닌에 세부진료과목코드가 있으면 지움
                m_Dptcd.Add(reader["DPTCD"].ToString(), strInsDptcd);
                m_Dptcd2.Add(reader["DPTCD"].ToString(), strInsDptcd2);
            }
            reader.Close();
        }

        private String GetInsDptcd(String dptcd)
        {
            String strRet = "";
            if (m_Dptcd.ContainsKey(dptcd))
            {
                strRet = m_Dptcd[dptcd];
            }
            return strRet;
        }

        private String GetInsDptcd2(String dptcd)
        {
            String strRet = "";
            if (m_Dptcd2.ContainsKey(dptcd))
            {
                strRet = m_Dptcd2[dptcd];
            }
            return strRet;
        }
    }
}
