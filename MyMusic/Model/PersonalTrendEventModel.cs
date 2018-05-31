using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    // 由于调用接口直接得到类里头的Event 数量不确定，不利于多个动态绑定。
    public class PersonalTrendEventModel : INotifyPropertyChanged
    {
        private string _msg;
        private string _SongPlaylist;
        private string _picUrl;
        private string _avatarUrl;
        private string _Aritistname;
        private string _nickname;
        private int _commentCount;
        private int _likedCount;
        private int _shareCount;
        private string _mp3url;
        private string _MusicOrPlaylist;
        //  public int size { get; set; }   // 事件个数
        //    public Event[] events { get; set; }  //事件类
        public string MusicOrPlaylist { get => _MusicOrPlaylist; set { _MusicOrPlaylist = value; OnPropertyChanged(); } }
        public string msg { get => _msg; set { _msg = value; OnPropertyChanged(); } }//发布消息
        public string SongPlaylist { get => _SongPlaylist; set { _SongPlaylist = value; OnPropertyChanged(); } }   //歌曲名
        public string picUrl { get => _picUrl; set { _picUrl = value; OnPropertyChanged(); } } // 歌曲封面图片链接
        public string avatarUrl { get => _avatarUrl; set { _avatarUrl = value; OnPropertyChanged(); } }  //用户头像
        public string Aritistname { get => _Aritistname; set { _Aritistname = value; OnPropertyChanged(); } }  //歌手名字
        public string nickname { get => _nickname; set { _nickname = value; OnPropertyChanged(); } } //用户昵称
        public int commentCount { get => _commentCount; set { _commentCount = value; OnPropertyChanged(); } }   // 点赞数
        public int likedCount { get => _likedCount; set { _likedCount = value; OnPropertyChanged(); } }
        public int shareCount { get => _shareCount; set { _shareCount = value; OnPropertyChanged(); } } //分享数
        public string mp3url { get => _mp3url; set { _mp3url = value; OnPropertyChanged(); } }  //mp3 

        public PersonalTrendEventModel(string msg, string SongPlaylist, string picurl, string artistname, string avatarUrl, string nickname, int commentcount, int likedcount, int sharecount, string mp3url, string musicorplaylist)
        {
            this.msg = msg;
            this.SongPlaylist = SongPlaylist;
            this.picUrl = picurl;
            this.avatarUrl = avatarUrl;
            this.Aritistname = artistname;
            this.nickname = nickname;
            this.commentCount = commentcount;
            this.likedCount = likedcount;
            this.shareCount = sharecount;
            this.mp3url = mp3url;
            this.MusicOrPlaylist = musicorplaylist;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
