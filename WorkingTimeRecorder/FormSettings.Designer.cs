namespace WorkingTimeRecorder
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonPath = new System.Windows.Forms.Button();
            this.comboBoxGetTime = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelGetTime = new System.Windows.Forms.Label();
            this.buttonGetTime = new System.Windows.Forms.Button();
            this.textBoxGetTime = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxInfo2 = new System.Windows.Forms.CheckBox();
            this.checkBoxInfo1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSetWorkTime = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxAutoStart = new System.Windows.Forms.CheckBox();
            this.checkBoxTopMost = new System.Windows.Forms.CheckBox();
            this.checkBoxShowInTaskBar = new System.Windows.Forms.CheckBox();
            this.checkBoxMianForm = new System.Windows.Forms.CheckBox();
            this.labelColor = new System.Windows.Forms.Label();
            this.labelFont = new System.Windows.Forms.Label();
            this.buttonSetColor = new System.Windows.Forms.Button();
            this.buttonSetFont = new System.Windows.Forms.Button();
            this.buttonSetLocation = new System.Windows.Forms.Button();
            this.textBoxLocationY = new System.Windows.Forms.TextBox();
            this.textBoxLocationX = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(10, 10);
            this.textBoxPath.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            this.textBoxPath.Size = new System.Drawing.Size(257, 21);
            this.textBoxPath.TabIndex = 0;
            // 
            // buttonPath
            // 
            this.buttonPath.Location = new System.Drawing.Point(133, 35);
            this.buttonPath.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPath.Name = "buttonPath";
            this.buttonPath.Size = new System.Drawing.Size(133, 23);
            this.buttonPath.TabIndex = 1;
            this.buttonPath.Text = "设置log文件保存路径";
            this.buttonPath.UseVisualStyleBackColor = true;
            this.buttonPath.Click += new System.EventHandler(this.buttonPath_Click);
            // 
            // comboBoxGetTime
            // 
            this.comboBoxGetTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGetTime.FormattingEnabled = true;
            this.comboBoxGetTime.Items.AddRange(new object[] {
            "本机时间",
            "局域网时间",
            "外网时间"});
            this.comboBoxGetTime.Location = new System.Drawing.Point(4, 19);
            this.comboBoxGetTime.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxGetTime.Name = "comboBoxGetTime";
            this.comboBoxGetTime.Size = new System.Drawing.Size(89, 20);
            this.comboBoxGetTime.TabIndex = 3;
            this.comboBoxGetTime.SelectedIndexChanged += new System.EventHandler(this.comboBoxGetTime_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelGetTime);
            this.groupBox1.Controls.Add(this.buttonGetTime);
            this.groupBox1.Controls.Add(this.textBoxGetTime);
            this.groupBox1.Controls.Add(this.comboBoxGetTime);
            this.groupBox1.Location = new System.Drawing.Point(9, 58);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(256, 69);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "获取时间方式";
            // 
            // labelGetTime
            // 
            this.labelGetTime.AutoSize = true;
            this.labelGetTime.Location = new System.Drawing.Point(97, 22);
            this.labelGetTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelGetTime.Name = "labelGetTime";
            this.labelGetTime.Size = new System.Drawing.Size(41, 12);
            this.labelGetTime.TabIndex = 6;
            this.labelGetTime.Text = "lable1";
            // 
            // buttonGetTime
            // 
            this.buttonGetTime.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonGetTime.Font = new System.Drawing.Font("宋体", 8F);
            this.buttonGetTime.Location = new System.Drawing.Point(195, 34);
            this.buttonGetTime.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGetTime.Name = "buttonGetTime";
            this.buttonGetTime.Size = new System.Drawing.Size(57, 30);
            this.buttonGetTime.TabIndex = 5;
            this.buttonGetTime.Text = "检查是\r\n否可用";
            this.buttonGetTime.UseVisualStyleBackColor = true;
            this.buttonGetTime.Click += new System.EventHandler(this.buttonGetTime_Click);
            // 
            // textBoxGetTime
            // 
            this.textBoxGetTime.Location = new System.Drawing.Point(5, 43);
            this.textBoxGetTime.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxGetTime.Name = "textBoxGetTime";
            this.textBoxGetTime.Size = new System.Drawing.Size(183, 21);
            this.textBoxGetTime.TabIndex = 4;
            this.textBoxGetTime.TextChanged += new System.EventHandler(this.textBoxGetTime_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxInfo2);
            this.groupBox2.Controls.Add(this.checkBoxInfo1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBoxSetWorkTime);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(10, 133);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(256, 63);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "工作时间设置";
            // 
            // checkBoxInfo2
            // 
            this.checkBoxInfo2.AutoSize = true;
            this.checkBoxInfo2.Checked = true;
            this.checkBoxInfo2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxInfo2.Location = new System.Drawing.Point(86, 44);
            this.checkBoxInfo2.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxInfo2.Name = "checkBoxInfo2";
            this.checkBoxInfo2.Size = new System.Drawing.Size(156, 16);
            this.checkBoxInfo2.TabIndex = 4;
            this.checkBoxInfo2.Text = "在主窗体显示已加班时间";
            this.checkBoxInfo2.UseVisualStyleBackColor = true;
            // 
            // checkBoxInfo1
            // 
            this.checkBoxInfo1.AutoSize = true;
            this.checkBoxInfo1.Checked = true;
            this.checkBoxInfo1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxInfo1.Location = new System.Drawing.Point(8, 44);
            this.checkBoxInfo1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxInfo1.Name = "checkBoxInfo1";
            this.checkBoxInfo1.Size = new System.Drawing.Size(72, 16);
            this.checkBoxInfo1.TabIndex = 3;
            this.checkBoxInfo1.Text = "下班提醒";
            this.checkBoxInfo1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 20);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "小时";
            // 
            // textBoxSetWorkTime
            // 
            this.textBoxSetWorkTime.Location = new System.Drawing.Point(82, 18);
            this.textBoxSetWorkTime.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSetWorkTime.Name = "textBoxSetWorkTime";
            this.textBoxSetWorkTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxSetWorkTime.Size = new System.Drawing.Size(26, 21);
            this.textBoxSetWorkTime.TabIndex = 1;
            this.textBoxSetWorkTime.Text = "9";
            this.textBoxSetWorkTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSetWorkTime.TextChanged += new System.EventHandler(this.textBoxSetWorkTime_TextChanged);
            this.textBoxSetWorkTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSetWorkTime_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "预定工作时间";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxAutoStart);
            this.groupBox3.Controls.Add(this.checkBoxTopMost);
            this.groupBox3.Controls.Add(this.checkBoxShowInTaskBar);
            this.groupBox3.Controls.Add(this.checkBoxMianForm);
            this.groupBox3.Controls.Add(this.labelColor);
            this.groupBox3.Controls.Add(this.labelFont);
            this.groupBox3.Controls.Add(this.buttonSetColor);
            this.groupBox3.Controls.Add(this.buttonSetFont);
            this.groupBox3.Controls.Add(this.buttonSetLocation);
            this.groupBox3.Controls.Add(this.textBoxLocationY);
            this.groupBox3.Controls.Add(this.textBoxLocationX);
            this.groupBox3.Location = new System.Drawing.Point(278, 10);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(179, 154);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "窗体设置";
            // 
            // checkBoxAutoStart
            // 
            this.checkBoxAutoStart.AutoSize = true;
            this.checkBoxAutoStart.Location = new System.Drawing.Point(97, 94);
            this.checkBoxAutoStart.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxAutoStart.Name = "checkBoxAutoStart";
            this.checkBoxAutoStart.Size = new System.Drawing.Size(72, 16);
            this.checkBoxAutoStart.TabIndex = 10;
            this.checkBoxAutoStart.Text = "开机自启";
            this.checkBoxAutoStart.UseVisualStyleBackColor = true;
            this.checkBoxAutoStart.CheckedChanged += new System.EventHandler(this.checkBoxAutoStart_CheckedChanged);
            // 
            // checkBoxTopMost
            // 
            this.checkBoxTopMost.AutoSize = true;
            this.checkBoxTopMost.Location = new System.Drawing.Point(4, 134);
            this.checkBoxTopMost.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxTopMost.Name = "checkBoxTopMost";
            this.checkBoxTopMost.Size = new System.Drawing.Size(108, 16);
            this.checkBoxTopMost.TabIndex = 9;
            this.checkBoxTopMost.Text = "主窗体置顶显示";
            this.checkBoxTopMost.UseVisualStyleBackColor = true;
            this.checkBoxTopMost.CheckedChanged += new System.EventHandler(this.checkBoxTopMost_CheckedChanged);
            // 
            // checkBoxShowInTaskBar
            // 
            this.checkBoxShowInTaskBar.AutoSize = true;
            this.checkBoxShowInTaskBar.Location = new System.Drawing.Point(4, 114);
            this.checkBoxShowInTaskBar.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxShowInTaskBar.Name = "checkBoxShowInTaskBar";
            this.checkBoxShowInTaskBar.Size = new System.Drawing.Size(96, 16);
            this.checkBoxShowInTaskBar.TabIndex = 8;
            this.checkBoxShowInTaskBar.Text = "在任务栏显示";
            this.checkBoxShowInTaskBar.UseVisualStyleBackColor = true;
            this.checkBoxShowInTaskBar.CheckedChanged += new System.EventHandler(this.checkBoxShowInTaskBar_CheckedChanged);
            // 
            // checkBoxMianForm
            // 
            this.checkBoxMianForm.AutoSize = true;
            this.checkBoxMianForm.Checked = true;
            this.checkBoxMianForm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMianForm.Location = new System.Drawing.Point(4, 94);
            this.checkBoxMianForm.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxMianForm.Name = "checkBoxMianForm";
            this.checkBoxMianForm.Size = new System.Drawing.Size(84, 16);
            this.checkBoxMianForm.TabIndex = 7;
            this.checkBoxMianForm.Text = "显示主窗体";
            this.checkBoxMianForm.UseVisualStyleBackColor = true;
            this.checkBoxMianForm.CheckedChanged += new System.EventHandler(this.checkBoxMianForm_CheckedChanged);
            // 
            // labelColor
            // 
            this.labelColor.AutoSize = true;
            this.labelColor.Location = new System.Drawing.Point(104, 76);
            this.labelColor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelColor.Name = "labelColor";
            this.labelColor.Size = new System.Drawing.Size(41, 12);
            this.labelColor.TabIndex = 6;
            this.labelColor.Text = "label4";
            // 
            // labelFont
            // 
            this.labelFont.AutoSize = true;
            this.labelFont.Location = new System.Drawing.Point(71, 53);
            this.labelFont.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFont.Name = "labelFont";
            this.labelFont.Size = new System.Drawing.Size(41, 12);
            this.labelFont.TabIndex = 5;
            this.labelFont.Text = "label3";
            // 
            // buttonSetColor
            // 
            this.buttonSetColor.Location = new System.Drawing.Point(5, 71);
            this.buttonSetColor.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSetColor.Name = "buttonSetColor";
            this.buttonSetColor.Size = new System.Drawing.Size(95, 23);
            this.buttonSetColor.TabIndex = 4;
            this.buttonSetColor.Text = "设置文字颜色";
            this.buttonSetColor.UseVisualStyleBackColor = true;
            this.buttonSetColor.Click += new System.EventHandler(this.buttonSetColor_Click);
            // 
            // buttonSetFont
            // 
            this.buttonSetFont.Location = new System.Drawing.Point(5, 48);
            this.buttonSetFont.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSetFont.Name = "buttonSetFont";
            this.buttonSetFont.Size = new System.Drawing.Size(62, 23);
            this.buttonSetFont.TabIndex = 3;
            this.buttonSetFont.Text = "设置字体";
            this.buttonSetFont.UseVisualStyleBackColor = true;
            this.buttonSetFont.Click += new System.EventHandler(this.buttonSetFont_Click);
            // 
            // buttonSetLocation
            // 
            this.buttonSetLocation.Location = new System.Drawing.Point(71, 18);
            this.buttonSetLocation.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSetLocation.Name = "buttonSetLocation";
            this.buttonSetLocation.Size = new System.Drawing.Size(98, 23);
            this.buttonSetLocation.TabIndex = 2;
            this.buttonSetLocation.Text = "设置主窗体坐标";
            this.buttonSetLocation.UseVisualStyleBackColor = true;
            this.buttonSetLocation.Click += new System.EventHandler(this.buttonSetLocation_Click);
            // 
            // textBoxLocationY
            // 
            this.textBoxLocationY.Location = new System.Drawing.Point(38, 20);
            this.textBoxLocationY.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLocationY.Name = "textBoxLocationY";
            this.textBoxLocationY.Size = new System.Drawing.Size(29, 21);
            this.textBoxLocationY.TabIndex = 1;
            // 
            // textBoxLocationX
            // 
            this.textBoxLocationX.Location = new System.Drawing.Point(5, 20);
            this.textBoxLocationX.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxLocationX.Name = "textBoxLocationX";
            this.textBoxLocationX.Size = new System.Drawing.Size(29, 21);
            this.textBoxLocationX.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(283, 177);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "允许上传日志";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(393, 173);
            this.buttonUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(64, 23);
            this.buttonUpdate.TabIndex = 8;
            this.buttonUpdate.Text = "检查更新";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 206);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonPath);
            this.Controls.Add(this.textBoxPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.Text = "设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonPath;
        private System.Windows.Forms.ComboBox comboBoxGetTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelGetTime;
        private System.Windows.Forms.Button buttonGetTime;
        private System.Windows.Forms.TextBox textBoxGetTime;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxInfo2;
        private System.Windows.Forms.CheckBox checkBoxInfo1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSetWorkTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxTopMost;
        private System.Windows.Forms.CheckBox checkBoxShowInTaskBar;
        private System.Windows.Forms.CheckBox checkBoxMianForm;
        private System.Windows.Forms.Label labelColor;
        private System.Windows.Forms.Label labelFont;
        private System.Windows.Forms.Button buttonSetColor;
        private System.Windows.Forms.Button buttonSetFont;
        private System.Windows.Forms.Button buttonSetLocation;
        private System.Windows.Forms.TextBox textBoxLocationY;
        private System.Windows.Forms.TextBox textBoxLocationX;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.CheckBox checkBoxAutoStart;
    }
}