﻿using System;
using System.Collections.Generic;
using System.IO;
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
        string IniPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "QuickStartSettings.ini";
        int Count = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        string[] keys = new string[]
        {
            "0","1","2","3","4","5","6","7","8","9",
            "a","b","c","d","e","f","g","h","i","j","k","l","m",
            "n","o","p","q","r","s","t","u","v","w","x","y","z"
        };

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddButton(Count);
                //IniHelper.Ini_Create(iniPath);
                IniHelper.Ini_Write("Button", "Button_" + keys[Count], "ブランク", IniPath);

                Count++;
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("无法再添加按钮了");
            }
        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = wraPanel.FindName("Button_" + keys[Count - 1]) as Button;
            StackPanel stackPanel = button.FindName("stackPanel_" + keys[Count - 1]) as StackPanel;

            stackPanel.UnregisterName("textBlock_" + keys[Count - 1]);
            stackPanel.UnregisterName("image_" + keys[Count - 1]);
            button.UnregisterName("stackPanel_" + keys[Count - 1]);
            wraPanel.UnregisterName("Button_" + keys[Count - 1]);

            wraPanel.Children.RemoveAt(Count - 1);
            IniHelper.Ini_Write("Button", "Button_" + keys[Count - 1], "ブランク", IniPath);
            Count--;
        }

        private void btn_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                IniHelper.Ini_Write("Button", (sender as Button).Name, files[0], IniPath);
                string str = (sender as Button).Name.ToString();
                StackPanel stackPanel = (sender as Button).FindName(str.Replace("Button_", "stackPanel_")) as StackPanel;
                Image image = stackPanel.FindName(stackPanel.Name.Replace("stackPanel_", "image_")) as Image;
                SetImageSource(files[0], ref image);
                TextBlock textBlock = stackPanel.FindName(stackPanel.Name.Replace("stackPanel_", "textBlock_")) as TextBlock;
                string postfix = textBlock.Text.Substring(textBlock.Text.Length - 3, 3);
                textBlock.Text = System.IO.Path.GetFileName(files[0]) + postfix;
            }
        }

        private void btn_click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string startProject = IniHelper.Ini_Read("Button", btn.Name, IniPath);
            try
            {
                System.Diagnostics.Process.Start(startProject);
                this.Close();
            }
            catch
            {
                MessageBox.Show("启动失败！");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Top = int.Parse(IniHelper.Ini_Read("Window", "top", IniPath));
                this.Left = int.Parse(IniHelper.Ini_Read("Window", "left", IniPath));
                this.Height = int.Parse(IniHelper.Ini_Read("Window", "height", IniPath));
                this.Width = int.Parse(IniHelper.Ini_Read("Window", "width", IniPath));
            }
            catch { }

            for (int i = 0; i < keys.Length; i++)
            {
                string val = IniHelper.Ini_Read("Button", "Button_" + keys[i], IniPath);
                if (val == "ブランク") return;
                AddButton(i, val);
                Count = i + 1;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string top = this.Top.ToString();
            string left = this.Left.ToString();
            string height = this.Height.ToString();
            string width = this.Width.ToString();
            IniHelper.Ini_Write("Window", "top", top, IniPath);
            IniHelper.Ini_Write("Window", "left", left, IniPath);
            IniHelper.Ini_Write("Window", "height", height, IniPath);
            IniHelper.Ini_Write("Window", "width", width, IniPath);
        }

        //参考 https://blog.csdn.net/wushang923/article/details/6688056?utm_medium=distribute.pc_relevant.none-task-blog-BlogCommendFromMachineLearnPai2-1.nonecase&depth_1-utm_source=distribute.pc_relevant.none-task-blog-BlogCommendFromMachineLearnPai2-1.nonecase
        private void AddButton(int count, string path = "")
        {
            Button btn = new Button
            {
                Name = "Button_" + keys[count],
                Height = 23,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(10, 10, 0, 0),
                AllowDrop = true
            };
            wraPanel.Children.Add(btn);
            wraPanel.RegisterName("Button_" + keys[count], btn);//注册名字，以便以后使用
            btn.Click += new RoutedEventHandler(btn_click);
            btn.Drop += new DragEventHandler(btn_Drop);

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            btn.Content = stackPanel;
            stackPanel.Name = btn.Name.Replace("Button_", "stackPanel_");
            btn.RegisterName(btn.Name.Replace("Button_", "stackPanel_"), stackPanel);

            Image image = new Image();
            SetImageSource(path, ref image);
            image.Stretch = Stretch.Uniform;
            image.HorizontalAlignment = HorizontalAlignment.Left;
            stackPanel.Children.Add(image);
            image.Name = btn.Name.Replace("Button_", "image_");
            stackPanel.RegisterName(btn.Name.Replace("Button_", "image_"), image);

            string name = System.IO.Path.GetFileName(path);
            TextBlock textBlock = new TextBlock();
            textBlock.Text = string.IsNullOrEmpty(name) ? "Button_" + keys[count] :
                    System.IO.Path.GetFileName(name) + " _" + keys[count];
            stackPanel.Children.Add(textBlock);
            textBlock.Name = btn.Name.Replace("Button_", "textBlock_");
            stackPanel.RegisterName(btn.Name.Replace("Button_", "textBlock_"), textBlock);
        }

        private void SetImageSource(string path, ref Image image)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (File.Exists(path))
                {
                    var icon = System.Drawing.Icon.ExtractAssociatedIcon(path);
                    var bitmap = icon.ToBitmap();
                    IntPtr hBitmap = bitmap.GetHbitmap();
                    ImageSource wpfBitmap =
                        System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                            hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    image.Source = wpfBitmap;
                }
                else if (Directory.Exists(path))
                {
                    image.Source = new BitmapImage(new Uri("Image\\folder.png", UriKind.Relative));
                }
                else
                {
                    //throw new PathNotExistException("路径" + path + "不存在");
                    MessageBox.Show("路径" + path + "不存在");
                }
            }
            else
            {
                image.Source = new BitmapImage(new Uri("Image\\default.png", UriKind.Relative));
            }
        }

        /* 方法添加button，不方便设置样式
        private void AddButton(int count, string path = "")
        {
            Button btn = new Button
            {
                Name = "Button_" + keys[count],
                Content = string.IsNullOrEmpty(path) ? "Button _" + keys[count] : 
                    System.IO.Path.GetFileName(path) + " _" + keys[count],
                Height = 23,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(10, 10, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                Visibility = Visibility.Visible,
                AllowDrop = true
            };
            if (!string.IsNullOrEmpty(path))
            {
                SetBackground(path, ref btn);
            }
            btn.Click += new RoutedEventHandler(btn_click);
            btn.Drop += new DragEventHandler(btn_Drop);
            wraPanel.Children.Add(btn);
        }

        private void SetBackground(string path, ref Button btn)
        {
            var icon = System.Drawing.Icon.ExtractAssociatedIcon(path);
            var bitmap = icon.ToBitmap();
            IntPtr hBitmap = bitmap.GetHbitmap();
            ImageSource wpfBitmap =
                System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = wpfBitmap;
            imageBrush.Stretch = Stretch.Uniform;
            btn.Background = imageBrush;
        }
        */

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Opacity = 1;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            Opacity = 0.6;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(string.Format("e.ImeProcessedKey = {0}, e.Key = {1}", e.ImeProcessedKey, e.Key));
            try
            {
                int enterKey = e.Key == Key.ImeProcessed ? (int)e.ImeProcessedKey : (int)(e.Key);
                enterKey -= 34;
                Button button = wraPanel.FindName("Button_" + keys[enterKey]) as Button;
                button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));// 触发button点击事件
            }
            catch
            {

            }
        }
    }

    class PathNotExistException : Exception
    {
        public PathNotExistException(string message) : base(message)
        {
        }
    }
}
