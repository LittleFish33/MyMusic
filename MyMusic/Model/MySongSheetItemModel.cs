using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    class SongSheetItemModel
    {
        public bool more { get; set; }
        public int code { get; set; }
        public SongSheetModelResult[] playlist { get; set; }
    }

    public class SongSheetModelResult
    {
        public object[] subscribers { get; set; }
        public bool subscribed { get; set; }
        public Creator creator { get; set; }
        public object artists { get; set; }
        public object tracks { get; set; }
        public long updateTime { get; set; }
        public string commentThreadId { get; set; }
        public int totalDuration { get; set; }
        public bool highQuality { get; set; }
        public int trackCount { get; set; }
        public int playCount { get; set; }
        public string coverImgUrl { get; set; }
        public bool ordered { get; set; }
        public string[] tags { get; set; }
        public int adType { get; set; }
        public long trackNumberUpdateTime { get; set; }
        public string description { get; set; }
        public int specialType { get; set; }
        public bool anonimous { get; set; }
        public int privacy { get; set; }
        public bool newImported { get; set; }
        public int status { get; set; }
        public int userId { get; set; }
        public long coverImgId { get; set; }
        public long createTime { get; set; }
        public long trackUpdateTime { get; set; }
        public int cloudTrackCount { get; set; }
        public int subscribedCount { get; set; }
        public string name { get; set; }
        public Int64 id { get; set; }
        public string coverImgId_str { get; set; }
    }

    public class Creator
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
        public string[] expertTags { get; set; }
        public object experts { get; set; }
        public int djStatus { get; set; }
        public int vipType { get; set; }
        public object remarkName { get; set; }
        public string avatarImgIdStr { get; set; }
        public string backgroundImgIdStr { get; set; }
        public string avatarImgId_str { get; set; }
    }

}
