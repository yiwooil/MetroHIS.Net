using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD7003E
{
    class CFmtHelper
    {
        static public string ToDateTimeString(string p_value)
        {
            string ret = p_value;
            if (p_value.Length == 14)
            {
                ret = p_value.Substring(0, 4) + "-" + p_value.Substring(4, 2) + "-" + p_value.Substring(6, 2) + " " + p_value.Substring(8, 2) + ":" + p_value.Substring(10, 2) + ":" + p_value.Substring(12, 2);
            }
            else if (p_value.Length == 12)
            {
                ret = p_value.Substring(0, 4) + "-" + p_value.Substring(4, 2) + "-" + p_value.Substring(6, 2) + " " + p_value.Substring(8, 2) + ":" + p_value.Substring(10, 2);
            }
            else if (p_value.Length == 10)
            {
                ret = p_value.Substring(0, 4) + "-" + p_value.Substring(4, 2) + "-" + p_value.Substring(6, 2) + " " + p_value.Substring(8, 2);
            }
            else if (p_value.Length == 8)
            {
                ret = p_value.Substring(0, 4) + "-" + p_value.Substring(4, 2) + "-" + p_value.Substring(6, 2);
            }
            return ret;
        }
    }
}
