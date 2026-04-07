using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    class CQuery_ASM035 : CQuery
    {
        public List<CDataASM035_003> Query_ASM035(OleDbConnection conn, string frdt, string todt)
        {
            // 마취
            m_dic_cnectdd.Clear();
            m_dic_dcount.Clear();
            m_dic_billsno.Clear();
            m_dic_cnecno.Clear();

            List<CDataASM035_003> list = new List<CDataASM035_003>();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI2A A";
            sql += Environment.NewLine + " WHERE A.BDODT>='" + frdt + "'";
            sql += Environment.NewLine + "   AND A.BDODT<='" + todt + "'";
            sql += Environment.NewLine + "   AND ISNULL(A.DEMNO,'')<>''";
            sql += Environment.NewLine + "   AND LEFT(A.QFYCD,1) IN ('2','3')";
            sql += Environment.NewLine + " ORDER BY A.PID, A.STEDT";

            int no = 0;
            MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                // 사용가능한 자료인지 점검
                if (Check_ASM035(row, conn) == true)
                {
                    CDataASM035_003 data = new CDataASM035_003();
                    data.Clear();

                    SetData(data, row, conn, ref no, "2", frdt, todt);

                    list.Add(data);
                }

                return MetroLib.SqlHelper.CONTINUE;
            });

            return list;
        }

        private bool Check_ASM035(DataRow p_row, OleDbConnection p_conn)
        {
            System.Windows.Forms.Application.DoEvents();

            // 마취
            string sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI2F I2F INNER JOIN TA02 A02 ON A02.PRICD=I2F.PRICD";
            sql += Environment.NewLine + "                                   AND A02.CREDT<=(SELECT MAX(X.CREDT)";
            sql += Environment.NewLine + "                                                     FROM TA02 X";
            sql += Environment.NewLine + "                                                    WHERE X.PRICD=I2F.PRICD";
            sql += Environment.NewLine + "                                                      AND X.CREDT<=LEFT(I2F.EXDT, 8)";
            sql += Environment.NewLine + "                                                )";
            sql += Environment.NewLine + " WHERE I2F.BDODT = '" + p_row["BDODT"].ToString() + "'";
            sql += Environment.NewLine + "   AND I2F.QFYCD = '" + p_row["QFYCD"].ToString() + "'";
            sql += Environment.NewLine + "   AND I2F.JRBY = '" + p_row["JRBY"].ToString() + "'";
            sql += Environment.NewLine + "   AND I2F.PID = '" + p_row["PID"].ToString() + "'";
            sql += Environment.NewLine + "   AND I2F.UNISQ = '" + p_row["UNISQ"].ToString() + "'";
            sql += Environment.NewLine + "   AND I2F.SIMCS = '" + p_row["SIMCS"].ToString() + "'";
            sql += Environment.NewLine + "   AND I2F.BGIHO LIKE 'L%'"; // 마취료코드
            sql += Environment.NewLine + "   AND A02.GUBUN = '1'"; // 수가만

            int no = 0;
            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                // 마취료가 있는지 찾아본다.
                no++;
                return MetroLib.SqlHelper.CONTINUE;
            });

            return (no > 0);
        }

    }
}
