namespace TTCarProject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.button_openGate = new System.Windows.Forms.Button();
            this.buttonStartVideo1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.labelPlate = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.m_richTextPrintInfo = new System.Windows.Forms.RichTextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label6 = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.rightbutton = new System.Windows.Forms.Button();
            this.upbutton = new System.Windows.Forms.Button();
            this.rightupbutton = new System.Windows.Forms.Button();
            this.leftupbutton = new System.Windows.Forms.Button();
            this.leftbutton = new System.Windows.Forms.Button();
            this.leftdownbutton = new System.Windows.Forms.Button();
            this.downbutton = new System.Windows.Forms.Button();
            this.rightdownbutton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.outPlateLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.outVideoPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outVideoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选项";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(14, 45);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(65, 23);
            this.buttonConnect.TabIndex = 7;
            this.buttonConnect.Text = "连接";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Visible = false;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // button_openGate
            // 
            this.button_openGate.Enabled = false;
            this.button_openGate.Location = new System.Drawing.Point(14, 45);
            this.button_openGate.Name = "button_openGate";
            this.button_openGate.Size = new System.Drawing.Size(65, 23);
            this.button_openGate.TabIndex = 9;
            this.button_openGate.Text = "打开道闸";
            this.button_openGate.UseVisualStyleBackColor = true;
            this.button_openGate.Click += new System.EventHandler(this.button_openGate_Click);
            // 
            // buttonStartVideo1
            // 
            this.buttonStartVideo1.Enabled = false;
            this.buttonStartVideo1.Location = new System.Drawing.Point(98, 45);
            this.buttonStartVideo1.Name = "buttonStartVideo1";
            this.buttonStartVideo1.Size = new System.Drawing.Size(65, 23);
            this.buttonStartVideo1.TabIndex = 10;
            this.buttonStartVideo1.Text = "开始视频";
            this.buttonStartVideo1.UseVisualStyleBackColor = true;
            this.buttonStartVideo1.Click += new System.EventHandler(this.buttonStartVideo1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(866, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "进门车牌：";
            // 
            // labelPlate
            // 
            this.labelPlate.AutoSize = true;
            this.labelPlate.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPlate.Location = new System.Drawing.Point(919, 14);
            this.labelPlate.Name = "labelPlate";
            this.labelPlate.Size = new System.Drawing.Size(106, 19);
            this.labelPlate.TabIndex = 14;
            this.labelPlate.Text = "山A091333";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(667, 35);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 12);
            this.labelStatus.TabIndex = 16;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(27, 115);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(439, 302);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(25, 91);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "进门道闸视频";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(999, 115);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(439, 302);
            this.pictureBox2.TabIndex = 19;
            this.pictureBox2.TabStop = false;
            // 
            // m_richTextPrintInfo
            // 
            this.m_richTextPrintInfo.Location = new System.Drawing.Point(27, 453);
            this.m_richTextPrintInfo.Name = "m_richTextPrintInfo";
            this.m_richTextPrintInfo.Size = new System.Drawing.Size(923, 203);
            this.m_richTextPrintInfo.TabIndex = 21;
            this.m_richTextPrintInfo.Text = "";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(998, 453);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(439, 203);
            this.listView1.TabIndex = 22;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1041, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 23;
            this.label6.Text = "用户名：";
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exitButton.Location = new System.Drawing.Point(1172, 11);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(65, 20);
            this.exitButton.TabIndex = 24;
            this.exitButton.Text = "切换账户";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(1100, 15);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(41, 12);
            this.usernameLabel.TabIndex = 25;
            this.usernameLabel.Text = "用户名";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(997, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "监控视频";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(181, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 23);
            this.button1.TabIndex = 27;
            this.button1.Text = "预约";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rightbutton
            // 
            this.rightbutton.Location = new System.Drawing.Point(1407, 251);
            this.rightbutton.Name = "rightbutton";
            this.rightbutton.Size = new System.Drawing.Size(30, 30);
            this.rightbutton.TabIndex = 28;
            this.rightbutton.Text = "→";
            this.rightbutton.UseVisualStyleBackColor = true;
            this.rightbutton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rightbutton_MouseDown);
            this.rightbutton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rightbutton_MouseUp);
            // 
            // upbutton
            // 
            this.upbutton.Location = new System.Drawing.Point(1203, 116);
            this.upbutton.Name = "upbutton";
            this.upbutton.Size = new System.Drawing.Size(30, 30);
            this.upbutton.TabIndex = 29;
            this.upbutton.Text = "↑";
            this.upbutton.UseVisualStyleBackColor = true;
            this.upbutton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.upbutton_MouseDown);
            this.upbutton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.upbutton_MouseUp);
            // 
            // rightupbutton
            // 
            this.rightupbutton.Location = new System.Drawing.Point(1407, 116);
            this.rightupbutton.Name = "rightupbutton";
            this.rightupbutton.Size = new System.Drawing.Size(30, 30);
            this.rightupbutton.TabIndex = 30;
            this.rightupbutton.Text = "↗";
            this.rightupbutton.UseVisualStyleBackColor = true;
            this.rightupbutton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rightupbutton_MouseDown);
            this.rightupbutton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rightupbutton_MouseUp);
            // 
            // leftupbutton
            // 
            this.leftupbutton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.leftupbutton.Location = new System.Drawing.Point(1000, 116);
            this.leftupbutton.Name = "leftupbutton";
            this.leftupbutton.Size = new System.Drawing.Size(30, 30);
            this.leftupbutton.TabIndex = 31;
            this.leftupbutton.Text = "↖";
            this.leftupbutton.UseVisualStyleBackColor = false;
            this.leftupbutton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.leftupbutton_MouseDown);
            this.leftupbutton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.leftupbutton_MouseUp);
            // 
            // leftbutton
            // 
            this.leftbutton.Location = new System.Drawing.Point(1000, 251);
            this.leftbutton.Name = "leftbutton";
            this.leftbutton.Size = new System.Drawing.Size(30, 30);
            this.leftbutton.TabIndex = 32;
            this.leftbutton.Text = "←";
            this.leftbutton.UseVisualStyleBackColor = true;
            this.leftbutton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.leftbutton_MouseDown);
            this.leftbutton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.leftbutton_MouseUp);
            // 
            // leftdownbutton
            // 
            this.leftdownbutton.Location = new System.Drawing.Point(1000, 386);
            this.leftdownbutton.Name = "leftdownbutton";
            this.leftdownbutton.Size = new System.Drawing.Size(30, 30);
            this.leftdownbutton.TabIndex = 33;
            this.leftdownbutton.Text = "↙";
            this.leftdownbutton.UseVisualStyleBackColor = true;
            this.leftdownbutton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.leftdownbutton_MouseDown);
            this.leftdownbutton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.leftdownbutton_MouseUp);
            // 
            // downbutton
            // 
            this.downbutton.Location = new System.Drawing.Point(1203, 386);
            this.downbutton.Name = "downbutton";
            this.downbutton.Size = new System.Drawing.Size(30, 30);
            this.downbutton.TabIndex = 34;
            this.downbutton.Text = "↓";
            this.downbutton.UseVisualStyleBackColor = true;
            this.downbutton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.downbutton_MouseDown);
            this.downbutton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.downbutton_MouseUp);
            // 
            // rightdownbutton
            // 
            this.rightdownbutton.Location = new System.Drawing.Point(1407, 386);
            this.rightdownbutton.Name = "rightdownbutton";
            this.rightdownbutton.Size = new System.Drawing.Size(30, 30);
            this.rightdownbutton.TabIndex = 35;
            this.rightdownbutton.Text = "↘";
            this.rightdownbutton.UseVisualStyleBackColor = true;
            this.rightdownbutton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rightdownbutton_MouseDown);
            this.rightdownbutton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rightdownbutton_MouseUp);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(265, 45);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 23);
            this.button2.TabIndex = 36;
            this.button2.Text = "监控回放";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(866, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 37;
            this.label2.Text = "出门车牌：";
            // 
            // outPlateLabel
            // 
            this.outPlateLabel.AutoSize = true;
            this.outPlateLabel.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outPlateLabel.Location = new System.Drawing.Point(919, 52);
            this.outPlateLabel.Name = "outPlateLabel";
            this.outPlateLabel.Size = new System.Drawing.Size(106, 19);
            this.outPlateLabel.TabIndex = 38;
            this.outPlateLabel.Text = "山A091333";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(511, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 39;
            this.label3.Text = "出门道闸视频";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(494, 45);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 41;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // outVideoPictureBox
            // 
            this.outVideoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outVideoPictureBox.Location = new System.Drawing.Point(513, 115);
            this.outVideoPictureBox.Name = "outVideoPictureBox";
            this.outVideoPictureBox.Size = new System.Drawing.Size(439, 302);
            this.outVideoPictureBox.TabIndex = 40;
            this.outVideoPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1467, 682);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.outVideoPictureBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.outPlateLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.rightdownbutton);
            this.Controls.Add(this.downbutton);
            this.Controls.Add(this.leftdownbutton);
            this.Controls.Add(this.leftbutton);
            this.Controls.Add(this.leftupbutton);
            this.Controls.Add(this.rightupbutton);
            this.Controls.Add(this.upbutton);
            this.Controls.Add(this.rightbutton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.m_richTextPrintInfo);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelPlate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonStartVideo1);
            this.Controls.Add(this.button_openGate);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "仝仝道闸";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outVideoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button button_openGate;
        private System.Windows.Forms.Button buttonStartVideo1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelPlate;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RichTextBox m_richTextPrintInfo;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button rightbutton;
        private System.Windows.Forms.Button upbutton;
        private System.Windows.Forms.Button rightupbutton;
        private System.Windows.Forms.Button leftupbutton;
        private System.Windows.Forms.Button leftbutton;
        private System.Windows.Forms.Button leftdownbutton;
        private System.Windows.Forms.Button downbutton;
        private System.Windows.Forms.Button rightdownbutton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label outPlateLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox outVideoPictureBox;
    }
}

