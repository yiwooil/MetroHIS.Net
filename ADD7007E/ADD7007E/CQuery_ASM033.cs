using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    class CQuery_ASM033 : CQuery
    {
        public List<CDataASM033_003> Query_ASM033(OleDbConnection conn, string frdt, string todt)
        {
            m_dic_cnectdd.Clear();
            m_dic_dcount.Clear();
            m_dic_billsno.Clear();
            m_dic_cnecno.Clear();

            List<CDataASM033_003> list = new List<CDataASM033_003>();

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
                if (Check_ASM033(row, conn) == true)
                {
                    CDataASM033_003 data = new CDataASM033_003();
                    data.Clear();

                    SetData(data, row, conn, ref no, "2", frdt, todt);

                    list.Add(data);
                }

                return MetroLib.SqlHelper.CONTINUE;
            });

            return list;
        }

        private bool Check_ASM033(DataRow p_row, OleDbConnection p_conn)
        {
            System.Windows.Forms.Application.DoEvents();
            return true;
        }

    }
}
