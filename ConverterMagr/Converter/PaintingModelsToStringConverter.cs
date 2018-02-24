using NaiveInkCanvas.Helpers.DefHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace NaiveInkCanvas.Converter
{
    public class PaintingModelsToStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                return PaintingModelHelper.ModelStrings[(PaintingModels)value];
            }
            catch (Exception) { }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
