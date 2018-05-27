using MyMusic.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    public class SearchSongsForDisplay: INotifyPropertyChanged
    {
        private string _id;
        private string _name;
        private string _artist;
        private string _alName;
        private long _duration;
        private string _length;

        public SearchSongsForDisplay(SearchSongsJsonObject.Song other)
        {
            this._id = other.id.ToString();
            this._name = other.name;
            this._alName = other.album.name;
            this._length = ConverteLongToTimeStr.Convert(other.duration);
            this._artist = other.artists[0].name;
            this._duration = other.duration;
        }

        public string Id {
            get => _id;
            set { _id = value; OnPropertyChanged("Id"); }
        }
        public string Name {
            get => _name;
            set { _name = value; OnPropertyChanged("Name"); }
        }
        public string AlName {
            get => _alName;
            set { _alName = value; OnPropertyChanged("AlName"); }
        }
        public string Length {
            get => _length;
            set { _length = value; OnPropertyChanged("Length"); }
        }

        public string Artist {
            get => _artist;
            set { _artist = value; OnPropertyChanged("Artist"); }
        }

        public long Duration {
            get => _duration;
            set { _duration = value; OnPropertyChanged("Duration"); }
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
