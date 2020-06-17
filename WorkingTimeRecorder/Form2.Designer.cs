using System.Drawing;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Data;
using System.ComponentModel;
using System.Collections;

namespace WorkingTimeRecorder
{
    partial class Form2
    {
        [DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll", EntryPoint = "SetParent")]
        static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle lpRect);
        [DllImport("user32.dll")]
        static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);
        static IntPtr hShell = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Shell_TrayWnd", null);
        static IntPtr hBar = FindWindowEx(hShell, IntPtr.Zero, "ReBarWindow32", null);
        static IntPtr hMin = FindWindowEx(hBar, IntPtr.Zero, "MSTaskSwWClass", null);

        Rectangle rcShell = new Rectangle();
        Rectangle rcBar = new Rectangle();
        Rectangle rcMin = new Rectangle();
        Rectangle rcMin_backup = new Rectangle();

        /// <summary>
        /// 任务栏尺寸取得
        /// </summary>
        private void asd()
        {
            GetWindowRect(hShell, ref rcShell);
            GetWindowRect(hBar, ref rcBar);
            GetWindowRect(hMin, ref rcMin);
            rcMin_backup = rcMin;
            SetParent(this.Handle, hMin);
            Rectangle screen = System.Windows.Forms.SystemInformation.VirtualScreen;
            int sWidth = screen.Width;
            int sHeight = screen.Height;

            MoveWindow(hMin, 0, 0, rcMin.Right - rcMin.Left - this.Width, rcMin.Bottom - rcMin.Top, true);
            MoveWindow(this.Handle, (rcMin.Width - this.Width) + 300, -45, this.Width, this.Height, true);
        }

        public delegate void tempChange(object sender, EventArgs e);
        public event tempChange OntempChange;
        public Rectangle Temp
        {
            get
            {
                return rcMin;
            }
            set
            {
                if (rcMin != value)
                {
                    OntempChange(this, new EventArgs());
                }
                rcMin = value;
                asd();
            }
        }


        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.components = new System.ComponentModel.Container();
            this.AutoScaleDimensions = new System.Drawing.SizeF(500, 100);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 100);
            this.Name = "Form2";
            this.TopMost = true;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.ResumeLayout(false);
            this.PerformLayout();
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);

        }

        #endregion
        [System.Runtime.InteropServices.DllImport("gdi32", CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateFont(int H, int W, int E, int O, int FW, int I, int u, int S, int C, int OP, int CP, int Q, int PAF, string F);
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern IntPtr GetDC(IntPtr hwnd);
        [System.Runtime.InteropServices.DllImport("gdi32")]
        private static extern IntPtr BeginPath(IntPtr hdc);
        [System.Runtime.InteropServices.DllImport("gdi32")]
        private static extern IntPtr EndPath(IntPtr hdc);
        [System.Runtime.InteropServices.DllImport("gdi32")]
        private static extern IntPtr PathToRegion(IntPtr hdc);
        [System.Runtime.InteropServices.DllImport("gdi32")]
        private static extern int SetBkMode(IntPtr hdc, int nBkMode);
        [System.Runtime.InteropServices.DllImport("gdi32")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);
        [System.Runtime.InteropServices.DllImport("gdi32")]
        private static extern int TextOut(IntPtr hdc, int x, int y, string lpString, int nCount);
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern IntPtr SetWindowRgn(IntPtr hwnd, IntPtr hRgn, bool bRedraw);

        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //创建新字体
            IntPtr MyFont = CreateFont(16, 10, 0, 0, 700, 0, 0, 0, 1, 0, 0, 0, 0, "メイリオ");
            //取得设备句柄
            IntPtr MyDC = GetDC(this.Handle);
            //设置背景模式
            SetBkMode(MyDC, 1);
            //开始记录轮廓路径
            BeginPath(MyDC);
            //设置文字输出字体
            IntPtr MyOldFont = SelectObject(MyDC, MyFont);
            //输出文字
            TextOut(MyDC, 50, 50, "出勤時間:", 10);
            TextOut(MyDC, 50, 70, Settings.Default.startWorkTime.Remove(0, 11), 10);
            //恢复原始字体对象
            SelectObject(MyDC, MyOldFont);
            //结束记录轮廓路径绘制
            EndPath(MyDC);
            //创建文字区域
            IntPtr MyRgn = PathToRegion(MyDC);
            //使用文字区域创建程序窗体
            SetWindowRgn(this.Handle, MyRgn, true);
        }
        private System.Windows.Forms.Timer timer2;

    }
}