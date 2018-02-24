using NaiveInkCanvas.Model.NewModels.Layer.LayerDefs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Input;
using Windows.UI.Input.Inking;

namespace NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines
{
    public interface IGraphicsOperator
    {
        /// <summary>
        /// 鼠标是否在图形上方
        /// 用来右键选择图像,默认不在
        /// </summary>
        /// <returns></returns>
        bool IsPointOn(InkPoint  ipoint,PointerPoint point);
        /// <summary>
        /// 是否可操作此图形
        /// </summary>
        /// <returns></returns>
        bool CanOperation();
        /// <summary>
        /// 图像移动
        /// </summary>
        /// <param name="point"></param>
        bool Move(InkPoint  ipoint,PointerPoint point);
        /// <summary>
        /// 图形开始移动
        /// </summary>
        /// <param name="point"></param>
        void BeginMove(InkPoint  ipoint,PointerPoint point);
    }
}
