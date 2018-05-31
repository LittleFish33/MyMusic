using MyMusic.Model;
using MyMusic.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using MyMusic.ViewModel;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MyMusic.Control
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    /// 

    public sealed partial class UserBasicInfor :UserControl
    {
        private ObservableCollection<UsersResultForDisplay> _fanUserItems = new ObservableCollection<UsersResultForDisplay>();
        private ObservableCollection<UsersResultForDisplay> _forcusUserItems = new ObservableCollection<UsersResultForDisplay>();
        private HttpService httpService = HttpService.GetInstance();
        private JsonSerializer jsonSerializer = JsonSerializer.Create();
        private ContentFrameViewModel ContentFrameVm = ContentFrameViewModel.GetInstance();
        private PersonInfoViewModel user = PersonInfoViewModel.GetInstance();
        private int num = 4;
        private bool isMoreForcusUser = false;
        private bool isMoreFanUser = false;

        internal ObservableCollection<UsersResultForDisplay> FanUserItems { get => _fanUserItems; }
        internal ObservableCollection<UsersResultForDisplay> ForcusUserItems { get => _forcusUserItems; }

        public UserBasicInfor()
        {
            this.InitializeComponent();
            InitializeUserItems();
        }

        private async void InitializeUserItems()
        {
            string content_forcus = await httpService.GetJsonStringAsync(APIUrlReference.ForcusUserUrl + user.Id);
            ForcusUserModel forcusModel = jsonSerializer.Deserialize<ForcusUserModel>(new JsonTextReader(new StringReader(content_forcus)));
            if (forcusModel != null)
            {
                forcus.Text += forcusModel.follow.Length;
                int count = 0;
                foreach (UsersModelResult item in forcusModel.follow)
                {
                    if (count == num || count == forcusModel.follow.Length)
                    {
                        break;
                    }
                    _forcusUserItems.Add(new UsersResultForDisplay(item));
                    count++;
                }
            }

            string content_fan = await httpService.GetJsonStringAsync(APIUrlReference.FanUserUrl + user.Id);
            FanUserModel fanModel = jsonSerializer.Deserialize<FanUserModel>(new JsonTextReader(new StringReader(content_fan)));
            if (fanModel != null)
            {
                fan.Text += fanModel.followeds.Length;
                int count = 0;
                foreach (UsersModelResult item in fanModel.followeds)
                {
                    if (count == num || count == fanModel.followeds.Length)
                    {
                        break;
                    }
                    _fanUserItems.Add(new UsersResultForDisplay(item));
                    count++;
                }
            }
        }

        private async void MoreForcusUser(object sender, RoutedEventArgs e)
        {
            string content = await httpService.GetJsonStringAsync(APIUrlReference.ForcusUserUrl + user.Id);
            ForcusUserModel model = jsonSerializer.Deserialize<ForcusUserModel>(new JsonTextReader(new StringReader(content)));
            if (model != null)
            {
                if (isMoreForcusUser)
                {
                    for(int i = model.follow.Length - 1; i >= num; i--)
                    {
                        _forcusUserItems.RemoveAt(i);
                    }
                    isMoreForcusUser = false;
                    up1.Visibility = (Visibility)1;
                    down1.Visibility = 0;
                    return;
                }
                isMoreForcusUser = true;
                int count = 0;
                foreach (UsersModelResult item in model.follow)
                {
                    if(count >= num)
                    {
                        if (count == model.follow.Length)
                        {
                            break;
                        }
                        _forcusUserItems.Add(new UsersResultForDisplay(item));
                    }
                    count++;
                }
                down1.Visibility = (Visibility)1;
                up1.Visibility = 0;
            }
        }

        private async void MoreFanUser(object sender, RoutedEventArgs e)
        {
            string content = await httpService.GetJsonStringAsync(APIUrlReference.FanUserUrl + user.Id);
            FanUserModel model = jsonSerializer.Deserialize<FanUserModel>(new JsonTextReader(new StringReader(content)));
            if (model != null)
            {
                if (isMoreFanUser)
                {
                    for (int i = model.followeds.Length - 1; i >= num; i--)
                    {
                        _fanUserItems.RemoveAt(i);
                    }
                    isMoreFanUser = false;
                    up2.Visibility = (Visibility)1;
                    down2.Visibility = 0;
                    return;
                }
                isMoreFanUser = true;
                int count = 0;
                foreach (UsersModelResult item in model.followeds)
                {
                    if (count >= num)
                    {
                        if (count == model.followeds.Length)
                        {
                            break;
                        }
                        _fanUserItems.Add(new UsersResultForDisplay(item));
                    }
                    count++;
                }
                down2.Visibility = (Visibility)1;
                up2.Visibility = 0;
            }
        }

        private void ChangeText(object sender, PointerRoutedEventArgs e)
        {
            introduction.Visibility = (Visibility)1;
            changeText.Visibility = 0;
        }

        private async void changeText_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                string url = APIUrlReference.UpdateUserInfoUrl_0 + user.Gender;
                if(changeText.Text != "这个人什么都没有写`_>`")
                {
                    url += APIUrlReference.UpdateUserInfoUrl_1 + changeText.Text;
                }
                else
                {
                    url += APIUrlReference.UpdateUserInfoUrl_1 + "";
                }
                url += APIUrlReference.UpdateUserInfoUrl_2 + user.City;
                url += APIUrlReference.UpdateUserInfoUrl_3 + user.nickname;
                url += APIUrlReference.UpdateUserInfoUrl_4 + user.Birthday;
                url += APIUrlReference.UpdateUserInfoUrl_5 + user.Province;
                try
                {
                    string content = await httpService.GetJsonAfterLogIn(url);
                }catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                introduction.Visibility = 0;
                changeText.Visibility = (Visibility)1;
            }
        }
    }
}
