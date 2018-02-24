using NaiveInkCanvas.Model.NewModels.Graphicses.OperatorHistory.HistoryDefs;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Model.NewModels.Graphicses.OperatorHistory.Histories
{
    class GraphicsHistory:HistoryBase
    {
        public object Graphics { get; set; }
        public IGraphicsDraw Drawer { get; set; }
    }
}
