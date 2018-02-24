using NaiveInkCanvas.Model.NewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace NaiveInkCanvas.Converter
{
    public class LayerToCanvasConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is LayerModel lm &&parameter is FrameworkElement ue)
            {
                Canvas.SetLeft(ue, lm.CanvasSize.Left);
                Canvas.SetTop(ue, lm.CanvasSize.Top);
                ue.Width = lm.CanvasSize.Right - lm.CanvasSize.Left;
                ue.Height= lm.CanvasSize.Bottom - lm.CanvasSize.Top; 
                return lm.Canvas;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
