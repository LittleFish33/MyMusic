using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Util
{
    class HttpService
    {
        //单例模式
        private static HttpService instance;
        private HttpClient httpClient = new HttpClient();
        private static object _lock = new object();

        private HttpService()
        {
        }

        public static HttpService GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new HttpService();
                    }
                }
            }
            return instance;
        }

        public async Task<string> GetJsonStringAsync(string url)
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);
            string content =  await response.Content.ReadAsStringAsync();
            return content;
        }
        
    }
}
