
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input;
using Microsoft.Graphics.Canvas;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using Windows.Graphics.Effects;

namespace NaiveInkCanvas.Model.NewModels.Graphicses
{
    public class GraphicsStraightLineModel:GraphicsRectSizeBase
    {
        public Vector2 PointX
        {
            get
            {
                Debug.Assert(Points.Count > 0);
                var p1 = Points[0];
                return new Vector2((float)p1.Position.X, (float)p1.Position.Y);

            }
        }
        public Vector2 PointY
        {
            get
            {
                Debug.Assert(Points.Count > 0);
                var p1 = Points[Points.Count-1];
                return new Vector2((float)p1.Position.X, (float)p1.Position.Y);
            }
        }

        public GraphicsStraightLineModel(PenModel penModel,IGraphicsDraw draw) 
            : base( GraphicsTypes.StraightLine,penModel,draw)
        {
        }
        public override void Draw(ICanvasResourceCreator creator, CanvasDrawingSession ds)
        {
            ds.DrawLine(PointX, PointY, PenAttribute.GetBrush(creator), PenAttribute.StrokeWidth, StrokeStyle);
        }
    }
}
