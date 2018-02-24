using NaiveInkCanvas.Model.NewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace NaiveInkCanvas.Converter
{
    public class LayerToTextColorConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is LayerModel lm)
            {
                return lm.IsAppear ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.DarkBlue);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
