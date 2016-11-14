using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace XMLY
{
    public partial class MainForm : Form
    {
        private List<SynFileInfo> m_DownloadList = new List<SynFileInfo>();


        public MainForm()
        {
            InitializeComponent();
        }


        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int connectionDescription, int reservedValue);


        private bool isConnected()
        {
            int num = 0;
            return MainForm.InternetGetConnectedState(out num, 0);
        }

        private void showinfo(string txt)
        {
            this.lblInfo.Text = txt;
            Application.DoEvents();
        }

        private void GetJsonInfo(SynFileInfo tf)
        {
            string value = this.HttpGet("http://www.ximalaya.com/tracks/" + tf.DocID + ".json");
            JToken jToken = JsonConvert.DeserializeObject<JToken>(value);
            tf.DownPath = jToken["play_path_64"].ToString();
            if (tf.DownPath.IndexOf("http://") < 0)
            {
                tf.DownPath = "http://fdfs.xmcdn.com/" + jToken["play_path_64"].ToString();
            }
            string value2 = jToken["duration"].ToString();
            tf.DocName = this.FixFileName(jToken["title"].ToString());
            tf.Album = this.FixFileName(jToken["album_title"].ToString());
            tf.duration = (int)Convert.ToDouble(value2);
            this.mkdir(Application.StartupPath + "\\" + tf.Album + "\\");
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

        private string lastAlbum = "";

        private void StartNewWork()
        {
            try
            {
                SynFileInfo next = this.GetNext();
                if (next == null)
                {
                    MessageBox.Show("所有文件下载完毕!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                this.showinfo("分析:" + next.DocName);
                this.GetJsonInfo(next);
                next.RowObject.Cells["duration"].Value = (next.duration / 60).ToString() + "分钟";
                this.showinfo("下载:" + next.DocName);


                //如果专辑名称不一样，则保存起来
                if (next.Album != lastAlbum)
                {
                    lastAlbum = next.Album;
                    this.SaveAlbum(lastAlbum);
                }


                this.StartDownload(next);
            }
            catch (Exception exp)
            {
                var content = "下载出现错误，请重试！若仍有问题，请联系作者！";
                LogHelper.WriteLog("StartNewWork" + content + exp.Message);
                this.showinfo(content);
            }
        }
        private void SaveAlbum(string album)
        {
            var path = Application.StartupPath + "\\下载列表.txt";
            File.AppendAllText(path, string.Format("{0}\t{1}\r\n", album, DateTime.Now.ToString("yyyy-MM-dd")));
        }

        private void StartDownload(SynFileInfo sfi)
        {
            try
            {
                sfi.LastTime = DateTime.Now;
                WebClient webClient = new WebClient();
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.client_DownloadProgressChanged);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.client_DownloadFileCompleted);
                webClient.DownloadFileAsync(new Uri(sfi.DownPath), sfi.SavePath, sfi);
            }
            catch (Exception exp)
            {
                var content = "下载出现错误，请重试！若仍有问题，请联系作者！";
                LogHelper.WriteLog("StartDownload" + content + exp.Message);
                this.showinfo(content);
            }
        }
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                SynFileInfo synFileInfo = (SynFileInfo)e.UserState;
                synFileInfo.SynProgress = e.ProgressPercentage + "%";
                double totalSeconds = (DateTime.Now - synFileInfo.LastTime).TotalSeconds;
                synFileInfo.SynSpeed = FileOperate.GetAutoSizeString(Convert.ToDouble((double)e.BytesReceived / totalSeconds), 2) + "/s";
                if (synFileInfo.RowObject.Cells["FileSize"].Value == null)
                {
                    synFileInfo.FileSize = e.TotalBytesToReceive;
                    synFileInfo.RowObject.Cells["FileSize"].Value = FileOperate.GetAutoSizeString((double)e.TotalBytesToReceive, 2);
                }
                synFileInfo.RowObject.Cells["SynProgress"].Value = synFileInfo.SynProgress;
                synFileInfo.RowObject.Cells["SynSpeed"].Value = synFileInfo.SynSpeed;
            }
            catch (Exception exp)
            {
                var content = "下载出现错误，请重试！若仍有问题，请联系作者！";
                LogHelper.WriteLog("client_DownloadProgressChanged" + content + exp.Message);
                this.showinfo(content);
            }
        }


        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            SynFileInfo synFileInfo = (SynFileInfo)e.UserState;
            synFileInfo.isComplete = true;
            this.StartNewWork();
        }

        private void mkdir(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        private string HttpGet(string url)
        {
            WebClient webClient = new WebClient();
            byte[] bytes = webClient.DownloadData(url);
            return Encoding.UTF8.GetString(bytes);
        }

        private SynFileInfo GetNext()
        {
            foreach (SynFileInfo current in this.m_DownloadList)
            {
                if (!current.isComplete)
                {
                    return current;
                }
            }
            return null;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.m_downlist.Rows.Clear();
            this.m_DownloadList.Clear();
        }



        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.isConnected())
                {
                    MessageBox.Show("未连接网络!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                this.showinfo("正在获取...");
                if (this.txtUrl.Text.Contains("/sound/"))
                {
                    AnalyzeSigleDoc();
                }
                else
                {
                    AnalyzeAlbum();
                }
            }
            catch (Exception exp)
            {
                var content = " 内容解析失败，请查看链接地址是否正确！若重试仍有问题，请联系作者！";
                LogHelper.WriteLog("btnAnalyze_Click" + content + exp.Message);
                this.showinfo(content);
            }
        }

        /// <summary>
        /// 分析整张专辑
        /// </summary>
        private void AnalyzeAlbum()
        {
            try
            {
                //DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                //this.m_downlist.Columns.Insert(0, chk);
                //chk.HeaderText = "选择";
                //chk.Name = "chk";

                WebClient webClient = new WebClient();
                byte[] bytes = webClient.DownloadData(this.txtUrl.Text);
                string @string = Encoding.UTF8.GetString(bytes);
                this.showinfo("完成!");
                string value = Regex.Match(@string, "<h1>([\\w\\W]*?)</h1>").Groups[1].Value;
                string pattern = "(?i)<li sound_id=\"(\\d*)\" class=\"\">[\\w\\W]*?hashlink[\\w\\W]*?>([\\w\\W]*?)</a>";// "sound_ids=\".*\">";//
                MatchCollection matchCollection = Regex.Matches(@string, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                foreach (Match match in matchCollection)
                {
                    string[] values2 = new string[]
                       {
                    match.Groups[1].Value.Trim(),
                    value,
                    match.Groups[2].Value.Trim()
                       };

                    var dgvRow = new DataGridViewRow();

                 

                    int index2 = this.m_downlist.Rows.Add(values2);
                    DataGridViewRow rowObject2 = this.m_downlist.Rows[index2];
                    SynFileInfo item = new SynFileInfo
                    {
                        DocID = match.Groups[1].Value.Trim(),
                        DocName = match.Groups[2].Value.Trim(),
                        RowObject = rowObject2,
                        Album = value
                    };
                    this.m_DownloadList.Add(item);
                }
                this.showinfo("共" + this.m_DownloadList.Count.ToString());
            }
            catch (Exception exp)
            {
                var content = "内容解析失败，请查看链接地址是否正确！";
                LogHelper.WriteLog(content + exp.Message);
                this.showinfo(content);
            }
        }

        /// <summary>
        /// 分析单个
        /// </summary>
        private void AnalyzeSigleDoc()
        {
            string text = this.txtUrl.Text;
            string docID = text.Substring(text.IndexOf("sound/") + 6);
            SynFileInfo synFileInfo = new SynFileInfo();
            synFileInfo.DocID = docID;
            this.GetJsonInfo(synFileInfo);
            string[] values = new string[]
            {
                    synFileInfo.DocID,
                    synFileInfo.Album,
                    synFileInfo.DocName
            };
            int index = this.m_downlist.Rows.Add(values);
            DataGridViewRow rowObject = this.m_downlist.Rows[index];

            synFileInfo.RowObject = rowObject;
            synFileInfo.RowObject.Cells["duration"].Value = (synFileInfo.duration / 60).ToString() + "分钟";

            this.m_DownloadList.Add(synFileInfo);

            return;
        }

        private string FixFileName(string s)
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



        private void btnDownload_Click(object sender, EventArgs e)
        {
            this.StartNewWork();
        }

        private void txtUrl_TextChanged(object sender, EventArgs e)
        {
            string text = Clipboard.GetText();
            if (text.StartsWith("http://www.ximalaya.com/"))
            {
                this.txtUrl.Text = text;
            }
        }

        /// <summary>
        /// 打开所在目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenExplorer_Click(object sender, EventArgs e)
        {
            var path = Application.StartupPath;
            System.Diagnostics.Process.Start("Explorer.exe", path);
        }

        /// <summary>
        /// 清空下载列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.m_downlist.Rows.Clear();
            this.m_DownloadList.Clear();
        }

        private void m_downlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (Convert.ToString(m_downlist.Rows[e.RowIndex].Cells[0].Value) == "true")
                    m_downlist.Rows[e.RowIndex].Cells[0].Value = "false";
                else
                    m_downlist.Rows[e.RowIndex].Cells[0].Value = "true";
            }
        }
    }
}
