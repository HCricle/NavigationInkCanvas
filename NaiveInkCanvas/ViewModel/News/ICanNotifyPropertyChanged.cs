using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.ViewModel.News
{
    public interface ICanNotifyPropertyChanged
    {
        void RaisePropertyChanged([CallerMemberName] string name = "");
    }
}
