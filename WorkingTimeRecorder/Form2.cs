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
            asd();
        }
        /// <summary>
        /// 任务栏表示时位置实时调整
        /// </summary>
        private void timer2_Tick(object sender, EventArgs e)
        {
            GetWindowRect(hMin, ref rcMin);
            if (rcMin != rcMin_backup)
            {
                rcMin_backup = rcMin;
                MoveWindow(hMin, 0, 0, rcMin.Right - rcMin.Left - this.Width, rcMin.Bottom - rcMin.Top, true);
                MoveWindow(this.Handle, (rcMin.Width - this.Width) + 300, -45, this.Width, this.Height, true);
            }
        }

    }
}
