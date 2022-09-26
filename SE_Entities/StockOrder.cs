namespace SE_Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StockOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StockOrder()
        {
            UserStocks = new HashSet<UserStock>();
        }

        public int Id { get; set; }

        public int StockId { get; set; }

        public int StockCount { get; set; }

        public decimal OrderStockPrice { get; set; }

        public bool IsLimitOrder { get; set; }

        public bool IsOrderExecuted { get; set; }

        public int UserId { get; set; }

        public DateTime DateTimeAdded { get; set; }

        public DateTime? DateTimeUpdated { get; set; }

        public DateTime? DateTimeDeleted { get; set; }

        public bool IsBuyOrder { get; set; }

        public bool IsActive { get; set; }

        public decimal? NewAverageStockPrice { get; set; }

        public virtual Stock Stock { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserStock> UserStocks { get; set; }
    }
}
