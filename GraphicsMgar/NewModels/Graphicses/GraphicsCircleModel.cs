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
using NaiveInkCanvas.Model.NewModels.Layer.LayerDefs;
using Windows.UI.Input.Inking;
using System.Diagnostics;
using Windows.Graphics.Effects;

namespace NaiveInkCanvas.Model.NewModels.Graphicses
{
    public class GraphicsCircleModel:GraphicsRectSizeBase
    {
        public GraphicsCircleModel(PenModel penModel,IGraphicsDraw draw) 
            : base( GraphicsTypes.Circle,penModel,draw)
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
        public float Radius
        {
            get
            {
                Debug.Assert(Points.Count > 0);
                var p1 = Points[0];
                var p2 = Points[Points.Count-1];
                return (float)Math.Sqrt(Math.Pow(p1.Position.X - p2.Position.X, 2) + Math.Pow(p1.Position.Y - p2.Position.Y, 2));
            }
        }

        public override bool CanRemove()
        {
            return CenterPoint == null;
        }
        public override void Draw(ICanvasResourceCreator creator,CanvasDrawingSession ds)
        {
            if (IsFill)
                ds.FillCircle(CenterPoint, Radius, PenAttribute.GetBrush(creator));
            else
                ds.DrawCircle(CenterPoint, Radius, PenAttribute.GetBrush(creator), PenAttribute.StrokeWidth, StrokeStyle);
        }
    }
}
