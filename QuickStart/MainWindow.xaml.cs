using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuickStart
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        string iniPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "QuickStartSettings.ini";

        public MainWindow()
        {
            InitializeComponent();
        }

        string[] keys = new string[]
        {
            "1","2","3","4","5","6","7","8","9","0",
            "a","b","c","d","e","f","g","h","i","j","k","l","m",
            "n","o","p","q","r","s","t","u","v","w","x","y","z"
        };
        int count = 0;

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = new Button
                {
                    Name = "Button_" + keys[count],
                    Content = "Button _" + keys[count],
                    Height = 23,
                    Width = 64,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(10, 10, 0, 0),
                    VerticalAlignment = VerticalAlignment.Top,
                    Visibility = Visibility.Visible,
                    AllowDrop = true
                };
                btn.Click += new RoutedEventHandler(btn_click);
                btn.Drop += new DragEventHandler(btn_Drop);
                wraPanel.Children.Add(btn);

                //IniHelper.Ini_Create(iniPath);
                IniHelper.Ini_Write("Button", btn.Name, "ブランク", iniPath);

                count++;
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("无法再添加按钮了");
            }
        }

        private void btn_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);
                IniHelper.Ini_Write("Button", (sender as Button).Name, files[0], iniPath);
                (sender as Button).Content = System.IO.Path.GetFileName(files[0]).PadRight(40) + (sender as Button).Content;
            }
        }

        private void btn_click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string startProject = IniHelper.Ini_Read("Button", btn.Name, iniPath);
            try
            {
                System.Diagnostics.Process.Start(startProject);
            }
            catch
            {
                MessageBox.Show("启动失败！");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
