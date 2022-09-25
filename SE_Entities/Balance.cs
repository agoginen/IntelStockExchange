namespace SE_Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Balance
    {
        public int Id { get; set; }

        [Column("Balance")]
        public decimal Balance1 { get; set; }

        public int UserId { get; set; }

        public DateTime DateTimeAdded { get; set; }

        public DateTime? DateTimeUpdated { get; set; }

        public DateTime? DateTimeDeleted { get; set; }

        public bool IsWithdraw { get; set; }

        public virtual User User { get; set; }
    }
}
