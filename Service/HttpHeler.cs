using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SD.Net;

namespace XMLY
{
    //internal class HttpHelper
    //{
    //    private WebClient wc = new WebClient();
    //}
    public static class HttpHelper
    {
        /// <summary>
        /// 发起HTTP请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            try
            {
                var http = HttpClient.Create();
                var txt1 = http.Go(url).GetResponse().ToText();
                return txt1;
                //WebClient webClient = new WebClient();
                //byte[] bytes = webClient.DownloadData(url);
                //return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static T HttpGet<T>(string url)
        {
            try
            {
                var http = HttpClient.Create();
                var txt1 = http.Go(url).GetResponse().ToType<T>();
                return txt1;
                //WebClient webClient = new WebClient();
                //byte[] bytes = webClient.DownloadData(url);
                //return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception)
            {
                return default(T);
            }

        }
    }



}
