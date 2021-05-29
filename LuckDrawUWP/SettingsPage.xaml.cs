using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace LuckDraw
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            App.numberOfPeople = Algorithm.Parser(NumberTextBox.Text, 10000).number;
            App.doShowToasts = ToastToggleSwitch.IsOn;
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["numberOfPeople"] = App.numberOfPeople;
            localSettings.Values["doShowToasts"] = App.doShowToasts;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NumberTextBox.Text = App.numberOfPeople.ToString();
            ToastToggleSwitch.IsOn = App.doShowToasts;
        }
    }
}
