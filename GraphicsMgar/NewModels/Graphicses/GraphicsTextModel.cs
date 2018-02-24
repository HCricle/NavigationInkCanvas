using Microsoft.Graphics.Canvas.Text;
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
using NaiveInkCanvas.Model.NewModels.Layer.LayerDefs;
using System.Diagnostics;
using Windows.UI.Input.Inking;
using Windows.Graphics.Effects;

namespace NaiveInkCanvas.Model.NewModels.Graphicses
{
    public class GraphicsTextModel : GraphicsRectSizeBase
    {
        public string Text { get; set; }
        public CanvasTextFormat TextFormat { get; set; }
        public GraphicsTextModel(PenModel penModel,IGraphicsDraw draw) 
            : base( GraphicsTypes.Text,penModel,draw)
        {

        }
        public override void BeginDraw(InkPoint point,PointerPoint pointer)
        {
            base.BeginDraw(point, pointer);
            Text= Drawer.Text;
            TextFormat = Drawer.TextFormat;
        }

        public override void Draw(ICanvasResourceCreator creator, CanvasDrawingSession ds)
        {
            ds.DrawText(Text, Bounds, PenAttribute.GetBrush(creator), TextFormat);
        }
    }
}
