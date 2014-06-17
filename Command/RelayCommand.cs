using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Gifter.Command
{
    class RelayCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            if (_canexecute != null)
            {
                return _canexecute();
            }
            return true;
        }
        public RelayCommand(Action t, Func<bool> p)
            : this(t)
        {
            _canexecute = p;
        }
        public RelayCommand(Action t)
        {
            _execute = t;
        }
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
        private Action _execute;
        private Func<bool> _canexecute;
        public void Execute(object parameter)
        {
            if (_execute != null)
            {
                _execute();
            }
        }
    }
}
