using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SE_Services.ViewModels
{
	public class StockViewModel
	{
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string StockName { get; set; }

        public int StockCount { get; set; }

        public decimal? Price { get; set; }

        public decimal? HighPrice { get; set; }

        public decimal? LowPrice { get; set; }

        public decimal? StartPrice { get; set; }

        public decimal? ExecutedPrice { get; set; }

        public int Volume { get; set; }

        public decimal MarketCapitalization { get; set; }

        public decimal? Gain { get; set; }
        
        public decimal? GainPercetage { get; set; }

        public decimal CurrentValue { get; set; }

        public DateTime DateTimeAdded { get; set; }

        public DateTime? DateTimeUpdated { get; set; }

        public DateTime? DateTimeDeleted { get; set; }
    }
}