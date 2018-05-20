using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Util
{
    public class ShareDataService
    {
        //单例模式
        private static ShareDataService instance;
        private static object _lock = new object();
        private bool isLogin;                  

        private ShareDataService()
        {
            isLogin = false;
        }

        public static ShareDataService GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new ShareDataService();
                    }
                }
            }
            return instance;
        }
    }
}
