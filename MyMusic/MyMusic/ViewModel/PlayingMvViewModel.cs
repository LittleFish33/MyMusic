using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.ViewModel
{
    public class PlayingMvViewModel : ViewModelBase
    {
        private static PlayingMvViewModel instance;
        private static object _lock = new object();


        //单例模式
        private PlayingMvViewModel()
        {

        }

        public static PlayingMvViewModel GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new PlayingMvViewModel();
                    }
                }
            }
            return instance;
        }

    }
}
