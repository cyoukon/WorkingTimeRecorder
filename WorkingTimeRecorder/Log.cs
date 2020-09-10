using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorkingTimeRecorder
{
    class Log
    {
        public static void WriteLog(String msg)
        {
            StreamWriter writer = null;
            try
            {
                writer = File.AppendText(Application.ProductName + ".log");
                writer.WriteLine("【{0}】 {1}", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"), msg);
                writer.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
    }
}
