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
using MyMusic.ViewModel;
using MyMusic.Model;
using Windows.UI.Core;
using System.Diagnostics;
using MyMusic.View;
using Windows.Storage;

namespace MyMusic
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel MainPageVm => (MainViewModel)DataContext;
        private ContentFrameViewModel _contentFrameVm = ContentFrameViewModel.GetInstance();

        public ContentFrameViewModel ContentFrameVm { get => _contentFrameVm; }

        public MainPage()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            Debug.WriteLine(localFolder.Path);
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += BackRequested;
            ContentFrame.Navigate(typeof(ExploreMusicPage));
            _contentFrameVm.contentFrameRef = ContentFrame;
        }

        //后退按钮点击事件
        private void BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (ContentFrame != null && ContentFrame.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                ContentFrame.GoBack();
                _contentFrameVm.setCurContentName(ContentFrame.CurrentSourcePageType.FullName);
                if (MainPageVm.NavListDictionary.ContainsKey(_contentFrameVm.CurContentName))
                {
                    NavListView.SelectedIndex = MainPageVm.NavListDictionary[_contentFrameVm.CurContentName];
                    NavMyMusicListView.SelectionMode = ListViewSelectionMode.None;
                    NavMyMusicListView.SelectionMode = ListViewSelectionMode.Single;
                }
                else
                {
                    NavMyMusicListView.SelectedIndex = MainPageVm.NavMyMusicListDictionary[_contentFrameVm.CurContentName];
                    NavListView.SelectionMode = ListViewSelectionMode.None;
                    NavListView.SelectionMode = ListViewSelectionMode.Single;
                }
                
            }
        }

        private void NavList_ItemClick(object sender, ItemClickEventArgs e)
        {
            LeftNavListItemModel SelectedItem = (LeftNavListItemModel)(e.ClickedItem);
            _contentFrameVm.NavigateToPage(SelectedItem.PageType);
            NavMyMusicListView.SelectionMode = ListViewSelectionMode.None;
            NavMyMusicListView.SelectionMode = ListViewSelectionMode.Single;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = ContentFrame.CanGoBack ?
                    AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void NavMyMusicList_ItemClick(object sender, ItemClickEventArgs e)
        {
            LeftNavListItemModel SelectedItem = (LeftNavListItemModel)(e.ClickedItem);
            _contentFrameVm.NavigateToPage(SelectedItem.PageType);
            NavListView.SelectionMode = ListViewSelectionMode.None;
            NavListView.SelectionMode = ListViewSelectionMode.Single;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = ContentFrame.CanGoBack ?
                    AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void AdaptiveNavigator(object sender, RoutedEventArgs e)
        {
            bool isOpen = mySplitView.IsPaneOpen;
            myMusicText.Visibility = isOpen ? Visibility.Collapsed : Visibility.Visible;
            expandButton.Visibility = isOpen ? Visibility.Visible : Visibility.Collapsed;
            mySplitView.IsPaneOpen = !isOpen;
        }

        private void ExpandPanel(object sender, RoutedEventArgs e)
        {
            bool isOpen = mySplitView.IsPaneOpen;
            myMusicText.Visibility = isOpen ? Visibility.Collapsed : Visibility.Visible;
            expandButton.Visibility = isOpen ? Visibility.Visible : Visibility.Collapsed;
            mySplitView.IsPaneOpen = !isOpen;

        }

    }
}
