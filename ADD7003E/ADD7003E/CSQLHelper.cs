using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class CSQLHelper
    {
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
                            if (bContinue==false) break;
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //MessageBox.Show(ex.Message);
            }
        }

    }
}
