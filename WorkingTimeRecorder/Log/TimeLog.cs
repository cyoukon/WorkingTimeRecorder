using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace WorkingTimeRecorder.TimeLog
{
    class TimeLog
    {
        private string savePath = string.IsNullOrEmpty(Settings.Default.savePath) ? System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase : Settings.Default.savePath;
        /// <summary>
        /// 工作开始后的操作
        /// </summary>
        public void Start(out string Label1)
        {
            GetTime getTime =new GetTime();
            GetTime.TimeFormat time = getTime.GetTimeFormat(out bool result);
            string UnLock = "屏幕解锁时间：";
            using (StreamWriter file = new StreamWriter(savePath + time.yearMonth + "工作时间.txt", true, Encoding.UTF8))
            {
                file.WriteLine(UnLock + time.fullTime);
                file.Close();
            }
            ReadWorkingTime(out Label1);
        }
        /// <summary>
        /// 工作结束后的操作
        /// </summary>
        public void End()
        {
            GetTime getTime = new GetTime();
            GetTime.TimeFormat time = getTime.GetTimeFormat(out bool result);
            string Lock = "屏幕锁定时间：";
            using (StreamWriter file = new StreamWriter(savePath + time.yearMonth + "工作时间.txt", true, Encoding.UTF8))
            {
                file.WriteLine(Lock + time.fullTime);
                file.Close();
            }
        }

        public void ReadWorkingTime(out string Label1)
        {
            string workTimePath = savePath  + DateTime.Today.ToString("yyyyMM") + "工作时间.txt";
            StreamReader sr = new StreamReader(workTimePath);
            string startWorkTime = DateTime.MinValue.ToString();
            string endWorkTime = DateTime.MinValue.ToString();
            string read = sr.ReadToEnd();
            sr.Close();
            string[] str = read.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (str[i].Contains(DateTime.Today.ToString("yyyy/MM/dd")))
                {
                    if (str[i].Contains("解锁"))
                        startWorkTime = str[i].Remove(0, 7);
                }
                else if (str[i].Contains("锁定"))
                {
                    endWorkTime = str[i].Remove(0, 7);
                    break;
                }
            }
            string lastLine = string.Empty;
            if (File.Exists(savePath + DateTime.Today.ToString("yyyyMM") + "考勤时间.txt"))
            {
                lastLine = File.ReadLines(savePath + DateTime.Today.ToString("yyyyMM") + "考勤时间.txt").Last();
                lastLine = lastLine.Split('：', ' ')[1];
            }
            if (lastLine != startWorkTime.Split(' ')[0])
            {
                Settings.Default.startWorkTime = startWorkTime;
                Settings.Default.inFoMessageBox = true;
                Settings.Default.Save();
                WriteInto考勤时间(endWorkTime, startWorkTime);
            }
            Label1 = Settings.Default.startWorkTime;
        }

        private void WriteInto考勤时间(string endWorkTime, string startWorkTime)
        {
            using (StreamWriter sw = new StreamWriter(savePath + DateTime.Today.ToString("yyyyMM") + "考勤时间.txt", true, Encoding.UTF8))
            {
                sw.WriteLine("下班时间：" + endWorkTime);
                sw.WriteLine("上班时间：" + startWorkTime);
                sw.Close();
            }
        }
    }
}
