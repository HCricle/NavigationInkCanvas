using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveInkCanvas.Pens.PenDefs
{
    public interface IPenManager
    {
        void RegiestPen(PenBase pen);
        void UnRegiestPen(PenBase pen);
        void CleanPens();
    
}
}
