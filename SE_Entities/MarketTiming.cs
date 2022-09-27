namespace SE_Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MarketTiming
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Day { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan CloseTime { get; set; }

        public bool IsActive { get; set; }
    }
}
