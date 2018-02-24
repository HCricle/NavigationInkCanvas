using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;

namespace NaiveInkCanvas.Converter
{
    public class EnumStringConverter <TENUM>: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is GraphicsTypes gt)
            {
                return Enum.GetName(typeof(TENUM), value);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                return Enum.Parse(typeof(TENUM), value.ToString());
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
