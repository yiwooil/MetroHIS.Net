using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    class CQuery_ASM049 : CQuery
    {
        public List<CDataASM049_001> Query_ASM049(OleDbConnection conn, string frdt, string todt)
        {
            m_dic_cnectdd.Clear();
            m_dic_dcount.Clear();
            m_dic_billsno.Clear();
            m_dic_cnecno.Clear();

            List<CDataASM049_001> list = new List<CDataASM049_001>();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI2A A (NOLOCK)";
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
                if (Check_ASM049(row, conn, "2") == true)
                {
                    CDataASM049_001 data = new CDataASM049_001();
                    data.Clear();

                    SetData(data, row, conn, ref no, "2", frdt, todt);

                    list.Add(data);
                }

                return MetroLib.SqlHelper.CONTINUE;
            });

            // 외래
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI1A A (NOLOCK)";
            sql += Environment.NewLine + " WHERE A.EXDATE>='" + frdt.Substring(0, 6) + "'";
            sql += Environment.NewLine + "   AND A.EXDATE<='" + todt.Substring(0, 6) + "'";
            sql += Environment.NewLine + "   AND ISNULL(A.DEMNO,'')<>''";
            sql += Environment.NewLine + "   AND LEFT(A.QFYCD,1) IN ('2','3')";
            sql += Environment.NewLine + " ORDER BY A.PID, A.STEDT";

            MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                // 사용가능한 자료인지 점검
                if (Check_ASM049(row, conn, "1") == true)
                {
                    CDataASM049_001 data = new CDataASM049_001();
                    data.Clear();

                    SetData(data, row, conn, ref no, "1", frdt, todt);

                    list.Add(data);
                }

                return MetroLib.SqlHelper.CONTINUE;
            });

            return list;
        }

        private bool Check_ASM049(DataRow p_row, OleDbConnection p_conn, string p_iofg)
        {
            System.Windows.Forms.Application.DoEvents();

            // SEQ1 = 10, POS2 = 3 : CT
            // SEQ1 = 10, POS2 = 4 : MRI
            // SEQ1 = 10, POS2 = 5 : PET
            // SEQ1 = 11 : CT
            // SEQ1 = 12 : MRI
            // SEQ1 = 13 : PET

            string tTI2F = "TI2F";
            string fBDODT = "BDODT";
            if (p_iofg == "1")
            {
                tTI2F = "TI1F";
                fBDODT = "EXDATE";
            }
            int count = 0;
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
            sql += Environment.NewLine + "   AND (ACTFG IN ('05','06','07') OR GRPACT IN ('05','06','07'))";
            sql += Environment.NewLine + "   AND ISNULL(OKCD,'')=''"; // 위탁진료 제외

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                count++;
                return MetroLib.SqlHelper.BREAK;
            });
            return count > 0;
        }

    }
}
