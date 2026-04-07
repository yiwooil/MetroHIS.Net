using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    public interface IDataRemake
    {
        void ReadDataFromEMR(OleDbConnection p_conn, OleDbTransaction p_tran);
        void UpdData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran);
    }
}
