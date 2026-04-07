using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CODEDr.NET
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            MetroLib.SubModule.CheckSubModuleAndDown();

            string drid = "";
            string serverIp = "";
            string hosid = "";

            if (args.Length > 0)
            {
                ParseArg(args[0], ref drid, ref serverIp, ref hosid);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CODEDr(drid, serverIp, hosid));
        }

        private static void ParseArg(string arg, ref string drid, ref string serverIp, ref string hosid)
        {
            String[] aryArg = (arg + ',').Split(',');
            for (int i = 0; i < aryArg.Length; i++)
            {
                String[] val = (aryArg[i] + '=').Split('=');
                if ("DRID".Equals(val[0].ToUpper())) drid = val[1];
                else if ("SERVERIP".Equals(val[0].ToUpper())) serverIp = val[1];
                else if ("HOSID".Equals(val[0].ToUpper())) hosid = val[1];
            }
        }
    }
}
