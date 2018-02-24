using NaiveInkCanvas.Pens.PenDefs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;

namespace NaiveInkCanvas.Pens.SystemPens
{
    public class EraserGraphics : PenBase
    {
        public EraserGraphics(IGraphicsDraw drawer, PensManagerBase pensManager) 
            : base(drawer, pensManager, "图形擦")
        {

        }
        public override Action PenChanged => () => Drawer.PenType = GraphicsTypes.EraserGraphics;
    }
}
