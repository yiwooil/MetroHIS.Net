using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRNO001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RNO001_LIST.Clear();

            string sql = "";

            if (p_dsdata.IOFG == "2")
            {
                //
                sql = "";
                sql += System.Environment.NewLine + "SELECT V92.WDATE, V92.WTIME, V92.RESULT, V92.PNURES AS PNURSE, V92.PDRID";
                sql += System.Environment.NewLine + "     , A07.DPTCD, A09.INSDPTCD, A09.INSDPTCD2";
                sql += System.Environment.NewLine + "  FROM TV92 V92 INNER JOIN TA07 A07 ON A07.DRID=V92.PDRID";
                sql += System.Environment.NewLine + "                INNER JOIN TA09 A09 ON A09.DPTCD=A07.DPTCD";
                sql += System.Environment.NewLine + " WHERE V92.PID='" + p_dsdata.PID + "'";
                sql += System.Environment.NewLine + "   AND V92.BEDEDT='" + p_dsdata.FRDT + "'";
                sql += System.Environment.NewLine + "   AND V92.WDATE>='" + p_dsdata.FRDT + "'";
                sql += System.Environment.NewLine + "   AND V92.WDATE<='" + p_dsdata.TODT + "'";
                sql += System.Environment.NewLine + " ORDER BY V92.WDATE, V92.WTIME";
            }
            else
            {
                //
                sql = "";
                sql += System.Environment.NewLine + "SELECT E93.WDATE, E93.WTIME, E93.RESULT, E93.EMPID AS PNURSE, E93.PDRID";
                sql += System.Environment.NewLine + "     , A07.DPTCD, A09.INSDPTCD, A09.INSDPTCD2";
                sql += System.Environment.NewLine + "  FROM TE93 E93 INNER JOIN TA07 A07 ON A07.DRID=E93.PDRID";
                sql += System.Environment.NewLine + "                INNER JOIN TA09 A09 ON A09.DPTCD=A07.DPTCD";
                sql += System.Environment.NewLine + " WHERE E93.PID='" + p_dsdata.PID + "'";
                sql += System.Environment.NewLine + "   AND E93.EXDT>='" + p_dsdata.FRDT + "'";
                sql += System.Environment.NewLine + "   AND E93.EXDT<='" + p_dsdata.TODT + "'";
                sql += System.Environment.NewLine + " ORDER BY E93.WDATE, E93.WTIME";
            }

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataRNO001 data = new CDataRNO001();
                data.Clear();

                data.DPTCD = row["DPTCD"].ToString();
                data.INSDPTCD = row["INSDPTCD"].ToString();
                data.INSDPTCD2 = row["INSDPTCD2"].ToString();

                data.WDATE = row["WDATE"].ToString();
                data.WTIME = row["WTIME"].ToString();
                data.RESULT = row["RESULT"].ToString();
                data.PNURSE = row["PNURSE"].ToString();
                data.PNURSE_NM = CUtil.GetEmpnm(data.PNURSE, p_conn);

                p_dsdata.RNO001_LIST.Add(data);

                return true;
            });

        }
    }
}
