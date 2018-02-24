using Microsoft.Graphics.Canvas.Brushes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace NaiveInkCanvas.Model.NewModels
{
    public interface ICanSetBrush
    {
        //void SetLinearColorBrush(params CanvasGradientStop[] canvasGradientStops);
        void SetSolidColorBrush(Color color);
        //void SetRadialColorBrush(params CanvasGradientStop[] canvasGradientStops);

    }
}
