using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MyMusic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.ViewModel
{
    public class ViewModelLocator
    {
        public const string MainPageKey = "MainPage";
        public const string ExploreMusicPageKey = "ExploreMusicPage";

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var nav = new NavigationService();
            nav.Configure(MainPageKey, typeof(MainPage));
            nav.Configure(ExploreMusicPageKey, typeof(ExploreMusicPage));

            SimpleIoc.Default.Register<INavigationService>(() => nav);

            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ExploreMusicViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public ExploreMusicViewModel ExploreMusic => ServiceLocator.Current.GetInstance<ExploreMusicViewModel>();
    }
}
