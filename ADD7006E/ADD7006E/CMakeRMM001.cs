using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRMM001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RMM001_LIST.Clear();

            if (p_dsdata.IOFG != "2") return;

            // 입원처방
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT V20.ODT,V20.OCD,V20.DQTY,V20.ORDCNT,V20.DUNIT,V20.DODT,V20.DOHR,V20.DOMN";
            sql += System.Environment.NewLine + "     , V01.EXDRID";
            sql += System.Environment.NewLine + "     , V01A.FLDCD4";
            sql += System.Environment.NewLine + "     , A18.ONM,A18.PRICD";
            sql += System.Environment.NewLine + "     , A02.ISPCD";
            sql += System.Environment.NewLine + "     , A07.DPTCD,A09.INSDPTCD,A09.INSDPTCD2";
            sql += System.Environment.NewLine + "  FROM TV20 V20 INNER JOIN TV01 V01 ON V01.PID=V20.PID AND V01.BEDEDT=V20.BEDEDT AND V01.BDIV=V20.BDIV AND V01.ODT=V20.ODT AND V01.ONO=V20.ONO";
            sql += System.Environment.NewLine + "                INNER JOIN TV01A V01A ON V01A.BPID=V01.PID AND V01A.BBEDEDT=V01.BEDEDT AND V01A.BBDIV=V01.BDIV AND V01A.BODT=V01.ODT AND V01A.BONO=V01.ONO AND V01A.OCD=V20.OCD AND V01A.HDID=V01.HDID";
            sql += System.Environment.NewLine + "                LEFT JOIN TA18 A18 ON A18.OCD=V20.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=V20.OCD AND X.CREDT<=V20.ODT)";
            sql += System.Environment.NewLine + "                LEFT JOIN TA02 A02 ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=V20.ODT)";
            sql += System.Environment.NewLine + "                LEFT JOIN TA07 A07 ON A07.DRID=V01.EXDRID";
            sql += System.Environment.NewLine + "                LEFT JOIN TA09 A09 ON A09.DPTCD=A07.DPTCD";
            sql += System.Environment.NewLine + " WHERE V20.PID='" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND V20.BEDEDT='" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + "   AND V20.OCD LIKE 'M%'";
            sql += System.Environment.NewLine + "   AND V20.DSTSCD='Y'";
            sql += System.Environment.NewLine + " ORDER BY V20.PID,V20.BEDEDT,V20.BDIV,V20.ODT,V20.ONO";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataRMM001 data = new CDataRMM001();
                data.Clear();

                data.ODT = row["ODT"].ToString();
                data.OCD = row["OCD"].ToString();
                data.ONM = row["ONM"].ToString();
                data.PRICD = row["PRICD"].ToString();
                data.ISPCD = row["ISPCD"].ToString();
                data.DQTY = row["DQTY"].ToString();
                data.ORDCNT = row["ORDCNT"].ToString();
                data.DUNIT = row["DUNIT"].ToString();
                data.ODAYCNT = "1";
                data.FLDCD4 = row["FLDCD4"].ToString();
                data.DODT = row["DODT"].ToString();
                data.DOHR = row["DOHR"].ToString();
                data.DOMN = row["DOMN"].ToString();
                data.EXDRID = row["EXDRID"].ToString();
                data.DPTCD = row["DPTCD"].ToString();
                data.INSDPTCD = row["INSDPTCD"].ToString();
                data.INSDPTCD2 = row["INSDPTCD2"].ToString();

                p_dsdata.RMM001_LIST.Add(data);

                return true;
            });

            // 원외 및 퇴원약
            sql = "";
            sql += System.Environment.NewLine + "SELECT V01.ODT,V01A.OCD,V01A.OQTY,V01A.ORDCNT,V01A.OUNIT DUNIT,V01A.ODAYCNT";
            sql += System.Environment.NewLine + "     , V01.EXDRID";
            sql += System.Environment.NewLine + "     , V01A.FLDCD4";
            sql += System.Environment.NewLine + "     , A18.ONM,A18.PRICD";
            sql += System.Environment.NewLine + "     , A02.ISPCD";
            sql += System.Environment.NewLine + "     , A07.DPTCD,A09.INSDPTCD,A09.INSDPTCD2";
            sql += System.Environment.NewLine + "     , V01.OKCD, V01A.FLDCD1";
            sql += System.Environment.NewLine + "  FROM TV01 V01 INNER JOIN TV01A V01A ON V01A.BPID=V01.PID AND V01A.BBEDEDT=V01.BEDEDT AND V01A.BBDIV=V01.BDIV AND V01A.BODT=V01.ODT AND V01A.BONO=V01.ONO AND V01A.HDID=V01.HDID";
            sql += System.Environment.NewLine + "                LEFT JOIN TA18 A18 ON A18.OCD=V01A.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=V01A.OCD AND X.CREDT<=V01.ODT)";
            sql += System.Environment.NewLine + "                LEFT JOIN TA02 A02 ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=V01.ODT)";
            sql += System.Environment.NewLine + "                LEFT JOIN TA07 A07 ON A07.DRID=V01.EXDRID";
            sql += System.Environment.NewLine + "                LEFT JOIN TA09 A09 ON A09.DPTCD=A07.DPTCD";
            sql += System.Environment.NewLine + " WHERE V01.PID='" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND V01.BEDEDT='" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + "   AND V01A.OCD LIKE 'M%'";
            sql += System.Environment.NewLine + "   AND ISNULL(V01.DCFG,'') IN ('','0')";
            sql += System.Environment.NewLine + "   AND V01.ODIVCD IN ('MO','MI','MF')";
            sql += System.Environment.NewLine + "   AND (V01.OKCD='3' OR V01A.FLDCD1='99')";
            sql += System.Environment.NewLine + " ORDER BY V01A.FLDCD1,V01.PID,V01.BEDEDT,V01.BDIV,V01.ODT,V01.ONO";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataRMM001 data = new CDataRMM001();
                data.Clear();

                double oqty = 0;
                double.TryParse(row["OQTY"].ToString(), out oqty);
                double ordcnt = 0;
                double.TryParse(row["ORDCNT"].ToString(), out ordcnt);
                if (ordcnt == 0) ordcnt = 1;
                double dqty = oqty / ordcnt;

                data.ODT = row["ODT"].ToString();
                data.OCD = row["OCD"].ToString();
                data.ONM = row["ONM"].ToString();
                data.PRICD = row["PRICD"].ToString();
                data.ISPCD = row["ISPCD"].ToString();
                data.DQTY = dqty.ToString();
                data.ORDCNT = ordcnt.ToString();
                data.DUNIT = row["DUNIT"].ToString();
                data.ODAYCNT = row["ODAYCNT"].ToString();
                data.FLDCD4 = row["FLDCD4"].ToString();
                data.EXDRID = row["EXDRID"].ToString();
                data.DPTCD = row["DPTCD"].ToString();
                data.INSDPTCD = row["INSDPTCD"].ToString();
                data.INSDPTCD2 = row["INSDPTCD2"].ToString();
                data.OKCD = row["OKCD"].ToString();
                data.FLDCD1 = row["FLDCD1"].ToString();

                p_dsdata.RMM001_LIST.Add(data);

                return true;
            });

            
        }
    }
}
