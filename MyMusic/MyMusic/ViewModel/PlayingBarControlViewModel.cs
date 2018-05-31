using GalaSoft.MvvmLight;
using MyMusic.Converter;
using MyMusic.Model;
using MyMusic.Util;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MyMusic.ViewModel
{
    public enum PLAYTYPE { RepeatAll, RepeatOne, ShufflePlay, SequentiaPlay }

    public class PlayingBarControlViewModel: ViewModelBase
    {
        private static PlayingBarControlViewModel instance;
        private static object _lock = new object();
        private DataBaseService dBService = DataBaseService.GetInstance();
        private HttpService httpService = HttpService.GetInstance();
        private JsonSerializer jsonSerializer = JsonSerializer.Create();
        private ObservableCollection<PlayingSongForSqlModel> _playingList = new ObservableCollection<PlayingSongForSqlModel>();
        public MediaElement media = null;

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
            updateModel();
            _listCount.DisPlayCount = ConverteLongToTimeStr.Expand(_playingList.Count, ' ');
        }

        internal void RemoveLikeSong()
        {
            
        }

        internal string getNextSongIdAuto()
        {
            if (_playingList.Count > 0)
            {
                if (_type == PLAYTYPE.RepeatAll)
                {
                    curIndex += 1;
                    curIndex %= _playingList.Count;
                    CurrentPlayingSong = _playingList[curIndex] as PlayingSongForSqlModel;
                }
                else if (_type == PLAYTYPE.RepeatOne)
                {
                    return "";
                }
                else if (_type == PLAYTYPE.ShufflePlay)
                {
                    Random rd = new Random();
                    int newIndex = rd.Next(1, _playingList.Count);
                    while (newIndex == curIndex)
                    {
                        newIndex = rd.Next(1, _playingList.Count);
                    }
                    curIndex = newIndex;
                    CurrentPlayingSong = _playingList[curIndex] as PlayingSongForSqlModel;
                }
                else
                {
                    curIndex += 1;
                    if (curIndex == _playingList.Count)
                    {
                        return "Done";
                    }
                    curIndex %= _playingList.Count;
                    CurrentPlayingSong = _playingList[curIndex] as PlayingSongForSqlModel;
                }
            }
            updateModel();
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
                updateModel();
            }
        }

        private void updateModel()
        {
            
        }

        public async void addToPlayingList(SearchSongsForDisplay selectItem)
        {
            foreach (var item in _playingList)
            {
                if(item.Id == selectItem.Id)
                {
                    return;
                }
            }
            PlayingSongForSqlModel model = new PlayingSongForSqlModel();
            model.Id = selectItem.Id;
            model.Name = selectItem.Name;
            model.Artist = selectItem.Artist;
            model.AlName = selectItem.AlName;
            model.Duration = selectItem.Duration;
            model.Length = selectItem.Length;
            string content = await httpService.GetJsonStringAsync(APIUrlReference.SongDetailUrl + selectItem.Id);
            SongDetailJsonModel jsonModel = jsonSerializer.Deserialize<SongDetailJsonModel>(new JsonTextReader(new StringReader(content)));
            model.PicUrl = jsonModel.songs[0].al.picUrl;
            model.Url = " ";
            model.Like = 0;
            model.Playing = 1;
            content = await httpService.GetJsonStringAsync(APIUrlReference.GetLyricUrl + selectItem.Id);
            LyricModel lyricModel = jsonSerializer.Deserialize<LyricModel>(new JsonTextReader(new StringReader(content)));
            StringBuilder validLyric = new StringBuilder();
            bool flag = false;
            for (int i = 0; i < lyricModel.lrc.lyric.Length-1; i++)
            {
                if (flag)
                {
                    validLyric.Append(lyricModel.lrc.lyric[i]);
                }
                else
                {
                    if(lyricModel.lrc.lyric[i] == '[' && lyricModel.lrc.lyric[i+1] == '0')
                    {
                        flag = true;
                        validLyric.Append(lyricModel.lrc.lyric[i]);
                    }
                }
            }
            if (validLyric.ToString() != "")
            {
                validLyric.Append(lyricModel.lrc.lyric[lyricModel.lrc.lyric.Length - 1]);
            }
            else
            {
                validLyric.Append(" ");
            }
            model.Lyric = validLyric.ToString();
            curIndex = _playingList.Count;
            _playingList.Add(model);
            ListCount.DisPlayCount = ConverteLongToTimeStr.Expand(_playingList.Count, ' ');
            dBService.CreatePlayListItem(model);
        }
    }
}
