using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetroLib
{
    public class DBHelper
    {

        public static string GetConnectionString()
        {
            try
            {
                string strIniFile = "C:/Metro/DLL/MetroHIS.ini";
                string strDBServer = INIHelper.ReadIniFile(strIniFile, "MetroHis", "DBSERVER");
                string strDBName = INIHelper.ReadIniFile(strIniFile, "MetroHis", "DBNAME");
                string strUid = INIHelper.ReadIniFile(strIniFile, "MetroHis", "UID");
                string strPwd = INIHelper.ReadIniFile(strIniFile, "MetroHis", "PWD");
                if ("".Equals(strUid)) strUid = "sa";
                if ("".Equals(strPwd)) strPwd = "mms";
                string strConn = "Provider=SQLOLEDB.1;Password=" + strPwd + ";Persist Security Info=true;User ID=" + strUid + ";Initial Catalog=" + strDBName + ";Data Source=" + strDBServer + "";
                return strConn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
