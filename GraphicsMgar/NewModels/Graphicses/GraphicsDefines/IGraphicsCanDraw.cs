using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Effects;
using Windows.UI.Input;
using Windows.UI.Xaml.Input;

namespace NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines
{
    public interface IGraphicsCanDraw
    {
        void KeyDown(KeyRoutedEventArgs args);
        void DrawGraphics(ICanvasResourceCreator creator, CanvasDrawingSession session);
        void DrawEffect(IGraphicsEffectSource eff, CanvasDrawingSession session);
    }
}
