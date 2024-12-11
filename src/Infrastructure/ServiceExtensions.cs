using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;

namespace Infrastructure
{
	public static class ServiceExtensions
    {
		public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
		{
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Connection string is not configured.");

            var builder = new SqlConnectionStringBuilder(connectionString);

            services.AddDbContext<BookStoreDbContext>(options =>
                options.UseSqlServer(builder.ConnectionString, sqlServerOptions =>
                    sqlServerOptions.MigrationsAssembly(typeof(BookStoreDbContext).Assembly.FullName)));

            return services;
        }

		public static IServiceCollection ConfigureCors(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", builder =>
				{
					builder.AllowAnyOrigin()
						   .AllowAnyMethod()
						   .AllowAnyHeader();
				});
			});

			return services;
		}
        
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
			return services
				.AddScoped<IUnitOfWork, UnitOfWork>()
				.AddScoped<BookStoreDbContextSeeder>()
				.AddTransient<IBookService, BookService>()
				.AddTransient<IBookRepository, BookRepository>()
				.AddTransient<IGenresService, GenresService>()
				.AddTransient<IGenreRepository, GenreRepository>()
				.AddTransient<IUserService, UserService>()
				.AddTransient<IUserRepository, UserRepository>()
				.AddTransient<ICartService, CartService>()
				.AddTransient<ICartRepository, CartRepository>()
				.AddTransient<ICartItemService, CartItemService>()
				.AddTransient<ICartItemRepository, CartItemRepository>();
			    //.AddTransient<IOrderItemService, OrderItemService>()
                //.AddTransient<IOrderService, OrderService>();
        }
    }
}