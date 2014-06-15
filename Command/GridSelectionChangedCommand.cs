using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;
using System.IO;
using MahApps.Metro.Controls;
using System.Windows;
using Gifter.DataOperator;
using System.Windows.Media;
using System.Windows.Controls;

namespace Gifter.Command
{
    public class GridSelectionChangedCommand : ICommand
    {
        private ViewModel.MainWindowViewModel _mainWindowViewModel;

        public GridSelectionChangedCommand(ViewModel.MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }
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
        public void Execute(object parameter)
        {
            DataGrid dgGifts = (parameter as DataGrid);
            
            if (dgGifts.SelectedIndex == -1)
            {
                dgGifts.SelectedIndex = _mainWindowViewModel.PreviousSelected;
            }
            _mainWindowViewModel.PreviousSelected = dgGifts.SelectedIndex;
            _mainWindowViewModel.ImgVisibility = Visibility.Visible;
            _mainWindowViewModel.TileVisibility = Visibility.Hidden;
            _mainWindowViewModel.WinnerText = "";
            _mainWindowViewModel.WinnerVisibility = Visibility.Hidden;
            _mainWindowViewModel.DescVisibility = Visibility.Visible;
            _mainWindowViewModel.DrawEnable = true;
            _mainWindowViewModel.WinnerDataVisibility = Visibility.Hidden;
            _mainWindowViewModel.WinnerData = "";
            _mainWindowViewModel.WinnerColor = new SolidColorBrush(Color.FromArgb(204, 17, 158, 218));
        }
    }
}
