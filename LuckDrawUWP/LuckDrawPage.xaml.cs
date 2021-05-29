using Microsoft.Toolkit.Uwp.Notifications; // Notifications library
using System;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace LuckDraw
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LuckDrawPage : Page
    {
        public LuckDrawPage()
        {
            this.InitializeComponent();
        }
        private bool Current_Activated(object sender, WindowActivatedEventArgs e)
        {
            bool visibility = e.WindowActivationState != CoreWindowActivationState.Deactivated;
            return visibility;
        }
        public void sendNotifications(string title, string notifications, int waitSeconds)
        {
            ToastVisual visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                    {
                        new AdaptiveText()
                        {
                            Text = title,
                            HintMaxLines = 1
                        },

                        new AdaptiveText()
                        {
                            Text = notifications
                        },

                        new AdaptiveText()
                        {
                            Text = "来自：Luck Draw"
                        }
                    }
                }
            };
            ToastContent toastContent = new ToastContent()
            {
                Visual = visual,
            };
            var toast = new ToastNotification(toastContent.GetXml());
            toast.ExpirationTime = DateTime.Now.AddSeconds(waitSeconds);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    private void GetNumber_Click(object sender, RoutedEventArgs e)
        {
            NumberComboBox.IsEnabled = false;
            GetNumberButton.IsEnabled = false;

            int number;
            int max = App.numberOfPeople;
            Union union = Algorithm.Parser(NumberComboBox.Text, max);
            if (union.number == 0)
            {
                ResultTextBlock.Text = union.message;

                NumberComboBox.IsEnabled = true;
                GetNumberButton.IsEnabled = true;
                return;
            }

            number = union.number;
            string output = Algorithm.Getter(number, max);
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["numbersLastTime"] = output;
            ResultTextBlock.Text = output;

            if (App.doShowToasts)
            {
                sendNotifications("Done!", output, 30);
            }

            GetNumberButton.IsEnabled = true;
            NumberComboBox.IsEnabled = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NumberComboBox.Text = "1";
        }

    }
}
