using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetroLib
{
    public class StrHelper
    {
        public const Boolean BREAK = false;
        public const Boolean CONTINUE = true;

        public static String ToNumberWithComma(String value)
        {
            try
            {
                if (value == "") value = "0";
                int val = int.Parse(value);
                return String.Format("{0:#,##0}", val);
            }
            catch (Exception ex)
            {
                return value;
            }
        }

        public static String LeftH(String Target, int Length)
        {
            // SubstringH를 사용하면 마지막 한글이 짤려서 깨지는 현상이 발생함.
            // 이런 현상을 개선한 함수임.
            String ret = "";
            for (int i = 0; i < Target.Length; i++)
            {
                String tmpS = Target.Substring(i, 1); // 덧붙일 문자 1개
                int lenH = LengthH(ret + tmpS); // 덧붙인 후 길이를 구한다(한글 2바이트로 계산).
                if (lenH <= Length) ret += tmpS; // 덧붙인 후 반환할 길이보다 작거나 같으면 반환할 문자열에 포함시킨다.
                else ret += " ";
                if (LengthH(ret) >= Length) break; // 자르고자 하는 길이에 도달하면 그만 한다.
            }
            return ret;
        }

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

        public static String RSubstringH(String Target, int Start, int Length)
        {
            // 한글을 2바이트로 처리하여 자른다.
            byte[] byteTarget = Encoding.GetEncoding("korean").GetBytes(Target);
            if (byteTarget.Length < Length) Length = byteTarget.Length;
            if (Length == 0)
            {
                // 끝까지
                return Encoding.GetEncoding("korean").GetString(byteTarget, byteTarget.Length - Length - Start, byteTarget.Length - Start);
            }
            else
            {
                return Encoding.GetEncoding("korean").GetString(byteTarget, byteTarget.Length - Length - Start, Length);
            }
        }

        public static int LengthH(String Target)
        {
            byte[] byteTarget = Encoding.GetEncoding("korean").GetBytes(Target);
            return byteTarget.Length;
        }

        public static double ToDouble(String Value)
        {
            double result = 0;
            double.TryParse(Value, out result);
            return result;
        }

        public static long ToLong(String Value)
        {
            double result = 0;
            double.TryParse(Value, out result);
            long ret = 0;
            long.TryParse(result.ToString(), out ret);
            return ret;
        }

        public static double Round(string Value)
        {
            return Round(Value, 0);
        }

        public static double Round(string Value, int decimals)
        {
            double dValue = ToDouble(Value);
            return Math.Round(dValue, decimals);
        }
    }
}
