using NaiveInkCanvas.Pens.PenDefs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using NaiveInkCanvas.Pens.PenPart;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;

namespace NaiveInkCanvas.Pens.SystemPens
{
    public class SelectPen : PenBase
    {
        public SelectPen(IGraphicsDraw drawer, PensManagerBase pensManager) 
            : base(drawer, pensManager, "选择笔")
        {
            PenParts.Add(new ToggerPenPart(Drawer, "IsLock", "锁定图层"));
        }
        public override Action PenChanged => () => Drawer.PenType = GraphicsTypes.SelectObject;

    }
}
