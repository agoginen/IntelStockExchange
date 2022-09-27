using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SE_Services.ViewModels;
using StockExchangePresentation.Commands;
using StockExchangePresentation.StockExchangeServices;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace StockExchangePresentation.ViewModel
{
	public class UserHistoryViewModel : ViewModelBase
	{
		public ICommand Logout { get; set; }
		public ICommand Accounting { get; set; }
		public ICommand Home { get; set; }
		public ICommand Portfolio { get; set; }
		public ICommand Cancel { get; set; }

		private ObservableCollection<StockOrderViewModel> _stockOrders;
		public ObservableCollection<StockOrderViewModel> StockOrders
		{
			get { return _stockOrders; }
			set
			{
				_stockOrders = value;
			}
		}

		public UserHistoryViewModel()
		{
			this._stockOrders = new ObservableCollection<StockOrderViewModel>();
			LoadStockOrders();
			Logout = new RelayCommand(LogoutCommand);
			Accounting = new RelayCommand(AccountingCommand);
			Home = new RelayCommand(HomeCommand);
			Portfolio = new RelayCommand(PortfolioCommand);
			Cancel = new DelegateCommand(CancelCommand);
		}

		/// <summary>
		/// Cancels limit order
		/// </summary>
		/// <param name="param"></param>
		private void CancelCommand(object param)
		{
			StockOrderViewModel stockOrder = param as StockOrderViewModel;
			if (stockOrder != null)
			{
				StockExchangeOrderClient client = new StockExchangeOrderClient();
				client.CancelPendingOrder(stockOrder.Id);
				client.Close();
			}
			LoadStockOrders();
		}

		/// <summary>
		/// Opens Accounting Page
		/// </summary>
		private void AccountingCommand()
		{
			//Open Accouinting Window
			AccountingWindow accountingWindow = new AccountingWindow();
			accountingWindow.Show();
			//Close Current window
			Application.Current.Windows[0].Close();
		}

		/// <summary>
		/// Opens Home Page
		/// </summary>
		private void HomeCommand()
		{
			//Open User Home Window
			UserHomeWindow homeWindow = new UserHomeWindow();
			homeWindow.Show();
			//Close Current Window
			Application.Current.Windows[0].Close();
		}

		/// <summary>
		/// Opens Portfolio Page
		/// </summary>
		private void PortfolioCommand()
		{
			//Open User Home Window
			PortfolioWindow portfolioWindow = new PortfolioWindow();
			portfolioWindow.Show();
			//Close Current Window
			Application.Current.Windows[0].Close();
		}

		/// <summary>
		/// Logs Out User
		/// </summary>
		private void LogoutCommand()
		{
			StockExchangeOrderClient client = new StockExchangeOrderClient();
			client.LogoutAsync(client.GetCurrentUserId());
			client.Close();
			//Open Mainwindow
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			//Close Current window
			Application.Current.Windows[0].Close();
		}

		/// <summary>
		/// Loads stocks orders from database
		/// </summary>
		public void LoadStockOrders()
		{
			_stockOrders.Clear();
			StockExchangeOrderClient client = new StockExchangeOrderClient();
			var allStocks = client.GetStockOrderHistory(client.GetCurrentUserId());
			client.Close();
			foreach (var s in allStocks)
			{
				_stockOrders.Add(s);
			}
		}
	}
}
