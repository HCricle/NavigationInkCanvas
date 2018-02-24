using NaiveInkCanvas.Model.NewModels.LayerDefines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace NaiveInkCanvas.Pens.PenPart.PenPartDefs
{
    public interface IPenPartSharp
    {
        string Text { get; }
        IGraphicsDraw Drawer { get; set; }
        object Sender { get; set; }
        string Property { get; }
        IPenPartControl Control { get; }
        Rect Range { get; }
    }
}
