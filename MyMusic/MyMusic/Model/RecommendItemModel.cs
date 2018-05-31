using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    public class RecommendItemModel
    {
        public bool hasTaste { get; set; }
        public int code { get; set; }
        public int category { get; set; }
        public RecommendItemModelResult[] result { get; set; }
    }

    public class RecommendItemModelForDisplay
    {
        public bool hasTaste { get; set; }
        public int code { get; set; }
        public int category { get; set; }
        public RecommendItemModelResult[] result { get; set; }
    }
}
