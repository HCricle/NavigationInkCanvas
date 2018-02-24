using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    public class GraphicsEllipseModel:GraphicsRectSizeBase
    {
        public GraphicsEllipseModel(PenModel penModel,IGraphicsDraw draw) 
            : base( GraphicsTypes.Ellipse,penModel,draw)
        {
        }
        public Vector2 CenterPoint
        {
            get
            {
                Debug.Assert(Points.Count > 0);
                var p1 = Points[0];
                return new Vector2((float)p1.Position.X, (float)p1.Position.Y);
            }
        }
        public float RadiusX {
            get
            {
                Debug.Assert(Points.Count > 0);
                var p1 = Points[0];
                var p2 = Points[Points.Count - 1];
                return (float)(p1.Position.X-p2.Position.X);
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
        public override void Draw(ICanvasResourceCreator creator, CanvasDrawingSession session)
        {
            if (IsFill)
                session.FillEllipse(CenterPoint, RadiusX, RadiusY, PenAttribute.GetBrush(creator));
            else
                session.DrawEllipse(CenterPoint, RadiusX, RadiusY, PenAttribute.GetBrush(creator), PenAttribute.StrokeWidth, StrokeStyle);
        }
    }
}
