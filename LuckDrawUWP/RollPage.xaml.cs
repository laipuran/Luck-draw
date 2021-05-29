using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板
// From Litrop: https://github.com/Litrop/roll

namespace LuckDraw
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public partial class RollPage
    {
        public RollPage()
        {
            InitializeComponent();
        }
        private bool isRolling = false;
        int max = App.numberOfPeople;
        Random r = new Random();
        private async void GetNumberButton_Click(object sender, RoutedEventArgs e)
        {
            isRolling = !isRolling;
            while (isRolling)
            {
                GetNumberButton.Content = "结束";
                int num = r.Next(1, max + 1);
                string number = num.ToString();
                NumberTextBlock.Text = number;
                await Task.Delay(100);
            }
            GetNumberButton.Content = "开始";

        }
    }
}
