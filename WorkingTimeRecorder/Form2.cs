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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            SetParent(this.Handle, hBar);
            moveform2window();
            FormClosed += Form2_FormClosed;
        }
        /// <summary>
        /// 任务栏表示时位置实时调整
        /// </summary>
        private void timer2_Tick(object sender, EventArgs e)
        {
            GetWindowRect(hMin, ref rcMin);
            if (rcMin != rcMin_backup)
            {
                moveform2window();
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            GetWindowRect(hMin, ref rcMin);
            MoveWindow(hMin, 0, 0, rcMin.Right - rcMin.Left + 10, rcMin.Bottom - rcMin.Top, true);
        }
    }
}
