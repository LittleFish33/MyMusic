using MyMusic.Model;
using MyMusic.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MyMusic.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PersonalTrendView : Page
    {
        //  PersonalTrendModel personalTrend = PersonalTrendModel.GetInstance();
        PersonalTrendViewModel trend = PersonalTrendViewModel.GetInstance();
        PersonInfoViewModel user = PersonInfoViewModel.GetInstance();
        ContentFrameViewModel frame = ContentFrameViewModel.GetInstance();
        public PersonalTrendView()
        {
            this.InitializeComponent();
            if (user.Id != 0 && trend.AllItems.Count == 0)
            {
                warning.Text = "没有动态";
                warning.Visibility = 0;
                back.Visibility = (Visibility)1;
                head.Visibility = (Visibility)1;
            }
            else if (user.Id == 0)
            {
                warning.Text = "请先登录";
                warning.Visibility = 0;
                back.Visibility = (Visibility)1;
                head.Visibility = (Visibility)1;
            }
            else
            {
                warning.Visibility = (Visibility)1;
                back.Visibility = 0;
                head.Visibility = 0;
            }
        }

        private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBlock_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            frame.NavigateToPage(typeof(UserPage));
        }
    }
}
