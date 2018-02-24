using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NaiveInkCanvas.Converter
{
    public class FloatDoubleConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ParseValue(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ParseValue(value);
        }
        private object ParseValue(object value)
        {
            if (value is float)
            {
                return double.Parse(value.ToString());
            }
            else if(value is double)
            {
                return float.Parse(value.ToString());
            }
            return null;
        }
    }
}
