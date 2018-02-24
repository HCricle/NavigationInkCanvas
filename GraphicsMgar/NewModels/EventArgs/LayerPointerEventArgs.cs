using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input;
using Windows.UI.Xaml.Input;

namespace NaiveInkCanvas.Model.NewModels.EventArgs
{
    public class LayerPointerEventArgs
    {
        public PointerPoint Arg { get;}
        public bool BaseHandle { get; }
        public LayerPointerEventArgs(PointerPoint e,bool bs)
        {
            Arg = e;
            BaseHandle = bs;
        }
    }
}
