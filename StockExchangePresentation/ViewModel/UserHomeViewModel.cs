﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SE_Services.ViewModels;
using StockExchangePresentation.Commands;
using StockExchangePresentation.StockExchangeServices;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace StockExchangePresentation.ViewModel
{
	public class UserHomeViewModel : ViewModelBase
	{
		private ObservableCollection<StockViewModel> _userStocks;
		public ObservableCollection<StockViewModel> UserStocks
		{
			get { return _userStocks; }
			set
			{
				_userStocks = value;
			}
		}

		public ICommand Buy { get; set; }
		public ICommand Sell { get; set; }
		public ICommand Logout { get; set; }

		public UserHomeViewModel()
		{
			this._userStocks = new ObservableCollection<StockViewModel>();
			LoadStocks();
			Buy = new DelegateCommand(BuyCommand);
			Sell = new DelegateCommand(SellCommand);
			Logout = new RelayCommand(LogoutCommand);
		}

		private void BuyCommand(object param)
		{
			StockViewModel stock = param as StockViewModel;
			if (stock != null)
			{
				TransactionWindow transaction = new TransactionWindow(stock.Id,stock.StockName, stock.Price.Value,"Buy");
				transaction.Show();
			}
		}

		private void SellCommand(object param)
		{
			StockViewModel stock = param as StockViewModel;
			if (stock != null)
			{
				TransactionWindow transaction = new TransactionWindow(stock.Id, stock.StockName, stock.Price.Value, "Sell");
				transaction.Show();
			}
		}

		private void LogoutCommand()
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
	}
}