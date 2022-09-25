using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SE_Services.ViewModels;
using StockExchangePresentation.StockExchangeServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace StockExchangePresentation.ViewModel
{
	public class AdminViewModel : ViewModelBase
    {
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
        public Window AdminWindow { get; set; }

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
                return Stock.Price ?? 0;
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
            Stock = new Stock();
            this._stocks = new ObservableCollection<StockViewModel>();
            AddStockCommand = new RelayCommand(AddStock);
            LoadStocks();
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

        public void LoadStocks()
		{
            _stocks.Clear();
            StockExchangeOrderClient client = new StockExchangeOrderClient();
            var allStocks = client.GetAllStocks();
            client.Close();
            foreach (var s in allStocks)
            {
                _stocks.Add(s);
            }
        }
    }
}
