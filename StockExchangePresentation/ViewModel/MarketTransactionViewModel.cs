using GalaSoft.MvvmLight;
using SE_Services.ViewModels;
using StockExchangePresentation.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StockExchangePresentation.ViewModel
{
	public class MarketTransactionViewModel : ObservableObject
	{
        private ObservableCollection<StockViewModel> _transactions;
        public ObservableCollection<StockViewModel> Transactions
        {
            get { return _transactions; }
            set
            {
                _transactions = value;
            }
        }

        public ICommand SubmitCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ICommand SubmitAllCommand { get; set; }
        public ICommand CancelAllCommand { get; set; }

        public MarketTransactionViewModel()
		{
            _transactions = new ObservableCollection<StockViewModel>();

            SubmitCommand = new DelegateCommand(Submit);
            SubmitAllCommand = new DelegateCommand(SubmitAll);
            CancelCommand = new DelegateCommand(Cancel);
            CancelAllCommand = new DelegateCommand(CancelAll);
        }

        private void Submit(object param)
        {
            var transaction = param as StockViewModel;
            if (transaction != null)
            {
                Transactions.Remove(transaction);
            }
        }

        private void Cancel(object param)
        {
            var transaction = param as StockViewModel;
            if (transaction != null)
            {
                Transactions.Remove(transaction);
            }
        }

        private void SubmitAll(object param)
        {
            foreach (var t in Transactions)
            {

            }
            Transactions.Clear();
        }

        private void CancelAll(object param)
        {
            foreach (var t in Transactions)
            {

            }
            Transactions.Clear();
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
