using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRTT001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RTT001_LIST.Clear();

            // 수술기록자료
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT U01.PID,U01.OPDT,U01.OPSEQ,U01.OPSDT,U01.OPSHR,U01.OPSMN,U01.OPEDT,U01.OPEHR,U01.OPEMN,U01.DPTCD,U01.DRID";
            sql += System.Environment.NewLine + "     , A09.INSDPTCD,A09.INSDPTCD2";
            sql += System.Environment.NewLine + "  FROM TU01 U01 LEFT JOIN TA09 A09 ON A09.DPTCD = U01.DPTCD";
            sql += System.Environment.NewLine + " WHERE U01.PID='" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND U01.OPDT>='" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + "   AND U01.OPDT<='" + p_dsdata.TODT + "'";
            sql += System.Environment.NewLine + "   AND ISNULL(U01.CHGDT,'')=''";
            sql += System.Environment.NewLine + "   AND ISNULL(U01.SISUL_FG,'')='1'";
            sql += System.Environment.NewLine + " ORDER BY U01.OPDT,U01.OPSHR";


            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataRTT001 data = new CDataRTT001();
                data.Clear();

                // TU01
                data.OPSDT = row["OPSDT"].ToString();
                data.OPSHR = row["OPSHR"].ToString();
                data.OPSMN = row["OPSMN"].ToString();
                data.OPEDT = row["OPEDT"].ToString();
                data.OPEHR = row["OPEHR"].ToString();
                data.OPEMN = row["OPEMN"].ToString();

                data.DR_GUBUN = "1"; // 1.집도의
                data.DPTCD = row["DPTCD"].ToString();
                data.INSDPTCD = row["INSDPTCD"].ToString();
                data.INSDPTCD2 = row["INSDPTCD2"].ToString();
                data.DRID = row["DRID"].ToString();
                data.DRNM = CUtil.GetEmpnm(data.DRID, p_conn);

                string sql2 = "";
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT U02.OCD,U02.OPDT";
                sql2 += System.Environment.NewLine + "     , A18.ONM";
                sql2 += System.Environment.NewLine + "     , A02.ISPCD";
                sql2 += System.Environment.NewLine + "  FROM TU02 U02 INNER JOIN TA18 A18 ON A18.OCD=U02.OCD AND A18.CREDT=(SELECT MAX(X.CREDT) FROM TA18 X WHERE X.OCD=U02.OCD AND X.CREDT<=U02.OPDT)";
                sql2 += System.Environment.NewLine + "                INNER JOIN TA02 A02 ON A02.PRICD=A18.PRICD AND A02.CREDT=(SELECT MAX(X.CREDT) FROM TA02 X WHERE X.PRICD=A18.PRICD AND X.CREDT<=U02.OPDT)";
                sql2 += System.Environment.NewLine + " WHERE U02.PID='" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND U02.OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND U02.DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND U02.OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND ISNULL(U02.CHGDT,'')=''";
                sql2 += System.Environment.NewLine + " ORDER BY U02.OCD,U02.SEQ";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.ONM.Add(row2["ONM"].ToString());
                    data.ISPCD.Add(row2["ISPCD"].ToString());

                    return MetroLib.SqlHelper.CONTINUE;
                });

                // TU90
                sql2 = "";
                sql2 += System.Environment.NewLine + "SELECT EMPID,WDATE,WTIME,PREDX,POSDX,SURFNDNPRO,RMKONAPP";
                sql2 += System.Environment.NewLine + "  FROM TU90";
                sql2 += System.Environment.NewLine + " WHERE PID='" + row["PID"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND OPDT='" + row["OPDT"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND DPTCD='" + row["DPTCD"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND OPSEQ='" + row["OPSEQ"].ToString() + "'";
                sql2 += System.Environment.NewLine + "   AND SEQ='" + row["SEQ"].ToString() + "'";

                MetroLib.SqlHelper.GetDataRow(sql2, p_conn, delegate(DataRow row2)
                {
                    data.EMPID = row2["EMPID"].ToString(); // 작성의사
                    data.EMPNM = CUtil.GetEmpnm(data.EMPID, p_conn); // 작성의사명
                    data.WDATE = row2["WDATE"].ToString(); // 작성일자
                    data.WTIME = row2["WTIME"].ToString(); // 작성시간
                    data.PREDX = row2["PREDX"].ToString(); // 수술전진단
                    data.POSDX = row2["POSDX"].ToString(); // 수술후진단
                    data.SURFNDNPRO = row2["SURFNDNPRO"].ToString(); // 수술절차
                    data.RMKONAPP = row2["RMKONAPP"].ToString(); // 특이사항

                    return MetroLib.SqlHelper.CONTINUE;
                });


                p_dsdata.RTT001_LIST.Add(data);

                return MetroLib.SqlHelper.CONTINUE;
            });

        }

    }
}
