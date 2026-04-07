using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRAA001
    {
        // TU03
        public string ANSDT; // 마취시작일
        public string ANSHR; // 마취시작시
        public string ANSMN; // 마취시작분
        public string ANEDT; // 마취종료일
        public string ANEHR; // 마취종료시
        public string ANEMN; // 마취종료분
        public string ANEDR; // 마취통증의학과 전문의
        public string ANEDRNM;
        public string USRID; // 작성자
        public string USRNM;
        public string ENTDT; // 작성일자
        public string ENTMS; // 작성시분초

        // TU01
        public string OPSDT; // 수술시작일
        public string OPSHR; // 수술시작시
        public string OPSMN; // 수술시작분
        public string OPEDT; // 수술종료일
        public string OPEHR; // 수술종료시
        public string OPEMN; // 수술종료분

        // TU02
        public List<string> OPNM = new List<string>(); // 수술명
        public List<string> ISPCD = new List<string>(); // 수가코드(EDI코드)

        // TU05
        public List<string> DXD = new List<string>(); // 진단명
        public List<string> DACD = new List<string>(); // 상병분류기호

        // EMR082_2
        public string NCT_FRM_CD; // 마취형태 1.정규 2.응급
        public string ASA;
        public string ASA6;
        public string ASA7;
        public string ASA_PNT // ASA점수
        {
            get
            {
                string ret = "";
                string asa = ASA + "      "; // 오류 방지용
                if (asa.Substring(0, 1) == "1") ret = "1";
                else if (asa.Substring(1, 1) == "1") ret = "2";
                else if (asa.Substring(2, 1) == "1") ret = "3";
                else if (asa.Substring(3, 1) == "1") ret = "4";
                else if (asa.Substring(4, 1) == "1") ret = "5";
                else if (ASA6 == "1") ret = "6";
                else if (ASA7 == "1") ret = "7";
                else if (asa.Substring(5, 1) == "1") ret = "8";
                return ret;
            }
        }

        // EMR320
        public string MTH_CD; // 마취방법
        public string NCT_MTH_CD
        {
            get
            {
                string ret = "";
                string[] mth_cd = MTH_CD.Split((char)21);
                for (int i = 0; i < mth_cd.Length; i++)
                {
                    if (mth_cd[i] == "1")
                    {
                        if (i == 0) ret = (ret == "" ? "01" : "/01");
                        if (i == 1) ret = (ret == "" ? "02" : "/02");
                        if (i == 2) ret = (ret == "" ? "03" : "/03");
                        if (i == 3) ret = (ret == "" ? "04" : "/04");
                        if (i == 4) ret = (ret == "" ? "05" : "/05");
                        if (i == 5) ret = (ret == "" ? "06" : "/06");
                        if (i == 6) ret = (ret == "" ? "07" : "/07");
                        if (i == 7) ret = (ret == "" ? "08" : "/08");
                        if (i == 8) ret = (ret == "" ? "09" : "/09");
                        if (i == 9) ret = (ret == "" ? "99" : "/99");
                    }
                }
                return ret;
                /*
                string mth_cd = MTH_CD + "          ";
                if (mth_cd.Substring(0, 1) == "1") ret = "01";
                if (mth_cd.Substring(1, 1) == "1") ret = (ret == "" ? "02" : "/02");
                if (mth_cd.Substring(2, 1) == "1") ret = (ret == "" ? "03" : "/03");
                if (mth_cd.Substring(3, 1) == "1") ret = (ret == "" ? "04" : "/04");
                if (mth_cd.Substring(4, 1) == "1") ret = (ret == "" ? "05" : "/05");
                if (mth_cd.Substring(5, 1) == "1") ret = (ret == "" ? "06" : "/06");
                if (mth_cd.Substring(6, 1) == "1") ret = (ret == "" ? "07" : "/07");
                if (mth_cd.Substring(7, 1) == "1") ret = (ret == "" ? "08" : "/08");
                if (mth_cd.Substring(8, 1) == "1") ret = (ret == "" ? "09" : "/09");
                if (mth_cd.Substring(9, 1) == "1") ret = (ret == "" ? "99" : "/99");
                return ret;
                */
            }
        }
        public string MTH_ETC; // 마취방법상세(마위방법이 99인경우)
        public string NCT_MTH_ETC_TXT
        {
            get
            {
                string ret = "";
                string[] mth_cd = MTH_CD.Split((char)21);
                if (mth_cd.Length >= 10 && mth_cd[9] == "1")
                {
                    ret = MTH_ETC;
                }
                return ret;
                //return ((MTH_CD + "          ").Substring(9, 1) == "1") ? MTH_ETC : "";
            }
        }
        public string MIDD_MNTR_YN; // 마취 중 감시여부
        public string NCT_MIDD_MNTR_YN
        {
            get
            {
                string ret="";
                string[] mntr_yn = (MIDD_MNTR_YN + (char)21).Split((char)21);
                if (mntr_yn[0] == "1") ret = "1";
                else if (mntr_yn[1] == "1") ret = "2";
                return ret;

            }
        }
        public string MNTR_KND_CD; // 마취 중 감시 종류
        public string NCT_MNTR_KND_CD
        {
            get
            {
                string ret = "";
                string[] mntr_knd_cd = MNTR_KND_CD.Split((char)21);
                for (int i = 0; i < mntr_knd_cd.Length; i++)
                {
                    if (mntr_knd_cd[i] == "1")
                    {
                        if (i == 0) ret += (ret == "" ? "01" : "/01");
                        if (i == 1) ret += (ret == "" ? "02" : "/02");
                        if (i == 2) ret += (ret == "" ? "03" : "/03");
                        if (i == 3) ret += (ret == "" ? "04" : "/04");
                        if (i == 4) ret += (ret == "" ? "05" : "/05");
                        if (i == 5) ret += (ret == "" ? "06" : "/06");
                        if (i == 6) ret += (ret == "" ? "07" : "/07");
                        if (i == 7) ret += (ret == "" ? "08" : "/08");
                        if (i == 8) ret += (ret == "" ? "09" : "/09");
                        if (i == 9) ret += (ret == "" ? "99" : "/99");
                    }
                }
                return ret;
                /*
                string mntr_knd_cd = MNTR_KND_CD + "         ";
                if (mntr_knd_cd.Substring(0, 1) == "1") ret = "01";
                if (mntr_knd_cd.Substring(1, 1) == "1") ret = (ret == "" ? "02" : "/02");
                if (mntr_knd_cd.Substring(2, 1) == "1") ret = (ret == "" ? "03" : "/03");
                if (mntr_knd_cd.Substring(3, 1) == "1") ret = (ret == "" ? "04" : "/04");
                if (mntr_knd_cd.Substring(4, 1) == "1") ret = (ret == "" ? "05" : "/05");
                if (mntr_knd_cd.Substring(5, 1) == "1") ret = (ret == "" ? "06" : "/06");
                if (mntr_knd_cd.Substring(6, 1) == "1") ret = (ret == "" ? "07" : "/07");
                if (mntr_knd_cd.Substring(7, 1) == "1") ret = (ret == "" ? "08" : "/08");
                if (mntr_knd_cd.Substring(8, 1) == "1") ret = (ret == "" ? "99" : "/99");
                return ret;
                */
            }
        }
        public string MNTR_ETC; // 마취 중 감시 종류 상세
        public string MNTR_ETC_TXT
        {
            get
            {
                string ret = "";
                string[] mntr_knd_cd = MNTR_KND_CD.Split((char)21);
                if (mntr_knd_cd.Length >= 10 && mntr_knd_cd[9] == "1")
                {
                    ret = MNTR_ETC;
                }
                return ret;
                //return ((MNTR_KND_CD + "         ").Substring(8, 1) == "1") ? MNTR_ETC : "";
            }
        }

        // EMR320_VS
        public List<string> VTSG_MASR_DT = new List<string>(); // 활력징후 측정일시
        public List<string> BPRSU = new List<string>(); // 활력징후 혈압
        public List<string> PULS = new List<string>(); // 활력징후 맥박
        public List<string> BRT = new List<string>(); // 활력징후 호흡
        public List<string> TMPR = new List<string>(); // 활력징후 체온

        // EMR320_MNTR
        public List<string> MNTR_MASR_DT = new List<string>(); // 마취 중 감시측정 측정일시
        public List<string> OXY_STRT = new List<string>(); // 마취 중 감시측정 산소포화도
        public List<string> CRBR_OXY_STRT = new List<string>(); // 마취 중 감시측정 대뇌산소포화도
        public List<string> NRRT_CNDC_CD = new List<string>(); // 마취 중 감시측정 TOF
        public List<string> NRRT_CNDC_RT = new List<string>(); // 마취 중 감시측정 ratio 상세
        public List<string> NRRT_CNDC_CNT = new List<string>(); // 마취 중 감시측정 count 상세
        public List<string> BIS_CNT = new List<string>(); // 마취 중 감시측정 BIS
        public List<string> CROT_CNT = new List<string>(); // 마취 중 감시측정 CO
        public List<string> CVP_CNT = new List<string>(); // 마취 중 감시측정 CVP
        public List<string> RMK_TXT = new List<string>(); // 마취 중 감시측정 특이사항

        // EMR320_MDCT
        public List<string> KND_CD = new List<string>(); // 마취 중 투약 분류
        public List<string> MDCT_STDT = new List<string>(); // 마취 중 투약 시작일자
        public List<string> MDCT_STTM = new List<string>(); // 마취 중 투약 시작시간
        public List<string> MDCT_EDDT = new List<string>(); // 마취 중 투약 종료일자
        public List<string> MDCT_EDTM = new List<string>(); // 마취 중 투약 종료시간
        public List<string> MDS_NM = new List<string>(); // 마취 중 투약 약품명
        public List<string> OQTY = new List<string>(); // 마취 중 투약 1회 투약량
        public List<string> UNIT = new List<string>(); // 마취 중 트약 단위
        public string MDCT_DT(int idx)
        {
            return MDCT_STDT[idx] + MDCT_STTM[idx];
        }
        public string CNTN_MDCT_END_DT(int idx)
        {
            return MDCT_EDDT[idx] + MDCT_EDTM[idx];
        }

        // EMR320_IN
        public long IN_TOT_QTY; // 섭취량 총량
        public long IN_IFSL_QTY; // 섭취량 수액
        public List<string> BLTS_KND = new List<string>(); // 수혈 종류
        public List<long> BLTS_QTY = new List<long>(); // 수혈량

        // EMR320_OUT
        public long OUT_TOT_QTY; // 배셜량 총량
        public long OUT_URNN_QTY; // 배설량 배뇨
        public long OUT_BLD_QTY; // 배설량 출혈
        public long OUT_ETC_QTY; // 배설량 기타

        // EMR320_RCD
        public List<string> OCUR_DT = new List<string>(); // 마취관련 기록 발생일자
        public List<string> OCUR_TM = new List<string>(); // 마취관련 기록 발생시간
        public List<string> RCD_TXT = new List<string>(); // 마취관련 기록 내용
        public string OCUR_DTM(int idx)
        {
            return OCUR_DT[idx] + OCUR_TM[idx];
        }


        public string ANSDTM { get { return ANSDT + ANSHR + ANSMN; } }
        public string ANEDTM { get { return ANEDT + ANEHR + ANEMN; } }
        public string ENTDTM { get { return ENTDT + CUtil.GetSubstring(ENTMS, 0, 4); } }

        public string OPSDTM { get { return OPSDT + OPSHR + OPSMN; } }
        public string OPEDTM { get { return OPEDT + OPEHR + OPEMN; } }

        public string OP_INFO
        {
            get
            {
                string ret = "";
                foreach (string opnm in OPNM)
                {
                    ret += (ret!="" ? Environment.NewLine : "") + opnm;
                }
                return ret;
            }
        }
        public string DACD_INFO
        {
            get
            {
                string ret = "";
                foreach (string dxd in DXD)
                {
                    ret += (ret != "" ? Environment.NewLine : "") + dxd;
                }
                return ret;
            }
        }

        public void Clear()
        {
            // TU03
            ANSDT = ""; // 마취시작일
            ANSHR = ""; // 마취시작시
            ANSMN = ""; // 마취시작분
            ANEDT = ""; // 마취종료일
            ANEHR = ""; // 마취종료시
            ANEMN = ""; // 마취종료분
            ANEDR = ""; // 마취통증의학과 전문의
            ANEDRNM = "";
            USRID = ""; // 작성자
            USRNM = "";
            ENTDT = ""; // 작성일자
            ENTMS = ""; // 작성시분초

            // TU01
            OPSDT = ""; // 수술시작일
            OPSHR = ""; // 수술시작시
            OPSMN = ""; // 수술시작분
            OPEDT = ""; // 수술종료일
            OPEHR = ""; // 수술종료시
            OPEMN = ""; // 수술종료분

            // TU02
            OPNM.Clear(); // 수술명
            ISPCD.Clear(); // 수가코드(EDI코드)

            // TU05
            DXD.Clear(); // 진단명
            DACD.Clear(); // 상병분류기호

            // EMR082_2
            NCT_FRM_CD = ""; // 마취형태 1.정규 2.응급
            ASA = ""; // ASA
            ASA6 = ""; // ASA6
            ASA7 = ""; // ASA7

            // EMR320
            MTH_CD = ""; // 마취방법
            MTH_ETC = ""; // 마취방법상세(마위방법이 99인경우)
            MIDD_MNTR_YN = ""; // 마취 중 감시여부
            MNTR_KND_CD = ""; // 마취 중 감시 종류
            MNTR_ETC = ""; // 마취 중 감시 종류상세

            // EMR320_VS
            VTSG_MASR_DT.Clear(); // 활력징후 측정일시
            BPRSU.Clear(); // 활력징후 혈압
            PULS.Clear(); // 활력징후 맥박
            BRT.Clear(); // 활력징후 호흡
            TMPR.Clear(); // 활력징후 체온

            // EMR320_MNTR
            MNTR_MASR_DT.Clear(); // 마취 중 감시측정 측정일시
            OXY_STRT.Clear(); // 마취 중 감시측정 산소포화도
            CRBR_OXY_STRT.Clear(); // 마취 중 감시측정 대뇌산소포화도
            NRRT_CNDC_CD.Clear(); // 마취 중 감시측정 TOF
            NRRT_CNDC_RT.Clear(); // 마취 중 감시측정 ratio 상세
            NRRT_CNDC_CNT.Clear(); // 마취 중 감시측정 count 상세
            BIS_CNT.Clear(); // 마취 중 감시측정 BIS
            CROT_CNT.Clear(); // 마취 중 감시측정 CO
            CVP_CNT.Clear(); // 마취 중 감시측정 CVP
            RMK_TXT.Clear(); // 마취 중 감시측정 특이사항

            // EMR320_MDCT
            KND_CD.Clear(); // 마취 중 투약 분류
            MDCT_STDT.Clear(); // 마취 중 투약 시작일자
            MDCT_STTM.Clear(); // 마취 중 투약 시작시간
            MDCT_EDDT.Clear(); // 마취 중 투약 종료일자
            MDCT_EDTM.Clear(); // 마취 중 투약 종료시간
            MDS_NM.Clear(); // 마취 중 투약 약품명
            OQTY.Clear(); // 마취 중 투약 1회 투약량
            UNIT.Clear(); // 마취 중 트약 단위

            // EMR320_IN
            IN_TOT_QTY = 0; // 섭취량 총량
            IN_IFSL_QTY = 0; // 섭취량 수액
            BLTS_KND.Clear(); // 수혈 종류
            BLTS_QTY.Clear(); // 수혈량

            // EMR320_OUT
            OUT_TOT_QTY = 0; // 배셜량 총량
            OUT_URNN_QTY = 0; // 배설량 배뇨
            OUT_BLD_QTY = 0; // 배설량 출혈
            OUT_ETC_QTY = 0; // 배설량 기타

            // EMR_320_RCD
            OCUR_DT.Clear(); // 마취관련 기록 발생일자
            OCUR_TM.Clear(); // 마취관련 기록 발생시간
            RCD_TXT.Clear(); // 마취관련 기록 내용
        }

    }
}
