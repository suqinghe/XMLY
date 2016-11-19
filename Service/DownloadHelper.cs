using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

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
            tf.AlbumId = jToken["album_id"].ToString();
            FileHelper.MakeDirectory(FileHelper.GetSavePath() + "\\" + tf.Album + "\\");
            tf.SavePath = string.Concat(new string[]
            {
                FileHelper.GetSavePath(),
                "\\",
                tf.Album,
                "\\",
                tf.DocName,
                ".mp3"
            });
        }


        // 喜马拉雅音频下载器.Form1
        public static string SaveList(List<SynFileInfo> lst, string xmlPath = null)
        {
            if (string.IsNullOrWhiteSpace(xmlPath))
                xmlPath = string.Format("{0}/{1}/{2}.xml", Application.StartupPath, "Download", DateTime.Now.ToString("yyyyMMdd"));

            var fl = new FileInfo(xmlPath);
            FileHelper.MakeDirectory(fl.DirectoryName);

            lst.ForEach(a => a.XmlName = DateTime.Now.ToString("yyyyMMdd"));

            var existList = new List<SynFileInfo>();
            if (fl.Exists)
            {
                existList = LoadList(xmlPath);
            }

            var newList = lst.Union(existList, new SynFileInfoComPare()).ToList();
            FileStream fileStream = new FileStream(xmlPath, FileMode.Create);
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SynFileInfo>));
                xmlSerializer.Serialize(fileStream, newList);
                fileStream.Close();
            }
            catch (Exception exp)
            {
                LogHelper.WriteLog("保存类别出错：" + exp.Message);
                fileStream.Close();
            }

            return xmlPath;
        }


        // 喜马拉雅音频下载器.Form1
        public static List<SynFileInfo> LoadList(string xmlPath, string xmlName = null)
        {
            var lst = new List<SynFileInfo>();

            if (string.IsNullOrWhiteSpace(xmlPath) && !string.IsNullOrWhiteSpace(xmlName))
            {
                xmlPath = string.Format("{0}/{1}/{2}.xml", Application.StartupPath, "Download", xmlName);
            }

            try
            {
                using (FileStream fileStream = new FileStream(xmlPath, FileMode.OpenOrCreate))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(lst.GetType());
                    lst = (List<SynFileInfo>)xmlSerializer.Deserialize(fileStream);
                    fileStream.Close();
                }
            }
            catch (Exception exp)
            {
                LogHelper.WriteLog("加载列表出错：" + exp.Message);
            }

            return lst;
        }

        /// <summary>
        /// 更新列表
        /// </summary>
        /// <param name="m_DownloadList"></param>
        internal static void UpdateList(List<SynFileInfo> m_DownloadList)
        {
            var albumId = m_DownloadList.FirstOrDefault().AlbumId;

            foreach (var xmlName in m_DownloadList.Select(a => a.XmlName).Distinct())
            {
                var fullPath = string.Format("{0}/{1}/{2}.xml", Application.StartupPath, "Download", xmlName);

                var fl = new FileInfo(fullPath);
                if (!fl.Exists)
                    continue;

                var subItems = LoadList(fullPath);

                var completeIds = m_DownloadList.Where(a => a.isComplete).Select(a => a.DocID).ToList();

                subItems.Where(a => a.AlbumId == albumId && completeIds.Contains(a.DocID)).ToList().ForEach(a => a.isComplete = true);

                SaveList(subItems, fullPath);
            }
        }

        internal static List<SynFileInfo> LoadAlbumList(string albumId)
        {
            try
            {
                var albId = albumId.Split('_')[0].ToString();
                var xmlId = albumId.Split('_')[1].ToString();

                var allList = LoadList(string.Empty, xmlId);

                var ablumList = allList.Where(a => a.AlbumId == albId).ToList();
                return ablumList;
            }
            catch (Exception exp)
            {
                LogHelper.WriteLog("加载专辑列表失败：" + exp.Message);
                return null;
            }
        }
    }
}
