using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XMLY
{
    public static class FileHelper
    {


        /// <summary>
        /// 保存专辑
        /// </summary>
        /// <param name="album"></param>
        public static void SaveAlbum(string album)
        {
            var path = Application.StartupPath + "\\下载列表.txt";
            File.AppendAllText(path, string.Format("{0}\t{1}\r\n", album, DateTime.Now.ToString("yyyy-MM-dd")));
        }



        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="dir"></param>
        public static void MakeDirectory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }




        /// <summary>
        /// 名称过滤
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FixFileName(string s)
        {
            string text = "\\/:*?\"<>|'";
            char[] array = text.ToCharArray();
            char newChar = '_';
            char[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                char oldChar = array2[i];
                s = s.Replace(oldChar, newChar);
            }
            return s;
        }

        /// <summary>
        /// 设置保存路径
        /// </summary>
        /// <param name="path"></param>
        public static void SetSavePath(string path)
        {
            var fl = new FileInfo(Application.StartupPath + "/setting.ini");

            MakeDirectory(fl.DirectoryName);

            if (!File.Exists(fl.FullName))
            {
                using (StreamWriter sw = File.CreateText(fl.FullName))
                {
                    sw.WriteLine(path);
                    sw.Close();
                }
            }
            else
            {
                using (StreamWriter w = File.AppendText(fl.FullName))
                {
                    w.WriteLine(path);
                    w.Close();
                }
            }

        }


        private static string SavePath = string.Empty;
        /// <summary>
        /// 读取保存路径
        /// </summary>
        /// <returns></returns>
        public static string GetSavePath()
        {
            if (!string.IsNullOrWhiteSpace(SavePath))
                return SavePath;

            var path = Application.StartupPath;

            var fl = new FileInfo(Application.StartupPath + "/setting.ini");
            if (fl.Exists)
            {
                var temp = File.ReadAllText(fl.FullName);
                if (!string.IsNullOrWhiteSpace(temp))
                {
                    var newFl = new DirectoryInfo(temp);
                    if (!newFl.Exists)
                        MakeDirectory(newFl.FullName);

                    path = newFl.FullName;

                    SavePath = path;
                }

            }
            return path;
        }
    }
}
