using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class MySendResult
    {
        public string STATUS = "";
        public string STATUS_NM { get { return GetStatusNm(STATUS); } }
        public string ERR_CODE = "";
        public string ERR_DESC = "";

        // 송신후 결과
        public string DOC_NO; //문서번호
        public string SUPL_DATA_FOM_CD; //서식코드
        public string RCV_NO; //접수번호
        public string SP_SNO; //명세서일련번호
        public string HOSP_RNO; //환자등록번호
        public string PAT_NM; //환자성명
        public string INSUP_TP_CD; //참고업무구분

        public void Clear()
        {
            STATUS = "";
            ERR_CODE = "";
            ERR_DESC = "";

            // 송신후 결과
            DOC_NO = ""; //문서번호
            SUPL_DATA_FOM_CD = ""; //서식코드
            RCV_NO = ""; //접수번호
            SP_SNO = ""; //명세서일련번호
            HOSP_RNO = ""; //환자등록번호
            PAT_NM = ""; //환자성명
            INSUP_TP_CD = ""; //참고업무구분
        }

        private string GetStatusNm(string p_status)
        {
            string ret = "";
            if (p_status == "A") ret = "자료준비중"; // 자료 읽는중
            else if (p_status == "B") ret = "자료준비됨"; // 자료 준비됨
            else if (p_status == "Y") ret = "전송성공"; // 전송성공
            else if (p_status == "T") ret = "임시전송성공"; // 임시전송성공
            else if (p_status == "E") ret = "전송오류"; // 전송오류
            else if (p_status == "N") ret = "전송중"; // 전송중
            else if (p_status == "X") ret = "전송제외"; // 전송제외
            else ret = "";
            return ret;
        }
    }
}
