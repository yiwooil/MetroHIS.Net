using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ADD7003E
{
    class INIHelper
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

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
