using SE_Entities;
using SE_Services.ViewModels;
using StockExchangeApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SE_Services
{
	public class StockExchange : IStockExchangeOrder
    {
        private Timer _stockTicker;
        private readonly bool[] addSub = { true, false };

        /// <summary>
        /// Starts Ticker
        /// </summary>
		public void StockPriceTicker()
		{
            _stockTicker = new Timer(StockPriceSimulator, null, 0, 1000);
        }

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
                        HighPrice = x.HighPrice,
                        LowPrice = x.LowPrice,
                        StartPrice = x.StartPrice,
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
                        HighPrice = x.HighPrice,
                        LowPrice = x.LowPrice,
                        StartPrice = x.StartPrice,
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

        /// <summary>
        /// Get Balance of User from Database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Record a Balance Transaction(WithDrawal/Deposit)
        /// </summary>
        /// <param name="balance"></param>
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
        /// Get Total Count of Stock for One User
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        private int GetTotalStockCount(Stock stock)
		{
            int count = 0;
            if(stock.UserStocks.Count > 0)
			{
				foreach (var userStock in stock.UserStocks)
				{
                    count = +userStock.StockCount;
				}
			}
            return count;
		}

        /// <summary>
        /// Places Buy order for Market Order for given stock
        /// </summary>
        /// <param name="stockOrder"></param>
        public bool MarketOrderBuy(StockOrderViewModel stockOrder)
		{
            var result = true;
            var userStockCount = 0;
            using (var ctx = new IntelStockExchange())
            {
                var stock = ctx.Stocks
                               .Where(x => x.Id == stockOrder.StockId)
                               .FirstOrDefault();

                if (stock.Id != 0)
                    userStockCount = GetTotalStockCount(stock);

                //Checking if Volume is available or not
                if (stock.Id != 0 && stock.Volume >= (stockOrder.StockCount + userStockCount))
                {
                    //The reason why we are adding Balance transaction is, When purchasing stock we consider it as withdrawal
                    var balanceEntity = new Balance
                    {
                        Balance1 = stockOrder.StockCount * stockOrder.OrderStockPrice,
                        IsWithdraw = true,
                        DateTimeAdded = DateTime.Now,
                        UserId = stockOrder.UserId
                    };

                    ctx.Balances.Add(balanceEntity);

                    var stockOrderEntity = new StockOrder
                    {
                        StockId = stockOrder.StockId,
                        StockCount = stockOrder.StockCount,
                        OrderStockPrice = stockOrder.OrderStockPrice,
                        UserId = stockOrder.UserId,
                        IsLimitOrder = stockOrder.IsLimitOrder,
                        IsOrderExecuted = true,
                        IsBuyOrder = true,
                        IsActive = true,
                        DateTimeAdded = DateTime.Now
                    };

                    var recentOrder = ctx.StockOrders.Add(stockOrderEntity);

                    if (recentOrder.UserStocks == null || recentOrder.UserStocks.Count == 0)
                    {
                        var userStock = new UserStock
                        {
                            UserId = stockOrder.UserId,
                            StockId = stockOrder.StockId,
                            StockCount = stockOrder.StockCount,
                            StockOrderId = recentOrder.Id,
                            DateTimeAdded = DateTime.Now
                        };

                        ctx.UserStocks.Add(userStock);
                    }
                    else
                    {
                        recentOrder.UserStocks.First().StockCount += recentOrder.StockCount;
                    }

                    ctx.SaveChanges();
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Simulates stock price like real time
        /// </summary>
        private void StockPriceSimulator(object state)
		{
            using (var ctx = new IntelStockExchange())
			{
                var stocks = ctx.Stocks.ToList();

                foreach (var s in stocks)
                {
                    s.Price = StockPriceGenerator(s.Price);

                    if (!s.HighPrice.HasValue || s.Price > s.HighPrice.Value)
                        s.HighPrice = s.Price;

                    if(!s.LowPrice.HasValue || s.Price < s.LowPrice.Value)
                        s.LowPrice = s.Price;

                    if(!s.DateTimeUpdated.HasValue || s.DateTimeUpdated.Value.Date != DateTime.Now.Date)
                        s.StartPrice = s.Price;

                    s.DateTimeUpdated = DateTime.Now;
                    ctx.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Simulates Stock Price
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        private decimal StockPriceGenerator(decimal price)
		{
            decimal result = 0;
            var isAdd = GetRandom(addSub);

            Random rand = new Random();
            var randNumber = NextDouble(rand, 0.01, 0.15);

            if (isAdd)
			{
                result = price + (decimal)randNumber;
			}
			else
			{
                result = price - (decimal)randNumber;
            }

            return result;
		}

        /// <summary>
        /// Gets Random Double Value
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        private double NextDouble(Random rand, double minValue, double maxValue)
        {
            return rand.NextDouble() * (maxValue - minValue) + minValue;
        }

        /// <summary>
        /// Gets Random element from array
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private bool GetRandom(bool[] arr)
        {
            Random rand = new Random();
            int n = rand.Next(arr.Length - 1);
            return arr[n];
        }

        /// <summary>
        /// Places Sell Order for Market Order for given stock
        /// </summary>
        /// <param name="stockOrder"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
		public bool MarketOrderSell(StockOrderViewModel stockOrder)
		{
            var result = true;
            var userStockCount = 0;
            using (var ctx = new IntelStockExchange())
            {
                var stock = ctx.Stocks
                               .Where(x => x.Id == stockOrder.StockId)
                               .FirstOrDefault();

                if (stock.Id != 0)
                    userStockCount = GetTotalStockCount(stock);

                //Checking if stocks are available or not
                if (stock.Id != 0 && stockOrder.StockCount >= userStockCount)
                {
                    //The reason why we are adding Balance transaction is, When purchasing stock we consider it as Deposit
                    var balanceEntity = new Balance
                    {
                        Balance1 = stockOrder.StockCount * stockOrder.OrderStockPrice,
                        IsWithdraw = false,
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
                        IsBuyOrder = false,
                        IsActive = true,
                        DateTimeAdded = DateTime.Now
                    };

                    ctx.Balances.Add(balanceEntity);
                    var recentOrder = ctx.StockOrders.Add(stockOrderEntity);

					if (recentOrder.UserStocks == null || recentOrder.UserStocks.Count == 0)
					{
                        var userStock = new UserStock
                        {
                            UserId = stockOrder.UserId,
                            StockId = stockOrder.StockId,
                            StockCount = stockOrder.StockCount,
                            StockOrderId = recentOrder.Id,
                            DateTimeAdded = DateTime.Now
                        };

                        ctx.UserStocks.Add(userStock);
                    }
                    else
					{
                        recentOrder.UserStocks.First().StockCount -= recentOrder.StockCount; 
                    }

                    ctx.SaveChanges();
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }
	}
}