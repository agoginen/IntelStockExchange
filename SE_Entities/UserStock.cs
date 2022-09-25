namespace SE_Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserStock
    {
        public int Id { get; set; }

        public int StockId { get; set; }

        public int StockCount { get; set; }

        public int UserId { get; set; }

        public DateTime DateTimeAdded { get; set; }

        public DateTime? DateTimeUpdated { get; set; }

        public DateTime? DateTimeDeleted { get; set; }

        public int StockOrderId { get; set; }

        public virtual StockOrder StockOrder { get; set; }

        public virtual Stock Stock { get; set; }

        public virtual User User { get; set; }
    }
}
