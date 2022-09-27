using SE_Services.ViewModels;
using StockExchangePresentation.StockExchangeServices;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StockExchangePresentation
{
	/// <summary>
	/// Interaction logic for TransactionWindow.xaml
	/// </summary>
	public partial class TransactionWindow : INotifyPropertyChanged
	{
        private int _stockId { get; set; }
        private decimal _stockPrice { get; set; }
        private string _stockName { get; set; }
        private int _stockCount { get; set; }
        private string _orderType { get; set; }
        private string _transaction { get; set; }
        private MarketTimingViewModel _marketTimingViewModel { get; set; }

        public string StockName
        {
            get
            {
                return _stockName;
            }
            set
            {
                if(_stockName != value)
				{
                    _stockName = value;
                    OnPropertyChanged();
                }
            }
        }

		public decimal StockPrice
        {
            get
            {
                return _stockPrice;
            }
            set
            {
                if (_stockPrice != value)
                {
                    _stockPrice = value;
                    OnPropertyChanged();
                }
            }
        }

        public int StockCount
        {
            get
            {
                return _stockCount;
            }
            set
            {
                if (_stockCount != value)
                {
                    _stockCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public string OrderType
        {
            get
            {
                return _orderType;
            }
            set
            {
                if (_orderType != value)
                {
                    _orderType = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Transaction
        {
            get
            {
                return _transaction;
            }
            set
            {
                if (_transaction != value)
                {
                    _transaction = value;
                    OnPropertyChanged();
                }
            }
        }

        public TransactionWindow(int stockId, string stockName, decimal stockPrice, string transaction)
		{
            DataContext = this;
            _stockId = stockId;
            _stockName = stockName;
            _stockPrice = stockPrice;
            _transaction = transaction;
            _marketTimingViewModel = GetMarketTiming();
            InitializeComponent();
        }

		public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets Market Timing For today
        /// </summary>
        /// <returns></returns>
        private MarketTimingViewModel GetMarketTiming()
		{
            StockExchangeOrderClient client = new StockExchangeOrderClient();
            var marketTiming = client.GetMarketTimingForToday();
            return marketTiming;
        }

        /// <summary>
        /// This is triggered when user clicks on Buy or Sell Stock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Transaction_Click(object sender, RoutedEventArgs e)
        {
            if (_stockCount > 0)
            {
                if (_marketTimingViewModel.IsActive && (_marketTimingViewModel.StartTime > DateTime.Now.TimeOfDay && _marketTimingViewModel.CloseTime < DateTime.Now.TimeOfDay)) 
                {
                    if (_transaction == "Buy")
                    {
                        StockExchangeOrderClient client = new StockExchangeOrderClient();
                        var balanceAmount = client.GetBalance(client.GetCurrentUserId());

                        if (balanceAmount > _stockPrice * _stockCount)
                        {
                            var stockOrderViewModel = new StockOrderViewModel
                            {
                                StockId = _stockId,
                                UserId = client.GetCurrentUserId(),
                                OrderStockPrice = _stockPrice,
                                IsLimitOrder = false,
                                StockCount = _stockCount
                            };

                            var isOrderPlaced = false;

                            if (this.cmbOrderType.Text == "Market Order")
                            {
                                isOrderPlaced = client.MarketOrderBuy(stockOrderViewModel);
                            }
                            else if (this.cmbOrderType.Text == "Limit Order")
                            {
                                stockOrderViewModel.IsLimitOrder = true;
                                isOrderPlaced = client.LimitOrderBuy(stockOrderViewModel);
                            }

                            client.Close();

                            if (isOrderPlaced)
                            {
                                //Open Mainwindow
                                UserHomeWindow userWindow = new UserHomeWindow();
                                userWindow.Show();
                                //Close Current window
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("There is no Stock Volume to support your Stock Count", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                            MessageBox.Show("Your Balance is low to place order. Please deposit Money and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else if (_transaction == "Sell")
                    {
                        StockExchangeOrderClient client = new StockExchangeOrderClient();

                        var stockOrderViewModel = new StockOrderViewModel
                        {
                            StockId = _stockId,
                            UserId = client.GetCurrentUserId(),
                            OrderStockPrice = _stockPrice,
                            IsLimitOrder = false,
                            StockCount = _stockCount
                        };

                        var isOrderPlaced = false;

                        if (this.cmbOrderType.Text == "Market Order")
                        {
                            isOrderPlaced = client.MarketOrderSell(stockOrderViewModel);
                        }
                        else if (this.cmbOrderType.Text == "Limit Order")
                        {
                            stockOrderViewModel.IsLimitOrder = true;
                            isOrderPlaced = client.LimitOrderSell(stockOrderViewModel);
                        }

                        if (isOrderPlaced)
                        {
                            //Open Mainwindow
                            UserHomeWindow userWindow = new UserHomeWindow();
                            userWindow.Show();
                            //Close Current window
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("You dont own enough stocks to support the sell", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You have selected 0 stocks to place order.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
				else
				{
                    MessageBox.Show("Cant place order, Market is closed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
			else
			{
                MessageBox.Show("Cant Place order for 0 stocks.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		//Event Handler To allow only integers
		private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //Event Handler to allow only decimals
        private void DecimalValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
			this.Close();
		}

        private void btnLogin_Click(object sender, RoutedEventArgs e) { }

		private void cmbOrderType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            if (this.cmbOrderType.Text == "Market Order")
            {
                this.txtStockPrice.IsReadOnly = false;
            }
            else if (this.cmbOrderType.Text == "Limit Order")
            {
                this.txtStockPrice.IsReadOnly = true;
            }
        }
	}
}
