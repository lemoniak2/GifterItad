using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Gifter.Command;
using System.ComponentModel;
using Gifter.DataOperator;
using System.IO;

namespace Gifter.ViewModel
{
    public class AddGiftWindowViewModel : INotifyPropertyChanged
    {
        private string _currentpath;
        public GiftViewModel Gift { get; set; }
        public AddGiftWindowViewModel()
        {
            _currentpath = Path.GetFullPath(@".\");
            CancelCommand = new RelayCommand(() =>
                {
                    // Tutaj bedzie zamykanie okna, tylko jak?
                });
            SaveCommand = new RelayCommand(() =>
                {
                    Importer p = new Importer();
                    Gift.ImageUrl = p.CopyFileName(Gift.ImageUrl, _currentpath);
                    GiftRepo.Create(Gift);
                    //zamykanie
                }, (object p) =>
                {
                    if (String.IsNullOrEmpty(Gift.Name) | String.IsNullOrEmpty(Gift.Description) | String.IsNullOrEmpty(Gift.ImageUrl))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                });

            LoadCommand = new RelayCommand(() =>
                {
                    Gift.ImageUrl = new Dialog().OpenDialog2();
                });
            Gift = new GiftViewModel()
                {
                    Name = "Podaj nazwe...",
                    Description = "Podaj opis...",
                };
        }

        public AddGiftWindowViewModel(DAL.GiftRepository _giftrepo)
            : this()
        {
            GiftRepo = _giftrepo;
        }

        public ICommand CancelCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }
        public bool IsOpened { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public DAL.GiftRepository GiftRepo { get; set; }
    }
}
