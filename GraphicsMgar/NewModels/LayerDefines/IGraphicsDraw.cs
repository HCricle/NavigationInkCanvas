using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines;
using NaiveInkCanvas.Model.NewModels.Graphicses.OperatorHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;

namespace NaiveInkCanvas.Model.NewModels.LayerDefines
{
    public interface IGraphicsDraw : IGraphics, ICanvasDraw
    {
        /// <summary>
        /// 线段是否闭合
        /// </summary>
        CanvasFigureLoop LocFigureLoop { get; set; }
        /// <summary>
        /// 画出的图形
        /// </summary>
        GraphicsTypes PenType { get; set; }
        /// <summary>
        /// 撤销重做管理
        /// </summary>
        HistoryManager HistoriesManager { get; }
        /// <summary>
        /// 画布大小
        /// </summary>
        Rect CanvasSize { get; set; }
        /// <summary>
        /// 正方形选取绘画
        /// </summary>
        Rect SelectCanvasSize { get; set; }
        /// <summary>
        /// 笔属性
        /// </summary>
        PenModel LocPenModel { get; set; }
        /// <summary>
        /// 背景颜色
        /// </summary>
        Color BackgroundColor { get; set; }
        /// <summary>
        /// 是否锁定图层
        /// </summary>
        bool IsLock { get; set; }
        /// <summary>
        /// 图形是否填充
        /// </summary>
        bool IsFill { get; set; }
        /// <summary>
        /// 是否为自定义形状
        /// </summary>
        bool IsOtherGraphics { get; set; }
        /// <summary>
        /// 绘画文本的数据
        /// </summary>
        string Text { get; set; }
        /// <summary>
        /// 文字图形的属性
        /// </summary>
        CanvasTextFormat TextFormat { get; set; }
        /// <summary>
        /// 笔属性新建
        /// </summary>
        event Action<PenModelChangedEventArgs> PenModelChanged;
        /// <summary>
        /// 更新图层
        /// </summary>
        void UpdataLayer();
    }
}
