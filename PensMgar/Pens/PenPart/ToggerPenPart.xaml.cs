using NaiveInkCanvas.Model.NewModels.LayerDefines;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace NaiveInkCanvas.Pens.PenPart
{
    public sealed partial class ToggerPenPart : PenPartControlBase
    {
        public ToggerPenPart(IGraphicsDraw drawer, string propath, string text)
            : base(drawer, propath,text, new Rect())
        {
            this.InitializeComponent();
        }
        public override void UpdataBinding(object source)
        {
            base.UpdataBinding(source);
            TgsSwitch?.SetBinding(ToggleSwitch.IsOnProperty, PropBinding);
        }
        private void PenPartControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            TgsSwitch?.SetBinding(ToggleSwitch.IsOnProperty, PropBinding);
        }
    }
}
