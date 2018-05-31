using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    class RecommendMVModel
    {
        public int code { get; set; }
        public int category { get; set; }
        public RecommendMVModelResult[] result { get; set; }
    }
}
