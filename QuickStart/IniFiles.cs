using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace QuickStart
{
    /// <summary>
    /// ini文件操作类
    /// </summary>
    public class IniHelper
    {
        #region 动态链接库调用
        /// <summary>
        /// 调用动态链接库读取值
        /// </summary>
        /// <param name="lpAppName">ini节名</param>
        /// <param name="lpKeyName">ini键名</param>
        /// <param name="lpDefault">默认值：当无对应键值，则返回该值。</param>
        /// <param name="lpReturnedString">结果缓冲区</param>
        /// <param name="nSize">结果缓冲区大小</param>
        /// <param name="lpFileName">ini文件位置</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedString,
            int nSize,
            string lpFileName);

        /// <summary>
        /// 调用动态链接库写入值
        /// </summary>
        /// <param name="mpAppName">ini节名</param>
        /// <param name="mpKeyName">ini键名</param>
        /// <param name="mpDefault">写入值</param>
        /// <param name="mpFileName">文件位置</param>
        /// <returns>0：写入失败 1：写入成功</returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(
            string mpAppName,
            string mpKeyName,
            string mpDefault,
            string mpFileName);
        #endregion

        /// <summary>
        /// 读ini文件
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <returns>返回读取值</returns>
        public static string Ini_Read(string section, string key, string path)
        {
            StringBuilder stringBuilder = new StringBuilder(1024); // 定义一个最大长度为1024的可变字符串
            GetPrivateProfileString(section, key, "ブランク", stringBuilder, 1024, path); // 读取INI文件
            string value = stringBuilder.ToString(); // stringBuilder.ToString()
            if (value.Contains("//"))
            {
                // "//"后面的算作注释
                value = value.Split(new string[] { "//" }, System.StringSplitOptions.RemoveEmptyEntries)[0].TrimEnd();
            }
            return value;
        }

        /// <summary>
        /// 写ini文件
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <param name="iValue">待写入值</param>
        /// <param name="comments">ini文件注释</param>
        public static void Ini_Write(string section, string key, string iValue, string path, string comments = null)
        {
            if (!string.IsNullOrEmpty(comments))
            {
                iValue = iValue.PadRight(50 - key.Length) + "//" + comments;
            }
            WritePrivateProfileString(section, key, iValue, path); // 写入
        }

        /// <summary>
        /// 根据文件名创建文件
        /// </summary>
        /// <param name="path">文件名称以及路径</param>
        public static void Ini_Create(string path)
        {
            // 判断是否存在相关文件
            if (!File.Exists(path))
            {
                FileStream _fs = File.Create(path); // 不存在则创建ini文件
                _fs.Close(); // 关闭文件，解除占用
            }
        }

        /// <summary>
        /// 删除ini文件中键
        /// </summary>
        /// <param name="section">节名称</param>
        /// <param name="key">键名称</param>
        /// <param name="path">ini文件路径</param>
        public static void Ini_Del_Key(string section, string key, string path)
        {
            WritePrivateProfileString(section, key, null, path);  // 写入
        }

        /// <summary>
        /// 删除ini文件中节
        /// </summary>
        /// <param name="section">节名</param>
        /// <param name="path">ini文件路径</param>
        public static void Ini_Del_Section(string section, string path)
        {
            WritePrivateProfileString(section, null, null, path); // 写入
        }
    }
}