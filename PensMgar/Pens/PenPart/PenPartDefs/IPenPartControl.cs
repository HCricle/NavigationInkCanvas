using NaiveInkCanvas.Model.NewModels.LayerDefines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace NaiveInkCanvas.Pens.PenPart.PenPartDefs
{
    public interface IPenPartControl
    {
        /// <summary>
        /// 目标控件
        /// </summary>
        IGraphicsDraw Drawer { get; set; }
        /// <summary>
        /// 绑定
        /// </summary>
        Binding PropBinding { get; set; }
        /// <summary>
        /// 绑定路径
        /// </summary>
        string BindingPath { get; }
        /// <summary>
        /// 双向绑定值
        /// </summary>
        object Value { get; set; }
        /// <summary>
        /// 范围
        /// </summary>
        Rect Bounds { get; set; }
        /// <summary>
        ///笔部分控件
        /// </summary>
        UserControl Control { get; }
        /// <summary>
        /// 显示的文本
        /// </summary>
        string Text { get; }
        /// <summary>
        /// 数据转换器
        /// </summary>
        IValueConverter ValueConverter { get; set; }
        /// <summary>
        /// 错误更新事件
        /// </summary>
        Action<IPenPartControl, Exception> ErrorUpdata { get; set; }
        /// <summary>
        /// 更新值
        /// </summary>
        /// <param name="newvalue"></param>
        void UpdataPainting(object newvalue);
        /// <summary>
        /// 更新绑定
        /// </summary>
        /// <param name="source"></param>
        void UpdataBinding(object source);        
    }
}
