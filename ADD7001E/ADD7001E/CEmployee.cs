using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7001E
{
    class CEmployee
    {
        static public String GetEmpnm(OleDbConnection p_conn, String p_empid)
        {
            if ("".Equals(p_empid)) return "";

            String strRet = "";
            String sql = "";
            if (p_empid.StartsWith("AA"))
            {
                sql += System.Environment.NewLine + "SELECT A07.DRNM EMPNM ";
                sql += System.Environment.NewLine + "  FROM TA07 A07";
                sql += System.Environment.NewLine + " WHERE A07.DRID LIKE '" + p_empid + "%' ";
                sql += System.Environment.NewLine + " ORDER BY A07.DRID";
            }
            else
            {
                sql += System.Environment.NewLine + "SELECT A13.EMPNM ";
                sql += System.Environment.NewLine + "  FROM TA13 A13";
                sql += System.Environment.NewLine + " WHERE A13.EMPID='" + p_empid + "' ";
            }

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    strRet = reader["EMPNM"].ToString();
                }
                reader.Close();
            }
            return strRet;
        }
    }
}
