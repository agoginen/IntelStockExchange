using SE_Entities;
using StockExchangeApp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SE_Services
{
	public class StockExchange : IStockExchangeOrder
    {

        public bool Register(string userName, string password, string emailAddress)
        {
            bool registerSucceeded = false;
            using (var ctx = new IntelStockExchange())
            {
                var q = from user in ctx.Users
                        where user.UserName == userName && user.Password == password
                        select user;

                if (q.ToList().Count == 0)
                {
                    ctx.Users.Add(new User
                    {
                        UserName = userName,
                        Password = password,
                        EmailAddress = emailAddress,
                        UserType = 1,
                        DateTimeAdded = DateTime.Now
                    });
                    ctx.SaveChanges();
                    registerSucceeded = true;
                }
            }
            return registerSucceeded;
        }

        public int Login(User user)
        {
            using (var ctx = new IntelStockExchange())
            {
                var q = from u in ctx.Users
                        where u.UserName == user.UserName && user.Password == user.Password
                        select u;

                // the database contains the user-pass pair
                if (q.ToList().Count == 1)
                {
                    return SessionManager.Instance.AddUser(user.UserName);
                }
                // the database does not contain the user-pass pair
                else
                {
                    return 0;
                }
            }
        }

        public List<Stock> GetAllStocks(int userId)
        {
            if (SessionManager.Instance.ValidateUser(userId))
            {
                using (var ctx = new IntelStockExchange())
                {
                    List<Stock> stocks = ctx.Stocks.ToList();
                    return stocks;
                }
            }
            else
            {
                return null;
            }
        }
        
        public List<UserStock> GetAllUserStocks(int userId)
        {
            if (SessionManager.Instance.ValidateUser(userId))
            {
                using (var ctx = new IntelStockExchange())
                {
                    List<UserStock> userStocks = ctx.UserStocks
                                                    .Where(x => x.UserId == userId)
                                                    .ToList();
                    return userStocks;
                }
            }
            else
            {
                return null;
            }
        }

        public int GetStockPrice(int id)
        {
            if (SessionManager.Instance.ValidateUser(id))
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        public bool ProceedOrder(int userId, List<UserStock> userStock)
        {
            if (SessionManager.Instance.ValidateUser(userId))
            {
                bool orderProceedSucceeded = false;
                using (var ctx = new IntelStockExchange())
                {
                    string userName = SessionManager.Instance.GetUserNameByGuid(userId);

                    User user = (from u in ctx.Users
                                 where u.UserName == userName
                                 select u).FirstOrDefault();

                    orderProceedSucceeded = true;
                    ctx.SaveChanges();
                }
                return orderProceedSucceeded;
            }
            else
            {
                return false;
            }
        }
        public bool Logout(int userId)
        {
            bool success = false;
            SessionManager.Instance.ActiveUsers.Remove(userId);
            success = true;
            return success;
        }
    }
}