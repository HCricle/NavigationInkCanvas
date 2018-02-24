using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml.Media;

namespace InkBtnManager.Model.Base
{
    interface IInkCustomPenControl
    {
        BrushCollection Brushs { get; set; }
        InkDrawingAttributes PenAttributes { get; set; }
    }
}
