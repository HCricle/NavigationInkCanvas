using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace NaiveInkCanvas.Model.NewModels.LayerDefines
{
    public interface ILayerCanvas
    {
        string Name { get; set; }
        CanvasControl Canvas { get; }
        bool IsAppear { get; set; }
        void DrawGraphicses(ICanvasResourceCreator creator, CanvasDrawingSession ds);
    }
}
