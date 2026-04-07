using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeROO001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.ROO001_LIST.Clear();

            if (p_dsdata.IOFG != "1") return;

            CDataROO001 data = null;


            // EMR289
            bool bFind = false;
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT A.PID,A.EXDT,A.WDATE,A.SEQ,A.MJ_HOSO,A.ONSET,A.PI,A.PHX,A.FHX,A.ROS,A.PE,A.CUREPLAN,A.SYSDT,A.SYSTM,A.EMPID,A.DPTCD,A.DRID";
            sql += System.Environment.NewLine + "     , A.ALRG,A.ALRG_TXT,A.MDS_DOS,A.MDS_KND,A.PRBM_LIST";
            sql += System.Environment.NewLine + "     , A09.INSDPTCD,A09.INSDPTCD2";
            sql += System.Environment.NewLine + "     , A07.DRNM";
            sql += System.Environment.NewLine + "  FROM EMR289 A LEFT JOIN TA09 A09 ON A09.DPTCD=A.DPTCD";
            sql += System.Environment.NewLine + "                LEFT JOIN TA07 A07 ON A07.DRID=A.DRID";
            sql += System.Environment.NewLine + " WHERE A.PID =	'" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND A.EXDT = '" + p_dsdata.STEDT + "'";
            sql += System.Environment.NewLine + "   AND A.DPTCD = '" + p_dsdata.DPTCD + "'";
            sql += System.Environment.NewLine + " ORDER BY A.PID,A.WDATE DESC,A.SEQ DESC";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                data = new CDataROO001();
                data.Clear();

                data.DPTCD = row["DPTCD"].ToString();
                data.INSDPTCD = row["INSDPTCD"].ToString();
                data.INSDPTCD2 = row["INSDPTCD2"].ToString();

                data.DRID = row["DRID"].ToString();
                data.DRNM = row["DRNM"].ToString();

                data.EXDT = row["EXDT"].ToString();
                data.CC = row["MJ_HOSO"].ToString();
                data.CC_DATE = row["ONSET"].ToString();
                data.PI = row["PI"].ToString();
                data.ALRG = row["ALRG"].ToString();
                data.ALRG_TXT = row["ALRG_TXT"].ToString();
                data.PHX = row["PHX"].ToString();
                data.MDS_DOS = row["MDS_DOS"].ToString();
                data.MDS_KND = row["MDS_KND"].ToString();
                data.FHX = row["FHX"].ToString();
                data.ROS = row["ROS"].ToString();
                data.PE = row["PE"].ToString();
                data.PRBM_LIST = row["PRBM_LIST"].ToString();
                data.TXPLAN = row["CUREPLAN"].ToString();
                data.SYSDT = row["SYSDT"].ToString();
                data.SYSTM = row["SYSTM"].ToString();
                data.EMPID = row["EMPID"].ToString();
                data.EMPNM = GetEmpnm(p_conn, data.EMPID);

                bFind = true;
                return MetroLib.SqlHelper.BREAK;
            });


            // TE12C
            if (bFind == false)
            {
                sql = "";
                sql += System.Environment.NewLine + "SELECT PID,BEDEDT,BDIV,EXDT,USERID,SEQ,SORT_SEQ,C_CASE,SYSDT,SYSTM,RMK1";
                sql += System.Environment.NewLine + "  FROM TE12C";
                sql += System.Environment.NewLine + " WHERE PID =	'" + p_dsdata.PID + "'";
                sql += System.Environment.NewLine + "   AND BEDEDT = '" + p_dsdata.STEDT + "'";
                sql += System.Environment.NewLine + " ORDER BY PID,BEDEDT,EXDT,BDIV,USERID,SEQ,SORT_SEQ";

                MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
                {

                    string rmk1 = row["RMK1"].ToString().Trim();
                    string c_case = row["C_CASE"].ToString();

                    if ("CC".Equals(c_case, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // 주호소
                        if (data == null)
                        {
                            data = new CDataROO001();
                            data.Clear();
                        }
                        if ("".Equals(data.CC))
                        {
                            data.EMPID = row["USERID"].ToString();
                            data.EMPNM = GetEmpnm(p_conn, data.EMPID);
                            data.SYSDT = row["SYSDT"].ToString();
                            data.SYSTM = row["SYSTM"].ToString();
                            data.CC = rmk1;
                            bFind = true;
                        }
                    }
                    else if ("PI".Equals(c_case, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // 현병력
                        if (data == null)
                        {
                            data = new CDataROO001();
                            data.Clear();
                        }
                        if ("".Equals(data.PI))
                        {
                            data.EMPID = row["USERID"].ToString();
                            data.EMPNM = GetEmpnm(p_conn, data.EMPID);
                            data.SYSDT = row["SYSDT"].ToString();
                            data.SYSTM = row["SYSTM"].ToString();
                            data.PI = rmk1;
                            bFind = true;
                        }
                    }
                    else if ("PHX".Equals(c_case, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // 과거력
                        if (data == null)
                        {
                            data = new CDataROO001();
                            data.Clear();
                        }
                        if ("".Equals(data.PHX))
                        {
                            data.EMPID = row["USERID"].ToString();
                            data.EMPNM = GetEmpnm(p_conn, data.EMPID);
                            data.SYSDT = row["SYSDT"].ToString();
                            data.SYSTM = row["SYSTM"].ToString();
                            data.PHX = rmk1;
                            bFind = true;
                        }
                    }
                    else if ("FHX".Equals(c_case, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // 가족력
                        if (data == null)
                        {
                            data = new CDataROO001();
                            data.Clear();
                        }
                        if ("".Equals(data.FHX))
                        {
                            data.EMPID = row["USERID"].ToString();
                            data.EMPNM = GetEmpnm(p_conn, data.EMPID);
                            data.SYSDT = row["SYSDT"].ToString();
                            data.SYSTM = row["SYSTM"].ToString();
                            data.FHX = rmk1;
                            bFind = true;
                        }
                    }
                    else if ("PE".Equals(c_case, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // 신체검사
                        if (data == null)
                        {
                            data = new CDataROO001();
                            data.Clear();
                        }
                        if ("".Equals(data.PE))
                        {
                            data.EMPID = row["USERID"].ToString();
                            data.EMPNM = GetEmpnm(p_conn, data.EMPID);
                            data.SYSDT = row["SYSDT"].ToString();
                            data.SYSTM = row["SYSTM"].ToString();
                            data.PE = rmk1;
                            bFind = true;
                        }
                    }
                    else if ("ROS".Equals(c_case, StringComparison.CurrentCultureIgnoreCase))
                    {
                        // 계통문진
                        if (data == null)
                        {
                            data = new CDataROO001();
                            data.Clear();
                        }
                        if ("".Equals(data.ROS))
                        {
                            data.EMPID = row["USERID"].ToString();
                            data.EMPNM = GetEmpnm(p_conn, data.EMPID);
                            data.SYSDT = row["SYSDT"].ToString();
                            data.SYSTM = row["SYSTM"].ToString();
                            data.ROS = rmk1;
                            bFind = true;
                        }
                    }
                    return MetroLib.SqlHelper.CONTINUE;
                });
            }

            // TS06
            if (bFind == true)
            {
                sql = "";
                sql += System.Environment.NewLine + "SELECT PTYSQ,ROFG,DACD,DXD";
                sql += System.Environment.NewLine + "  FROM TS06";
                sql += System.Environment.NewLine + " WHERE PID =	'" + p_dsdata.PID + "'";
                sql += System.Environment.NewLine + "   AND EXDT = '" + p_dsdata.STEDT + "'";
                sql += System.Environment.NewLine + " ORDER BY PTYSQ,DPTCD,SEQ";

                MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
                {
                    data.PTYSQ.Add(row["PTYSQ"].ToString());
                    data.ROFG.Add(row["ROFG"].ToString());
                    data.DXD.Add(row["DXD"].ToString());

                    return MetroLib.SqlHelper.CONTINUE;
                });
            }

            if (data != null) p_dsdata.ROO001_LIST.Add(data);

        }

        private String GetEmpnm(OleDbConnection p_conn, String p_empid)
        {
            if ("".Equals(p_empid)) return "";

            String strRet = "";
            String sql = "";
            if (p_empid.StartsWith("AA"))
            {
                sql += System.Environment.NewLine + "SELECT A07.DRNM EMPNM ";
                sql += System.Environment.NewLine + "  FROM TA07 A07";
                sql += System.Environment.NewLine + " WHERE A07.DRID LIKE '" + p_empid + "%' ";
                sql += System.Environment.NewLine + " ORDER BY A07.DRID";
            }
            else
            {
                sql += System.Environment.NewLine + "SELECT A13.EMPNM ";
                sql += System.Environment.NewLine + "  FROM TA13 A13";
                sql += System.Environment.NewLine + " WHERE A13.EMPID='" + p_empid + "' ";
            }

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                strRet = row["EMPNM"].ToString();
                return MetroLib.SqlHelper.BREAK;
            });
            return strRet;
        }
    }
}
