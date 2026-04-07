using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ADD_EDI_SANJE
{
    class CUtil
    {
        private const string HOST = "apix.comwel.or.kr";
        private const int PORT = 443;
        private const string SCHEMA = "https";

        // replace with your own accessKey
        private const string API_KEY = "****";

        // replace with your own secretKey
        private const string PIN_CD = "****";
        private const string HOSPITAL_NO = "****"; // 요양기관기호
        private const string SAEOPJA_DRNO = "****"; // 사업자번호

        public static string CallAPI(string path, string strJson)
        {
            string method = "POST";

            HttpWebRequest request = null;
            HttpWebResponse response = null;

            string url = string.Format("{0}://{1}:{2}{3}", SCHEMA, HOST, PORT, path);

            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = "application/json";
            request.Accept = "application/json";

            /*****************************************************************/
            // Java 코드에는 authorization 변수가 선언되지 않았습니다.
            // 필요 시 HMAC 계산 후 아래 헤더 추가
            // string authorization = "...";
            // Console.WriteLine(authorization);
            // request.Headers.Add("Authorization", authorization);
            /*****************************************************************/

            // set header
            request.Headers.Add("API_KEY", API_KEY);
            request.Headers.Add("PIN_CD", PIN_CD);
            request.Headers.Add("HOSPITAL_NO", HOSPITAL_NO);
            request.Headers.Add("SAEOPJA_DRNO", SAEOPJA_DRNO);

            byte[] postBytes = Encoding.UTF8.GetBytes(strJson);
            request.ContentLength = postBytes.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(postBytes, 0, postBytes.Length);
            }

            response = (HttpWebResponse)request.GetResponse();

            Console.WriteLine("status code: " + (int)response.StatusCode);
            Console.WriteLine("status message: " + response.StatusDescription);

            string result = "";
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
                Console.WriteLine("result: " + result);
            }

            return result;
        }

        public static byte[] ReadFileToBytes(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] buffer = new byte[8192];
                    int read;

                    while ((read = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }

                    return ms.ToArray();
                }
            }
        }


    }
}
