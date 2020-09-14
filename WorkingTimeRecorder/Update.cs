using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WorkingTimeRecorder
{
    class Update
    {
        readonly string netPath = @"\\ZHAOKUN\_readonly_share\WorkingTimeRecorder\WorkingTimeRecorder.exe";

        bool auto;

        public Update(bool auto = false)
        {
            this.auto = auto;
            try
            {
                string localPath = Application.ExecutablePath;
                var localFvi = FileVersionInfo.GetVersionInfo(localPath);
                var netFvi = FileVersionInfo.GetVersionInfo(netPath);
                if (CompareVer(localFvi.FileVersion, netFvi.FileVersion, netPath))
                {
                    string CmdPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Restart.bat";
                    FileStream fs = new FileStream(CmdPath, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("@echo Please do not close this window manually, otherwise the update will fail");
                    //sw.WriteLine("@echo 更新中です。このウィンドウを手動で閉じないでください。");
                    sw.WriteLine("@echo off");
                    sw.WriteLine("taskkill /f /im WorkingTimeRecorder.exe");
                    sw.WriteLine(@"del /f /s /q " + localPath);
                    sw.WriteLine("copy " + netPath + " " + localPath);
                    sw.WriteLine("start " + localPath);
                    sw.Write(@"del /f /s /q " + CmdPath);
                    sw.Close();
                    fs.Close();

                    Process.Start(CmdPath);
                }
            }
            catch (Exception ex)
            {
                if (!auto) MessageBox.Show("检查更新失败！");

                Log.WriteLog("检查更新失败！\r\n" + ex.ToString().Replace(netPath, Application.ProductName));
            }
        }

        /// <summary>
        /// 比较版本号，并由用户选择是否更新
        /// </summary>
        /// <param name="ver1">本地版本号</param>
        /// <param name="ver2">服务器版本号</param>
        /// <returns></returns>
        private bool CompareVer(string ver1, string ver2, string netPath)
        {
            bool result = false;
            string[] verNum1 = ver1.Split('.');
            string[] verNum2 = ver2.Split('.');
            for (int num = 0; num < verNum2.Length; num++)
            {
                if (int.Parse(verNum2[num]) > int.Parse(verNum1[num]))
                {
                    result = true;
                    break;
                }
            }
            if (result)
            {
                string messageStr = string.Empty;
                using (StreamReader sr = new StreamReader(netPath.Replace(Path.GetFileName(netPath), "version")))
                {
                    messageStr = sr.ReadToEnd();
                }
                if (MessageBox.Show(messageStr, "发现新版本，是否更新？", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    result = false;
            }
            else if (!auto)
            {
                MessageBox.Show("当前版本为最新版，无需更新");
            }
            return result;
        }
    }
}
