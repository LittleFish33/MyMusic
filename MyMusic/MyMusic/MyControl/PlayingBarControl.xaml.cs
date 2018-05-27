using MyMusic.Converter;
using MyMusic.Model;
using MyMusic.Util;
using MyMusic.View;
using MyMusic.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace MyMusic.MyControl
{
    public sealed partial class PlayingBarControl : UserControl
    {
        private PlayingBarControlViewModel PlayingBarVm = PlayingBarControlViewModel.GetInstance();
        private HttpService httpService = HttpService.GetInstance();
        private JsonSerializer jsonSerializer = JsonSerializer.Create();
        private DataBaseService dBService = DataBaseService.GetInstance();
        private ContentFrameViewModel ContentFrameVm = ContentFrameViewModel.GetInstance();
        private string State = "";
        private bool isPause = false;

        public PlayingBarControl()
        {
            this.InitializeComponent();
            likeButton.Visibility = (PlayingBarVm.CurrentPlayingSong.Like == 1) ? Visibility.Visible : Visibility.Collapsed;
            disLikeButton.Visibility = (PlayingBarVm.CurrentPlayingSong.Like != 1) ? Visibility.Visible : Visibility.Collapsed;
            this.SizeChanged += (s, e) =>
            {
                if (e.NewSize.Width < 720)
                {
                    State = "NarrowLayout";
                }
                else if (e.NewSize.Width > 720)
                {
                    State = "WideLayout";
                }

                VisualStateManager.GoToState(this, State, true);
            };
            PlayingBarVm.media = PlayingSongMedia;
        }

        private async void SetMediaSource()
        {
            ProgressSlider.IsEnabled = false;
            PlayButton.Visibility = Visibility.Collapsed;
            PauseButton.Visibility = Visibility.Visible;
            if (PlayingBarVm.CurrentPlayingSong.Url == " ")
            {
                string content = await httpService.GetJsonStringAsync(APIUrlReference.GetPlayingSongUrl + PlayingBarVm.CurrentPlayingSong.Id);
                PlayingSongUrlJsonObject model = jsonSerializer.Deserialize<PlayingSongUrlJsonObject>(new JsonTextReader(new StringReader(content)));

                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(new Uri(model.data[0].url));

                IBuffer buffer = await response.Content.ReadAsBufferAsync();

                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFolder folder = null;
                if (localFolder.GetFolderAsync("PlayCache") == null)
                {
                    folder = await localFolder.CreateFolderAsync("PlayCache", CreationCollisionOption.ReplaceExisting);
                }
                else
                {
                    folder = await localFolder.CreateFolderAsync("PlayCache", CreationCollisionOption.OpenIfExists);
                }

                string name = getSongName(model.data[0].url);
                string type = getSongType(name);

                StorageFile file = await folder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
                StorageFile storageFile = await folder.GetFileAsync(name);

                await FileIO.WriteBufferAsync(storageFile, buffer);//从缓冲写入文件

                IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);

                ((PlayingSongForSqlModel)PlayingBarVm.PlayingList[PlayingBarVm.CurIndex]).Url = name;
                PlayingBarVm.CurrentPlayingSong.Url = name;
                dBService.UpdatePlayList(PlayingBarVm.CurrentPlayingSong.Id, name);

                PlayingSongMedia.SetSource(fileStream, file.FileType);
            }
            else
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFolder folder = await localFolder.CreateFolderAsync("PlayCache", CreationCollisionOption.OpenIfExists);
                StorageFile file = await folder.CreateFileAsync(PlayingBarVm.CurrentPlayingSong.Url, CreationCollisionOption.OpenIfExists);

                IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
                PlayingSongMedia.SetSource(fileStream, file.FileType);
            }
            UpdateControl();
        }

        private void UpdateControl()
        {
            coverImage.ImageSource = new BitmapImage(new Uri(PlayingBarVm.CurrentPlayingSong.PicUrl));
            SongNameBlock.Text = PlayingBarVm.CurrentPlayingSong.Name;
            ArtistBlock.Text = PlayingBarVm.CurrentPlayingSong.Artist;
            SongLengthBlock.Text = PlayingBarVm.CurrentPlayingSong.Length;
            ProgressSlider.IsEnabled = true;
            
        }

        private string getSongType(string name)
        {
            StringBuilder builder = new StringBuilder();
            int i;
            for (i = name.Length - 1;name[i] != '.'; i--)
            {
                builder.Insert(0, name[i]);
            }
            builder.Insert(0, name[i]);
            return builder.ToString();
        }

        private string getSongName(string url)
        {
            StringBuilder builder = new StringBuilder();
            for(int i = url.Length - 1;url[i] != '/'; i--)
            {
                builder.Insert(0,url[i]);
            }
            return builder.ToString();
        }

        private void ExpandButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            expandButton.Visibility = Visibility.Visible;
        }

        private void ExpandButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            expandButton.Visibility = Visibility.Collapsed;
        }

        private void ExpandButton_Click(object sender, RoutedEventArgs e)
        {
            Frame RootFrame = Window.Current.Content as Frame;
            RootFrame.Navigate(typeof(PlayingSongPage));
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            PlayButton.Visibility = Visibility.Collapsed;
            PauseButton.Visibility = Visibility.Visible;
            if (isPause)
            {
                PlayingSongMedia.Play();
            }
            else
            {
                SetMediaSource();
            }
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            PauseButton.Visibility = Visibility.Collapsed;
            PlayButton.Visibility = Visibility.Visible;
            PlayingSongMedia.Pause();
            isPause = true;
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            likeButton.Visibility = Visibility.Collapsed;
            disLikeButton.Visibility = Visibility.Visible;
            // 下面的函数暂时为空，因为“我喜欢的音乐”和“登录”页面还没做好
            PlayingBarVm.AddLikeSong();
        }

        private void DisLikeButton_Click(object sender, RoutedEventArgs e)
        {
            likeButton.Visibility = Visibility.Visible;
            disLikeButton.Visibility = Visibility.Collapsed;
            // 下面的函数暂时为空，因为“我喜欢的音乐”和“登录”页面还没做好
            PlayingBarVm.RemoveLikeSong();
        }

        private void RepeatAllButton_Click(object sender, RoutedEventArgs e)
        {
            RepeatAllButton.Visibility = Visibility.Collapsed;
            RepeatOneButton.Visibility = Visibility.Visible;
            PlayingBarVm.Type = PLAYTYPE.RepeatOne;
        }

        private void RepeatOneButton_Click(object sender, RoutedEventArgs e)
        {
            RepeatOneButton.Visibility = Visibility.Collapsed;
            ShuffleButton.Visibility = Visibility.Visible;
            PlayingBarVm.Type = PLAYTYPE.ShufflePlay;
        }

        private void ShuffleButton_Click(object sender, RoutedEventArgs e)
        {
            ShuffleButton.Visibility = Visibility.Collapsed;
            SequentialButton.Visibility = Visibility.Visible;
            PlayingBarVm.Type = PLAYTYPE.SequentiaPlay;
        }

        private void SequentialButton_Click(object sender, RoutedEventArgs e)
        {
            SequentialButton.Visibility = Visibility.Collapsed;
            RepeatAllButton.Visibility = Visibility.Visible;
            PlayingBarVm.Type =  PLAYTYPE.RepeatAll;
        }

        private void PlayingSong_MediaOpened(object sender, RoutedEventArgs e)
        {
            ProgressSlider.Maximum = PlayingSongMedia.NaturalDuration.TimeSpan.TotalSeconds;
            ProgressSlider.Value = 0;
        }

        private void PlayingSong_MediaEnded(object sender, RoutedEventArgs e)
        {
            if(PlayingBarVm.Type == PLAYTYPE.RepeatOne)
            {
                PlayingSongMedia.Play();
            }
            else
            {
                string nextSongId = PlayingBarVm.getNextSongIdAuto();
                if(nextSongId != "Done")
                {
                    SetMediaSource();
                }
            }
            
        }

        private void VolumeButton_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null) FlyoutBase.ShowAttachedFlyout(element);
        }

        private void HiddenPlayListButton_Click(object sender, RoutedEventArgs e)
        {
            RenderPlayList();
            FrameworkElement element = sender as FrameworkElement;
            if (element != null) FlyoutBase.ShowAttachedFlyout(element);
        }

        private void RenderPlayList()
        {
            List<StackPanel> list = new List<StackPanel>();
            FindChildren<StackPanel>(list, HiddenPlayList);
            for(int i = 0;i < list.Count; i++)
            {
                StackPanel panel = list[i];
                if(i == PlayingBarVm.CurIndex)
                {
                    ((TextBlock)panel.Children[0]).Foreground = new SolidColorBrush(Color.FromArgb(255, 223, 59, 59));
                    ((TextBlock)panel.Children[1]).Foreground = new SolidColorBrush(Color.FromArgb(255, 223, 59, 59));
                    ((TextBlock)panel.Children[2]).Foreground = new SolidColorBrush(Color.FromArgb(255, 223, 59, 59));
                    ((TextBlock)panel.Children[3]).Foreground = new SolidColorBrush(Color.FromArgb(255, 223, 59, 59));
                }
                else
                {
                    ((TextBlock)panel.Children[0]).Foreground = new SolidColorBrush(Color.FromArgb(0, 255, 0, 0));
                    ((TextBlock)panel.Children[1]).Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                    ((TextBlock)panel.Children[2]).Foreground = new SolidColorBrush(Color.FromArgb(255, 102, 102, 102));
                    ((TextBlock)panel.Children[3]).Foreground = new SolidColorBrush(Color.FromArgb(255, 102, 102, 102));
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

        private void ClearPlayListButton_Click(object sender, RoutedEventArgs e)
        {
            PlayingBarVm.PlayingList.Clear();
            PlayingBarVm.ListCount.DisPlayCount = ConverteLongToTimeStr.Expand(0, ' ');
            // 下一步是同步数据库，但是，等建好播放页面再说吧
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (PlayingBarVm.Type == PLAYTYPE.RepeatOne)
            {
                SetMediaSource();
            }
            else if(PlayingBarVm.Type == PLAYTYPE.ShufflePlay)
            {
                PlayingBarVm.getNextSongIdAuto();
                SetMediaSource();
            }
            else
            {
                PlayingBarVm.getPreviousSongIdAuto();
                SetMediaSource();
            }
            PlayButton.Visibility = Visibility.Collapsed;
            PauseButton.Visibility = Visibility.Visible;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            string nextSongId = PlayingBarVm.getNextSongIdAuto();
            if (nextSongId != "Done")
            {
                SetMediaSource();
            }
            else
            {
                PlayingSongMedia.Play();
            }
            PlayButton.Visibility = Visibility.Collapsed;
            PauseButton.Visibility = Visibility.Visible;
        }

        private void HiddenPlayList_ItemClick(object sender, ItemClickEventArgs e)
        {
            PlayingSongForSqlModel model = e.ClickedItem as PlayingSongForSqlModel;
            for (int i = 0; i < PlayingBarVm.PlayingList.Count; i++){
                if(model.Id == PlayingBarVm.PlayingList[i].Id)
                {
                    PlayingBarVm.CurIndex = i;
                    PlayingBarVm.CurrentPlayingSong = PlayingBarVm.PlayingList[i];
                }
            }
            SetMediaSource();
            RenderPlayList();
        }
    }
}
