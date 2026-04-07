using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7001E
{
    class CUtil
    {
        static public String GetSysDate(OleDbConnection p_conn)
        {
            String strRet = "";
            String sql = "SELECT CONVERT(VARCHAR,GETDATE(),112) AS SYSDATE ";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    strRet = reader["SYSDATE"].ToString();
                }
                reader.Close();
            }
            return strRet;
        }
        static public String GetSysTime(OleDbConnection p_conn)
        {
            String strRet = "";
            String sql = "SELECT REPLACE(CONVERT(VARCHAR,GETDATE(),8),':','') AS SYSTIME ";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    strRet = reader["SYSTIME"].ToString();
                }
                reader.Close();
            }
            return strRet;
        }
    }
}
