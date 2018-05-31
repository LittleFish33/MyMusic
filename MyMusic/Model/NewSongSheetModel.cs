using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    public class NewSongSheetModel
    {
        public long id { get; set; }
        public int code { get; set; }
        public Playlist playlist { get; set; }
    }

    public class Playlist
    {
        public object[] subscribers { get; set; }
        public object subscribed { get; set; }
        public object creator { get; set; }
        public object artists { get; set; }
        public object tracks { get; set; }
        public long coverImgId { get; set; }
        public int trackUpdateTime { get; set; }
        public int trackCount { get; set; }
        public bool anonimous { get; set; }
        public long updateTime { get; set; }
        public int userId { get; set; }
        public int playCount { get; set; }
        public string commentThreadId { get; set; }
        public int totalDuration { get; set; }
        public int privacy { get; set; }
        public bool newImported { get; set; }
        public int specialType { get; set; }
        public long createTime { get; set; }
        public string coverImgUrl { get; set; }
        public bool highQuality { get; set; }
        public int status { get; set; }
        public bool ordered { get; set; }
        public object[] tags { get; set; }
        public int subscribedCount { get; set; }
        public int cloudTrackCount { get; set; }
        public object description { get; set; }
        public int adType { get; set; }
        public int trackNumberUpdateTime { get; set; }
        public string name { get; set; }
        public long id { get; set; }
    }


}
