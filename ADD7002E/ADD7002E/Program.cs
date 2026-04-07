using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ADD7002E
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            CheckSubModuldAndDown();

            String user = "";
            String pwd = "";

            if (args.Length > 0)
            {
                ParseArg(args[0], ref user, ref pwd);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ADD7002E(user, pwd));
        }
        private static void ParseArg(String arg, ref String user, ref String pwd)
        {
            String[] aryArg = (arg + ',').Split(',');
            for (int i = 0; i < aryArg.Length; i++)
            {
                String[] val = (aryArg[i] + '=').Split('=');
                if ("USER".Equals(val[0].ToUpper())) user = val[1];
                else if ("PWD".Equals(val[0].ToUpper())) pwd = val[1];
            }
        }

        private static void CheckSubModuldAndDown()
        {
            try
            {
                // 개발자모드이면 검사하지 않는다.
                String strDevelop = "";
                strDevelop = DBHelper.ReadIniFile("C:/METROSOFT.ini", "MetroHIS", "DEVELOP");
                if ("Y".Equals(strDevelop)) return;

                // 필요한 모듈이 있는지 검사한다.
                ArrayList subFormid = new ArrayList();
                subFormid.Add("DevExpress.Data.v12.2.dll");
                subFormid.Add("DevExpress.Printing.v12.2.Core.dll");
                subFormid.Add("DevExpress.Utils.v12.2.dll");
                subFormid.Add("DevExpress.XtraEditors.v12.2.dll");
                subFormid.Add("DevExpress.XtraGrid.v12.2.dll");
                subFormid.Add("DevExpress.XtraLayout.v12.2.dll");

                subFormid.Add("Newtonsoft.Json.dll");

                String ftpServer = DBHelper.GetFtpServer();
                //MessageBox.Show("ftpServer=" + ftpServer);

                // DevExpress를 12.2.6에서 12.1.28로 올렸음. DevExpress dll을 다운로드 해야함.
                String verPath = "C:/Metro/DLL/DevExpress.Ver.12.2.18.txt";
                System.IO.FileInfo verfi = new System.IO.FileInfo(verPath);

                // 필요한 모듈이 PC에 있는지 점검한다.
                foreach (String subForm in subFormid)
                {
                    String filePath = "C:/Metro/DLL/" + subForm;
                    //MessageBox.Show("filePath=" + filePath);
                    System.IO.FileInfo fi = new System.IO.FileInfo(filePath);
                    if (fi.Exists == false || verfi.Exists == false)
                    {
                        // 파일 없음. 서버에서 복사해옴.
                        String ftpFilePath = "//" + ftpServer + "/FtpHome/VBEXEs/DLL/" + subForm;
                        //MessageBox.Show("ftpFilePath=" + ftpFilePath);
                        System.IO.FileInfo ftpFile = new System.IO.FileInfo(ftpFilePath);
                        ftpFile.CopyTo(filePath, true);
                    }
                }

                if (verfi.Exists == false)
                {
                    System.IO.File.WriteAllText("C:/Metro/DLL/DevExpress.Ver.12.2.18.txt", "");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
