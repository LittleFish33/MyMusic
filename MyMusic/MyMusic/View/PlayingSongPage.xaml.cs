using MyMusic.Model;
using MyMusic.TemplateControl;
using MyMusic.Util;
using MyMusic.ViewModel;
using Newtonsoft.Json;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MyMusic.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PlayingSongPage : Page
    {

        private PlayingSongViewModel PlayingSongVm = PlayingSongViewModel.GetInstance();
        private PlayingBarControlViewModel PlayingBarVm = PlayingBarControlViewModel.GetInstance();
        private LikeSongsViewModel likeSongsVm = LikeSongsViewModel.GetInstance();
        private DataBaseService dbService = DataBaseService.GetInstance();
        private ShareDataService MyShareDataService = ShareDataService.GetInstance();
        public MediaElement media = null;
        private HttpService httpService = HttpService.GetInstance();
        private JsonSerializer jsonSerializer = JsonSerializer.Create();
        private bool isPause = false;

        private bool isLike = false;

        public PlayingSongPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            PlayingSongVm.setCurSong(PlayingBarVm.CurrentPlayingSong);
            media = PlayingBarVm.media;
            media.CurrentStateChanged += Me_CurrentStateChanged;
            media.SeekCompleted += Me_SeekCompleted;
            isLike = (PlayingBarVm.CurrentPlayingSong.Like == 1);
            heartText.Visibility = isLike ? Visibility.Collapsed : Visibility.Visible;
            heartFillText.Visibility = isLike ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Me_SeekCompleted(object sender, RoutedEventArgs e)
        {
            LyricViewer.moveTo((int)media.Position.TotalMilliseconds);
        }

        private void Me_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            if (media.CurrentState == MediaElementState.Paused)
            {
                LyricViewer.pause();
                isPause = true;
                needle_storyboard.Stop();
                cover_storyboard.Pause();
            }
            else if (media.CurrentState == MediaElementState.Playing)
            {
                LyricViewer.start();
                needle_storyboard.Begin();
                if (isPause)
                {
                    cover_storyboard.Resume();
                }
                else {
                    cover_storyboard.Begin();
                }
            }
            else if (media.CurrentState == MediaElementState.Opening)
            {
                if(PlayingBarVm.CurrentPlayingSong.Lyric.Length > 100)
                {
                    noLyricText.Visibility = Visibility.Collapsed;
                    LyricViewer.Visibility = Visibility.Visible;
                    LyricViewer.init();
                }
                else
                {
                    LyricViewer.Visibility = Visibility.Collapsed;
                    noLyricText.Visibility = Visibility.Visible;
                }
                RenderPage();
            }
        }

        private void RenderPage()
        {
            bgImage.Source = new BitmapImage(new Uri(PlayingBarVm.CurrentPlayingSong.PicUrl));
            alNameText.Text = PlayingBarVm.CurrentPlayingSong.AlName;
            artistText.Text = PlayingBarVm.CurrentPlayingSong.Artist;
            songNameText.Text = PlayingBarVm.CurrentPlayingSong.Name;
            coverImage.ImageSource = new BitmapImage(new Uri(PlayingBarVm.CurrentPlayingSong.PicUrl));
            PlayingSongVm.CurSong.CopyValue(PlayingBarVm.CurrentPlayingSong);
            isLike = (PlayingBarVm.CurrentPlayingSong.Like == 1);
            heartText.Visibility = isLike ? Visibility.Collapsed : Visibility.Visible;
            heartFillText.Visibility = isLike ? Visibility.Visible : Visibility.Collapsed;
        }

        public void initLyric(LyricViewControl instance)
        {
            string lyric = PlayingBarVm.CurrentPlayingSong.Lyric;
            int open, close = 0;
            open = lyric.IndexOf('[');
            while (open < lyric.Length && (close = lyric.IndexOf(']', open)) != -1)
            {
                string timeSpan = lyric.Substring(open + 1, close - open - 1);
                string[] spans = timeSpan.Split(':');
                int min = Convert.ToInt32(spans[0]);
                double sec = Convert.ToDouble(spans[1]);
                open = lyric.IndexOf('[', close);
                if (open == -1)
                    open = lyric.Length;
                string singleLyric = lyric.Substring(close + 1, open - close - 1);
                instance.Add(singleLyric, (int)(min * 60 * 1000 + sec * 1000));
            }
        }

        private void LyricViewer_ParseLyric(object sender, EventArgs e)
        {
            var instance = sender as LyricViewControl;
            initLyric(instance);
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            if(isLike == false)
            {
                if (MyShareDataService.IsLogin)
                {
                    //httpService.update();
                }
                else
                {
                    dbService.CreateLikeSongs(PlayingSongVm.CurSong.Id, PlayingSongVm.CurSong.PicUrl, PlayingSongVm.CurSong.Url, PlayingSongVm.CurSong.Name,
                        PlayingSongVm.CurSong.Artist, PlayingSongVm.CurSong.Duration, PlayingSongVm.CurSong.AlName, PlayingSongVm.CurSong.Lyric);
                    likeSongsVm.LikeSongs.Add(new LikeSongModelForSql(PlayingSongVm.CurSong));
                }
            }
            else
            {
                if (MyShareDataService.IsLogin)
                {
                    //httpService.update();
                }
                else
                {
                    dbService.RemoveLikeSongs(PlayingSongVm.CurSong.Id);
                    for(int i = 0;i < likeSongsVm.LikeSongs.Count; i++)
                    {
                        if(likeSongsVm.LikeSongs[i].Id == PlayingSongVm.CurSong.Id)
                        {
                            likeSongsVm.LikeSongs.RemoveAt(i);
                            break;
                        }
                    }
                }
            }

            isLike = !isLike;
            heartText.Visibility = isLike ? Visibility.Collapsed : Visibility.Visible;
            heartFillText.Visibility = isLike ? Visibility.Visible : Visibility.Collapsed;
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShareButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            Frame RootFrame = Window.Current.Content as Frame;
            RootFrame.Navigate(typeof(CommentPage));
        }
    }
}
