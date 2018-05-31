using MyMusic.Model;
using MyMusic.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MyMusic.ViewModel;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MyMusic.Control
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CollectedControl : UserControl
    {
        private DataBaseService dBService = DataBaseService.GetInstance();
        private HttpService httpService = HttpService.GetInstance();
        private PersonInfoViewModel user = PersonInfoViewModel.GetInstance();
        private JsonSerializer jsonSerializer = JsonSerializer.Create();

        private ObservableCollection<CommentForSqlModel> _commentItems = new ObservableCollection<CommentForSqlModel>();
        private ObservableCollection<CollectedSongerModelResultForDisplay> _songerItems = new ObservableCollection<CollectedSongerModelResultForDisplay>();
        private ObservableCollection<SongSheetModelResultForDisplay> _songsheetItems = new ObservableCollection<SongSheetModelResultForDisplay>();

        internal ObservableCollection<CommentForSqlModel> CommentItems { get => _commentItems; }
        internal ObservableCollection<CollectedSongerModelResultForDisplay> SongerItems { get => _songerItems; }
        internal ObservableCollection<SongSheetModelResultForDisplay> SongSheetItems { get => _songsheetItems; }

        public CollectedControl()
        {
            this.InitializeComponent();
            InitializeCommentItems();
            InitializeSongertems();
            InitializeSongsheetItems();
        }

        private void InitializeCommentItems()
        {
            if(_commentItems.Count != 0)
            {
                _commentItems.Clear();
            }
            dBService.GetAllCommentListItem(_commentItems, "32953014");
        }

        private async void InitializeSongertems()
        {
            //dBService.GetAllSongerListItem(_songerItems, "320791392");
            try
            {
                string content = await httpService.GetJsonAfterLogIn(APIUrlReference.CollectedSongerUrl);
                CollectedSongerModel model = jsonSerializer.Deserialize<CollectedSongerModel>(new JsonTextReader(new StringReader(content)));
                if (model != null)
                {
                    int count = 0;
                    foreach (CollectedSongerModelResult item in model.data)
                    {
                        if (count++ == model.data.Length)
                        {
                            break;
                        }
                        _songerItems.Add(new CollectedSongerModelResultForDisplay(item));
                    }
                }
                dBService.SetSongerList(_songerItems, user.Id.ToString());
            }
            catch(Exception e)
            {
                //重新登陆
                _songerItems.Clear();
                Debug.WriteLine(user.Id.ToString());
                dBService.GetAllSongerListItem(_songerItems, user.Id.ToString());
            }
        }

        private async void InitializeSongsheetItems()
        {
            string content = await httpService.GetJsonStringAsync(APIUrlReference.UserSongSheetUrl + user.Id);
            SongSheetItemModel model = jsonSerializer.Deserialize<SongSheetItemModel>(new JsonTextReader(new StringReader(content)));
            if (model != null)
            {
                int count = 0;
                foreach (SongSheetModelResult item in model.playlist)
                {
                    if (item.creator.userId != user.Id)
                    {
                        if (count++ == model.playlist.Length)
                        {
                            break;
                        }
                        _songsheetItems.Add(new SongSheetModelResultForDisplay(item));
                    }
                }
            }
        }

    }
}
