using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace NaiveInkCanvas.Pens.PenPart.PenPartDefs
{
    public class PenPartSharp : IPenPartSharp
    {
        public PenPartSharp()
        {
            
        }
        public string Text { get; }

        public IGraphicsDraw Drawer { get; set; }

        public Rect Range { get; }

        public IPenPartControl Control { get; }

        public object Sender { get; set; }

        string IPenPartSharp.Property { get; }
    }
}
