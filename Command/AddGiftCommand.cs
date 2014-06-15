using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Gifter.View;
using Gifter.ViewModel;

namespace Gifter.Command
{
    class AddGiftCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
        private ViewModel.MainWindowViewModel mainWindowViewModel;
        private DAL.GiftRepository _giftrepo;

        public AddGiftCommand(ViewModel.MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        public AddGiftCommand(MainWindowViewModel mainWindowViewModel, DAL.GiftRepository _giftrepo)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this._giftrepo = _giftrepo;
        }

        public void Execute(object parameter)
        {
            AddGiftWindow p = new AddGiftWindow();
            p.DataContext = new AddGiftWindowViewModel(_giftrepo);
            p.ShowDialog();
            mainWindowViewModel.Refresh();
        }
    }
}
