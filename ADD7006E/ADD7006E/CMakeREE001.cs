using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeREE001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.REE001_LIST.Clear();

            // 응급기록자료
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT S21.PID,S21.EXDT";
            sql += System.Environment.NewLine + "  FROM TS21 S21";
            sql += System.Environment.NewLine + " WHERE S21.PID='" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND S21.EXDT>='" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + "   AND S21.EXDT<='" + p_dsdata.TODT + "'";
            sql += System.Environment.NewLine + "   AND ISNULL(S21.CCFG,'') IN ('','0')";
            sql += System.Environment.NewLine + "   AND S21.DPTCD='ER'";
            sql += System.Environment.NewLine + " ORDER BY S21.EXDT,S21.HMS";


            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataREE001 data = new CDataREE001();
                data.Clear();

                // EDIS.DBO.EMIHPTMI
                // 테이블이 있는지 검사해보자
                string objid = "";
                string sql2 = "";
                sql2 = "SELECT OBJECT_ID('EDIS..EMIHPTMI') AS OBJID";
                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    objid = row2["OBJID"].ToString();
                    return MetroLib.SqlHelper.BREAK;
                });

                if (objid != "")
                {
                    sql2 = "";
                    sql2 += System.Environment.NewLine + "SELECT A.PTMIINDT,A.PTMIINTM,A.PTMIOTDT,A.PTMIOTTM,A.PTMIKTDT,A.PTMIKTTM,A.PTMIKTS1,A.PTMIEMRT,A.PTMIINCD";
                    sql2 += System.Environment.NewLine + "  FROM EDIS.DBO.EMIHPTMI A";
                    sql2 += System.Environment.NewLine + " WHERE A.PTMIIDNO='" + row["PID"].ToString() + "'";
                    sql2 += System.Environment.NewLine + "   AND A.PTMIINDT='" + row["EXDT"].ToString() + "'";

                    MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                    {
                        data.PTMIINDT = row2["PTMIINDT"].ToString(); // 응급실 도착일자
                        data.PTMIINTM = row2["PTMIINTM"].ToString(); // 응급실 도착시간
                        data.PTMIOTDT = row2["PTMIOTDT"].ToString(); // 응급실 퇴실일자
                        data.PTMIOTTM = row2["PTMIOTTM"].ToString(); // 응급실 퇴실시간
                        data.PTMIKTDT = row2["PTMIKTDT"].ToString(); // KTAS 중증도 분류일자
                        data.PTMIKTTM = row2["PTMIKTTM"].ToString(); // KTAS 중증도 분류시간
                        data.PTMIKTS1 = row2["PTMIKTS1"].ToString(); // KTAS 중증도 등급
                        data.PTMIEMRT = row2["PTMIEMRT"].ToString(); // 퇴실형태
                        data.PTMIINCD = row2["PTMIINCD"].ToString(); // 전원요양기관기호

                        return MetroLib.SqlHelper.BREAK;
                    });
                }

                // EMR293
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT PID,BEDEDT,WDATE,SEQ,MJ_HOSO,ONSET,PI,PHX,FHX,ROS,PE,IMP,CUREPLAN,SYSDT,SYSTM,EMPID";
                sql2 += System.Environment.NewLine + "     , ALRG,ALRG_TXT,MDS_DOS,MDS_KND,MDS_KND_PRT,MDS_KND_ETC";
                sql2 += System.Environment.NewLine + "  FROM EMR293";
                sql2 += System.Environment.NewLine + " WHERE PID =	'" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND BEDEDT = '" + row["EXDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + " ORDER BY PID,BEDEDT,WDATE DESC,SEQ DESC";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.MJ_HOSO = row2["MJ_HOSO"].ToString();
                    data.ONSET = row2["ONSET"].ToString();
                    data.PI = row2["PI"].ToString();
                    data.ALRG = row2["ALRG"].ToString();
                    data.ALRG_TXT = row2["ALRG_TXT"].ToString();
                    data.PHX = row2["PHX"].ToString();
                    data.MDS_DOS = row2["MDS_DOS"].ToString();
                    data.MDS_KND = row2["MDS_KND"].ToString();
                    data.MDS_KND_ETC = row2["MDS_KND_ETC"].ToString();
                    data.FHX = row2["FHX"].ToString();
                    data.ROS = row2["ROS"].ToString();
                    data.PE = row2["PE"].ToString();

                    return MetroLib.SqlHelper.BREAK;
                });

                // TK71BU
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT DEATHDT,DEATHHHMM,DEATH_SICK_SYM,DEATH_DIAG_NM";
                sql2 += System.Environment.NewLine + "  FROM TK71BU";
                sql2 += System.Environment.NewLine + " WHERE PID =	'" + row["PID"].ToString() + "'";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.DEATHDT = row2["DEATHDT"].ToString(); // 사망일
                    data.DEATHHHMM = row2["DEATHHHMM"].ToString(); // 사망시분
                    data.DEATH_SICK_SYM = row2["DEATH_SICK_SYM"].ToString(); // 원사인 상병분류기호
                    data.DEATH_DIAG_NM = row2["DEATH_DIAG_NM"].ToString(); // 진단명

                    return MetroLib.SqlHelper.BREAK;
                });



                // TE12C
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT E12C.EXDT,E12C.C_CASE,E12C.RMK1,E12C.SYSDT,E12C.SYSTM,E12C.ENTDT,E12C.ENTTM,E12C.USERID";
                sql2 += System.Environment.NewLine + "     , A07.DRNM,A07.GDRLCID";
                sql2 += System.Environment.NewLine + "     , A09.DPTCD,A09.INSDPTCD,A09.INSDPTCD2";
                sql2 += System.Environment.NewLine + "  FROM TE12C E12C LEFT JOIN TA07 A07 ON A07.DRID=E12C.USERID";
                sql2 += System.Environment.NewLine + "                  LEFT JOIN TA09 A09 ON A09.DPTCD=A07.DPTCD";
                sql2 += System.Environment.NewLine + " WHERE E12C.PID='" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND E12C.EXDT='" + row["EXDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND E12C.C_CASE IN ('PN','S','O','A','P')";
                sql2 += System.Environment.NewLine + "   AND E12C.BDIV='3'";
                sql2 += System.Environment.NewLine + " ORDER BY E12C.EXDT,E12C.BDIV,E12C.USERID,E12C.SEQ,E12C.SORT_SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    string c_case = row2["C_CASE"].ToString();
                    string exdt = row2["EXDT"].ToString();
                    string userid = row2["USERID"].ToString();
                    string usernm = row2["DRNM"].ToString();
                    string gdrlcid = row2["GDRLCID"].ToString();
                    string sysdt = row2["SYSDT"].ToString();
                    string systm = row2["SYSTM"].ToString();
                    string entdt = row2["ENTDT"].ToString();
                    string enttm = row2["ENTTM"].ToString();
                    string rmk1 = row2["RMK1"].ToString().Trim();
                    string dptcd = row2["DPTCD"].ToString();
                    string insdptcd = row2["INSDPTCD"].ToString();
                    string insdptcd2 = row2["INSDPTCD2"].ToString();

                    bool bFind = false;
                    for(int i=0 ; i<data.EXDT.Count;i++)
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
                        data.GDRLCID.Add(gdrlcid);
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

                // TS06
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT PTYSQ,ROFG,DXD,DACD";
                sql2 += System.Environment.NewLine + "  FROM TS06";
                sql2 += System.Environment.NewLine + " WHERE PID='" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND EXDT='" + row["EXDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND DPTCD='ER'";

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
                sql2 += System.Environment.NewLine + "   AND V01.BEDEDT='" + row["EXDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND V01.BDIV='3'";
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

                p_dsdata.REE001_LIST.Add(data);

                return MetroLib.SqlHelper.BREAK;
            });

        }
    }
}
