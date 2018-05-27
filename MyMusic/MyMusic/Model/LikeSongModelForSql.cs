using MyMusic.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    public class LikeSongModelForSql : INotifyPropertyChanged
    {
        private string _id;
        private string _picUrl;
        private string _url;
        private string _name;
        private string _artist;
        private long _duration;
        private string _length;
        private string _alName;
        private string _lyric;

        public string Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged("Id"); }
        }
        public string PicUrl
        {
            get => _picUrl;
            set { _picUrl = value; OnPropertyChanged("PicUrl"); }
        }

        public string Url
        {
            get => _url;
            set { _url = value; OnPropertyChanged("Url"); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged("Name"); }
        }
        public string Artist
        {
            get => _artist;
            set { _artist = value; OnPropertyChanged("Artist"); }
        }
        public long Duration
        {
            get => _duration;
            set { _duration = value; OnPropertyChanged("Duration"); }
        }
        public string Length
        {
            get => _length;
            set { _length = value; OnPropertyChanged("Length"); }
        }

        public string AlName
        {
            get => _alName;
            set { _alName = value; OnPropertyChanged("AlName"); }
        }

        public string Lyric
        {
            get => _lyric;
            set { _lyric = value; OnPropertyChanged("Lyric"); }
        }

        public LikeSongModelForSql(string id, string picUrl, string url, string name, string artist, long duration, string alName, string lyric)
        {
            this._id = id;
            this._picUrl = picUrl;
            this._url = url;
            this._name = name;
            this._artist = artist;
            this._duration = duration;
            this._length = ConverteLongToTimeStr.Convert(duration);
            this._alName = alName;
            this._lyric = lyric;
        }

        public LikeSongModelForSql() { }

        public LikeSongModelForSql(PlayingSongForSqlModel curSong)
        {
            this._id = curSong.Id;
            this._picUrl = curSong.PicUrl;
            this._url = curSong.Url;
            this._name = curSong.Name;
            this._artist = curSong.Artist;
            this._duration = curSong.Duration;
            this._length = ConverteLongToTimeStr.Convert(_duration);
            this._alName = curSong.AlName;
            this._lyric = curSong.Lyric; 
        }

        public void CopyValue(LikeSongModelForSql other)
        {
            this._id = other._id;
            this._picUrl = other._picUrl;
            this._url = other._url;
            this._name = other._name;
            this._artist = other._artist;
            this._duration = other._duration;
            this._length = other._length;
            this._alName = other._alName;
            this._lyric = other._lyric;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
