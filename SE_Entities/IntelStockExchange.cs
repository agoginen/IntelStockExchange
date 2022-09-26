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
		public virtual DbSet<StockOrder> StockOrders { get; set; }
		public virtual DbSet<Stock> Stocks { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<UserStock> UserStocks { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Balance>()
				.Property(e => e.Balance1)
				.HasPrecision(19, 4);

			modelBuilder.Entity<StockOrder>()
				.Property(e => e.OrderStockPrice)
				.HasPrecision(19, 4);

			modelBuilder.Entity<StockOrder>()
				.Property(e => e.NewAverageStockPrice)
				.HasPrecision(19, 4);

			modelBuilder.Entity<StockOrder>()
				.HasMany(e => e.UserStocks)
				.WithRequired(e => e.StockOrder)
				.WillCascadeOnDelete(false);

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
				.Property(e => e.StartPrice)
				.HasPrecision(19, 4);

			modelBuilder.Entity<Stock>()
				.HasMany(e => e.StockOrders)
				.WithRequired(e => e.Stock)
				.WillCascadeOnDelete(false);

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
				.HasMany(e => e.Balances)
				.WithRequired(e => e.User)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.StockOrders)
				.WithRequired(e => e.User)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<User>()
				.HasMany(e => e.UserStocks)
				.WithRequired(e => e.User)
				.WillCascadeOnDelete(false);
		}
	}
}
