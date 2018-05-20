using MyMusic.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    public class ContentFrameType : INotifyPropertyChanged
    {
        private Type _type;
        private static ContentFrameType instance;
        private static object _lock = new object();


        private ContentFrameType()
        {
            Type = typeof(ExploreMusicPage);
        }
        public static ContentFrameType GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new ContentFrameType();
                    }
                }
            }
            return instance;
        }

        public Type Type {
            get => _type;
            set { _type = value; OnPropertyChanged("Type"); }
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
