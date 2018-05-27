﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    class LyricModel
    {

        public bool sgc { get; set; }
        public bool sfy { get; set; }
        public bool qfy { get; set; }
        public Lrc lrc { get; set; }
        public Klyric klyric { get; set; }
        public Tlyric tlyric { get; set; }
        public int code { get; set; }

        public class Lrc
        {
            public int version { get; set; }
            public string lyric { get; set; }
        }

        public class Klyric
        {
            public int version { get; set; }
            public object lyric { get; set; }
        }

        public class Tlyric
        {
            public int version { get; set; }
            public object lyric { get; set; }
        }

    }
}
