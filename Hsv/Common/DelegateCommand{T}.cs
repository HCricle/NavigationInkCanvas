﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hsv
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> commandExecuteAction;

        private readonly Func<T, bool> commandCanExecute;

        public DelegateCommand(Action<T> executeAction, Func<T, bool> canExecute = null)
        {
            if (executeAction == null)
            {
                throw new ArgumentNullException(nameof(executeAction));
            }

            commandExecuteAction = executeAction;
            commandCanExecute = canExecute ?? (e => true);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// The parameter used by the command.
        /// </param>
        /// <returns>
        /// Returns a value indicating whether this command can be executed.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            try
            {
                return commandCanExecute(ConvertParameterValue(parameter));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        /// The parameter used by the command.
        /// </param>
        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            try
            {
                commandExecuteAction(ConvertParameterValue(parameter));
            }
            catch
            {
                Debugger.Break();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private static T ConvertParameterValue(object parameter)
        {
            parameter = parameter is T ? parameter : Convert.ChangeType(parameter, typeof(T));
            return (T)parameter;
        }
    }
}
