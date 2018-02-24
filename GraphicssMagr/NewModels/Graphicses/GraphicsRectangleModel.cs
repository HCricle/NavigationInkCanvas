using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Input;
using Microsoft.Graphics.Canvas;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using System.Diagnostics;
using Windows.Graphics.Effects;

namespace NaiveInkCanvas.Model.NewModels.Graphicses
{
    public class GraphicsRectangleModel : GraphicsRectSizeBase
    {
        public GraphicsRectangleModel(PenModel penModel,IGraphicsDraw draw)
            : base( GraphicsTypes.Rectangle,penModel,draw)
        {
        }
        public override void Draw(ICanvasResourceCreator creator, CanvasDrawingSession ds)
        {
            if (IsFill)
                ds.FillRectangle(Bounds, PenAttribute.GetBrush(creator));
            else
                ds.DrawRectangle(Bounds, PenAttribute.GetBrush(creator), PenAttribute.StrokeWidth, StrokeStyle);
        }
    }
}
