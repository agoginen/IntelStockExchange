using System;

namespace SE_Services.ViewModels
{
	public class StockOrderViewModel
	{
        public int Id { get; set; }

        public int StockId { get; set; }

        public string StockName { get; set; }

        public int StockCount { get; set; }

        public decimal OrderStockPrice { get; set; }

        public bool IsLimitOrder { get; set; }

        public String OrderType { get; set; }

        public bool IsOrderExecuted { get; set; }

        public string OrderStatus { get; set; }

        public int UserId { get; set; }

        public bool IsBuyOrder { get; set; }

        public string BuySellType { get; set; }

        public DateTime DateTimeAdded { get; set; }

        public DateTime? DateTimeUpdated { get; set; }

        public DateTime? DateTimeDeleted { get; set; }
    }
}