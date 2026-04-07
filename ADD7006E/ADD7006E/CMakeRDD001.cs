using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRDD001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RDD001_LIST.Clear();

            if (p_dsdata.IOFG == "2")
            {

                // 입원처방
                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT V01.PID,V01.BEDEDT,V01.BDIV,V01.ODT,V01.ONO,V01.OTM,V01.EXDRID,V01.ODIVCD,V01.DPTCD,V01.RMK";
                sql += System.Environment.NewLine + "     , V01A.OCD,V01A.OQTY,V01A.OUNIT,V01A.ORDCNT,V01A.ODAYCNT";
                sql += System.Environment.NewLine + "     , A07.DRNM EXDRNM";
                sql += System.Environment.NewLine + "     , A09.INSDPTCD,A09.INSDPTCD2";
                sql += System.Environment.NewLine + "     , A18.ONM";
                sql += System.Environment.NewLine + "     , A04.BEDEHM";
                sql += System.Environment.NewLine + "  FROM TV01 V01 INNER JOIN TV01A V01A ON V01A.HDID=V01.HDID";
                sql += System.Environment.NewLine + "                INNER JOIN TA04 A04 ON A04.PID=V01.PID AND A04.BEDEDT=V01.BEDEDT";
                sql += System.Environment.NewLine + "                LEFT JOIN TA07 A07 ON A07.DRID=V01.EXDRID";
                sql += System.Environment.NewLine + "                LEFT JOIN TA09 A09 ON A09.DPTCD=V01.DPTCD";
                sql += System.Environment.NewLine + "                LEFT JOIN TA18 A18 ON A18.OCD=V01A.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=V01A.OCD AND X.CREDT<=V01.ODT)";
                sql += System.Environment.NewLine + " WHERE V01.PID='" + p_dsdata.PID + "'";
                sql += System.Environment.NewLine + "   AND V01.BEDEDT='" + p_dsdata.FRDT + "'";
                sql += System.Environment.NewLine + "   AND V01.DCFG NOT IN ('1')";
                sql += System.Environment.NewLine + "   AND V01.ODIVCD NOT LIKE 'H%'"; // D/C 시키는 처방 제외
                sql += System.Environment.NewLine + " ORDER BY V01.PID,V01.BEDEDT,V01.BDIV,V01.ODT,V01.ONO,V01A.SEQ";

                MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
                {
                    CDataRDD001 data = new CDataRDD001();
                    data.Clear();

                    data.BEDEDT = row["BEDEDT"].ToString();
                    data.BEDEHM = row["BEDEHM"].ToString();

                    data.DPTCD = row["DPTCD"].ToString();
                    data.INSDPTCD = row["INSDPTCD"].ToString();
                    data.INSDPTCD2 = row["INSDPTCD2"].ToString();
                    data.EXDRID = row["EXDRID"].ToString();
                    data.EXDRNM = row["EXDRNM"].ToString();

                    data.ODIVCD = row["ODIVCD"].ToString();
                    data.ODT = row["ODT"].ToString();
                    data.OTM = row["OTM"].ToString();
                    data.OCD = row["OCD"].ToString();
                    data.ONM = row["ONM"].ToString();
                    data.OQTY = row["OQTY"].ToString();
                    data.OUNIT = row["OUNIT"].ToString();
                    data.ORDCNT = row["ORDCNT"].ToString();
                    data.ODAYCNT = row["ODAYCNT"].ToString();

                    data.RMK = row["RMK"].ToString();

                    p_dsdata.RDD001_LIST.Add(data);

                    return true;
                });
            }
            else
            {
                string exdt = "";
                string hms = "";

                // 외래접수내역
                string sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT EXDT,HMS";
                sql += System.Environment.NewLine + "  FROM TS21";
                sql += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                sql += System.Environment.NewLine + "   AND EXDT='" + p_dsdata.FRDT + "'";
                sql += System.Environment.NewLine + "   AND DPTCD='" + p_dsdata.DPTCD + "'";
                sql += System.Environment.NewLine + "   AND ISNULL(CCFG,'') IN ('','0')";
                sql += System.Environment.NewLine + " ORDER BY EXDT, HMS DESC";

                MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
                {
                    exdt = row["EXDT"].ToString();
                    hms = row["HMS"].ToString();

                    return false;
                });


                // 외래처방
                sql = "";
                sql += System.Environment.NewLine + "SELECT E01.PID,E01.ODT,E01.ONO,E01.OTM,E01.EXDRID,E01.ODIVCD,E01.DPTCD,E01.RMK";
                sql += System.Environment.NewLine + "     , E01A.OCD,E01A.OQTY,E01A.OUNIT,E01A.ORDCNT,E01A.ODAYCNT";
                sql += System.Environment.NewLine + "     , A07.DRNM EXDRNM";
                sql += System.Environment.NewLine + "     , A09.INSDPTCD,A09.INSDPTCD2";
                sql += System.Environment.NewLine + "     , A18.ONM";
                sql += System.Environment.NewLine + "  FROM TE01 E01 INNER JOIN TE01A E01A ON E01A.HDID=E01.HDID";
                sql += System.Environment.NewLine + "                LEFT JOIN TA07 A07 ON A07.DRID=E01.EXDRID";
                sql += System.Environment.NewLine + "                LEFT JOIN TA09 A09 ON A09.DPTCD=E01.DPTCD";
                sql += System.Environment.NewLine + "                LEFT JOIN TA18 A18 ON A18.OCD=E01A.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=E01A.OCD AND X.CREDT<=E01.ODT)";
                sql += System.Environment.NewLine + " WHERE E01.PID='" + p_dsdata.PID + "'";
                sql += System.Environment.NewLine + "   AND E01.ODT='" + p_dsdata.FRDT + "'";
                sql += System.Environment.NewLine + "   AND E01.DCFG NOT IN ('1')";
                sql += System.Environment.NewLine + "   AND E01.ODIVCD NOT LIKE 'H%'"; // D/C 시키는 처방 제외
                sql += System.Environment.NewLine + " ORDER BY E01.PID,E01.ODT,E01.ONO,E01A.SEQ";

                MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
                {
                    CDataRDD001 data = new CDataRDD001();
                    data.Clear();

                    data.EXDT = exdt;
                    data.HMS = hms;

                    data.DPTCD = row["DPTCD"].ToString();
                    data.INSDPTCD = row["INSDPTCD"].ToString();
                    data.INSDPTCD2 = row["INSDPTCD2"].ToString();
                    data.EXDRID = row["EXDRID"].ToString();
                    data.EXDRNM = row["EXDRNM"].ToString();

                    data.ODIVCD = row["ODIVCD"].ToString();
                    data.ODT = row["ODT"].ToString();
                    data.OTM = row["OTM"].ToString();
                    data.OCD = row["OCD"].ToString();
                    data.ONM = row["ONM"].ToString();
                    data.OQTY = row["OQTY"].ToString();
                    data.OUNIT = row["OUNIT"].ToString();
                    data.ORDCNT = row["ORDCNT"].ToString();
                    data.ODAYCNT = row["ODAYCNT"].ToString();

                    data.RMK = row["RMK"].ToString();

                    p_dsdata.RDD001_LIST.Add(data);

                    return true;
                });
            }
        }
    }
}
