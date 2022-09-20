using GalaSoft.MvvmLight;
using StockExchangePresentation.StockExchangeServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockExchangePresentation.ViewModel
{
	public class UserHomeViewModel : ViewModelBase
	{
		public List<Stock> Stocks { get; set; }
		public UserHomeViewModel()
		{
		}
	}
}
