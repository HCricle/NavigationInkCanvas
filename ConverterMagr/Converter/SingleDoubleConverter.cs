using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NaiveInkCanvas.Converter
{
    public class SingleDoubleConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double.TryParse(value.ToString(), out var res);
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            Single.TryParse(((value as double?).Value/100.0).ToString(), out var res);
            return res;
        }
    }
}
