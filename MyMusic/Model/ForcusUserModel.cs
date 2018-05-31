using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    public class ForcusUserModel
    {
        public UsersModelResult[] follow { get; set; }
        public int touchCount { get; set; }
        public bool more { get; set; }
        public int code { get; set; }

    }

    public class FanUserModel
    {
        public UsersModelResult[] followeds { get; set; }
        public bool more { get; set; }
        public int code { get; set; }
    }

    public class UsersModelResult
    {
        public string py { get; set; }
        public long time { get; set; }
        public bool followed { get; set; }
        public int userId { get; set; }
        public int accountStatus { get; set; }
        public int vipType { get; set; }
        public int gender { get; set; }
        public string nickname { get; set; }
        public object remarkName { get; set; }
        public int follows { get; set; }
        public bool mutual { get; set; }
        public string avatarUrl { get; set; }
        public object experts { get; set; }
        public int followeds { get; set; }
        public object expertTags { get; set; }
        public int authStatus { get; set; }
        public int userType { get; set; }
        public string signature { get; set; }
        public int eventCount { get; set; }
        public int playlistCount { get; set; }
    }

    public class UsersResultForDisplay
    {
        public UsersResultForDisplay(UsersModelResult other)
        {
            this.py = other.py;
            this.time = other.time;
            this.followed = other.followed;
            this.userId = other.userId;
            this.accountStatus = other.accountStatus;
            this.vipType = other.vipType;
            this.gender = other.gender;
            this.nickname = other.nickname;
            this.remarkName = other.remarkName;
            this.follows = other.follows;
            this.mutual = other.mutual;
            this.avatarUrl = other.avatarUrl;
            this.experts = other.experts;
            this.followeds = other.followeds;
            this.expertTags = other.expertTags;
            this.authStatus = other.authStatus;
            this.userType = other.userType;
            this.signature = other.signature;
            this.eventCount = other.eventCount;
            this.playlistCount = other.playlistCount;
        }

        public string py { get; set; }
        public long time { get; set; }
        public bool followed { get; set; }
        public int userId { get; set; }
        public int accountStatus { get; set; }
        public int vipType { get; set; }
        public int gender { get; set; }
        public string nickname { get; set; }
        public object remarkName { get; set; }
        public int follows { get; set; }
        public bool mutual { get; set; }
        public string avatarUrl { get; set; }
        public object experts { get; set; }
        public int followeds { get; set; }
        public object expertTags { get; set; }
        public int authStatus { get; set; }
        public int userType { get; set; }
        public string signature { get; set; }
        public int eventCount { get; set; }
        public int playlistCount { get; set; }
    }
}
