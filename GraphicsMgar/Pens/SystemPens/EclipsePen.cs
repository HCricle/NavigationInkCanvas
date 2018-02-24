using NaiveInkCanvas.Pens.PenDefs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using NaiveInkCanvas.Pens.PenPart;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using Windows.Foundation;

namespace NaiveInkCanvas.Pens.SystemPens
{
    public class EclipsePen : PenBase
    {
        public EclipsePen(IGraphicsDraw drawer, PensManagerBase pensManager) 
            : base(drawer, pensManager, "圆形笔")
        {
            PenParts.Add(new SliderPenPart(drawer, "LocPenModel.StrokeWidth", "笔宽", new Rect(0, 0, 100, 100)));
            PenParts.Add(new ToggerPenPart(drawer, "IsFill", "填充"));
        }
        public override Action PenChanged => () => Drawer.PenType = GraphicsTypes.Ellipse;
    }
}
