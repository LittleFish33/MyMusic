using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    // 很不想建着个类的，但是为了将一个int实现实时绑定没办法建了，如果你们有解决办法，把这个解决了吧
    class PlayingListModel: INotifyPropertyChanged
    {
        string _disPlayCount = "";

        public PlayingListModel()
        {

        }

        public string DisPlayCount {
            get => _disPlayCount;
            set { _disPlayCount = value; OnPropertyChanged("DisPlayCount"); }
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
