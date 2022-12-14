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
        private readonly bool[] addSub = { true, false, true };

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
                        StockCount = x.UserStocks.FirstOrDefault(y => y.UserId == userId) == null ? 0 : x.UserStocks.FirstOrDefault(y => y.UserId == userId).StockCount,
                        StockName = x.StockName,
                        Price = x.Price,
                        HighPrice = x.HighPrice,
                        LowPrice = x.LowPrice,
                        StartPrice = x.StartPrice,
                        Id = x.Id,
                        ExecutedPrice = x.UserStocks.Count == 0 ? 0 : x.UserStocks.FirstOrDefault().StockOrder.OrderStockPrice,
                        CurrentValue = x.Price * (x.UserStocks.Count == 0 ? 0 : x.UserStocks.FirstOrDefault().StockCount),
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
            var userStocks = stock.UserStocks
                                  .Where(x => x.UserId == GetCurrentUserId())
                                  .ToList();
            if(userStocks.Count > 0)
			{
				foreach (var userStock in userStocks)
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

                    var oldUserStock = ctx.UserStocks
                                        .Where(x => x.StockId == recentOrder.StockId
                                                 && x.UserId == recentOrder.UserId)
                                        .FirstOrDefault(); ;
                    //First Purchase
                    if (oldUserStock == null || oldUserStock.Id == 0)
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
                        //Since no old stocks
                        recentOrder.NewAverageStockPrice = recentOrder.OrderStockPrice;
                    }
                    else
                    {
                        //Calculating New cost of each stock
                        recentOrder.NewAverageStockPrice = ((oldUserStock.StockCount * oldUserStock.Stock.Price) + (recentOrder.StockCount * recentOrder.OrderStockPrice))/(oldUserStock.StockCount + recentOrder.StockCount);
                        oldUserStock.StockCount += recentOrder.StockCount;
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
        /// Places Buy order for limit Order for given stock
        /// </summary>
        /// <param name="stockOrder"></param>
        public bool LimitOrderBuy(StockOrderViewModel stockOrder)
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
                        IsOrderExecuted = false,
                        IsBuyOrder = true,
                        IsActive = true,
                        DateTimeAdded = DateTime.Now
                    };

                    var recentOrder = ctx.StockOrders.Add(stockOrderEntity);

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
                var day = DateTime.Now.DayOfWeek.ToString();

                var marketTimings = ctx.MarketTimings
                                       .FirstOrDefault(x => x.Day.ToString() == day);

				if (marketTimings.IsActive && (DateTime.Now.TimeOfDay > marketTimings.StartTime && DateTime.Now.TimeOfDay < marketTimings.CloseTime))
				{
                    var stocks = ctx.Stocks.ToList();

                    foreach (var s in stocks)
                    {
                        s.Price = StockPriceGenerator(s.Price);

                        if (!s.HighPrice.HasValue || s.Price > s.HighPrice.Value)
                            s.HighPrice = s.Price;

                        if (!s.LowPrice.HasValue || s.Price < s.LowPrice.Value)
                            s.LowPrice = s.Price;

                        if (!s.DateTimeUpdated.HasValue || s.DateTimeUpdated.Value.Date != DateTime.Now.Date)
						{
                            s.StartPrice = s.Price;
                            s.HighPrice = s.Price;
                            s.LowPrice = s.Price;
                        }

                        s.DateTimeUpdated = DateTime.Now;
                        ctx.SaveChanges();
                    }
                }
            }

            //After price change execute limit orders
            ExecuteValidLimitOrders();
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
            int n = rand.Next(arr.Length);
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

                //Checking if USerstocks are available or not
                if (stock.Id != 0 && stockOrder.StockCount <= userStockCount)
                {
                    //The reason why we are adding Balance transaction is, When selling stock we consider it as Deposit
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
                        //False Since Executed
                        IsActive = true,
                        DateTimeAdded = DateTime.Now
                    };

                    ctx.Balances.Add(balanceEntity);

                    var recentOrder = ctx.StockOrders
                                         .Add(stockOrderEntity);

                    var oldUserStock = ctx.UserStocks
                                        .Where(x => x.StockId == recentOrder.StockId
                                                 && x.UserId == recentOrder.UserId)
                                        .FirstOrDefault();

					if (oldUserStock == null || oldUserStock.Id == 0)
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
                        oldUserStock.StockCount -= recentOrder.StockCount; 
                    }

                    //No Need To calculate Average
                    recentOrder.NewAverageStockPrice = oldUserStock.Stock.Price;

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
        /// Places Sell Order for Limit Order for given stock
        /// </summary>
        /// <param name="stockOrder"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool LimitOrderSell(StockOrderViewModel stockOrder)
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

                //Checking if USerstocks are available or not
                if (stock.Id != 0 && stockOrder.StockCount <= userStockCount)
                {
                    var stockOrderEntity = new StockOrder
                    {
                        StockId = stockOrder.StockId,
                        StockCount = stockOrder.StockCount,
                        OrderStockPrice = stockOrder.OrderStockPrice,
                        UserId = stockOrder.UserId,
                        IsLimitOrder = stockOrder.IsLimitOrder,
                        IsOrderExecuted = false,
                        IsBuyOrder = false,
                        IsActive = true,
                        DateTimeAdded = DateTime.Now
                    };

                    var recentOrder = ctx.StockOrders
                                         .Add(stockOrderEntity);

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
        /// Gets Portfolio Stock Information
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<StockViewModel> GetPortfolioStocks(int userId)
		{
            //validating if request from valid userId
            if (SessionManager.Instance.ValidateUser(userId))
            {
                using (var ctx = new IntelStockExchange())
                {
                    List<StockViewModel> stocks = ctx.UserStocks
                                                     .Where(x => x.UserId == userId)
                                                     .Select(x => new StockViewModel
                                                     {
                                                        StockCount = x.StockCount,
                                                        StockName = x.Stock.StockName,
                                                        Price = x.Stock.Price,
                                                        HighPrice = x.Stock.HighPrice,
                                                        LowPrice = x.Stock.LowPrice,
                                                        StartPrice = x.Stock.StartPrice,
                                                        Id = x.Id,
                                                        ExecutedPrice = x.StockOrder.OrderStockPrice,
                                                        AverageShareCost = x.StockOrder.NewAverageStockPrice.Value,
                                                        CurrentValue = x.Stock.Price * x.StockCount,
                                                        Gain = x.StockCount == 0 ? 0 : (x.Stock.Price * x.StockCount) - (x.StockOrder.NewAverageStockPrice * x.StockCount),
                                                        GainPercetage= x.StockCount == 0 ? 0 : (((x.Stock.Price * x.StockCount) - (x.StockOrder.NewAverageStockPrice * x.StockCount))/(x.StockCount * x.StockOrder.NewAverageStockPrice))
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
        /// Gets Stock Order History from Database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<StockOrderViewModel> GetStockOrderHistory(int userId)
		{
            var result = new List<StockOrderViewModel>();

            if (SessionManager.Instance.ValidateUser(userId))
			{
                using (var ctx = new IntelStockExchange())
                {
                    result = ctx.StockOrders
                                .Where(x => x.UserId == userId)
                                .Select(x => new StockOrderViewModel { 
                                    Id = x.Id,
                                    StockCount = x.StockCount,
                                    StockName = x.Stock.StockName,
                                    OrderStockPrice = x.OrderStockPrice,
                                    IsLimitOrder = x.IsLimitOrder,
                                    IsOrderExecuted = x.IsOrderExecuted,
                                    UserId = x.UserId,
                                    OrderStatus = x.IsOrderExecuted ? "Completed Order" : (x.IsActive ? "Pending Order" : "Canceled Order"),
                                    OrderType = x.IsLimitOrder ? "Limit Order" : "Market Order",
                                    BuySellType = x.IsBuyOrder ? "Buy" : "Sell",
                                    CanOrderBeCanceled = !x.IsOrderExecuted && x.IsActive
                                })
                                .ToList();
                }
            }

            return result;
		}

        /// <summary>
        /// Executes All Limit Orders
        /// </summary>
        private void ExecuteValidLimitOrders()
		{
            using (var ctx = new IntelStockExchange())
			{
                var pendingOrders = ctx.StockOrders
                                       .Where(x => x.IsActive
                                                   && x.IsLimitOrder
                                                   && !x.IsOrderExecuted)
                                       .ToList();
				
                foreach (var pendingOrder in pendingOrders)
				{
                    if(pendingOrder.DateTimeAdded.Day == DateTime.Now.Day)
					{
                        var currentStock = ctx.Stocks
                                              .Where(x => x.Id == pendingOrder.StockId)
                                              .FirstOrDefault();

                        if (pendingOrder.IsBuyOrder)
                        {
                            if (currentStock.Price <= pendingOrder.OrderStockPrice  )
                            {
                                ExecuteLimitBuy(pendingOrder);
                            }
                        }
                        else
                        {
                            if (currentStock.Price >= pendingOrder.OrderStockPrice)
                            {
                                ExecuteLimitSell(pendingOrder);
                            }
                        }
                    }
					else
					{
                        CancelPendingOrder(pendingOrder.Id);
                    }
				}
			}
        }

        /// <summary>
        /// Executes pending Limit Order Buy
        /// </summary>
        /// <param name="stockOrder"></param>
        private void ExecuteLimitBuy(StockOrder pendingOrder)
		{
            using (var context = new IntelStockExchange())
			{
                var stockOrder = context.StockOrders
                                    .First(x => x.Id == pendingOrder.Id);

                var oldUserStock = context.UserStocks
                                      .Where(x => x.StockId == pendingOrder.StockId
                                               && x.UserId == pendingOrder.UserId)
                                      .FirstOrDefault(); ;
                //First Purchase
                if (oldUserStock == null || oldUserStock.Id == 0)
                {
                    var userStock = new UserStock
                    {
                        UserId = stockOrder.UserId,
                        StockId = stockOrder.StockId,
                        StockCount = stockOrder.StockCount,
                        StockOrderId = stockOrder.Id,
                        DateTimeAdded = DateTime.Now
                    };

                    context.UserStocks.Add(userStock);
                    //Since no old stocks
                    stockOrder.NewAverageStockPrice = pendingOrder.OrderStockPrice;
                }
                else
                {
                    //Calculating New cost of each stock
                    stockOrder.NewAverageStockPrice = ((oldUserStock.StockCount * oldUserStock.Stock.Price) + (pendingOrder.StockCount * pendingOrder.OrderStockPrice)) / (oldUserStock.StockCount + pendingOrder.StockCount);
                    oldUserStock.StockCount += pendingOrder.StockCount;
                }
                stockOrder.IsOrderExecuted = true;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Excutes Pending Limit Sell
        /// </summary>
        /// <param name="pendingOrder"></param>
        private void ExecuteLimitSell(StockOrder pendingOrder)
        {
            using (var context = new IntelStockExchange())
			{
                var oldUserStock = context.UserStocks
                                      .Where(x => x.StockId == pendingOrder.StockId
                                               && x.UserId == pendingOrder.UserId)
                                      .FirstOrDefault();

                if (oldUserStock == null || oldUserStock.Id == 0)
                {
                    var userStock = new UserStock
                    {
                        UserId = pendingOrder.UserId,
                        StockId = pendingOrder.StockId,
                        StockCount = pendingOrder.StockCount,
                        StockOrderId = pendingOrder.Id,
                        DateTimeAdded = DateTime.Now
                    };

                    context.UserStocks.Add(userStock);
                }
                else
                {
                    oldUserStock.StockCount -= pendingOrder.StockCount;
                }

                var stockOrder = context.StockOrders.First(x => x.Id == pendingOrder.Id);
                //No Need To calculate Average
                stockOrder.NewAverageStockPrice = oldUserStock.Stock.Price;
                stockOrder.IsOrderExecuted = true;

                //The reason why we are adding Balance transaction is, When selling stock we consider it as Deposit
                var balanceEntity = new Balance
                {
                    Balance1 = stockOrder.StockCount * stockOrder.OrderStockPrice,
                    IsWithdraw = false,
                    DateTimeAdded = DateTime.Now,
                    UserId = stockOrder.UserId
                };

                context.Balances.Add(balanceEntity);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// CancelsPending Order
        /// </summary>
        /// <param name="pendingOrderId"></param>
        public void CancelPendingOrder(int pendingOrderId)
        {
            using (var context = new IntelStockExchange())
            {
                var stockOrder = context.StockOrders
                                        .First(x => x.Id == pendingOrderId);
                stockOrder.IsActive = false;

				if (stockOrder.IsBuyOrder)
				{
                    //The reason why we are adding Balance transaction is, When purchasing stock we consider it as withdrawal
                    var balanceEntity = new Balance
                    {
                        Balance1 = stockOrder.StockCount * stockOrder.OrderStockPrice,
                        IsWithdraw = false,
                        DateTimeAdded = DateTime.Now,
                        UserId = stockOrder.UserId
                    };

                    context.Balances.Add(balanceEntity);
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets Market Timing for today
        /// </summary>
        /// <returns></returns>
        public MarketTimingViewModel GetMarketTimingForToday()
		{
            using (var context = new IntelStockExchange())
			{
                var day = DateTime.Now.DayOfWeek.ToString();
                var marketTiming = context.MarketTimings
                                           .Where(x => x.Day.ToString() == day)
                                           .Select(x => new MarketTimingViewModel()
										   {
                                               Day = x.Day,
                                               StartTime = x.StartTime,
                                               CloseTime = x.CloseTime,
                                               Id = x.Id,
                                               IsActive = x.IsActive
										   })
                                           .FirstOrDefault();
                return marketTiming;
            }
        }

        /// <summary>
        /// Gets All Market Timings
        /// </summary>
        /// <returns></returns>
        public List<MarketTimingViewModel> GetAllMarketTimings()
		{
            using (var context = new IntelStockExchange())
            {
                var day = DateTime.Now.DayOfWeek.ToString();
                var marketTiming = context.MarketTimings
                                           .Select(x => new MarketTimingViewModel()
                                           {
                                               Day = x.Day,
                                               StartTime = x.StartTime,
                                               CloseTime = x.CloseTime,
                                               Id = x.Id,
                                               IsActive = x.IsActive
                                           })
                                           .ToList();
                return marketTiming;
            }
        }

        /// <summary>
        /// Updates Market Timings
        /// </summary>
        /// <param name="StartTime"></param>
        /// <param name="CloseTime"></param>
        public void UpdateMarketTimings(string StartTime, string CloseTime)
		{
            using (var context = new IntelStockExchange())
			{
                var marketTimings = context.MarketTimings
                                           .ToList();
				foreach (var market in marketTimings)
				{
                    market.StartTime = TimeSpan.Parse(StartTime);
                    market.CloseTime = TimeSpan.Parse(CloseTime);
                }

                context.SaveChanges();
			}
        }

        /// <summary>
        /// Updates Market Days
        /// </summary>
        /// <param name="m"></param>
        /// <param name="t"></param>
        /// <param name="w"></param>
        /// <param name="th"></param>
        /// <param name="f"></param>
        /// <param name="sat"></param>
        /// <param name="sun"></param>
        public void UpdateMarketDays(bool m,bool t, bool w, bool th, bool f, bool sat, bool sun)
        {
            using (var context = new IntelStockExchange())
            {
                var marketTimings = context.MarketTimings
                                           .ToList();

                marketTimings.FirstOrDefault(x => x.Day == "Monday").IsActive = m;
                marketTimings.FirstOrDefault(x => x.Day == "Tuesday").IsActive = t;
                marketTimings.FirstOrDefault(x => x.Day == "Wednesday").IsActive = w;
                marketTimings.FirstOrDefault(x => x.Day == "Thursday").IsActive = th;
                marketTimings.FirstOrDefault(x => x.Day == "Friday").IsActive = f;
                marketTimings.FirstOrDefault(x => x.Day == "Saturday").IsActive = sat;
                marketTimings.FirstOrDefault(x => x.Day == "Sunday").IsActive = sun;

                context.SaveChanges();
            }
        }
    }
}