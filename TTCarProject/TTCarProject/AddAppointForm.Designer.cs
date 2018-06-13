namespace TTCarProject
{
    partial class AddAppointForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAppointForm));
            this.label1 = new System.Windows.Forms.Label();
            this.managerTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.beginDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.endDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.carnumberTextBox = new System.Windows.Forms.TextBox();
            this.driverNameTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.driverPhonetextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.reasonRichTextBox = new System.Windows.Forms.RichTextBox();
            this.cancleButton = new System.Windows.Forms.Button();
            this.confirmButton = new System.Windows.Forms.Button();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "办理人：";
            // 
            // managerTextBox
            // 
            this.managerTextBox.Location = new System.Drawing.Point(122, 48);
            this.managerTextBox.Name = "managerTextBox";
            this.managerTextBox.Size = new System.Drawing.Size(140, 21);
            this.managerTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "预约类型：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "开始时间：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(290, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "结束时间：";
            // 
            // beginDateTimePicker
            // 
            this.beginDateTimePicker.CustomFormat = "yyyy-MM-dd hh:mm";
            this.beginDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.beginDateTimePicker.Location = new System.Drawing.Point(124, 90);
            this.beginDateTimePicker.Name = "beginDateTimePicker";
            this.beginDateTimePicker.Size = new System.Drawing.Size(138, 21);
            this.beginDateTimePicker.TabIndex = 6;
            // 
            // endDateTimePicker
            // 
            this.endDateTimePicker.CustomFormat = "yyyy-MM-dd hh:mm";
            this.endDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDateTimePicker.Location = new System.Drawing.Point(361, 90);
            this.endDateTimePicker.Name = "endDateTimePicker";
            this.endDateTimePicker.Size = new System.Drawing.Size(140, 21);
            this.endDateTimePicker.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(53, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "车牌号：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(290, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "司机名称：";
            // 
            // carnumberTextBox
            // 
            this.carnumberTextBox.Location = new System.Drawing.Point(122, 135);
            this.carnumberTextBox.Name = "carnumberTextBox";
            this.carnumberTextBox.Size = new System.Drawing.Size(140, 21);
            this.carnumberTextBox.TabIndex = 10;
            // 
            // driverNameTextBox
            // 
            this.driverNameTextBox.Location = new System.Drawing.Point(361, 135);
            this.driverNameTextBox.Name = "driverNameTextBox";
            this.driverNameTextBox.Size = new System.Drawing.Size(140, 21);
            this.driverNameTextBox.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(53, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "司机电话：";
            // 
            // driverPhonetextBox
            // 
            this.driverPhonetextBox.Location = new System.Drawing.Point(122, 185);
            this.driverPhonetextBox.Name = "driverPhonetextBox";
            this.driverPhonetextBox.Size = new System.Drawing.Size(140, 21);
            this.driverPhonetextBox.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(53, 238);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "预约缘由：";
            // 
            // reasonRichTextBox
            // 
            this.reasonRichTextBox.Location = new System.Drawing.Point(122, 238);
            this.reasonRichTextBox.Name = "reasonRichTextBox";
            this.reasonRichTextBox.Size = new System.Drawing.Size(358, 76);
            this.reasonRichTextBox.TabIndex = 15;
            this.reasonRichTextBox.Text = "";
            // 
            // cancleButton
            // 
            this.cancleButton.Location = new System.Drawing.Point(166, 355);
            this.cancleButton.Name = "cancleButton";
            this.cancleButton.Size = new System.Drawing.Size(75, 23);
            this.cancleButton.TabIndex = 16;
            this.cancleButton.Text = "取消";
            this.cancleButton.UseVisualStyleBackColor = true;
            this.cancleButton.Click += new System.EventHandler(this.cancleButton_Click);
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(324, 355);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(75, 23);
            this.confirmButton.TabIndex = 17;
            this.confirmButton.Text = "确定";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // typeComboBox
            // 
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "生成车辆",
            "来访车辆",
            "自有登记车辆"});
            this.typeComboBox.Location = new System.Drawing.Point(361, 48);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(140, 20);
            this.typeComboBox.TabIndex = 18;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(311, 51);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(0, 12);
            this.linkLabel1.TabIndex = 19;
            // 
            // AddAppointForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 439);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.cancleButton);
            this.Controls.Add(this.reasonRichTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.driverPhonetextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.driverNameTextBox);
            this.Controls.Add(this.carnumberTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.endDateTimePicker);
            this.Controls.Add(this.beginDateTimePicker);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.managerTextBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddAppointForm";
            this.Text = "AddAppointForm";
            this.Load += new System.EventHandler(this.AddAppointForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox managerTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker beginDateTimePicker;
        private System.Windows.Forms.DateTimePicker endDateTimePicker;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox carnumberTextBox;
        private System.Windows.Forms.TextBox driverNameTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox driverPhonetextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox reasonRichTextBox;
        private System.Windows.Forms.Button cancleButton;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}