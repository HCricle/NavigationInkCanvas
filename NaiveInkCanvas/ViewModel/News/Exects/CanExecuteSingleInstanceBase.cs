using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.ViewModel.News.Exects
{
    public abstract class CanExecuteSingleInstanceBase<TCLASS>:CanExecuteBase
        where TCLASS:new()
    {
        private static TCLASS Instance = default(TCLASS);
        public static TCLASS Default => 
            Instance == null ? (Instance = new TCLASS()) : Instance;
        internal CanExecuteSingleInstanceBase() { }
    }
}
