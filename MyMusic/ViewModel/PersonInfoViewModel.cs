using GalaSoft.MvvmLight;
using MyMusic.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MyMusic.ViewModel
{
	public class PersonInfoViewModel : ViewModelBase, INotifyPropertyChanged
    {    
        public string nickname {
            get => _nickname;
            set { _nickname = value; OnPropertyChanged("nickname"); }
        }
        private string _nickname = "未登录";

        
         public string Avatar
         {
             get => _Avatar;
             set { _Avatar = value; OnPropertyChanged("Avatar"); }
         }
         private string _Avatar = "http://p3.music.126.net/vHvMbZaEgnNkKIJrh7EVvQ==/109951163308969365.jpg";

        public string Signnature
        {
            get => _Signnature;
            set { _Signnature = value; OnPropertyChanged("Signnature"); }
        }
        private string _Signnature = "";

        public int Id
        {
            get => _Id;
            set { _Id = value; OnPropertyChanged("Id"); }
        }
        private int _Id = 0;//320791392;

        public int Province;
        public int City;
        public int Gender;
        public long Birthday;


        private static PersonInfoViewModel instance = null;
        private static object _lock = new object();
        public static PersonInfoViewModel GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new PersonInfoViewModel();
                    }
                }
            }
            return instance;
        }

        //在登录成功时调用，刷新用户控件的值
       public void initial()
        {
            PersonInfoModel model = PersonInfoModel.GetInstance();
            System.Diagnostics.Debug.WriteLine("avatarUrl" + model.profile.avatarUrl);
            nickname = model.profile.nickname;
            Signnature = model.profile.signature;
            Id = model.account.id;
            Province = model.profile.province;
            City = model.profile.city;
            Gender = model.profile.gender;
            Birthday = model.profile.birthday;
            if(Signnature == "")
            {
                Signnature = "这个人什么都没有写`_>`";
            }
            Avatar = model.profile.avatarUrl;
            _Avatar = model.profile.avatarUrl;

            /*LogInInfo.GetInstance();
            LogInInfo.GetInstance().setString(model.profile.avatarUrl, model.profile.nickname);
              try
            {
                nickname = model.profile.nickname;
            }
            catch (Exception e)
            {
                throw e;
            }*/
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

       
    }

}