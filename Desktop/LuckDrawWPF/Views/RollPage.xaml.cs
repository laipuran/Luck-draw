/*
 * From Litrop
 * https://github.com/Litrop/roll
 * Thanks very much
*/

using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LuckDraw
{
    /// <summary>
    /// RollPage.xaml 的交互逻辑
    /// </summary>
    public partial class RollPage : Page
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
