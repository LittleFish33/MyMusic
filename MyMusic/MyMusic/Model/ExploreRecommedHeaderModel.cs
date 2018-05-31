using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Model
{
    public class Recommendheader
    {
        public string pictureUrl { get; set; }
        public string background { get; set; }
        public string type { get; set; }
        public string id { get; set; }
    }

    public class ExploreRecommedHeaderModel
    {
        public Recommendheader[] recommendHeader { get; set; }
    }
}



