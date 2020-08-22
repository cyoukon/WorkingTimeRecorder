namespace WorkingTimeRecorder
{
    partial class FormProxy
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProxy));
            this.checkBoxProxy = new System.Windows.Forms.CheckBox();
            this.labelProxy = new System.Windows.Forms.Label();
            this.textBoxProxy = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPwd = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // checkBoxProxy
            // 
            this.checkBoxProxy.AutoSize = true;
            this.checkBoxProxy.Location = new System.Drawing.Point(13, 13);
            this.checkBoxProxy.Name = "checkBoxProxy";
            this.checkBoxProxy.Size = new System.Drawing.Size(89, 19);
            this.checkBoxProxy.TabIndex = 0;
            this.checkBoxProxy.Text = "使用代理";
            this.checkBoxProxy.UseVisualStyleBackColor = true;
            this.checkBoxProxy.CheckedChanged += new System.EventHandler(this.checkBoxProxy_CheckedChanged);
            // 
            // labelProxy
            // 
            this.labelProxy.AutoSize = true;
            this.labelProxy.Location = new System.Drawing.Point(13, 48);
            this.labelProxy.Name = "labelProxy";
            this.labelProxy.Size = new System.Drawing.Size(67, 15);
            this.labelProxy.TabIndex = 1;
            this.labelProxy.Text = "代理地址";
            // 
            // textBoxProxy
            // 
            this.textBoxProxy.Location = new System.Drawing.Point(86, 45);
            this.textBoxProxy.Name = "textBoxProxy";
            this.textBoxProxy.Size = new System.Drawing.Size(161, 25);
            this.textBoxProxy.TabIndex = 6;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(265, 48);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(67, 15);
            this.labelPort.TabIndex = 1;
            this.labelPort.Text = "代理端口";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(338, 45);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(100, 25);
            this.textBoxPort.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "用户名";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(86, 80);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(161, 25);
            this.textBoxName.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(265, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "密码";
            // 
            // textBoxPwd
            // 
            this.textBoxPwd.Location = new System.Drawing.Point(338, 80);
            this.textBoxPwd.Name = "textBoxPwd";
            this.textBoxPwd.Size = new System.Drawing.Size(100, 25);
            this.textBoxPwd.TabIndex = 6;
            this.textBoxPwd.UseSystemPasswordChar = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(338, 121);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(80, 37);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormProxy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 170);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxPwd);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxProxy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.labelProxy);
            this.Controls.Add(this.checkBoxProxy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProxy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WorkingTimeRecorder";
            this.Load += new System.EventHandler(this.FormProxy_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxProxy;
        private System.Windows.Forms.Label labelProxy;
        private System.Windows.Forms.TextBox textBoxProxy;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPwd;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Timer timer1;
    }
}