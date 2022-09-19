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
        public int Balance1 { get; set; }

        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
