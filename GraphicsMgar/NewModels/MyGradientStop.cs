using Microsoft.Graphics.Canvas.Brushes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace NaiveInkCanvas.Model.NewModels
{
    public class MyGradientStop
    {
        public Color Color { get; set; }
        public Single Position { get; set; }
        public CanvasGradientStop ToCanvasGradientStop()
        {
            return new CanvasGradientStop() { Color = Color, Position = Position };
        }
    }
}
