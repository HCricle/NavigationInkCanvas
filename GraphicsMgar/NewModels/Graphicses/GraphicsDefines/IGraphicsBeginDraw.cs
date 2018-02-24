using NaiveInkCanvas.Model.NewModels.Layer.LayerDefs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input;
using Windows.UI.Input.Inking;

namespace NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines
{
    public interface IGraphicsBeginDraw
    {
        void BeginDraw(InkPoint  point,PointerPoint pointer);
    }
}
