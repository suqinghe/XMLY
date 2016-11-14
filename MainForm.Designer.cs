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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.m_downlist = new System.Windows.Forms.DataGridView();
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DocId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Album = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.duration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SynSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SynProgress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnOpenExplorer = new System.Windows.Forms.Button();
            this.lblLink = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.m_downlist)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "专辑/声音";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(113, 8);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(730, 21);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.DoubleClick += new System.EventHandler(this.txtUrl_TextChanged);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(27, 46);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(65, 12);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = "分析结果：";
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(873, 41);
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
            this.m_downlist.Location = new System.Drawing.Point(13, 70);
            this.m_downlist.MultiSelect = false;
            this.m_downlist.Name = "m_downlist";
            this.m_downlist.ReadOnly = true;
            this.m_downlist.RowHeadersVisible = false;
            this.m_downlist.RowTemplate.Height = 23;
            this.m_downlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_downlist.Size = new System.Drawing.Size(951, 364);
            this.m_downlist.TabIndex = 5;
            this.m_downlist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_downlist_CellContentClick);
            // 
            // Selected
            // 
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
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(872, 7);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(75, 23);
            this.btnAnalyze.TabIndex = 6;
            this.btnAnalyze.Text = "分析并添加";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(792, 41);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "清空列表";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnOpenExplorer
            // 
            this.btnOpenExplorer.Location = new System.Drawing.Point(711, 41);
            this.btnOpenExplorer.Name = "btnOpenExplorer";
            this.btnOpenExplorer.Size = new System.Drawing.Size(75, 23);
            this.btnOpenExplorer.TabIndex = 8;
            this.btnOpenExplorer.Text = "打开目录";
            this.btnOpenExplorer.UseVisualStyleBackColor = true;
            this.btnOpenExplorer.Click += new System.EventHandler(this.btnOpenExplorer_Click);
            // 
            // lblLink
            // 
            this.lblLink.AutoSize = true;
            this.lblLink.LinkArea = new System.Windows.Forms.LinkArea(5, 34);
            this.lblLink.LinkVisited = true;
            this.lblLink.Location = new System.Drawing.Point(29, 451);
            this.lblLink.Name = "lblLink";
            this.lblLink.Size = new System.Drawing.Size(276, 19);
            this.lblLink.TabIndex = 9;
            this.lblLink.TabStop = true;
            this.lblLink.Text = "作者博客：http://blog.csdn.net/suqingheangle";
            this.lblLink.UseCompatibleTextRendering = true;
            this.lblLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLink_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(6, 9);
            this.linkLabel1.Location = new System.Drawing.Point(377, 451);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(122, 19);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "QQ交流群：159817585";
            this.linkLabel1.UseCompatibleTextRendering = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 479);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.lblLink);
            this.Controls.Add(this.btnOpenExplorer);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.m_downlist);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "喜马拉雅音频下载器";
            ((System.ComponentModel.ISupportInitialize)(this.m_downlist)).EndInit();
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
        private Button btnOpenExplorer;
        private LinkLabel lblLink;
        private LinkLabel linkLabel1;
        private DataGridViewCheckBoxColumn Selected;
        private DataGridViewTextBoxColumn DocId;
        private DataGridViewTextBoxColumn Album;
        private DataGridViewTextBoxColumn DocName;
        private DataGridViewTextBoxColumn duration;
        private DataGridViewTextBoxColumn FileSize;
        private DataGridViewTextBoxColumn SynSpeed;
        private DataGridViewTextBoxColumn SynProgress;
    }
}

