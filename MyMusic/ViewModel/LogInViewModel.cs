using GalaSoft.MvvmLight;
using MyMusic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.ViewModel
{
   

    class LogInViewModel
    {
        private ObservableCollection<PersonInfoModel> _PersonInfoSource = new ObservableCollection<PersonInfoModel>();

        public ObservableCollection<PersonInfoModel> PersonInfoSource { get => PersonInfoSource; }


    }
}
