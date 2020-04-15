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
            //this.label1.Font = font;
            //this.label2.Font = font;
            //label1.Top = label2.Height + 5;
        }
        public void SetColor(Color color)
        {
            //this.label1.ForeColor = (Color)color;
            //this.label2.ForeColor = (Color)color;
        }
    }
}
