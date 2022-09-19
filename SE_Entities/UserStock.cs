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

        public virtual User User { get; set; }
    }
}
