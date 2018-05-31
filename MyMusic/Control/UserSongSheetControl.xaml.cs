using MyMusic.Model;
using MyMusic.Util;
using MyMusic.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using Windows.UI.Popups;
using MyMusic.ViewModel;
using System.Diagnostics;
using Windows.System;

namespace MyMusic.Control
{
    public sealed partial class UserSongSheetControl : UserControl
    {
        private ObservableCollection<SongSheetModelResultForDisplay> _songsheetItems = new ObservableCollection<SongSheetModelResultForDisplay>();
        private HttpService httpService = HttpService.GetInstance();
        private JsonSerializer jsonSerializer = JsonSerializer.Create();
        private ContentFrameViewModel ContentFrameVm = ContentFrameViewModel.GetInstance();
        private PersonInfoViewModel user = PersonInfoViewModel.GetInstance();

        internal ObservableCollection<SongSheetModelResultForDisplay> Items { get => _songsheetItems; }



        public UserSongSheetControl()
        {
            InitializeSongsheetItems();
            this.InitializeComponent();
            Debug.WriteLine("刷新");
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
                    if(item.creator.userId == user.Id)
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

        private void MoreSongSheet_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            // 进行页面跳转，做好了之后要进行修改
            ContentFrameVm.NavigateToPage(typeof(BlankPage));
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = ContentFrameVm.contentFrameRef.CanGoBack ?
                AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void CreateSongSheet(object sender, RoutedEventArgs e)
        {
            newSheet.Visibility = 0;

        }

        private async void EnsureCreate(object sender, RoutedEventArgs e)
        {
            try
            {
                Debug.WriteLine(APIUrlReference.CreateNewSheet + SheetName.Text);
                newSheet.Visibility = (Visibility)1;
                string content = await httpService.GetJsonAfterLogIn(APIUrlReference.CreateNewSheet + SheetName.Text);
                NewSongSheetModel model = jsonSerializer.Deserialize<NewSongSheetModel>(new JsonTextReader(new StringReader(content)));
                Debug.WriteLine(content);
                if (model.code == 200)
                {
                    MessageDialog x = new MessageDialog("", "新建成功！");
                    await x.ShowAsync();
                    _songsheetItems.Add(new SongSheetModelResultForDisplay(model));
                }
                else
                {
                    MessageDialog x = new MessageDialog("", "新建失败！");
                    await x.ShowAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void SheetName_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                EnsureCreate(sender, e as RoutedEventArgs);
            }
        }
    }
}
