﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IWshRuntimeLibrary;

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
            this.checkBoxInfo1.CheckedChanged += new System.EventHandler(this.checkBoxInfo_CheckedChanged);
            this.checkBoxInfo2.CheckedChanged += new System.EventHandler(this.checkBoxInfo_CheckedChanged);
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
                Settings.Default.savePath = folderBrowserDialog.SelectedPath + "\\";
                textBoxPath.Text = folderBrowserDialog.SelectedPath + "\\";
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

        private void FormSettings_Load(object sender, EventArgs e)
        {
            this.textBoxPath.Text = string.IsNullOrEmpty(Settings.Default.savePath) ? System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase : Settings.Default.savePath;
            this.comboBoxGetTime.SelectedIndex = Settings.Default.comboBoxGetTime;
            textBoxGetTime_TextChanged(sender, e);
            this.textBoxLocationX.Text = Settings.Default.pointX.ToString();
            this.textBoxLocationY.Text = Settings.Default.pointY.ToString();
            this.labelFont.Text = Settings.Default.font.Name + " " + Settings.Default.font.Size;
            this.labelColor.Text = Settings.Default.color.Name;
        }

        private void buttonSetFont_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                form1.SetFont(fontDialog.Font);
                this.labelFont.Text = fontDialog.Font.Name + " " + fontDialog.Font.Size;
                Settings.Default.font = fontDialog.Font;
            }
        }

        private void buttonSetColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                form1.SetColor(colorDialog.Color);
                this.labelColor.Text = colorDialog.Color.Name;
                Settings.Default.color = colorDialog.Color;
            }
        }

        private void checkBoxAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string startupPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonStartup);
                //设置开机自启动  
                if (checkBoxAutoStart.Checked == true)
                {
                    //获得文件的当前路径
                    string dir = Directory.GetCurrentDirectory();
                    //获取可执行文件的全部路径
                    string exeDir = dir + @"\WorkingTimeRecorder.exe";
                    ShortcutCreator.CreateShortcut(startupPath, "WorkingTimeRecorder.lnk", exeDir);
                }
                //取消开机自启动  
                else
                {
                    System.IO.File.Delete(startupPath + @"\WorkingTimeRecorder.lnk");
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("设置失败，请以管理员权限启动后重试");
                this.checkBoxAutoStart.Checked = !this.checkBoxAutoStart.Checked;
            }
            catch
            {
                MessageBox.Show("设置不了，放弃吧");
                this.checkBoxAutoStart.Checked = !this.checkBoxAutoStart.Checked;
            }
        }

        private void checkBoxTopMost_CheckedChanged(object sender, EventArgs e)
        {
            form1.SetTopMost(this.checkBoxTopMost.Checked);
            Settings.Default.topMost = this.checkBoxTopMost.Checked;
        }

        private void checkBoxInfo_CheckedChanged(object sender, EventArgs e)
        {
            form1.SetOvertimeInfo(checkBoxInfo1.Checked || checkBoxInfo2.Checked);
            Settings.Default.inFo1 = checkBoxInfo1.Checked;
            Settings.Default.inFo2 = checkBoxInfo2.Checked;
        }

        private void textBoxSetWorkTime_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBoxSetWorkTime.Text, out int hour);
            if (hour > 12)
            {
                MessageBox.Show("抵制996，从我做起");
                textBoxSetWorkTime.Text = "12";
            }
            else
                Settings.Default.inFoTime = hour;
        }

        private void textBoxSetWorkTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }

    #region 创建快捷方式
    /// <summary>
    /// 创建快捷方式的类
    /// </summary>
    /// <remarks></remarks>
    public class ShortcutCreator
    {
        //需要引入IWshRuntimeLibrary，搜索Windows Script Host Object Model

        /// <summary>
        /// 创建快捷方式
        /// </summary>
        /// <param name="directory">快捷方式所处的文件夹</param>
        /// <param name="shortcutName">快捷方式名称</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="description">描述</param>
        /// <param name="iconLocation">图标路径，格式为"可执行文件或DLL路径, 图标编号"，
        /// 例如System.Environment.SystemDirectory + "\\" + "shell32.dll, 165"</param>
        /// <remarks></remarks>
        public static void CreateShortcut(string directory, string shortcutName, string targetPath,
            string description = null, string iconLocation = null)
        {
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            string shortcutPath = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);//创建快捷方式对象
            shortcut.TargetPath = targetPath;//指定目标路径
            shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);//设置起始位置
            shortcut.WindowStyle = 1;//设置运行方式，默认为常规窗口
            shortcut.Description = description;//设置备注
            shortcut.IconLocation = string.IsNullOrWhiteSpace(iconLocation) ? targetPath : iconLocation;//设置图标路径
            shortcut.Save();//保存快捷方式
        }

        /// <summary>
        /// 创建桌面快捷方式
        /// </summary>
        /// <param name="shortcutName">快捷方式名称</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="description">描述</param>
        /// <param name="iconLocation">图标路径，格式为"可执行文件或DLL路径, 图标编号"</param>
        /// <remarks></remarks>
        public static void CreateShortcutOnDesktop(string shortcutName, string targetPath,
            string description = null, string iconLocation = null)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//获取桌面文件夹路径
            CreateShortcut(desktop, shortcutName, targetPath, description, iconLocation);
        }
    }
    #endregion
}
