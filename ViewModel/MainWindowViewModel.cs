using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Gifter.Command;
using System.Windows;
using System.Windows.Media;
using Gifter.DAL;
using System.IO;
using Gifter.DataOperator;
using System.Windows.Threading;

namespace Gifter.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            _giftrepo = new GiftRepository(@".\..\..\Data\Reward\Gifts.xml");
            Gifts = new ObservableCollection<GiftViewModel>();
            Refresh();
            RandomCommand = new RelayCommand(() =>
            {
                _importer = new Importer(PathCSV);
                ImgVisibility = Visibility.Hidden;
                TileVisibility = Visibility.Visible;
                DrawEnable = false;
                if (PathCSV == null)
                {
                    Randomize();
                }
                else
                {
                    DescVisibility = Visibility.Hidden;
                    WinnerVisibility = Visibility.Visible;
                    WinnerDataVisibility = Visibility.Visible;
                    RandomizeFromFile();
                }
            });
            FromCSVCommand = new CSVCommand(this);
            GridSelectionChangedCommand = new GridSelectionChangedCommand(this);
            AddGift = new AddGiftCommand(this, _giftrepo);
            DeleteGift = new DeleteGiftCommand(this, _giftrepo);
        }
        private GiftRepository _giftrepo;
        public ICommand AddGift { get; private set; }
        public ICommand DeleteGift { get; private set; }
        public void Refresh()
        {
            Gifts.Clear();
            foreach (var item in _giftrepo.GetAllGifts())
            {
                Gifts.Add(item);
            }
        }
        #region // Randomize with file or without
        public void RandomizeFromFile()
        {
            _lenght = File.ReadAllLines(PathCSV).Length;
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
                RandomValue = rnd.Next(1, _lenght + 1).ToString();
            }
            else
            {
                WinnerText = "";
                WinnerColor = new SolidColorBrush(Color.FromArgb(204, 31, 199, 31));
                (sender as DispatcherTimer).Stop();
            }
            WinnerData = _importer.GetPersonById(int.Parse(RandomValue) - 1).ToString();
        }
        public void Randomize()
        {
            _count = 0;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            _count++;
            if (_count <= 50)
            {
                Random rnd = new Random();
                RandomValue = rnd.Next(1, GenerateNumberMax).ToString();
            }
            else
            {
                WinnerText = "WYGRAŁ";
                WinnerColor = new SolidColorBrush(Color.FromArgb(204, 31, 199, 31));
                WinnerVisibility = Visibility.Visible;
                DescVisibility = Visibility.Hidden;
                DrawEnable = false;
                (sender as DispatcherTimer).Stop();
            }
        }
        #endregion

        public GiftViewModel Gift { get; set; }
        public ICommand RandomCommand { get; private set; }
        public ICommand FromCSVCommand { get; set; }
        public ICommand GridSelectionChangedCommand { get; set; }
        public int GenerateNumberMax { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private int _previousSelected;
        public int PreviousSelected
        {
            get { return _previousSelected; }
            set { _previousSelected = value; OnPropertyChanged("PreviousSelected"); }
        }
        private string _winnerText;
        public string WinnerText
        {
            get { return _winnerText; }
            set { _winnerText = value; OnPropertyChanged("WinnerText"); }
        }
        private string _randomvalue;
        public string RandomValue
        {
            get { return _randomvalue; }
            set { _randomvalue = value; OnPropertyChanged("RandomValue"); }
        }
        private Visibility _imgVisibility;
        public Visibility ImgVisibility
        {
            get { return _imgVisibility; }
            set { _imgVisibility = value; OnPropertyChanged("ImgVisibility"); }
        }
        private Visibility _tileVisibility = Visibility.Hidden;
        public Visibility TileVisibility
        {
            get { return _tileVisibility; }
            set { _tileVisibility = value; OnPropertyChanged("TileVisibility"); }
        }
        private Visibility _descVisibility;
        public Visibility DescVisibility
        {
            get { return _descVisibility; }
            set { _descVisibility = value; OnPropertyChanged("DescVisibility"); }
        }
        private Visibility _winnerVisibility = Visibility.Hidden;
        public Visibility WinnerVisibility
        {
            get { return _winnerVisibility; }
            set { _winnerVisibility = value; OnPropertyChanged("WinnerVisibility"); }
        }
        private string _winnerData;
        public string WinnerData
        {
            get { return _winnerData; }
            set { _winnerData = value; OnPropertyChanged("WinnerData"); }
        }

        private SolidColorBrush _winnerColor = new SolidColorBrush(Color.FromArgb(204, 17, 158, 218));
        public SolidColorBrush WinnerColor
        {
            get { return _winnerColor; }
            set { _winnerColor = value; OnPropertyChanged("WinnerColor"); }
        }
        
        private Visibility _winnerDataVisibility = Visibility.Hidden;
        public Visibility WinnerDataVisibility
        {
            get { return _winnerDataVisibility; }
            set { _winnerDataVisibility = value; OnPropertyChanged("WinnerDataVisibility"); }
        }
        private bool _drawEnable = true;
        private int _count;
        private int _lenght;
        private Importer _importer;
        public bool DrawEnable
        {
            get { return _drawEnable; }
            set { _drawEnable = value; OnPropertyChanged("DrawEnable"); }
        }




        public string PathCSV { get; set; }
        public ObservableCollection<GiftViewModel> Gifts { get; set; }
        private void OnPropertyChanged(string name)
        {
            var p = PropertyChanged;
            if (p != null)
            {
                p(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
