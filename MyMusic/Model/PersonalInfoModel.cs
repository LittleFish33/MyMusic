using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{

    public class PersonInfoModel
    {
        public int loginType { get; set; }
        public string clientId { get; set; }
        public int effectTime { get; set; }
        public int code { get; set; }
        public Account account { get; set; }
        public Profile profile { get; set; }
        public Binding[] bindings { get; set; }

        private static PersonInfoModel instance = null;
        private static object _lock = new object();

        /*private static class Holder
        {
            private static PersonInfoModel instance = new PersonInfoModel();
        }u*/
      

        public static PersonInfoModel GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new PersonInfoModel();
                    }
                }
            }
            return instance;

        }

        public void init(string str)
        {
            instance = JsonConvert.DeserializeObject<PersonInfoModel>(str);
        }        
    }

    public class Account
    {
        public int id { get; set; }
        public string userName { get; set; }
        public int type { get; set; }
        public int status { get; set; }
        public int whitelistAuthority { get; set; }
        public long createTime { get; set; }
        public string salt { get; set; }
        public int tokenVersion { get; set; }
        public int ban { get; set; }
        public int baoyueVersion { get; set; }
        public int donateVersion { get; set; }
        public int vipType { get; set; }
        public int viptypeVersion { get; set; }
        public bool anonimousUser { get; set; }
    }

    public class Profile
    {
        public string backgroundUrl { get; set; }
        public string detailDescription { get; set; }
        public int djStatus { get; set; }
        public bool followed { get; set; }
        public int userId { get; set; }
        public int accountStatus { get; set; }
        public long avatarImgId { get; set; }
        public Experts experts { get; set; }
        public object expertTags { get; set; }
        public int authStatus { get; set; }
        public long backgroundImgId { get; set; }
        public int userType { get; set; }
        public int vipType { get; set; }
        public int gender { get; set; }
        public string nickname { get; set; }
        public object remarkName { get; set; }
        public bool mutual { get; set; }
        public int province { get; set; }
        public bool defaultAvatar { get; set; }
        public string avatarUrl { get; set; }
        public long birthday { get; set; }
        public int city { get; set; }
        public string avatarImgIdStr { get; set; }
        public string backgroundImgIdStr { get; set; }
        public string description { get; set; }
        public string signature { get; set; }
        public int authority { get; set; }
        public string avatarImgId_str { get; set; }
    }

    public class Experts
    {
    }

    public class Binding
    {
        public int refreshTime { get; set; }
        public string url { get; set; }
        public int userId { get; set; }
        public string tokenJsonStr { get; set; }
        public bool expired { get; set; }
        public int expiresIn { get; set; }
        public long id { get; set; }
        public int type { get; set; }
    }
   
}
