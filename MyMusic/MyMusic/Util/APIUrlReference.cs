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
        // 获得音乐的歌词
        public readonly static string GetSongLyricUrl = "http://littlefish33.cn:3000/lyric?id=";
        // 添加喜欢的音乐
        public readonly static string AddLikeSongUrl = "http://littlefish33.cn:3000/playlist/tracks?op=add&pid=";
        // 搜索音乐
        public readonly static string SearchSongsUrl = "http://littlefish33.cn:3000/search?type=1&keywords=";
        // 搜索歌手
        public readonly static string SearchSingerUrl = "http://littlefish33.cn:3000/search?type=100&keywords=";
        // 搜索MV
        public readonly static string SearchMVUrl = "http://littlefish33.cn:3000/search?type=1004&keywords=";
        // 搜索歌单
        public readonly static string SearchSheetUrl = "http://littlefish33.cn:3000/search?type=1000&keywords=";
        // 获得热搜
        public readonly static string HotSearchUrl = "http://littlefish33.cn:3000/search/hot";
        // 获得歌曲详情
        public readonly static string SongDetailUrl = "http://littlefish33.cn:3000/song/detail?ids=";
        // 获得歌词
        public readonly static string GetLyricUrl = "http://littlefish33.cn:3000/lyric?id=";
    }
}
