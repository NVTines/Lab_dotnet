using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class BookStoreDbContext: DbContext
    {
		public DbSet<Book> Books { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Catalog> Catalogs { get; set; }
		public DbSet<BookCatalog> BookCatalogs { get; set; }
		public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Book>()
				.HasOne(b => b.Genre)
				.WithMany(g => g.Books)
				.HasForeignKey(b => b.GenreId);

			modelBuilder.Entity<CartItem>()
				.HasOne(ci => ci.Cart)
				.WithMany(c => c.CartItems)
				.HasForeignKey(ci => ci.CartId);

			modelBuilder.Entity<OrderItem>()
				.HasOne(oi => oi.Order)
				.WithMany(o => o.OrderItems)
				.HasForeignKey(oi => oi.OrderId);

			modelBuilder.Entity<BookCatalog>()
				.HasOne(bc => bc.Book)
				.WithMany(b => b.BookCatalogs)
				.HasForeignKey(bc => bc.BookId);

			modelBuilder.Entity<BookCatalog>()
				.HasOne(bc => bc.Catalog)
				.WithMany(c => c.BookCatalogs)
				.HasForeignKey(bc => bc.CatalogId);

			modelBuilder.Entity<Cart>()
				.HasOne(c => c.User)
				.WithMany(u => u.Carts)
				.HasForeignKey(c => c.UserId);

			modelBuilder.Entity<Order>()
				.HasOne(o => o.User)
				.WithMany(u => u.Orders)
				.HasForeignKey(o => o.UserId);

			base.OnModelCreating(modelBuilder);
		}
    }
}
