using GalaSoft.MvvmLight;
using MyMusic.Model;
using MyMusic.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.ViewModel
{
    class SearchViewModel : ViewModelBase
    {
        private static SearchViewModel instance;
        private static object _lock = new object();
        private HttpService httpService = HttpService.GetInstance();
        private JsonSerializer jsonSerializer = JsonSerializer.Create();
        private DataBaseService dbService = DataBaseService.GetInstance();

        private ObservableCollection<SearchSongsForDisplay> _searchSongsResult = new ObservableCollection<SearchSongsForDisplay>();
        private ObservableCollection<SearchSingerForDisplay> _searchSingerReuslt = new ObservableCollection<SearchSingerForDisplay>();
        private ObservableCollection<SearchMvForDisplay> _searchMvResult = new ObservableCollection<SearchMvForDisplay>();
        private ObservableCollection<SearchSheetForDisplay> _searchSheetResult = new ObservableCollection<SearchSheetForDisplay>();
        private ObservableCollection<String> _hotSearch = new ObservableCollection<String>();
        private ObservableCollection<String> _searchHistory = new ObservableCollection<String>();


        internal ObservableCollection<SearchSongsForDisplay> SearchSongsResult { get => _searchSongsResult; set => _searchSongsResult = value; }
        public ObservableCollection<String> HotSearch { get => _hotSearch; set => _hotSearch = value; }
        public ObservableCollection<string> SearchHistory { get => _searchHistory; set => _searchHistory = value; }
        internal ObservableCollection<SearchSingerForDisplay> SearchSingerReuslt { get => _searchSingerReuslt; set => _searchSingerReuslt = value; }
        internal ObservableCollection<SearchMvForDisplay> SearchMvResult { get => _searchMvResult; set => _searchMvResult = value; }
        internal ObservableCollection<SearchSheetForDisplay> SearchSheetResult { get => _searchSheetResult; set => _searchSheetResult = value; }

        //单例模式
        private SearchViewModel()
        {
            InitializeHotSearch();
            InitializeSearchHistory();
        }

        public static SearchViewModel GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new SearchViewModel();
                    }
                }
            }
            return instance;
        }


        private async void InitializeHotSearch()
        {
            string content = await httpService.GetJsonStringAsync(APIUrlReference.HotSearchUrl);
            HotJsonObjectModel model = jsonSerializer.Deserialize<HotJsonObjectModel>(new JsonTextReader(new StringReader(content)));
            int maxLength = 0;
            string max = "";
            foreach (var item in model.result.hots)
            {
                if(maxLength < item.first.Length)
                {
                    maxLength = item.first.Length;
                    max = item.first;
                } 
            }
            _hotSearch.Add(max);
            foreach (var item in model.result.hots)
            {
                if (max != item.first)
                {
                    _hotSearch.Add(item.first);
                }
            }
        }

        private void InitializeSearchHistory()
        {
            dbService.GetAllSearchHistory(_searchHistory);
        }


    }
}
