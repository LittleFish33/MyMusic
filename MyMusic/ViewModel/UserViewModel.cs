using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using MyMusic.Util;

namespace MyMusic.ViewModel
{

    public class UserViewModel : ViewModelBase
    {
        private HttpService httpService = HttpService.GetInstance();
        private JsonSerializer jsonSerializer = JsonSerializer.Create();


    }
}
