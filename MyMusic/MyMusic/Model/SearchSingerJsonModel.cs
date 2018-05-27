using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    class SearchSingerJsonModel
    {

        public Result result { get; set; }
        public int code { get; set; }

        public class Result
        {
            public int artistCount { get; set; }
            public Artist[] artists { get; set; }
        }

        public class Artist
        {
            public int id { get; set; }
            public string name { get; set; }
            public string picUrl { get; set; }
            public string[] alias { get; set; }
            public int albumSize { get; set; }
            public long picId { get; set; }
            public string img1v1Url { get; set; }
            public long img1v1 { get; set; }
            public int mvSize { get; set; }
            public bool followed { get; set; }
            public string alg { get; set; }
            public string[] alia { get; set; }
            public object trans { get; set; }
        }
    }
}
