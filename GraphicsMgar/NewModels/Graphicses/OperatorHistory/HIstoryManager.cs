using NaiveInkCanvas.Model.NewModels.Graphicses.OperatorHistory.HistoryDefs;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Model.NewModels.Graphicses.OperatorHistory
{
    public class HistoryManager:IHistoryManagerSharp
    {
        public ObservableCollection<IHistorySharp> HistoryCollection { get; }
        public ObservableCollection<IHistorySharp> RedoCollection { get; }

        public bool CanGoback => HistoryCollection.Count > 0;

        public bool CanGoahead => RedoCollection.Count > 0;

        public HistoryManager()
        {
            HistoryCollection = new ObservableCollection<IHistorySharp>();
            RedoCollection = new ObservableCollection<IHistorySharp>();
        }     

        public IHistorySharp PopHistory()
        {
            if (CanGoback)
            {
                var hist = HistoryCollection[HistoryCollection.Count - 1];
                HistoryCollection.Remove(hist);
                RedoCollection.Add(hist);
                return hist;
            }
            return null;
        }

        public IHistorySharp PopRedo()
        {
            if (CanGoahead)
            {
                var redo = RedoCollection[RedoCollection.Count - 1];
                RedoCollection.Remove(redo);
                HistoryCollection.Add(redo);
                return redo;
            }
            return null;
        }

        public void PushHistory(IHistorySharp history)
        {
            HistoryCollection.Add(history);
        }

        public void PushRedo(IHistorySharp history)
        {
            RedoCollection.Add(history);
        }
    }
}
