using MyMusic.Model;
using MyMusic.Util;
using MyMusic.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public sealed partial class SearchPage : Page
    {
        private SearchViewModel searchVm = SearchViewModel.GetInstance();
        private DataBaseService dbService = DataBaseService.GetInstance();
        //0: 歌曲 1：歌手 2：mv 3：歌单
        private int searchType = 0;
        private bool firstSearch = true;

        private HttpService httpService = HttpService.GetInstance();
        private JsonSerializer jsonSerializer = JsonSerializer.Create();
        private PlayingBarControlViewModel playingVm = PlayingBarControlViewModel.GetInstance();



        public SearchPage()
        {
            this.InitializeComponent();
            searchResultPivot.SelectionChanged += ChangeBar;
        }

        private void ChangeBar(object sender, SelectionChangedEventArgs e)
        {
            if (searchResultPivot.SelectedIndex == 0)
            {
                border1.BorderThickness = new Thickness(0, 0, 0, 2);
                border2.BorderThickness = new Thickness(0, 0, 0, 0);
                border3.BorderThickness = new Thickness(0, 0, 0, 0);
            }
            else if (searchResultPivot.SelectedIndex == 1)
            {
                border2.BorderThickness = new Thickness(0, 0, 0, 2);
                border1.BorderThickness = new Thickness(0, 0, 0, 0);
                border3.BorderThickness = new Thickness(0, 0, 0, 0);
            }
            else
            {
                border3.BorderThickness = new Thickness(0, 0, 0, 2);
                border1.BorderThickness = new Thickness(0, 0, 0, 0);
                border2.BorderThickness = new Thickness(0, 0, 0, 0);
            }
        }

        private async void searchSongs(string selectedItem)
        {
            InputSearchBox.Text = selectedItem;
            if (firstSearch)
            {
                beforeSearchGrid.Visibility = Visibility.Collapsed;
                Underline.Visibility = Visibility.Visible;
                searchResultPivot.Visibility = Visibility.Visible;
                firstSearch = !firstSearch;
            }
            dbService.CreateHistory(selectedItem);

            if (!searchVm.SearchHistory.Contains(selectedItem))
            {
                searchVm.SearchHistory.Add(selectedItem);
            }

            if (searchResultPivot.SelectedIndex == 0 && searchType == 0)
            {
                string content = await httpService.GetJsonStringAsync(APIUrlReference.SearchSongsUrl + selectedItem);
                SearchSongsJsonObject model = jsonSerializer.Deserialize<SearchSongsJsonObject>(new JsonTextReader(new StringReader(content)));
                searchVm.SearchSongsResult.Clear();
                foreach (var item in model.result.songs)
                {
                    searchVm.SearchSongsResult.Add(new SearchSongsForDisplay(item));
                }
            }
            else if(searchResultPivot.SelectedIndex == 0 && searchType == 1)
            {
                string content = await httpService.GetJsonStringAsync(APIUrlReference.SearchSingerUrl + selectedItem);
                SearchSingerJsonModel model = jsonSerializer.Deserialize<SearchSingerJsonModel>(new JsonTextReader(new StringReader(content)));
                searchVm.SearchSingerReuslt.Clear();
                foreach (var item in model.result.artists)
                {
                    searchVm.SearchSingerReuslt.Add(new SearchSingerForDisplay(item));
                }
            }
            else if (searchResultPivot.SelectedIndex == 0 && searchType == 2)
            {
                showMvResult();
            }
        }


        private void searchSongs(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            searchSongs(InputSearchBox.Text);
        }

        private void SetSearchSongBtn(object sender, RoutedEventArgs e)
        {
            if(searchType == 1)
            {
                singerBtn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
                singerBtn.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            }
            else if(searchType == 2)
            {
                mvBtn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
                mvBtn.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            }
            else if(searchType == 3)
            {
                sheetBtn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
                sheetBtn.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            }
            else
            {
                return;
            }
            searchType = 0;
            songBtn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 223, 59, 59));
            songBtn.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
            showSongResult();
        }


        private void SetSearchSingerBtn(object sender, RoutedEventArgs e)
        {
            if (searchType == 0)
            {
                songBtn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
                songBtn.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            }
            else if (searchType == 2)
            {
                mvBtn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
                mvBtn.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            }
            else if (searchType == 3)
            {
                sheetBtn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
                sheetBtn.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            }
            else
            {
                return;
            }
            searchType = 1;
            singerBtn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 223, 59, 59));
            singerBtn.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
            showSingerResult();
        }

        private void SetSearchMvBtn(object sender, RoutedEventArgs e)
        {
            if (searchType == 0)
            {
                songBtn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
                songBtn.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            }
            else if (searchType == 1)
            {
                singerBtn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
                singerBtn.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            }
            else if (searchType == 3)
            {
                sheetBtn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
                sheetBtn.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            }
            else
            {
                return;
            }
            searchType = 2;
            mvBtn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 223, 59, 59));
            mvBtn.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
            showMvResult();
        }

        private void SetSearchSheetBtn(object sender, RoutedEventArgs e)
        {
            if (searchType == 0)
            {
                songBtn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
                songBtn.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            }
            else if (searchType == 1)
            {
                singerBtn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
                singerBtn.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            }
            else if (searchType == 2)
            {
                mvBtn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
                mvBtn.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
            }
            else
            {
                return;
            }
            searchType = 3;
            sheetBtn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 223, 59, 59));
            sheetBtn.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
            showSheetResult();
        }

        private void RemoveHistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Grid grid = btn.Parent as Grid;
            TextBlock textBlock =  grid.Children[0] as TextBlock;
            foreach (var item in searchVm.SearchHistory)
            {
                if (item == textBlock.Text)
                {
                    searchVm.SearchHistory.Remove(item);
                    return;
                }
            }
            dbService.RemoveHistory(textBlock.Text);
        }

        private void Search_ItemClick(object sender, ItemClickEventArgs e)
        {
            String SelectedItem = (String)(e.ClickedItem);
            searchSongs(SelectedItem);
        }

        private void Search_BtnClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            searchSongs(btn.Content.ToString());
        }

        private void HiddenPlayList_ItemClick(object sender, ItemClickEventArgs e)
        {
            SearchSongsForDisplay select_Item = e.ClickedItem as SearchSongsForDisplay;
            String[] arr = new String[4];
            arr[0] = select_Item.Name;
            arr[1] = select_Item.Artist;
            arr[2] = select_Item.AlName;
            arr[3] = select_Item.Length;
            bool flag = true;
            List<Grid> list = new List<Grid>();
            FindChildren<Grid>(list, HiddenPlayList);
            for (int i = 0; i < list.Count; i++)
            {
                if(list[i].Children.Count != 6)
                {
                    continue;
                }
                flag = true;
                for (int j = 0; j < 4; j++)
                {
                    TextBlock textBlock = list[i].Children[j] as TextBlock;
                    if (textBlock != null)
                    {
                        if (arr[j] != textBlock.Text)
                        {
                            flag = false;
                        }
                    }
                }
                Grid gridBtn1 = list[i].Children[4] as Grid;
                Grid gridBtn2 = list[i].Children[5] as Grid;
                if(gridBtn1 != null && gridBtn1 != null)
                {
                    if (flag)
                    {
                        Button btn1 = gridBtn1.Children[0] as Button;
                        Button btn2 = gridBtn2.Children[0] as Button;
                        btn1.IsEnabled = true;
                        btn2.IsEnabled = true;
                        gridBtn1.Opacity = 1;
                        gridBtn2.Opacity = 1;
                        
                    }
                    else
                    {
                        Button btn1 = gridBtn1.Children[0] as Button;
                        Button btn2 = gridBtn2.Children[0] as Button;
                        btn1.IsEnabled = false;
                        btn2.IsEnabled = false;
                        gridBtn1.Opacity = 0;
                        gridBtn2.Opacity = 0;
                    }
                }
            }
        }

        //遍历startNode的子节点，找到类型为T的控件并且放在results中
        internal static void FindChildren<T>(List<T> results, DependencyObject startNode)
             where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(startNode);
            for (int i = 0; i < count; i++)
            {
                DependencyObject current = VisualTreeHelper.GetChild(startNode, i);
                if ((current.GetType()).Equals(typeof(T)) || (current.GetType().GetTypeInfo().IsSubclassOf(typeof(T))))
                {
                    T asType = (T)current;
                    results.Add(asType);
                }
                FindChildren<T>(results, current);
            }
        }

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            
            SearchSongsForDisplay selectItem = HiddenPlayList.SelectedItem as SearchSongsForDisplay;
            playingVm.addToPlayingList(selectItem);
        }

        private void MoreBtn_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null) FlyoutBase.ShowAttachedFlyout(element);
        }

        private void HiddenMenuList_ItemClick(object sender, ItemClickEventArgs e)
        {
            TextBlock block = e.ClickedItem as TextBlock;
            if(block.Text == "下载")
            {

            }
            else if(block.Text == "评论")
            {

            }
            else if(block.Text == "喜欢")
            {

            }
            else if(block.Text == "分享")
            {

            }
        }


        private async void showSingerResult()
        {
            string content = await httpService.GetJsonStringAsync(APIUrlReference.SearchSingerUrl + InputSearchBox.Text);
            SearchSingerJsonModel model = jsonSerializer.Deserialize<SearchSingerJsonModel>(new JsonTextReader(new StringReader(content)));
            searchVm.SearchSingerReuslt.Clear();
            foreach (var item in model.result.artists)
            {
                searchVm.SearchSingerReuslt.Add(new SearchSingerForDisplay(item));
            }
            HiddenSingerList.Visibility = Visibility.Visible;
            HiddenPlayList.Visibility = Visibility.Collapsed;
            HiddenMvView.Visibility = Visibility.Collapsed;
            HiddenSheetList.Visibility = Visibility.Collapsed;
        }


        private async void showSongResult()
        {
            string content = await httpService.GetJsonStringAsync(APIUrlReference.SearchSongsUrl + InputSearchBox.Text);
            SearchSongsJsonObject model = jsonSerializer.Deserialize<SearchSongsJsonObject>(new JsonTextReader(new StringReader(content)));
            searchVm.SearchSongsResult.Clear();
            foreach (var item in model.result.songs)
            {
                searchVm.SearchSongsResult.Add(new SearchSongsForDisplay(item));
            }
            HiddenSingerList.Visibility = Visibility.Collapsed;
            HiddenPlayList.Visibility = Visibility.Visible;
            HiddenMvView.Visibility = Visibility.Collapsed;
            HiddenSheetList.Visibility = Visibility.Collapsed;
        }



        private async void showMvResult()
        {
            string content = await httpService.GetJsonStringAsync(APIUrlReference.SearchMVUrl + InputSearchBox.Text);
            SearchMvJsonModel model = jsonSerializer.Deserialize<SearchMvJsonModel>(new JsonTextReader(new StringReader(content)));
            searchVm.SearchMvResult.Clear();
            foreach (var item in model.result.mvs)
            {
                searchVm.SearchMvResult.Add(new SearchMvForDisplay(item));
            }
            HiddenSingerList.Visibility = Visibility.Collapsed;
            HiddenPlayList.Visibility = Visibility.Collapsed;
            HiddenMvView.Visibility = Visibility.Visible;
            HiddenSheetList.Visibility = Visibility.Collapsed;
        }


        private async void showSheetResult()
        {
            string content = await httpService.GetJsonStringAsync(APIUrlReference.SearchSheetUrl + InputSearchBox.Text);
            SearchSheetJsonModel model = jsonSerializer.Deserialize<SearchSheetJsonModel>(new JsonTextReader(new StringReader(content)));
            searchVm.SearchSheetResult.Clear();
            foreach (var item in model.result.playlists)
            {
                searchVm.SearchSheetResult.Add(new SearchSheetForDisplay(item));
            }
            HiddenSingerList.Visibility = Visibility.Collapsed;
            HiddenPlayList.Visibility = Visibility.Collapsed;
            HiddenMvView.Visibility = Visibility.Collapsed;
            HiddenSheetList.Visibility = Visibility.Visible;
        }

        private void HiddenMvView_ItemClick(object sender, PointerRoutedEventArgs e)
        {

        }

        private void HiddenSingerList_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void HiddenSheetList_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
