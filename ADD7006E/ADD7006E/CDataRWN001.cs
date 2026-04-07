using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CDataRWN001
    {
        // EMR330
        public string BIRTH_YN; // 출생일 확인 여부(1.Yes 2.No)
        public string BIRTH_DT; // 출생일시(ccyymmddhhmm)
        public string BIRTH_PLC_CD; // 출생장소(1.본원 2.타기관 9.기타)
        public string BIRTH_PLC_ETC_TXT; //출생장소 기타상세(출생장소가 9 기타일 경우)
        public string PARTU_FRM_CD; // 분만형태(1.자연분문 2.제왕절개 9.기타)
        public string PARTU_FRM_ETC_TXT; // 분만형태 기타상세(분만형태가 9 기타일 경우)
        public string FTUS_DEV_TRM; // 재태기간(  주  일)형태로
        public string MEMB_YN; // 다태아여부(1.Yes 2.No)
        public string MEMB_TXT; // 다태아내용(다태아여부 1 Yes 일경우)(예시 세 쌍둥이 중 첫째 아기인 경우 3/1)
        public string APSC_YN; // Apgar Score(1.Yes 2.No)
        public string APSC_PNT; // Apgar Score 내용 (Apgar Score 1 Yes 일경우)(예시 1분 2점, 5분 8점인 경우:2/8)
        public string NBY_BIRTH_BWGT; // 출생시체중
        public string FST_IPAT_DT; // 최초입실일시(ccyymmddhhmm)
        public string CHRG_DR_NM; // 담당의 성명
        public string WRTP_NM; // 작성자성명
        public string SPRM_IPAT_DT; // 입실일시(ccyymmddhhmm)
        public string SPRM_IPAT_PTH_CD; // 입실경로(1.수술실 2.응급실 3.외래 4.분만실 5.타병동에서 전실, 9.기타)
        public string IPAT_PTH_ETC_TXT; // 입실경로 기타상세(입실경로 9 기타일경우 입실경로 평문기재)
        public string NBY_IPAT_RS_CD; // 입실사유(1.미숙아 집중관찰 2.저체중 출생아 집중관찰 3.재태기간이나 출생체중과 관계없이 환아의 상태가 위중한 경우 4.특별한 처치 또는 관리가 필요한 경우 5.의료진의 치료 계획에 따라 예정된 재입실 9.기타)
        public string RE_IPAT_RS_TXT; // 입실사유 재입실상세(입실사유 5 일경우 평문)
        public string IPAT_RS_ETC_TXT; // 입실사유 기타상세(입실사유 9 기타일경우 평문)
        public string IPAT_NBY_BWGT; // 입실시 체중
        public string SPRM_DSCG_RST_CD; // 퇴실상태(01.퇴원 02.전실(전동) 03.전실(ICU) 04.전실(신생아실) 05.전원 06.사망 07.뇌사판정(이식) 08.계속 입원 99.기타)
        public string DSCG_RST_ETC_TXT; //퇴실상태 기타상세(퇴실상태 99 기타일경우 평문)
        public string DEATH_DT; // 사망일시(ccyymmddhhmm)(퇴실상태가 06.사망인 경우)
        public string DEATH_SICK_SYM; // 원사인 상병분류기호(퇴실상태가 06.사망인 경우)
        public string DEATH_DIAG_NM; // 사망진단명(퇴실상태가 06.사망인 경우)
        public string SPRM_DSCG_DT; //퇴실일시(ccyymmddhhmm)
        public string ATFL_RPRT_YN; // 인공호흡기 적용 여부(1.Yes 2.No)
        public string OXY_CURE_YN; // 산소요법 적용 여부(1.Yes 2.No)
        public string CNNL_YN; // 삽입관 및 배액관 적용여부(1.Yes 2.No)

        public string OSTM_YN; // 장루유무(1.Yes 2.No 9.기타)
        public string OSTM_ETC_TXT; // 장루유무-기타상세(장루유무가 9기타인경우 평문)
        public string ETC_DSPL_CD; // 기타처치 시행여부(00.해당없음 01.광선요법 02.저체온요법 03.하기도 증기흡입요법 04.교환수혈 05.심폐소생술 99.기타)

        // EMR330_CNNL
        public List<string> CNNL_KND_CD = new List<string>(); // 삽입관 및 배액관 종류(01.Um....)
        public List<string> CNNL_KND_ETC_TXT = new List<string>(); // 삽입관 및 배액관유형-기타상세(99기타인경우 평문)
        public List<string> CNNL_INS_DT = new List<string>(); // 삽입일시(ccyymmddhhmm)
        public List<string> CNNL_DEL_DT = new List<string>(); // 제거일시(ccyymmddhhmm)

        public void Clear()
        {
            // EMR330
            BIRTH_YN = ""; // 출생일 확인 여부(1.Yes 2.No)
            BIRTH_DT = ""; // 출생일시(ccyymmddhhmm)
            BIRTH_PLC_CD = ""; // 출생장소(1.본원 2.타기관 9.기타)
            BIRTH_PLC_ETC_TXT = ""; //출생장소 기타상세(출생장소가 9 기타일 경우)
            PARTU_FRM_CD = ""; // 분만형태(1.자연분문 2.제왕절개 9.기타)
            PARTU_FRM_ETC_TXT = ""; // 분만형태 기타상세(분만형태가 9 기타일 경우)
            FTUS_DEV_TRM = ""; // 재태기간(  주  일)형태로
            MEMB_YN = ""; // 다태아여부(1.Yes 2.No)
            MEMB_TXT = ""; // 다태아내용(다태아여부 1 Yes 일경우)(예시 세 쌍둥이 중 첫째 아기인 경우 3/1)
            APSC_YN = ""; // Apgar Score(1.Yes 2.No)
            APSC_PNT = ""; // Apgar Score 내용 (Apgar Score 1 Yes 일경우)(예시 1분 2점, 5분 8점인 경우:2/8)
            NBY_BIRTH_BWGT = ""; // 출생시체중
            FST_IPAT_DT = ""; // 최초입실일시(ccyymmddhhmm)
            CHRG_DR_NM = ""; // 담당의 성명
            WRTP_NM = ""; // 작성자성명
            SPRM_IPAT_DT = ""; // 입실일시(ccyymmddhhmm)
            SPRM_IPAT_PTH_CD = ""; // 입실경로(1.수술실 2.응급실 3.외래 4.분만실 5.타병동에서 전실, 9.기타)
            IPAT_PTH_ETC_TXT = ""; // 입실경로 기타상세(입실경로 9 기타일경우 입실경로 평문기재)
            NBY_IPAT_RS_CD = ""; // 입실사유(1.미숙아 집중관찰 2.저체중 출생아 집중관찰 3.재태기간이나 출생체중과 관계없이 환아의 상태가 위중한 경우 4.특별한 처치 또는 관리가 필요한 경우 5.의료진의 치료 계획에 따라 예정된 재입실 9.기타)
            RE_IPAT_RS_TXT = ""; // 입실사유 재입실상세(입실사유 5 일경우 평문)
            IPAT_RS_ETC_TXT = ""; // 입실사유 기타상세(입실사유 9 기타일경우 평문)
            IPAT_NBY_BWGT = ""; // 입실시 체중
            SPRM_DSCG_RST_CD = ""; // 퇴실상태(01.퇴원 02.전실(전동) 03.전실(ICU) 04.전실(신생아실) 05.전원 06.사망 07.뇌사판정(이식) 08.계속 입원 99.기타)
            DSCG_RST_ETC_TXT = ""; //퇴실상태 기타상세(퇴실상태 99 기타일경우 평문)
            DEATH_DT = ""; // 사망일시(ccyymmddhhmm)(퇴실상태가 06.사망인 경우)
            DEATH_SICK_SYM = ""; // 원사인 상병분류기호(퇴실상태가 06.사망인 경우)
            DEATH_DIAG_NM = ""; // 사망진단명(퇴실상태가 06.사망인 경우)
            SPRM_DSCG_DT = ""; //퇴실일시(ccyymmddhhmm)
            ATFL_RPRT_YN = ""; // 인공호흡기 적용 여부(1.Yes 2.No)
            OXY_CURE_YN = ""; // 산소요법 적용 여부(1.Yes 2.No)
            CNNL_YN = ""; // 삽입관 및 배액관 적용여부(1.Yes 2.No)

            OSTM_YN = ""; // 장루유무(1.Yes 2.No 9.기타)
            OSTM_ETC_TXT = ""; // 장루유무-기타상세(장루유무가 9기타인경우 평문)
            ETC_DSPL_CD = ""; // 기타처치 시행여부(00.해당없음 01.광선요법 02.저체온요법 03.하기도 증기흡입요법 04.교환수혈 05.심폐소생술 99.기타)

            // EMR330_CNNL
            CNNL_KND_CD.Clear(); // 삽입관 및 배액관 종류(01.Umbilical venous catheter 02.Umbilical arterial catheter 03.Peripherally inserted central catheter 04.Arterial catheter 05.Central venous catheter 06.Tracheostomy 07.Endotracheal tube 99.기타)
            CNNL_KND_ETC_TXT.Clear(); // 삽입관 및 배액관유형-기타상세(99기타인경우 평문)
            CNNL_INS_DT.Clear(); // 삽입일시(ccyymmddhhmm)
            CNNL_DEL_DT.Clear(); // 제거일시(ccyymmddhhmm)
        }
    }
}
