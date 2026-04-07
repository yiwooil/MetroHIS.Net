using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    // 2026.01.22 WOOIL - 이 클래스 사용 안 함.
    class CQuery_ASM010 : CQuery
    {
        public event EventHandler<QueryEventArgs> QueryEvent;

        public List<CDataASM010_002> Query_ASM010(OleDbConnection conn, string frdt, string todt)
        {
            m_dic_cnectdd.Clear();
            m_dic_dcount.Clear();
            m_dic_billsno.Clear();
            m_dic_cnecno.Clear();

            List<CDataASM010_002> list = new List<CDataASM010_002>();

            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI2A A (NOLOCK)";
            sql += Environment.NewLine + " WHERE A.BDODT>='" + frdt + "'";
            sql += Environment.NewLine + "   AND A.BDODT<='" + todt + "'";
            sql += Environment.NewLine + "   AND ISNULL(A.DEMNO,'')<>''";
            sql += Environment.NewLine + "   AND LEFT(A.QFYCD,1) IN ('2','3')";
            sql += Environment.NewLine + " ORDER BY A.PNM, A.STEDT";

            int no = 0;
            MetroLib.SqlHelper.GetDataRow(sql, conn, delegate(DataRow row)
            {
                // 진행중표시
                OnQueryEvent(row["PNM"].ToString());

                System.Windows.Forms.Application.DoEvents();

                string jrkwa = row["JRKWA"].ToString();
                string[] arr = jrkwa.Split('$');
                if (arr[0] != "2") return MetroLib.SqlHelper.CONTINUE; // 외과분야 만

                // 사용가능한 자료인지 점검
                if (Check_ASM010(row, conn) == true)
                {
                    CDataASM010_002 data = new CDataASM010_002();
                    data.Clear();

                    SetData(data, row, conn, ref no, "2", frdt, todt);

                    if (data.A04_BEDEDT.CompareTo(frdt) < 0) return MetroLib.SqlHelper.CONTINUE; // frdt 이후에 입원해야 함.

                    list.Add(data);
                }

                return MetroLib.SqlHelper.CONTINUE;
            });

            return list;
        }

        private bool Check_ASM010(DataRow p_row, OleDbConnection p_conn)
        {
            System.Windows.Forms.Application.DoEvents();
            bool ret = false;
            bool isOp = false; // 수술코드여부
            bool isAnti = false; // 항생제여부
            string opCode = "";
            string antiCode = "";
            string sql = "";
            sql = "";
            sql += Environment.NewLine + "SELECT *";
            sql += Environment.NewLine + "  FROM TI2F (NOLOCK)";
            sql += Environment.NewLine + " WHERE BDODT='" + p_row["BDODT"].ToString() + "'";
            sql += Environment.NewLine + "   AND QFYCD='" + p_row["QFYCD"].ToString() + "'";
            sql += Environment.NewLine + "   AND JRBY='" + p_row["JRBY"].ToString() + "'";
            sql += Environment.NewLine + "   AND PID='" + p_row["PID"].ToString() + "'";
            sql += Environment.NewLine + "   AND UNISQ='" + p_row["UNISQ"].ToString() + "'";
            sql += Environment.NewLine + "   AND SIMCS='" + p_row["SIMCS"].ToString() + "'";
            //sql += Environment.NewLine + "   AND ISNULL(MAFG,'')='2'";
            sql += Environment.NewLine + "   AND ISNULL(OKCD,'')=''"; // 위탁진료 제외
            sql += Environment.NewLine + " ORDER BY ELINENO";

            MetroLib.SqlHelper.GetDataRow(sql, p_conn, delegate(DataRow row)
            {
                System.Windows.Forms.Application.DoEvents();

                string bgiho = row["BGIHO"].ToString();
                if (isOp == false && CUtil_ASM010.IsOPCode(bgiho) == true)
                {
                    isOp = true;
                    opCode = bgiho;
                }
                if (isAnti==false && CUtil_ASM010.IsANBOCode(bgiho) == true)
                {
                    isAnti = true;
                    antiCode = bgiho;
                }
                if (isOp == true && isAnti == true) return MetroLib.SqlHelper.BREAK; // 다 찾았으면 그만

                return MetroLib.SqlHelper.CONTINUE;
            });
            if (isOp == true && isAnti == true)
            {
                ret = true;
            }
            return ret;
        }

        protected virtual void OnQueryEvent(string message)
        {
            if (QueryEvent != null)
            {
                QueryEvent(this, new QueryEventArgs(message));
            }
        }

    }
}
