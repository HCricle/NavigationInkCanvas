using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;
using NaiveInkCanvas.Model.NewModels;
using Windows.Foundation;

namespace NaiveInkCanvas.Pens.PenPart.PenPartDefs
{
    public abstract class PenPartControlBase : UserControl, IPenPartControl
    {
        [Obsolete("此构造函数只能用于XAML文件中")]
        public PenPartControlBase()
        {

        }
        /// <summary>
        /// 生成实例
        /// </summary>
        /// <param name="drawer">图层实例</param>
        /// <param name="bindingpath">属性双向绑定路径</param>
        public PenPartControlBase(IGraphicsDraw drawer,string bindingpath,string text, Rect bounds)
            :this(drawer,bindingpath,text)
        {
            Bounds = bounds;
        }
        public PenPartControlBase(IGraphicsDraw drawer, string bindingpath, string text)
        {
            Debug.Assert(LayerObservated != null);
            Drawer = drawer;
            BindingPath = bindingpath;
            Text = text;
            UpdataBinding(Drawer);
            LayerObservated.ValueChanged += LayerObservated_ValueChanged;
            Drawer.PenModelChanged += OnPenModelChanged;
        }
        private void ThisPenModelChanged(PenModelChangedEventArgs e)
        {
            OnPenModelChanged(e);

        }
        public virtual void OnPenModelChanged(PenModelChangedEventArgs obj)
        {
            PenModelChanged?.Invoke(obj);
        }

        private void LayerObservated_ValueChanged(object obj)
        {
            if (obj is IGraphicsDraw drawer)
            {
                Drawer.PenModelChanged -= ThisPenModelChanged;
                Drawer = drawer;
                UpdataBinding(drawer);              
                Drawer.PenModelChanged += ThisPenModelChanged;
                LayerChanged?.Invoke(drawer);
            }
        }

        public static ICanObservation LayerObservated { get; set; }

        public IGraphicsDraw Drawer { get; set; }

        public Action<IPenPartControl, Exception> ErrorUpdata { get; set; }

        public Binding PropBinding { get; set; }

        public string BindingPath { get; }

        public object Value { get; set; }

        public Rect Bounds { get; set; }

        public UserControl Control => this;

        public string Text { get; }

        public IValueConverter ValueConverter { get; set; }

        public event Action<IGraphicsDraw> LayerChanged;

        public event Action<PenModelChangedEventArgs> PenModelChanged;
        public virtual void UpdataBinding(object source)
        {
            PropBinding = new Binding()
            {
                Path = new PropertyPath(BindingPath),
                Source = source,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Converter = ValueConverter
            };
        }

        public virtual void UpdataPainting(object newvalue)
        {
            try
            {
                Value = newvalue;              
            }
            catch (Exception ex)
            {
                ErrorUpdata?.Invoke(this, ex);
            }
        }

    }
}
