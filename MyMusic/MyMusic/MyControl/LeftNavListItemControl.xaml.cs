using MyMusic.Model;
using System;
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

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace MyMusic.MyControl
{
    public sealed partial class LeftNavListItemControl : UserControl
    {
        private Type type = null;

        public LeftNavListItemControl()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty MenuSourceProperty =
            DependencyProperty.Register("ModelSource", typeof(LeftNavListItemModel), typeof(LeftNavListItemControl), new PropertyMetadata(null, (s, e) =>
            {
                var listItemControl = s as LeftNavListItemControl;
                if (listItemControl != null)
                {
                    LeftNavListItemModel model = e.NewValue as LeftNavListItemModel;
                    listItemControl.icon.Symbol = model.Symbol;
                    listItemControl.navTextBlock.Text = model.DisplayName;
                    listItemControl.type = model.PageType;
                }
            }));

        public LeftNavListItemModel ModelSource
        {
            get { return (LeftNavListItemModel)GetValue(MenuSourceProperty); }
            set { SetValue(MenuSourceProperty, value); }
        }
    }
}
