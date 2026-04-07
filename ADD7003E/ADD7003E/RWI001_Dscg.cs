using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RWI001_Dscg
    {
        public string CRDHM;
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;
        public string PDRID;
        public string PDRNM;

        // ------------

        public string CHRG_DR_NM // 담당의사
        {
            get { return PDRNM; }
        }
        public string DGSBJT_CD // 진료과
        {
            get { return INSDPTCD; }
        }
        public string IFLD_DTL_SPC_SBJT_CD // 내과세부
        {
            get { return INSDPTCD=="01" ? INSDPTCD2 : ""; }
        }
        public string WRTP_NM // 작성자 성명
        {
            get { return ""; }
        }
        public string SPRM_IPAT_DT // 입실일시
        {
            get { return CRDHM; }
        }
        public string SPRM_IPAT_PTH_CD // 입실경로
        {
            get { return ""; }
        }
        public string IPAT_PTH_ETC_TXT // 입실경로 상세
        {
            get { return ""; }
        }
        public string SPRM_IPAT_RS_CD // 입실사유
        {
            get { return ""; }
        }
        public string RE_IPAT_TS_TXT // 재입실사유
        {
            get { return ""; }
        }
        public string IPAT_RS_ETC_TXT // 입실사유 기타상세
        {
            get { return ""; }
        }
        public string SPRM_DSCG_RST_CD // 퇴실상태
        {
            get { return ""; }
        }
        public string DSCG_RST_TXT // 퇴실현황기타상세
        {
            get { return ""; }
        }
        public string DEATH_DT // 사망일시
        {
            get { return ""; }
        }
        public string DEATH_SICK_SYM // 원사인 상병분류 기호
        {
            get { return ""; }
        }
        public string DEATH_DIAG_NM // 사망 진단명
        {
            get { return ""; }
        }
        public string SPRM_DSCG_DT // 퇴실일시
        {
            get { return ""; }
        }

    }
}
