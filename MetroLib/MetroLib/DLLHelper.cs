using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace MetroLib
{
    public class DLLHelper
    {
        public static void VersionCheckOnlyDLL(String formName, Boolean isEXE)
        {
            if (IsDeveloper() == true)
            {
                // 개발자 PC이면 버전 체크를 하지 않는다.
            }
            else
            {
                // PC에 있는 파일의 버전을 확인
                String versionAtClient = GetVersionAtClient(formName, isEXE);
                // 서버에 등록된 버전을 확인
                String versionAtServer = GetVersionAtServer(formName, isEXE);
                // 버전이 변경되었으면 다운로드
                if (versionAtClient != versionAtServer) CopyFile(formName, isEXE);
            }
        }

        public static void LoadDLL(String formName, String formText, String para, Boolean isEXE)
        {
            IntPtr hWnd = Win32API.FindWindow(null, formText + "(" + formName + ")");
            if (hWnd == IntPtr.Zero)
            {
                // 실행중인 창이 없음.

                if (IsDeveloper() == true)
                {
                    // 개발자 PC이면 버전 체크를 하지 않는다.
                }
                else
                {
                    // PC에 있는 파일의 버전을 확인
                    String versionAtClient = GetVersionAtClient(formName, isEXE);
                    // 서버에 등록된 버전을 확인
                    String versionAtServer = GetVersionAtServer(formName, isEXE);
                    // 버전이 변경되었으면 다운로드
                    if (versionAtClient != versionAtServer) CopyFile(formName, isEXE);
                }
                // 실행
                ProcessStart(formName, para, isEXE);
            }
            else
            {
                // 실행중임.
                if (para != "")
                {
                    byte[] buff = System.Text.Encoding.Default.GetBytes(para);
                    Win32API.COPYDATASTRUCT cds = new MetroLib.Win32API.COPYDATASTRUCT();
                    cds.dwData = IntPtr.Zero;
                    cds.cbData = (uint)buff.Length + 1;
                    cds.lpData = para;
                    Win32API.SendMessage(hWnd, Win32API.WM_COPYDATA, IntPtr.Zero, ref cds);
                }
                Win32API.SetForegroundWindow(hWnd);
            }
        }

        private static String GetVersionAtServer(String formName, Boolean isEXE)
        {
            String version = "";
            if (isEXE) formName += ".exe";
            string sql = "SELECT VERSION FROM TA92 WHERE FORMID = '" + formName + "' ";
            string strConn = MetroLib.DBHelper.GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                // TSQL문장과 Connection 객체를 지정   
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    // 데이타는 서버에서 가져오도록 실행
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        version = reader["VERSION"].ToString();
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return version;
        }

        private static String GetVersionAtClient(String formName,Boolean isEXE)
        {
            String fileName = "C:/Metro/DLL/" + formName + (isEXE ? ".exe" : ".dll");
            FileInfo fileInfo = new FileInfo(fileName);
            if (fileInfo.Exists == false)
            {
                return "";
            }
            else
            {
                FileVersionInfo.GetVersionInfo(fileName);
                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(fileName);
                return myFileVersionInfo.FileVersion;
            }
        }

        private static void CopyFile(String formName, Boolean isEXE)
        {
            String ftpServer = INIHelper.GetFtpServer();
            String sourceFileName = "//" + ftpServer + "/FtpHome/VBEXEs/DLL/" + formName + (isEXE ? ".exe" : ".dll");
            String destFileName = "C:/Metro/DLL/" + formName + (isEXE ? ".exe" : ".dll");
            File.Copy(sourceFileName, destFileName, true);
        }

        private static void ProcessStart(String formName, String para, Boolean isEXE)
        {
            if (isEXE)
            {
                String fileName = "C:/Metro/DLL/" + formName;
                if (isEXE) fileName += ".exe";
                Process.Start(fileName, para);
            }
        }

        private static Boolean IsDeveloper()
        {
            String develop = INIHelper.ReadIniFile("C:/METROSOFT.ini", "MetroHIS", "DEVELOP");
            return (develop=="Y");
        }
    }
}
