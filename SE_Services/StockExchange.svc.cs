using SE_Entities;
using StockExchangeApp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SE_Services
{
	public class StockExchange : IStockExchangeOrder
    {
        /// <summary>
        /// This Method Registers users into our application. It only creates regular user., doesnt create Administor users
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public bool Register(string userName, string password, string emailAddress)
        {
            bool registerSucceeded = false;
            using (var ctx = new IntelStockExchange())
            {
                var q = ctx.Users
                           .Where(x => x.UserName == userName
                                    && x.Password == password);

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

					try
					{
                        ctx.SaveChanges();
                        registerSucceeded = true;
                    }
					catch (Exception ex)
					{
                        registerSucceeded = false;
                    }

                }
            }
            return registerSucceeded;
        }

        /// <summary>
        /// Logs Registered users into the system
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Login(User user)
        {
            using (var ctx = new IntelStockExchange())
            {
                var q = ctx.Users
                           .Where(x => x.UserName == user.UserName
                                    && x.Password == user.Password);

                if (q.ToList().Count == 1)
                {
                    SessionManager.Instance.AddUser(user.UserName);
                    return q.First().Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        public List<Stock> GetAllStocks(int userId)
        {
            using (var ctx = new IntelStockExchange())
            {
                List<Stock> stocks = ctx.Stocks.ToList();
                return stocks;
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

        /// <summary>
        /// Removes User Credentials from the Session Manager
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Logout(int userId)
        {
            bool success = false;
            SessionManager.Instance.ActiveUsers.Remove(userId);
            success = true;
            return success;
        }
    }
}