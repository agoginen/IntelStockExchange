namespace SE_Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Balances = new HashSet<Balance>();
            UserStocks = new HashSet<UserStock>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string EmailAddress { get; set; }

        public int UserType { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public DateTime? DateTimeUpdated { get; set; }

        public DateTime? DateTimeDeleted { get; set; }

        public DateTime DateTimeAdded { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Balance> Balances { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserStock> UserStocks { get; set; }
    }
}
