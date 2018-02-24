using NaiveInkCanvas.Model.NewModels.LayerDefines;
using NaiveInkCanvas.Pens.PenPart.PenPartDefs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace NaiveInkCanvas.Pens.PenDefs
{
    public interface IPenShape
    {
        string Name { get; }
        IGraphicsDraw Drawer { get; set; }
        ObservableCollection<IPenPartControl> PenParts { get; }
    }
}
