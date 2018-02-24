using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Model.NewModels.Graphicses.OperatorHistory.HistoryDefs
{
    public class HistoryBase:IHistorySharp
    {
        public Action Revocation { get; set; }
        public Action Redo { get; set; }
        public string Description { get; set; }        
    }
}
