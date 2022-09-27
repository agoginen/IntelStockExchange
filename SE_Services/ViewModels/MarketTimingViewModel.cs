using System;

namespace SE_Services.ViewModels
{
	public class MarketTimingViewModel
	{
        public int Id { get; set; }

        public string Day { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan CloseTime { get; set; }

        public bool IsActive { get; set; }
    }
}