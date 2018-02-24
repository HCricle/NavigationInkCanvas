using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace InkBtnManager.Model.Base
{
    public class InkBtnPenButtonBase:InkToolbarCustomPenButton
    {
        public InkBtnPenButtonBase(InkBtnPenBase pen)
        {
            CustomPen = pen;
            Palette = pen.Brushs;
            InkBtnBuilder.ocPens.Add(this);
        }
    }
}
