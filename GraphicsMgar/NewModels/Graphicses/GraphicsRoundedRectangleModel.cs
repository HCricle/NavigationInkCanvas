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
    public class GraphicsRoundedRectangleModel:GraphicsRectSizeBase
    {
        public float RadiusX {
            get
            {
                Debug.Assert(Points.Count > 0);
                var p1 = Points[0];
                var p2 = Points[Points.Count - 1];
                return (float)(p1.Position.X - p2.Position.X);
            }
        }

        public float RadiusY {
            get
            {
                Debug.Assert(Points.Count > 0);
                var p1 = Points[0];
                var p2 = Points[Points.Count - 1];
                return (float)(p1.Position.Y - p2.Position.Y);
            }
        }
        public GraphicsRoundedRectangleModel(PenModel penModel,IGraphicsDraw draw) 
            : base( GraphicsTypes.RoundRectangle,penModel,draw)
        {
        }
        public override void Draw(ICanvasResourceCreator creator, CanvasDrawingSession ds)
        {
            if (IsFill)
                ds.FillRoundedRectangle(Bounds, RadiusX, RadiusY, PenAttribute.GetBrush(creator));
            else
                ds.DrawRoundedRectangle(Bounds, RadiusX, RadiusY, PenAttribute.GetBrush(creator), PenAttribute.StrokeWidth,StrokeStyle);
        }
    }
}
