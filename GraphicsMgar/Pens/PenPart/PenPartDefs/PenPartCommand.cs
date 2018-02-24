using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NaiveInkCanvas.Pens.PenPart.PenPartDefs
{
    public class PenPartCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public Action<object> ExecuteAction { get; }
        public Func<object, bool?> CanExecuteAction { get; }
        public PenPartCommand(Action<object> execute)
        {
            ExecuteAction = execute;
        }
        public PenPartCommand(Action<object> execute, Func<object, bool?> canExecute)
            :this(execute)
        {
            CanExecuteAction = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            var b = CanExecuteAction?.Invoke(parameter);
            if (b == false)
                return false;
            else
                return true;
        }

        public void Execute(object parameter)
        {
            ExecuteAction?.Invoke(parameter);
        }
    }
}
