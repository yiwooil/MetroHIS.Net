using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ADD_ROI_LOGIN
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            if (IsAdministrator() == false)
            {
                // 관리자 권한으로 실행되도록 한다.
                try
                {
                    string arguments = "";
                    for (int i = 0; i < args.Length; i++)
                    {
                        if (i == 0) arguments = args[i];
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

                if (args.Length > 0)
                {
                    ParseArg(args[0], ref user, ref pwd, ref prjcd);
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ADD_ROI_LOGIN(user, pwd, prjcd));
            }
        }

        private static void ParseArg(String arg, ref String user, ref String pwd, ref String prjcd)
        {
            String[] aryArg = (arg + ',').Split(',');
            for (int i = 0; i < aryArg.Length; i++)
            {
                String[] val = (aryArg[i] + '=').Split('=');
                if ("USER".Equals(val[0].ToUpper())) user = val[1];
                else if ("PWD".Equals(val[0].ToUpper())) pwd = val[1];
                else if ("PRJCD".Equals(val[0].ToUpper())) prjcd = val[1];
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
