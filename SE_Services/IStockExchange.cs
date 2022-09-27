using SE_Entities;
using SE_Services.ViewModels;
using System.Collections.Generic;
using System.ServiceModel;

namespace StockExchangeApp
{
	[ServiceContract]
    public interface IStockExchangeOrder
    {
        [OperationContract]
        bool Register(string userName, string password, string emailAddress);
        [OperationContract]
        UserViewModel Login(User user);
        [OperationContract]
        void Logout(int userId);
        [OperationContract]
        List<StockViewModel> GetAllStocks(int userId);
        [OperationContract]
        List<StockViewModel> GetAllUserStocks(int userId);
        [OperationContract]
        void AddStock(Stock stock);
        [OperationContract]
        int GetCurrentUserId();
        [OperationContract]
        void BalanceTransaction(BalanceViewModel balance);
        [OperationContract]
        decimal GetBalance(int userId);
        [OperationContract]
        bool MarketOrderBuy(StockOrderViewModel stockOrder);
        [OperationContract]
        bool MarketOrderSell(StockOrderViewModel stockOrder);
        [OperationContract]
        void StockPriceTicker();
        [OperationContract]
        List<StockViewModel> GetPortfolioStocks(int userId);
        [OperationContract]
        List<StockOrderViewModel> GetStockOrderHistory(int userId);
        [OperationContract]
        bool LimitOrderBuy(StockOrderViewModel stockOrder);
        [OperationContract]
        bool LimitOrderSell(StockOrderViewModel stockOrder);
        [OperationContract]
        void CancelPendingOrder(int pendingOrderId);
        [OperationContract]
        MarketTimingViewModel GetMarketTimingForToday();
        [OperationContract]
        List<MarketTimingViewModel> GetAllMarketTimings();
        [OperationContract]
        void UpdateMarketTimings(string StartTime, string CloseTime);
        [OperationContract]
        void UpdateMarketDays(bool m, bool t, bool w, bool th, bool f, bool sat, bool sun);
    }
}