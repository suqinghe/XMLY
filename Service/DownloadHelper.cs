using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace XMLY
{
    /// <summary>
    /// 下载帮助类
    /// </summary>
    public static class DownloadHelper
    {

        /// <summary>
        /// 获取下载JSON信息
        /// </summary>
        /// <param name="tf"></param>
        public static void GetJsonInfo(SynFileInfo tf)
        {
            string value = HttpHelper.HttpGet("http://www.ximalaya.com/tracks/" + tf.DocID + ".json");
            JToken jToken = JsonConvert.DeserializeObject<JToken>(value);
            tf.DownPath = jToken["play_path_64"].ToString();
            if (tf.DownPath.IndexOf("http://") < 0)
            {
                tf.DownPath = "http://fdfs.xmcdn.com/" + jToken["play_path_64"].ToString();
            }
            string value2 = jToken["duration"].ToString();
            tf.DocName = FileHelper.FixFileName(jToken["title"].ToString());
            tf.Album = FileHelper.FixFileName(jToken["album_title"].ToString());
            tf.duration = (int)Convert.ToDouble(value2);
            FileHelper.mkdir(Application.StartupPath + "\\" + tf.Album + "\\");
            tf.SavePath = string.Concat(new string[]
            {
                Application.StartupPath,
                "\\",
                tf.Album,
                "\\",
                tf.DocName,
                ".mp3"
            });
        }


    }
}
