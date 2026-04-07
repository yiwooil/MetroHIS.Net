using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ADD0546R
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
            String prjcd = "";
            String addpara = "";

            if (args.Length > 0)
            {
                ParseArg(args[0], ref user, ref pwd, ref prjcd, ref addpara);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ADD0546R(user, pwd, prjcd, addpara));
        }

        private static void ParseArg(String arg, ref String user, ref String pwd, ref String prjcd, ref String addpara)
        {
            String[] aryArg = (arg + ',').Split(',');
            for (int i = 0; i < aryArg.Length; i++)
            {
                String[] val = (aryArg[i] + '=').Split('=');
                if ("USER".Equals(val[0].ToUpper())) user = val[1];
                else if ("PWD".Equals(val[0].ToUpper())) pwd = val[1];
                else if ("PRJCD".Equals(val[0].ToUpper())) prjcd = val[1];
                else if ("ADDPARA".Equals(val[0].ToUpper())) addpara = val[1];
            }
        }
    }
}
