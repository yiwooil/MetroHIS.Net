using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RIY001_Info
    {
        public int R_CNT;
        // TA04
        public string BEDEDT;
        public string BEDEHM;
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;
        // TK71GR
        public string IN_DATE; // 전과일
        public string IN_DRID; // 전입의
        public string IN_DRNM;
        public string IN_DPTCD;
        public string IN_INSDPTCD;
        public string IN_INSDPTCD2;
        public string DRNM;
        public string SYSDT;
        public string SYSTM;
        public string NOWT;
        public string APLAN;
        public string DXD;
        // TK71HM
        public string OT_DRID;
        public string OT_DRNM;
        public string OT_DPTCD;
        public string OT_INSDPTCD; // 전출과
        public string OT_INSDPTCD2; // 전출과

        // ---

        public string IPAT_DT // 입원일시
        {
            get { return BEDEDT + BEDEHM; }
        }
        public string IPAT_DGSBJT_CD // 입원과
        {
            get { return INSDPTCD; }
        }
        public string IPAT_IFLD_DTL_SPC_SBJT_CD // 입원과 내과세부
        {
            get { return INSDPTCD == "01" ? INSDPTCD2 : ""; }
        }
        public string TRFR_DD // 전과일
        {
            get { return IN_DATE; }
        }
        public string MVIN_DGSBJT_CD // 전입과
        {
            get { return IN_INSDPTCD; }
        }
        public string MVIN_IFLD_DTL_SPC_SBJT_CD // 전입과 내과세부
        {
            get { return IN_INSDPTCD == "01" ? IN_INSDPTCD2 : ""; }
        }
        public string MVOT_DGSBJT_CD // 전출과
        {
            get { return OT_INSDPTCD; }
        }
        public string MVOT_IFLD_DTL_SPC_SBJT_CD // 전출과 내과세부
        {
            get { return OT_INSDPTCD == "01" ? OT_INSDPTCD2 : OT_INSDPTCD; }
        }
        public string CHRG_DR_NM // 담당의사
        {
            get { return IN_DRNM; }
        }
        public string WRTP_NM // 작성자성명
        {
            get { return DRNM; }
        }
        public string WRT_DT // 작성일시
        {
            get { return SYSDT + SYSTM; }
        }
        public string CUR_HOC_TXT // 현병력
        {
            get { return NOWT; }
        }
        public string PHBD_MEDEXM_TXT // 신체검진
        {
            get { return ""; }
        }
        public string PRBM_LIST_TXT // 문제목록
        {
            get { return ""; }
        }
        public string DIAG_NM // 진단명
        {
            get { return DXD; }
        }
        public string TRET_PLAN_TXT // 치료계획
        {
            get { return APLAN; }
        }

        // ---
        public void Clear()
        {
            R_CNT = 0;
            // TA04
            BEDEDT = "";
            BEDEHM = "";
            DPTCD = "";
            INSDPTCD = "";
            INSDPTCD2 = "";
            // TK71GR
            IN_DATE = ""; // 전과일
            IN_DRID = ""; // 전입의
            IN_DRNM = "";
            IN_DPTCD = "";
            IN_INSDPTCD = "";
            IN_INSDPTCD2 = "";
            DRNM = "";
            SYSDT = "";
            SYSTM = "";
            NOWT = "";
            APLAN = "";
            DXD = "";
            // TK71HM
            OT_DRID = "";
            OT_DRNM = "";
            OT_DPTCD = "";
            OT_INSDPTCD = ""; // 전출과
            OT_INSDPTCD2 = ""; // 전출과
        }
    }
}
