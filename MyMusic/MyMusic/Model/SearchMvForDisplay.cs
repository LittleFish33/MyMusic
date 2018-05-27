using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    class SearchMvForDisplay : INotifyPropertyChanged
    {
        private string _id;
        private string _name;
        private string _coverUrl;
        private string _artist;
        private int _playCount;
        private long _duration;

        public SearchMvForDisplay(SearchMvJsonModel.Mv other)
        {
            this._id = other.id.ToString();
            this._name = other.name;
            this._coverUrl = other.cover;
            this._artist = other.artistName;
            this._playCount = other.playCount;
            this._duration = other.duration;
        }

        public string Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged("Id"); }
        }
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged("Name"); }
        }
        public string CoverUrl
        {
            get => _coverUrl;
            set { _coverUrl = value; OnPropertyChanged("CoverUrl"); }
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

        public int PlayCount {
            get => _playCount;
            set { _playCount = value; OnPropertyChanged("PlayCount"); }
        }

        public string CoverUrl1 {
            get => _coverUrl;
            set { _coverUrl = value; OnPropertyChanged("CoverUrl1"); }
        }

        //显示实现接口，实现数据绑定动态更新
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
