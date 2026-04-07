using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7005E
{
    class CData
    {
        // TI2A
        private string m_EPRTNO;

        public bool SEL { get; set; }
        public int NO { get; set; }

        public string DEMNO { get; set; }
        public string EPRTNO
        {
            set { m_EPRTNO = value; }
            get
            {
                string ret = m_EPRTNO;
                if (m_EPRTNO.Length == 4) ret = "0" + m_EPRTNO;
                else if (m_EPRTNO.Length == 3) ret = "00" + m_EPRTNO;
                else if (m_EPRTNO.Length == 2) ret = "000" + m_EPRTNO;
                else if (m_EPRTNO.Length == 1) ret = "0000" + m_EPRTNO;
                return ret;
            }
        }
        public string PID { get; set; }
        public string PNM { get; set; }
        public string PSEX { get; set; }
        public string PSEX_MF
        {
            get { return (PSEX == "1" ? "M" : "F"); }
        }
        public string DPTCD { get; set; }
        public string STEDT { get; set; }
        public string ENDDT { get; set; }
        public string BDEDT { get; set; }
        public string RESID { get; set; }
        public string RESID1
        {
            get { return (RESID.Length >= 6 ? RESID.Substring(0, 6) : RESID); }
        }
        public string QFYCD { get; set; }
        public string STATUS { get; set; }
        public string STATUS_NM
        {
            get
            {
                string ret = "";
                if (STATUS == "Y") ret = "성공";
                else if (STATUS == "T") ret = "임시성공";// 임시전송성공
                else if (STATUS == "E") ret = "오류";
                else if (STATUS == "N") ret = "진행중";
                else if (STATUS == "X") ret = "전송제외";
                else ret = "";

                return ret;
            }
        }
        public string ERRMSG
        {
            get { return "(" + ERR_CODE + ")" + ERR_DESC; }
        }

        // EMR306
        public string RDATE { get; set; }
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
        public string Q4 { get; set; }
        public string Q5 { get; set; }
        public string Q6 { get; set; }
        public string Q7 { get; set; }
        public string Q8 { get; set; }
        public string Q9 { get; set; }
        public string Q10 { get; set; }
        public string Q11 { get; set; }
        public string Q12 { get; set; }
        public string Q13 { get; set; }
        public string Q14 { get; set; }
        public string Q15 { get; set; }
        public string Q16 { get; set; }
        public string Q17 { get; set; }
        public string Q18 { get; set; }
        public string Q19 { get; set; }
        public string Q20 { get; set; }
        public string Q21 { get; set; }
        public string Q22 { get; set; }
        public string Q23 { get; set; }
        public string Q24 { get; set; }
        public string Q25 { get; set; }
        public string Q26 { get; set; }
        public string OTHERS { get; set; }
        public string GUBUN { get; set; }
        public string DIAG { get; set; }
        public string QOTHER { get; set; }
        public string Q25N { get; set; }
        public string Q26N { get; set; }

        public string RCV_NO_0000000;

        public string A_KEY;
        public string B_KEY;

        public string ERR_CODE;
        public string ERR_DESC;

        public string DOC_NO; //문서번호
        public string SUPL_DATA_FOM_CD; //서식코드
        public string RCV_NO; //접수번호
        public string SP_SNO; //명세서일련번호
        public string HOSP_RNO; //환자등록번호
        public string PAT_NM; //환자성명
        public string INSUP_TP_CD; //참고업무구분

        public void Clear()
        {
            SEL = false;

            // TI2A
            DEMNO = "";
            EPRTNO = "";
            PID = "";
            PNM = "";
            PSEX = "";
            DPTCD = "";
            STEDT = "";
            BDEDT = "";
            RESID = "";
            QFYCD = "";

            STATUS = "";

            // EMR306
            RDATE = "";
            Q1 = "";
            Q2 = "";
            Q3 = "";
            Q4 = "";
            Q5 = "";
            Q6 = "";
            Q7 = "";
            Q8 = "";
            Q9 = "";
            Q10 = "";
            Q11 = "";
            Q12 = "";
            Q13 = "";
            Q14 = "";
            Q15 = "";
            Q16 = "";
            Q17 = "";
            Q18 = "";
            Q19 = "";
            Q20 = "";
            Q21 = "";
            Q22 = "";
            Q23 = "";
            Q24 = "";
            Q25 = "";
            Q26 = "";
            OTHERS = "";
            GUBUN = "";
            DIAG = "";
            QOTHER = "";
            Q25N = "";
            Q26N = "";

            RCV_NO_0000000 = "";

            A_KEY = ""; // TI2A(TI1A) KEY
            B_KEY = ""; // EMR306 KEY

            ERR_CODE = "";
            ERR_DESC = "";

            DOC_NO = ""; //문서번호
            SUPL_DATA_FOM_CD = ""; //서식코드
            RCV_NO = ""; //접수번호
            SP_SNO = ""; //명세서일련번호
            HOSP_RNO = ""; //환자등록번호
            PAT_NM = ""; //환자성명
            INSUP_TP_CD = ""; //참고업무구분

        }

        // 심평원에서 yes=1, no=2로 받음
        // 우리는 yes=1, no=0으로 저장함.
        public string GetYn(string value)
        {
            return value == "1" ? "1" : "2"; 
        }

    }
}
