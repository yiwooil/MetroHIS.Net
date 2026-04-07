using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    class CMakeRSS001
    {
        public void Make(CData p_dsdata, OleDbConnection p_conn)
        {
            p_dsdata.RSS001_LIST.Clear();

            // 수술기록자료
            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT U01.PID,U01.OPDT,U01.OPSEQ";
            sql += System.Environment.NewLine + "     , U01.OPSDT,U01.OPSHR,U01.OPSMN,U01.OPEDT,U01.OPEHR,U01.OPEMN,U01.DPTCD,U01.DRID,U01.STAFG";
            sql += System.Environment.NewLine + "     , U03.ANETP";
            sql += System.Environment.NewLine + "     , U90.EMPID,U90.OPDT WDATE,U90.WTIME,U90.PREDX,U90.POSDX,U90.POS,U90.LESION,U90.INDIOFSURGERY,U90.SURFNDNPRO";
            sql += System.Environment.NewLine + "     , U90.SBDR1 AS U90_SBDR1,U90.SBDR2 AS U90_SBDR2,U90.SBDR3 AS U90_SBDR3";
            sql += System.Environment.NewLine + "     , A09.INSDPTCD,A09.INSDPTCD2";
            sql += System.Environment.NewLine + "     , A07.DRNM, A07.GDRLCID";
            sql += System.Environment.NewLine + "     , A31.CDNM ANETPNM";
            sql += System.Environment.NewLine + "  FROM TU01 U01 INNER JOIN TU03 U03 ON U03.PID=U01.PID AND U03.OPDT=U01.OPDT AND U03.DPTCD=U01.DPTCD AND U03.OPSEQ=U01.OPSEQ";
            sql += System.Environment.NewLine + "                LEFT JOIN TU90 U90 ON U90.PID=U01.PID AND U90.OPDT=U01.OPDT AND U90.DPTCD=U01.DPTCD AND U90.OPSEQ=U01.OPSEQ AND U90.SEQ=U01.SEQ";
            sql += System.Environment.NewLine + "                LEFT JOIN TA09 A09 ON A09.DPTCD = U01.DPTCD";
            sql += System.Environment.NewLine + "                LEFT JOIN TA07 A07 ON A07.DRID = U01.DRID";
            sql += System.Environment.NewLine + "                LEFT JOIN TA31 A31 ON A31.MST1CD='58' AND A31.MST2CD=U03.ANETP";
            sql += System.Environment.NewLine + " WHERE U01.PID='" + p_dsdata.PID + "'";
            sql += System.Environment.NewLine + "   AND U01.OPDT>='" + p_dsdata.FRDT + "'";
            sql += System.Environment.NewLine + "   AND U01.OPDT<='" + p_dsdata.TODT + "'";
            sql += System.Environment.NewLine + "   AND ISNULL(U01.CHGDT,'')=''";
            sql += System.Environment.NewLine + "   AND ISNULL(U03.CHGDT,'')=''";
            sql += System.Environment.NewLine + "   AND U03.ANENO NOT LIKE 'ZZZZ%'";
            sql += System.Environment.NewLine + " ORDER BY U01.OPDT,U01.OPSHR";


            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                CDataRSS001 data = new CDataRSS001();
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
                data.DRNM = row["DRNM"].ToString();
                data.GDRLCID = row["GDRLCID"].ToString(); // 수술의면허번호

                data.DR_GUBUN_SUB1 = ""; // 2.보조의
                data.DPTCD_SUB1 = "";
                data.INSDPTCD_SUB1 = "";
                data.INSDPTCD2_SUB1 = "";
                data.DRID_SUB1 = row["U90_SBDR1"].ToString();
                data.DRNM_SUB1 = "";
                data.GDRLCID_SUB1 = "";

                data.DR_GUBUN_SUB2 = ""; // 2.보조의
                data.DPTCD_SUB2 = "";
                data.INSDPTCD_SUB2 = "";
                data.INSDPTCD2_SUB2 = "";
                data.DRID_SUB2 = row["U90_SBDR2"].ToString();
                data.DRNM_SUB2 = "";
                data.GDRLCID_SUB2 = "";

                data.DR_GUBUN_SUB3 = ""; // 2.보조의
                data.DPTCD_SUB3 = "";
                data.INSDPTCD_SUB3 = "";
                data.INSDPTCD2_SUB3 = "";
                data.DRID_SUB3 = row["U90_SBDR3"].ToString();
                data.DRNM_SUB3 = "";
                data.GDRLCID_SUB3 = "";

                data.STAFG = row["STAFG"].ToString(); // 응급여부(0.정규 1.초응급 2.중응급 3.응급)

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

                // TU03
                data.ANETP = row["ANETP"].ToString(); // 마취종류
                data.ANETPNM = row["ANETPNM"].ToString(); // 마취종류명칭
                // TU90
                data.EMPID = row["EMPID"].ToString(); // 작성의사
                data.EMPNM = CUtil.GetEmpnm(data.EMPID, p_conn); // 작성의사명
                data.WDATE = row["WDATE"].ToString(); // 작성일자
                data.WTIME = row["WTIME"].ToString(); // 작성시간
                data.PREDX = row["PREDX"].ToString(); // 수술전진단
                data.POSDX = row["POSDX"].ToString(); // 수술후진단
                data.LESION = row["LESION"].ToString(); // 병병의 위치
                data.POS = row["POS"].ToString(); // 수술체위
                data.INDIOFSURGERY = row["INDIOFSURGERY"].ToString(); // 수술소견
                data.SURFNDNPRO = row["SURFNDNPRO"].ToString(); // 수술절차
                data.REMARK = "-"; // 특이사항 내용이 없으면 "-"을 기재

                if (data.DRID_SUB1 != "")
                {
                    string dptcd = "";
                    string insdptcd = "";
                    string insdptcd2 = "";
                    string drnm = "";
                    string gdrlcid = "";

                    int cnt = GetDoctorInfo(data.DPTCD_SUB1, ref dptcd, ref insdptcd, ref insdptcd2, ref drnm, ref gdrlcid, p_conn);
                    if (cnt > 0)
                    {
                        data.DR_GUBUN_SUB1 = "2"; // 2.보조의
                        data.DPTCD_SUB1 = dptcd;
                        data.INSDPTCD_SUB1 = insdptcd;
                        data.INSDPTCD2_SUB1 = insdptcd2;
                        data.DRNM_SUB1 = drnm;
                        data.GDRLCID_SUB1 = gdrlcid; // 수술의면허번호
                    }
                }
                if (data.DRID_SUB2 != "")
                {
                    string dptcd = "";
                    string insdptcd = "";
                    string insdptcd2 = "";
                    string drnm = "";
                    string gdrlcid = "";

                    int cnt = GetDoctorInfo(data.DPTCD_SUB2, ref dptcd, ref insdptcd, ref insdptcd2, ref drnm, ref gdrlcid, p_conn);
                    if (cnt > 0)
                    {
                        data.DR_GUBUN_SUB2 = "2"; // 2.보조의
                        data.DPTCD_SUB2 = dptcd;
                        data.INSDPTCD_SUB2 = insdptcd;
                        data.INSDPTCD2_SUB2 = insdptcd2;
                        data.DRNM_SUB2 = drnm;
                        data.GDRLCID_SUB2 = gdrlcid; // 수술의면허번호
                    }
                }
                if (data.DRID_SUB3 != "")
                {
                    string dptcd = "";
                    string insdptcd = "";
                    string insdptcd2 = "";
                    string drnm = "";
                    string gdrlcid = "";

                    int cnt = GetDoctorInfo(data.DPTCD_SUB3, ref dptcd, ref insdptcd, ref insdptcd2, ref drnm, ref gdrlcid, p_conn);
                    if (cnt > 0)
                    {
                        data.DR_GUBUN_SUB3 = "2"; // 2.보조의
                        data.DPTCD_SUB3 = dptcd;
                        data.INSDPTCD_SUB3 = insdptcd;
                        data.INSDPTCD2_SUB3 = insdptcd2;
                        data.DRNM_SUB3 = drnm;
                        data.GDRLCID_SUB3 = gdrlcid; // 수술의면허번호
                    }
                }

                p_dsdata.RSS001_LIST.Add(data);

                return MetroLib.SqlHelper.CONTINUE;
            });

        }

        private int GetDoctorInfo(string p_drid, ref string p_dptcd, ref string p_insdptcd, ref string p_insdptcd2, ref string p_drnm, ref string p_gdrlcid, OleDbConnection p_conn)
        {
            int count = 0;
            string dptcd = "";
            string insdptcd = "";
            string insdptcd2 = "";
            string drnm = "";
            string gdrlcid = "";

            string sql = "";
            sql = "";
            sql += System.Environment.NewLine + "SELECT A07.DPTCD,A07.DRNM,A07.GDRLCID";
            sql += System.Environment.NewLine + "     , A09.INSDPTCD,A09.INSDPTCD2";
            sql += System.Environment.NewLine + "  FROM TA07 A07 INNER JOIN TA09 A09 ON A09.DPTCD=A07,DPTCD";
            sql += System.Environment.NewLine + " WHERE A07.DRID='" + p_drid + "'";
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                count++;
                dptcd = row["DPTCD"].ToString();
                insdptcd = row["INSDPTCD"].ToString();
                insdptcd2 = row["INSDPTCDCD2"].ToString();
                drnm = row["DRNM"].ToString();
                gdrlcid = row["GDRLCID"].ToString();

                return MetroLib.SqlHelper.BREAK;
            });

            p_dptcd = dptcd;
            p_insdptcd = insdptcd;
            p_insdptcd2 = insdptcd2;
            p_drnm = drnm;
            p_gdrlcid = gdrlcid;

            return count;
        }
    }
}
