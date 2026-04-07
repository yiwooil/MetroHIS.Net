using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ADD7006E
{
    interface CMake001
    {
        void Make(CData p_dsdata, OleDbConnection p_conn);
    }
}
