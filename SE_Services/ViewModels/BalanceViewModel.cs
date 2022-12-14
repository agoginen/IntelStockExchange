using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE_Services.ViewModels
{
	public class BalanceViewModel
	{
        public int Id { get; set; }

        public decimal Balance { get; set; }

        public int UserId { get; set; }

        public DateTime DateTimeAdded { get; set; }

        public DateTime? DateTimeUpdated { get; set; }

        public DateTime? DateTimeDeleted { get; set; }

        public bool IsWithdraw { get; set; }
    }
}