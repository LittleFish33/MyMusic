using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace MyMusic.Converter
{
    public static class ConverteLongToTimeStr
    {
        public static string Convert(long l)
        {
            int hour = 0;
            int minute = 0;
            int second = 0;
            second = (int)l / 1000;

            if (second > 60)
            {
                minute = second / 60;
                second = second % 60;
            }
            if (minute > 60)
            {
                hour = minute / 60;
                minute = minute % 60;
            }
            if(hour == 0)
            {
                return ( Expand(minute) + ":"
                   + Expand(second));
            }
            else
            {
                return (Expand(hour) + ":" + Expand(minute) + ":" + Expand(second));
            }
            
        }

        public static string Expand(int i,char c = '0')
        {
            if(i < 10)
            {
                return c + i.ToString();
            }
            else
            {
                return i.ToString();
            }
        }

    }
}
