using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRIY001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RIY001_LIST.Clear();

            if (p_dsdata.IOFG != "2") return;

            string bededt = "";
            string bedehm = "";
            string dptcd = "";
            string insdptcd = "";
            string insdptcd2 = "";

            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT A04.BEDEDT,A04.BEDEHM,A04.DPTCD,A09.INSDPTCD,A09.INSDPTCD2";
            sql += System.Environment.NewLine + "  FROM TA04 A04 LEFT JOIN TA09 A09 ON A09.DPTCD=A04.DPTCD";
            sql += System.Environment.NewLine + " WHERE A04.PID='" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND A04.BEDEDT='" + p_dsdata.FRDT + "'";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                bededt = row["BEDEDT"].ToString();
                bedehm = row["BEDEHM"].ToString();
                dptcd = row["DPTCD"].ToString();
                insdptcd = row["INSDPTCD"].ToString();
                insdptcd2 = row["INSDPTCD2"].ToString();

                return false;
            });


            // 전입기록지(TK71GR)
            sql = "";
            sql += System.Environment.NewLine + "SELECT GR.TDATE, GR.TDRID, GR.DRID AS EMPID, GR.SYSDT, GR.SYSTM, GR.NOWT, GR.DXD, GR.APLAN, GR.PROBLEMS";
            sql += System.Environment.NewLine + "     , A07.DRNM AS TDRNM, A07.DPTCD AS GR_DPTCD";
            sql += System.Environment.NewLine + "     , A09.INSDPTCD AS GR_INSDPTCD, A09.INSDPTCD2 AS GR_INSDPTCD2";
            sql += System.Environment.NewLine + "  FROM TK71GR GR LEFT JOIN TA07 A07 ON A07.DRID=GR.TDRID";
            sql += System.Environment.NewLine + "                 LEFT JOIN TA09 A09 ON A09.DPTCD=A07.DPTCD";
            sql += System.Environment.NewLine + " WHERE GR.PID='" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND GR.TDATE>='" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + "   AND GR.TDATE<='" + p_dsdata.TODT + "'";
            sql += System.Environment.NewLine + "   AND ISNULL(GR.CANCEL,'')=''";
            sql += System.Environment.NewLine + " ORDER BY GR.TDATE";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                string dxd = row["DXD"].ToString(); // 진단명
                string[] ary_dxd = dxd.Split(new[] { "\r", "\n", "\r\n" }, StringSplitOptions.None);

                CDataRIY001 data = new CDataRIY001();
                data.Clear();

                data.BEDEDT = bededt; // 입원일자
                data.BEDEHM = bedehm; // 입원시간
                data.DPTCD = dptcd; // 입원과
                data.INSDPTCD = insdptcd; // 입원 진료과목
                data.INSDPTCD2 = insdptcd2; // 입원 내과세부진료과목

                data.TDATE = row["TDATE"].ToString(); // 전과일
                data.GR_DPTCD = row["GR_DPTCD"].ToString(); // 전입과
                data.GR_INSDPTCD = row["GR_INSDPTCD"].ToString(); // 전입 진료과목
                data.GR_INSDPTCD2 = row["GR_INSDPTCD2"].ToString(); // 전입 내과세부진료과목
                data.HM_DPTCD = ""; // 전출과
                data.HM_INSDPTCD = ""; // 전출 진료과목
                data.HM_INSDPTCD2 = ""; // 전출 내과세부진료과목
                data.TDRID = row["TDRID"].ToString(); // 담당의사(환자를 받은과의 담당의사)
                data.TDRNM = row["TDRNM"].ToString(); // 담당의명(환자를 받은과의 담당의사)
                data.EMPID = row["EMPID"].ToString(); // 작성자
                data.EMPNM = CUtil.GetEmpnm(data.EMPID, p_conn); // 작성자성명
                data.SYSDT = row["SYSDT"].ToString(); // 작성일자
                data.SYSTM = row["SYSTM"].ToString(); // 작성시간
                data.NOWT = row["NOWT"].ToString(); // 현병력
                data.PE = ""; // 신체검진
                data.PROBLEMS = row["PROBLEMS"].ToString(); // 문제목록
                data.APLAN = row["APLAN"].ToString(); // 치료계획

                // 진단명
                foreach (string str in ary_dxd)
                {
                    data.DXD.Add(str);
                }

                string sql2 = "";
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT A07.DPTCD AS HM_DPTCD";
                sql2 += System.Environment.NewLine + "     , A09.INSDPTCD AS HM_INSDPTCD, A09.INSDPTCD2 AS HM_INSDPTCD2";
                sql2 += System.Environment.NewLine + "  FROM TK71HM HM LEFT JOIN TA07 A07 ON A07.DRID=HM.TDRID";
                sql2 += System.Environment.NewLine + "                 LEFT JOIN TA09 A09 ON A09.DPTCD=A07.DPTCD";
                sql2 += System.Environment.NewLine + " WHERE HM.PID='" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND HM.TDATE='" + row["TDATE"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(HM.CANCEL,'')=''";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2){
                    data.HM_DPTCD = row2["HM_DPTCD"].ToString(); // 전출과
                    data.HM_INSDPTCD = row2["HM_INSDPTCD"].ToString(); // 전출 진료과목
                    data.HM_INSDPTCD2 = row2["HM_INSDPTCD2"].ToString(); // 전출 내과세부진료과목

                    return MetroLib.SqlHelper.BREAK;
                });


                p_dsdata.RIY001_LIST.Add(data);

                return MetroLib.SqlHelper.CONTINUE;
            });

        }
    }
}
