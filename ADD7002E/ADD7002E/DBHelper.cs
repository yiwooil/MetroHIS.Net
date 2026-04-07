using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ADD7002E
{
    class DBHelper
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static string GetConnectionString()
        {
            try
            {
                string strIniFile = "C:/Metro/DLL/MetroHIS.ini";
                string strDBServer = ReadIniFile(strIniFile, "MetroHis", "DBSERVER");
                string strDBName = ReadIniFile(strIniFile, "MetroHis", "DBNAME");
                string strUid = ReadIniFile(strIniFile, "MetroHis", "UID");
                string strPwd = ReadIniFile(strIniFile, "MetroHis", "PWD");
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

        public static string ReadIniFile(string path, string section, string key)
        {
            try
            {
                StringBuilder sb = new StringBuilder(255);
                GetPrivateProfileString(section, key, "", sb, sb.Capacity, path);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetFtpServer()
        {
            string strIniFile = "C:/Metro/DLL/MetroHIS.ini";
            string strFtpServer = ReadIniFile(strIniFile, "MetroHis", "FTPSERVER");
            return strFtpServer;
        }
    }
}
