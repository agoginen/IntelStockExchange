namespace SE_Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Stock
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string StockName { get; set; }

        public decimal? HighPrice { get; set; }

        public decimal? LowPrice { get; set; }

        public DateTime DateTimeAdded { get; set; }

        public DateTime? DateTimeUpdated { get; set; }

        public DateTime? DateTimeDeleted { get; set; }
    }
}
