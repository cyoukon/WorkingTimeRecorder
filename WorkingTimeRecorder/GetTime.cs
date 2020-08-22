using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;

namespace WorkingTimeRecorder
{
    static class GetTime
    {
        public struct TimeFormat
        {
            public DateTime dateTime;
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
        private static DateTime GetBeijingTime()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Settings.Default.extranet);

            // 设置代理
            if (Settings.Default.isProxy)
            {
                ServicePointManager.Expect100Continue = false;
                WebProxy mWebProxy = new WebProxy();
                Uri newUri = new Uri("http://" + Settings.Default.extranetProxy + ":" + Settings.Default.extranetPort);
                mWebProxy.Address = newUri;
                mWebProxy.Credentials = new NetworkCredential(Settings.Default.extranetUserName, Settings.Default.extranetUserPwd);
                request.Proxy = mWebProxy;
            }

            request.Method = "HEAD";
            request.AllowAutoRedirect = false;
            HttpWebResponse reponse = (HttpWebResponse)request.GetResponse();
            string cc = reponse.GetResponseHeader("date");
            reponse.Close();
            DateTime time;
            bool s = GMTStrParse(cc, out time);
            return time.AddHours(8); //GMT要加8个小时才是北京时间
        }
        private static bool GMTStrParse(string gmtStr, out DateTime gmtTime)  //抓取的date是GMT格式的字符串，这里转成datetime
        {
            CultureInfo enUS = new CultureInfo("en-US");
            bool s = DateTime.TryParseExact(gmtStr, "r", enUS, DateTimeStyles.None, out gmtTime);
            return s;
        }
        #endregion

        #region 调用CMD获取局域网指定电脑时间 GetLocalNetworkTime
        private static DateTime GetLocalNetworkTime()
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
                    output = output.Split(new char[] { 'は', 'で' }, StringSplitOptions.RemoveEmptyEntries)[1].Replace('?', ' ');
                    break;
                case "zh-CN":
                    string[] str = output.Split(new string[] { "是" ,"\r\n"}, System.StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i<str.Length; i++)
                    {
                        if (str[i].Contains("时间"))
                        {
                            output = str[i + 1].Replace('?', ' ');
                            break;
                        }
                    }
                    break;
                case "en-US":
                default:
                    System.Windows.Forms.MessageBox.Show("只支持中文和日语系统");
                    break;
            }
            //使用DateTime.ParseExact，字符串表示形式的格式必须与指定的格式完全匹配，否则会引发异常(如 当 月份只有一位时)
            //DateTime dt = DateTime.ParseExact(output, "yyyy/MM/dd HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None);
            DateTime dt = Convert.ToDateTime(output, new DateTimeFormatInfo());
            return dt;
        }
        #endregion

        /// <summary>
        /// 返回指定格式的时间
        /// </summary>
        public static TimeFormat GetTimeFormat(out bool result)
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
            timeFormat.dateTime = dateTime;
            timeFormat.fullTime = dateTime.ToString("yyyy/MM/dd HH:mm:ss");
            timeFormat.yearMonth = dateTime.ToString("yyyyMM");
            timeFormat.yearMonthDay = dateTime.ToString("yyyy/MM/dd");
            timeFormat.hourMinuteSecond = dateTime.ToString("HH:mm:ss");
            return timeFormat;
        }
    }
}
