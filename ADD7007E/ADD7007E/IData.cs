using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7007E
{
    interface IData
    {
        void Clear();
        void ClearMe();
        void ReadDataFromSaved(OleDbConnection p_conn, OleDbTransaction p_tran);
        void InsData(string p_sysdt, string p_systm, string p_user, OleDbConnection p_conn, OleDbTransaction p_tran, bool del_fg);
        void DelAllData(OleDbConnection p_conn, OleDbTransaction p_tran);
    }
}
