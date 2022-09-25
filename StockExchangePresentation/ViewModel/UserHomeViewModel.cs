using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SE_Services.ViewModels;
using StockExchangePresentation.Commands;
using StockExchangePresentation.StockExchangeServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Unity;

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

		private MarketTransactionViewModel _marketViewModel;

		[Dependency]
		public MarketTransactionViewModel MarketViewModel
		{
			get { return _marketViewModel; }
			set
			{
				if (value != _marketViewModel)
				{
					_marketViewModel = value;
				}
			}
		}

		public UserHomeViewModel()
		{
			this._userStocks = new ObservableCollection<StockViewModel>();
			this._marketViewModel = new MarketTransactionViewModel();
			LoadStocks();
			Buy = new DelegateCommand(BuyCommand);
			Sell = new DelegateCommand(SellCommand);
		}

		private void BuyCommand(object param)
		{
			StockViewModel stock = param as StockViewModel;
			if (stock != null)
			{
				MarketViewModel.AddBuyTransaction(stock);
			}
		}

		private void SellCommand(object param)
		{
			StockViewModel stock = param as StockViewModel;
			if (stock != null)
			{
				MarketViewModel.AddSellTransaction(stock);
			}
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