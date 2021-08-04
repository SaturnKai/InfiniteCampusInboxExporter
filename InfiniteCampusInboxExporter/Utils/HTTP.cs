using System;
using System.IO;
using System.Net;
using System.Text;

namespace InfiniteCampusInboxExporter.Utils
{
    class HTTP
    {
        public static string Get(string url, ref CookieContainer cookies)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.UserAgent = "InfiniteCampusInboxExporter/1.0 Developed By: Brandon";
                request.CookieContainer = cookies;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                cookies.Add(response.Cookies);
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string responseSource = streamReader.ReadToEnd();
                streamReader.Dispose();
                return responseSource;
            }
            catch (Exception e)
            {
                return "Error: " + e;
            }
        }

        public static string Post(string url, string postData, ref CookieContainer cookies)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                request.UserAgent = "InfiniteCampusInboxExporter/1.0 Developed By: Brandon";
                request.CookieContainer = cookies;
                Stream postDataStream = request.GetRequestStream();
                postDataStream.Write(Encoding.ASCII.GetBytes(postData), 0, postData.Length);
                postDataStream.Dispose();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                cookies.Add(response.Cookies);
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string responseSource = streamReader.ReadToEnd();
                streamReader.Dispose();
                return responseSource;
            }
            catch (Exception e)
            {
                return "Error: " + e;
            }
        }
    }
}
