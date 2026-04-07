using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RNH001_Info
    {
        public int R_CNT;

        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;
        public string PDRID;
        public string PDRNM;

        // ----------------

        public string DGSBJT_CD // 진료과목
        {
            get { return INSDPTCD; }
        }
        public string IFLD_DTL_SPC_SBJT_CD // 내과세부
        {
            get { return INSDPTCD == "01" ? INSDPTCD2 : ""; }
        }
        public string CHRG_DR_NM // 담당의사
        {
            get { return PDRNM; }
        }
        public string BLDVS_STNS_MNTR_YN // 혈관협착 모니터링 실시 여부
        {
            get { return "2"; }
        }
        public string BLDVS_STNS_MNTR_YN_NM
        {
            get { return "No"; }
        }
        public string DLYS_KND_CD // 투석종류
        {
            get { return "1"; }
        }
        public string DLYS_KND_CD_NM
        {
            get { return "혈액투석"; }
        }

        public void Clear()
        {
            R_CNT = 0;
            DPTCD = "";
            INSDPTCD = "";
            INSDPTCD2 = "";
            PDRID = "";
            PDRNM = "";
        }
    }
}
