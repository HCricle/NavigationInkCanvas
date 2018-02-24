using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace InkBtnManager.Model.Base
{
    public class InkBtnPenBase : InkToolbarCustomPen, IInkCustomPenControl
    {
        public BrushCollection Brushs { get; set; } = new BrushCollection();
        public InkDrawingAttributes PenAttributes { get; set; }
        public Size PenSize { get; set; }
        public InkBtnPenBase(InkDrawingAttributes attr, IEnumerable<Brush> brushs)
        {
            if (brushs != null)
                foreach (var item in brushs)
                {
                    Brushs.Add(item);
                }
            PenAttributes = attr;
        }
        protected override InkDrawingAttributes CreateInkDrawingAttributesCore(Brush brush, double strokeWidth)
        {
            PenAttributes.Color = ((SolidColorBrush)brush).Color;
            if (PenSize == null)
                PenAttributes.Size = new Size(strokeWidth, strokeWidth);
            else
                PenAttributes.Size = PenSize;
            return PenAttributes;
        }
    }
}
