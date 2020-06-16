using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileVersion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FileVer(out string ver);
            label1.Text = "当前版本为" + ver + "是否需要更改？";
            textBox1.Text = ver;
        }

        List<string> lines;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 7)
            {
                lines[36] = lines[36].Replace(label1.Text.Substring(5 ,7), textBox1.Text);
                File.WriteAllLines("AssemblyInfo.cs", lines.ToArray());
                Close();
            }
            else
            {
                MessageBox.Show("版本号输入错误");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FileVer(out string ver)
        {
            lines = new List<string>(File.ReadAllLines("AssemblyInfo.cs"));
            ver = lines[36].Remove(0, 32).Remove(7);
        }
    }
}
