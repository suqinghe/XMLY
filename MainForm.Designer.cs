using System.Windows.Forms;

namespace XMLY
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("专辑列表");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.m_downlist = new System.Windows.Forms.DataGridView();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmi_setting_path = new System.Windows.Forms.ToolStripMenuItem();
            this.tmi_open_explorer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tmi_save_list = new System.Windows.Forms.ToolStripMenuItem();
            this.tmi_load_list = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmi_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmi_about_author = new System.Windows.Forms.ToolStripMenuItem();
            this.tmi_about_system = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_version = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tmsi_user_guide = new System.Windows.Forms.ToolStripMenuItem();
            this.tml_author_blog = new System.Windows.Forms.ToolStripMenuItem();
            this.tmi_donate_author = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tmi_license = new System.Windows.Forms.ToolStripMenuItem();
            this.tmi_contact_us = new System.Windows.Forms.ToolStripMenuItem();
            this.tvPendingDownload = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_downAlbum = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_open_album = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_stop_down = new System.Windows.Forms.Button();
            this.btn_save_list = new System.Windows.Forms.Button();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_select = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_cancle_selected = new System.Windows.Forms.ToolStripMenuItem();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DocId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Album = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SynSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SynProgress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tsmi_select_all = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_toggle_select = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.m_downlist)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "专辑/声音：";
            // 
            // txtUrl
            // 
            this.txtUrl.ForeColor = System.Drawing.Color.Gray;
            this.txtUrl.Location = new System.Drawing.Point(273, 42);
            this.txtUrl.Multiline = true;
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(570, 104);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "请在此处输入专辑链接，多个链接请换行";
            this.txtUrl.Click += new System.EventHandler(this.txtUrl_Click);
            this.txtUrl.DoubleClick += new System.EventHandler(this.txtUrl_TextChanged);
            this.txtUrl.Leave += new System.EventHandler(this.txtUrl_Leave);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(185, 157);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(65, 12);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = "分析结果：";
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(872, 96);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 4;
            this.btnDownload.Text = "开始下载";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // m_downlist
            // 
            this.m_downlist.AllowUserToAddRows = false;
            this.m_downlist.AllowUserToDeleteRows = false;
            this.m_downlist.AllowUserToResizeRows = false;
            this.m_downlist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.m_downlist.BackgroundColor = System.Drawing.Color.White;
            this.m_downlist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_downlist.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.m_downlist.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.m_downlist.ColumnHeadersHeight = 30;
            this.m_downlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.m_downlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.DocId,
            this.Album,
            this.DocName,
            this.duration,
            this.FileSize,
            this.SynSpeed,
            this.SynProgress});
            this.m_downlist.Location = new System.Drawing.Point(187, 186);
            this.m_downlist.Name = "m_downlist";
            this.m_downlist.ReadOnly = true;
            this.m_downlist.RowHeadersVisible = false;
            this.m_downlist.RowTemplate.Height = 23;
            this.m_downlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_downlist.Size = new System.Drawing.Size(776, 450);
            this.m_downlist.TabIndex = 5;
            this.m_downlist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_downlist_CellContentClick);
            this.m_downlist.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_downlist_MouseClick);
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(872, 38);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(75, 23);
            this.btnAnalyze.TabIndex = 6;
            this.btnAnalyze.Text = "分析并添加";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(872, 154);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "清空列表";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 25);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmi_setting_path,
            this.tmi_open_explorer,
            this.toolStripSeparator4,
            this.tmi_save_list,
            this.tmi_load_list,
            this.toolStripSeparator1,
            this.tmi_exit,
            this.toolStripSeparator3});
            this.设置ToolStripMenuItem.Image = global::XMLY.Properties.Resources.setting;
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // tmi_setting_path
            // 
            this.tmi_setting_path.Name = "tmi_setting_path";
            this.tmi_setting_path.Size = new System.Drawing.Size(148, 22);
            this.tmi_setting_path.Text = "下载路径设置";
            this.tmi_setting_path.Click += new System.EventHandler(this.tmi_setting_path_Click);
            // 
            // tmi_open_explorer
            // 
            this.tmi_open_explorer.Name = "tmi_open_explorer";
            this.tmi_open_explorer.Size = new System.Drawing.Size(148, 22);
            this.tmi_open_explorer.Text = "打开下载目录";
            this.tmi_open_explorer.Click += new System.EventHandler(this.btnOpenExplorer_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(145, 6);
            // 
            // tmi_save_list
            // 
            this.tmi_save_list.Name = "tmi_save_list";
            this.tmi_save_list.Size = new System.Drawing.Size(148, 22);
            this.tmi_save_list.Text = "保存列表";
            this.tmi_save_list.Click += new System.EventHandler(this.tmi_save_list_Click);
            // 
            // tmi_load_list
            // 
            this.tmi_load_list.Name = "tmi_load_list";
            this.tmi_load_list.Size = new System.Drawing.Size(148, 22);
            this.tmi_load_list.Text = "加载列表";
            this.tmi_load_list.Click += new System.EventHandler(this.tmi_load_list_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // tmi_exit
            // 
            this.tmi_exit.Name = "tmi_exit";
            this.tmi_exit.Size = new System.Drawing.Size(148, 22);
            this.tmi_exit.Text = "退出系统";
            this.tmi_exit.Click += new System.EventHandler(this.tmi_exit_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(145, 6);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmi_about_author,
            this.tmi_about_system,
            this.tsmi_version,
            this.toolStripSeparator5,
            this.tmsi_user_guide,
            this.tml_author_blog,
            this.tmi_donate_author,
            this.toolStripSeparator2,
            this.tmi_license,
            this.tmi_contact_us});
            this.关于ToolStripMenuItem.Image = global::XMLY.Properties.Resources.about;
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // tmi_about_author
            // 
            this.tmi_about_author.Name = "tmi_about_author";
            this.tmi_about_author.Size = new System.Drawing.Size(124, 22);
            this.tmi_about_author.Text = "关于作者";
            this.tmi_about_author.Click += new System.EventHandler(this.tmi_about_author_Click);
            // 
            // tmi_about_system
            // 
            this.tmi_about_system.Name = "tmi_about_system";
            this.tmi_about_system.Size = new System.Drawing.Size(124, 22);
            this.tmi_about_system.Text = "关于软件";
            this.tmi_about_system.Click += new System.EventHandler(this.tmi_about_system_Click);
            // 
            // tsmi_version
            // 
            this.tsmi_version.Name = "tsmi_version";
            this.tsmi_version.Size = new System.Drawing.Size(124, 22);
            this.tsmi_version.Text = "版本说明";
            this.tsmi_version.Click += new System.EventHandler(this.tsmi_version_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(121, 6);
            // 
            // tmsi_user_guide
            // 
            this.tmsi_user_guide.Name = "tmsi_user_guide";
            this.tmsi_user_guide.Size = new System.Drawing.Size(124, 22);
            this.tmsi_user_guide.Text = "用户手册";
            this.tmsi_user_guide.Click += new System.EventHandler(this.tmsi_user_guide_Click);
            // 
            // tml_author_blog
            // 
            this.tml_author_blog.Name = "tml_author_blog";
            this.tml_author_blog.Size = new System.Drawing.Size(124, 22);
            this.tml_author_blog.Text = "作者博客";
            this.tml_author_blog.Click += new System.EventHandler(this.tml_author_blog_Click);
            // 
            // tmi_donate_author
            // 
            this.tmi_donate_author.Name = "tmi_donate_author";
            this.tmi_donate_author.Size = new System.Drawing.Size(124, 22);
            this.tmi_donate_author.Text = "打赏作者";
            this.tmi_donate_author.Click += new System.EventHandler(this.tmi_donate_author_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(121, 6);
            // 
            // tmi_license
            // 
            this.tmi_license.Name = "tmi_license";
            this.tmi_license.Size = new System.Drawing.Size(124, 22);
            this.tmi_license.Text = "免责声明";
            this.tmi_license.Click += new System.EventHandler(this.tmi_license_Click);
            // 
            // tmi_contact_us
            // 
            this.tmi_contact_us.Name = "tmi_contact_us";
            this.tmi_contact_us.Size = new System.Drawing.Size(124, 22);
            this.tmi_contact_us.Text = "联系我们";
            this.tmi_contact_us.Click += new System.EventHandler(this.tmi_contact_us_Click);
            // 
            // tvPendingDownload
            // 
            this.tvPendingDownload.Location = new System.Drawing.Point(12, 42);
            this.tvPendingDownload.Name = "tvPendingDownload";
            treeNode3.Name = "pendingDown";
            treeNode3.Text = "专辑列表";
            this.tvPendingDownload.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.tvPendingDownload.Size = new System.Drawing.Size(139, 594);
            this.tvPendingDownload.TabIndex = 10;
            this.tvPendingDownload.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvPendingDownload_NodeMouseClick);
            this.tvPendingDownload.DoubleClick += new System.EventHandler(this.tvPendingDownload_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "directy.png");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_downAlbum,
            this.tsmi_open_album});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 48);
            // 
            // tsmi_downAlbum
            // 
            this.tsmi_downAlbum.Name = "tsmi_downAlbum";
            this.tsmi_downAlbum.Size = new System.Drawing.Size(136, 22);
            this.tsmi_downAlbum.Text = "下载该专辑";
            this.tsmi_downAlbum.Click += new System.EventHandler(this.tsmi_downAlbum_Click);
            // 
            // tsmi_open_album
            // 
            this.tsmi_open_album.Name = "tsmi_open_album";
            this.tsmi_open_album.Size = new System.Drawing.Size(136, 22);
            this.tsmi_open_album.Text = "打开该专辑";
            this.tsmi_open_album.Click += new System.EventHandler(this.tsmi_open_album_Click);
            // 
            // btn_stop_down
            // 
            this.btn_stop_down.Location = new System.Drawing.Point(872, 125);
            this.btn_stop_down.Name = "btn_stop_down";
            this.btn_stop_down.Size = new System.Drawing.Size(75, 23);
            this.btn_stop_down.TabIndex = 11;
            this.btn_stop_down.Text = "停止下载";
            this.btn_stop_down.UseVisualStyleBackColor = true;
            this.btn_stop_down.Click += new System.EventHandler(this.btn_stop_down_Click);
            // 
            // btn_save_list
            // 
            this.btn_save_list.Location = new System.Drawing.Point(872, 67);
            this.btn_save_list.Name = "btn_save_list";
            this.btn_save_list.Size = new System.Drawing.Size(75, 23);
            this.btn_save_list.TabIndex = 12;
            this.btn_save_list.Text = "保存列表";
            this.btn_save_list.UseVisualStyleBackColor = true;
            this.btn_save_list.Click += new System.EventHandler(this.btn_save_list_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_select,
            this.tsmi_cancle_selected,
            this.tsmi_select_all,
            this.tsmi_toggle_select});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(101, 92);
            // 
            // tsmi_select
            // 
            this.tsmi_select.Name = "tsmi_select";
            this.tsmi_select.Size = new System.Drawing.Size(100, 22);
            this.tsmi_select.Text = "选中";
            this.tsmi_select.Click += new System.EventHandler(this.tsmi_select_Click);
            // 
            // tsmi_cancle_selected
            // 
            this.tsmi_cancle_selected.Name = "tsmi_cancle_selected";
            this.tsmi_cancle_selected.Size = new System.Drawing.Size(152, 22);
            this.tsmi_cancle_selected.Text = "取消";
            this.tsmi_cancle_selected.Click += new System.EventHandler(this.tsmi_cancle_selected_Click);
            // 
            // Selected
            // 
            this.Selected.ContextMenuStrip = this.contextMenuStrip2;
            this.Selected.FillWeight = 26.892F;
            this.Selected.HeaderText = "选择";
            this.Selected.Name = "Selected";
            this.Selected.ReadOnly = true;
            // 
            // DocId
            // 
            this.DocId.FillWeight = 74.29462F;
            this.DocId.HeaderText = "专辑编号";
            this.DocId.Name = "DocId";
            this.DocId.ReadOnly = true;
            // 
            // Album
            // 
            this.Album.FillWeight = 74.29462F;
            this.Album.HeaderText = "专辑";
            this.Album.Name = "Album";
            this.Album.ReadOnly = true;
            // 
            // DocName
            // 
            this.DocName.FillWeight = 74.29462F;
            this.DocName.HeaderText = "声音";
            this.DocName.Name = "DocName";
            this.DocName.ReadOnly = true;
            // 
            // duration
            // 
            this.duration.FillWeight = 74.29462F;
            this.duration.HeaderText = "时长";
            this.duration.Name = "duration";
            this.duration.ReadOnly = true;
            // 
            // FileSize
            // 
            this.FileSize.FillWeight = 74.29462F;
            this.FileSize.HeaderText = "大小";
            this.FileSize.Name = "FileSize";
            this.FileSize.ReadOnly = true;
            // 
            // SynSpeed
            // 
            this.SynSpeed.FillWeight = 74.29462F;
            this.SynSpeed.HeaderText = "速度";
            this.SynSpeed.Name = "SynSpeed";
            this.SynSpeed.ReadOnly = true;
            // 
            // SynProgress
            // 
            this.SynProgress.FillWeight = 74.29462F;
            this.SynProgress.HeaderText = "进度";
            this.SynProgress.Name = "SynProgress";
            this.SynProgress.ReadOnly = true;
            // 
            // tsmi_select_all
            // 
            this.tsmi_select_all.Name = "tsmi_select_all";
            this.tsmi_select_all.Size = new System.Drawing.Size(152, 22);
            this.tsmi_select_all.Text = "全选";
            this.tsmi_select_all.Click += new System.EventHandler(this.tsmi_select_all_Click);
            // 
            // tsmi_toggle_select
            // 
            this.tsmi_toggle_select.Name = "tsmi_toggle_select";
            this.tsmi_toggle_select.Size = new System.Drawing.Size(152, 22);
            this.tsmi_toggle_select.Text = "反选";
            this.tsmi_toggle_select.Click += new System.EventHandler(this.tsmi_toggle_select_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 662);
            this.Controls.Add(this.btn_save_list);
            this.Controls.Add(this.btn_stop_down);
            this.Controls.Add(this.tvPendingDownload);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.m_downlist);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "喜马拉雅音频下载器";
            ((System.ComponentModel.ISupportInitialize)(this.m_downlist)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.DataGridView m_downlist;
        private System.Windows.Forms.Button btnAnalyze;
        private Button btnClear;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 设置ToolStripMenuItem;
        private ToolStripMenuItem tmi_setting_path;
        private ToolStripMenuItem tmi_open_explorer;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem tmi_exit;
        private ToolStripMenuItem 关于ToolStripMenuItem;
        private ToolStripMenuItem tmi_about_author;
        private ToolStripMenuItem tmi_about_system;
        private ToolStripMenuItem tmi_license;
        private ToolStripMenuItem tmi_contact_us;
        private ToolStripSeparator toolStripSeparator2;
        private TreeView tvPendingDownload;
        private ImageList imageList1;
        private ToolStripMenuItem tml_author_blog;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem tsmi_downAlbum;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem tmi_save_list;
        private ToolStripMenuItem tmi_load_list;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem tmi_donate_author;
        private ToolStripMenuItem tsmi_open_album;
        private ToolStripMenuItem tmsi_user_guide;
        private Button btn_stop_down;
        private Button btn_save_list;
        private ToolStripMenuItem tsmi_version;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem tsmi_select;
        private DataGridViewCheckBoxColumn Selected;
        private ToolStripMenuItem tsmi_cancle_selected;
        private DataGridViewTextBoxColumn DocId;
        private DataGridViewTextBoxColumn Album;
        private DataGridViewTextBoxColumn DocName;
        private DataGridViewTextBoxColumn duration;
        private DataGridViewTextBoxColumn FileSize;
        private DataGridViewTextBoxColumn SynSpeed;
        private DataGridViewTextBoxColumn SynProgress;
        private ToolStripMenuItem tsmi_select_all;
        private ToolStripMenuItem tsmi_toggle_select;
    }
}

