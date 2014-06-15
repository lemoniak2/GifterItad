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

namespace Gifter.Command
{
    public class RandomCommand : ICommand
    {
        private ViewModel.MainWindowViewModel _mainWindowViewModel;
        private int _count;
        private int _lenght;
        private Importer _importer;
        public RandomCommand(ViewModel.MainWindowViewModel mainWindowViewModel)
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
            _importer = new Importer(_mainWindowViewModel.PathCSV);
            _mainWindowViewModel.ImgVisibility = Visibility.Hidden;
            _mainWindowViewModel.TileVisibility = Visibility.Visible;
            _mainWindowViewModel.DrawEnable = false;
            if (_mainWindowViewModel.PathCSV == null)
            {
                Randomize();
            }
            else
            {
                _mainWindowViewModel.DescVisibility = Visibility.Hidden;
                _mainWindowViewModel.WinnerVisibility = Visibility.Visible;
                _mainWindowViewModel.WinnerDataVisibility = Visibility.Visible;
                RandomizeFromFile();
            }
        }
        public void Randomize()
        {
            _count = 0;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            _count++;
            if (_count <= 40)
            {
                Random rnd = new Random();
                _mainWindowViewModel.RandomValue = rnd.Next(1, 300).ToString();
            }
            else
            {
                _mainWindowViewModel.WinnerText = "WYGRAŁ";
                _mainWindowViewModel.WinnerColor = new SolidColorBrush(Color.FromArgb(204, 31, 199, 31));
                _mainWindowViewModel.WinnerVisibility = Visibility.Visible;
                _mainWindowViewModel.DescVisibility = Visibility.Hidden;
                _mainWindowViewModel.DrawEnable = false;
                (sender as DispatcherTimer).Stop();
            }
        }
        public void RandomizeFromFile()
        {
            _lenght = File.ReadAllLines(_mainWindowViewModel.PathCSV).Length;
            _count = 0;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick1);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Start();
        }
        void timer_Tick1(object sender, EventArgs e)
        {
            _count++;
            if (_count <= 40)
            {
                Random rnd = new Random();
                _mainWindowViewModel.RandomValue = rnd.Next(1, _lenght+1).ToString();
            }
            else
            {
                _mainWindowViewModel.WinnerText = "";
                _mainWindowViewModel.WinnerColor = new SolidColorBrush(Color.FromArgb(204, 31, 199, 31));
                (sender as DispatcherTimer).Stop();
            }
            _mainWindowViewModel.WinnerData = _importer.GetPersonById(int.Parse(_mainWindowViewModel.RandomValue) - 1).ToString();
        }
    }
}
