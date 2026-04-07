using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRWI001
    {
        // A.기본 정보
        public string FST_IPAT_DT; // 최초 입실일시
        public List<string> DXD = new List<string>(); // 진단명

        // B.중환자실 입.퇴실 정보
        public string CHRG_DR_NM; // 담당의사 성명
        public string DPTCD; // 진료과코드
        public string INSDPTCD; // 진료과목
        public string INSDPTCD2; // 내과 세부전문과목
        public string DEPT_INFO { get { return DPTCD + "(" + INSDPTCD + INSDPTCD2 + ")"; } }
        public string WRTP_NM; // 작성자 성명
        public string SPRM_IPAT_DT; // 입실일시
        public string SPRM_IPAT_PTH_CD; // 입실경로(1.수술실 2.응급실 3.외래 4.분만실 5.타병동에서 전실 9.기타)
        public string IPAT_PTH_ETC_TXT; // 입실경로 기타 상세
        public string SPRM_IPAT_RS_CD; // 입실사유(1.상태 악화되어 집중관찰 2.특수한 치료 또는 관리가 필요한 이유 3.수술이나 시술 후 집중관찰 5.의료진의 치료 계획에 따라 예정되 재입실 9.기타)
        public string RE_IPAT_RS_TXT; // 입실사유 재입실사유
        public string IPAT_RS_ETC_TXT; // 입실사유 기타상세
        public string SPRM_DSCG_RST_CD; // 퇴실상태(01.퇴원 02.전실(병동) 03.전실(ICU) 04.전실(신생아실) 05.전원 06.사망 07.퇴사판정(이식) 08.계속 입원 99.기타)
        public string DSCG_RST_TXT; // 퇴실현환 기타 상세
        public string DEATH_DT; // 사망 일시
        public string DEATH_SICK_SYM; // 원사인 상병분류기호
        public string DEATH_DIAG_NM; // 사망 진단명
        public string SPRM_DSCG_DT; // 퇴실일시

        // C.중환자실 관찰 기록

        // D.기타정보
        public string ATFL_RPRT_ENFC_YN; // 인공호흡기 적용 여부
        public string OXY_CURE_YN; // 산소요법 적용 여부
        public string CNNL_ENFC_YN; // 삽입관 적용 여부
        public string DRN_ENFC_YN; // 배액관 적용 여부
        public string SPCL_TRET_CD; // 특수 처치(00.해당 없음 01.ECMO 02.신대체요법 99.기타)
        public string SPCL_TRET_ETC_TXT; // 특수 처치 기타 상세
        public string MNTR_KND_CD; // 모니터링 종류(00.해당없음 01.ABP Monitor 02.EKG Monitor 03.O2 Saturation Monitor 04.Continuous EEG 99.기타)
        public string SGRD_PNT_YN; // 중증도 점수 여부(1.Yes 2.No)
        public string SGRD_RVSN_TL_CD; // 중증도 종류(1.APACH II 2.PAPCHEIII 3.SAPS 2 3.SAPS 3 9.기타)
        public string SGRD_RVSN_TL_TXT; // 증증도 보정도구 종류상세

        public void Clear()
        {
            // A.기본 정보
            FST_IPAT_DT = ""; // 최초 입실일시
            DXD.Clear(); // 진단명

            // B.중환자실 입.퇴실 정보
            CHRG_DR_NM = ""; // 담당의사 성명
            DPTCD = ""; // 진료과코드
            INSDPTCD = ""; // 진료과목
            INSDPTCD2 = ""; // 내과 세부전문과목
            WRTP_NM = ""; // 작성자 성명
            SPRM_IPAT_DT = ""; // 입실일시
            SPRM_IPAT_PTH_CD = ""; // 입실경로(1.수술실 2.응급실 3.외래 4.분만실 5.타병동에서 전실 9.기타)
            IPAT_PTH_ETC_TXT = ""; // 입실경로 기타 상세
            SPRM_IPAT_RS_CD = ""; // 입실사유(1.상태 악화되어 집중관찰 2.특수한 치료 또는 관리가 필요한 이유 3.수술이나 시술 후 집중관찰 5.의료진의 치료 계획에 따라 예정되 재입실 9.기타)
            RE_IPAT_RS_TXT = ""; // 입실사유 재입실사유
            IPAT_RS_ETC_TXT = ""; // 입실사유 기타상세
            SPRM_DSCG_RST_CD = ""; // 퇴실상태(01.퇴원 02.전실(병동) 03.전실(ICU) 04.전실(신생아실) 05.전원 06.사망 07.퇴사판정(이식) 08.계속 입원 99.기타)
            DSCG_RST_TXT = ""; // 퇴실현환 기타 상세
            DEATH_DT = ""; // 사망 일시
            DEATH_SICK_SYM = ""; // 원사인 상병분류기호
            DEATH_DIAG_NM = ""; // 사망 진단명
            SPRM_DSCG_DT = ""; // 퇴실일시

            // C.중환자실 관찰 기록

            // D.기타정보
            ATFL_RPRT_ENFC_YN = ""; // 인공호흡기 적용 여부
            OXY_CURE_YN = ""; // 산소요법 적용 여부
            CNNL_ENFC_YN = ""; // 삽입관 적용 여부
            DRN_ENFC_YN = ""; // 배액관 적용 여부
            SPCL_TRET_CD = ""; // 특수 처치(00.해당 없음 01.ECMO 02.신대체요법 99.기타)
            SPCL_TRET_ETC_TXT = ""; // 특수 처치 기타 상세
            MNTR_KND_CD = ""; // 모니터링 종류(00.해당없음 01.ABP Monitor 02.EKG Monitor 03.O2 Saturation Monitor 04.Continuous EEG 99.기타)
            SGRD_PNT_YN = ""; // 중증도 점수 여부(1.Yes 2.No)
            SGRD_RVSN_TL_CD = ""; // 중증도 종류(1.APACH II 2.PAPCHEIII 3.SAPS 2 3.SAPS 3 9.기타)
            SGRD_RVSN_TL_TXT = ""; // 증증도 보정도구 종류상세
        }
    }
}
