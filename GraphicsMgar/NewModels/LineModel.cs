using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input.Inking;

namespace NaiveInkCanvas.Model.NewModels
{
    public class LineModel
    {
        public List<InkPoint> Points { get; }
        public CanvasFigureLoop Figure { get; set; }
        public bool IsDraw { get; set; } = true;
        public LineModel(List<InkPoint> list)
        {
            Points = list;
        }
    }
}
