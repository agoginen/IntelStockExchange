using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using StockExchangePresentation.StockExchangeServices;
using System;
using System.Windows;
using System.Windows.Input;

namespace StockExchangePresentation.ViewModel
{
	public class AdminViewModel : ViewModelBase
    {
        private Stock stock;
        public ICommand AddStockCommand { get; set; }

        public AdminViewModel()
        {
            this.stock = new Stock();
            AddStockCommand = new RelayCommand(AddStock);
        }

        public string StockName
        {
            get
            {
                return stock.StockName;
            }
            set
            {
                stock.StockName = value;
            }
        }

        public decimal Password
        {
            get
            {
                return stock.Price.Value;
            }
            set
            {
                stock.Price = value;
            }
        }

        public int Volume
        {
            get
            {
                return stock.Volume;
            }
            set
            {
                stock.Volume = value;
            }
        }

        /// <summary>
        /// Adds stock to Database
        /// </summary>
        private async void AddStock()
		{
            StockExchangeOrderClient client = new StockExchangeOrderClient();
			try
			{
                await client.AddStockAsync(stock);
                client.Close();
            }
            catch (Exception ex)
			{
                MessageBox.Show("Adding stock " + stock.StockName + " to database has failed. Please contact administrator if problem persists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
