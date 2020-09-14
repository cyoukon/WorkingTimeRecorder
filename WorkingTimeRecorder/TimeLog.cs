using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace WorkingTimeRecorder
{
    class TimeLog
    {
        static TimeLog instance = null;
        private TimeLog() { Debug.WriteLine("实例化一次!"); }
        public static TimeLog GetInstance()
        {
            if (instance == null)
            {
                instance = new TimeLog();
            }
            return instance;
        }

        private string savePath = string.IsNullOrEmpty(Settings.Default.savePath) ? System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase : Settings.Default.savePath;
        private GetTime.TimeFormat time;

        /// <summary>
        /// 工作开始后的操作
        /// </summary>
        public void Start(out string Label1)
        {
            time = GetTime.GetTimeFormat(out bool result);
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
            time = GetTime.GetTimeFormat(out bool result);
            string Lock = "屏幕锁定时间：";
            using (StreamWriter file = new StreamWriter(savePath + time.yearMonth + "工作时间.txt", true, Encoding.UTF8))
            {
                file.WriteLine(Lock + time.fullTime);
                file.Close();
            }
        }

        public void ReadWorkingTime(out string Label1, bool changeStartWorkTime = false, string newStartWorkTime = null)
        {
            string workTimePath = savePath + time.yearMonth + "工作时间.txt";
            StreamReader sr = new StreamReader(workTimePath);
            string startWorkTime = DateTime.MinValue.ToString();
            string endWorkTime = DateTime.MinValue.ToString();
            string read = sr.ReadToEnd();
            sr.Close();
            string[] str = read.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            int changeIndex = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (str[i].Contains(time.yearMonthDay))
                {
                    if (str[i].Contains("解锁"))
                    {
                        startWorkTime = str[i].Remove(0, 7);
                        changeIndex = i;
                    }
                }
                else if (str[i].Contains("锁定"))
                {
                    endWorkTime = str[i].Remove(0, 7);
                    break;
                }
            }

            if (changeStartWorkTime)
            {
                newStartWorkTime = time.yearMonthDay + " " + newStartWorkTime;
                str[changeIndex] = str[changeIndex].Replace(startWorkTime, newStartWorkTime);
                startWorkTime = newStartWorkTime;
                string write = string.Empty;
                for (int i = 0; i < str.Length; i++)
                {
                    write += str[i] + "\r\n";
                }
                File.Delete(workTimePath);
                using (StreamWriter sw = new StreamWriter(workTimePath, true, Encoding.UTF8))
                {
                    sw.Write(write);
                    sw.Close();
                }
            }

            string lastLine = string.Empty;
            if (File.Exists(savePath + time.yearMonth + "考勤时间.txt"))
            {
                lastLine = File.ReadLines(savePath + time.yearMonth + "考勤时间.txt").Last();
                lastLine = lastLine.Split('：', ' ')[1];
            }
            if (lastLine != startWorkTime.Split(' ')[0] || changeStartWorkTime)
            {
                Settings.Default.startWorkTime = startWorkTime;
                Settings.Default.inFoMessageBox = true;
                Settings.Default.Save();
                if (changeStartWorkTime)
                {
                    List<string> lines = new List<string>(File.ReadAllLines(savePath + time.yearMonth + "考勤时间.txt"));
                    lines.RemoveAt(lines.Count - 1);
                    lines.Add("上班时间：" + startWorkTime);
                    File.WriteAllLines(savePath + time.yearMonth + "考勤时间.txt", lines.ToArray());
                }
                else
                {
                    WriteInto考勤时间(endWorkTime, startWorkTime);
                }
            }
            if (string.IsNullOrEmpty(Settings.Default.startWorkTime))
            {
                Settings.Default.startWorkTime = startWorkTime;
            }
            Label1 = startWorkTime;
        }

        private void WriteInto考勤时间(string endWorkTime, string startWorkTime)
        {
            using (StreamWriter sw = new StreamWriter(savePath + time.yearMonth + "考勤时间.txt", true, Encoding.UTF8))
            {
                // 判断是否跨月份
                if (endWorkTime == DateTime.MinValue.ToString())
                {
                    Settings.Default.vacationDays++; // 每月第一天，休假自动加一
                    Settings.Default.Save();
                    Log.WriteLog($"休假天数自动加一，加一后天数为{Settings.Default.vacationDays}天");

                    try
                    {
                        string lastPath = savePath + time.dateTime.AddMonths(-1).ToString("yyyyMM") + "工作时间.txt";
                        string[] lines = File.ReadAllLines(lastPath);
                        for (int i = lines.Length - 1; i >= 0; i--)
                        {
                            if (lines[i].Contains("锁定"))
                            {
                                sw.WriteLine("下班时间：" + lines[i].Remove(0, 7));
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.WriteLog(ex.ToString());
                    }
                }
                else
                {
                    sw.WriteLine("下班时间：" + endWorkTime);
                }
                sw.WriteLine("上班时间：" + startWorkTime);
                sw.Close();
            }
            new Update(true); // 自动检查更新
        }
    }
}
