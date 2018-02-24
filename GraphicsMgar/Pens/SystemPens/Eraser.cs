using NaiveInkCanvas.Pens.PenDefs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using Windows.Foundation;
using NaiveInkCanvas.Pens.PenPart;
using NaiveInkCanvas.Converter;
using Microsoft.Graphics.Canvas.Geometry;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;

namespace NaiveInkCanvas.Pens.SystemPens
{
    public class Eraser : PenBase
    {
        public Eraser(IGraphicsDraw drawer, PensManagerBase pensManager)
            : base(drawer, pensManager, "橡皮擦")
        {
            PenParts.Add(new SliderPenPart(drawer, "LocPenModel.StrokeWidth", "笔宽", new Rect(0, 0, 100, 100)));
            PenParts.Add(new ToggerPenPart(drawer, "IsFill", "填充"));
            PenParts.Add(new EnumSelectorPenPart(drawer, "LocFigureLoop", "闭合", new EnumStringConverter<CanvasFigureLoop>(), typeof(CanvasFigureLoop)));
        }
        public override Action PenChanged => () => Drawer.PenType = GraphicsTypes.Eraser;
    }

}
