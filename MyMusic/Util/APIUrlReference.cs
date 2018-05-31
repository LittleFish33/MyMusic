using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Util
{
    public static class APIUrlReference
    {
        // 获取发现音乐页面里FlipFlow控件内的内容
        public readonly static string ExploreHeaderUrl = "http://littlefish33.cn:8080/GetExploreJsonForMOSAD/GetJson";
        // 获得推荐歌单
        public readonly static string RecommendSongSheetUrl = "http://littlefish33.cn:3000/personalized";
        // 获取推荐MV
        public readonly static string RecommendMVUrl = "http://littlefish33.cn:3000/personalized/mv";
        // 获取音乐的播放地址
        public readonly static string GetPlayingSongUrl = "http://littlefish33.cn:3000/music/url?id=";
        //获得关注列表
        public readonly static string ForcusUserUrl = "http://littlefish33.cn:3000/user/follows?uid=";
        //获得粉丝列表
        public readonly static string FanUserUrl = "http://littlefish33.cn:3000/user/followeds?uid=";
        //获得用户歌单
        public readonly static string UserSongSheetUrl = "http://littlefish33.cn:3000/user/playlist?uid=";
        //新建歌单（登陆后）
        public readonly static string CreateNewSheet = "http://littlefish33.cn:3000/playlist/create?name=wew";
        //获得收藏歌手列表
        public readonly static string CollectedSongerUrl = "http://littlefish33.cn:3000/artist/sublist";
        //更新用户信息
        public readonly static string UpdateUserInfoUrl_0 = "http://littlefish33.cn:3000/user/update/?gender=";
        public readonly static string UpdateUserInfoUrl_1 = "&signature=";
        public readonly static string UpdateUserInfoUrl_2 = "&city=";
        public readonly static string UpdateUserInfoUrl_3 = "&nickname=";
        public readonly static string UpdateUserInfoUrl_4 = "&birthday=";
        public readonly static string UpdateUserInfoUrl_5 = "&province=";

        //获取用户信息
        public readonly static string GetPersonInfo = "http://littlefish33.cn:3000/login/cellphone?phone=";

        //获取用户动态
        public readonly static string GetPersonTrend = "http://littlefish33.cn:3000/user/event?uid=";
    }
}
