using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{

    public class SongSheetModelResultForDisplay
    {
        public SongSheetModelResultForDisplay(NewSongSheetModel result)
        {
            id = result.playlist.id;
            shortName = result.playlist.name;
            picUrl = result.playlist.coverImgUrl;
        }
        public SongSheetModelResultForDisplay(SongSheetModelResult result)
        {
            this.id = result.id;
            this.name = result.name;
            StringBuilder builder = new StringBuilder();
            if (result.name.Length > 8)
            {
                double width = 8, actualWidth = 0;
                int count = 0, length = result.name.Length;
                while (actualWidth < width && count < length)
                {
                    char c = result.name.ElementAt(count++);
                    builder.Append(c);
                    if (c >= 0x4e00 && c <= 0x9fbb)
                    {
                        actualWidth += 1;
                    }
                    else
                    {
                        actualWidth += 0.5;
                    }
                }
                builder.Append("...");
            }
            else
            {
                builder.Append(result.name);
            }
            this.shortName = builder.ToString();
            this.picUrl = result.coverImgUrl;
            this.playCount = result.playCount;
            this.trackCount = result.trackCount;
            this.highQuality = result.highQuality;
            this.creatorId = result.creator.userId;
        }

        public long id { get; set; }
        public int type { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string picUrl { get; set; }
        public float playCount { get; set; }
        public int trackCount { get; set; }
        public bool highQuality { get; set; }
        public int creatorId { get; set; }
    }
}
