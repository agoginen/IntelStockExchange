using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SE_Services.ViewModels;
using StockExchangePresentation.Commands;
using StockExchangePresentation.StockExchangeServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace StockExchangePresentation.ViewModel
{
	public class PortfolioViewModel : ViewModelBase
	{
		private readonly Timer _refreshTicker;
		private string _balance { get; set; }
		public string Balance
		{
			get { return _balance; }
			set
			{
				_balance = value;
			}
		}
		private decimal _newBalance { get; set; }
		public decimal NewBalance
		{
			get { return _newBalance; }
			set
			{
				_newBalance = value;
			}
		}
		private ObservableCollection<StockViewModel> _userStocks;
		public ObservableCollection<StockViewModel> UserStocks
		{
			get { return _userStocks; }
			set
			{
				_userStocks = value;
			}
		}
		private static object _lock = new object();

		public ICommand Buy { get; set; }
		public ICommand Sell { get; set; }
		public ICommand Logout { get; set; }
		public ICommand Accounting { get; set; }
		public ICommand Home { get; set; }
		public ICommand Portfolio { get; set; }

		public PortfolioViewModel()
		{
			this._userStocks = new ObservableCollection<StockViewModel>();
			BindingOperations.EnableCollectionSynchronization(_userStocks, _lock);
			_refreshTicker = new Timer(RefreshStocks, null, 0, 1000);
			Buy = new DelegateCommand(BuyCommand);
			Sell = new DelegateCommand(SellCommand);
			Logout = new RelayCommand(LogoutCommand);
			Accounting = new RelayCommand(AccountingCommand);
			Home = new RelayCommand(HomeCommand);
			Portfolio = new RelayCommand(PortfolioCommand);
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
		/// Triggers Buy Action
		/// </summary>
		/// <param name="param"></param>
		private void BuyCommand(object param)
		{
			StockViewModel stock = param as StockViewModel;
			if (stock != null)
			{
				TransactionWindow transaction = new TransactionWindow(stock.Id, stock.StockName, stock.Price.Value, "Buy");
				transaction.Show();
			}
		}

		/// <summary>
		/// Triggers Sell Action
		/// </summary>
		/// <param name="param"></param>
		private void SellCommand(object param)
		{
			StockViewModel stock = param as StockViewModel;
			if (stock != null)
			{
				TransactionWindow transaction = new TransactionWindow(stock.Id, stock.StockName, stock.Price.Value, "Sell");
				transaction.Show();
			}
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
		/// Refreshes DataSource Regularly
		/// </summary>
		/// <param name="state"></param>
		private void RefreshStocks(object state)
		{
			_userStocks.Clear();
			StockExchangeOrderClient client = new StockExchangeOrderClient();
			var allStocks = client.GetPortfolioStocks(client.GetCurrentUserId());
			client.Close();
			foreach (var s in allStocks)
			{
				_userStocks.Add(s);
			}
		}
	}
}
