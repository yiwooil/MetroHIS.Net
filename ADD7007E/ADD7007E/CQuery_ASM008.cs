using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    class CQuery_ASM008 : CQuery
    {
        public List<CDataASM008_002> Query_ASM008(OleDbConnection conn, string frdt, string todt)
        {
            m_dic_cnectdd.Clear();
            m_dic_dcount.Clear();
            m_dic_billsno.Clear();
            m_dic_cnecno.Clear();

            List<CDataASM008_002> list = new List<CDataASM008_002>();

            string sql = "";
            int no = 0;

            // 입원
            /*
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI2A A";
            sql += Environment.NewLine + " WHERE A.BDODT>='" + frdt + "'";
            sql += Environment.NewLine + "   AND A.BDODT<='" + todt + "'";
            sql += Environment.NewLine + "   AND ISNULL(A.DEMNO,'')<>''";
            sql += Environment.NewLine + "   AND LEFT(A.QFYCD,1) IN ('2','3')";
            sql += Environment.NewLine + " ORDER BY A.PID, A.STEDT";

            MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                // 사용가능한 자료인지 점검
                if (Check_ASM008(row, conn, "2") == true)
                {
                    CDataASM008_002 data = new CDataASM008_002();
                    data.Clear();

                    SetData(data, row, conn, ref no, "2");

                    list.Add(data);
                }

                return MetroLib.SqlHelper.CONTINUE;
            });
            */
            // 외래
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI1A A";
            sql += Environment.NewLine + " WHERE A.EXDATE>='" + frdt.Substring(0, 6) + "'";
            sql += Environment.NewLine + "   AND A.EXDATE<='" + todt.Substring(0, 6) + "'";
            sql += Environment.NewLine + "   AND ISNULL(A.DEMNO,'')<>''";
            sql += Environment.NewLine + "   AND LEFT(A.QFYCD,1) IN ('2','3')";
            sql += Environment.NewLine + " ORDER BY A.PID, A.STEDT";

            MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                // 사용가능한 자료인지 점검
                if (Check_ASM008(row, conn, "1") == true)
                {
                    CDataASM008_002 data = new CDataASM008_002();
                    data.Clear();

                    SetData(data, row, conn, ref no, "1", frdt, todt);

                    list.Add(data);
                }

                return MetroLib.SqlHelper.CONTINUE;
            });

            return list;
        }

        private bool Check_ASM008(DataRow p_row, OleDbConnection p_conn, string p_iofg)
        {
            System.Windows.Forms.Application.DoEvents();
            string tTI2F = "TI2F";
            string fBDODT = "BDODT";
            if (p_iofg == "1")
            {
                tTI2F = "TI1F";
                fBDODT = "EXDATE";
            }
            bool ret = false;
            string bgiho = "";
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM " + tTI2F + " (NOLOCK)";
            sql += Environment.NewLine + " WHERE " + fBDODT + "='" + p_row[fBDODT].ToString() + "'";
            sql += Environment.NewLine + "   AND QFYCD='" + p_row["QFYCD"].ToString() + "'";
            sql += Environment.NewLine + "   AND JRBY='" + p_row["JRBY"].ToString() + "'";
            sql += Environment.NewLine + "   AND PID='" + p_row["PID"].ToString() + "'";
            sql += Environment.NewLine + "   AND UNISQ='" + p_row["UNISQ"].ToString() + "'";
            sql += Environment.NewLine + "   AND SIMCS='" + p_row["SIMCS"].ToString() + "'";
            sql += Environment.NewLine + "   AND ISNULL(MAFG,'')='2'";
            sql += Environment.NewLine + "   AND ISNULL(OKCD,'')=''"; // 위탁진료 제외
            sql += Environment.NewLine + " ORDER BY ELINENO";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                bgiho = row["BGIHO"].ToString();
                if (bgiho == "O7020")
                {
                    ret = true;
                    return MetroLib.SqlHelper.BREAK;
                }
                return MetroLib.SqlHelper.CONTINUE;
            });
            return ret;
        }

    }
}
