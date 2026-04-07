using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRIP001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RIP001_LIST.Clear();

            if (p_dsdata.IOFG != "2") return;

            // 입원마스터
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT A04.PID,A04.BEDEDT,A04.BEDEHM,A04.DPTCD,A04.PDRID";
            sql += System.Environment.NewLine + "     , A07.DRNM";
            sql += System.Environment.NewLine + "     , A09.INSDPTCD,A09.INSDPTCD2";
            sql += System.Environment.NewLine + "  FROM TA04 A04 LEFT JOIN TA07 A07 ON A07.DRID=A04.PDRID";
            sql += System.Environment.NewLine + "                LEFT JOIN TA09 A09 ON A09.DPTCD=A04.DPTCD";
            sql += System.Environment.NewLine + " WHERE A04.PID='" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND A04.BEDEDT='" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + " ORDER BY A04.BEDEDT,A04.BEDEHM";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataRIP001 data = new CDataRIP001();
                data.Clear();

                // TA04
                data.BEDEDT = row["BEDEDT"].ToString();
                data.BEDEHM = row["BEDEHM"].ToString();
                data.DRID = row["PDRID"].ToString();
                data.DRNM = row["DRNM"].ToString();

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

                // TT05
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT PTYSQ,ROFG,DACD,DXD";
                sql2 += System.Environment.NewLine + "  FROM TT05";
                sql2 += System.Environment.NewLine + " WHERE PID =	'" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND BDEDT = '" + row["BEDEDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + " ORDER BY PTYSQ,DPTCD,SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.PTYSQ.Add(row2["PTYSQ"].ToString());
                    data.ROFG.Add(row2["ROFG"].ToString());
                    data.DXD.Add(row2["DXD"].ToString());

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // TV01
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT V01.ODT,V01.OTM";
                sql2 += System.Environment.NewLine + "     , V01A.OCD";
                sql2 += System.Environment.NewLine + "     , A18.ONM";
                sql2 += System.Environment.NewLine + "  FROM TV01 V01 INNER JOIN TV01A V01A ON V01A.HDID=V01.HDID";
                sql2 += System.Environment.NewLine + "                INNER JOIN TA18 A18 ON A18.OCD = V01A.OCD AND A18.CREDT = (SELECT MAX(Z.CREDT) FROM TA18 Z WHERE Z.OCD = V01A.OCD AND Z.CREDT <= V01.ODT)";
                sql2 += System.Environment.NewLine + " WHERE V01.PID='" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND V01.BEDEDT='" + row["BEDEDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND V01.BDIV IN ('2','3')";
                sql2 += System.Environment.NewLine + "   AND V01.ODIVCD='T'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(V01.DCFG,'') IN ('','0')";
                sql2 += System.Environment.NewLine + " ORDER BY V01.ODT,V01.ONO";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.ODT.Add(row2["ODT"].ToString()); // 시행일자
                    data.OTM.Add(row2["OTM"].ToString()); // 시행시간
                    data.ONM.Add(row2["ONM"].ToString()); // 시술.처치 및 수술명(TA18)

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // TE12C
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT E12C.PID,E12C.BEDEDT,E12C.BDIV,E12C.EXDT,E12C.USERID,E12C.SEQ,E12C.SORT_SEQ,E12C.C_CASE,E12C.SYSDT,E12C.SYSTM,E12C.ENTDT,E12C.ENTTM,E12C.RMK1";
                sql2 += System.Environment.NewLine + "     , A07.DRNM";
                sql2 += System.Environment.NewLine + "     , A09.DPTCD,A09.INSDPTCD,A09.INSDPTCD2";
                sql2 += System.Environment.NewLine + "  FROM TE12C E12C LEFT JOIN TA07 A07 ON A07.DRID=E12C.USERID";
                sql2 += System.Environment.NewLine + "                  LEFT JOIN TA09 A09 ON A09.DPTCD=A07.DPTCD";
                sql2 += System.Environment.NewLine + " WHERE E12C.PID =	'" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND E12C.BEDEDT = '" + row["BEDEDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND E12C.C_CASE IN ('PN','S','O','A','P')";
                sql2 += System.Environment.NewLine + " ORDER BY E12C.EXDT,E12C.BDIV,E12C.USERID,E12C.SEQ,E12C.SORT_SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    string c_case = row2["C_CASE"].ToString();
                    string exdt = row2["EXDT"].ToString();
                    string userid = row2["USERID"].ToString();
                    string usernm = row2["DRNM"].ToString();
                    string sysdt = row2["SYSDT"].ToString();
                    string systm = row2["SYSTM"].ToString();
                    string entdt = row2["ENTDT"].ToString();
                    string enttm = row2["ENTTM"].ToString();
                    string rmk1 = row2["RMK1"].ToString().Trim();
                    string dptcd = row2["DPTCD"].ToString();
                    string insdptcd = row2["INSDPTCD"].ToString();
                    string insdptcd2 = row2["INSDPTCD2"].ToString();

                    bool bFind = false;
                    for (int i = 0; i < data.EXDT.Count; i++)
                    {
                        if (data.EXDT[i] == exdt && data.USERID[i] == userid && data.DPTCD[i] == dptcd)
                        {
                            if ("PN".Equals(c_case, StringComparison.CurrentCultureIgnoreCase)) data.PN[i] += rmk1 + Environment.NewLine;
                            if ("S".Equals(c_case, StringComparison.CurrentCultureIgnoreCase)) data.S[i] += rmk1 + Environment.NewLine;
                            if ("O".Equals(c_case, StringComparison.CurrentCultureIgnoreCase)) data.O[i] += rmk1 + Environment.NewLine;
                            if ("A".Equals(c_case, StringComparison.CurrentCultureIgnoreCase)) data.A[i] += rmk1 + Environment.NewLine;
                            if ("P".Equals(c_case, StringComparison.CurrentCultureIgnoreCase)) data.P[i] += rmk1 + Environment.NewLine;

                            bFind = true;
                            break;
                        }
                    }
                    if (bFind == false)
                    {
                        data.EXDT.Add(exdt);
                        data.USERID.Add(userid);
                        data.USERNM.Add(usernm);
                        data.SYSDT.Add(sysdt);
                        data.SYSTM.Add(systm);
                        data.ENTDT.Add(entdt);
                        data.ENTTM.Add(enttm);
                        data.DPTCD.Add(dptcd);
                        data.INSDPTCD.Add(insdptcd);
                        data.INSDPTCD2.Add(insdptcd2);
                        data.PN.Add("");
                        data.S.Add("");
                        data.O.Add("");
                        data.A.Add("");
                        data.P.Add("");

                        if ("PN".Equals(c_case, StringComparison.CurrentCultureIgnoreCase)) data.PN[data.PN.Count - 1] = rmk1;
                        if ("S".Equals(c_case, StringComparison.CurrentCultureIgnoreCase)) data.S[data.S.Count - 1] = rmk1;
                        if ("O".Equals(c_case, StringComparison.CurrentCultureIgnoreCase)) data.O[data.O.Count - 1] = rmk1;
                        if ("A".Equals(c_case, StringComparison.CurrentCultureIgnoreCase)) data.A[data.A.Count - 1] = rmk1;
                        if ("P".Equals(c_case, StringComparison.CurrentCultureIgnoreCase)) data.P[data.P.Count - 1] = rmk1;
                    }

                    return MetroLib.SqlHelper.CONTINUE;
                });


                p_dsdata.RIP001_LIST.Add(data);

                return MetroLib.SqlHelper.CONTINUE;
            });

        }

    }
}
