using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{

    public class CollectedSongerModelResult
    {
        public string info { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public object trans { get; set; }
        public string[] alias { get; set; }
        public int albumSize { get; set; }
        public int mvSize { get; set; }
        public long picId { get; set; }
        public string picUrl { get; set; }
        public string img1v1Url { get; set; }
    }

    public class CollectedSongerModelResultForDisplay
    {
        private int _id;
        private string _name;
        private string _picUrl;

        public CollectedSongerModelResultForDisplay() { id = 0; }

        public CollectedSongerModelResultForDisplay(CollectedSongerModelResult result)
        {
            _id = result.id;
            _name = result.name;
            _picUrl = result.picUrl;
        }

        public CollectedSongerModelResultForDisplay(int i, string n, string p)
        {
            _id = i;
            _name = n;
            _picUrl = p;
        }
        public int id
        {
            get => _id;
            set { _id = value; OnPropertyChanged("id"); }
        }
        public string name
        {
            get => _name;
            set { _name = value; OnPropertyChanged("string"); }
        }
        public string picUrl
        {
            get => _picUrl;
            set { _picUrl = value; OnPropertyChanged("picUrl"); }
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
