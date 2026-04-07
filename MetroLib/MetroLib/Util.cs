using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MetroLib
{
    public class Util
    {
        static public string AddMonth(string input, int month)
        {
            DateTime date = DateTime.ParseExact(input, "yyyyMMdd", CultureInfo.InvariantCulture);
            DateTime dateMonthsAgo = date.AddMonths(month);
            string result = dateMonthsAgo.ToString("yyyyMMdd");
            return result;
        }

        static public String GetSysDate(OleDbConnection p_conn)
        {
            String strRet = "";
            String sql = "SELECT CONVERT(VARCHAR,GETDATE(),112) AS SYSDATE ";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    strRet = reader["SYSDATE"].ToString();
                }
                reader.Close();
            }
            return strRet;
        }

        static public String GetSysDate(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            String strRet = "";
            String sql = "SELECT CONVERT(VARCHAR,GETDATE(),112) AS SYSDATE ";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                if (p_tran != null) cmd.Transaction = p_tran;
                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    strRet = reader["SYSDATE"].ToString();
                }
                reader.Close();
            }
            return strRet;
        }

        static public String GetSysTime(OleDbConnection p_conn)
        {
            String strRet = "";
            String sql = "SELECT REPLACE(CONVERT(VARCHAR,GETDATE(),8),':','') AS SYSTIME ";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    strRet = reader["SYSTIME"].ToString();
                }
                reader.Close();
            }
            return strRet;
        }

        static public String GetSysTime(OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            String strRet = "";
            String sql = "SELECT REPLACE(CONVERT(VARCHAR,GETDATE(),8),':','') AS SYSTIME ";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                if (p_tran != null) cmd.Transaction = p_tran;
                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    strRet = reader["SYSTIME"].ToString();
                }
                reader.Close();
            }
            return strRet;
        }

        static public String GetAge(String Fday, String Tday, String option)
        {
            try
            {
                if (ValDt(Fday) == false || ValDt(Tday) == false) return "";
                if (Fday.CompareTo(Tday) > 0) return "";

                long OYY;
                long OMM;
                long ODD;

                String Dcnt;

                long FYY;
                long FMM;
                long FDD;
                long TYY;
                long TMM;
                long TDD;

                Dcnt = "31,28,31,30,31,30,31,31,30,31,30,31";

                FYY = BtoL(Fday.Substring(0, 4));
                FMM = BtoL(Fday.Substring(4, 2));
                FDD = BtoL(Fday.Substring(6, 2));

                TYY = BtoL(Tday.Substring(0, 4));
                TMM = BtoL(Tday.Substring(4, 2));
                TDD = BtoL(Tday.Substring(6, 2));

                if (((FYY % 4) == 0) && (((FYY % 100) != 0) || ((FYY % 400) == 0)))
                {
                    Dcnt = "31,29,31,30,31,30,31,31,30,31,30,31";
                }
                String[] arrDcnt = Dcnt.Split(',');

                OYY = TYY - FYY - 1;
                OMM = (12 - FMM) + (TMM - 1);
                ODD = BtoL(arrDcnt[FMM - 1]) - FDD + TDD;
                if (ODD >= BtoL(arrDcnt[FMM - 1]))
                {
                    ODD = ODD - BtoL(arrDcnt[FMM - 1]);
                    OMM = OMM + 1;
                }
                if (ODD >= BtoL(arrDcnt[TMM - 1]))
                {
                    ODD = ODD - BtoL(arrDcnt[TMM - 1]);
                    OMM = OMM + 1;
                }
                while (OMM >= 12)
                {
                    OMM = OMM - 12;
                    OYY = OYY + 1;
                }

                String rtn;
                if (option == "year")
                {
                    rtn = OYY.ToString();
                }
                else
                {
                    rtn = ((10000 + OYY).ToString()).Substring(2, 4);
                    rtn += ((100 + OMM).ToString()).Substring(2, 2);
                    rtn += ((100 + ODD).ToString()).Substring(2, 2);
                }
                return rtn;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        static public Boolean ValDt(String date)
        {
            if (date.Length != 8) return false;
            try
            {
                DateTime dt = DateTime.ParseExact(date, "yyyyMMdd", null);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        static public long BtoL(string sValue)
        {
            long ret = 0;
            long.TryParse(sValue, out ret);
            return ret;
        }

        static public void WritePIICLog(string usrid, string prjid, string frmnm, string job, string ippm, string oprv, string remark, OleDbConnection p_conn)
        {
            string seq = "";
            string hostip = "";
            string hostnm = System.Net.Dns.GetHostName();
            System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(hostnm);
            foreach (System.Net.IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    hostip = ip.ToString();
                    break;
                }
            }

            string sql = "";
            sql = "";
            sql = sql + Environment.NewLine + "SELECT ISNULL(MAX(SEQ),0) + 1 NEXT_SEQ";
            sql = sql + Environment.NewLine + "  FROM TA94C_PIICL";
            sql = sql + Environment.NewLine + " WHERE USRIP=?";
            sql = sql + Environment.NewLine + "   AND USRID=?";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", hostip));
                cmd.Parameters.Add(new OleDbParameter("@2", usrid));

                // 데이타는 서버에서 가져오도록 실행
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    seq = reader["NEXT_SEQ"].ToString();
                }
            }

            sql = "";
            sql = sql + Environment.NewLine + "INSERT INTO TA94C_PIICL(USRIP, USRID, SEQ, PRJID, FRMNM, JOB, IPPM, OPRV, REMARK, HSTNM, ENTDT, ENTTM)";
            sql = sql + Environment.NewLine + "VALUES";
            sql = sql + Environment.NewLine + "(?,?,?,?,?,?,?,?,?,?";
            sql = sql + Environment.NewLine + ",CONVERT(VARCHAR,GETDATE(),112),LEFT(REPLACE(CONVERT(VARCHAR,GETDATE(),14),':',''),6))";

            // TSQL문장과 Connection 객체를 지정   
            using (OleDbCommand cmd = new OleDbCommand(sql, p_conn))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new OleDbParameter("@1", hostip));
                cmd.Parameters.Add(new OleDbParameter("@2", usrid));
                cmd.Parameters.Add(new OleDbParameter("@3", seq));
                cmd.Parameters.Add(new OleDbParameter("@4", prjid));
                cmd.Parameters.Add(new OleDbParameter("@5", frmnm));
                cmd.Parameters.Add(new OleDbParameter("@6", job));
                cmd.Parameters.Add(new OleDbParameter("@7", ippm));
                cmd.Parameters.Add(new OleDbParameter("@8", oprv));
                cmd.Parameters.Add(new OleDbParameter("@9", remark));
                cmd.Parameters.Add(new OleDbParameter("@10", hostnm));

                cmd.ExecuteNonQuery();
            }
        }

    }
}
