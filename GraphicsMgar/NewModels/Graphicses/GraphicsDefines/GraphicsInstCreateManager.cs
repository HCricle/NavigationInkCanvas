using NaiveInkCanvas.Model.NewModels.Layer.LayerDefs;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines
{
    public static class GraphicsInstCreateManager
    {
        public static Dictionary<GraphicsTypes, Type> Pens { get; }
            = new Dictionary<GraphicsTypes, Type>();
        static GraphicsInstCreateManager()
        {
            Pens.Add(GraphicsTypes.Circle, typeof(GraphicsCircleModel));
            Pens.Add(GraphicsTypes.CurveLine, typeof(GraphicsLineModel));
            Pens.Add(GraphicsTypes.Ellipse, typeof(GraphicsEllipseModel));
            Pens.Add(GraphicsTypes.Rectangle, typeof(GraphicsRectangleModel));
            Pens.Add(GraphicsTypes.RoundRectangle, typeof(GraphicsRoundedRectangleModel));
            Pens.Add(GraphicsTypes.StraightLine, typeof(GraphicsStraightLineModel));
            Pens.Add(GraphicsTypes.Text, typeof(GraphicsTextModel));
            Pens.Add(GraphicsTypes.GaussianBlur, typeof(GaussianBlurModel));
        }
        public static object CreateGraphics(GraphicsTypes pen,PenModel penModel,IGraphicsDraw draw,IInkCanvasSharp canvasSharp)
        {
            return Activator.CreateInstance(Pens[pen], penModel,draw);
        }
    }
}
