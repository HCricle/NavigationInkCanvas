using NaiveInkCanvas.Model.NewModels.Graphicses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Model.NewModels.LayerDefines
{
    public interface IGraphics
    {
        List<GraphicsRectSizeBase> GraphicsRects { get; }
    }
}
