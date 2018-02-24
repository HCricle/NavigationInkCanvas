using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace InkBtnManager.Model.Base
{
    public class InkBtnToggleBase:InkToolbarCustomToggleButton
    {
       public InkBtnToggleBase()
        {
            InkBtnBuilder.ocToggles.Add(this);
        }
    }
}
