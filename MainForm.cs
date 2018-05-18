using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using XMLY.Model;

namespace XMLY
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region 局部变量

        /// <summary>
        /// 下载列表
        /// </summary>
        private List<SynFileInfo> m_DownloadList = new List<SynFileInfo>();

        /// <summary>
        /// 上个专辑名称
        /// </summary>
        private string lastAlbum = "";

        private string placeHolder = "请在此处输入专辑链接，多个链接请换行";

        private bool IsDownload = true;

        #endregion 局部变量

        #region 监测连接状态

        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int connectionDescription, int reservedValue);

        private bool isConnected()
        {
            int num = 0;
            return MainForm.InternetGetConnectedState(out num, 0);
        }

        #endregion 监测连接状态

        #region 界面信息显示

        /// <summary>
        /// 显示提示信息
        /// </summary>
        /// <param name="txt"></param>
        private void showinfo(string txt)
        {
            this.lblInfo.Text = txt;
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            Application.DoEvents();
        }

        #endregion 界面信息显示

        #region 内容列表选择

        /// <summary>
        /// 下载列表内容选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_downlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                m_downlist.Rows[e.RowIndex].Cells[0].Value = !Convert.ToBoolean(m_downlist.Rows[e.RowIndex].Cells[0].Value);
                this.m_DownloadList[e.RowIndex].Selected = false;
            }
        }

        #endregion 内容列表选择

        #region 下载部分

        /// <summary>
        /// 开始下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownload_Click(object sender, EventArgs e)
        {
            this.IsDownload = true;

            this.StartNewWork();
        }

        /// <summary>
        /// 开始下载
        /// </summary>
        /// <param name="sfi"></param>
        private void StartDownload(SynFileInfo sfi)
        {
            try
            {
                if (sfi.DownPath == null)
                {
                    this.showinfo("下载地址不正确，该音频可能为付费音频，不能下载！");

                    sfi.isComplete = true;

                    var repeatRow = this.m_downlist.Rows.Cast<DataGridViewRow>().FirstOrDefault(a => a.Cells["DocId"].Value.ToString() == sfi.DocID);
                    if (repeatRow != null)
                    {
                        repeatRow.DefaultCellStyle.ForeColor = Color.Red;
                    }

                    this.StartNewWork();
                }
                sfi.LastTime = DateTime.Now;
                WebClient webClient = new WebClient();
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.client_DownloadProgressChanged);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.client_DownloadFileCompleted);
                webClient.DownloadFileAsync(new Uri(sfi.DownPath), sfi.SavePath, sfi);
            }
            catch (Exception exp)
            {
                var content = "下载出现错误，该音频可能为付费音频，不能下载！若仍有问题，请联系作者！";
                LogHelper.WriteLog("StartDownload" + content + exp.Message);
                this.showinfo(content);
                return;
            }
        }

        /// <summary>
        /// 下载进度更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 下载完成提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            SynFileInfo synFileInfo = (SynFileInfo)e.UserState;
            synFileInfo.isComplete = true;

            var repeatRow = this.m_downlist.Rows.Cast<DataGridViewRow>().FirstOrDefault(a => a.Cells["DocId"].Value.ToString() == synFileInfo.DocID);
            if (repeatRow != null)
            {
                repeatRow.DefaultCellStyle.ForeColor = Color.Green;
            }

            //该张专辑全部下载完后，字体变绿
            if (m_DownloadList.Where(a => a.AlbumId == synFileInfo.AlbumId).All(a => a.isComplete))
            {
                var parentNode = FindPendingNode();
                var nodes = parentNode.Nodes.Find(synFileInfo.AlbumId, false);
                if (nodes.Count() > 0)
                {
                    var currentNode = nodes.FirstOrDefault();
                    currentNode.ForeColor = Color.Green;
                }

                var albumItems = this.m_DownloadList.Where(a => a.AlbumId == synFileInfo.AlbumId).ToList();
                DownloadHelper.UpdateList(albumItems);
            }

            this.StartNewWork();
        }

        /// <summary>
        /// 获取下一个下载信息
        /// </summary>
        /// <returns></returns>
        private SynFileInfo GetNext()
        {
            if (!IsDownload)
                return null;

            foreach (SynFileInfo current in this.m_DownloadList)
            {
                if (current.Selected && !current.isComplete)
                {
                    return current;
                }
            }
            return null;
        }

        /// <summary>
        /// 开始新的下载
        /// </summary>
        private void StartNewWork()
        {
            try
            {
                SynFileInfo next = this.GetNext();
                if (next == null)
                {
                    var msg = "所有文件下载完毕!";
                    if (!IsDownload)
                    {
                        msg = "已停止下载";
                    }
                    MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                this.showinfo("分析:" + next.DocName);
                DownloadHelper.GetJsonInfo(next);
                next.RowObject.Cells["duration"].Value = (next.duration / 60).ToString() + "分钟";
                this.showinfo("下载:" + next.DocName);

                //如果专辑名称不一样，则保存起来
                if (next.Album != lastAlbum)
                {
                    lastAlbum = next.Album;
                    FileHelper.SaveAlbum(lastAlbum);
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

        /// <summary>
        /// 添加文件到列表
        /// </summary>
        /// <param name="lst"></param>
        private void AddFileToGridDataView(List<SynFileInfo> lst)
        {
            foreach (var item in lst)
            {
                var repeatRow = this.m_downlist.Rows.Cast<DataGridViewRow>().FirstOrDefault(a => a.Cells["DocId"].Value.ToString() == item.DocID);
                if (repeatRow != null)
                    continue;

                int index = this.m_downlist.Rows.Add();
                DataGridViewRow row = this.m_downlist.Rows[index];

                row.Cells["DocId"].Value = item.DocID;
                row.Cells["Album"].Value = item.Album;
                //row.Cells["AlbumId"].Value = item.AlbumId;
                row.Cells["DocName"].Value = item.DocName;
                row.Cells["SynProgress"].Value = item.isComplete ? "100%" : "0";
                //row.Cells["FileSize"].Value = item.FileSize;
                //row.Cells["isComplete"].Value = item.isComplete;
                //row.Cells["DownPath"].Value = item.DownPath;
                //row.Cells["SavePath"].Value = item.SavePath;
                //row.Cells["duration"].Value = (item.duration / 60).ToString() + "分钟";
                row.Cells["Selected"].Value = true;
                item.Selected = true;

                if (item.isComplete)
                    row.DefaultCellStyle.ForeColor = Color.Green;

                item.RowObject = row;
                //this.m_downlist.Rows.Add(item.RowObject);
            }
        }

        /// <summary>
        /// 停止下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_stop_down_Click(object sender, EventArgs e)
        {
            showinfo("已准备停止，下载完当前声音后会自动停止，请耐心等待……");
            IsDownload = false;
            DownloadHelper.UpdateList(this.m_DownloadList);
        }

        /// <summary>
        /// 保存清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_list_Click(object sender, EventArgs e)
        {
            var path = DownloadHelper.SaveList(this.m_DownloadList);

            if (!string.IsNullOrWhiteSpace(path))
                MessageBox.Show("列表保存成功，保存路径为：" + path, "列表保存提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion 下载部分

        #region 清空操作

        /// <summary>
        /// 清空下载列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.m_downlist.Rows.Clear();
            this.m_DownloadList.Clear();
            this.txtUrl.Text = placeHolder;
            this.txtUrl.ForeColor = System.Drawing.Color.Gray;
            this.lblInfo.Text = string.Empty;

            //把即将下载的节点下的子节点清除
            var parentNode = FindPendingNode();
            parentNode.Nodes.Clear();
        }

        #endregion 清空操作

        #region URL地址栏操作

        /// <summary>
        /// 点击URL栏目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUrl_Click(object sender, EventArgs e)
        {
            if (txtUrl.Text.Equals(placeHolder))
            {
                this.txtUrl.Text = string.Empty;
                this.txtUrl.ForeColor = System.Drawing.Color.Black;
            }
        }

        /// <summary>
        /// 离开URL栏目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUrl_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUrl.Text))
            {
                this.txtUrl.Text = placeHolder;
                this.txtUrl.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void txtUrl_TextChanged(object sender, EventArgs e)
        {
            string text = Clipboard.GetText();
            if (text.StartsWith("http://www.ximalaya.com/"))
            {
                this.txtUrl.Text = text;
            }
        }

        #endregion URL地址栏操作

        #region 分析下载部分

        /// <summary>
        /// 分析按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.isConnected())
                {
                    MessageBox.Show("未连接网络!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                this.showinfo("正在获取内容...");

                var url = this.txtUrl.Text.Trim();

                if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(url.Replace(placeHolder, "")))
                {
                    this.showinfo("链接地址不能为空");
                }
                else if (!url.StartsWith("http"))
                {
                    this.showinfo("链接地址不正确");
                }
                else
                {
                    var lstUrl = new List<string>();
                    if (url.Contains("\r\n"))
                    {
                        foreach (var subItem in url.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (!lstUrl.Contains(subItem))
                                lstUrl.Add(subItem);
                        }
                    }
                    else
                    {
                        lstUrl.Add(url);
                    }

                    foreach (var item in lstUrl)
                    {
                        AnalyzeAlbum(item);
                    }

                    this.AddToPendingList(this.m_DownloadList);
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
        private void AnalyzeAlbum(string link)
        {
            try
            {
                var lstSynFileInfo = new List<SynFileInfo>();
                var albumId = 0;

                var ids = link.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                //专辑
                if (ids.Count == 5)
                {
                    if (ids.Count() > 0)
                        albumId = int.Parse(ids.LastOrDefault());

                    var albumUrl = $@"http://www.ximalaya.com/revision/album?albumId={albumId}";

                    var albumTitle = string.Empty;
                    var totalTrack = 0;
                    var pageSize = 30;
                    var totalPage = 0;

                    var albumInfo = HttpHelper.HttpGet<AlbumInfo>(albumUrl);

                    if (albumInfo != null && albumInfo.data != null)
                    {
                        albumId = albumInfo.data.albumId;

                        if (albumInfo.data.mainInfo != null)
                            albumTitle = albumInfo.data.mainInfo.albumTitle;

                        if (albumInfo.data.tracksInfo != null)
                        {
                            totalTrack = albumInfo.data.tracksInfo.trackTotalCount;
                            totalPage = totalTrack / pageSize + 1;
                        }
                    }

                    for (int i = 1; i <= totalPage; i++)
                    {
                        var pageAlbumUrl = $"http://www.ximalaya.com/revision/album/getTracksList?albumId={albumId}&pageNum={i}";

                        var pageAlbumInfo = HttpHelper.HttpGet<PageAlbumInfo>(pageAlbumUrl);

                        if (pageAlbumInfo != null && pageAlbumInfo.data != null && pageAlbumInfo.data.tracks != null && pageAlbumInfo.data.tracks.Count > 0)
                        {
                            foreach (var track in pageAlbumInfo.data.tracks)
                            {
                                if (!this.m_DownloadList.Any(a => a.DocID == track.trackId.ToString()))
                                {
                                    int index2 = this.m_downlist.Rows.Add(new string[] { "true", track.trackId.ToString(), albumTitle, track.title });
                                    DataGridViewRow rowObject2 = this.m_downlist.Rows[index2];
                                    SynFileInfo item = new SynFileInfo
                                    {
                                        DocID = track.trackId.ToString(),
                                        DocName = track.title.Trim(),
                                        RowObject = rowObject2,
                                        Album = albumTitle,
                                        Selected = true,
                                        AlbumId = albumId.ToString()
                                    };
                                    this.m_DownloadList.Add(item);
                                }
                            }
                        }
                    }
                }
                else
                {
                    var trackId = int.Parse(ids.LastOrDefault());
                    AnalyzeSigleDoc(trackId);
                }

                this.showinfo("共" + this.m_DownloadList.Count.ToString() + "条声音");
            }
            catch (Exception exp)
            {
                var content = "内容解析失败，请查看链接地址是否正确！";
                LogHelper.WriteLog(content + exp.Message);
                this.showinfo(content);
            }
        }

        private void AddToPendingList(List<SynFileInfo> lst)
        {
            var items = from p in lst
                        group p by p.Album
                     into g
                        select new
                        {
                            Name = g.Key,
                            AlbumId = g.FirstOrDefault().AlbumId,
                            IsAllComplete = g.All(a => a.isComplete),
                            XmlName = g.FirstOrDefault().XmlName
                        };

            var parentNode = FindPendingNode();
            foreach (var item in items)
            {
                var key = string.Format("{0}_{1}", item.AlbumId, item.XmlName);
                if (!parentNode.Nodes.ContainsKey(key))
                {
                    var addNode = parentNode.Nodes.Add(key, item.Name);
                    if (item.IsAllComplete)
                        addNode.ForeColor = Color.Green;
                }
            }
        }

        private TreeNode FindPendingNode()
        {
            var allNodes = this.tvPendingDownload.Nodes.Find("pendingDown", false);
            var parentNode = allNodes.FirstOrDefault();
            parentNode.Expand();

            return parentNode;
        }

        private void AddToPendingList(SynFileInfo info)
        {
            var parentNode = FindPendingNode();
            var key = string.Format("{0}_{1}", info.AlbumId, info.XmlName);
            if (!parentNode.Nodes.ContainsKey(key))
            {
                parentNode.Nodes.Add(key, info.Album);
            }
        }

        /// <summary>
        /// 分析单个
        /// </summary>
        private void AnalyzeSigleDoc(int trackId)
        {
            if (trackId <= 0)
                return;

            string trackUrl = $"http://www.ximalaya.com/revision/play/tracks?trackIds={trackId}";

            SynFileInfo tf = new SynFileInfo { Selected = true };

            var soundInfo = HttpHelper.HttpGet<TrackInfos>(trackUrl);

            if (soundInfo != null && soundInfo.data != null && soundInfo.data.tracksForAudioPlay != null && soundInfo.data.tracksForAudioPlay.Count > 0)
            {
                var downInfo = soundInfo.data.tracksForAudioPlay.FirstOrDefault();
                tf.DownPath = downInfo.src;
                tf.DocID = trackId.ToString();
                tf.DocName = downInfo.trackName;
                tf.Album = downInfo.albumName;
                tf.duration = downInfo.duration;
                tf.AlbumId = downInfo.albumId.ToString();
                var savePath = FileHelper.GetSavePath();
                FileHelper.MakeDirectory(savePath + "\\" + tf.Album + "\\");
                tf.SavePath = $@"{savePath}\{tf.Album}\{tf.DocName}.mp3";
            }

            string[] values = new string[]
            {
                    "true",
                    tf.DocID,
                    tf.Album,
                    tf.DocName
            };

            //列表中不存在，才进行添加
            if (!this.m_DownloadList.Any(a => a.DocID == tf.DocID))
            {
                int index = this.m_downlist.Rows.Add(values);
                DataGridViewRow rowObject = this.m_downlist.Rows[index];

                tf.RowObject = rowObject;
                tf.RowObject.Cells["duration"].Value = (tf.duration / 60).ToString() + "分钟";

                this.m_DownloadList.Add(tf);
            }
            else
            {
                this.showinfo("列表中已经存在" + tf.DocID + "的声音");
            }

            AddToPendingList(tf);

            return;
        }

        #endregion 分析下载部分

        #region 关于部分

        /// <summary>
        /// 关于作者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmi_about_author_Click(object sender, EventArgs e)
        {
            MessageBox.Show("文艺程序猿一枚，主攻后端，兼做全栈，爱好开源，支持版权。\r\n上得了厅堂，下得了厨房，写得了代码，查得出异常，杀得了木马，翻得了围墙。\r\n\r\n--有态度的Coder      http://blog.cdsn.net/suqingheangle", "关于作者", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 关于系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmi_about_system_Click(object sender, EventArgs e)
        {
            MessageBox.Show("本程序仅为喜马拉雅爱好者提供批量下载使用，且对下载内容不负任何责任。\r\n本程序严禁用于商业用途或者出售，一经发现，必当追究！\r\n\r\n--有态度的Coder      http://blog.cdsn.net/suqingheangle", "关于系统", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 免责声明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmi_license_Click(object sender, EventArgs e)
        {
            MessageBox.Show("本程序仅为交流学习使用，严禁用于任何商业用途！\r\n请勿侵犯他人权益，因此而产生的法律责任，由下载者自行承担！\r\n若有发现侵权，请及时致电：suqingheangle@163.com，将在第一时间协助处理！\r\n\r\n--有态度的Coder      http://blog.cdsn.net/suqingheangle", "免责声明", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 联系我们
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmi_contact_us_Click(object sender, EventArgs e)
        {
            MessageBox.Show("作者很忙的，只留下了如下联系方式：\r\n邮箱：suqingheangle@163.com\r\n博客：http://blog.cdsn.net/suqingheangle \r\n\r\n--有态度的Coder", "联系我们", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 捐赠该软件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmi_donate_author_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://blog.csdn.net/suqingheangle/article/details/53229287");
        }

        /// <summary>
        /// 用户手册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmsi_user_guide_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://blog.csdn.net/suqingheangle/article/details/53228402");
        }

        /// <summary>
        /// 打开博客链接
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tml_author_blog_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://blog.csdn.net/suqingheangle");
        }

        /// <summary>
        /// 版本说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_version_Click(object sender, EventArgs e)
        {
            MessageBox.Show("当前软件版本为：" + Application.ProductVersion, "软件版本", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion 关于部分

        #region 系统设置

        /// <summary>
        /// 下载路径设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmi_setting_path_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var path = dialog.SelectedPath;
                FileHelper.SetSavePath(path);
                MessageBox.Show("保存路径设置成功，当前保存路径为：" + path, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 打开所在目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenExplorer_Click(object sender, EventArgs e)
        {
            var path = FileHelper.GetSavePath();
            System.Diagnostics.Process.Start("Explorer.exe", path);
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmi_exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.m_downlist.Rows.Clear();
            this.m_DownloadList.Clear();
        }

        /// <summary>
        /// 保存列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmi_save_list_Click(object sender, EventArgs e)
        {
            var path = DownloadHelper.SaveList(this.m_DownloadList);

            if (!string.IsNullOrWhiteSpace(path))
                MessageBox.Show("列表保存成功，保存路径为：" + path, "列表保存提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 加载列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmi_load_list_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdig = new OpenFileDialog();
            ofdig.InitialDirectory = Application.StartupPath + "/Download";
            if (ofdig.ShowDialog() == DialogResult.OK)
            {
                var path = ofdig.FileName;
                this.m_DownloadList = DownloadHelper.LoadList(path);

                AddFileToGridDataView(this.m_DownloadList);

                AddToPendingList(this.m_DownloadList);
            }
        }

        #endregion 系统设置

        #region 下载树状菜单事件

        /// <summary>
        /// 即将下载双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvPendingDownload_DoubleClick(object sender, EventArgs e)
        {
            //var selectNode = this.tvPendingDownload.SelectedNode;
            //if (selectNode.Name == "pendingNode")
            //{
            //}
            //else
            //{
            //    var albumId = selectNode.Name;
            //}
        }

        /// <summary>
        /// 即将下载的右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvPendingDownload_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//判断你点的是不是右键
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = tvPendingDownload.GetNodeAt(ClickPoint);
                if (CurrentNode != null)//判断你点的是不是一个节点
                {
                    CurrentNode.ContextMenuStrip = contextMenuStrip1;

                    CurrentNode.ContextMenuStrip.Show();

                    tvPendingDownload.SelectedNode = CurrentNode;//选中这个节点
                }
            }
        }

        /// <summary>
        /// 下载专辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_downAlbum_Click(object sender, EventArgs e)
        {
            var selectNode = this.tvPendingDownload.SelectedNode;
            if (selectNode.Name != "pendingNode")
            {
                var albumId = selectNode.Name;

                var lst = DownloadHelper.LoadAlbumList(albumId);

                if (lst != null)
                {
                    this.m_DownloadList = lst;

                    if (lst.All(a => a.isComplete))
                        MessageBox.Show("该专辑已全部下载完毕", "系统提示");
                    else
                    {
                        this.m_downlist.Rows.Clear();
                        this.AddFileToGridDataView(lst);

                        AddFileToGridDataView(lst);
                    }
                }
                else
                {
                    MessageBox.Show("找不到该专辑", "系统提示");
                }
            }
            else
            {
                MessageBox.Show("下载错误", "系统提示");
            }
        }

        /// <summary>
        /// 打开专辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_open_album_Click(object sender, EventArgs e)
        {
            var selectNode = this.tvPendingDownload.SelectedNode;
            if (selectNode != null)
            {
                var path = FileHelper.GetSavePath(selectNode.Text);
                if (string.IsNullOrWhiteSpace(path))
                {
                    MessageBox.Show("该专辑尚未下载或者目录不对，请重新确认", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    System.Diagnostics.Process.Start("Explorer.exe", path);
                }
            }
        }

        #endregion 下载树状菜单事件

        private void m_downlist_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//判断你点的是不是右键
            {
                this.m_downlist.ContextMenuStrip = contextMenuStrip2;
                this.m_downlist.ContextMenuStrip.Show();
            }
        }

        private void tsmi_select_Click(object sender, EventArgs e)
        {
            var selectedNode = this.m_downlist.SelectedRows;

            var selectIds = new List<string>();

            foreach (DataGridViewRow row in selectedNode)
            {
                row.Cells["Selected"].Value = true;

                var docId = row.Cells["DocId"].Value.ToString();

                selectIds.Add(docId);
            }

            //更新选择状态
            this.m_DownloadList.Where(a => selectIds.Contains(a.DocID)).ToList().ForEach(a => a.Selected = true);
        }

        private void tsmi_cancle_selected_Click(object sender, EventArgs e)
        {
            var selectedNode = this.m_downlist.SelectedRows;

            var selectIds = new List<string>();

            foreach (DataGridViewRow row in selectedNode)
            {
                row.Cells["Selected"].Value = false;

                var docId = row.Cells["DocId"].Value.ToString();

                selectIds.Add(docId);
            }

            //更新选择状态
            this.m_DownloadList.Where(a => selectIds.Contains(a.DocID)).ToList().ForEach(a => a.Selected = false);
        }

        private void tsmi_select_all_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.m_downlist.Rows)
            {
                row.Cells["Selected"].Value = true;
            }

            this.m_DownloadList.ForEach(a => a.Selected = true);
        }

        private void tsmi_toggle_select_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.m_downlist.Rows)
            {
                var selectValue = row.Cells["Selected"].Value;
                var boolValue = selectValue.ToString().ToLower().Equals("true");
                row.Cells["Selected"].Value = !boolValue;

                var docId = row.Cells["DocId"].Value.ToString();
                this.m_DownloadList.FirstOrDefault(a => a.DocID == docId).Selected = !boolValue;
            }
        }
    }
}