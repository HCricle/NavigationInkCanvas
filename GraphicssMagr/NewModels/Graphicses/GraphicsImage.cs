using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using NaiveInkCanvas.Model.NewModels.LayerDefines;

namespace NaiveInkCanvas.Model.NewModels.Graphicses
{
    /// <summary>
    /// 暂时不用这个
    /// </summary>
    public class GraphicsImage : GraphicsRectSizeBase
    {
        public GraphicsImage(GraphicsTypes graphics, PenModel penModel, IGraphicsDraw draw)
            : base(GraphicsTypes.Image, penModel, draw)
        {
        }
        public override void Draw(ICanvasResourceCreator creator, CanvasDrawingSession session)
        {

        }
    }
}
