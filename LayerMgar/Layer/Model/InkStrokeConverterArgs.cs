using NaiveInkCanvas.Model.NewModels.Graphicses;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml.Controls;

namespace NaiveInkCanvas.Model.NewModels.Layer.Model
{
    public class InkStrokeConverterArgs
    {
        public InkStrokeConverterArgs(GraphicsRectSizeBase graphics, InkCanvas canvas, InkStrokeBuilder strokeBuilder)
        {
            Graphics = graphics;
            Canvas = canvas;
            StrokeBuilder = strokeBuilder;
            PenAttribute = new InkDrawingAttributes();
        }

        public GraphicsRectSizeBase Graphics { get; }
        public InkCanvas Canvas  { get; }
        public InkStrokeBuilder StrokeBuilder { get;  }
        public InkDrawingAttributes PenAttribute { get; }

    }
}
