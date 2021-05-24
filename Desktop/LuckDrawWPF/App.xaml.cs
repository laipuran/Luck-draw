using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace LuckDraw
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static int numberOfPeople = LuckDraw.Properties.Settings.Default.numberOfPeople;
        internal static Window Floating = new Floating();
        internal static Window Window = new MainWindow();
        internal static bool closeApp = false;
        internal static NotifyIcon trayIcon;
        protected override void OnStartup(StartupEventArgs e)
        {
            ShowSplashScreen();
            AddTrayIcon();
            Floating.Show();
            Task.Run(ExitApp);
            base.OnStartup(e);
        }
        private void ShowSplashScreen()
        {
            SplashScreen s = new SplashScreen("Resources/SplashScreen.png");
            s.Show(true);
        }
        private void AddTrayIcon()
        {
            Icon icon = LuckDraw.Properties.Resources.favicon;
            trayIcon = new NotifyIcon
            {
                Icon = icon,
                Text = "Luck Draw by Puran Lai"
            };
            trayIcon.Visible = true;

            //实例化右键菜单
            ContextMenuStrip menuStrip = new ContextMenuStrip();

            menuStrip.Items.Add("显示主窗口", null, (sender, eventArgs) => {
                MainWindow.Visibility = Visibility.Visible;
                MainWindow.ShowInTaskbar = true;
                MainWindow.Activate();
            });
            menuStrip.Items.Add("退出", null, (sender, eventArgs) => {
                closeApp = true;
            });
            
            trayIcon.ContextMenuStrip = menuStrip;
        }
        private void ExitApp()
        {
            while (true)
            {
                if (closeApp)
                {
                    Window.Close();
                    Floating.Close();
                    RemoveTrayIcon();
                    return;
                }
                Task.Delay(500);
            }
        } 
        public void RemoveTrayIcon()
        {
            if (trayIcon != null)
            {
                trayIcon.Visible = false;
                trayIcon.Dispose();
                trayIcon = null;
            }
        }
    }
}
