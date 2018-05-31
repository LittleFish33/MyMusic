using MyMusic.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace MyMusic.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LogInPage : Page
    {
        public LogInPage()
        {
            this.InitializeComponent();
        }

        /*
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            HideMessage();
            string phone = PhoneNumber_str.Text;
            string password = Password_str.Password;

            Util.HttpService LogInApI = Util.HttpService.GetInstance();

            string url = "littlefish33.cn:3000/login/cellphone?phone=" + phone + "&password=" + password;
            String LogInJson = LogInApI.GetJsonStringAsync(url).ToString();
            if (LogInJson.Length < 100)
            {

            }
            else
            {
                PersonInfoModel res = PersonInfoModel.GetInstance();
                res = JsonConvert.DeserializeObject<PersonInfoModel>(LogInJson);

            }
        }*/
    }
}
