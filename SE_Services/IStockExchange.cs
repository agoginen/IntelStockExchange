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
        int GetStockPrice(int id);
        [OperationContract]
        void AddStock(Stock stock);
        [OperationContract]
        int GetCurrentUserId();
        [OperationContract]
        void BalanceTransaction(BalanceViewModel balance);
        [OperationContract]
        decimal GetBalance(int userId);
        [OperationContract]
        void MarketOrder(StockOrderViewModel stockOrder);
    }
}