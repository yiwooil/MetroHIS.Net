using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CODE902E.NET
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CA02Up a02Up = new CA02Up();
            a02Up.main();

            return;

            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            */
        }
    }
}
