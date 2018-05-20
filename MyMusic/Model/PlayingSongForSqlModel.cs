using GalaSoft.MvvmLight;
using MyMusic.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    public class PlayingSongForSqlModel : INotifyPropertyChanged
    {
        private string _id;
        private string _picUrl;
        private string _url;
        private string _name;
        private string _artist;
        private long _duration;
        private string _length;
        private int _like;
        private int _playing;

        public string Id {
            get => _id;
            set { _id = value; OnPropertyChanged("Id"); }
        }
        public string PicUrl {
            get => _picUrl;
            set { _picUrl = value; OnPropertyChanged("PicUrl"); }
        }

        public string Url {
            get => _url;
            set { _url = value; OnPropertyChanged("Url"); }
        }

        public string Name {
            get => _name;
            set { _name = value; OnPropertyChanged("Name"); }
        }
        public string Artist {
            get => _artist;
            set { _artist = value; OnPropertyChanged("Artist"); }
        }
        public long Duration {
            get => _duration;
            set { _duration = value; OnPropertyChanged("Duration"); }
        }
        public int Like {
            get => _like;
            set { _like = value; OnPropertyChanged("Like"); }
        }
        public int Playing {
            get => _playing;
            set { _playing = value; OnPropertyChanged("Playing"); }
        }
        public string Length {
            get => _length;
            set { _length = value; OnPropertyChanged("Length"); }
        }

        public PlayingSongForSqlModel(string id,string picUrl,string url,string name,string artist,long duration, int like,int playing)
        {
            this._id = id;
            this._picUrl = picUrl;
            this._url = url;
            this._name = name;
            this._artist = artist;
            this._duration = duration;
            this._like = like;
            this._playing = playing;
            this._length = ConverteLongToTimeStr.Convert(duration);
        }

        public PlayingSongForSqlModel() { }

        public void CopyValue(PlayingSongForSqlModel other)
        {
            this._id = other._id;
            this._picUrl = other._picUrl;
            this._url = other._url;
            this._name = other._name;
            this._artist = other._artist;
            this._duration = other._duration;
            this._like = other._like;
            this._playing = other._playing;
            this._length = other._length;
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
