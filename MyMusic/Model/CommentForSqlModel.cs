using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    public class CommentForSqlModel
    {
        private string _cid;
        private string _uid;
        private string _content;
        private string _name;
        private string _picUrl;
        private string _date;

        public string CommentId
        {
            get => _cid;
            set { _cid = value; OnPropertyChanged("CommentId"); }
        }
        public string UserId
        {
            get => _uid;
            set { _uid = value; OnPropertyChanged("UserId"); }
        }
        public string Content
        {
            get => _content;
            set { _content = value; OnPropertyChanged("Content"); }
        }
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged("Name"); }
        }
        public string PicUrl
        {
            get => _picUrl;
            set { _picUrl = value; OnPropertyChanged("PicUrl"); }
        }
        public string Date
        {
            get => _date;
            set { _date = value; OnPropertyChanged("Date"); }
        }

        public CommentForSqlModel(string cid, string uid, string content, string name, string picUrl, string date)
        {
            _content = content;
            _date = date;
            _cid = cid;
            _uid = uid;
            _name = name;
            _picUrl = picUrl;
        }

        public CommentForSqlModel() { }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
