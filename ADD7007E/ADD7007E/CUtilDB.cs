using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    class CUtilDB
    {
        static public string ReadTI88B(string kind, string key, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            string value = "";

            string sql = "";
            sql += Environment.NewLine + "SELECT FLD1QTY";
            sql += Environment.NewLine + "  FROM TI88B";
            sql += Environment.NewLine + " WHERE MST1CD='A'";
            sql += Environment.NewLine + "   AND MST2CD='EFormASM'";
            sql += Environment.NewLine + "   AND MST3CD='" + kind + "'";
            sql += Environment.NewLine + "   AND MST4CD='" + key + "'";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, p_tran, delegate(DataRow row)
            {
                value = row["FLD1QTY"].ToString().Trim();
                return MetroLib.SqlHelper.BREAK;
            });

            return value;
        }

        static public string GetPCODENM(string pcode, string gubun, OleDbConnection conn)
        {
            // 현재일 기준으로 검색
            string sysdt = MetroLib.Util.GetSysDate(conn);
            return GetPCODENM(pcode, gubun, sysdt, conn);
        }

        static public string GetPCODENM(string pcode, string gubun, string exdt, OleDbConnection conn)
        {
            if (pcode == "" || gubun == "") return "";

            string pcodnm = "";
            string sql = "";
            sql += Environment.NewLine + "SELECT PCODENM";
            sql += Environment.NewLine + "  FROM TI09 I09";
            sql += Environment.NewLine + " WHERE I09.GUBUN='" + gubun + "'";
            sql += Environment.NewLine + "   AND I09.PCODE='" + pcode + "'";
            sql += Environment.NewLine + "   AND I09.ADTDT=(SELECT MAX(X.ADTDT) FROM TI09 X WHERE X.GUBUN=I09.GUBUN AND X.PCODE=I09.PCODE AND X.ADTDT<='" + exdt + "')";

            MetroLib.SqlHelper.GetDataRow(sql, conn, null, delegate(DataRow row)
            {
                pcodnm = row["PCODENM"].ToString();
                return MetroLib.SqlHelper.BREAK;
            });

            return pcodnm;
        }

    }

}
