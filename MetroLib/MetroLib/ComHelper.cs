using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetroLib
{
    public class ComHelper
    {
        static public string MExe(string method, string p1)
        {
            string[] paras = { "", p1 };
            string ret = MExe(method, paras);
            return ret;
        }

        static public string MExe(string method, string p1, string p2)
        {
            string[] paras = { "", p1, p2 };
            string ret = MExe(method, paras);
            return ret;
        }

        static public string MExe(string method, string p1, string p2, string p3)
        {
            string[] paras = { "", p1, p2, p3 };
            string ret = MExe(method, paras);
            return ret;
        }

        static public string MExe(string method, string p1, string p2, string p3, string p4)
        {
            string[] paras = { "", p1, p2, p3, p4 };
            string ret = MExe(method, paras);
            return ret;
        }

        static public string MExe(string method, string p1, string p2, string p3, string p4, string p5)
        {
            string[] paras = { "", p1, p2, p3, p4, p5 };
            string ret = MExe(method, paras);
            return ret;
        }

        static public string MExe(string method, string p1, string p2, string p3, string p4, string p5, string p6)
        {
            string[] paras = { "", p1, p2, p3, p4, p5, p6 };
            string ret = MExe(method, paras);
            return ret;
        }

        static public string MExe(string method, string p1, string p2, string p3, string p4, string p5, string p6, string p7)
        {
            string[] paras = { "", p1, p2, p3, p4, p5, p6, p7 };
            string ret = MExe(method, paras);
            return ret;
        }

        static public string MExe(string method, string p1, string p2, string p3, string p4, string p5, string p6, string p7, string p8)
        {
            string[] paras = { "", p1, p2, p3, p4, p5, p6, p7, p8 };
            string ret = MExe(method, paras);
            return ret;
        }

        static public string MExe(string method, string p1, string p2, string p3, string p4, string p5, string p6, string p7, string p8, string p9)
        {
            string[] paras = { "", p1, p2, p3, p4, p5, p6, p7, p8, p9 };
            string ret = MExe(method, paras);
            return ret;
        }

        static public string MExe(string method, string p1, string p2, string p3, string p4, string p5, string p6, string p7, string p8, string p9, string p10)
        {
            string[] paras = { "", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10 };
            string ret = MExe(method, paras);
            return ret;
        }

        static private string MExe(string method, string[] paras)
        {
            string myRtn = "";
            string myLabel = "";
            try
            {
                string[] method_arr = method.Split('^');
                myRtn = method_arr[1];
                myLabel = method_arr[0].Replace("$$", "");
                object[] args = { myRtn, myLabel, paras, paras.Length - 1 };
                System.Type objCompType = System.Type.GetTypeFromProgID("MTRGWS.wormHole", true);
                object objComp = System.Activator.CreateInstance(objCompType);
                string ret = objCompType.InvokeMember
                    ("DoIt"
                    , System.Reflection.BindingFlags.InvokeMethod
                    , null
                    , objComp
                    , args
                    ).ToString();
                return ret;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                string msg2 = "";
                if (ex.InnerException != null) msg2 = ex.InnerException.Message;

                throw new Exception(myLabel + "^" + myRtn + "\r\n\r\n" + msg + "\r\n\r\n" + msg2);
            }
        }
    }
}
