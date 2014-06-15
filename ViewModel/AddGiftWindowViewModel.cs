using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Gifter.Command;
using System.ComponentModel;

namespace Gifter.ViewModel
{
    public class AddGiftWindowViewModel : INotifyPropertyChanged
    {
        public GiftViewModel Gift { get; set; }
        public AddGiftWindowViewModel()
        {
            CancelCommand = new AddGiftCancelCommand(this);
            SaveCommand = new AddGiftSaveCommand(this);
            LoadCommand = new AddGiftLoadCommand(this);
            Gift = new GiftViewModel();
        }

        public AddGiftWindowViewModel(DAL.GiftRepository _giftrepo) : this()
        {
            GiftRepo = _giftrepo;
        }
        
        public ICommand CancelCommand { get;private set; }
        public ICommand SaveCommand { get;private set; }
        public ICommand LoadCommand { get;private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public DAL.GiftRepository GiftRepo { get; set; }
    }
}
