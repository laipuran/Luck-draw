﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Data.Json;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace LuckDraw
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            FrameOfMainPage.Navigate(typeof(LuckDrawPage));
        }

        private void ListBoxOfMainPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LuckDrawListBoxItem.IsSelected)
            {
                FrameOfMainPage.Navigate(typeof(LuckDrawPage));
            }
            else if (SettingsListBoxItem.IsSelected)
            {
                FrameOfMainPage.Navigate(typeof(SettingsPage));
            }
            else if (RollListBoxItem.IsSelected)
            {
                FrameOfMainPage.Navigate(typeof(RollPage));
            }
        }
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            SplitViewOfMainPage.IsPaneOpen = !SplitViewOfMainPage.IsPaneOpen;
        }

    }
}