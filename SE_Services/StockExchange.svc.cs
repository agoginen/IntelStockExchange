using SE_Entities;
using SE_Services.ViewModels;
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
        /// Logs Registered users into the system in raw view
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserViewModel Login(User user)
        {
            using (var ctx = new IntelStockExchange())
            {
                var users = ctx.Users
                           .Where(x => x.UserName == user.UserName
                                    && x.Password == user.Password)
                           .Select(x => new UserViewModel {
                               UserName = x.UserName,
                               UserType = x.UserType,
                               Id = x.Id
                           })
                           .ToList();

                if (users.Count == 1)
                {
                    SessionManager.Instance.AddUser(users.FirstOrDefault());
                    return users.First();
                }
                else
                {
                    return new UserViewModel();
                }
            }
        }

        /// <summary>
        /// Get all stocks with user information
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<StockViewModel> GetAllUserStocks(int userId)
        {
            //validating if request from valid userId
            if (SessionManager.Instance.ValidateUser(userId)) 
            {
                using (var ctx = new IntelStockExchange())
                {
                    List<StockViewModel> stocks = ctx.Stocks.Select(x => new StockViewModel
                    {
                        StockCount = x.UserStocks.Count == 0 ? 0 : x.UserStocks.FirstOrDefault().StockCount,
                        StockName = x.StockName,
                        Price = x.Price,
                        Id = x.Id
                    }).ToList();
                    return stocks;
                }
            }
			else
			{
                return new List<StockViewModel>();
			}
        }
        
        /// <summary>
        /// Gets all stocks from database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<StockViewModel> GetAllStocks(int userId)
        {
            //validating if request from valid userId
            if (SessionManager.Instance.ValidateUser(userId))
			{
                using (var ctx = new IntelStockExchange())
                {
                    List<StockViewModel> stocks = ctx.Stocks.Select(x => new StockViewModel
                    {
                        StockName = x.StockName,
                        Price = x.Price,
                        Volume = x.Volume,
                        MarketCapitalization = (decimal)(x.Price.HasValue ? x.Volume * x.Price : 0)
                    }).ToList();
                    return stocks;
                }
            }
			else
			{
                return new List<StockViewModel>();
			}
        }

        /// <summary>
        /// Adds stock to Database
        /// </summary>
        /// <param name="stock"></param>
        public void AddStock(Stock stock)
		{
            using (var ctx = new IntelStockExchange())
			{
                stock.DateTimeAdded = DateTime.Now;
                ctx.Stocks.Add(stock);
                ctx.SaveChanges();
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

        public int GetCurrentUserId()
		{
            return SessionManager.Instance.ActiveUsers.FirstOrDefault().Key;
		}

        /// <summary>
        /// Removes User Credentials from the Session Manager
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public void Logout(int userId)
        {
            SessionManager.Instance.ActiveUsers.Remove(userId);
        }
    }
}