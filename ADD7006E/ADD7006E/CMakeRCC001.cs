using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRCC001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RCC001_LIST.Clear();

            if (p_dsdata.IOFG != "2") return;

            // 의뢰내역
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT V01.PID,V01.BEDEDT,V01.BDIV,V01.ODT,V01.ONO,V01.OTM,V01A.FLDCD9,V01.EXDRID";
            sql += System.Environment.NewLine + "     , A07.DRNM,A07.DPTCD,A09.INSDPTCD,A09.INSDPTCD2";
            sql += System.Environment.NewLine + "  FROM TV01 V01 INNER JOIN TV01A V01A ON V01A.HDID=V01.HDID";
            sql += System.Environment.NewLine + "                LEFT JOIN TA07 A07 ON A07.DRID=V01.EXDRID";
            sql += System.Environment.NewLine + "                LEFT JOIN TA09 A09 ON A09.DPTCD=A07.DPTCD";
            sql += System.Environment.NewLine + " WHERE V01.PID='" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND V01.BEDEDT='" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + "   AND V01.ODIVCD='C'";
            sql += System.Environment.NewLine + "   AND V01.DCFG='0'";
            sql += System.Environment.NewLine + " ORDER BY V01.ODT,V01.ONO";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataRCC001 data = new CDataRCC001();
                data.Clear();

                data.ODT = row["ODT"].ToString();
                data.OTM = row["OTM"].ToString();
                data.EXDPTCD = row["DPTCD"].ToString();
                data.EXINSDPTCD = row["INSDPTCD"].ToString();
                data.EXINSDPTCD2 = row["INSDPTCD2"].ToString();
                data.EXDRID = row["EXDRID"].ToString();
                data.EXDRNM = row["DRNM"].ToString();
                data.FLDCD9 = row["FLDCD9"].ToString(); // 의뢰내용

                // 회신내역
                string sql2 = "";
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT U07.CSTDPTCD,U07.CSTDRID,U07.REPLY,U07.REPLYDT,U07.REPLYTM";
                sql2 += System.Environment.NewLine + "     , A07.DRNM,A09.INSDPTCD,A09.INSDPTCD2";
                sql2 += System.Environment.NewLine + "  FROM TU07 U07 LEFT JOIN TA07 A07 ON A07.DRID=U07.CSTDRID";
                sql2 += System.Environment.NewLine + "                LEFT JOIN TA09 A09 ON A09.DPTCD=U07.CSTDPTCD";
                sql2 += System.Environment.NewLine + " WHERE U07.PID='" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND U07.BEDEDT='" + row["BEDEDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND U07.BDIV='" + row["BDIV"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND U07.ODT='" + row["ODT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND U07.ONO='" + row["ONO"].ToString() + "'";
                sql2 += System.Environment.NewLine + " ORDER BY U07.PID,U07.BEDEDT,U07.BDIV,U07.ODT,U07.ONO";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.REPLYDT = row2["REPLYDT"].ToString();
                    data.REPLYTM = row2["REPLYTM"].ToString();
                    data.CSTDPTCD = row2["CSTDPTCD"].ToString();
                    data.CSTINSDPTCD = row2["INSDPTCD"].ToString();
                    data.CSTINSDPTCD2 = row2["INSDPTCD2"].ToString();
                    data.CSTDRID = row2["CSTDRID"].ToString();
                    data.CSTDRNM = row2["DRNM"].ToString();
                    data.REPLY = row2["REPLY"].ToString(); // 회신내용

                    return MetroLib.SqlHelper.BREAK;
                });

                // TT05
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT PTYSQ,ROFG,DACD,DXD";
                sql2 += System.Environment.NewLine + "  FROM TT05";
                sql2 += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND BDEDT='" + p_dsdata.FRDT + "'";
                sql2 += System.Environment.NewLine + " ORDER BY PTYSQ,DPTCD,SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.PTYSQ.Add(row2["PTYSQ"].ToString());
                    data.ROFG.Add(row2["ROFG"].ToString());
                    data.DACD.Add(row2["DACD"].ToString());
                    data.DXD.Add(row2["DXD"].ToString());

                    return MetroLib.SqlHelper.CONTINUE;
                });

                p_dsdata.RCC001_LIST.Add(data);

                return MetroLib.SqlHelper.CONTINUE;
            });
        }
    }
}
