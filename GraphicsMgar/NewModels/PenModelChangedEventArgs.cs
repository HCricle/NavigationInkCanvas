using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace NaiveInkCanvas.Model.NewModels
{
    public class PenModelChangedEventArgs
    {
        public PenModel NewPenModel { get; }
        public bool Handle { get; set; }
        public PenModelChangedEventArgs(PenModel pm)
        {
            NewPenModel = pm;
        }
    }
}
