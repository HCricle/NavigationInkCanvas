using NaiveInkCanvas.Model.NewModels.Graphicses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace GraphicsMgar.NewModels.LayerDefines
{
    public interface ICanvasSelector
    {        
        /// <summary>
        /// 根据所选点选择图像，并且根据colError计算颜色差度
        /// </summary>
        /// <param name="colError">最大颜色差度</param>
        /// <returns>所选的矩形</returns>
        Rect SelectRectByPoint(Point point,int colError);
        /// <summary>
        /// 根据所选点选择图像描边，并且根据colError计算颜色差度
        /// </summary>
        /// <param name="colError">最大颜色差度</param>
        /// <returns>所选点的外围接近颜色点集合</returns>
        Vector2[] SelectPolygonByPoint(Point point, int colError);
    }
}
