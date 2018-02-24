using NaiveInkCanvas.Model.NewModels;
using NaiveInkCanvas.Model.NewModels.LayerDefines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Pens.PenDefs
{
    public abstract class PensManagerBase
    {
        public PensManagerBase()
        {
            Pens = new ObservableCollection<PenManager>();
        }
        private IGraphicsDraw drawer;

        public IGraphicsDraw Drawer
        {
            get => drawer;
            set
            {
                drawer = value;
                DrawerChanged?.Invoke(this, value);
            }
        }
        public ObservableCollection<PenManager> Pens { get;}

        public event Action<PensManagerBase, IGraphicsDraw> DrawerChanged;

        public PenManager RegiestPen(string name)
        {
            var pm = PenManager.RegiestPens(name);
            Pens.Add(pm);
            return pm;
        }
        public void InsetManager(PenManager manager)
        {
            Debug.Assert(Pens.IndexOf(manager) == -1);
            Pens.Add(manager);
        }
        public void RemoveManager(PenManager manager)
        {
            Debug.Assert(Pens.IndexOf(manager) != -1);
            Pens.Remove(manager);
        }
        public void CleanManager()
        {
            Pens.Clear();
        }
    }
}
