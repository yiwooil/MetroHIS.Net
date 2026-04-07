using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADD8101E
{
    class StrHelper
    {
        public static String SubstringH(String Target, int Start, int Length)
        {
            // 한글을 2바이트로 처리하여 자른다.
            byte[] byteTarget = Encoding.GetEncoding("korean").GetBytes(Target);
            if (byteTarget.Length < Length) Length = byteTarget.Length;
            if (Length == 0)
            {
                // 끝까지
                return Encoding.GetEncoding("korean").GetString(byteTarget, Start, byteTarget.Length - Start);
            }
            else
            {
                return Encoding.GetEncoding("korean").GetString(byteTarget, Start, Length);
            }
        }

    }
}
