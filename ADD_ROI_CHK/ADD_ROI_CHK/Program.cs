using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ADD_ROI_CHK
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            // ROI.exe가 실행중이면 검사를 중단한다. 동시에 실행이 안된다.
            bool bFind = false;
            System.Diagnostics.Process[] allProc = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process p in allProc)
            {
                if (p.ProcessName.ToUpper() == "ROI.Expert.Main".ToUpper())
                {
                    bFind = true;
                    break;
                }
            }
            if (bFind)
            {
                AutoClosingMessageBox.Show("<진료비사전점검 프로그램 ROI>가 실행중이면 사용할 수 없습니다.\r\n\r\n이 창은 1초후 자동으로 종료됩니다.", "ADD_ROI_CHK 알림", 1000);
                return;
            }

            if (IsAdministrator() == false)
            {
                // 관리자 권한으로 실행되도록 한다.
                try
                {
                    string arguments = "";
                    for (int i = 0; i < args.Length; i++)
                    {
                        if (i== 0) arguments = args[i];
                        else arguments += " " + args[i];
                    }
                    System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo()
                    {
                        UseShellExecute = true,
                        FileName = Application.ExecutablePath,
                        WorkingDirectory = Environment.CurrentDirectory,
                        Arguments = arguments,
                        Verb = "runas"
                    };
                    System.Diagnostics.Process.Start(info);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MetroLib.SubModule.CheckSubModuleAndDown();

                String user = "";
                String pwd = "";
                String prjcd = "";
                String roibase = "";
                String ex_msg_code = "";
                String un_ex_msg_code = "";


                if (args.Length > 0)
                {
                    ParseArg(args[0], ref user, ref pwd, ref prjcd, ref roibase, ref ex_msg_code, ref un_ex_msg_code);
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ADD_ROI_CHK(user, pwd, prjcd, roibase, ex_msg_code, un_ex_msg_code));
            }
        }

        private static void ParseArg(String arg, ref String user, ref String pwd, ref String prjcd, ref String roibase, ref String ex_msg_code, ref String un_ex_msg_code)
        {
            String[] aryArg = (arg + ',').Split(',');
            for (int i = 0; i < aryArg.Length; i++)
            {
                String[] val = (aryArg[i] + '=').Split('=');
                if ("USER".Equals(val[0].ToUpper())) user = val[1];
                else if ("PWD".Equals(val[0].ToUpper())) pwd = val[1];
                else if ("PRJCD".Equals(val[0].ToUpper())) prjcd = val[1];
                else if ("ROIBASE".Equals(val[0].ToUpper())) roibase = val[1];
                else if ("SETEX".Equals(val[0].ToUpper())) ex_msg_code = val[1];
                else if ("UNSETEX".Equals(val[0].ToUpper())) un_ex_msg_code = val[1];
            }
        }

        private static bool IsAdministrator()
        {
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            if (null != identity)
            {
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            }
            return false;
        }

    }
}
