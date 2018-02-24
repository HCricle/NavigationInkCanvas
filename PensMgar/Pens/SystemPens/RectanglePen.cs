using NaiveInkCanvas.Pens.PenDefs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using NaiveInkCanvas.Pens.PenPart;
using Windows.Foundation;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;

namespace NaiveInkCanvas.Pens.SystemPens
{
    public class RectanglePen : PenBase
    {
        public RectanglePen(IGraphicsDraw drawer, PensManagerBase pensManager)
            : base(drawer, pensManager, "正方形")
        {
            PenParts.Add(new SliderPenPart(drawer, "LocPenModel.StrokeWidth", "笔宽", new Rect(0, 0, 100, 100)));
            PenParts.Add(new SliderPenPart(drawer, "LocPenModel.Rotation", "旋转度", new Rect(0, 0, 360, 360)));
            PenParts.Add(new ToggerPenPart(drawer, "IsFill", "填充"));
        }
        public override Action PenChanged => ()=>Drawer.PenType = GraphicsTypes.Rectangle;
    }
}
