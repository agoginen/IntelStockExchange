using SE_Entities;
using System;
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
        int Login(User user);
        [OperationContract]
        bool Logout(int userId);
        [OperationContract]
        List<Stock> GetAllStocks(int userId);
        [OperationContract]
        List<UserStock> GetAllUserStocks(int userId);
        [OperationContract]
        int GetStockPrice(int id);
        [OperationContract]
        bool ProceedOrder(int userId, List<UserStock> userStocks);
    }
}
