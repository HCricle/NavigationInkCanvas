using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Model.NewModels
{
    public interface ICanObservation
    {
        event Action<object> ValueChanged;
    }
}
