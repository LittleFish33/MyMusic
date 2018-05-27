using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace MyMusic.Converter
{
    class ProgressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((TimeSpan)value).TotalSeconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return TimeSpan.FromSeconds((double)value);
        }
    }

    class ProgressConverterForStr : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ConverteLongToTimeStr.Convert((long)((TimeSpan)value).TotalSeconds * 1000);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return TimeSpan.FromSeconds((double)1);
        }
    }
}
