using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gifter.ViewModel;
using System.Windows.Input;

namespace Gifter.Command
{
    class DeleteGiftCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return parameter == null ? false : true;
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
        private ViewModel.MainWindowViewModel mainWindowViewModel;
        private DAL.GiftRepository _giftrepo;

        public DeleteGiftCommand(ViewModel.MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        public DeleteGiftCommand(ViewModel.MainWindowViewModel mainWindowViewModel, DAL.GiftRepository _giftrepo)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this._giftrepo = _giftrepo;
        }

        public void Execute(object parameter)
        {
            _giftrepo.Delete((parameter as GiftViewModel));
            mainWindowViewModel.Refresh();
        }
    }
}
