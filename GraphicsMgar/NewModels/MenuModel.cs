using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace NaiveInkCanvas.Model.NewModels
{
    public class MenuModel
    {
        public string Text { get; set; }
        public Symbol Ico { get; set; }
        public ICommand Command { get; set; }
    }
}
