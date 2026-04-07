using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeERR001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.ERR001_LIST.Clear();

            // 영상검사결과지
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT X01.DPTCD,X01.EXDRID,X01.ODT,X01.PHTDT,X01.PHTTM,X01.RPTDT,X01.RPTTM,X01.OCD,X01.RDRID1";
            sql += System.Environment.NewLine + "     , X01.ONO,X01.RPTNO,X01.BEDEDT,X01.BDIV";
            sql += System.Environment.NewLine + "     , A18.ONM";
            sql += System.Environment.NewLine + "     , A07.DRNM EXDRNM";
            sql += System.Environment.NewLine + "     , A071.DRNM RDRNM1, A071.GDRLCID";
            sql += System.Environment.NewLine + "     , A09.INSDPTCD,A09.INSDPTCD2 ";
            sql += System.Environment.NewLine + "     , X02.RPTXT";
            sql += System.Environment.NewLine + "  FROM TX01 X01 INNER JOIN TX02 X02 ON X02.RPTDT=X01.RPTDT AND X02.RPTNO=X01.RPTNO";
            sql += System.Environment.NewLine + "                LEFT JOIN TA07 A07 ON A07.DRID = X01.EXDRID ";
            sql += System.Environment.NewLine + "                LEFT JOIN TA07 A071 ON A071.DRID = X01.RDRID1 ";
            sql += System.Environment.NewLine + "                LEFT JOIN TA09 A09 ON A09.DPTCD = X01.DPTCD";
            sql += System.Environment.NewLine + "                INNER JOIN TA18 A18 ON A18.OCD = X01.OCD AND A18.CREDT = (SELECT MAX(Z.CREDT) FROM TA18 Z WHERE Z.OCD = X01.OCD AND Z.CREDT <= X01.ODT)";
            sql += System.Environment.NewLine + " WHERE X01.PID = '" + p_dsdata.PID + "' ";
            sql += System.Environment.NewLine + "   AND X01.ODT >= '" + p_dsdata.FRDT + "' ";
            sql += System.Environment.NewLine + "   AND X01.ODT <= '" + p_dsdata.TODT + "' ";
            sql += System.Environment.NewLine + "   AND ISNULL(X01.OSTSCD,'') = '5' "; // 판독결고가 있는 자료만
            sql += System.Environment.NewLine + " ORDER BY X01.ODT,X01.ONO";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataERR001 data = new CDataERR001();
                data.Clear();

                data.DPTCD = row["DPTCD"].ToString();
                data.INSDPTCD = row["INSDPTCD"].ToString();
                data.INSDPTCD2 = row["INSDPTCD2"].ToString();
                data.DRID = row["EXDRID"].ToString();
                data.DRNM = row["EXDRNM"].ToString();
                data.ODT = row["ODT"].ToString();
                data.PHTDT = row["PHTDT"].ToString(); // 검사일자
                data.PHTTM = row["PHTTM"].ToString(); // 검사시간
                data.RPTDT = row["RPTDT"].ToString(); // 판독일자
                data.RPTTM = row["RPTTM"].ToString(); // 판독시간
                data.RDRID = row["RDRID1"].ToString(); // 판독의ID
                data.RDRNM = row["RDRNM1"].ToString(); // 판독의명
                data.GDRLCID = row["GDRLCID"].ToString(); // 판독의면허번호
                data.OCD = row["OCD"].ToString(); // 처방코드
                data.ONM = row["ONM"].ToString(); // 처방명
                data.RPTXT = row["RPTXT"].ToString(); // 판독결과

                string sql2 = "";
                if (row["BDIV"].ToString() == "1")
                {
                    // 외래
                    sql2 = "";
                    sql2 += System.Environment.NewLine + "SELECT OTM";
                    sql2 += System.Environment.NewLine + "  FROM TE01";
                    sql2 += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                    sql2 += System.Environment.NewLine + "   AND ODT='" + row["ODT"].ToString() + "'";
                    sql2 += System.Environment.NewLine + "   AND ONO=" + row["ONO"].ToString() + "";

                }
                else
                {
                    // 입원
                    sql2 = "";
                    sql2 += System.Environment.NewLine + "SELECT OTM";
                    sql2 += System.Environment.NewLine + "  FROM TV01";
                    sql2 += System.Environment.NewLine + " WHERE PID='" + p_dsdata.PID + "'";
                    sql2 += System.Environment.NewLine + "   AND BEDEDT='" + row["BEDEDT"].ToString() + "'";
                    sql2 += System.Environment.NewLine + "   AND BDIV='" + row["BDIV"].ToString() + "'";
                    sql2 += System.Environment.NewLine + "   AND ODT='" + row["ODT"].ToString() + "'";
                    sql2 += System.Environment.NewLine + "   AND ONO=" + row["ONO"].ToString() + "";
                }
                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.OTM = row2["OTM"].ToString();
                    return false;
                });

                p_dsdata.ERR001_LIST.Add(data);

                return true;
            });

        }
    }
}
