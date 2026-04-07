using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeROP001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.ROP001_LIST.Clear();

            if (p_dsdata.IOFG != "1") return;

            // 외래접수내역
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT S21.PID,S21.EXDT,S21.HMS,S21.DPTCD,S21.DRID";
            sql += System.Environment.NewLine + "     , A07.DRNM";
            sql += System.Environment.NewLine + "     , A09.INSDPTCD,A09.INSDPTCD2";
            sql += System.Environment.NewLine + "  FROM TS21 S21 LEFT JOIN TA07 A07 ON A07.DRID=S21.DRID";
            sql += System.Environment.NewLine + "                LEFT JOIN TA09 A09 ON A09.DPTCD=S21.DPTCD";
            sql += System.Environment.NewLine + " WHERE S21.PID='" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND S21.EXDT='" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + "   AND S21.DPTCD='" + p_dsdata.DPTCD + "'";
            sql += System.Environment.NewLine + "   AND ISNULL(S21.CCFG,'') IN ('','0')";
            sql += System.Environment.NewLine + " ORDER BY S21.EXDT,S21.HMS DESC";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataROP001 data = new CDataROP001();
                data.Clear();

                // TS21
                data.EXDT = row["EXDT"].ToString();
                data.HMS = row["HMS"].ToString();
                data.DRID = row["DRID"].ToString();
                data.DRNM = row["DRNM"].ToString();
                data.DPTCD = row["DPTCD"].ToString();
                data.INSDPTCD = row["INSDPTCD"].ToString();
                data.INSDPTCD2 = row["INSDPTCD2"].ToString();

                // TE12H_AL
                string sql2 = "";
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT ALLERGY_DRUGCHK,ALLERGY_DRUG";
                sql2 += System.Environment.NewLine + "  FROM TE12H_AL";
                sql2 += System.Environment.NewLine + " WHERE PID =	'" + row["PID"].ToString() + "'";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.ALLERGY_DRUGCHK = row2["ALLERGY_DRUGCHK"].ToString();
                    data.ALLERGY_DRUG = row2["ALLERGY_DRUG"].ToString();

                    return MetroLib.SqlHelper.BREAK;
                });

                // TS06
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT PTYSQ,ROFG,DACD,DXD";
                sql2 += System.Environment.NewLine + "  FROM TS06";
                sql2 += System.Environment.NewLine + " WHERE PID =	'" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND EXDT = '" + row["EXDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND DPTCD ='" + p_dsdata.DPTCD + "'";
                sql2 += System.Environment.NewLine + " ORDER BY PTYSQ,DPTCD,SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.PTYSQ.Add(row2["PTYSQ"].ToString());
                    data.ROFG.Add(row2["ROFG"].ToString());
                    data.DACD.Add(row2["DACD"].ToString());
                    data.DXD.Add(row2["DXD"].ToString());

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // TE01
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT E01.ODT,E01.OTM";
                sql2 += System.Environment.NewLine + "     , E01A.OCD";
                sql2 += System.Environment.NewLine + "     , A18.ONM";
                sql2 += System.Environment.NewLine + "  FROM TE01 E01 INNER JOIN TE01A E01A ON E01A.HDID=E01.HDID";
                sql2 += System.Environment.NewLine + "                INNER JOIN TA18 A18 ON A18.OCD = E01A.OCD AND A18.CREDT = (SELECT MAX(Z.CREDT) FROM TA18 Z WHERE Z.OCD = E01A.OCD AND Z.CREDT <= E01.ODT)";
                sql2 += System.Environment.NewLine + " WHERE E01.PID='" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND E01.EXDT='" + row["EXDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND E01.ODIVCD='T'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(E01.DCFG,'') IN ('','0')";
                sql2 += System.Environment.NewLine + " ORDER BY E01.ODT,E01.ONO";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.ODT.Add(row2["ODT"].ToString()); // 시행일자
                    data.OTM.Add(row2["OTM"].ToString()); // 시행시간
                    data.ONM.Add(row2["ONM"].ToString()); // 시술.처치 및 수술명(TA18)

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // TE12C
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT E12C.PID,E12C.BDIV,E12C.EXDT,E12C.USERID,E12C.SEQ,E12C.SORT_SEQ,E12C.C_CASE,E12C.SYSDT,E12C.SYSTM,E12C.RMK1";
                sql2 += System.Environment.NewLine + "     , A07.DRNM";
                sql2 += System.Environment.NewLine + "  FROM TE12C E12C LEFT JOIN TA07 A07 ON A07.DRID=E12C.USERID";
                sql2 += System.Environment.NewLine + " WHERE E12C.PID =	'" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND E12C.BEDEDT = '" + row["EXDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + " ORDER BY E12C.PID,E12C.BEDEDT,E12C.EXDT,E12C.BDIV,E12C.USERID,E12C.SEQ,E12C.SORT_SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    string c_case = row2["C_CASE"].ToString();

                    if ("PN".Equals(c_case, StringComparison.CurrentCultureIgnoreCase)||
                        "S".Equals(c_case, StringComparison.CurrentCultureIgnoreCase)||
                        "O".Equals(c_case, StringComparison.CurrentCultureIgnoreCase)||
                        "A".Equals(c_case, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (data.EMPID == "") data.EMPID = row2["USERID"].ToString();
                        if (data.EMPNM == "") data.EMPNM = row2["DRNM"].ToString();
                        if (data.SYSDT == "") data.SYSDT = row2["SYSDT"].ToString();
                        if (data.SYSTM == "") data.SYSTM = row2["SYSTM"].ToString();

                        if (data.PN == "") data.PN = row2["RMK1"].ToString().Trim();
                        else data.PN += Environment.NewLine + row2["RMK1"].ToString().Trim();
                    }
                    else if ("P".Equals(c_case, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (data.EMPID == "") data.EMPID = row2["USERID"].ToString();
                        if (data.EMPNM == "") data.EMPNM = row2["DRNM"].ToString();
                        if (data.SYSDT == "") data.SYSDT = row2["SYSDT"].ToString();
                        if (data.SYSTM == "") data.SYSTM = row2["SYSTM"].ToString();

                        if (data.TXPLAN == "") data.TXPLAN = row2["RMK1"].ToString().Trim();
                        else data.TXPLAN += Environment.NewLine + row2["RMK1"].ToString().Trim();
                    }
                    return MetroLib.SqlHelper.CONTINUE;
                });


                p_dsdata.ROP001_LIST.Add(data);

                return false;
            });
        }
    }
}
