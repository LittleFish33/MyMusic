using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    public class CollectedSongerModel
    {
        public CollectedSongerModelResult[] data { get; set; }
        public bool hasMore { get; set; }
        public int count { get; set; }
        public int code { get; set; }
    }

    public class CollectedSongerModelForDisPlay
    {
        public CollectedSongerModelResult[] data { get; set; }
        public bool hasMore { get; set; }
        public int count { get; set; }
        public int code { get; set; }
    }


}
