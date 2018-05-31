using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MyMusic.Model
{
    public class LeftNavListItemModel
    {
        public LeftNavListItemModel(Symbol symbol, string displayName, Type pageType)
        {
            Symbol = symbol;
            DisplayName = displayName;
            PageType = pageType;
        }
        public Symbol Symbol { get; set; }
        public string DisplayName { get; set; }
        public Type PageType { get; set; }
    }
}
