using Microsoft.Win32;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WorkingTimeRecorder
{
    public partial class Form1 : Form
    {
        partial void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            this.TransparencyKey = Color.White;
            SetPenetrate();

            this.SetFont(Settings.Default.font);
            this.SetColor(Settings.Default.color);
            this.TopMost = Settings.Default.topMost;
            TimeLog.GetInstance().Start(out string str);
            this.label1.Text = str;
            this.label2.Visible = false;
            this.SetLocation(Settings.Default.pointX, Settings.Default.pointY);
            SetForm1Visible(Settings.Default.showMainForm);
            SetForm2Visible(Settings.Default.showInTaskBar);
            SetOvertimeInfo(Settings.Default.inFo1 || Settings.Default.inFo2);
            //SystemEvents.SessionEnding += new SessionEndingEventHandler(SystemEvents_SessionEnding);
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        }

        #region 监控，记录时间
        /// <summary>
        /// 监控屏幕锁定与解锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
        {
            if (e.Reason == Microsoft.Win32.SessionSwitchReason.SessionLock)
            {
                // 屏幕锁定
                TimeLog.GetInstance().End();
            }

            else if (e.Reason == Microsoft.Win32.SessionSwitchReason.SessionUnlock)
            {
                // 屏幕解锁
                TimeLog.GetInstance().Start(out string str);
                this.label1.Text = str;
                SetForm2Visible(Settings.Default.showInTaskBar);
                this.label2.Visible = false;
            }
        }
        /*
        /// <summary>
        /// 系统注销或关闭事件处理程序，  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            //if (MessageBox.Show(this, "是否允许系统注销！", "系统提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
            //{
            //    e.Cancel = true;
            //}
            //else
            //{
            //    e.Cancel = false;
            //}
            //SessionEndReasons reason = e.Reason;
            //switch (reason)
            //{
            //    case SessionEndReasons.Logoff:
            //        MessageBox.Show("用户正在注销。操作系统继续运行，但启动此应用程序的用户正在注销。");
            //        break;
            //    case SessionEndReasons.SystemShutdown:
            //        MessageBox.Show("操作系统正在关闭。");
            //        break;
            //}
            //
            TimeLog.GetInstance().End();
        }
        //如果把上面的事件处理程序修改成如下  
        //private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)  
        //       {  
        //          e.Cancel = true; 
        //   } 

        //那会出现什么情况，你点击开始菜单关机选择注销、关机、或重新启动将会失效，电脑不能正常关机了，进一步的话把程序做成Windows服务，晕，恶作剧？ 

        //SessionEnded事件同上，事件参数类为SessionEndedEventArgs，同SessionEndingEventArgs相比少了Cancel属性，Cancel属性同一些windows下的某些事件差不多，比如Form.Closing事件，Control.Validating事件。

        //补充，如果需要获取应用程序需要的系统信息，可以访问System.Windows.Forms.SystemInformation类，这也是一个很有用的类，它提供了一组静态属性。
        */
        #endregion

        #region 设置全局热键
        public class AppHotKey
        {
            [DllImport("kernel32.dll")]
            public static extern uint GetLastError();
            //如果函数执行成功，返回值不为0。
            //如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool RegisterHotKey(
                IntPtr hWnd,                //要定义热键的窗口的句柄
                int id,                     //定义热键ID（不能与其它ID重复）          
                KeyModifiers fsModifiers,   //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效
                Keys vk                     //定义热键的内容
                );

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool UnregisterHotKey(
                IntPtr hWnd,                //要取消热键的窗口的句柄
                int id                      //要取消热键的ID
                );

            //定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）
            [Flags()]
            public enum KeyModifiers
            {
                None = 0,
                Alt = 1,
                Ctrl = 2,
                Shift = 4,
                WindowsKey = 8
            }
            /// <summary>
            /// 注册热键
            /// </summary>
            /// <param name="hwnd">窗口句柄</param>
            /// <param name="hotKey_id">热键ID</param>
            /// <param name="keyModifiers">组合键</param>
            /// <param name="key">热键</param>
            public static void RegKey(IntPtr hwnd, int hotKey_id, KeyModifiers keyModifiers, Keys key)
            {
                try
                {
                    if (!RegisterHotKey(hwnd, hotKey_id, keyModifiers, key))
                    {
                        if (Marshal.GetLastWin32Error() == 1409) { MessageBox.Show("热键被占用 ！"); }
                        else
                        {
                            MessageBox.Show("注册热键失败！");
                        }
                    }
                }
                catch (Exception) { }
            }
            /// <summary>
            /// 注销热键
            /// </summary>
            /// <param name="hwnd">窗口句柄</param>
            /// <param name="hotKey_id">热键ID</param>
            public static void UnRegKey(IntPtr hwnd, int hotKey_id)
            {
                //注销Id号为hotKey_id的热键设定
                UnregisterHotKey(hwnd, hotKey_id);
            }
        }
        private const int WM_HOTKEY = 0x312; //窗口消息-热键
        private const int WM_CREATE = 0x1; //窗口消息-创建
        private const int WM_DESTROY = 0x2; //窗口消息-销毁
        private const int CtrlShiftSpace = 0x3572; //热键ID
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM_HOTKEY: //窗口消息-热键ID
                    switch (m.WParam.ToInt32())
                    {
                        case CtrlShiftSpace: //热键ID
                            if (Settings.Default.showMainForm)
                            {
                                this.Visible = true;
                                this.WindowState = FormWindowState.Normal;//正常大小
                                this.Activate(); //激活窗体
                            }
                            break;
#if DEBUG
                        case 0x3573:
                            TimeLog.GetInstance().Start(out string str);
                            this.label1.Text = str;
                            this.label2.Visible = false;
                            SetForm2Visible(Settings.Default.showInTaskBar);
                            break;
                        case 0x3574:
                            TimeLog.GetInstance().End();
                            break;
                        case 0x3575:
                            Settings.Default.Reset();
                            break;
#endif
                        default:
                            break;
                    }
                    break;
                case WM_CREATE: //窗口消息-创建
                    AppHotKey.RegKey(Handle, CtrlShiftSpace, AppHotKey.KeyModifiers.Ctrl | AppHotKey.KeyModifiers.Shift, Keys.Space); //热键为Ctrl+Shift+空格
#if DEBUG
                    AppHotKey.RegKey(Handle, 0x3573, AppHotKey.KeyModifiers.Alt | AppHotKey.KeyModifiers.Ctrl, Keys.S);//start work
                    AppHotKey.RegKey(Handle, 0x3574, AppHotKey.KeyModifiers.Alt | AppHotKey.KeyModifiers.Ctrl, Keys.E);//end work
                    AppHotKey.RegKey(Handle, 0x3575, AppHotKey.KeyModifiers.Alt | AppHotKey.KeyModifiers.Ctrl, Keys.C);//reset settings.settings
#endif
                    break;
                case WM_DESTROY: //窗口消息-销毁
                    AppHotKey.UnRegKey(Handle, CtrlShiftSpace); //销毁热键
#if DEBUG
                    AppHotKey.UnRegKey(Handle, 0x3573); //销毁热键
                    AppHotKey.UnRegKey(Handle, 0x3574); //销毁热键
                    AppHotKey.UnRegKey(Handle, 0x3575); //销毁热键
#endif
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 鼠标穿透
        private const uint WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int GWL_STYLE = (-16);
        private const int GWL_EXSTYLE = (-20);
        private const int LWA_ALPHA = 0;

        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(
        IntPtr hwnd,
        int nIndex,
        uint dwNewLong
        );

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(
        IntPtr hwnd,
        int nIndex
        );

        [DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")]
        private static extern int SetLayeredWindowAttributes(
        IntPtr hwnd,
        int crKey,
        int bAlpha,
        int dwFlags
        );

        /// <summary> 
        /// 设置窗体具有鼠标穿透效果 
        /// </summary> 
        public void SetPenetrate()
        {
            SetWindowLong(this.Handle, GWL_EXSTYLE, WS_EX_TRANSPARENT | WS_EX_LAYERED);
            //SetLayeredWindowAttributes(this.Handle, 0, 100, LWA_ALPHA);
            SetWindowLong(this.Handle, GWL_EXSTYLE, WS_EX_TRANSPARENT | WS_EX_LAYERED);
        }
        #endregion

        #region 屏蔽快捷键关闭form1
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            //e.SuppressKeyPress = (e.Alt && e.KeyCode == Keys.F4);
            e.SuppressKeyPress = (e.KeyData == (Keys.Alt | Keys.F4)); // 阻止当前控件接收按键。
        }
        #endregion
    }
}
