using CineShop.DataBase;
using CineShop.Models;
using CineShop.Services;
using Microsoft.EntityFrameworkCore;

namespace CineShop
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllersWithViews();

            // Session support for cart functionality
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICartService, CartService>();

            // Database connection
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Default Connection not found");

            builder.Services.AddDbContext<MovieDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Application services
            builder.Services.AddScoped<IStatisticsService, StatisticsService>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            var app = builder.Build();

            // ?? Seed the database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<MovieDbContext>();

                // Apply migrations and seed data
                context.Database.Migrate();
                await SeedData.InitializeAsync(context);
            }

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            await app.RunAsync();
        }
    }
}

