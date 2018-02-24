using Microsoft.Graphics.Canvas.UI.Xaml;
using NaiveInkCanvas.Model.NewModels.EventArgs;
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
        event Action<IGraphicsDraw, LayerPointerEventArgs> LayerPointerPressed;
        event Action<IGraphicsDraw, LayerPointerEventArgs> LayerPointerRelaesed;
        event Action<IGraphicsDraw, LayerPointerEventArgs> LayerPointerMove;
    }
}
