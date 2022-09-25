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
                        MarketCapitalization = (decimal)(x.Volume * x.Price)
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

        public decimal GetBalance(int userId)
		{
            decimal totalBalance = 0;
            using (var ctx = new IntelStockExchange())
            {
                var balances = ctx.Balances
                                  .Where(x => x.UserId == userId)
                                  .ToList();

                foreach(var balance in balances)
				{
                    if (balance.IsWithdraw)
                        totalBalance -= balance.Balance1;
                    else
                        totalBalance += balance.Balance1;
                }

                return totalBalance;
            }
        }

        public void BalanceTransaction(BalanceViewModel balance)
        {
            using (var ctx = new IntelStockExchange())
            {
				var balanceEntity = new Balance
				{
					Balance1 = balance.Balance,
					IsWithdraw = balance.IsWithdraw,
					DateTimeAdded = balance.DateTimeAdded,
					UserId = balance.UserId
				};

				ctx.Balances.Add(balanceEntity);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        /// Places Market Order For stock
        /// </summary>
        /// <param name="stockOrder"></param>
        public void MarketOrder(StockOrderViewModel stockOrder)
		{
            using (var ctx = new IntelStockExchange())
            {
                //The reason why we are adding Balance transaction is, When purchasing stock we consider it as withdrawal
                var balanceEntity = new Balance
                {
                    Balance1 = stockOrder.StockCount * stockOrder.OrderStockPrice,
                    IsWithdraw = true,
                    DateTimeAdded = DateTime.Now,
                    UserId = stockOrder.UserId
                };

				var stockOrderEntity = new StockOrder
				{
					StockId = stockOrder.StockId,
                    StockCount = stockOrder.StockCount,
                    OrderStockPrice = stockOrder.OrderStockPrice,
                    UserId = stockOrder.UserId,
                    IsLimitOrder = stockOrder.IsLimitOrder,
                    IsOrderExecuted = true,
					DateTimeAdded = DateTime.Now
				};

				ctx.Balances.Add(balanceEntity);
                var result = ctx.StockOrders.Add(stockOrderEntity);

                var userStock = new UserStock
                {
                    UserId = stockOrder.UserId,
                    StockId = stockOrder.StockId,
                    StockCount = stockOrder.StockCount,
                    StockOrderId = result.Id,
                    DateTimeAdded = DateTime.Now
                };

                ctx.UserStocks.Add(userStock);
                ctx.SaveChanges();
            }
        }
    }
}