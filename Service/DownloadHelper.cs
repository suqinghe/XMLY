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
            FileHelper.MakeDirectory(Application.StartupPath + "\\" + tf.Album + "\\");
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


        // 喜马拉雅音频下载器.Form1
        public static void SaveList(List<SynFileInfo> lst)
        {
            var xmlPath = string.Format("{0}/{1}/{2}.xml", Application.StartupPath, "Download", DateTime.Now.ToString("yyyyMMdd"));

            var fl = new FileInfo(xmlPath);
            FileHelper.MakeDirectory(fl.DirectoryName);

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
                fileStream.Close();
            }
        }


        // 喜马拉雅音频下载器.Form1
        public static List<SynFileInfo> LoadList(string fileName)
        {
            var lst = new List<SynFileInfo>();
            var xmlPath = string.Format("{0}/{1}/{2}.xml", Application.StartupPath, "Download", DateTime.Now.ToString("yyyyMMdd"));

            FileStream fileStream = new FileStream(xmlPath, FileMode.OpenOrCreate);
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(lst.GetType());
                lst = (List<SynFileInfo>)xmlSerializer.Deserialize(fileStream);
                fileStream.Close();
                //foreach (SynFileInfo current in this.m_DownloadList)
                //{
                //    int index = this.dataGridView1.Rows.Add();
                //    DataGridViewRow dataGridViewRow = this.dataGridView1.Rows[index];
                //    dataGridViewRow.Cells["DocID"].Value = current.DocID;
                //    dataGridViewRow.Cells["Album"].Value = current.Album;
                //    dataGridViewRow.Cells["DocName"].Value = current.DocName;
                //    if (current.isComplete)
                //    {
                //        dataGridViewRow.Cells["SynProgress"].Value = "完成";
                //    }
                //    current.RowObject = dataGridViewRow;
                //}
            }
            catch (Exception exp)
            {
                fileStream.Close();
            }

            return lst;
        }

    }
}
