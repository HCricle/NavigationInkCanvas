using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using Windows.UI.Input;
using Windows.UI.Input.Inking;
using Microsoft.Graphics.Canvas.Geometry;

namespace NaiveInkCanvas.Model.NewModels.Graphicses
{
    public class GraphicsScanModel : GraphicsRectSizeBase
    {
        /// <summary>
        /// 暂时不用
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="penModel"></param>
        /// <param name="draw"></param>
        public GraphicsScanModel(GraphicsTypes graphics, PenModel penModel, IGraphicsDraw draw)
            : base( GraphicsTypes.Scan, penModel, draw)
        {

        }
    }
}
