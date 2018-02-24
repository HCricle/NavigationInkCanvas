using NaiveInkCanvas.Pens.PenDefs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Pens
{
    public class PenManager : IPenManager
    {
        private static Dictionary<string, PenManager> PensCollection { get; }
        static PenManager()
        {
            PensCollection = new Dictionary<string, PenManager>();
        }
        public static PenManager RegiestPens(string name)
        {
            Debug.Assert(PensCollection.Where(m => m.Key == name).Count() == 0);
            var pm = new PenManager();
            PensCollection.Add(name,pm);
            return pm;
        }
        public static void UnRegiestPens(string name)
        {
            Debug.Assert(PensCollection.Where(m => m.Key == name).Count() == 1);
            PensCollection.Remove(name);
        }

        private PenManager()
        {
            PenCollection = new ObservableCollection<PenBase>();
        }        
        public ObservableCollection<PenBase> PenCollection { get; }
        public void RegiestPen(PenBase pen)
        {
            Debug.Assert(PenCollection.Where(p => p == pen).Count() == 0);
            PenCollection.Add(pen);
        }
        public void UnRegiestPen(PenBase pen)
        {
            Debug.Assert(PenCollection.Where(p => p == pen).Count() == 1);
            PenCollection.Remove(pen);
        }        
        public void CleanPens()
        {
            PenCollection.Clear();
        }
    }
}
