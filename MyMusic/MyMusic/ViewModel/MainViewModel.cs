using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using MyMusic.Model;
using MyMusic.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private readonly INavigationService _navigationService;

        private ObservableCollection<LeftNavListItemModel> _leftNavListSource = new ObservableCollection<LeftNavListItemModel>();
        private ObservableCollection<LeftNavListItemModel> _leftNavMyMusicListSource = new ObservableCollection<LeftNavListItemModel>();
        private Dictionary<string, int> _navListDictionary = new Dictionary<string, int>();
        private Dictionary<string, int> _navMyMusicListDictionary = new Dictionary<string, int>();

        public ObservableCollection<LeftNavListItemModel> LeftNavListSource{ get=>_leftNavListSource;}
        public ObservableCollection<LeftNavListItemModel> LeftNavMyMusicListSource { get => _leftNavMyMusicListSource; }
        public Dictionary<string, int> NavListDictionary { get => _navListDictionary; }
        public Dictionary<string, int> NavMyMusicListDictionary { get => _navMyMusicListDictionary; }

        public MainViewModel(
            INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitializeLeftNavList();
            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            _navListDictionary.Add("搜索", 0);
            _navListDictionary.Add("发现音乐", 1);
            _navListDictionary.Add("朋友", 2);

            _navMyMusicListDictionary.Add("本地音乐", 0);
            _navMyMusicListDictionary.Add("下载管理", 1);
            _navMyMusicListDictionary.Add("最近播放", 2);
            _navMyMusicListDictionary.Add("我喜欢的音乐", 3);

        }

        private void InitializeLeftNavList()
        {
            this._leftNavListSource = new ObservableCollection<LeftNavListItemModel>()
            {
                new LeftNavListItemModel(Windows.UI.Xaml.Controls.Symbol.Find,"搜索",typeof(SearchPage)),
                new LeftNavListItemModel(Windows.UI.Xaml.Controls.Symbol.Audio,"发现音乐",typeof(ExploreMusicPage)),
                new LeftNavListItemModel(Windows.UI.Xaml.Controls.Symbol.People,"朋友",typeof(BlankPage)),
            };

            this._leftNavMyMusicListSource = new ObservableCollection<LeftNavListItemModel>()
            {
                new LeftNavListItemModel(Windows.UI.Xaml.Controls.Symbol.OpenLocal,"本地音乐",typeof(BlankPage)),
                new LeftNavListItemModel(Windows.UI.Xaml.Controls.Symbol.Download,"下载管理",typeof(BlankPage)),
                new LeftNavListItemModel(Windows.UI.Xaml.Controls.Symbol.Clock,"最近播放",typeof(BlankPage)),
                new LeftNavListItemModel(Windows.UI.Xaml.Controls.Symbol.Like,"我喜欢的音乐",typeof(LikeSongsPage)),
            };
        }
    }
}
