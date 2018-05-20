using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MyMusic.Model
{
    public class ContentFrameViewModel : ViewModelBase
    {
        private static ContentFrameViewModel instance;
        public Frame contentFrameRef = null;
        private string _curContentName = null;
        private Dictionary<string, string> _pageNameDictionary = new Dictionary<string, string>();
        private static object _lock = new object();
        private ContentFrameType _frameType = null;

        public ContentFrameType FrameType { get => _frameType; set => _frameType = value; }
        public string CurContentName { get => _curContentName; set => _curContentName = value; }
        public Dictionary<string, string> PageNameDictionary { get => _pageNameDictionary;}

        //单例模式
        private ContentFrameViewModel()
        {
            _frameType = ContentFrameType.GetInstance();
            _curContentName = "发现音乐";
            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            _pageNameDictionary.Add("MyMusic.View.BlankPage", "搜索");
            _pageNameDictionary.Add("MyMusic.View.ExploreMusicPage", "发现音乐");
            _pageNameDictionary.Add("MyMusic.View.BlankPage1", "朋友");
            _pageNameDictionary.Add("MyMusic.View.BlankPage2", "本地音乐");
            _pageNameDictionary.Add("MyMusic.View.BlankPage3", "下载管理");
            _pageNameDictionary.Add("MyMusic.View.BlankPage4", "最近播放");
            _pageNameDictionary.Add("MyMusic.View.BlankPage5", "我的收藏");

            _pageNameDictionary.Add("MyMusic.View.PlayingSongPage", "播放音乐界面");
            _pageNameDictionary.Add("MyMusic.View.MainPage", "主界面");
        }

        internal void setCurContentName(string fullName)
        {
            _curContentName  = PageNameDictionary[fullName];
        }

        public static ContentFrameViewModel GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new ContentFrameViewModel();
                    }
                }
            }
            return instance;
        }

        public void NavigateToPage(Type type)
        {
            if(_curContentName != type.FullName)
            {
                _frameType.Type = type;
                _curContentName = type.FullName;
            }
        }
    }
}
