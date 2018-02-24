using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;

namespace NaiveInkCanvas.Model.NewModels.LayerDefines
{
    public interface ICanvasDraw
    {
        event Action<CanvasControl, CanvasDrawEventArgs> LayerReDraw;
        event Action<LayerModel, PointerRoutedEventArgs> LayerPressed;
        event Action<LayerModel, PointerRoutedEventArgs> LayerRelaesed;
    }
}
