using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WorkingTimeRecorder
{
    public partial class FormProxy : Form
    {
        Random rand = new Random();

        public FormProxy()
        {
            InitializeComponent();
        }

        private void checkBoxProxy_CheckedChanged(object sender, EventArgs e)
        {
            textBoxProxy.Enabled = checkBoxProxy.Checked;
            textBoxPort.Enabled = checkBoxProxy.Checked;
            textBoxName.Enabled = checkBoxProxy.Checked;
            textBoxPwd.Enabled = checkBoxProxy.Checked;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Settings.Default.isProxy = checkBoxProxy.Checked;
            Settings.Default.extranetProxy = textBoxProxy.Text;
            Settings.Default.extranetPort = textBoxPort.Text;
            Settings.Default.extranetUserName = textBoxName.Text;
            Settings.Default.extranetUserPwd = textBoxPwd.Text;
            Settings.Default.Save();
            this.Close();
        }

        private void FormProxy_Load(object sender, EventArgs e)
        {
            //System.Threading.Thread t;
            //t = new System.Threading.Thread(() =>
            //{
            //    if (this.InvokeRequired)
            //    {
            //        this.Invoke(new Action(() =>
            //        {
            //            Random rand = new Random();
            //            while (button1.Enabled)
            //            {
            //                int c1 = rand.Next(0, 244);
            //                int c2 = rand.Next(0, 244);
            //                int c3 = rand.Next(0, 244);
            //                this.BackColor = Color.FromArgb(c1, c2, c3);
            //                Application.DoEvents();
            //                System.Threading.Thread.Sleep(1000);
            //            }
            //        }));
            //    }
            //});
            //t.IsBackground = true;
            //t.Start();

            checkBoxProxy.Checked = Settings.Default.isProxy;
            textBoxProxy.Text = Settings.Default.extranetProxy;
            textBoxPort.Text = Settings.Default.extranetPort;
            textBoxName.Text = Settings.Default.extranetUserName;
            textBoxPwd.Text = Settings.Default.extranetUserPwd;
            checkBoxProxy_CheckedChanged(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int c1 = rand.Next(0, 244);
            int c2 = rand.Next(0, 244);
            int c3 = rand.Next(0, 244);
            this.BackColor = Color.FromArgb(c1, c2, c3);
        }
    }
}
