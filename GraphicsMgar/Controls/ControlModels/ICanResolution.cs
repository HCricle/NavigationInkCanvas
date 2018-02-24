using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace NaiveInkCanvas.Controls.ControlModels
{
    public interface ICanResolution
    {
        SolidColorBrush UnUsedBrush { get; }
        SolidColorBrush UsedBrush { get; }
        bool IsUsed { get; set; }
    }
}
