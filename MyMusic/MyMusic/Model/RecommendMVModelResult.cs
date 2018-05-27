using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    class RecommendMVModelResult
    {
        public int id { get; set; }
        public int type { get; set; }
        public string name { get; set; }
        public string copywriter { get; set; }
        public string picUrl { get; set; }
        public bool canDislike { get; set; }
        public int duration { get; set; }
        public int playCount { get; set; }
        public bool subed { get; set; }
        public Artist[] artists { get; set; }
        public string artistName { get; set; }
        public int artistId { get; set; }
        public string alg { get; set; }

        public class Artist
        {
            public int id { get; set; }
            public string name { get; set; }
        }
    }
}
