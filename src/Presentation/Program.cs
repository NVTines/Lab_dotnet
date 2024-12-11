using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Application.Services;
using Application.Interfaces;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllersWithViews();
builder.Services.ConfigureCors();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();

// Cấu hình Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Cấu hình Authentication với Cookie
builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Account/Login";
        options.LogoutPath = "/Admin/Account/Logout";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();  // Add this line
app.UseSession();
app.UseAuthorization();

// Route mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Chuyển hướng trang mặc định đến Admin/Account/Login
app.MapGet("/", async context =>
{
    context.Response.Redirect("/Admin/Account/Login");
    await Task.CompletedTask;
});

// Seed dữ liệu vào cơ sở dữ liệu
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<BookStoreDbContextSeeder>();
    await seeder.InitialiseAsync();
    await seeder.SeedAsync();
}

app.Run();
