using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRIZ001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RIZ001_LIST.Clear();

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


            // 전출기록지(TK71HM)
            sql = "";
            sql += System.Environment.NewLine + "SELECT HM.TDATE, HM.DRID AS TDRID, HM.DRID AS EMPID, HM.SYSDT, HM.SYSTM, HM.CHKTREA, HM.STATUS, HM.STATUS, HM.OPNAME, HM.DXDNM, HM.TXTRESULT";
            sql += System.Environment.NewLine + "     , A07.DRNM AS TDRNM, A07.DPTCD AS HM_DPTCD";
            sql += System.Environment.NewLine + "     , A09.INSDPTCD AS HM_INSDPTCD, A09.INSDPTCD2 AS HM_INSDPTCD2";
            sql += System.Environment.NewLine + "  FROM TK71HM HM LEFT JOIN TA07 A07 ON A07.DRID=HM.DRID";
            sql += System.Environment.NewLine + "                 LEFT JOIN TA09 A09 ON A09.DPTCD=A07.DPTCD";
            sql += System.Environment.NewLine + " WHERE HM.PID='" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND HM.TDATE>='" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + "   AND HM.TDATE<='" + p_dsdata.TODT + "'";
            sql += System.Environment.NewLine + "   AND ISNULL(HM.CANCEL,'')=''";
            sql += System.Environment.NewLine + " ORDER BY HM.TDATE";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                string opname = row["OPNAME"].ToString();
                string dxdnm = row["DXDNM"].ToString();
                string[] ary_opname = opname.Split(new[] { "\r", "\n", "\r\n" }, StringSplitOptions.None);
                string[] ary_dxdnm = dxdnm.Split(new[] { "\r", "\n", "\r\n" }, StringSplitOptions.None);

                CDataRIZ001 data = new CDataRIZ001();
                data.Clear();

                data.BEDEDT = bededt; // 입원일자
                data.BEDEHM = bedehm; // 입원시간
                data.DPTCD = dptcd; // 입원과
                data.INSDPTCD = insdptcd; // 입원 진료과목
                data.INSDPTCD2 = insdptcd2; // 입원 내과세부진료과목

                data.TDATE = row["TDATE"].ToString(); // 전과일
                data.HM_DPTCD = row["HM_DPTCD"].ToString(); // 전출과
                data.HM_INSDPTCD = row["HM_INSDPTCD"].ToString(); // 전출 진료과목
                data.HM_INSDPTCD2 = row["HM_INSDPTCD2"].ToString(); // 전출 내과세부진료과목
                data.GR_DPTCD = ""; // 전입과
                data.GR_INSDPTCD = ""; // 전입 진료과목
                data.GR_INSDPTCD2 = ""; // 전입 내과세부진료과목
                data.TDRID = row["TDRID"].ToString(); // 담당의사(환자를 보내는 과의 담당의사)
                data.TDRNM = row["TDRNM"].ToString(); // 담당의명(환자를 보내는 과의 담당의사)
                data.EMPID = row["EMPID"].ToString(); // 작성자
                data.EMPNM = CUtil.GetEmpnm(data.EMPID, p_conn); // 작성자성명
                data.SYSDT = row["SYSDT"].ToString(); // 작성일자
                data.SYSTM = row["SYSTM"].ToString(); // 작성시간
                data.CHKTREA = row["CHKTREA"].ToString(); // 전과사유
                data.STATUS = row["STATUS"].ToString(); // 치료경과 및 환자상태
                data.TXTRESULT = row["TXTRESULT"].ToString(); // 주요검사결과
                foreach (string str in ary_opname)
                {
                    data.OPDTM.Add(str.Substring(0, 10).Replace("-", "") + "0000"); // 처치 및 수술 시행일시
                    data.OPNAME.Add(str.Substring(11)); // 처치 및 수술명
                }
                foreach (string str in ary_dxdnm)
                {
                    data.DXDNM.Add(str); // 진단명
                }

                string sql2 = "";
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT A07.DPTCD AS GR_DPTCD";
                sql2 += System.Environment.NewLine + "     , A09.INSDPTCD AS GR_INSDPTCD, A09.INSDPTCD2 AS GR_INSDPTCD2";
                sql2 += System.Environment.NewLine + "  FROM TK71GR GR LEFT JOIN TA07 A07 ON A07.DRID=GR.TDRID";
                sql2 += System.Environment.NewLine + "                 LEFT JOIN TA09 A09 ON A09.DPTCD=A07.DPTCD";
                sql2 += System.Environment.NewLine + " WHERE GR.PID='" + p_dsdata.PID + "'";
                sql2 += System.Environment.NewLine + "   AND GR.TDATE='" + row["TDATE"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(GR.CANCEL,'')=''";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.GR_DPTCD = row2["GR_DPTCD"].ToString(); // 전출과
                    data.GR_INSDPTCD = row2["GR_INSDPTCD"].ToString(); // 전출 진료과목
                    data.GR_INSDPTCD2 = row2["GR_INSDPTCD2"].ToString(); // 전출 내과세부진료과목
                    return false;
                });


                p_dsdata.RIZ001_LIST.Add(data);

                return true;
            });

        }
    }
}
