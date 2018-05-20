using GalaSoft.MvvmLight;
using MyMusic.Converter;
using MyMusic.Model;
using MyMusic.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.ViewModel
{
    public enum PLAYTYPE { RepeatAll, RepeatOne, ShufflePlay, SequentiaPlay }

    public class PlayingBarControlViewModel: ViewModelBase
    {
        private static PlayingBarControlViewModel instance;
        private static object _lock = new object();
        private DataBaseService dBService = DataBaseService.GetInstance();
        private ObservableCollection<PlayingSongForSqlModel> _playingList = new ObservableCollection<PlayingSongForSqlModel>();


        private PlayingSongForSqlModel _currentPlayingSong = new PlayingSongForSqlModel();
        private PLAYTYPE _type = PLAYTYPE.RepeatAll;
        private PlayingListModel _listCount = new PlayingListModel();
        private int curIndex = 0;

        public PlayingSongForSqlModel CurrentPlayingSong { get => _currentPlayingSong; set => _currentPlayingSong = value; }
        public PLAYTYPE Type { get => _type; set => _type = value; }
        public ObservableCollection<PlayingSongForSqlModel> PlayingList { get => _playingList; set => _playingList = value; }
        public int CurIndex { get => curIndex; set => curIndex = value; }
        internal PlayingListModel ListCount { get => _listCount; set => _listCount = value; }


        //单例模式
        private PlayingBarControlViewModel()
        {
            InitializePlayingList();
        }

        public static PlayingBarControlViewModel GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new PlayingBarControlViewModel();
                    }
                }
            }
            return instance;
        }

        private void InitializePlayingList()
        {
            dBService.GetAllPlayListItem(_playingList);
            foreach (var item in _playingList)
            {
                PlayingSongForSqlModel model = item as PlayingSongForSqlModel;
                if(model.Playing == 1)
                {
                    _currentPlayingSong.CopyValue(model);
                    break;
                }
                curIndex++;
            }
            _listCount.DisPlayCount = ConverteLongToTimeStr.Expand(_playingList.Count, ' ');
        }

        internal void RemoveLikeSong()
        {
            
        }

        internal string getNextSongIdAuto()
        {
            if(_type == PLAYTYPE.RepeatAll)
            {
                curIndex += 1;
                curIndex %= _playingList.Count;
                CurrentPlayingSong = _playingList[curIndex] as PlayingSongForSqlModel;
            }
            else if(_type == PLAYTYPE.RepeatOne)
            {
                return "";
            }
            else if(_type == PLAYTYPE.ShufflePlay)
            {
                Random rd = new Random();
                int newIndex = rd.Next(1, _playingList.Count);
                while(newIndex == curIndex)
                {
                    newIndex = rd.Next(1, _playingList.Count);
                }
                curIndex = newIndex;
                CurrentPlayingSong = _playingList[curIndex] as PlayingSongForSqlModel;
            }
            else
            {
                curIndex += 1;
                if(curIndex == _playingList.Count)
                {
                    return "Done";
                }
                curIndex %= _playingList.Count;
                CurrentPlayingSong = _playingList[curIndex] as PlayingSongForSqlModel;
            }
            return CurrentPlayingSong.Id;
        }

        internal void AddLikeSong()
        {
            
        }

        internal void getPreviousSongIdAuto()
        {
            if (curIndex > 0)
            {
                curIndex -= 1;
                CurrentPlayingSong = _playingList[curIndex] as PlayingSongForSqlModel;
            }
        }
    }
}
