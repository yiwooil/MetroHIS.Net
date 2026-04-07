using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7000E
{
    class CDataTK71T
    {

        public String BEDEDT;
        public String BEDODT;
        public String M_DXD;
        public String S_DXD;
        public String CHKALL;
        public String DRUG_DESC;
        public String AN_DESC;
        public String OP;
        public String DIE;
        public String BEDODT_CASE;
        public String BP_DESC;
        public String RPTDT;
        public String DRID;
        public String DRNM;

        private String[] aryS_DXD;

        private String[] aryCHKALL;
        private String[] aryAN_DESC;
        private String[] aryDIE;
        private String[] aryBEDODT_CASE;

        public String GetMajDacd()
        {
            String[] tmp;
            tmp = (M_DXD.Replace('-', (char)22) + (char)22).Split((char)22);

            String strDisecd = tmp[0].Replace(".", "");
            String[] tmp2;
            tmp2 = (strDisecd + "_").Split('_');
            strDisecd = tmp2[0];
            if (strDisecd.Length > 6) strDisecd = strDisecd.Substring(0, 6);

            return strDisecd;
        }

        public String GetMajDanm()
        {
            String[] tmp;
            tmp = (M_DXD + (char)22).Split((char)22);
            return tmp[1];
        }

        public String GetEtcDacd(long index)
        {
            String[] tmp;
            tmp = (aryS_DXD[index].Replace('-', (char)22) + (char)22).Split((char)22);

            String strDisecd = tmp[0].Replace(".", "");
            String[] tmp2;
            tmp2 = (strDisecd + "_").Split('_');
            strDisecd = tmp2[0];
            if (strDisecd.Length > 6) strDisecd = strDisecd.Substring(0, 6);

            return strDisecd;
        }

        public String GetEtcDanm(long index)
        {
            String[] tmp;
            tmp = (aryS_DXD[index] + (char)22).Split((char)22);
            return tmp[1];
        }

        public int GetEtcDacdCount()
        {
            if (aryS_DXD == null) return 0;
            else return aryS_DXD.Length;
        }

        public string GetItemValue(String itemNo)
        {
            if (aryCHKALL == null) return "";

            String itemValue = "";
            switch (itemNo)
            {
                case "1.1.1":
                    if (aryCHKALL.Length < 2)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[0])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[1])) itemValue = "2";
                    }
                    break;
                case "1.1.2":
                    if (aryCHKALL.Length < 4)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[2])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[3])) itemValue = "2";
                    }
                    break;
                case "1.1.3":
                    if (aryCHKALL.Length < 6)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[4])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[5])) itemValue = "2";
                    }
                    break;
                case "1.1.4":
                    if (aryCHKALL.Length < 8)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[6])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[7])) itemValue = "2";
                    }
                    break;
                case "2.1.1":
                    if (aryCHKALL.Length < 10)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[8])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[9])) itemValue = "2";
                    }
                    break;
                case "2.1.2":
                    if (aryCHKALL.Length < 12)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[10])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[11])) itemValue = "2";
                    }
                    break;
                case "2.1.3":
                    if (aryCHKALL.Length < 14)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[12])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[13])) itemValue = "2";
                    }
                    break;
                case "2.1.4":
                    if (aryCHKALL.Length < 16)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[14])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[15])) itemValue = "2";
                    }
                    break;
                case "2.2":
                    if (aryCHKALL.Length < 18)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[16])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[17])) itemValue = "2";
                    }
                    break;
                case "2.3":
                    if (aryCHKALL.Length < 20)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[18])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[19])) itemValue = "2";
                    }
                    break;
                case "2.4":
                    if (aryCHKALL.Length < 22)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[20])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[21])) itemValue = "2";
                    }
                    break;
                case "2.5":
                    if (aryCHKALL.Length < 24)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[22])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[23])) itemValue = "2";
                    }
                    break;
                case "2.6":
                    if (aryCHKALL.Length < 26)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[24])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[25])) itemValue = "2";
                    }
                    break;
                case "3.1":
                    if (aryCHKALL.Length < 28)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[26])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[27])) itemValue = "2";
                    }
                    break;
                case "3.2":
                    if (aryCHKALL.Length < 30)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[28])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[29])) itemValue = "2";
                    }
                    break;
                case "3.3":
                    if (aryCHKALL.Length < 32)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[30])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[31])) itemValue = "2";
                    }
                    break;
                case "3.4.1":
                    if (aryCHKALL.Length < 34)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[32])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[33])) itemValue = "2";
                    }
                    break;
                case "3.4.2":
                    if (aryCHKALL.Length < 36)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[34])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[35])) itemValue = "2";
                    }
                    break;
                case "3.4.3":
                    if (aryCHKALL.Length < 38)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[36])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[37])) itemValue = "2";
                    }
                    break;
                case "3.4.4":
                    if (aryCHKALL.Length < 40)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[38])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[39])) itemValue = "2";
                    }
                    break;
                case "3.4.5":
                    if (aryCHKALL.Length < 42)
                    {
                        itemValue = "";
                    }
                    else
                    {
                        if ("1".Equals(aryCHKALL[40])) itemValue = "1";
                        else if ("1".Equals(aryCHKALL[41])) itemValue = "2";
                    }
                    break;
            }
            return itemValue;
        }

        public string GetItemValueEx(String itemNo)
        {
            String itemValue = "";
            switch (itemNo)
            {
                case "2.1.3":
                    itemValue = DRUG_DESC;
                    break;
                case "2.1.4":
                    if (aryAN_DESC != null)
                    {
                        if (aryAN_DESC.Length < 2)
                        {
                            itemValue = "";
                        }
                        else
                        {
                            itemValue = aryAN_DESC[0] + aryAN_DESC[1];
                        }
                    }
                    break;
                case "2.4":
                    String[] tmp;
                    tmp = (OP + (char)22 + (char)22).Split((char)22);
                    itemValue = tmp[2];
                    break;
                case "2.6":
                    if (aryDIE != null)
                    {
                        if (aryDIE.Length < 2)
                        {
                            itemValue = "";
                        }
                        else
                        {
                            if ("V".Equals(aryDIE[0])) itemValue = "1";
                            else if ("V".Equals(aryDIE[1])) itemValue = "2";
                        }
                    }
                    break;
                case "3.3":
                    if (aryBEDODT_CASE != null)
                    {
                        if (aryBEDODT_CASE.Length < 3)
                        {
                            itemValue = "";
                        }
                        else
                        {
                            if ("V".Equals(aryBEDODT_CASE[0])) itemValue = "1";
                            else if ("V".Equals(aryBEDODT_CASE[1])) itemValue = "2";
                            else if ("V".Equals(aryBEDODT_CASE[2])) itemValue = "3";
                        }
                    }
                    break;
            }
            return itemValue;
        }

        public void Clear()
        {
            BEDEDT = "";
            BEDODT = "";
            M_DXD = "";
            S_DXD = "";
            CHKALL = "";
            DRUG_DESC = "";
            AN_DESC = "";
            OP = "";
            DIE = "";
            BEDODT_CASE = "";
            BP_DESC = "";
            RPTDT = "";
            DRID = "";
            DRNM = "";

            aryS_DXD = null;
            aryCHKALL = null;
            aryAN_DESC = null;
            aryDIE = null;
            aryBEDODT_CASE = null;
        }

        public void SetData(CDataTI2A i2a)
        {
            try
            {
                string sql="";
                sql += System.Environment.NewLine + "SELECT	TOP 1 BEDEDT";	/* 입원일 */
                sql += System.Environment.NewLine + "     , BEDODT";	    /* 퇴원일 */
                sql += System.Environment.NewLine + "     , M_DXD";	        /* 주진단 */
                sql += System.Environment.NewLine + "     , S_DXD";			/* 부진단 */
                sql += System.Environment.NewLine + "     , CHKALL";		/* 1. 수술 전 진료의 점검사항 ~ 3. 퇴원 전 진료의 점검사항까지 모든 체크박스 (체크 1  아니면 0) */
                sql += System.Environment.NewLine + "     , DRUG_DESC";		/* 2. 입원 중 진료의 점검사항 2.1 입원중에 일어난 상해 3) 투약실수 혹은 약물부작용 */
                sql += System.Environment.NewLine + "     , AN_DESC";		/* 2. 입원 중 진료의 점검사항 2.1 입원중에 일어난 상해 4) 마취사고 혹은 마취부작용 */
                sql += System.Environment.NewLine + "     , OP";			/* 2. 입원 중 진료의 점검사항 2.4 수술에 따른 합병증  */
                sql += System.Environment.NewLine + "     , DIE";			/* 2. 입원 중 진료의 점검사항 2.6 사망  */
                sql += System.Environment.NewLine + "     , BEDODT_CASE";	/* 3. 퇴원 전 진료의 점검사항 3.3 퇴원의 유형(정상퇴원 여부)  */
                sql += System.Environment.NewLine + "     , BP_DESC";		/* 3. 퇴원 전 진료의 점검사항 3.4 퇴원시 환자 상태의 안정성 1)혈압  */
                sql += System.Environment.NewLine + "     , RPTDT";			/* 작성일 */
                sql += System.Environment.NewLine + "     , DRID";	        /* 작성자ID */
                sql += System.Environment.NewLine + "     , DRNM";	        /* 작성자명 */ 
                sql += System.Environment.NewLine + "  FROM TK71T";
                sql += System.Environment.NewLine + " WHERE PID =	'" + i2a.PID + "'";
                sql += System.Environment.NewLine + "   AND BEDEDT = '" + i2a.BDEDT + "'";
                sql += System.Environment.NewLine + "   AND ISNULL(CANCEL,'') = ''";
                sql += System.Environment.NewLine + " ORDER BY SEQ DESC";
                string strConn = DBHelper.GetConnectionString();

                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    // TSQL문장과 Connection 객체를 지정   
                    OleDbCommand cmd = new OleDbCommand(sql, conn);

                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        BEDEDT = reader["BEDEDT"].ToString();
                        BEDODT = reader["BEDODT"].ToString();
                        M_DXD = reader["M_DXD"].ToString();
                        S_DXD = reader["S_DXD"].ToString();
                        CHKALL = reader["CHKALL"].ToString();
                        DRUG_DESC = reader["DRUG_DESC"].ToString();
                        AN_DESC = reader["AN_DESC"].ToString();
                        OP = reader["OP"].ToString();
                        DIE = reader["DIE"].ToString();
                        BEDODT_CASE = reader["BEDODT_CASE"].ToString();
                        RPTDT = reader["RPTDT"].ToString();
                        DRID = reader["DRID"].ToString();
                        DRNM = reader["DRNM"].ToString();

                        if ("".Equals(S_DXD)==false)
                        {
                            aryS_DXD = S_DXD.Split((char)23);
                        }


                        aryCHKALL = CHKALL.Split((char)22);
                        aryAN_DESC = AN_DESC.Split((char)22);
                        aryDIE = DIE.Split((char)22);
                        aryBEDODT_CASE = BEDODT_CASE.Split((char)22);

                        //DRNM = GetEmpnm(conn, DRID);
                        //if ("".Equals(DRNM)) DRNM = GetDrnm(conn, DRID);

                     }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private String GetEmpnm(OleDbConnection p_conn, String p_empid)
        {
            String strRet = "";
            String sql = "";
            sql += System.Environment.NewLine + "SELECT EMPNM ";
            sql += System.Environment.NewLine + "  FROM TA13 ";
            sql += System.Environment.NewLine + " WHERE EMPID='" + p_empid + "' ";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                strRet = reader["EMPNM"].ToString();
            }
            reader.Close();
            return strRet;
        }

        private String GetDrnm(OleDbConnection p_conn, String p_drid)
        {
            String strRet = "";
            String sql = "";
            sql += System.Environment.NewLine + "SELECT DRNM ";
            sql += System.Environment.NewLine + "  FROM TA07 ";
            sql += System.Environment.NewLine + " WHERE DRID='" + p_drid + "' ";

            // TSQL문장과 Connection 객체를 지정   
            OleDbCommand cmd = new OleDbCommand(sql, p_conn);

            // 데이타는 서버에서 가져오도록 실행
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                strRet = reader["DRNM"].ToString();
            }
            reader.Close();
            return strRet;
        }
    }
}
