using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SE_Services.ViewModels;
using StockExchangePresentation.Commands;
using StockExchangePresentation.StockExchangeServices;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace StockExchangePresentation.ViewModel
{
	public class UserHomeViewModel : ViewModelBase
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
		public ICommand Deposit { get; set; }
		public ICommand Withdraw { get; set; }
		public ICommand Portfolio { get; set; }
		public ICommand TransactionHistory { get; set; }

		public UserHomeViewModel()
		{
			this._userStocks = new ObservableCollection<StockViewModel>();
			BindingOperations.EnableCollectionSynchronization(_userStocks, _lock);
			_refreshTicker = new Timer(RefreshStocks, null, 0, 1000);
			this._balance = "$ " + GetBalance().ToString();
			Buy = new DelegateCommand(BuyCommand);
			Sell = new DelegateCommand(SellCommand);
			Logout = new RelayCommand(LogoutCommand);
			Accounting = new RelayCommand(AccountingCommand);
			Home = new RelayCommand(HomeCommand);
			Deposit = new RelayCommand(DepositCommand);
			Withdraw = new RelayCommand(WithdrawCommand);
			Portfolio = new RelayCommand(PortfolioCommand);
			TransactionHistory = new RelayCommand(TransactionHistoryCommand);
		}

		/// <summary>
		/// Gets Balance for current User
		/// </summary>
		/// <returns></returns>
		private decimal GetBalance()
		{
			StockExchangeOrderClient client = new StockExchangeOrderClient();
			var balanceAmount = client.GetBalance(client.GetCurrentUserId());
			client.Close();
			return balanceAmount;
		}

		/// <summary>
		/// User will be able to withdraw money
		/// </summary>
		private void DepositCommand()
		{
			StockExchangeOrderClient client = new StockExchangeOrderClient();
			var balanceViewModel = new BalanceViewModel
			{
				Balance = _newBalance,
				IsWithdraw = false,
				DateTimeAdded = DateTime.Now,
				UserId = client.GetCurrentUserId()
			};
			client.BalanceTransaction(balanceViewModel);
			AccountingCommand();
		}

		/// <summary>
		/// User will be able to withdraw money
		/// </summary>
		private void WithdrawCommand()
		{
			if(_newBalance > GetBalance())
			{
				MessageBox.Show("You dont have enough balance to WithDraw", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				StockExchangeOrderClient client = new StockExchangeOrderClient();
				var balanceViewModel = new BalanceViewModel
				{
					Balance = _newBalance,
					IsWithdraw = true,
					DateTimeAdded = DateTime.Now,
					UserId = client.GetCurrentUserId()
				};
				client.BalanceTransaction(balanceViewModel);
				AccountingCommand();
			}
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
			UserHomeWindow userHomeWindow = new UserHomeWindow();
			userHomeWindow.Show();
			//Close Current Window
			Application.Current.Windows[0].Close();
		}

		/// <summary>
		/// Opens Transaction History Page
		/// </summary>
		private void TransactionHistoryCommand()
		{
			//Open User Home Window
			UserHistoryWindow userHistory = new UserHistoryWindow();
			userHistory.Show();
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
				TransactionWindow transaction = new TransactionWindow(stock.Id,stock.StockName, stock.Price.Value,"Buy");
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
		/// Loads stocks from database
		/// </summary>
		public void LoadStocks()
		{
			_userStocks.Clear();
			StockExchangeOrderClient client = new StockExchangeOrderClient();
			var allStocks = client.GetAllUserStocks(client.GetCurrentUserId());
			client.Close();
			foreach (var s in allStocks)
			{
				_userStocks.Add(s);
			}
		}

		/// <summary>
		/// Refreshes DataSource Regularly
		/// </summary>
		/// <param name="state"></param>
		private void RefreshStocks(object state)
		{
			_userStocks.Clear();
			StockExchangeOrderClient client = new StockExchangeOrderClient();
			var allStocks = client.GetAllUserStocks(client.GetCurrentUserId());
			client.Close();
			foreach (var s in allStocks)
			{
				_userStocks.Add(s);
			}
		}
	}
}