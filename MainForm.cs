using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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
        #endregion

        #region 监测连接状态
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int connectionDescription, int reservedValue);

        private bool isConnected()
        {
            int num = 0;
            return MainForm.InternetGetConnectedState(out num, 0);
        }
        #endregion

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

        #endregion

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

        #endregion

        #region 下载部分
        /// <summary>
        /// 开始下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownload_Click(object sender, EventArgs e)
        {
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
            this.StartNewWork();
        }


        /// <summary>
        /// 获取下一个下载信息
        /// </summary>
        /// <returns></returns>
        private SynFileInfo GetNext()
        {
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
                    MessageBox.Show("所有文件下载完毕!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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


        #endregion

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
        }

        #endregion

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

        #endregion

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
                        if (item.Contains("/sound/"))
                        {
                            AnalyzeSigleDoc(item);
                        }
                        else
                        {
                            AnalyzeAlbum(item);
                        }
                    }

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

                WebClient webClient = new WebClient();
                byte[] bytes = webClient.DownloadData(link);
                string @string = Encoding.UTF8.GetString(bytes);
                this.showinfo("完成!");
                string value = Regex.Match(@string, "<h1>([\\w\\W]*?)</h1>").Groups[1].Value;
                string pattern = "(?i)<li sound_id=\"(\\d*)\" class=\"\">[\\w\\W]*?hashlink[\\w\\W]*?>([\\w\\W]*?)</a>";// "sound_ids=\".*\">";//
                MatchCollection matchCollection = Regex.Matches(@string, pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);

                //专辑ID
                var albumId = link.Split(new string[] { "album/" }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();

                foreach (Match match in matchCollection)
                {
                    string[] values2 = new string[]
                       {"true",
                    match.Groups[1].Value.Trim(),
                    value,
                    match.Groups[2].Value.Trim()
                       };

                    var dgvRow = new DataGridViewRow();


                    //列表中不存在，才进行添加
                    if (!this.m_DownloadList.Any(a => a.DocID == match.Groups[1].Value.Trim()))
                    {
                        int index2 = this.m_downlist.Rows.Add(values2);
                        DataGridViewRow rowObject2 = this.m_downlist.Rows[index2];
                        SynFileInfo item = new SynFileInfo
                        {
                            DocID = match.Groups[1].Value.Trim(),
                            DocName = match.Groups[2].Value.Trim(),
                            RowObject = rowObject2,
                            Album = value,
                            Selected = true,
                            AlbumId = albumId
                    };
                    this.m_DownloadList.Add(item);
                }
                    else
                    {
                    this.showinfo("列表中已经存在" + match.Groups[1].Value.Trim() + "的声音");
                }

            }

                this.AddToPendingList(this.m_DownloadList);

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
                    Count = g.Count(),
                    AlbumId = g.FirstOrDefault().AlbumId
                };

    foreach (var item in items)
    {
        if (!this.tvPendingDownload.Nodes.ContainsKey(item.AlbumId))
            this.tvPendingDownload.Nodes.Add(item.AlbumId, string.Format("{0}({1})", item.Name, item.Count));
    }
}

/// <summary>
/// 分析单个
/// </summary>
private void AnalyzeSigleDoc(string link)
{
    string text = link;
    string docID = text.Substring(text.IndexOf("sound/") + 6);
    SynFileInfo synFileInfo = new SynFileInfo { Selected = true };
    synFileInfo.DocID = docID;
    DownloadHelper.GetJsonInfo(synFileInfo);
    string[] values = new string[]
    {
                    "true",
                    synFileInfo.DocID,
                    synFileInfo.Album,
                    synFileInfo.DocName
    };

    //列表中不存在，才进行添加
    if (!this.m_DownloadList.Any(a => a.DocID == synFileInfo.DocID))
    {
        int index = this.m_downlist.Rows.Add(values);
        DataGridViewRow rowObject = this.m_downlist.Rows[index];

        synFileInfo.RowObject = rowObject;
        synFileInfo.RowObject.Cells["duration"].Value = (synFileInfo.duration / 60).ToString() + "分钟";

        this.m_DownloadList.Add(synFileInfo);
    }
    else
    {
        this.showinfo("列表中已经存在" + synFileInfo.DocID + "的声音");
    }
    return;
}



#endregion

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
    MessageBox.Show("作者很忙的，只留下了如下联系方式：\r\n邮箱：suqingheangle@163.com\r\n博客：http://blog.cdsn.net/suqingheangle \r\nGithub：https://github.com/suqinghe \r\n\r\n--有态度的Coder", "联系我们", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
#endregion

#region 系统设置
/// <summary>
/// 下载路径设置
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void tmi_setting_path_Click(object sender, EventArgs e)
{

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

        #endregion

    }
}
