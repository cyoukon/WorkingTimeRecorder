using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        bool FormSettingLoaded = false;
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
                    this.textBoxGetTime.Visible = false;
                    this.labelGetTime.Text = string.Empty;
                    break;
                case 1:
                    Settings.Default.comboBoxGetTime = 1;
                    this.textBoxGetTime.Visible = true;
                    this.textBoxGetTime.Text = Settings.Default.localNet;
                    this.labelGetTime.Text = "";
                    break;
                case 2:
                    Settings.Default.comboBoxGetTime = 2;
                    this.textBoxGetTime.Visible = true;
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
            this.buttonGetTime.Cursor = Cursors.WaitCursor;
            this.labelGetTime.Text = "时间获取中";
            this.labelGetTime.Refresh();
            GetTime.TimeFormat time = GetTime.GetTimeFormat(out bool result);
            this.labelGetTime.Text = result ? time.fullTime : "获取时间失败";
            this.buttonGetTime.Cursor = Cursors.Default;
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            this.textBoxPath.Text = string.IsNullOrEmpty(Settings.Default.savePath) ? System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase : Settings.Default.savePath;
            this.comboBoxGetTime.SelectedIndex = Settings.Default.comboBoxGetTime;
            textBoxGetTime_TextChanged(sender, e);
            this.checkBoxInfo1.Checked = Settings.Default.inFo1; // 下班提醒
            this.checkBoxInfo2.Checked = Settings.Default.inFo2; // 显示已加班时间
            this.textBoxLocationX.Text = Settings.Default.pointX.ToString();
            this.textBoxLocationY.Text = Settings.Default.pointY.ToString();
            this.labelFont.Text = Settings.Default.font.Name + " " + Settings.Default.font.Size;
            this.labelColor.Text = Settings.Default.color.Name;
            this.checkBoxAutoStart.Checked = Settings.Default.autoStart;
            this.checkBoxTopMost.Checked = Settings.Default.topMost;
            this.textBoxSetWorkTime.Text = Settings.Default.inFoTime == 0 ? "9" : Settings.Default.inFoTime.ToString();

            FormSettingLoaded = true;
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
            if (FormSettingLoaded)
            {
                bool restart = false;
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
                        ShortcutCreator.CreateShortcut(startupPath, "WorkingTimeRecorder", exeDir);
                    }
                    //取消开机自启动  
                    else
                    {
                        System.IO.File.Delete(startupPath + @"\WorkingTimeRecorder.lnk");
                    }
                    Settings.Default.autoStart = checkBoxAutoStart.Checked;
                }
                catch (UnauthorizedAccessException)
                {
                    var ret = MessageBox.Show("设置失败，是否需要以管理员权限启动后重试？", "WorkingTimeRecorder", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    restart = ret == DialogResult.Yes;
                    this.checkBoxAutoStart.CheckedChanged -= new System.EventHandler(this.checkBoxAutoStart_CheckedChanged);
                    this.checkBoxAutoStart.Checked = !this.checkBoxAutoStart.Checked;
                    this.checkBoxAutoStart.CheckedChanged += new System.EventHandler(this.checkBoxAutoStart_CheckedChanged);
                }
                catch
                {
                    MessageBox.Show("设置不了，放弃吧");
                    this.checkBoxAutoStart.Checked = !this.checkBoxAutoStart.Checked;
                }
                finally
                {
                    if (restart)
                    {
                        try
                        {
                            ProcessStartInfo psi = new ProcessStartInfo();
                            psi.WorkingDirectory = Environment.CurrentDirectory;
                            psi.FileName = Application.ExecutablePath;
                            psi.UseShellExecute = true;
                            psi.Verb = "runas";
                            Process p = new Process();
                            p.StartInfo = psi;
                            p.Start();
                            Process.GetCurrentProcess().Kill();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("程序无法获取Windows管理员身份运行，\n请手动使用Windows管理员身份运行", "WorkingTimeRecorder");
                        }
                    }
                }
            }
        }

        private void checkBoxTopMost_CheckedChanged(object sender, EventArgs e)
        {
            if (FormSettingLoaded)
            {
                form1.SetTopMost(this.checkBoxTopMost.Checked);
                Settings.Default.topMost = this.checkBoxTopMost.Checked;
            }
        }

        private void checkBoxInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (FormSettingLoaded)
            {
                form1.SetOvertimeInfo(checkBoxInfo1.Checked || checkBoxInfo2.Checked);
                Settings.Default.inFo1 = checkBoxInfo1.Checked;
                Settings.Default.inFo2 = checkBoxInfo2.Checked;
            }
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

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string localPath = Application.ExecutablePath;
                string netPath = @"##############";
                var localFvi = FileVersionInfo.GetVersionInfo(localPath);
                var netFvi = FileVersionInfo.GetVersionInfo(netPath);
                if (CompareVer(localFvi.FileVersion, netFvi.FileVersion, netPath))
                {
                    string CmdPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Restart.bat";
                    FileStream fs = new FileStream(CmdPath, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("@echo Please do not close this window manually, otherwise the update will fail");
                    //sw.WriteLine("@echo 更新中です。このウィンドウを手動で閉じないでください。");
                    sw.WriteLine("@echo off");
                    sw.WriteLine("taskkill /f /im WorkingTimeRecorder.exe");
                    sw.WriteLine(@"del /f /s /q " + localPath);
                    sw.WriteLine("copy " + netPath + " " + localPath);
                    sw.WriteLine("start " + localPath);
                    sw.Write(@"del /f /s /q " + CmdPath);
                    sw.Close();
                    fs.Close();

                    Process.Start(CmdPath);
                }
            }
            catch
            {
                ///////////
            }
        }

        /// <summary>
        /// 比较版本号，并由用户选择是否更新
        /// </summary>
        /// <param name="ver1">本地版本号</param>
        /// <param name="ver2">服务器版本号</param>
        /// <returns></returns>
        private bool CompareVer(string ver1,string ver2, string netPath)
        {
            bool result = false;
            string[] verNum1 = ver1.Split('.');
            string[] verNum2 = ver2.Split('.');
            for (int num = 0; num < verNum2.Length; num++)
            {
                if (int.Parse(verNum2[num]) > int.Parse(verNum1[num]))
                {
                    result = true;
                    break;
                }
            }
            if (result)
            {
                string messageStr = string.Empty;
                using (StreamReader sr = new StreamReader(netPath.Replace(Path.GetFileName(netPath), "version")))
                {
                    messageStr = sr.ReadToEnd();
                }
                if (MessageBox.Show(messageStr, "发现新版本，是否更新？", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    result = false;
            }
            else
            {
                MessageBox.Show("当前版本为最新版，无需更新");
            }
            return result;
        }

        private void checkBoxMianForm_CheckedChanged(object sender, EventArgs e)
        {
            this.checkBoxMianForm.CheckedChanged -= new System.EventHandler(this.checkBoxMianForm_CheckedChanged);
            checkBoxMianForm.Checked = !checkBoxMianForm.Checked;
            this.checkBoxMianForm.CheckedChanged += new System.EventHandler(this.checkBoxMianForm_CheckedChanged);
            MessageBox.Show("暂不支持修改", "WorkingTimeRecorder");
        }

        private void checkBoxShowInTaskBar_CheckedChanged(object sender, EventArgs e)
        {
                try
                {
                    //设置任务栏显示  
                    if (checkBoxShowInTaskBar.Checked == true)
                    {
                        //Form1隐藏
                        this.Visible = false;
                        //Form2显示
                        Form2 f2 = new Form2();
                        f2.ShowDialog();
                        this.Visible = true;
                    }
                    //取消任务栏显示  
                    else
                    {
                        //Form2隐藏
                        this.Visible = false;
                        //Form1显示
                        For1 f1 = new Form1();
                        f1.ShowDialog();
                        this.Visible = true;
                    }
                }
                catch
                {
                    MessageBox.Show("暂不支持修改", "WorkingTimeRecorder");
                    this.checkBoxShowInTaskBar.CheckedChanged -= new System.EventHandler(this.checkBoxShowInTaskBar_CheckedChanged);
                    this.checkBoxAutoStart.Checked = !this.checkBoxAutoStart.Checked;
                    this.checkBoxShowInTaskBar.CheckedChanged += new System.EventHandler(this.checkBoxShowInTaskBar_CheckedChanged);
                }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.checkBox1.CheckedChanged -= new System.EventHandler(this.checkBox1_CheckedChanged);
            checkBox1.Checked = !checkBox1.Checked;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            MessageBox.Show("暂不支持修改", "WorkingTimeRecorder");
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
