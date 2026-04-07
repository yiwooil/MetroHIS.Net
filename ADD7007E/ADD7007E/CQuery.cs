using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    class CQuery
    {
        protected Dictionary<string, string> m_dic_cnectdd = new Dictionary<string, string>();
        protected Dictionary<string, string> m_dic_dcount = new Dictionary<string, string>();
        protected Dictionary<string, string> m_dic_billsno = new Dictionary<string, string>();
        protected Dictionary<string, string> m_dic_cnecno = new Dictionary<string, string>();

        protected void SetData(CData data, DataRow row, OleDbConnection conn, ref int no, string p_iofg, string fr_date, string to_date)
        {
            string fBDODT = "BDODT";
            if (p_iofg == "1") fBDODT = "EXDATE";

            data.KEYSTR = row[fBDODT].ToString() + "," + row["QFYCD"].ToString() + "," + row["JRBY"].ToString() + "," + row["PID"].ToString() + "," + row["UNISQ"].ToString() + "," + row["SIMCS"].ToString();
            data.SEQ = 1;

            data.SEL = true;
            data.NO = (++no);
            data.IOFG = p_iofg;
            data.PID = row["PID"].ToString();
            data.PNM = row["PNM"].ToString();
            data.RESID = row["RESID"].ToString();
            data.BDEDT = row["BDEDT"].ToString();
            data.QFYCD = row["QFYCD"].ToString();
            data.GONSGB = row["GONSGB"].ToString();
            data.DACD = row["DACD"].ToString();
            data.DEMNO = row["DEMNO"].ToString();
            data.EPRTNO = row["EPRTNO"].ToString();

            data.BDODT = row[fBDODT].ToString();
            data.JRBY = row["JRBY"].ToString();
            data.UNISQ = row["UNISQ"].ToString();
            data.SIMCS = row["SIMCS"].ToString();
            data.STEDT = row["STEDT"].ToString();

            string a04_bedehm = "";
            string a04_bedodt = "";
            string a04_bedohm = "";
            string a04_bedodiv = "";
            if (p_iofg == "2")
            {
                GetA04(data.PID, data.BDEDT, conn, ref a04_bedehm, ref a04_bedodt, ref a04_bedohm, ref a04_bedodiv);
            }

            data.A04_BEDEHM = a04_bedehm;
            data.A04_BEDODT = a04_bedodt;
            data.A04_BEDOHM = a04_bedohm;
            data.A04_BEDODIV = a04_bedodiv;

            data.FR_DATE = fr_date;
            data.TO_DATE = to_date;

            string cnectdd = "";
            string dcount = "";
            string billsno = "";
            string cnecno = "";

            if (data.DEMNO != "")
            {
                GetCnecno(data.DEMNO, conn, ref cnecno, ref cnectdd, ref dcount, ref billsno); // 접수번호를 가져온다.
            }

            data.CNECNO = cnecno; // 접수번호
            data.CNECTDD = cnectdd; // 접수년도
            data.BILLSNO = billsno; // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)
        }

        private void GetCnecno(string p_demno, OleDbConnection p_conn, ref string p_cnecno, ref string p_cnectdd, ref string p_dcount, ref string p_billsno)
        {
            // 저방해 놓은 값을 반환한다.
            if (m_dic_cnecno.ContainsKey(p_demno) == true)
            {
                p_cnectdd = m_dic_cnectdd[p_demno];
                p_dcount = m_dic_dcount[p_demno];
                p_billsno = m_dic_billsno[p_demno];
                p_cnecno = m_dic_cnecno[p_demno];

                return;
            }

            string qfycd = "";
            string addz1 = "";
            string addz2 = "";
            {
                int cnt = 0;
                // 청구번호가 입원인지 검사. QFYCD,ADDZ1,ADDZ2를 구한다.
                string sql = "SELECT * FROM TI2A WHERE DEMNO='" + p_demno + "'";
                MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
                {
                    cnt++;
                    //iofg = "2";
                    qfycd = reader["QFYCD"].ToString();
                    addz1 = reader["ADDZ1"].ToString();
                    addz2 = reader["ADDZ2"].ToString();
                    return false;
                });
                if (cnt < 1)
                {
                    // 청구번호가 외래인지 검사. QFYCD,ADDZ1,ADDZ2를 구한다.
                    sql = "SELECT * FROM TI1A WHERE DEMNO='" + p_demno + "'";
                    MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
                    {
                        cnt++;
                        //iofg = "1";
                        qfycd = reader["QFYCD"].ToString();
                        addz1 = reader["ADDZ1"].ToString();
                        addz2 = reader["ADDZ2"].ToString();
                        return false;
                    });
                }

            }

            string cnecno = "";
            string cnectdd = "";
            string dcount = "";

            // 접수증을 읽는다.
            {
                String sql = "";
                sql = "";
                sql += System.Environment.NewLine + "SELECT * ";
                sql += System.Environment.NewLine + "  FROM TIE_F0102 A";
                sql += System.Environment.NewLine + " WHERE ISNULL(A.DEMNO,'')='" + p_demno + "'";
                sql += System.Environment.NewLine + "   AND ISNULL(A.CNECTNO,'')<>''"; // 접수번호가 있는 자료만
                sql += System.Environment.NewLine + " ORDER BY CNECTDD DESC";

                MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
                {
                    cnecno = reader["CNECTNO"].ToString(); // 접수번호
                    cnectdd = reader["CNECTDD"].ToString(); // 접수일자
                    return MetroLib.SqlHelper.BREAK;
                });
                // 2024.11.05 WOOIL - 접수증에 없으면 심결에서
                //                    심결에는 접수일자가 없다.
                if (cnecno == "")
                {
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT * ";
                    sql += System.Environment.NewLine + "  FROM TIE_F0201_062 A";
                    sql += System.Environment.NewLine + " WHERE ISNULL(A.DEMNO,'')='" + p_demno + "'";
                    sql += System.Environment.NewLine + "   AND ISNULL(A.CNECNO,'')<>''"; // 접수번호가 있는 자료만
                    MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
                    {
                        cnecno = reader["CNECNO"].ToString(); // 접수번호
                        return MetroLib.SqlHelper.BREAK;
                    });
                }
            }
            p_cnectdd = cnectdd;

            // 보완청구이면
            if ("1".Equals(addz1))
            {
                String sql = "";
                if (qfycd.StartsWith("3"))
                {
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT A.CNECNO,A.DCOUNT ";
                    sql += System.Environment.NewLine + "  FROM TIE_F0601_062 A";
                    sql += System.Environment.NewLine + " WHERE ISNULL(A.CNECNO,'')='" + addz2 + "'";
                    sql += System.Environment.NewLine + " ORDER BY A.CNECNO";
                }
                else
                {
                    sql = "";
                    sql += System.Environment.NewLine + "SELECT A.CNECNO,A.DCOUNT ";
                    sql += System.Environment.NewLine + "  FROM TIE_F0201_062 A";
                    sql += System.Environment.NewLine + " WHERE ISNULL(A.CNECNO,'')='" + addz2 + "'";
                    sql += System.Environment.NewLine + " ORDER BY A.CNECNO";
                }
                MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
                {
                    dcount = reader["DCOUNT"].ToString(); // 청구서 일련번호
                    return false;
                });
            }
            p_dcount = dcount.Replace(" ", "");

            if (cnecno == "")
            {
                cnecno = "0000000";
                p_billsno = "0"; // 청구서일련번호(접수번호 부여 전인 경우 0, 원청구에 대한 제출인 경우 1, 보완청구이면 심결에 통보된 번호)
            }
            else
            {
                p_billsno = dcount == "" ? "1" : p_dcount;
            }
            p_cnecno = cnecno;

            if (p_cnectdd == "")
            {
                p_cnectdd = p_demno; // 접수년도만 사용할 것이므로 청구번호로 만든다.
            }

            // 이후에 사용하기 위해 저장해 놓는다.
            m_dic_cnectdd.Add(p_demno, p_cnectdd);
            m_dic_dcount.Add(p_demno, p_dcount);
            m_dic_billsno.Add(p_demno, p_billsno);
            m_dic_cnecno.Add(p_demno, cnecno);
        }

        private void GetA04(string p_pid, string p_bededt, OleDbConnection p_conn, ref string p_bedehm, ref string p_bedodt, ref string p_bedohm, ref string p_bedodiv)
        {
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TA04";
            sql += Environment.NewLine + " WHERE PID='" + p_pid + "'";
            sql += Environment.NewLine + "   AND BEDEDT='" + p_bededt + "'";

            string bedehm = "";
            string bedodt = "";
            string bedohm = "";
            string bedodiv = "";
            MetroLib.SqlHelper.GetDataReader(sql, p_conn, delegate(OleDbDataReader reader)
            {
                bedehm = reader["BEDEHM"].ToString();
                bedodt = reader["BEDODT"].ToString();
                bedohm = reader["BEDOHM"].ToString();
                bedodiv = reader["BEDODIV"].ToString();
                return MetroLib.SqlHelper.BREAK;
            });

            p_bedehm = bedehm;
            p_bedodt = bedodt;
            p_bedohm = bedohm;
            p_bedodiv = bedodiv;
        }
    }
}
