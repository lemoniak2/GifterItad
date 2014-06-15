using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Gifter.DataOperator;

namespace Gifter.Command
{
    class AddGiftLoadCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        private ViewModel.AddGiftWindowViewModel addGiftWindowViewModel;

        public AddGiftLoadCommand(ViewModel.AddGiftWindowViewModel addGiftWindowViewModel)
        {
            this.addGiftWindowViewModel = addGiftWindowViewModel;
        }

        public void Execute(object parameter)
        {
            Dialog p = new Dialog();
            addGiftWindowViewModel.Gift.ImageUrl = p.OpenDialog2();
        }
    }
}
