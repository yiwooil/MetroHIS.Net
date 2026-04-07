using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class ERD001_Data
    {
        public string ODT;
        public string BLOODDT;
        public string BLOODTM;
        public string RCVDT;
        public string RCVTM;
        public string VFYDT;
        public string VFYTM;
        public string SPCNM;
        public string SPCNO;
        public string STSCD;
        public string ABBRNM;
        public string TESTCD;
        public string RSTVAL;
        public string REFERCHK;
        public string PANICCHK;
        public string DELTACHK;
        public string UNIT;
        public string REFERENCE;
        public string MAJDR;
        public string MAJNM;
        public string DEPTCD;
        public string INSDPTCD;
        public string INSDPTCD2;

        //
        public string EXM_PRSC_DT // 처방일시
        {
            get { return ODT == "" ? "" : ODT + "0000"; }
        }
        public string EXM_GAT_DT // 채취일시
        {
            get { return BLOODDT + BLOODTM; }
        }
        public string EXM_RCV_DT // 접수일시
        {
            get { return RCVDT + RCVTM; }
        }
        public string EXM_RST_DT // 검사일시
        {
            get { return VFYDT + VFYTM; }
        }
        public string EXM_SPCM_CD // 검체종류
        {
            get { return "99"; }
        }
        public string EXM_SPCM_CD_NM
        {
            get { return "기타"; }
        }
        public string EXM_SPCM_ETC_TXT // 검체종류상세
        {
            get { return SPCNM == "" ? "-" : SPCNM; }
        }
        public string EXM_MDFEE_CD // 수가코드
        {
            get { return "00"; }
        }
        public string EXM_CD // 검사코드
        {
            get { return TESTCD; }
        }
        public string EXM_NM // 검사명
        {
            get { return ABBRNM; }
        }
        public string EXM_RST_TXT // 검사결과
        {
            get { return RSTVAL == "" ? "-" : RSTVAL; }
        }
        public string EXM_REF_TXT // 참고치
        {
            get { return REFERENCE == "" ? "-" : REFERENCE; }
        }
        //public string DCT_DR_NM // 판독의사 성명
        //{
        //    get { return ""; }
        //}
        public string EXM_UNIT // 단위
        {
            get { return UNIT == "" ? "-" : UNIT; }
        }
        public string EXM_ADD_TXT // 추가정보
        {
            get { return ""; }
        }
        //public string DCT_DR_LCS_NO // 판독의사 면허번호
        //{
        //    get { return ""; }
        //}
        public string DGSBJT_CD // 진료과
        {
            get { return INSDPTCD; ; }
        }
        public string IFLD_DTL_SPC_SBJT_CD // 내과 세부전문과목
        {
            get { return (INSDPTCD == "01" ? INSDPTCD2 : ""); }
        }
        public string PRSC_DR_NM // 처방의사 성명
        {
            get { return MAJNM; }
        }
    }
}
