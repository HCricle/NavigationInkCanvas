using Microsoft.Graphics.Canvas.Brushes;
using NaiveInkCanvas.Model.NewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;

namespace NaiveInkCanvas.Converter
{
    public class MyGradientStopConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return new CanvasGradientStop() { Color = (Color)value, Position = (Single)parameter };
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
