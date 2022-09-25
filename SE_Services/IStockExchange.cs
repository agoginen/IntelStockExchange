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
        bool Logout(int userId);
        [OperationContract]
        List<StockViewModel> GetAllStocks();
        [OperationContract]
        List<StockViewModel> GetAllUserStocks(int userId);
        [OperationContract]
        int GetStockPrice(int id);
        [OperationContract]
        bool ProceedOrder(int userId, List<UserStock> userStocks);
        [OperationContract]
        void AddStock(Stock stock);
    }
}