using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using MyMusic.Model;
using MyMusic.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.ViewModel
{
    public class ExploreMusicViewModel : ViewModelBase
    {
        private ObservableCollection<Recommendheader> _headerItems = new ObservableCollection<Recommendheader>();
        private ObservableCollection<RecommendMVModelResult> _recommendMvItems = new ObservableCollection<RecommendMVModelResult>();
        public ObservableCollection<Recommendheader> HeaderItems { get => _headerItems; set => _headerItems = value; }
        internal ObservableCollection<RecommendMVModelResult> RecommendMvItems { get => _recommendMvItems; set => _recommendMvItems = value; }

        private HttpService httpService = HttpService.GetInstance();
        private JsonSerializer jsonSerializer = JsonSerializer.Create();

        public ExploreMusicViewModel()
        {
            InitializeHeaderItems();
            InitializeRecommendMVItems();
        }

        private async void InitializeRecommendMVItems()
        {
            string content = await httpService.GetJsonStringAsync(APIUrlReference.RecommendMVUrl);
            RecommendMVModel model = jsonSerializer.Deserialize<RecommendMVModel>(new JsonTextReader(new StringReader(content)));
            if (model != null)
            {
                foreach (RecommendMVModelResult item in model.result)
                {
                    _recommendMvItems.Add(item);
                }
            }
        }

        private async void InitializeHeaderItems()
        {
            string content = await httpService.GetJsonStringAsync(APIUrlReference.ExploreHeaderUrl);
            ExploreRecommedHeaderModel model = jsonSerializer.Deserialize<ExploreRecommedHeaderModel>(new JsonTextReader(new StringReader(content)));
            if (model != null)
            {
                foreach (Recommendheader item in model.recommendHeader)
                {
                    _headerItems.Add(item);
                }
            }
        }
    }
}
