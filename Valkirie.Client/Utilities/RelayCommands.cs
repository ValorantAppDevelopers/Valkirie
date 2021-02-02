using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Valkirie.Client.Utilities
{
    public class RelayCommands : ICommand
    {
        readonly Action<object> _execute;
        readonly Action<object, object> _executeTwoParams;
        readonly Action<object, Func<object, object>> _executeParamFunc;
        readonly Predicate<object> _canExecute;
        /// <summary>
        /// Constructer takes Execute events to register in CommandManager.
        /// </summary>
        /// <param name="execute">Execute method as action.</param>
        public RelayCommands(Action<object> execute)
            : this(execute, null)
        {
            try
            {
                if (null == execute)
                {
                    throw new NotImplementedException("Not implemented");
                }
                _execute = execute;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Constructer takes Execute and CanExcecute events to register in CommandManager.
        /// </summary>
        /// <param name="execute">Execute method as action.</param>
        /// <param name="canExecute">CanExecute method as return bool type.</param>
        public RelayCommands(Action<object> execute, Predicate<object> canExecute)
        {
            try
            {
                if (null == execute)
                {
                    _execute = null;
                    throw new NotImplementedException("Not implemented");
                }
                _execute = execute;
                _canExecute = canExecute;
            }
            catch (Exception)
            {
            }
        }

        public RelayCommands(Action<object, object> execute, Predicate<object> canExecute)
        {
            try
            {
                if (null == execute)
                {
                    _execute = null;
                    throw new NotImplementedException("Not implemented");
                }
                _executeTwoParams = execute;
                _canExecute = canExecute;
            }
            catch (Exception)
            {
            }
        }

        public RelayCommands(Action<object, Func<object, object>> execute, Predicate<object> canExecute)
        {
            try
            {
                if (null == execute)
                {
                    _execute = null;
                    throw new NotImplementedException("Not implemented");
                }
                _executeParamFunc = execute;
                _canExecute = canExecute;
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// Can Executed Changed Event
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        /// <summary>
        /// Execute method.
        /// </summary>
        /// <param name="parameter">Method parameter.</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void Execute(object param1, object param2)
        {
            _executeTwoParams(param1, param2);
        }

        public void Execute(object param, Func<object, object> func)
        {
            _executeTwoParams(param, func);
        }
        /// <summary>
        /// CanExecute method.
        /// </summary>
        /// <param name="parameter">Method parameter.</param>
        /// <returns>Return true if can execute.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }
        /// <summary>
        /// InvalidateCanExecute method will initiate validation of the Command.
        /// </summary>
        private void InvalidateCanExecute()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
