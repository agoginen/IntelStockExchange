using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SE_Entities
{
	public partial class IntelStockExchange : DbContext
	{
		public IntelStockExchange()
			: base("name=IntelStockExchange")
		{
		}

		public virtual DbSet<Balance> Balances { get; set; }
		public virtual DbSet<Stock> Stocks { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<UserStock> UserStocks { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Stock>()
				.Property(e => e.StockName)
				.IsUnicode(false);

			modelBuilder.Entity<Stock>()
				.Property(e => e.HighPrice)
				.HasPrecision(19, 4);

			modelBuilder.Entity<Stock>()
				.Property(e => e.LowPrice)
				.HasPrecision(19, 4);

			modelBuilder.Entity<Stock>()
				.Property(e => e.Price)
				.HasPrecision(19, 4);

			modelBuilder.Entity<Stock>()
				.HasMany(e => e.UserStocks)
				.WithRequired(e => e.Stock)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.Property(e => e.UserName)
				.IsFixedLength();

			modelBuilder.Entity<User>()
				.Property(e => e.EmailAddress)
				.IsFixedLength();

			modelBuilder.Entity<User>()
				.Property(e => e.Password)
				.IsFixedLength();

			modelBuilder.Entity<User>()
				.HasMany(e => e.UserStocks)
				.WithRequired(e => e.User)
				.WillCascadeOnDelete(false);
		}
	}
}
