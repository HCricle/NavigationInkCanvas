using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace InkBtnManager.Model.Base
{
    public class InkBtnToolBase:InkToolbarCustomToolButton
    {
        public InkBtnToolBase()
        {
            InkBtnBuilder.ocTool.Add(this);
        }
    }
}
