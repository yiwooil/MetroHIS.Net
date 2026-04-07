using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class RIZ001_Info
    {
        public int R_CNT;
        // TA04
        public string BEDEDT;
        public string BEDEHM;
        public string DPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;
        // TK71HM
        public string OT_DATE; // 전과일
        public string OT_DRID; // 전출의
        public string OT_DRNM;
        public string OT_DPTCD;
        public string OT_INSDPTCD;
        public string OT_INSDPTCD2;
        public string DRNM;
        public string SYSDT;
        public string SYSTM;
        public string CHKTREA1;
        public string CHKTREA2;
        public string CHKTREA3;
        public string CHKTREA4;
        public string STATUS;
        public string OPNAME;
        public string DXDNM;
        // TK71GR
        public string IN_RESULT; // 검사결과
        public string IN_INSDPTCD; // 전입과
        public string IN_INSDPTCD2; // 전입과

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
            get { return OT_DATE; }
        }
        public string MVOT_DGSBJT_CD // 전출과
        {
            get { return OT_INSDPTCD; }
        }
        public string MVOT_IFLD_DTL_SPC_SBJT_CD // 전출과 내과세부
        {
            get { return OT_INSDPTCD == "01" ? OT_INSDPTCD2 : ""; }
        }
        public string MVIN_DGSBJT_CD // 전입과
        {
            get { return IN_INSDPTCD; }
        }
        public string MVIN_IFLD_DTL_SPC_SBJT_CD // 전입과 내과세부
        {
            get { return IN_INSDPTCD == "01" ? IN_INSDPTCD2 : ""; }
        }
        public string CHRG_DR_NM // 담당의사
        {
            get { return OT_DRNM; }
        }
        public string WRTP_NM // 작성자성명
        {
            get { return DRNM; }
        }
        public string WRT_DT // 작성일시
        {
            get { return SYSDT + SYSTM; }
        }
        public string TRFR_RS_TXT // 전과사유
        {
            get
            {
                string ret = "";
                ret = CHKTREA1; ;
                if (CHKTREA2 == "1") ret += ",타과치료";
                if (CHKTREA3 == "1") ret += ",원내부재";
                if (CHKTREA4 == "1") ret += ",기타";
                return ret;
            }
        }
        public string PTNT_STAT_TXT // 치료경과 및 환자상태
        {
            get { return STATUS; }
        }
        public string EXM_RST_TXT // 주요 검사결과
        {
            get { return IN_RESULT; }
        }
        public string EXEC_DT // 처치.수술 시행일시
        {
            get { return ""; }
        }
        public string SOPR_NM // 처치.수술명
        {
            get { return OPNAME; }
        }
        public string DIAG_NM // 진단명
        {
            get { return DXDNM; }
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
            // TK71HM
            OT_DATE = ""; // 전과일
            OT_DRID = ""; // 전출의
            OT_DRNM = "";
            OT_DPTCD = "";
            OT_INSDPTCD = "";
            OT_INSDPTCD2 = "";
            DRNM = "";
            SYSDT = "";
            SYSTM = "";
            CHKTREA1 = "";
            CHKTREA2 = "";
            CHKTREA3 = "";
            CHKTREA4 = "";
            STATUS = "";
            OPNAME = "";
            DXDNM = "";
            // TK71GR
            IN_RESULT = ""; // 검사결과
            IN_INSDPTCD = ""; // 전입과
            IN_INSDPTCD2 = ""; // 전입과
        }
    }
}
