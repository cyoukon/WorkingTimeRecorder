using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorkingTimeRecorder
{
    public delegate void Messagedelegate();

    public partial class Form1 : Form
    {
        public static FormSettings formSettings;//声明窗体类的静态变量
        public static Form2 form2;//声明窗体类的静态变量

        public Form1()
        {
            InitializeComponent();
            this.label1.FontChanged += new System.EventHandler(this.LabelChanged);
            this.label2.VisibleChanged += new System.EventHandler(this.LabelChanged);

            this.WindowState = Settings.Default.showMainForm ? FormWindowState.Normal : FormWindowState.Minimized;
        }

        partial void Form1_Load(object sender, EventArgs e);

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (Settings.Default.showMainForm)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //防止重复打开
            if (formSettings == null || formSettings.IsDisposed)
            {
                formSettings = new FormSettings(this);//加入this用于传值，主窗体把自己的引用传给从窗体对象
                formSettings.Show();
            }
            else
                formSettings.Activate();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出吗？", "WorkingTimeRecorder", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                this.Close();
            }
        }

        public void SetLocation(int x, int y)
        {
            Settings.Default.pointX = x;
            Settings.Default.pointY = y;
            x = x == 999999999 ? (Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width) : x;
            if (y == 999999999)
            {
                y = Screen.PrimaryScreen.WorkingArea.Size.Height - this.Height + 40;
                if (!label2.Visible)
                {
                    y = y +  label1.Height;
                }
            }
            this.Location = new Point(x, y);
        }
        public void SetFont(Font font)
        {
            this.label1.Font = font;
            this.label2.Font = font;
            label2.Top = label1.Height + 5;
            this.Height = label2.Top + label2.Height + 40;
        }
        public void SetColor(Color color)
        {
            this.label1.ForeColor = color;
            this.label2.ForeColor = color;
        }
        public void SetTopMost(bool topMost)
        {
            this.TopMost = topMost;
        }
        public void SetOvertimeInfo(bool timer)
        {
            this.timer1.Enabled = timer;
        }

        public void SetOvertimeVisibleFalse()
        {
            label2.Visible = false;
        }

        public void SetForm1Visible(bool visible)
        {
            this.Visible = visible;
        }

        public void SetForm2Visible(bool visible)
        {
            if (!(form2 == null || form2.IsDisposed))
            {
                form2.Close();
            }
            if (visible)
            {
                form2 = new Form2();
                form2.Show();
            }
        }

        /// <summary>
        /// 每分钟运行一次，检查是否到了下班时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime.TryParse(Settings.Default.startWorkTime, out DateTime offTime);
            offTime = offTime.AddHours(Settings.Default.inFoTime); // 下班时间 = 开始工作时间 + 工作时间
            TimeSpan timeSpan = GetTime.GetTimeFormat(out bool result).dateTime - offTime;
            if (timeSpan.TotalHours >= 0)
            {
                // 下班提醒 && 是否要显示提醒弹窗（每天只需要提醒一次）
                if (Settings.Default.inFo1 && Settings.Default.inFoMessageBox)
                {
                    Messagedelegate dgt = new Messagedelegate(() =>
                    {
                        MessageBox.Show("可以下班了", "WorkingTimeRecorder");
                    });
                    dgt.BeginInvoke(null, null);
                    Settings.Default.inFoMessageBox = false;
                    Settings.Default.Save();
                }
                // 显示已加班时间
                if (Settings.Default.inFo2)
                {
                    this.label2.Visible = true;
                    this.label2.Text = ("已加班 " + timeSpan).Substring(0, 9).Replace(":", "时") + "分";
                    this.label2.Refresh();
                }
            }
            else
                this.label2.Visible = false;
        }

        private void LabelChanged(object sender, EventArgs e)
        {
            this.SetLocation(Settings.Default.pointX, Settings.Default.pointY);
        }

        private void 打开log文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string savePath = string.IsNullOrEmpty(Settings.Default.savePath) ? System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase : Settings.Default.savePath;
            System.Diagnostics.Process.Start("explorer", savePath);
        }

        private void 修改出勤时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - 550;
            int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - this.Height - 150;
            string input = Microsoft.VisualBasic.Interaction.InputBox("请按以下格式输入上班开始的时间：\r\nHH:mm:ss", "WorkingTimeRecorder", Settings.Default.startWorkTime.Remove(0, 11), x, y);
            if (!string.IsNullOrEmpty(input))
            {
                try
                {
                    DateTime.ParseExact(input, "HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None);
                    TimeLog.GetInstance().ReadWorkingTime(out string Label1, true, input.ToString());
                    this.label1.Text = Label1;
                    SetForm2Visible(Settings.Default.showInTaskBar);
                    this.label2.Visible = false;
                }
                catch (FormatException)
                {
                    MessageBox.Show("输入错误，修改无效！", "WorkingTimeRecorder");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("修改失败！\r\n" + ex, "WorkingTimeRecorder");
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            TimeLog.GetInstance().End();
            Settings.Default.Save();
        }
    }
}
