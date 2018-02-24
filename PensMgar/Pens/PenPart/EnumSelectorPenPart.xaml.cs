using NaiveInkCanvas.Model.NewModels.LayerDefines;
using NaiveInkCanvas.Pens.PenPart.PenPartDefs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public sealed partial class EnumSelectorPenPart : PenPartControlBase
    {
        public EnumSelectorPenPart(IGraphicsDraw drawer, string propath, string text,IValueConverter converter,Type enumtype)
            : base(drawer, propath,text)
        {
            InitializeComponent();
            Items = new ObservableCollection<string>();
            ValueConverter = converter;
            foreach (var item in Enum.GetValues(enumtype))
            {
                Items.Add(Enum.GetName(enumtype,item));
            }
        }
        public ObservableCollection<string> Items { get; }
        private void PenPartControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            CmbEnum?.SetBinding(ComboBox.SelectedItemProperty, PropBinding);
        }
        public override void UpdataBinding(object source)
        {
            base.UpdataBinding(source);
            CmbEnum?.SetBinding(ComboBox.SelectedItemProperty, PropBinding);
        }
    }
}
