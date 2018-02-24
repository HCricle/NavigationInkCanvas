using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Model.NewModels.Graphicses.OperatorHistory.HistoryDefs
{
    public interface IHistorySharp
    {
        string Description { get; set; }
        Action Revocation { get; set; }
        Action Redo { get; set; }
    }
}
