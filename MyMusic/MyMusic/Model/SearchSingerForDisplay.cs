using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    class SearchSingerForDisplay : INotifyPropertyChanged
    {
        private string _id;
        private string _name;
        private string _picUrl;

        public SearchSingerForDisplay(SearchSingerJsonModel.Artist other)
        {
            this._id = other.id.ToString();
            this._name = other.name;
            if (other.picUrl ==  null || other.picUrl == "")
            {
                this._picUrl = "ms-appx:///Assets/PersonalInfoControl/DefaultPortrait.jpg";
            }
            else
            {
                this._picUrl = other.picUrl;
            }
        }

        public string Id {
            get => _id;
            set { _id = value; OnPropertyChanged("Id"); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged("Name"); }
        }

        public string PicUrl {
            get => _picUrl;
            set { _picUrl = value; OnPropertyChanged("PicUrl"); }
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
