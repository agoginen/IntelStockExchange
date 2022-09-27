using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SE_Services.ViewModels;
using StockExchangePresentation.StockExchangeServices;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace StockExchangePresentation.ViewModel
{
	public class AdminViewModel : ViewModelBase
    {
        private readonly Timer _refreshTicker;
        private static object _lock = new object();

        private string _marketOpenTime { get; set; }
        public string MarketOpenTime 
        {
			get
			{
                return _marketOpenTime;
			}
			set
			{
                _marketOpenTime = value;
			}
        }
        private string _marketCloseTime { get; set; }
        public string MarketCloseTime 
        {
			get
			{
                return _marketCloseTime;

            }
			set
			{
                _marketCloseTime = value;
			}
        }

        private bool _monday { get; set; }
        public bool Monday 
        {
			get
			{
                return _monday;
			}
			set
			{
                _monday = value;
			}
        }
        private bool _tuesday { get; set; }
        public bool Tuesday
        {
            get
            {
                return _tuesday;
            }
            set
            {
                _tuesday = value;
            }
        }
        private bool _wednesday { get; set; }
        public bool Wednesday
        {
            get
            {
                return _wednesday;
            }
            set
            {
                _wednesday = value;
            }
        }
        private bool _thursday { get; set; }
        public bool Thursday
        {
            get
            {
                return _thursday;
            }
            set
            {
                _thursday = value;
            }
        }

        private bool _friday { get; set; }
        public bool Friday
        {
            get
            {
                return _friday;
            }
            set
            {
                _friday = value;
            }
        }

        private bool _saturday { get; set; }
        public bool Saturday
        {
            get
            {
                return _saturday;
            }
            set
            {
                _saturday = value;
            }
        }

        private bool _sunday { get; set; }
        public bool Sunday
        {
            get
            {
                return _sunday;
            }
            set
            {
                _sunday = value;
            }
        }

        private Stock Stock;
        private ObservableCollection<StockViewModel> _stocks;
        public ObservableCollection<StockViewModel> Stocks
        {
            get
            {
                return _stocks;
            }
			set
			{
                _stocks = value;
			}
        }
        public ICommand AddStockCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public Window AdminWindow { get; set; }
        public ICommand AddMarketTimingsCommand { get; set; }
        public ICommand AddMarketDaysCommand { get; set; }

        public string StockName
        {
            get
            {
                return Stock.StockName;
            }
            set
            {
                Stock.StockName = value;
            }
        }

        public decimal StockPrice
        {
            get
            {
                return Stock.Price;
            }
            set
            {
                Stock.Price = value;
            }
        }

        public int StockVolume
        {
            get
            {
                return Stock.Volume;
            }
            set
            {
                Stock.Volume = value;
            }
        }

        public AdminViewModel()
        {
            AssignMarketTimings();
            Stock = new Stock();
            this._stocks = new ObservableCollection<StockViewModel>();
            BindingOperations.EnableCollectionSynchronization(_stocks, _lock);
            _refreshTicker = new Timer(RefreshStocks, null, 0, 1000);
            AddStockCommand = new RelayCommand(AddStock);
            LogoutCommand = new RelayCommand(Logout);
            AddMarketTimingsCommand = new RelayCommand(AddMarketTimings);
            AddMarketDaysCommand = new RelayCommand(AddMarketDays);
        }

        /// <summary>
        /// Adds Market Timings to database
        /// </summary>
        public void AddMarketTimings()
		{
            StockExchangeOrderClient client = new StockExchangeOrderClient();
            client.UpdateMarketTimings(_marketOpenTime,_marketCloseTime);
            client.Close();
            AssignMarketTimings();
        }

        /// <summary>
        /// Adds Market Days to Database
        /// </summary>
        public void AddMarketDays()
		{
            StockExchangeOrderClient client = new StockExchangeOrderClient();
            client.UpdateMarketDays(_monday,_tuesday,_wednesday,_thursday,_friday,_saturday,_sunday);
            client.Close();
            AssignMarketTimings();
        }

        /// <summary>
        /// Assign Market Timings on load
        /// </summary>
        public void AssignMarketTimings()
		{
            StockExchangeOrderClient client = new StockExchangeOrderClient();
            var marketTimings = client.GetAllMarketTimings();
            _marketOpenTime = marketTimings.First()
                                            .StartTime
                                            .ToString();
            _marketCloseTime = marketTimings.First()
                                            .CloseTime
                                            .ToString();
            _monday = marketTimings.FirstOrDefault(x => x.Day == "Monday")
                                   .IsActive;
            _tuesday = marketTimings.FirstOrDefault(x => x.Day == "Tuesday")
                                   .IsActive;
            _wednesday = marketTimings.FirstOrDefault(x => x.Day == "Wednesday")
                                   .IsActive;
            _thursday = marketTimings.FirstOrDefault(x => x.Day == "Thursday")
                                   .IsActive;
            _friday = marketTimings.FirstOrDefault(x => x.Day == "Friday")
                                   .IsActive;
            _saturday = marketTimings.FirstOrDefault(x => x.Day == "Saturday")
                                   .IsActive;
            _sunday = marketTimings.FirstOrDefault(x => x.Day == "Sunday")
                       .IsActive;
        }

        /// <summary>
        /// Adds stock to Database
        /// </summary>
        public async void AddStock()
		{
			try
			{
                StockExchangeOrderClient client = new StockExchangeOrderClient();
                await client.AddStockAsync(Stock);
                client.Close();
                LoadStocks();
            }
            catch (Exception ex)
			{
                MessageBox.Show("Adding stock " + Stock.StockName + " to database has failed. Please contact administrator if problem persists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Loads Stock Data Grid - one time action
        /// </summary>
        public void LoadStocks()
		{
            _stocks.Clear();
            StockExchangeOrderClient client = new StockExchangeOrderClient();
            var allStocks = client.GetAllStocks(client.GetCurrentUserId());
            client.Close();
            foreach (var s in allStocks)
            {
                _stocks.Add(s);
            }
        }

        /// <summary>
        /// Logs Out Users
        /// </summary>
        private void Logout()
        {
            StockExchangeOrderClient client = new StockExchangeOrderClient();
            client.LogoutAsync(client.GetCurrentUserId());
            client.Close();
            //Open Mainwindow
            MainWindow userWindow = new MainWindow();
            userWindow.Show();
            //Close Current window
            Application.Current.Windows[0].Close();
        }

        /// <summary>
        /// Refreshes DataGrid Countiously
        /// </summary>
        /// <param name="state"></param>
        private void RefreshStocks(object state)
        {
            _stocks.Clear();
            StockExchangeOrderClient client = new StockExchangeOrderClient();
            var allStocks = client.GetAllStocks(client.GetCurrentUserId());
            client.Close();
            foreach (var s in allStocks)
            {
                _stocks.Add(s);
            }
        }
    }
}
