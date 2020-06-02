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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            this.TransparencyKey = Color.White;
        }

        partial void Form1_Load(object sender, EventArgs e);

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        public static FormSettings form;//声明窗体类的静态变量
        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //防止重复打开
            if (form == null || form.IsDisposed)
            {
                form = new FormSettings(this);//加入this用于传location值，主窗体把自己的引用传给从窗体对象
                form.Show();
            }
            else
                form.Activate();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetLocation(int x, int y)
        {
            this.Location = new Point(x, y);
            Settings.Default.pointX = x;
            Settings.Default.pointY = y;
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

        /// <summary>
        /// 每分钟运行一次，检查是否到了下班时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime.TryParse(Settings.Default.startWorkTime, out DateTime offTime);
            offTime = offTime.AddHours(Settings.Default.inFoTime); // 下班时间 = 开始工作时间 + 工作时间
            TimeSpan timeSpan = DateTime.Now - offTime;
            if (timeSpan.TotalHours >= 0)
            {
                // 下班提醒 && 是否要显示提醒弹窗（每天只需要提醒一次）
                if (Settings.Default.inFo1 && Settings.Default.inFoMessageBox)
                {
                    MessageBox.Show("可以下班了");
                    Settings.Default.inFoMessageBox = false;
                }
                // 显示已加班时间
                if (Settings.Default.inFo2)
                {
                    this.label2.Visible = true;
                    this.label2.Text = ("已加班 " + timeSpan).Substring(0, 12);
                }
            }
            else
                this.label2.Visible = false;
        }

        private void 打开log文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string savePath = string.IsNullOrEmpty(Settings.Default.savePath) ? System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase : Settings.Default.savePath;
            System.Diagnostics.Process.Start("explorer", savePath);
        }

        private void 重新判断出勤时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimeLog.TimeLog timeLog = new TimeLog.TimeLog();
            timeLog.Start(out string str);
            this.label1.Text = str;
            this.label2.Visible = false;
        }
    }
}
