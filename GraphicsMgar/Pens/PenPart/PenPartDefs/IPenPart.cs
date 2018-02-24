using NaiveInkCanvas.Model.NewModels.LayerDefines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace NaiveInkCanvas.Pens.PenPart.PenPartDefs
{
    [Obsolete]
    public interface IPenPart<TVALUE>
    {
        TVALUE Value { get; }
        event Action<TVALUE> ValueChanged;
        event Action<IPenPart<TVALUE>, TVALUE,Exception> ValueUpdataError;
        void ValueUpdata(TVALUE newvalue);        
    }
}
