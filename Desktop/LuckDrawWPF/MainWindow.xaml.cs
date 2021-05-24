using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace LuckDraw
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool MenuClosed = true;

        public MainWindow()
        {
            InitializeComponent();

            MiddleStackPanel.Width = 56;
            BackButton.Visibility = Visibility.Collapsed;
            LuckDrawListBoxItem.IsSelected = true;
            TitleTextBlock.Text = "抽奖";
            // Task.Run(GetBingWallPaper);
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuClosed)
            {
                Storyboard openMenu = (Storyboard)HamburgerButton.FindResource("OpenMenu");
                openMenu.Begin();
                MenuClosed = false;
                return;
            }
            else
            {
                Storyboard closeMenu = (Storyboard)HamburgerButton.FindResource("CloseMenu");
                closeMenu.Begin();
                MenuClosed = true;
                return;
            }
        }
        private void ListBoxOfMainWindow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LuckDrawListBoxItem.IsSelected)
            {
                FrameOfMainWindow.NavigationService.Navigate(new Uri("Views/LuckDrawPage.xaml", UriKind.Relative));
                BackButton.Visibility = Visibility.Collapsed;
                TitleTextBlock.Text = "抽奖";
                return;
            }
            if (RollListBoxItem.IsSelected)
            {
                FrameOfMainWindow.NavigationService.Navigate(new Uri("Views/RollPage.xaml", UriKind.Relative));
                BackButton.Visibility = Visibility.Visible;
                TitleTextBlock.Text = "摇奖";
                return;
            }
            if (SettingsListBoxItem.IsSelected)
            {
                FrameOfMainWindow.NavigationService.Navigate(new Uri("Views/SettingsPage.xaml", UriKind.Relative));
                BackButton.Visibility = Visibility.Visible;
                TitleTextBlock.Text = "设置";
                return;
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (FrameOfMainWindow.CanGoBack)
            {
                FrameOfMainWindow.GoBack();
                if (FrameOfMainWindow.Source.ToString() == "Views/LuckDrawPage.xaml")
                {
                    LuckDrawListBoxItem.IsSelected = true;
                    return;
                }
                if (FrameOfMainWindow.Source.ToString() == "Views/RollPage.xaml")
                {
                    RollListBoxItem.IsSelected = true;
                    return;
                }
                if (FrameOfMainWindow.Source.ToString() == "Views/SettingsPage.xaml")
                {
                    SettingsListBoxItem.IsSelected = true;
                    return;
                }
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            ShowInTaskbar = false;
            e.Cancel = !App.closeApp;

            base.OnClosing(e);
        }
        protected override void OnClosed(EventArgs e)
        {
            Properties.Settings.Default.numberOfPeople = App.numberOfPeople;
            Properties.Settings.Default.Save();

            base.OnClosed(e);
        }
        public void GetBingWallPaper()
        {
            var client = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            var html = client.DownloadString("https://cn.bing.com/");
            var match = Regex.Match(html, "id=\"bgLink\".*?href=\"(.+?)\"");
            string url = string.Format("https://cn.bing.com{0}", match.Groups[1].Value);

            var filePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Console.WriteLine(filePath);
            try
            {
                client.DownloadFile(url, filePath);
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(filePath));
                brush.Stretch = Stretch.UniformToFill;
                Background = brush;
            }
            catch { }
        }
    }
}
