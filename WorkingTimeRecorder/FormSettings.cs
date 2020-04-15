using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorkingTimeRecorder
{
    public partial class FormSettings : Form
    {
        private Form1 form1 = null;
        public FormSettings(Form1 form)
        {
            InitializeComponent();
            form1 = form;

            this.textBoxLocationX.KeyDown += new KeyEventHandler(this.LocationDetermined);
            this.textBoxLocationY.KeyDown += new KeyEventHandler(this.LocationDetermined);
        }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        private void LocationDetermined(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.buttonSetLocation.PerformClick();
            }
        }

        private void buttonSetLocation_Click(object sender, EventArgs e)
        {
            int.TryParse(textBoxLocationX.Text, out int x);
            int.TryParse(textBoxLocationY.Text, out int y);
            form1.SetLocation(x, y);
        }

        private void buttonPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择log文件保存路径";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.savePath = folderBrowserDialog.SelectedPath;
                textBoxPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void comboBoxGetTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comboBoxGetTime.SelectedIndex)
            {
                case 0:
                    Settings.Default.comboBoxGetTime = 0;
                    this.textBoxGetTime.Text = string.Empty;
                    this.labelGetTime.Text = string.Empty;
                    break;
                case 1:
                    Settings.Default.comboBoxGetTime = 1;
                    this.textBoxGetTime.Text = Settings.Default.localNet;
                    this.labelGetTime.Text = "";
                    break;
                case 2:
                    Settings.Default.comboBoxGetTime = 2;
                    this.textBoxGetTime.Text = Settings.Default.extranet;
                    this.labelGetTime.Text = string.Empty;
                    break;
                default:
                    break;
            }
        }

        private void textBoxGetTime_TextChanged(object sender, EventArgs e)
        {
            switch (this.comboBoxGetTime.SelectedIndex)
            {
                case 1:
                    Settings.Default.localNet = this.textBoxGetTime.Text;
                    break;
                case 2:
                    Settings.Default.extranet = this.textBoxGetTime.Text;
                    break;
                default:
                    break;
            }
        }

        private void buttonGetTime_Click(object sender, EventArgs e)
        {
            GetTime getTime = new GetTime();
            GetTime.TimeFormat time = getTime.GetTimeFormat(out bool result);
            this.labelGetTime.Text = result ? time.fullTime : "获取时间失败";
        }
    }
}
