using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            WebClient webClient = new WebClient();
            byte[] bytes = webClient.DownloadData(url);
            return Encoding.UTF8.GetString(bytes);
        }

    }



}
