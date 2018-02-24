using NaiveInkCanvas.Model.NewModels.LayerDefines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Model.NewModels.Graphicses.GraphicsDefines
{
    public interface IGraphicsOperation
    {
        void Revocate(IGraphicsDraw drawer);
        void Redo(IGraphicsDraw drawer,object data);
    }
}
