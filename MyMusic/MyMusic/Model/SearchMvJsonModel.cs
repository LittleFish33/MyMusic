using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    class SearchMvJsonModel
    {
        public Result result { get; set; }
        public int code { get; set; }

        public class Result
        {
            public int mvCount { get; set; }
            public Mv[] mvs { get; set; }
        }

        public class Mv
        {
            public int id { get; set; }
            public string cover { get; set; }
            public string name { get; set; }
            public int playCount { get; set; }
            public string briefDesc { get; set; }
            public object desc { get; set; }
            public string artistName { get; set; }
            public int artistId { get; set; }
            public int duration { get; set; }
            public int mark { get; set; }
            public bool subed { get; set; }
            public Artist[] artists { get; set; }
            public string arTransName { get; set; }
            public string alg { get; set; }
        }

        public class Artist
        {
            public int id { get; set; }
            public string name { get; set; }
            public string[] alias { get; set; }
            public object transNames { get; set; }
        }

    }
}
