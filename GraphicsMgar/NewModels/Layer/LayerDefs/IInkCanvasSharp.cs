using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml.Controls;

namespace NaiveInkCanvas.Model.NewModels.Layer.LayerDefs
{
    public interface IInkCanvasSharp
    {
        InkCanvas TopCanvas { get; }

        InkStrokeBuilder StrokeBuilder { get; }
        event Action<InkPoint, PointerPoint> BeginDraw;
        event Action<InkPoint, PointerPoint> Drawing;
        event Action<InkPoint, PointerPoint> Drawed;
        event Action<PointerPoint> PointerHovered;
        event Action<PointerPoint> PointerLost;
        void ChangeCanvasModel(bool iswin2d);
    }
}
