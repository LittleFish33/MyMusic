using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    class SearchSheetForDisplay : INotifyPropertyChanged
    {
        private long _id;
        private string _name;
        private string _coverUrl;

        public SearchSheetForDisplay(SearchSheetJsonModel.Playlist other)
        {
            this._id = other.id;
            this._name = other.name;
            if(other.coverImgUrl != null)
            {
                this._coverUrl = other.coverImgUrl;
            }
            else
            {
                this._coverUrl = "ms-appx:///Assets/PersonalInfoControl/DefaultPortrait.jpg";
            }

        }

        public long Id
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
