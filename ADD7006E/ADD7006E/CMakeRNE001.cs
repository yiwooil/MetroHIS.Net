using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRNE001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RNE001_LIST.Clear();

            CDataRNE001 data = new CDataRNE001();

            string sql = "";
            sql += Environment.NewLine + "SELECT BEDEDT, INTM, BEDODT, DCTM, INRT";
            sql += Environment.NewLine + "  FROM TV95_ER";
            sql += Environment.NewLine + " WHERE PID = '" + p_dsdata.PID + "'";
            sql += Environment.NewLine + "   AND BEDEDT = '" + p_dsdata.FRDT + "'";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                data.BEDEDT = row["BEDEDT"].ToString();
                data.INTM = row["INTM"].ToString();
                data.BEDODT = row["BEDODT"].ToString();
                data.DCTM = row["DCTM"].ToString();
                data.INRT = row["INRT"].ToString();

                // TU64(BDIV=3)
                string sql2 = "";
                sql2 += Environment.NewLine + "SELECT CHKDT, CHKTM, BP, PR, RR, TMP, SPO2, RMK";
                sql2 += Environment.NewLine + "  FROM TU64";
                sql2 += Environment.NewLine + " WHERE PID = '" + p_dsdata.PID + "'";
                sql2 += Environment.NewLine + "   AND BEDEDT = '" + row["BEDEDT"].ToString() + "'";
                sql2 += Environment.NewLine + "   AND BDIV='3'";
                sql2 += Environment.NewLine + " ORDER BY WDATE, WTIME";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.CHKDT.Add(row2["CHKDT"].ToString());
                    data.CHKTM.Add(row2["CHKTM"].ToString());
                    data.BP.Add(row2["BP"].ToString());
                    data.PR.Add(row2["PR"].ToString());
                    data.RR.Add(row2["RR"].ToString());
                    data.TMP.Add(row2["TMP"].ToString());
                    data.SPO2.Add(row2["SPO2"].ToString());
                    data.RMK.Add(row2["RMK"].ToString());

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // TV92(BDIV=3)
                sql2 = "";
                sql2 += Environment.NewLine + "SELECT WDATE, WTIME, RESULT, PNURES";
                sql2 += Environment.NewLine + "  FROM TV92";
                sql2 += Environment.NewLine + " WHERE PID = '" + p_dsdata.PID + "'";
                sql2 += Environment.NewLine + "   AND BEDEDT = '" + row["BEDEDT"].ToString() + "'";
                sql2 += Environment.NewLine + "   AND BDIV='3'";
                sql2 += Environment.NewLine + " ORDER BY WDATE, WTIME";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.WDATE.Add(row2["WDATE"].ToString());
                    data.WDATE.Add(row2["WTIME"].ToString());
                    data.RESULT.Add(row2["RESULT"].ToString());
                    data.PNURES.Add(row2["PNURES"].ToString());
                    data.PNURES_NM.Add(CUtil.GetEmpnm(row2["PNURES"].ToString(), p_conn));

                    return MetroLib.SqlHelper.CONTINUE;
                });

                return MetroLib.SqlHelper.CONTINUE;
            });


        }
    }
}
