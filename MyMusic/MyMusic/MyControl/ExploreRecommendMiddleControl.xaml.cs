using MyMusic.Model;
using MyMusic.View;
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

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace MyMusic.MyControl
{
    public sealed partial class ExploreRecommendMiddleControl : UserControl
    {
        string State;
        private ContentFrameViewModel ContentFrameVm = ContentFrameViewModel.GetInstance();

        public ExploreRecommendMiddleControl()
        {
            this.InitializeComponent();
            
            //自适应控制，当窗口的尺寸改变时调用下面的函数，更新状态
            this.SizeChanged += (s, e) =>
            {
                if (e.NewSize.Width < 640)
                {
                    State = "NarrowLayout";
                }
                else if (e.NewSize.Width > 640)
                {
                    State = "WideLayout";
                }

                VisualStateManager.GoToState(this, State, true);
            };

        }

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(1);
            ContentFrameVm.NavigateToPage(typeof(PlayingSongPage));
        }
    }
}
