using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRII001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RII001_LIST.Clear();

            if (p_dsdata.IOFG != "2") return;

            // 입원마스터
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT A04.PID,A04.BEDEDT,A04.BEDEHM,A04.BEDIPTHCD";
            sql += System.Environment.NewLine + "  FROM TA04 A04";
            sql += System.Environment.NewLine + " WHERE A04.PID='" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND A04.BEDEDT='" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + " ORDER BY A04.BEDEDT,A04.BEDEHM";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataRII001 data = new CDataRII001();
                data.Clear();

                // TA04
                data.BEDEDT = row["BEDEDT"].ToString();
                data.BEDEHM = row["BEDEHM"].ToString();
                data.BEDIPTHCD = row["BEDIPTHCD"].ToString();


                // TT04
                string sql2 = "";
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT T04.DPTC2,T04.PDR2";
                sql2 += System.Environment.NewLine + "     , A07.DRNM";
                sql2 += System.Environment.NewLine + "     , A09.DPTNM,A09.INSDPTCD,A09.INSDPTCD2";
                sql2 += System.Environment.NewLine + "  FROM TT04 T04 LEFT JOIN TA07 A07 ON A07.DRID=T04.PDR2";
                sql2 += System.Environment.NewLine + "                LEFT JOIN TA09 A09 ON A09.DPTCD=T04.DPTC2";
                sql2 += System.Environment.NewLine + " WHERE T04.PID='" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND T04.BDEDT='" + row["BEDEDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + " ORDER BY T04.CRDHM";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.DPTCD = row2["DPTC2"].ToString();
                    data.INSDPTCD = row2["INSDPTCD"].ToString();
                    data.INSDPTCD2 = row2["INSDPTCD2"].ToString();
                    data.DRID = row2["PDR2"].ToString();
                    data.DRNM = row2["DRNM"].ToString();

                    return false;
                });

                // EMR290
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT PID,BEDEDT,WDATE,SEQ,MJ_HOSO,ONSET,PI,PHX,FHX,ROS,PE,IMP,CUREPLAN,SYSDT,SYSTM,EMPID";
                sql2 += System.Environment.NewLine + "     , ALRG,ALRG_TXT,MDS_DOS,MDS_KND,MDS_KND_PRT,MDS_KND_ETC,PRBM_LIST";
                sql2 += System.Environment.NewLine + "  FROM EMR290";
                sql2 += System.Environment.NewLine + " WHERE PID =	'" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND BEDEDT = '" + row["BEDEDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + " ORDER BY PID,BEDEDT,WDATE DESC,SEQ DESC";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.CC = row2["MJ_HOSO"].ToString();
                    data.CC_DATE = row2["ONSET"].ToString();
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
                    data.PRBM_LIST = row2["PRBM_LIST"].ToString();
                    data.IMP = row2["IMP"].ToString();
                    data.TXPLAN = row2["CUREPLAN"].ToString();
                    data.SYSDT = row2["SYSDT"].ToString();
                    data.SYSTM = row2["SYSTM"].ToString();
                    data.EMPID = row2["EMPID"].ToString();
                    data.EMPNM = GetEmpnm(p_conn, data.EMPID);

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


                p_dsdata.RII001_LIST.Add(data);

                return MetroLib.SqlHelper.CONTINUE;
            });

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
