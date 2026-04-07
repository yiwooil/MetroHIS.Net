using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using MetroLib;

namespace ADD0348Q
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            MetroLib.SubModule.CheckSubModuleAndDown();
            

            String user = "";
            String pwd = "";

            if (args.Length > 0)
            {
                ParseArg(args[0], ref user, ref pwd);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ADD0348Q(user, pwd));
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
    }
}
