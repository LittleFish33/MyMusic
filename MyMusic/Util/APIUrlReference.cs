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
    }
}
