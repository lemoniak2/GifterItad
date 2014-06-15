using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Gifter.View;

namespace Gifter.Command
{
    class AddGiftCancelCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        private ViewModel.AddGiftWindowViewModel addGiftWindowViewModel;

        public AddGiftCancelCommand(ViewModel.AddGiftWindowViewModel addGiftWindowViewModel)
        {
            // TODO: Complete member initialization
            this.addGiftWindowViewModel = addGiftWindowViewModel;
        }

        public void Execute(object parameter)
        {
            (parameter as AddGiftWindow).Close();
        }
    }
}
