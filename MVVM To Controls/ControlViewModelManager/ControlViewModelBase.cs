using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_To_Controls.ControlViewModelManager
{
    public abstract class ControlViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaiseProperterChanged([CallerMemberName] string name="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public void Set<T>(ref T space, T value, [CallerMemberName] string name = "")
        {
            space = value;
            RaiseProperterChanged(name);
        }
    }
}
