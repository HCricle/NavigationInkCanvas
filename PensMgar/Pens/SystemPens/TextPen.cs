using NaiveInkCanvas.Pens.PenDefs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using Microsoft.Graphics.Canvas.Text;
using System.Reflection;
using NaiveInkCanvas.Pens.PenPart;
using Microsoft.Graphics.Canvas.Geometry;
using NaiveInkCanvas.Converter;
using Windows.Foundation;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;

namespace NaiveInkCanvas.Pens.SystemPens
{
    public class TextPen : PenBase
    {
        public TextPen(IGraphicsDraw drawer, PensManagerBase pensManager)
            : base(drawer, pensManager, "文字笔")
        {
            PenParts.Add(new InputBoxPenPart(Drawer, "Text", "内容"));
            PenParts.Add(new SliderPenPart(Drawer, "TextFormat.FontSize", "文字大小", new Rect(0, 0, 100, 100)));
        }
        public override Action PenChanged => () => Drawer.PenType = GraphicsTypes.Text;
    }
}
