using Microsoft.Graphics.Canvas.UI.Xaml;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using NaiveInkCanvas.Model.NewModels.Layer.LayerDefs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml.Input;

namespace NaiveInkCanvas.Model.NewModels.GraphicsBuilder.GraphicsBuilderDefs
{
    public interface ILayerPartBuild
    {
        Action<InkPoint, PointerPoint> CanvasPointerPressed { get; }
        Action<InkPoint, PointerPoint> CanvasPointerReleased { get; }
        Action<InkPoint, PointerPoint> CanvasPointerMoved { get; }
        Action<CanvasControl, CanvasDrawEventArgs> CanvasDraw { get; }
        Action<GraphicsTypes> CanvasPenTypeChanged { get; }

    }
}
