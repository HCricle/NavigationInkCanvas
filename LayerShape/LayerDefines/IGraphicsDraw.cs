using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;

namespace NaiveInkCanvas.Model.NewModels.LayerDefines
{
    public interface IGraphicsDraw : IGraphics
    {
        CanvasFigureLoop LocFigureLoop { get; set; }
        GraphicsTypes PenType { get; set; }
        Rect CanvasSize { get; set; }//画布大小
        PenModel LocPenModel { get; set; }
        Color BackgroundColor { get; set; }
        bool OnlyDrawTmpPoints { get; set; }
        bool IsLock { get; set; }
        bool IsFill { get; set; }
        bool IsStartPointCenter { get; set; }
        string Text { get; set; }
        CanvasTextFormat TextFormat { get; }
    }
}
