using System;
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
            
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 100;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this, "出勤時間" + Settings.Default.startWorkTime);
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
            MoveWindow(hMin, 0, 0, rcMin.Right - rcMin.Left, rcMin.Bottom - rcMin.Top, true);
        }
    }
}
