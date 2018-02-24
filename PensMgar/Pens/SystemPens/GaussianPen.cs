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
using NaiveInkCanvas.Converter;

namespace NaiveInkCanvas.Pens.SystemPens
{
    public class GaussianPen : PenBase
    {
        public GaussianPen(IGraphicsDraw drawer, PensManagerBase pensManager) 
            : base(drawer, pensManager, "高斯笔")
        {
            var sdp = new SliderPenPart(drawer, "LocPenModel.BlurAmount", "模糊程度", new Rect(0, 0, 100, 100))
            {
                ValueConverter = new FloatDoubleConverter()
            };
            PenParts.Add(sdp);
        }
        public override Action PenChanged =>()=> Drawer.PenType =GraphicsTypes.GaussianBlur;
    }
}
