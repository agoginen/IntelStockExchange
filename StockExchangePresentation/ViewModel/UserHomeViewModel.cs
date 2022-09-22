using GalaSoft.MvvmLight;
using SE_Services.ViewModels;
using System.Collections.Generic;
using System.Windows.Input;
using Unity;

namespace StockExchangePresentation.ViewModel
{
	public class UserHomeViewModel : ViewModelBase
	{
		private List<StockViewModel> _transactions;
		public List<StockViewModel> Transactions
		{
			get { return _transactions; }
			set
			{
				_transactions = value;
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
		}

		private void BuyCommand(object param)
		{
			var stock = param as StockViewModel;
			if (stock != null)
			{
				MarketViewModel.AddBuyTransaction(stock);
			}
		}

		private void SellCommand(object param)
		{
			var stock = param as StockViewModel;
			if (stock != null)
			{
				MarketViewModel.AddSellTransaction(stock);
			}
		}
	}
}