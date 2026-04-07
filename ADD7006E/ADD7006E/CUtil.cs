using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CUtil
    {
        public static string GetSubstring(string p_value, int startIndex, int length)
        {
            return p_value.Length >= length ? p_value.Substring(startIndex, length) : p_value;
        }

        public static string GetEmpnm(string p_empid, OleDbConnection p_conn)
        {
            string ret = "";
            string sql = "";
            if (p_empid.StartsWith("AA"))
            {
                sql = "SELECT DRNM EMPNM FROM TA07 WHERE DRID='" + p_empid + "'";
            }
            else
            {
                sql = "SELECT EMPNM FROM TA13 WHERE EMPID='" + p_empid + "'";
            }
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                ret = reader["EMPNM"].ToString();
                return false;
            });
            return ret;
        }
    }
}
