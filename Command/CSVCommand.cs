using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Gifter.DataOperator;

namespace Gifter.Command
{
    public class CSVCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return _mainWindowViewModel.PathCSV == null ? true : false;
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
        private ViewModel.MainWindowViewModel _mainWindowViewModel;

        public CSVCommand(ViewModel.MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public void Execute(object parameter)
        {
            _mainWindowViewModel.PathCSV = new Dialog().OpenDialog();
        }
    }
}
