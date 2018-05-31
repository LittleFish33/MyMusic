using MyMusic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MyMusic.ViewModel
{

    public class PersonalTrendViewModel 
    {

        PersonalTrendModel instance = PersonalTrendModel.GetInstance();
        private ObservableCollection<PersonalTrendEventModel> allItems = new ObservableCollection<PersonalTrendEventModel>();  // 监听所有的Event
        public ObservableCollection<PersonalTrendEventModel> AllItems { get { return this.allItems; } }
        static PersonInfoViewModel user = PersonInfoViewModel.GetInstance();

        private static PersonalTrendViewModel Instance = null;
        private static object _lock = new object();
        public static PersonalTrendViewModel GetInstance()
        {
            if (Instance == null)
            {
                lock (_lock)
                {
                    if (Instance == null)
                    {
                        if(user.Id != 0)
                            Instance = new PersonalTrendViewModel();
                    }
                }
            }
            return Instance;
        }

        public PersonalTrendViewModel()
        {
                init();
        }

        private void init()
        {
            int num = instance.size;
            string msg;
            string songorplaylistname;
            string picurl;
            string _avatarUrl;
            string artistname;
            string nickname;
            int commentcount;
            int likedcount;
            int sharecount;
            string mp3url;
            string musicorplaylist;
            for (int i = 0; i < num; i++)
            {
                if (instance.events[i].json.song != null)  //歌曲
                {
                    Debug.WriteLine("一个歌曲动态");
                    msg = instance.events[i].json.msg;
                    songorplaylistname = instance.events[i].json.song.name;
                    picurl = instance.events[i].json.song.album.blurPicUrl;
                    artistname = instance.events[i].json.song.artists[0].name;

                    _avatarUrl = instance.events[i].user.avatarUrl;
                    nickname = instance.events[i].user.nickname;
                    commentcount = instance.events[i].info.commentCount;
                    likedcount = instance.events[i].info.likedCount;
                    sharecount = instance.events[i].info.shareCount;
                    musicorplaylist = "分享音乐";
                    Debug.WriteLine(likedcount);
                    mp3url = "";//instance.events[i].json.song.mp3Url.ToString();
                    allItems.Add(new PersonalTrendEventModel(msg, songorplaylistname, picurl, artistname, _avatarUrl, nickname, commentcount, likedcount, sharecount, mp3url, musicorplaylist));

                }
                else if (instance.events[i].json.playlist != null) //歌单
                {
                    Debug.WriteLine("一个歌单动态");
                    msg = instance.events[i].json.msg;
                    songorplaylistname = instance.events[i].json.playlist.name;
                    picurl = instance.events[i].json.playlist.coverImgUrl;
                    artistname = instance.events[i].json.playlist.creator.nickname;

                    _avatarUrl = instance.events[i].user.avatarUrl;
                    nickname = instance.events[i].user.nickname;
                    commentcount = instance.events[i].info.commentCount;
                    likedcount = instance.events[i].info.likedCount;
                    sharecount = instance.events[i].info.shareCount;
                    musicorplaylist = "分享歌单";
                    Debug.WriteLine(likedcount);
                    mp3url = "";//instance.events[i].json.song.mp3Url.ToString();
                    allItems.Add(new PersonalTrendEventModel(msg, songorplaylistname, picurl, artistname, _avatarUrl, nickname, commentcount, likedcount, sharecount, mp3url, musicorplaylist));
                }
            }
        }
    }
}