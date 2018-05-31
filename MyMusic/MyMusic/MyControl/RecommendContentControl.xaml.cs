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
using System.Text;
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


namespace MyMusic.MyControl
{
    public sealed partial class RecommendContentControl : UserControl
    {
        private ObservableCollection<RecommendItemModelResultForDisplay> _recommendItems = new ObservableCollection<RecommendItemModelResultForDisplay>();
        private HttpService httpService = HttpService.GetInstance();
        private JsonSerializer jsonSerializer = JsonSerializer.Create();
        private ContentFrameViewModel ContentFrameVm = ContentFrameViewModel.GetInstance();

        internal ObservableCollection<RecommendItemModelResultForDisplay> RecommendItems { get => _recommendItems; }

        public RecommendContentControl()
        {
            InitializeRecommendItems();
            this.InitializeComponent();
        }


        private async void InitializeRecommendItems()
        {
            string content = await httpService.GetJsonStringAsync(APIUrlReference.RecommendSongSheetUrl);
            RecommendItemModel model = jsonSerializer.Deserialize<RecommendItemModel>(new JsonTextReader(new StringReader(content)));
            if (model != null)
            {
                int count = 0;
                foreach (RecommendItemModelResult item in model.result)
                {
                    if(count++ == 8)
                    {
                        break;
                    }
                    _recommendItems.Add(new RecommendItemModelResultForDisplay(item));
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
    }
}
