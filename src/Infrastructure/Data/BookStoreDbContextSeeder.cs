using Microsoft.EntityFrameworkCore;
using Domain.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class BookStoreDbContextSeeder
    {
        private readonly BookStoreDbContext _context;

        public BookStoreDbContextSeeder(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task SeedAsync()
        {
            if (!_context.Books.Any())
            {
                var user = new User
                {
                    //Id = 1,
                    Username = "abc",
                    Password = "1234",
                    Email = "testuser@example.com",
                    PhoneNumber = "1234567890",
                    Address = "Hồ Chí Minh"
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var genres = new List<Genre>
                {
                    new Genre { Name = "Hư cấu", Description = "Sách hư cấu" },
                    new Genre { Name = "Phi hư cấu", Description = "Sách phi hư cấu" },
                    new Genre { Name = "Khoa học", Description = "Sách liên quan đến khoa học" }
                };
                _context.Genres.AddRange(genres);

                var books = new List<Book>
                {
                    new Book { Title = "Bắt trẻ đồng xanh", Author = "J.D. Salinger", Price = 9.99m, Genre = genres[0], Publisher = "Little, Brown and Company", CreatedOn = DateTime.Now },
                    new Book { Title = "Sapiens: Lược sử loài người", Author = "Yuval Noah Harari", Price = 14.99m, Genre = genres[1], Publisher = "Harper", CreatedOn = DateTime.Now },
                    new Book { Title = "Lược sử thời gian", Author = "Stephen Hawking", Price = 12.99m, Genre = genres[2], Publisher = "Bantam", CreatedOn = DateTime.Now }
                };
                _context.Books.AddRange(books);

                var catalogs = new List<Catalog>
                {
                    new Catalog { Title = "Bán chạy nhất", Description = "Những cuốn sách bán chạy nhất" },
                    new Catalog { Title = "Sách mới", Description = "Những cuốn sách mới phát hành" }
                };
                _context.Catalogs.AddRange(catalogs);

                var bookCatalogs = new List<BookCatalog>
                {
                    new BookCatalog { Book = books[0], Catalog = catalogs[0] },
                    new BookCatalog { Book = books[1], Catalog = catalogs[1] },
                    new BookCatalog { Book = books[2], Catalog = catalogs[1] }
                };
                _context.BookCatalogs.AddRange(bookCatalogs);

                var cart = new Cart
                {
                    UserId = user.Id,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                };
                _context.Carts.Add(cart);

                var cartItems = new List<CartItem>
                {
                    new CartItem { Cart = cart, Book = books[0], Quantity = 2 },
                    new CartItem { Cart = cart, Book = books[1], Quantity = 1 }
                };
                _context.CartItems.AddRange(cartItems);
                var order = new Order
                {
                    UserId = user.Id,
                    TotalAmount = 24.98m,
                    OrderDate = DateTime.Now,
                    Status = "Đang chờ xử lý",
                    ShippingAddress = "123 Đường Hư cấu",
                    PaymentMethod = "Thẻ tín dụng"
                };
                _context.Orders.Add(order);

                var orderItems = new List<OrderItem>
                {
                    new OrderItem { Order = order, Book = books[0], Quantity = 1, Price = 9.99m },
                    new OrderItem { Order = order, Book = books[1], Quantity = 1, Price = 14.99m }
                };
                _context.OrderItems.AddRange(orderItems);

                await _context.SaveChangesAsync();
            }
            //if (!_context.Users.Any())
            //{
            //    var user = new User
            //    {
            //        Id = 1,
            //        Username = "abc",
            //        Password = "1234",
            //        Email = "testuser@example.com",
            //        PhoneNumber = "1234567890",
            //        Address = "Hồ Chí Minh"
            //    };
            //    _context.Users.Add(user);
            //    if (!_context.Genres.Any())
            //    {
            //        var genres = new List<Genre>
            //        {
            //            new Genre { Name = "Hư cấu", Description = "Sách hư cấu" },
            //            new Genre { Name = "Phi hư cấu", Description = "Sách phi hư cấu" },
            //            new Genre { Name = "Khoa học", Description = "Sách liên quan đến khoa học" }
            //        };
            //        _context.Genres.AddRange(genres);
            //        if (!_context.Books.Any())
            //        {
            //            var books = new List<Book>
            //            {
            //                new Book { Title = "Bắt trẻ đồng xanh", Author = "J.D. Salinger", Quantity=10, Price = 9.99m, Genre = genres[0], Publisher = "Little, Brown and Company", CreatedOn = DateTime.Now },
            //                new Book { Title = "Sapiens: Lược sử loài người", Author = "Yuval Noah Harari", Quantity=10, Price = 14.99m, Genre = genres[1], Publisher = "Harper", CreatedOn = DateTime.Now },
            //                new Book { Title = "Lược sử thời gian", Author = "Stephen Hawking", Price = 12.99m,Quantity=10, Genre = genres[2], Publisher = "Bantam", CreatedOn = DateTime.Now }
            //            };
            //            _context.Books.AddRange(books);
            //            if (!_context.Catalogs.Any())
            //            {
            //                var catalogs = new List<Catalog>
            //                {
            //                    new Catalog { Title = "Bán chạy nhất", Description = "Những cuốn sách bán chạy nhất" },
            //                    new Catalog { Title = "Sách mới", Description = "Những cuốn sách mới phát hành" }
            //                };
            //                _context.Catalogs.AddRange(catalogs);
            //                if (!_context.Catalogs.Any())
            //                {
            //                    var bookCatalogs = new List<BookCatalog>
            //                {
            //                    new BookCatalog { Book = books[0], Catalog = catalogs[0] },
            //                    new BookCatalog { Book = books[1], Catalog = catalogs[1] },
            //                    new BookCatalog { Book = books[2], Catalog = catalogs[1] }
            //                };
            //                    _context.BookCatalogs.AddRange(bookCatalogs);
            //                }
            //            }
            //            if (!_context.Carts.Any())
            //            {
            //                var cart = new Cart
            //                {
            //                    UserId = user.Id,
            //                    CreatedOn = DateTime.Now,
            //                    UpdatedOn = DateTime.Now
            //                };
            //                _context.Carts.Add(cart);
            //                if (!_context.Carts.Any())
            //                {
            //                    var cartItems = new List<CartItem>
            //                {
            //                    new CartItem { Cart = cart, Book = books[0], Quantity = 2 },
            //                    new CartItem { Cart = cart, Book = books[1], Quantity = 1 }
            //                };
            //                    _context.CartItems.AddRange(cartItems);
            //                }
            //            }
            //            if (!_context.Orders.Any())
            //            {
            //                var order = new Order
            //                {
            //                    UserId = user.Id,
            //                    TotalAmount = 24.98m,
            //                    OrderDate = DateTime.Now,
            //                    Status = "Đang chờ xử lý",
            //                    ShippingAddress = "123 Đường Hư cấu",
            //                    PaymentMethod = "Thẻ tín dụng"
            //                };
            //                _context.Orders.Add(order);
            //                if (!_context.OrderItems.Any())
            //                {
            //                    var orderItems = new List<OrderItem>
            //                {
            //                    new OrderItem { Order = order, Book = books[0], Quantity = 1, Price = 9.99m },
            //                    new OrderItem { Order = order, Book = books[1], Quantity = 1, Price = 14.99m }
            //                };
            //                    _context.OrderItems.AddRange(orderItems);
            //                }
            //            }
            //        }
            //    }
            //    await _context.SaveChangesAsync();
            //}
        }
    }
}