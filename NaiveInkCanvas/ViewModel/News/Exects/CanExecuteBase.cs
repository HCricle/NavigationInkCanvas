using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.ViewModel.News.Exects
{
    public abstract class CanExecuteBase : ICanExecute
    {
        public abstract void Execute();
        public virtual bool CanExecute()
        {
            return true;
        }
    }
}
