using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorkingTimeRecorder
{
    public partial class VacationForm : Form
    {
        private FormSettings formSettings = null;
        public VacationForm(FormSettings form)
        {
            InitializeComponent();
            formSettings = form;
        }

        private void radioButtonVacation_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.autoCalculateVacationDays = radioButtonAutomatic.Checked;
            if (radioButtonAutomatic.Checked)
            {
                groupBox1.Hide();
                groupBox2.Show();
            }
            else
            {
                groupBox1.Show();
                groupBox2.Hide();
            }
        }

        private void VacationForm_Load(object sender, EventArgs e)
        {
            radioButtonAutomatic.Checked = Settings.Default.autoCalculateVacationDays;
            textBox1.Text = Settings.Default.vacationDays.ToString();
            //textBox1.SelectAll();
            groupBox1.Visible = !Settings.Default.autoCalculateVacationDays;
            groupBox2.Visible = !groupBox1.Visible;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {  
            //限制只能输入数字，Backspace键，小数点
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')

            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && textBox1.Text.Trim() == "") e.Handled = true; //禁止第一个字符就输入小数点
            if (e.KeyChar == '.' && textBox1.Text.Contains(".")) e.Handled = true; //禁止输入多个小数点
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Settings.Default.vacationDays = Convert.ToSingle(textBox1.Text);
            formSettings.SetLabelDaysOff(Settings.Default.vacationDays);
            this.Close();
        }
    }
}
