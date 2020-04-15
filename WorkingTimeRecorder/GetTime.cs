using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;

namespace WorkingTimeRecorder
{
    class GetTime
    {
        public struct TimeFormat
        {
            public string fullTime;
            public string yearMonth;
            public string yearMonthDay;
            public string hourMinuteSecond;
        }

        #region 获取标准北京时间 GetBeijingTime
        ///<summary>
        /// 获取标准北京时间
        ///</summary>
        ///<returns></returns>
        private DateTime GetBeijingTime()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Settings.Default.extranet);

            request.Method = "HEAD";
            request.AllowAutoRedirect = false;
            HttpWebResponse reponse = (HttpWebResponse)request.GetResponse();
            string cc = reponse.GetResponseHeader("date");
            reponse.Close();
            DateTime time;
            bool s = GMTStrParse(cc, out time);
            return time.AddHours(8); //GMT要加8个小时才是北京时间
        }
        public bool GMTStrParse(string gmtStr, out DateTime gmtTime)  //抓取的date是GMT格式的字符串，这里转成datetime
        {
            CultureInfo enUS = new CultureInfo("en-US");
            bool s = DateTime.TryParseExact(gmtStr, "r", enUS, DateTimeStyles.None, out gmtTime);
            return s;
        }
        #endregion

        #region 调用CMD获取局域网指定电脑时间 GetLocalNetworkTime
        private DateTime GetLocalNetworkTime()
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序

            //根据是否指定IP，向cmd窗口发送输入信息
            p.StandardInput.WriteLine(@"net time " + Settings.Default.localNet + " &exit");

            p.StandardInput.AutoFlush = true;

            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();

            p.WaitForExit();//等待程序执行完退出进程
            p.Close();

            switch (System.Threading.Thread.CurrentThread.CurrentCulture.Name)
            {
                case "ja-JP":
                    output = output.Split(new char[] { 'は', 'で' }, StringSplitOptions.RemoveEmptyEntries)[1];
                    break;
                case "zh-CN":
                case "en-US":
                default:
                    System.Windows.Forms.MessageBox.Show("只支持日语系统");
                    break;
            }

            DateTime dt = DateTime.ParseExact(output, "yyyy/MM/dd HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None);
            return dt;
        }
        #endregion

        /// <summary>
        /// 返回指定格式的时间
        /// </summary>
        public TimeFormat GetTimeFormat(out bool result)
        {
            result = true;
            DateTime dateTime = DateTime.Now;
            TimeFormat timeFormat;
            try
            {
                switch (Settings.Default.comboBoxGetTime)
                {
                    case 0:
                        break;
                    case 1:
                        dateTime = GetLocalNetworkTime();
                        break;
                    case 2:
                        dateTime = GetBeijingTime();
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                result = false;
            }
            timeFormat.fullTime = dateTime.ToString("yyyy/MM/dd HH:mm:ss");
            timeFormat.yearMonth = dateTime.ToString("yyyyMM");
            timeFormat.yearMonthDay = dateTime.ToString("yyyy/MM/dd");
            timeFormat.hourMinuteSecond = dateTime.ToString("HH:mm:ss");
            return timeFormat;
        }
    }
}
