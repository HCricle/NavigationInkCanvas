using NaiveInkCanvas.Pens.PenPart.PenPartDefs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Reflection;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using System.Diagnostics;
using NaiveInkCanvas.Model.NewModels;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace NaiveInkCanvas.Pens.PenPart
{
    public sealed partial class SliderPenPart :PenPartControlBase
    {
        public SliderPenPart(IGraphicsDraw drawer, string propath,string text, Rect bouds)
            : base(drawer, propath,text, bouds)
        {
            this.InitializeComponent();
        }

        public override void UpdataBinding(object source)
        {
            base.UpdataBinding(source);
            SdrValue?.SetBinding(Slider.ValueProperty, PropBinding);
            Bindings?.Update();
        }
        public override void OnPenModelChanged(PenModelChangedEventArgs obj)
        {
            if (!obj.Handle)
            {
                var oldValue = SdrValue.Value;
                UpdataBinding(Drawer);
                Value = oldValue;
                obj.Handle = true;
            }
        }
        private void PenPartControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            SdrValue?.SetBinding(Slider.ValueProperty, PropBinding);

            //TODO      :FIX History System,PenPart System.
        }
    }
}
