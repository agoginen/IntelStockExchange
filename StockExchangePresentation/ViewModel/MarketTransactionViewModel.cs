using GalaSoft.MvvmLight;
using SE_Services.ViewModels;
using System.Collections.Generic;

namespace StockExchangePresentation.ViewModel
{
	public class MarketTransactionViewModel : ViewModelBase
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

        public void AddSellTransaction(StockViewModel stock)
        {
            Transactions.Add(stock);
        }

        public void AddBuyTransaction(StockViewModel stock)
        {
            Transactions.Add(stock);
        }
    }
}
