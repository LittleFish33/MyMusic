using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    class HotJsonObjectModel
    {
        public int code { get; set; }
        public Result result { get; set; }

        public class Result
        {
            public Hot[] hots { get; set; }
        }

        public class Hot
        {
            public string first { get; set; }
            public int second { get; set; }
            public object third { get; set; }
            public int iconType { get; set; }
        }

    }
}
