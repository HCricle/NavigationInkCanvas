using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Model.NewModels.Graphicses.OperatorHistory.HistoryDefs
{
    public interface IHistoryManagerSharp
    {
        /// <summary>
        /// 能不能撤销
        /// </summary>
        bool CanGoback { get; }
        /// <summary>
        /// 能不能重做
        /// </summary>
        bool CanGoahead { get; }
        ObservableCollection<IHistorySharp> HistoryCollection { get; }
        ObservableCollection<IHistorySharp> RedoCollection { get; }

        void PushHistory(IHistorySharp history);
        IHistorySharp PopHistory();
        void PushRedo(IHistorySharp history);
        IHistorySharp PopRedo();
    }
}
