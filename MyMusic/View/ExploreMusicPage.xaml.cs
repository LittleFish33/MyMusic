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
using Windows.UI.Core;
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
    public sealed partial class ExploreMusicPage : Page
    {
        public ExploreMusicViewModel ExploreMusicVm => (ExploreMusicViewModel)DataContext;
        private ContentFrameViewModel ContentFrameVm = ContentFrameViewModel.GetInstance();
        public ExploreMusicPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            DispatcherTimer time = new DispatcherTimer();
            time.Interval = new TimeSpan(0, 0, 5);
            time.Tick += Time_Tick;
            time.Start();
        }

        private void Time_Tick(object sender, object e)
        {
            int i = headerFlipView.SelectedIndex;
            i++;
            if (i >= headerFlipView.Items.Count)
            {
                i = 0;
            }
            headerFlipView.SelectedIndex = i;
        }

        private void Header_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Recommendheader SelectHeader = headerFlipView.SelectedItem as Recommendheader;
            Debug.WriteLine(SelectHeader.type);
            // TODO: 这里需要根据SelectHeader.type的值跳转到对应的页面,eg:
            //ContentFrameVm.NavigateToPage(typeof(PlayingSongPage));

        }
        private void RecommendMv_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            RecommendMVModelResult SelectMV = RecommendMv_GridView.SelectedItem as RecommendMVModelResult;
            if (SelectMV != null)
            {
                Debug.WriteLine(SelectMV.name);
                // TODO: 这里跳转到MV显示页面:eg
                //ContentFrameVm.NavigateToPage(typeof(PlayingSongPage));
            }
        }

        private void MoreMv_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            //进行页面跳转，做好了之后要进行修改
            ContentFrameVm.NavigateToPage(typeof(BlankPage));
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = ContentFrameVm.contentFrameRef.CanGoBack ?
                    AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
    }
}
