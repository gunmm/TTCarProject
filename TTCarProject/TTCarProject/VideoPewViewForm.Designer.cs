namespace TTCarProject
{
    partial class VideoPewViewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (m_lPlayHandle >= 0)
            {
                CHCNetSDK.NET_DVR_StopPlayBack(m_lPlayHandle);
            }
            if (m_lDownHandle >= 0)
            {
                CHCNetSDK.NET_DVR_StopGetFile(m_lDownHandle);

            }
            if (m_lUserID >= 0)
            {
                CHCNetSDK.NET_DVR_Logout(m_lUserID);
            }
            if (m_bInitSDK == true)
            {
                CHCNetSDK.NET_DVR_Cleanup();
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoPewViewForm));
            this.VideoPlayWnd = new System.Windows.Forms.PictureBox();
            this.listViewIPChannel = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.btnResume = new System.Windows.Forms.Button();
            this.labelReverse = new System.Windows.Forms.Label();
            this.btnReverse = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelPause = new System.Windows.Forms.Label();
            this.btnFrame = new System.Windows.Forms.Button();
            this.btnFast = new System.Windows.Forms.Button();
            this.btnSlow = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.btnBMP = new System.Windows.Forms.Button();
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.DownloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.btnStopDownload = new System.Windows.Forms.Button();
            this.btnDownloadName = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.btnPlaybackTime = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnStopPlayback = new System.Windows.Forms.Button();
            this.btnPlaybackName = new System.Windows.Forms.Button();
            this.listViewFile = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSearch = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.timerDownload = new System.Windows.Forms.Timer(this.components);
            this.PlaybackprogressBar = new System.Windows.Forms.ProgressBar();
            this.timerPlayback = new System.Windows.Forms.Timer(this.components);
            this.btnDownloadTime = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.VideoPlayWnd)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // VideoPlayWnd
            // 
            this.VideoPlayWnd.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.VideoPlayWnd.Location = new System.Drawing.Point(184, 103);
            this.VideoPlayWnd.Name = "VideoPlayWnd";
            this.VideoPlayWnd.Size = new System.Drawing.Size(440, 366);
            this.VideoPlayWnd.TabIndex = 30;
            this.VideoPlayWnd.TabStop = false;
            // 
            // listViewIPChannel
            // 
            this.listViewIPChannel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2});
            this.listViewIPChannel.FullRowSelect = true;
            this.listViewIPChannel.GridLines = true;
            this.listViewIPChannel.Location = new System.Drawing.Point(4, 12);
            this.listViewIPChannel.MultiSelect = false;
            this.listViewIPChannel.Name = "listViewIPChannel";
            this.listViewIPChannel.Size = new System.Drawing.Size(174, 467);
            this.listViewIPChannel.TabIndex = 32;
            this.listViewIPChannel.UseCompatibleStateImageBehavior = false;
            this.listViewIPChannel.View = System.Windows.Forms.View.Details;
            this.listViewIPChannel.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewIPChannel_ItemSelectionChanged);
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "通道 Channel";
            this.ColumnHeader1.Width = 90;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "状态 Status";
            this.ColumnHeader2.Width = 90;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.btnResume);
            this.groupBox2.Controls.Add(this.labelReverse);
            this.groupBox2.Controls.Add(this.btnReverse);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.labelPause);
            this.groupBox2.Controls.Add(this.btnFrame);
            this.groupBox2.Controls.Add(this.btnFast);
            this.groupBox2.Controls.Add(this.btnSlow);
            this.groupBox2.Controls.Add(this.btnPause);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.btnBMP);
            this.groupBox2.Location = new System.Drawing.Point(184, 478);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(440, 71);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(200, 50);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(77, 12);
            this.label25.TabIndex = 50;
            this.label25.Text = "恢复正常速度";
            // 
            // btnResume
            // 
            this.btnResume.Location = new System.Drawing.Point(197, 18);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(85, 23);
            this.btnResume.TabIndex = 49;
            this.btnResume.Text = "Normal Speed";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // labelReverse
            // 
            this.labelReverse.AutoSize = true;
            this.labelReverse.Location = new System.Drawing.Point(294, 49);
            this.labelReverse.Name = "labelReverse";
            this.labelReverse.Size = new System.Drawing.Size(65, 12);
            this.labelReverse.TabIndex = 48;
            this.labelReverse.Text = "切换为倒放";
            this.labelReverse.Visible = false;
            // 
            // btnReverse
            // 
            this.btnReverse.Location = new System.Drawing.Point(288, 18);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(75, 23);
            this.btnReverse.TabIndex = 47;
            this.btnReverse.Text = "Reverse";
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Visible = false;
            this.btnReverse.Click += new System.EventHandler(this.button1_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(149, 49);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(29, 12);
            this.label23.TabIndex = 46;
            this.label23.Text = "单帧";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(98, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 45;
            this.label11.Text = "快放";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(54, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 44;
            this.label9.Text = "慢放";
            // 
            // labelPause
            // 
            this.labelPause.AutoSize = true;
            this.labelPause.Location = new System.Drawing.Point(11, 49);
            this.labelPause.Name = "labelPause";
            this.labelPause.Size = new System.Drawing.Size(29, 12);
            this.labelPause.TabIndex = 43;
            this.labelPause.Text = "暂停";
            // 
            // btnFrame
            // 
            this.btnFrame.Location = new System.Drawing.Point(140, 18);
            this.btnFrame.Name = "btnFrame";
            this.btnFrame.Size = new System.Drawing.Size(50, 23);
            this.btnFrame.TabIndex = 42;
            this.btnFrame.Text = "|>";
            this.btnFrame.UseVisualStyleBackColor = true;
            this.btnFrame.Click += new System.EventHandler(this.btnFrame_Click);
            // 
            // btnFast
            // 
            this.btnFast.Location = new System.Drawing.Point(93, 18);
            this.btnFast.Name = "btnFast";
            this.btnFast.Size = new System.Drawing.Size(41, 23);
            this.btnFast.TabIndex = 41;
            this.btnFast.Text = ">>";
            this.btnFast.UseVisualStyleBackColor = true;
            this.btnFast.Click += new System.EventHandler(this.btnFast_Click);
            // 
            // btnSlow
            // 
            this.btnSlow.Location = new System.Drawing.Point(48, 18);
            this.btnSlow.Name = "btnSlow";
            this.btnSlow.Size = new System.Drawing.Size(41, 23);
            this.btnSlow.TabIndex = 40;
            this.btnSlow.Text = "<<";
            this.btnSlow.UseVisualStyleBackColor = true;
            this.btnSlow.Click += new System.EventHandler(this.btnSlow_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(7, 18);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(37, 23);
            this.btnPause.TabIndex = 39;
            this.btnPause.Text = "||";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(376, 49);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 12);
            this.label14.TabIndex = 38;
            this.label14.Text = "BMP抓图";
            this.label14.Visible = false;
            // 
            // btnBMP
            // 
            this.btnBMP.Location = new System.Drawing.Point(371, 18);
            this.btnBMP.Name = "btnBMP";
            this.btnBMP.Size = new System.Drawing.Size(56, 23);
            this.btnBMP.TabIndex = 36;
            this.btnBMP.Text = "BMP";
            this.btnBMP.UseVisualStyleBackColor = true;
            this.btnBMP.Visible = false;
            this.btnBMP.Click += new System.EventHandler(this.btnBMP_Click);
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeStart.Location = new System.Drawing.Point(101, 26);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.Size = new System.Drawing.Size(158, 21);
            this.dateTimeStart.TabIndex = 42;
            this.dateTimeStart.UseWaitCursor = true;
            this.dateTimeStart.Value = new System.DateTime(2014, 2, 28, 14, 43, 28, 0);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.DownloadProgressBar);
            this.groupBox4.Controls.Add(this.btnStopDownload);
            this.groupBox4.Controls.Add(this.btnDownloadName);
            this.groupBox4.Controls.Add(this.btnDownloadTime);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.btnPlaybackTime);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.btnStopPlayback);
            this.groupBox4.Controls.Add(this.btnPlaybackName);
            this.groupBox4.Controls.Add(this.listViewFile);
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.dateTimeEnd);
            this.groupBox4.Controls.Add(this.dateTimeStart);
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Controls.Add(this.groupBox8);
            this.groupBox4.Location = new System.Drawing.Point(630, -1);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(296, 550);
            this.groupBox4.TabIndex = 42;
            this.groupBox4.TabStop = false;
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Location = new System.Drawing.Point(4, 528);
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(283, 10);
            this.DownloadProgressBar.TabIndex = 61;
            this.DownloadProgressBar.Visible = false;
            // 
            // btnStopDownload
            // 
            this.btnStopDownload.Enabled = false;
            this.btnStopDownload.Location = new System.Drawing.Point(161, 482);
            this.btnStopDownload.Name = "btnStopDownload";
            this.btnStopDownload.Size = new System.Drawing.Size(126, 32);
            this.btnStopDownload.TabIndex = 60;
            this.btnStopDownload.Text = "Stop Download";
            this.btnStopDownload.UseVisualStyleBackColor = true;
            this.btnStopDownload.Visible = false;
            this.btnStopDownload.Click += new System.EventHandler(this.btnStopDownload_Click);
            // 
            // btnDownloadName
            // 
            this.btnDownloadName.Location = new System.Drawing.Point(161, 411);
            this.btnDownloadName.Name = "btnDownloadName";
            this.btnDownloadName.Size = new System.Drawing.Size(126, 32);
            this.btnDownloadName.TabIndex = 59;
            this.btnDownloadName.Text = "Download By Name";
            this.btnDownloadName.UseVisualStyleBackColor = true;
            this.btnDownloadName.Visible = false;
            this.btnDownloadName.Click += new System.EventHandler(this.btnDownloadName_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(99, 385);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(77, 12);
            this.label22.TabIndex = 57;
            this.label22.Text = "按文件名回放";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(108, 162);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 12);
            this.label19.TabIndex = 50;
            this.label19.Text = "录像文件查找";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(99, 101);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(65, 12);
            this.label20.TabIndex = 56;
            this.label20.Text = "按时间回放";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(108, 462);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(83, 12);
            this.label21.TabIndex = 54;
            this.label21.Text = "停止回放/下载";
            // 
            // btnPlaybackTime
            // 
            this.btnPlaybackTime.Location = new System.Drawing.Point(10, 125);
            this.btnPlaybackTime.Name = "btnPlaybackTime";
            this.btnPlaybackTime.Size = new System.Drawing.Size(124, 29);
            this.btnPlaybackTime.TabIndex = 55;
            this.btnPlaybackTime.Text = "按时间回放";
            this.btnPlaybackTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPlaybackTime.UseVisualStyleBackColor = true;
            this.btnPlaybackTime.Click += new System.EventHandler(this.btnPlaybackTime_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Location = new System.Drawing.Point(4, 160);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(289, 10);
            this.groupBox6.TabIndex = 45;
            this.groupBox6.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(3, 460);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(289, 10);
            this.groupBox5.TabIndex = 44;
            this.groupBox5.TabStop = false;
            // 
            // btnStopPlayback
            // 
            this.btnStopPlayback.Enabled = false;
            this.btnStopPlayback.Location = new System.Drawing.Point(9, 482);
            this.btnStopPlayback.Name = "btnStopPlayback";
            this.btnStopPlayback.Size = new System.Drawing.Size(125, 34);
            this.btnStopPlayback.TabIndex = 53;
            this.btnStopPlayback.Text = "停止回放";
            this.btnStopPlayback.UseVisualStyleBackColor = true;
            this.btnStopPlayback.Click += new System.EventHandler(this.btnStopPlayback_Click);
            // 
            // btnPlaybackName
            // 
            this.btnPlaybackName.Location = new System.Drawing.Point(9, 411);
            this.btnPlaybackName.Name = "btnPlaybackName";
            this.btnPlaybackName.Size = new System.Drawing.Size(125, 32);
            this.btnPlaybackName.TabIndex = 51;
            this.btnPlaybackName.Text = "按文件名回放";
            this.btnPlaybackName.UseVisualStyleBackColor = true;
            this.btnPlaybackName.Click += new System.EventHandler(this.btnPlaybackName_Click);
            // 
            // listViewFile
            // 
            this.listViewFile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewFile.FullRowSelect = true;
            this.listViewFile.GridLines = true;
            this.listViewFile.Location = new System.Drawing.Point(4, 185);
            this.listViewFile.Name = "listViewFile";
            this.listViewFile.Size = new System.Drawing.Size(290, 151);
            this.listViewFile.TabIndex = 49;
            this.listViewFile.UseCompatibleStateImageBehavior = false;
            this.listViewFile.View = System.Windows.Forms.View.Details;
            this.listViewFile.SelectedIndexChanged += new System.EventHandler(this.listViewFile_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "文件名";
            this.columnHeader3.Width = 93;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "开始时间";
            this.columnHeader4.Width = 109;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "结束时间";
            this.columnHeader5.Width = 113;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(9, 344);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(125, 32);
            this.btnSearch.TabIndex = 48;
            this.btnSearch.Text = "查找";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 59);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(71, 12);
            this.label17.TabIndex = 47;
            this.label17.Text = "Ending time";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(15, 77);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 46;
            this.label18.Text = "结束时间";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(89, 12);
            this.label16.TabIndex = 45;
            this.label16.Text = "Beginning time";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 35);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 44;
            this.label15.Text = "开始时间";
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeEnd.Location = new System.Drawing.Point(101, 68);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(156, 21);
            this.dateTimeEnd.TabIndex = 43;
            this.dateTimeEnd.Value = new System.DateTime(2014, 2, 28, 14, 40, 31, 0);
            // 
            // groupBox7
            // 
            this.groupBox7.Location = new System.Drawing.Point(4, 99);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(289, 10);
            this.groupBox7.TabIndex = 46;
            this.groupBox7.TabStop = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Location = new System.Drawing.Point(4, 382);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(289, 10);
            this.groupBox8.TabIndex = 46;
            this.groupBox8.TabStop = false;
            // 
            // timerDownload
            // 
            this.timerDownload.Tick += new System.EventHandler(this.timerProgress_Tick);
            // 
            // PlaybackprogressBar
            // 
            this.PlaybackprogressBar.Location = new System.Drawing.Point(184, 471);
            this.PlaybackprogressBar.Name = "PlaybackprogressBar";
            this.PlaybackprogressBar.Size = new System.Drawing.Size(440, 10);
            this.PlaybackprogressBar.TabIndex = 43;
            this.PlaybackprogressBar.Visible = false;
            // 
            // timerPlayback
            // 
            this.timerPlayback.Tick += new System.EventHandler(this.timerPlayback_Tick);
            // 
            // btnDownloadTime
            // 
            this.btnDownloadTime.Location = new System.Drawing.Point(161, 125);
            this.btnDownloadTime.Name = "btnDownloadTime";
            this.btnDownloadTime.Size = new System.Drawing.Size(126, 29);
            this.btnDownloadTime.TabIndex = 58;
            this.btnDownloadTime.Text = "Download By Time";
            this.btnDownloadTime.UseVisualStyleBackColor = true;
            this.btnDownloadTime.Visible = false;
            this.btnDownloadTime.Click += new System.EventHandler(this.btnDownloadTime_Click);
            // 
            // VideoPewViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 593);
            this.Controls.Add(this.PlaybackprogressBar);
            this.Controls.Add(this.listViewIPChannel);
            this.Controls.Add(this.VideoPlayWnd);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VideoPewViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "监控回放";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.VideoPlayWnd)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox VideoPlayWnd;
        private System.Windows.Forms.ListView listViewIPChannel;
        private System.Windows.Forms.ColumnHeader ColumnHeader1;
        private System.Windows.Forms.ColumnHeader ColumnHeader2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListView listViewFile;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button btnPlaybackName;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnStopPlayback;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnPlaybackTime;
        private System.Windows.Forms.Button btnFast;
        private System.Windows.Forms.Button btnSlow;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnFrame;
        private System.Windows.Forms.Label labelPause;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnDownloadName;
        private System.Windows.Forms.Button btnStopDownload;
        private System.Windows.Forms.ProgressBar DownloadProgressBar;
        private System.Windows.Forms.Timer timerDownload;
        private System.Windows.Forms.ProgressBar PlaybackprogressBar;
        private System.Windows.Forms.Timer timerPlayback;
        private System.Windows.Forms.Label labelReverse;
        private System.Windows.Forms.Button btnReverse;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnBMP;
        private System.Windows.Forms.Button btnDownloadTime;
    }
}

