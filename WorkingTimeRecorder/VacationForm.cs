using System;
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

        private void VacationForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = Settings.Default.vacationDays.ToString();
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
            Log.WriteLog($"手动修改了休假天数，修改后剩余天数为{Settings.Default.vacationDays}天");
            formSettings.SetLabelDaysOff(Settings.Default.vacationDays);
            this.Close();
        }
    }
}
