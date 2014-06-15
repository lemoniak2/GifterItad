﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Gifter.View;
using Gifter.DataOperator;
using System.Windows;

namespace Gifter.Command
{
    class AddGiftSaveCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        private ViewModel.AddGiftWindowViewModel addGiftWindowViewModel;

        public AddGiftSaveCommand(ViewModel.AddGiftWindowViewModel addGiftWindowViewModel)
        {
            this.addGiftWindowViewModel = addGiftWindowViewModel;
        }
        public void Execute(object parameter)
        {
            Dialog p = new Dialog();
            if (addGiftWindowViewModel.Gift.ImageUrl != null)
            {
                addGiftWindowViewModel.Gift.ImageUrl = @"\Images\" + p.CopyFileName(addGiftWindowViewModel.Gift.ImageUrl);
            } else addGiftWindowViewModel.Gift.ImageUrl = "";
            addGiftWindowViewModel.GiftRepo.Create(addGiftWindowViewModel.Gift);
            (parameter as AddGiftWindow).Close();
        }
    }
}
