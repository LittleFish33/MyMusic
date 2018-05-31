using MyMusic.Model;
using Newtonsoft.Json;
using MyMusic.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MyMusic.Util;
using MyMusic.ViewModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Windows.System;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace MyMusic.Control
{
    public sealed partial class LogInViewControl : UserControl
    {
        public object MessageDialogue { get; private set; }
        private HttpService httpService = HttpService.GetInstance();
        private JsonSerializer jsonSerializer = JsonSerializer.Create();
        PersonInfoViewModel viewModel = PersonInfoViewModel.GetInstance();
        private ContentFrameViewModel ContentFrameVm = ContentFrameViewModel.GetInstance();
        public LogInViewControl()
        {
            this.InitializeComponent();
        }

        public void ShowMessage(string msg)
        {
            txtMessage.Text = msg;
            txtMessage.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Hides the message.
        /// </summary>
        public void HideMessage()
        {
            txtMessage.Text = "";
            txtMessage.Visibility = Visibility.Collapsed;
        }


        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            HideMessage();
            string phone = PhoneNumber_str.Text;
            string password = Password_str.Password;
            string url = APIUrlReference.GetPersonInfo + phone + "&password=" + password; 
            string content = await httpService.GetJsonAfterLogIn(url);
            txtMessage.Visibility = Visibility.Visible;

            if (content.Contains("\"code\":502"))
            {
                txtMessage.Visibility = Visibility.Visible;
                txtMessage.Text = "密码错误";
            }
            else if (content.Contains("\"code\":400"))
            {
                txtMessage.Visibility = Visibility.Visible;
                txtMessage.Text = "不存在该账号";
            }
            else if (content.Contains("\"code\":415"))
            {
                txtMessage.Visibility = Visibility.Visible;
                txtMessage.Text = "您访问的次数太多了";
            }
            else if(content.Contains("\"code\":200"))
            {
                txtMessage.Text = "登录成功";
                
                txtMessage.Visibility= Visibility.Visible;
                //获取单例，刷新用户类信息
                PersonInfoModel.GetInstance().init(content);//= JsonConvert.DeserializeObject<PersonInfoModel>(content); 

                //获取动态消息
                string url1 = APIUrlReference.GetPersonTrend + PersonInfoModel.GetInstance().bindings[0].userId;

                string res = await httpService.GetJsonStringAsync(url1);
                Debug.WriteLine(res);

                //   HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(res, Encoding.GetEncoding("UTF-8"), "application/json") };
                Debug.WriteLine(res);

                //由于请求的字符串多了\ "等字符需要处理，然后才可以序列化成类
                res = res.Replace("\\", "");
                res = res.Replace("\"{", "{");
                res = res.Replace("}\"", "}");
                PersonalTrendModel.GetInstance().init(res);  //实例化
                login();
            }
            
        }

        private  void login()
        {
            viewModel.initial();
            //PersonalTrendViewModel.GetInstance();
            ContentFrameVm.NavigateToPage(typeof(UserPage));
        }

        private void Password_str_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                btnLogin_Click(sender, e as RoutedEventArgs);
            }
        }
    }
}
