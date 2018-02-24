using NaiveInkCanvas.Model.NewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace NaiveInkCanvas.Converter
{
    public class ObjectExchangeUIElementConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is LayerModel lm)
            {
                return lm.Canvas;
            }
            return null;
        }
    }
}
