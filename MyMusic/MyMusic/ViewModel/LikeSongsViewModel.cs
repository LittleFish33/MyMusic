using GalaSoft.MvvmLight;
using MyMusic.Model;
using MyMusic.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.ViewModel
{
    public class LikeSongsViewModel : ViewModelBase
    {
        private static LikeSongsViewModel instance;
        private static object _lock = new object();
        private ObservableCollection<LikeSongModelForSql> _likeSongs = new ObservableCollection<LikeSongModelForSql>();
        private ShareDataService shareDataService = ShareDataService.GetInstance();
        private DataBaseService dbService = DataBaseService.GetInstance();

        public ObservableCollection<LikeSongModelForSql> LikeSongs { get => _likeSongs; set => _likeSongs = value; }

        private LikeSongsViewModel()
        {
            InitializeLikeSongs();
        }

        private void InitializeLikeSongs()
        {
            if (shareDataService.IsLogin)
            {
                // 等你们登录搞好吧
            }
            else
            {
                // 未登录情况下，喜欢的音乐以本地数据库的方式保存
                dbService.GetAllLikeSongsItem(_likeSongs);
            }
        }

        public static LikeSongsViewModel GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new LikeSongsViewModel();
                    }
                }
            }
            return instance;
        }



    }
}
