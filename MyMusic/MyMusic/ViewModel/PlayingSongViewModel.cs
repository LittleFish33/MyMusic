using GalaSoft.MvvmLight;
using MyMusic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.ViewModel
{
    public class PlayingSongViewModel : ViewModelBase
    {
        private static object _lock = new object();
        private static PlayingSongViewModel instance;

        private PlayingSongForSqlModel _curSong = new PlayingSongForSqlModel();

        private PlayingSongViewModel()
        {
        }

        public PlayingSongForSqlModel CurSong { get => _curSong; set => _curSong = value; }


        public static PlayingSongViewModel GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new PlayingSongViewModel();
                    }
                }
            }
            return instance;
        }

        internal void setCurSong(PlayingSongForSqlModel currentPlayingSong)
        {
            _curSong.CopyValue(currentPlayingSong);
        }
    }
}
