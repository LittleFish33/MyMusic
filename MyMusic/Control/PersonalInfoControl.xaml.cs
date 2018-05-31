using MyMusic.Model;
using MyMusic.View;
using MyMusic.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MyMusic.ViewModel;
using System.Diagnostics;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace MyMusic.Control
{
    public sealed partial class PersonalInfoControl : UserControl
    {
        private ContentFrameViewModel ContentFrameVm = ContentFrameViewModel.GetInstance();
        public PersonInfoViewModel viewModel = PersonInfoViewModel.GetInstance();
        private PersonInfoViewModel user = PersonInfoViewModel.GetInstance();
        public PersonalInfoControl()
        {
            this.InitializeComponent();
        }
        private void LogIn_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            // 进行页面跳转，做好了之后要进行修改
            if(user.Id != 0)
            {
                ContentFrameVm.NavigateToPage(typeof(UserPage));
            }
            else
            {
                ContentFrameVm.NavigateToPage(typeof(LogInPage));
            }
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = ContentFrameVm.contentFrameRef.CanGoBack ?
                AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
    }
}
