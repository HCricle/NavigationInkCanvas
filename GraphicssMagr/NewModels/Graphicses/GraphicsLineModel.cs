using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Geometry;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Input;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using System.Diagnostics;
using Windows.Foundation;
using NaiveInkCanvas.Model.NewModels.Layer.LayerDefs;
using Windows.UI.Input.Inking;
using Windows.Graphics.Effects;
using Microsoft.Graphics.Canvas.Effects;

namespace NaiveInkCanvas.Model.NewModels.Graphicses
{
    public class GraphicsLineModel : GraphicsRectSizeBase
    {
        public GraphicsLineModel(PenModel penModel,IGraphicsDraw draw) 
            : base( GraphicsTypes.CurveLine,penModel,draw)
        {
            IsFill = true;
        }
        public override void Draw(ICanvasResourceCreator creator, CanvasDrawingSession ds)
        {
            using (var cp = new CanvasPathBuilder(creator))
            {
                cp.BeginFigure(new Vector2((float)Points[0].Position.X, (float)Points[0].Position.Y));
                Points.ForEach(p => cp.AddLine((float)p.Position.X, (float)p.Position.Y));
                cp.EndFigure(CanvasFigureLoop.Open);
                using (var cgg = CanvasGeometry.CreatePath(cp))
                {
                    ds.DrawGeometry(cgg, PenAttribute.GetBrush(creator), PenAttribute.StrokeWidth, StrokeStyle);
                }
            }
        }
    }
}
