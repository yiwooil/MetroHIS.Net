using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace MetroLib
{
    public class SqlHelper
    {
        // const는 자동으로 static 이다.
        public const bool CONTINUE = true;
        public const bool BREAK = false;

        static public void GetDataRow(string p_sql, OleDbConnection p_conn, Func<DataRow, bool> p_callback)
        {
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(p_sql, p_conn))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            adapter.Fill(ds);

                            foreach(DataRow row in ds.Tables[0].Rows)
                            {
                                bool bContinue = p_callback(row);
                                if (bContinue == false) break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static public void GetDataRow(string p_sql, List<Object> p_para, OleDbConnection p_conn, Func<DataRow, bool> p_callback)
        {
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(p_sql, p_conn))
                {
                    int i = 0;
                    foreach (Object obj in p_para)
                    {
                        cmd.Parameters.Add(new OleDbParameter("@" + (++i).ToString(), obj));
                    }

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            adapter.Fill(ds);

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                bool bContinue = p_callback(row);
                                if (bContinue == false) break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static public void GetDataRow(string p_sql, OleDbConnection p_conn, OleDbTransaction p_tran, Func<DataRow, bool> p_callback)
        {
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(p_sql, p_conn))
                {
                    if (p_tran != null) cmd.Transaction = p_tran;
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            adapter.Fill(ds);

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                bool bContinue = p_callback(row);
                                if (bContinue == false) break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static public void GetDataRow(string p_sql, List<Object> p_para, OleDbConnection p_conn, OleDbTransaction p_tran, Func<DataRow, bool> p_callback)
        {
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(p_sql, p_conn))
                {
                    if (p_tran != null) cmd.Transaction = p_tran;
                    int i = 0;
                    foreach (Object obj in p_para)
                    {
                        cmd.Parameters.Add(new OleDbParameter("@" + (++i).ToString(), obj));
                    }

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            adapter.Fill(ds);

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                bool bContinue = p_callback(row);
                                if (bContinue == false) break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static public void GetDataReader(string p_sql, OleDbConnection p_conn, Func<OleDbDataReader, bool> p_callback)
        {
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(p_sql, p_conn))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool bContinue = p_callback(reader);
                            if (bContinue == false) break;
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static public void GetDataReader(string p_sql, List<Object> p_para, OleDbConnection p_conn, Func<OleDbDataReader, bool> p_callback)
        {
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(p_sql, p_conn))
                {
                    int i = 0;
                    foreach (Object obj in p_para)
                    {
                        cmd.Parameters.Add(new OleDbParameter("@" + (++i).ToString(), obj));
                    }

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool bContinue = p_callback(reader);
                            if (bContinue == false) break;
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static public void GetDataReader(string p_sql, OleDbConnection p_conn, OleDbTransaction p_tran, Func<OleDbDataReader, bool> p_callback)
        {
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(p_sql, p_conn))
                {
                    if (p_tran != null) cmd.Transaction = p_tran;
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool bContinue = p_callback(reader);
                            if (bContinue == false) break;
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static public void GetDataReader(string p_sql, List<Object> p_para, OleDbConnection p_conn, OleDbTransaction p_tran, Func<OleDbDataReader, bool> p_callback)
        {
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(p_sql, p_conn))
                {
                    if (p_tran != null) cmd.Transaction = p_tran;
                    int i = 0;
                    foreach (Object obj in p_para)
                    {
                        cmd.Parameters.Add(new OleDbParameter("@" + (++i).ToString(), obj));
                    }

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool bContinue = p_callback(reader);
                            if (bContinue == false) break;
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static public int ExecuteSql(string p_sql, OleDbConnection p_conn)
        {
            int cnt = 0;
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(p_sql, p_conn))
                {
                    cnt = cmd.ExecuteNonQuery();
                }
                return cnt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static public int ExecuteSql(string p_sql, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            int cnt = 0;
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(p_sql, p_conn))
                {
                    if (p_tran != null) cmd.Transaction = p_tran;
                    cnt = cmd.ExecuteNonQuery();
                }
                return cnt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static public int ExecuteSql(string p_sql, List<Object> p_para, OleDbConnection p_conn, OleDbTransaction p_tran)
        {
            int cnt = 0;
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(p_sql, p_conn))
                {
                    if (p_tran != null) cmd.Transaction = p_tran;
                    int i = 0;
                    foreach (Object obj in p_para)
                    {
                        cmd.Parameters.Add(new OleDbParameter("@" + (++i).ToString(), obj));
                    }

                    cnt = cmd.ExecuteNonQuery();
                }
                return cnt;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
