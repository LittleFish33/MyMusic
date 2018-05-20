using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    public class RecommendItemModelResult
    {
        public long id { get; set; }
        public int type { get; set; }
        public string name { get; set; }
        public string copywriter { get; set; }
        public string picUrl { get; set; }
        public bool canDislike { get; set; }
        public float playCount { get; set; }
        public int trackCount { get; set; }
        public bool highQuality { get; set; }
        public string alg { get; set; }
    }

    public class RecommendItemModelResultForDisplay
    {
        public RecommendItemModelResultForDisplay(RecommendItemModelResult result)
        {
            this.id = result.id;
            this.type = result.type;
            this.name = result.name;
            StringBuilder builder = new StringBuilder();
            if (result.name.Length > 8)
            {
                double width = 8,actualWidth = 0;
                int count = 0,length = result.name.Length;
                while (actualWidth < width && count < length)
                {
                    char c = result.name.ElementAt(count++);
                    builder.Append(c);
                    if(c >= 0x4e00 && c <= 0x9fbb)
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
            this.copywriter = result.copywriter;
            this.picUrl = result.picUrl;
            this.canDislike = result.canDislike;
            this.playCount = result.playCount;
            this.trackCount = result.trackCount;
            this.highQuality = result.highQuality;
            this.alg = result.alg;
        }
        public long id { get; set; }
        public int type { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string copywriter { get; set; }
        public string picUrl { get; set; }
        public bool canDislike { get; set; }
        public float playCount { get; set; }
        public int trackCount { get; set; }
        public bool highQuality { get; set; }
        public string alg { get; set; }
    }
}
