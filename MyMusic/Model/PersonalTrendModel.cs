using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{

    public class PersonalTrendModel
    {
        public long lasttime { get; set; }   
        public bool more { get; set; }
        public int size { get; set; }   // 事件个数
        public Event[] events { get; set; }  //事件类
        public int code { get; set; }
     //   public PersonalTrendModel personalTrend = PersonalTrendModel.GetInstance();

        private static PersonalTrendModel instance = null;
        private static object _lock = new object();
        public static PersonalTrendModel GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new PersonalTrendModel();
                    }
                }
            }
            return instance;
        }
        public void init(string str)
        {
                Debug.WriteLine( "jason字符串：\n" + str + "\n##");
              instance = JsonConvert.DeserializeObject<PersonalTrendModel>(str);
            Debug.WriteLine(instance.events.Length);
        }
    }

    public class Event
    {
        public object actName { get; set; }
        public int forwardCount { get; set; }
        public int expireTime { get; set; }
        public long eventTime { get; set; }
        public Json json { get; set; }
        public object rcmdInfo { get; set; }
        public int actId { get; set; }
        public int tmplId { get; set; }
        public User user { get; set; }
        public object[] pics { get; set; }
        public string uuid { get; set; }
        public long showTime { get; set; }
        public long id { get; set; }
        public int type { get; set; }
        public bool topEvent { get; set; }
        public Info info { get; set; }
    }

    public class Json
    {
        public string msg { get; set; }   //发布消息
        public Playlist2 playlist { get; set; }
        public Song song { get; set; }
    }

    public class Playlist2
    {
        public string name { get; set; }
        public long id { get; set; }
        public long trackNumberUpdateTime { get; set; }
        public int status { get; set; }
        public int userId { get; set; }
        public long createTime { get; set; }
        public long updateTime { get; set; }
        public int subscribedCount { get; set; }
        public int trackCount { get; set; }
        public int cloudTrackCount { get; set; }
        public string coverImgUrl { get; set; }
        public long coverImgId { get; set; }
        public string description { get; set; }
        public string[] tags { get; set; }
        public int playCount { get; set; }
        public long trackUpdateTime { get; set; }
        public int specialType { get; set; }
        public int totalDuration { get; set; }
        public Creator2 creator { get; set; }
        public object tracks { get; set; }
        public object[] subscribers { get; set; }
        public object subscribed { get; set; }
        public string commentThreadId { get; set; }
        public bool newImported { get; set; }
        public int adType { get; set; }
        public bool highQuality { get; set; }
        public int privacy { get; set; }
        public bool ordered { get; set; }
        public bool anonimous { get; set; }
        public string coverImgId_str { get; set; }
    }

    public class Creator2
    {
        public bool defaultAvatar { get; set; }
        public int province { get; set; }
        public int authStatus { get; set; }
        public bool followed { get; set; }
        public string avatarUrl { get; set; }
        public int accountStatus { get; set; }
        public int gender { get; set; }
        public int city { get; set; }
        public long birthday { get; set; }
        public int userId { get; set; }
        public int userType { get; set; }
        public string nickname { get; set; }
        public string signature { get; set; }
        public string description { get; set; }
        public string detailDescription { get; set; }
        public long avatarImgId { get; set; }
        public long backgroundImgId { get; set; }
        public string backgroundUrl { get; set; }
        public int authority { get; set; }
        public bool mutual { get; set; }
        public object expertTags { get; set; }
        public Experts2 experts { get; set; }
        public int djStatus { get; set; }
        public int vipType { get; set; }
        public object remarkName { get; set; }
        public string backgroundImgIdStr { get; set; }
        public string avatarImgIdStr { get; set; }
        public string avatarImgId_str { get; set; }
    }

    public class Experts2
    {
        public string _1 { get; set; }
    }

    public class Song
    {
        public string name { get; set; }   //歌曲名
        public int id { get; set; }
        public int position { get; set; }
        public object[] alias { get; set; }
        public int status { get; set; }
        public int fee { get; set; }
        public int copyrightId { get; set; }
        public string disc { get; set; }
        public int no { get; set; }
        public Artist2[] artists { get; set; }
        public Album album { get; set; }
        public bool starred { get; set; }
        public float popularity { get; set; }
        public int score { get; set; }
        public int starredNum { get; set; }
        public int duration { get; set; }
        public int playedNum { get; set; }
        public int dayPlays { get; set; }
        public int hearTime { get; set; }
        public string ringtone { get; set; }
        public string crbt { get; set; }
        public object audition { get; set; }
        public string copyFrom { get; set; }
        public string commentThreadId { get; set; }
        public object rtUrl { get; set; }
        public int ftype { get; set; }
        public object[] rtUrls { get; set; }
        public int copyright { get; set; }
        public Hmusic hMusic { get; set; }
        public Mmusic mMusic { get; set; }
        public Lmusic lMusic { get; set; }
        public Bmusic bMusic { get; set; }
        public object mp3Url { get; set; }
        public int rtype { get; set; }
        public object rurl { get; set; }
        public int mvid { get; set; }
    }

    public class Album
    {
        public string name { get; set; }
        public int id { get; set; }
        public string type { get; set; }
        public int size { get; set; }
        public string picId { get; set; }
        public string blurPicUrl { get; set; }
        public int companyId { get; set; }
        public long pic { get; set; }
        public string picUrl { get; set; }  // 图片链接
        public long publishTime { get; set; }
        public string description { get; set; }
        public string tags { get; set; }
        public string company { get; set; }
        public string briefDesc { get; set; }
        public Artist artist { get; set; }
        public object[] songs { get; set; }
        public object[] alias { get; set; }
        public int status { get; set; }
        public int copyrightId { get; set; }
        public string commentThreadId { get; set; }
        public Artist1[] artists { get; set; }
        public string img80x80 { get; set; }
    }

    public class Artist
    {
        public string name { get; set; }   //歌手名字
        public int id { get; set; }
        public string picId { get; set; }
        public int img1v1Id { get; set; }
        public string briefDesc { get; set; }
        public string picUrl { get; set; }
        public string img1v1Url { get; set; }
        public int albumSize { get; set; }
        public object[] alias { get; set; }
        public string trans { get; set; }
        public int musicSize { get; set; }
    }

    public class Artist1
    {
        public string name { get; set; }
        public int id { get; set; }
        public string picId { get; set; }
        public int img1v1Id { get; set; }
        public string briefDesc { get; set; }
        public string picUrl { get; set; }
        public string img1v1Url { get; set; }
        public int albumSize { get; set; }
        public object[] alias { get; set; }
        public string trans { get; set; }
        public int musicSize { get; set; }
    }

    public class Hmusic
    {
        public string name { get; set; }
        public int id { get; set; }
        public int size { get; set; }
        public string extension { get; set; }
        public int sr { get; set; }
        public string dfsId { get; set; }
        public int bitrate { get; set; }
        public int playTime { get; set; }
        public float volumeDelta { get; set; }
    }

    public class Mmusic
    {
        public string name { get; set; }
        public int id { get; set; }
        public int size { get; set; }
        public string extension { get; set; }
        public int sr { get; set; }
        public string dfsId { get; set; }
        public int bitrate { get; set; }
        public int playTime { get; set; }
        public float volumeDelta { get; set; }
    }

    public class Lmusic
    {
        public string name { get; set; }
        public int id { get; set; }
        public int size { get; set; }
        public string extension { get; set; }
        public int sr { get; set; }
        public string dfsId { get; set; }
        public int bitrate { get; set; }
        public int playTime { get; set; }
        public float volumeDelta { get; set; }
    }

    public class Bmusic
    {
        public string name { get; set; }
        public int id { get; set; }
        public int size { get; set; }
        public string extension { get; set; }
        public int sr { get; set; }
        public string dfsId { get; set; }
        public int bitrate { get; set; }
        public int playTime { get; set; }
        public float volumeDelta { get; set; }
    }

    public class Artist2
    {
        public string name { get; set; }
        public int id { get; set; }
        public string picId { get; set; }
        public int img1v1Id { get; set; }
        public string briefDesc { get; set; }
        public string picUrl { get; set; }
        public string img1v1Url { get; set; }
        public int albumSize { get; set; }
        public object[] alias { get; set; }
        public string trans { get; set; }
        public int musicSize { get; set; }
    }

    public class User
    {
        public bool defaultAvatar { get; set; }
        public int province { get; set; }
        public int authStatus { get; set; }
        public bool followed { get; set; }
        public string avatarUrl { get; set; }
        public int accountStatus { get; set; }
        public int gender { get; set; }
        public int city { get; set; }
        public long birthday { get; set; }
        public int userId { get; set; }
        public int userType { get; set; }
        public string nickname { get; set; }  //用户昵称
        public string signature { get; set; }  
        public string description { get; set; }
        public string detailDescription { get; set; }
        public long avatarImgId { get; set; }
        public long backgroundImgId { get; set; }
        public string backgroundUrl { get; set; }
        public int authority { get; set; }
        public bool mutual { get; set; }
        public object expertTags { get; set; }
        public object experts { get; set; }
        public int djStatus { get; set; }
        public int vipType { get; set; }
        public object remarkName { get; set; }
        public string avatarImgIdStr { get; set; }
        public string backgroundImgIdStr { get; set; }
        public bool urlAnalyze { get; set; }
        public string avatarImgId_str { get; set; }
    }

    public class Info
    {
        public Commentthread commentThread { get; set; }
        public object latestLikedUsers { get; set; }
        public bool liked { get; set; }         //点赞数
        public object comments { get; set; }    //评论数
        public int resourceType { get; set; }
        public string resourceId { get; set; }
        public string threadId { get; set; }
        public int commentCount { get; set; }
        public int likedCount { get; set; }
        public int shareCount { get; set; }
    }

    public class Commentthread
    {
        public string id { get; set; }
        public object resourceInfo { get; set; }
        public int resourceType { get; set; }
        public int commentCount { get; set; }  //点赞数
        public int likedCount { get; set; }     //评论数
        public int shareCount { get; set; }    // 分享数
        public int hotCount { get; set; }
        public object latestLikedUsers { get; set; }
        public string resourceId { get; set; }
        public int resourceOwnerId { get; set; }
        public object resourceTitle { get; set; }
    }

    public class Resourceinfo
    {
        public long id { get; set; }
        public int userId { get; set; }
        public string name { get; set; }
        public object imgUrl { get; set; }
        public object creator { get; set; }
        public int eventType { get; set; }
    }

    public class Latestlikeduser
    {
        public int s { get; set; }
        public long t { get; set; }
    }

}
